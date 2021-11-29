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
	class FakturaZakupuSpis : Spis<Faktura>
	{
		public FakturaZakupuSpis()
		{
			DodajKolumne(nameof(Faktura.Numer), "Numer");
			DodajKolumne(nameof(Faktura.DataWystawienia), "Data wystawienia", format: "yyyy-MM-dd", szerokosc: 120);
			DodajKolumne(nameof(Faktura.DataSprzedazy), "Data sprzedaży", format: "yyyy-MM-dd", szerokosc: 120);
			DodajKolumne(nameof(Faktura.NazwaSprzedawcy), "Sprzedawca", rozciagnij: true);
			DodajKolumne(nameof(Faktura.NIPSprzedawcy), "NIP sprzedawcy", rozciagnij: true);
			DodajKolumneKwota(nameof(Faktura.RazemNetto), "Netto");
			DodajKolumneKwota(nameof(Faktura.RazemVat), "VAT");
			DodajKolumneKwota(nameof(Faktura.RazemBrutto), "Brutto");
			DodajKolumne(nameof(Faktura.WalutaFmt), "Waluta", szerokosc: 70);
			DodajKolumneId();
		}

		public override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.Faktury
				.Where(faktura => faktura.Rodzaj == RodzajFaktury.Zakup || faktura.Rodzaj == RodzajFaktury.KorektaZakupu)
				.Include(faktura => faktura.Waluta)
				.ToList();
		}

		protected override void UstawStylWiersza(Faktura rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.FakturaKorygujacaRef.IsNotNull) styl.ForeColor = Color.Gray;
			else if (rekord.Rodzaj == RodzajFaktury.KorektaZakupu) styl.ForeColor = Color.DarkBlue;
		}
	}
}
