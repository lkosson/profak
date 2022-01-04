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
		public override string Podsumowanie
		{
			get
			{
				var podsumowanie = base.Podsumowanie;
				if (WybraneRekordy.Count() > 1)
				{
					podsumowanie += $"\nRazem: {WybraneRekordy.Sum(skladka => skladka.SumaSkladek).ToString("n2")}";
				}
				return podsumowanie;
			}
		}

		public SkladkaZusSpis()
		{
			DodajKolumne(nameof(SkladkaZus.MiesiacFmt), "Miesiąc");
			DodajKolumne(nameof(SkladkaZus.SkladkaSpoleczna), "Składka społeczna", wyrownajDoPrawej: true, format: "#,##0.00", szerokosc: 150);
			DodajKolumne(nameof(SkladkaZus.SkladkaZdrowotna), "Składka zdrowotna", wyrownajDoPrawej: true, format: "#,##0.00", szerokosc: 150);
			DodajKolumne(nameof(SkladkaZus.SumaSkladek), "Razem", wyrownajDoPrawej: true, format: "#,##0.00", szerokosc: 150);
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.SkladkiZus.OrderBy(skladka => skladka.Miesiac).ToList();
		}
	}
}
