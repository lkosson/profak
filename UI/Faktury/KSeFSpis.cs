using Microsoft.EntityFrameworkCore;
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
	class KSeFSpis : Spis<Faktura>
	{
		private readonly bool sprzedaz;
		private readonly DataGridViewTextBoxColumn kolumnaNazwaNabywcy;
		private readonly DataGridViewTextBoxColumn kolumnaNIPNabywcy;
		private readonly DataGridViewTextBoxColumn kolumnaNazwaSprzedawcy;
		private readonly DataGridViewTextBoxColumn kolumnaNIPSprzedawcy;
		public override string Podsumowanie
		{
			get
			{
				var podsumowanie = base.Podsumowanie;
				if (WybraneRekordy.Count() > 1)
				{
					podsumowanie += $"\nRazem netto: {WybraneRekordy.Sum(faktura => faktura.RazemNetto).ToString("n2")}";
					podsumowanie += $"\nRazem brutto: {WybraneRekordy.Sum(faktura => faktura.RazemBrutto).ToString("n2")}";
				}
				return podsumowanie;
			}
		}


		public KSeFSpis()
		{
			DodajKolumne(nameof(Faktura.Numer), "Numer");
			DodajKolumne(nameof(Faktura.RodzajFmt), "Rodzaj");
			DodajKolumne(nameof(Faktura.DataWystawienia), "Data wystawienia", format: "yyyy-MM-dd", szerokosc: 120);
			DodajKolumne(nameof(Faktura.DataSprzedazy), "Data sprzedaży", format: "yyyy-MM-dd", szerokosc: 120);
			kolumnaNazwaNabywcy = DodajKolumne(nameof(Faktura.NazwaNabywcy), "Nabywca", rozciagnij: true);
			kolumnaNIPNabywcy = DodajKolumne(nameof(Faktura.NIPNabywcy), "NIP nabywcy", szerokosc: 120);
			kolumnaNazwaSprzedawcy = DodajKolumne(nameof(Faktura.NazwaSprzedawcy), "Sprzedawca", rozciagnij: true);
			kolumnaNIPSprzedawcy = DodajKolumne(nameof(Faktura.NIPSprzedawcy), "NIP sprzedawcy", szerokosc: 120);
			DodajKolumneKwota(nameof(Faktura.RazemNetto), "Netto");
			DodajKolumneKwota(nameof(Faktura.RazemVat), "VAT");
			DodajKolumneKwota(nameof(Faktura.RazemBrutto), "Brutto");
			DodajKolumne(nameof(Faktura.WalutaFmt), "Waluta", szerokosc: 70);
			DodajKolumne(nameof(Faktura.NumerKSeF), "Id", szerokosc: 200);
		}

		public KSeFSpis(string[] parametry)
			: this()
		{
			if (parametry == null) return;
			foreach (var parametr in parametry)
			{
				if (parametr == "Sprzedaz") sprzedaz = true;
				else if (parametr == "Zakup") sprzedaz = false;
			}
		}

		protected override void Przeladuj()
		{
			kolumnaNazwaNabywcy.Visible = sprzedaz;
			kolumnaNIPNabywcy.Visible = sprzedaz;
			kolumnaNazwaSprzedawcy.Visible = !sprzedaz;
			kolumnaNIPSprzedawcy.Visible = !sprzedaz;

			Rekordy = new Faktura[0];
		}
	}
}
