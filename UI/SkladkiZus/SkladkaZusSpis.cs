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
	class SkladkaZusSpis : Spis<SkladkaZus>
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
					podsumowanie += $"\nRazem: <{WybraneRekordy.Sum(skladka => skladka.SumaSkladek).ToString(Format.Kwota)}>";
					podsumowanie += $"\nRazem społeczne: <{WybraneRekordy.Sum(skladka => skladka.SkladkaSpoleczna).ToString(Format.Kwota)}>";
					podsumowanie += $"\nRazem zdrowotne: <{WybraneRekordy.Sum(skladka => skladka.SkladkaZdrowotna).ToString(Format.Kwota)}>";
				}
				return podsumowanie;
			}
		}

		public SkladkaZusSpis()
		{
			DodajKolumne(nameof(SkladkaZus.MiesiacFmt), "Miesiąc");
			DodajKolumneKwota(nameof(SkladkaZus.SkladkaSpoleczna), "Składka społeczna", szerokosc: 150);
			DodajKolumneKwota(nameof(SkladkaZus.SkladkaZdrowotna), "Składka zdrowotna", szerokosc: 150);
			DodajKolumneKwota(nameof(SkladkaZus.SumaSkladek), "Razem", szerokosc: 150);
			DodajKolumneId();
		}

		public SkladkaZusSpis(string[]? parametry)
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
			var q = Kontekst.Baza.SkladkiZus;
			if (odDaty.HasValue) q = q.Where(skladka => skladka.Miesiac >= odDaty.Value);
			if (doDaty.HasValue) q = q.Where(skladka => skladka.Miesiac < doDaty.Value);
			Rekordy = q.OrderBy(skladka => skladka.Miesiac).ToList();
		}
	}
}
