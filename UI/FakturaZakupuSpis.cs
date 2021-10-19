using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			DodajKolumne(nameof(Faktura.RazemNetto), "Netto", wyrownajDoPrawej: true, format: "#,##0.00");
			DodajKolumne(nameof(Faktura.RazemVat), "VAT", wyrownajDoPrawej: true, format: "#,##0.00");
			DodajKolumne(nameof(Faktura.RazemBrutto), "Brutto", wyrownajDoPrawej: true, format: "#,##0.00");
			DodajKolumne(nameof(Faktura.Waluta), "Waluta");
			DodajKolumne(nameof(Faktura.Id), "Id", wyrownajDoPrawej: true);
		}

		public override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.Faktury
				.Include(faktura => faktura.Waluta)
				.ToList();
		}
	}
}
