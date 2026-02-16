using Microsoft.EntityFrameworkCore;
using ProFak.DB;

namespace ProFak.UI
{
	class FakturaUsunietaSpis : Spis<Faktura>
	{
		public FakturaUsunietaSpis()
		{
			DodajKolumne(nameof(Faktura.NumerPrzedUsunieciem), "Numer");
			DodajKolumne(nameof(Faktura.RodzajFmt), "Rodzaj", szerokosc: 0);
			DodajKolumneData(nameof(Faktura.DataWystawienia), "Data wystawienia");
			DodajKolumneData(nameof(Faktura.DataSprzedazy), "Data sprzedaży");
			DodajKolumneData(nameof(Faktura.DataWprowadzenia), "Data wprowadzenia");
			DodajKolumneData(nameof(Faktura.DataUsuniecia), "Data usunięcia", tooltip: faktura => faktura.DataUsuniecia?.ToString("yyyy-MM-dd HH:mm:ss"));
			DodajKolumne(nameof(Faktura.NazwaNabywcy), "Nabywca", szerokosc: 250);
			DodajKolumne(nameof(Faktura.NIPNabywcy), "NIP nabywcy", szerokosc: 100);
			DodajKolumne(nameof(Faktura.NazwaSkroconaNabywcy), "Kontahent", szerokosc: 0);
			DodajKolumne(nameof(Faktura.NazwaSprzedawcy), "Sprzedawca", szerokosc: 250);
			DodajKolumne(nameof(Faktura.NIPSprzedawcy), "NIP sprzedawcy", szerokosc: 100);
			DodajKolumne(nameof(Faktura.NazwaSkroconaSprzedawcy), "Kontahent", szerokosc: 0);
			DodajKolumneKwota(nameof(Faktura.RazemNetto), "Netto");
			DodajKolumneKwota(nameof(Faktura.RazemVat), "VAT");
			DodajKolumneKwota(nameof(Faktura.RazemBrutto), "Brutto");
			DodajKolumne(nameof(Faktura.WalutaFmt), "Waluta", szerokosc: 70);
			DodajKolumneBool(nameof(Faktura.CzyZaplacona), "Zapł.", szerokosc: 50, tooltip: faktura => faktura.SumaWplat.ToString(Format.Kwota));
			DodajKolumneBool(nameof(Faktura.CzyKSeF), "KSeF", szerokosc: 50, tooltip: faktura => faktura.NumerKSeF);
			DodajKolumneBool(nameof(Faktura.CzyPliki), "Pliki", szerokosc: 50, tooltip: faktura => String.Join("\n", faktura.Pliki.Select(e => e.Nazwa)));
			DodajKolumne(nameof(Faktura.PozycjeFmt), "Pozycje", szerokosc: 150);
			DodajKolumne(nameof(Faktura.UwagiPubliczne), "Uwagi (publiczne)", szerokosc: 150);
			DodajKolumne(nameof(Faktura.UwagiWewnetrzne), "Uwagi (wewnętrzne)", szerokosc: 150);
			DodajKolumne(nameof(Faktura.OpisZdarzenia), "Opis KPiR", szerokosc: 0);
			DodajKolumneKwota(nameof(Faktura.SumaWplat), "Zapłacono", szerokosc: 0);
			DodajKolumneKwota(nameof(Faktura.PozostaloDoZaplaty), "Do zapłaty", szerokosc: 0);
			DodajKolumneKwota(nameof(Faktura.RazemRabat), "Rabat", szerokosc: 0);
			DodajKolumneData(nameof(Faktura.TerminPlatnosci), "Termin płatności");
			DodajKolumne(nameof(Faktura.DniPoTerminie), "Dni po terminie", szerokosc: 0);
			DodajKolumneData(nameof(Faktura.DataWplywu), "Data zapłaty");
			DodajKolumne(nameof(Faktura.NumerPowiazanej), "Powiązana");
			DodajKolumne(nameof(Faktura.NumerKSeF), "Numer KSeF", szerokosc: 230);
			DodajKolumneBool(nameof(Faktura.CzyTP), "TP", szerokosc: 0);
			DodajKolumneBool(nameof(Faktura.CzyWNT), "WNT", szerokosc: 0);
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			var q = Kontekst.Baza.Faktury
				.Include(faktura => faktura.Waluta)
				.Include(faktura => faktura.Wplaty)
				.Include(faktura => faktura.FakturaKorygowana)
				.Include(faktura => faktura.FakturaKorygujaca)
				.Include(faktura => faktura.Pozycje)
				.Include(faktura => faktura.Pliki)
				.Include(faktura => faktura.Sprzedawca)
				.Where(faktura => faktura.Rodzaj == RodzajFaktury.Usunięta);
			q = q.OrderBy(faktura => faktura.DataUsuniecia).ThenBy(faktura => faktura.Id);
			Rekordy = q.ToList();
		}
	}
}
