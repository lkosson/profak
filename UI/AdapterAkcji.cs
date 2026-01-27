using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	abstract class AdapterAkcji
	{
		public abstract string Nazwa { get; }
		public abstract bool CzyDostepna { get; }
		public abstract bool CzyDomyslna { get; }
		public abstract bool CzyGlobalna { get; }
		public abstract string NazwaBezSkrotu { get; }
		public abstract string Skrot { get; }
		public abstract IReadOnlyCollection<AdapterAkcji> Podrzedne { get; }

		public abstract void Uruchom();
		public abstract bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory);
	}

	class AdapterAkcji<TRekord> : AdapterAkcji
		where TRekord : Rekord<TRekord>
	{
		private readonly Regex opisSkrotuRegex = new Regex(@"(?<nazwa>[^[]+)(\[(?<skrot>.+)\])?");
		private readonly AkcjaNaSpisie<TRekord> akcja;
		private readonly Spis<TRekord> spis;
		private readonly List<AdapterAkcji<TRekord>> podrzedne;

		public override string Nazwa => akcja.Nazwa;
		public override bool CzyDostepna => akcja.CzyDostepnaDlaRekordow(spis.WybraneRekordy);
		public override bool CzyDomyslna => akcja.CzyKlawiszSkrotu(Keys.Enter, Keys.None);
		public override bool CzyGlobalna => akcja.CzyDostepnaDlaRekordow([]);
		public override string NazwaBezSkrotu => opisSkrotuRegex.Match(Nazwa) is var match ? match.Groups["nazwa"].Value : Nazwa;
		public override string Skrot => opisSkrotuRegex.Match(Nazwa) is var match && match.Groups["skrot"].Success ? match.Groups["skrot"].Value : "";
		public override IReadOnlyCollection<AdapterAkcji> Podrzedne => podrzedne;
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => akcja.CzyKlawiszSkrotu(klawisz, modyfikatory);

		public AdapterAkcji(AkcjaNaSpisie<TRekord> akcja, Spis<TRekord> spis)
		{
			this.akcja = akcja;
			this.spis = spis;
			this.podrzedne = akcja.Podrzedne.Select(podrzedna => (AdapterAkcji<TRekord>)podrzedna.UtworzAdapter(spis)).ToList();
		}

		public override void Uruchom()
		{
			try
			{
				if (!CzyDostepna) return;
				IEnumerable<TRekord> wybraneRekordy = spis.WybraneRekordy.ToList();
				akcja.Uruchom(spis.Kontekst, ref wybraneRekordy);
				if (spis.Kontekst.Dialog != null && spis.Kontekst.Dialog.DialogResult != DialogResult.None) return;
				if (akcja.PrzeladujPoZakonczeniu) spis.PrzeladujBezpiecznie();
				spis.WybraneRekordy = wybraneRekordy;
				spis.Focus();
			}
			catch (Exception exc)
			{
				OknoBledu.Pokaz(exc);
			}
		}
	}
}
