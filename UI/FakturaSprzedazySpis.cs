using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
