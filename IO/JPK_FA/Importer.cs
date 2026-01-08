using ProFak.DB;
using ProFak.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProFak.IO.JPK_FA;

class Importer
{
	public static void Wczytaj(Stream wejscie, Kontekst kontekst)
	{
		var xs = new XmlSerializer(typeof(JPK));
		var jpk = (JPK)xs.Deserialize(wejscie);

		var waluty = kontekst.Baza.Waluty.ToList();
		var stawkiVat = kontekst.Baza.StawkiVat.ToList();
		var sposobPlatnosci = kontekst.Baza.SposobyPlatnosci.FirstOrDefault(e => e.CzyDomyslny);

		var faktury = new Dictionary<string, Faktura>();
		foreach (var jpkFaktura in jpk.Faktura)
		{
			var faktura = kontekst.Baza.Faktury.FirstOrDefault(e => e.Numer == jpkFaktura.P_2A && e.NIPSprzedawcy == jpkFaktura.P_4B);
			if (faktura != null) continue;

			faktura = new Faktura();
			faktura.Numer = jpkFaktura.P_2A;
			faktura.DataWystawienia = jpkFaktura.P_1;
			faktura.NazwaNabywcy = jpkFaktura.P_3A;
			faktura.DaneNabywcy = jpkFaktura.P_3B;
			faktura.NazwaSprzedawcy = jpkFaktura.P_3C;
			faktura.DaneSprzedawcy = jpkFaktura.P_3D;
			faktura.NIPSprzedawcy = jpkFaktura.P_4B;
			faktura.NIPNabywcy = jpkFaktura.P_5B;
			faktura.DataSprzedazy = jpkFaktura.P_6;
			faktura.RazemBrutto = jpkFaktura.P_15;
			faktura.TerminPlatnosci = faktura.DataSprzedazy;
			faktura.Rodzaj = jpkFaktura.RodzajFaktury == JPKFakturaRodzajFaktury.KOREKTA ? RodzajFaktury.KorektaSprzedaży : RodzajFaktury.Sprzedaż;
			faktura.WalutaRef = waluty.FirstOrDefault(e => e.Nazwa == jpkFaktura.KodWaluty.ToString()) ?? waluty.FirstOrDefault(e => e.CzyDomyslna);
			faktura.UwagiPubliczne = jpkFaktura.PrzyczynaKorekty;
			faktura.SposobPlatnosciRef = sposobPlatnosci;
			if (jpkFaktura.P_106E_2) faktura.ProceduraMarzy = ProceduraMarży.BiuraPodróży;
			if (!String.IsNullOrEmpty(jpkFaktura.P_106E_3A) && jpkFaktura.P_106E_3A.Contains("używane")) faktura.ProceduraMarzy = ProceduraMarży.TowaryUżywane;
			if (!String.IsNullOrEmpty(jpkFaktura.P_106E_3A) && jpkFaktura.P_106E_3A.Contains("dzieła")) faktura.ProceduraMarzy = ProceduraMarży.DziełaSztuki;
			if (!String.IsNullOrEmpty(jpkFaktura.P_106E_3A) && jpkFaktura.P_106E_3A.Contains("kolekcjonerskie")) faktura.ProceduraMarzy = ProceduraMarży.PrzedmiotyKolekcjonerskie;

			if (jpkFaktura.P_106E_2 || jpkFaktura.P_106E_3)
			{
				if (faktura.Rodzaj == RodzajFaktury.Sprzedaż) faktura.Rodzaj = RodzajFaktury.VatMarża;
				if (faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży) faktura.Rodzaj = RodzajFaktury.KorektaVatMarży;
			}

			var sprzedawca = kontekst.Baza.Kontrahenci.FirstOrDefault(e => e.NIP == jpkFaktura.P_4B);
			if (sprzedawca == null)
			{
				sprzedawca = new Kontrahent();
				sprzedawca.NIP = jpkFaktura.P_4B;
				sprzedawca.Nazwa = jpkFaktura.P_3C;
				sprzedawca.AdresRejestrowy = jpkFaktura.P_3D;
				faktura.Sprzedawca = sprzedawca;
			}
			else
			{
				faktura.SprzedawcaRef = sprzedawca;
			}

			var nabywca = kontekst.Baza.Kontrahenci.FirstOrDefault(e => e.NIP == jpkFaktura.P_5B);
			if (nabywca == null && !String.IsNullOrEmpty(jpkFaktura.P_5B))
			{
				nabywca = new Kontrahent();
				nabywca.NIP = jpkFaktura.P_5B;
				nabywca.Nazwa = jpkFaktura.P_3A;
				nabywca.AdresRejestrowy = jpkFaktura.P_3B;
				faktura.Nabywca = nabywca;
			}
			else
			{
				faktura.NabywcaRef = nabywca;
			}

			kontekst.Baza.Zapisz(faktura);

			faktura.PrzeliczRazem(kontekst.Baza);
			faktury[faktura.Numer] = faktura;
		}

		foreach (var jpkPozycja in jpk.FakturaWiersz)
		{
			if (!faktury.TryGetValue(jpkPozycja.P_2B, out var faktura)) continue;

			var pozycja = new PozycjaFaktury();

			pozycja.FakturaRef = faktura;
			pozycja.Opis = jpkPozycja.P_7;
			pozycja.Ilosc = jpkPozycja.P_8B;
			pozycja.CenaNetto = jpkPozycja.P_9A;
			pozycja.CenaBrutto = jpkPozycja.P_9B;
			pozycja.WartoscNetto = jpkPozycja.P_11;
			pozycja.WartoscBrutto = jpkPozycja.P_11A;
			if (!jpkPozycja.P_12Specified) pozycja.StawkaVatRef = stawkiVat.FirstOrDefault(e => e.Wartosc == (int)Decimal.Round((pozycja.WartoscBrutto - pozycja.WartoscNetto) / pozycja.WartoscNetto * 100));
			else if (jpkPozycja.P_12 == JPKFakturaWierszP_12.np) pozycja.StawkaVatRef = stawkiVat.FirstOrDefault(e => e.Skrot == "NP");
			else if (jpkPozycja.P_12 == JPKFakturaWierszP_12.zw) pozycja.StawkaVatRef = stawkiVat.FirstOrDefault(e => e.Skrot == "ZW");
			else if (jpkPozycja.P_12.ToString().StartsWith("Item")) pozycja.StawkaVatRef = stawkiVat.FirstOrDefault(e => e.Wartosc == Int32.Parse(jpkPozycja.P_12.ToString().Substring(4)));
			pozycja.PrzeliczCeny(kontekst.Baza);

			kontekst.Baza.Zapisz(pozycja);
		}

		foreach (var faktura in faktury.Values)
		{
			faktura.PrzeliczRazem(kontekst.Baza);
			kontekst.Baza.Zapisz(faktura);

			var wplata = new Wplata();
			wplata.FakturaRef = faktura;
			wplata.Data = faktura.TerminPlatnosci;
			wplata.Kwota = faktura.PozostaloDoZaplaty;
			kontekst.Baza.Zapisz(wplata);
		}

		foreach (var jpkFaktura in jpk.Faktura)
		{
			if (jpkFaktura.RodzajFaktury != JPKFakturaRodzajFaktury.KOREKTA) continue;
			var fakturaKorygujaca = kontekst.Baza.Faktury.FirstOrDefault(e => e.Numer == jpkFaktura.P_2A && e.NIPSprzedawcy == jpkFaktura.P_4B);
			if (fakturaKorygujaca == null) continue;
			var fakturaKorygowana = kontekst.Baza.Faktury.FirstOrDefault(e => e.Numer == jpkFaktura.NrFaKorygowanej && e.NIPSprzedawcy == jpkFaktura.P_4B);
			if (fakturaKorygowana == null) continue;
			fakturaKorygujaca.FakturaKorygowana = fakturaKorygowana;
			fakturaKorygowana.FakturaKorygujaca = fakturaKorygujaca;

			kontekst.Baza.Zapisz(fakturaKorygowana);
			kontekst.Baza.Zapisz(fakturaKorygujaca);
		}
	}
}
