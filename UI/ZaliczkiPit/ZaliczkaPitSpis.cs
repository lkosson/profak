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
	class ZaliczkaPitSpis : Spis<ZaliczkaPit>
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
					podsumowanie += $"\nRazem podatek: {WybraneRekordy.Sum(zaliczka => zaliczka.Podatek).ToString(Format.Kwota)}";
					podsumowanie += $"\nRazem przychody: {WybraneRekordy.Sum(zaliczka => zaliczka.Przychody).ToString(Format.Kwota)}";
					podsumowanie += $"\nRazem koszty: {WybraneRekordy.Sum(zaliczka => zaliczka.Koszty).ToString(Format.Kwota)}";
				}
				return podsumowanie;
			}
		}

		public ZaliczkaPitSpis()
		{
			DodajKolumne(nameof(ZaliczkaPit.MiesiacFmt), "Miesiąc");
			DodajKolumneKwota(nameof(ZaliczkaPit.Przychody), "Przychody");
			DodajKolumneKwota(nameof(ZaliczkaPit.Koszty), "Koszty");
			DodajKolumneKwota(nameof(ZaliczkaPit.DoWplaty), "Do wpłaty");
			DodajKolumneId();
		}

		public ZaliczkaPitSpis(string[] parametry)
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
			var q = Kontekst.Baza.ZaliczkiPit;
			if (odDaty.HasValue) q = q.Where(zaliczka => zaliczka.Miesiac >= odDaty.Value);
			if (doDaty.HasValue) q = q.Where(zaliczka => zaliczka.Miesiac < doDaty.Value);
			Rekordy = q.OrderBy(zaliczka => zaliczka.Miesiac).ToList();
		}
	}
}
