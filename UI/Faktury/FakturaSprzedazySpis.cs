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
	class FakturaSprzedazySpis : Spis<Faktura>
	{
		public FakturaSprzedazySpis()
		{
			DodajKolumne(nameof(Faktura.Numer), "Numer");
			DodajKolumne(nameof(Faktura.DataWystawienia), "Data wystawienia", format: "yyyy-MM-dd", szerokosc: 120);
			DodajKolumne(nameof(Faktura.DataSprzedazy), "Data sprzedaży", format: "yyyy-MM-dd", szerokosc: 120);
			DodajKolumne(nameof(Faktura.NazwaNabywcy), "Nabywca", rozciagnij: true);
			DodajKolumne(nameof(Faktura.NIPNabywcy), "NIP nabywcy", rozciagnij: true);
			DodajKolumneKwota(nameof(Faktura.RazemNetto), "Netto");
			DodajKolumneKwota(nameof(Faktura.RazemVat), "VAT");
			DodajKolumneKwota(nameof(Faktura.RazemBrutto), "Brutto");
			DodajKolumne(nameof(Faktura.WalutaFmt), "Waluta");
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.Faktury
				.Where(faktura => faktura.Rodzaj == RodzajFaktury.Sprzedaż || faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży || faktura.Rodzaj == RodzajFaktury.Proforma)
				.Include(faktura => faktura.Waluta)
				.ToList();
		}

		protected override void UstawStylWiersza(Faktura rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.Rodzaj == RodzajFaktury.Proforma) styl.ForeColor = Color.DarkGreen;
			else if (rekord.FakturaKorygujacaRef.IsNotNull) styl.ForeColor = Color.Gray;
			else if (rekord.Rodzaj == RodzajFaktury.KorektaSprzedaży) styl.ForeColor = Color.DarkBlue;
		}
	}
}
