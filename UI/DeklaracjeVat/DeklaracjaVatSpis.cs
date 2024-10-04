using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class DeklaracjaVatSpis : Spis<DeklaracjaVat>
	{
		private readonly DateTime? odDaty;
		private readonly DateTime? doDaty; 

		public override string Podsumowanie
		{
			get
			{
				var podsumowanie = base.Podsumowanie;
				if (WybraneRekordy.Count() > 1)
				{
					podsumowanie += $"\nRazem do wpłaty: {WybraneRekordy.Sum(deklaracjaVat => deklaracjaVat.DoWplaty).ToString(Format.Kwota)}";
					podsumowanie += $"\nRazem należny: {WybraneRekordy.Sum(deklaracjaVat => deklaracjaVat.NaleznyRazem).ToString(Format.Kwota)}";
					podsumowanie += $"\nRazem naliczony: {WybraneRekordy.Sum(deklaracjaVat => deklaracjaVat.NaliczonyRazem).ToString(Format.Kwota)}";
				}
				return podsumowanie;
			}
		}

		public DeklaracjaVatSpis()
		{
			DodajKolumne(nameof(DeklaracjaVat.MiesiacFmt), "Miesiąc");
			DodajKolumneKwota(nameof(DeklaracjaVat.NettoRazem), "Podstawa", format: Format.KwotaBezGroszy);
			DodajKolumneKwota(nameof(DeklaracjaVat.NaleznyRazem), "Należny", format: Format.KwotaBezGroszy);
			DodajKolumneKwota(nameof(DeklaracjaVat.NaliczonyRazem), "Naliczony", format: Format.KwotaBezGroszy);
			DodajKolumneKwota(nameof(DeklaracjaVat.DoWplaty), "Do wpłaty", format: Format.KwotaBezGroszy);
			DodajKolumneId();
		}

		public DeklaracjaVatSpis(string[] parametry)
			: this()
		{
			if (parametry == null) return;
			int? rok = null;
			int? miesiac = null;
			foreach (var parametr in parametry)
			{
				if (parametr.StartsWith("R:")) rok = Int32.Parse(parametr[2..]);
				else if (parametr.StartsWith("M:")) miesiac = Int32.Parse(parametr[2..]);
			}
			if (!rok.HasValue) return;
			if (miesiac.HasValue)
			{
				odDaty = new DateTime(rok.Value, miesiac.Value, 1);
				doDaty = odDaty.Value.AddMonths(1);
			}
			else
			{
				odDaty = new DateTime(rok.Value, 1, 1);
				doDaty = odDaty.Value.AddYears(1);
			}
		}

		protected override void Przeladuj()
		{
			var q = Kontekst.Baza.DeklaracjeVat;
			if (odDaty.HasValue) q = q.Where(deklaracja => deklaracja.Miesiac >= odDaty.Value);
			if (doDaty.HasValue) q = q.Where(deklaracja => deklaracja.Miesiac < doDaty.Value);
			Rekordy = q.OrderBy(deklaracja => deklaracja.Miesiac).ToList();
		}
	}
}
