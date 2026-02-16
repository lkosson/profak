using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using ProFak.IO.JPK_FA.DefinicjeTypy;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ProFak.IO.JPK_FA
{
	class Generator
	{
		public static void Utworz(string plik, Baza baza, IEnumerable<DeklaracjaVat> deklaracje)
		{
			var jpk = Zbuduj(baza, deklaracje);
			var xo = new XmlAttributeOverrides();
			var xs = new XmlSerializer(typeof(JPK), xo);
			using var xw = XmlWriter.Create(plik, new XmlWriterSettings() { OmitXmlDeclaration = false, Indent = true });
			var nss = new XmlSerializerNamespaces();
			xs.Serialize(xw, jpk, nss);
		}

		private static JPK Zbuduj(Baza baza, IEnumerable<DeklaracjaVat> deklaracje)
		{
			var podmiot = baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot);
			if (podmiot == null) throw new ApplicationException("Nie uzupełniono danych firmy.");
			var faktury = baza.Faktury.Where(faktura => deklaracje.Contains(faktura.DeklaracjaVat))
				.Include(faktura => faktura.Pozycje).ThenInclude(pozycja => pozycja.StawkaVat)
				.Include(faktura => faktura.Pozycje).ThenInclude(pozycja => pozycja.Towar).ThenInclude(towar => towar!.JednostkaMiary)
				.Include(faktura => faktura.FakturaKorygowana)
				.ToList();

			var jpk = new JPK();
			jpk.Naglowek = new JPKNaglowek();
			jpk.Naglowek.KodFormularza = new TNaglowekKodFormularza();
			jpk.Naglowek.KodFormularza.kodSystemowy = "JPK_FA (4)";
			jpk.Naglowek.KodFormularza.wersjaSchemy = "1-0";
			jpk.Naglowek.WariantFormularza = TNaglowekWariantFormularza.Item4;
			jpk.Naglowek.CelZlozenia = TCelZlozenia.Item1;
			jpk.Naglowek.DataWytworzeniaJPK = DateTime.Now;
			jpk.Naglowek.DataOd = deklaracje.Min(e => e.Miesiac);
			jpk.Naglowek.DataDo = deklaracje.Max(e => e.Miesiac).AddMonths(1).AddDays(-1);
			jpk.Naglowek.KodUrzedu = Enum.Parse<TKodUS>("Item" + Wymagane(podmiot.KodUrzedu, "Nie uzupełniono kodu urzędu w karcie podmiotu."));

			jpk.Podmiot1 = new JPKPodmiot1();
			jpk.Podmiot1.IdentyfikatorPodmiotu = new TIdentyfikatorOsobyNiefizycznej1();
			jpk.Podmiot1.IdentyfikatorPodmiotu.NIP = Wymagane(podmiot.NIP, "Nie uzupełniono NIPu firmy.");
			jpk.Podmiot1.IdentyfikatorPodmiotu.PelnaNazwa = Wymagane(podmiot.PelnaNazwa, "Nie podano pełnej nazwy w karcie podmiotu.");

			var adressprzedawcy = new DaneAdresowe(podmiot.AdresRejestrowy);
			jpk.Podmiot1.AdresPodmiotu = new TAdresPolski1();
			jpk.Podmiot1.AdresPodmiotu.KodKraju = TKodKraju.PL;
			jpk.Podmiot1.AdresPodmiotu.Wojewodztwo = "UZUPEŁNIJ";
			jpk.Podmiot1.AdresPodmiotu.Powiat = "UZUPEŁNIJ";
			jpk.Podmiot1.AdresPodmiotu.Gmina = "UZUPEŁNIJ";
			jpk.Podmiot1.AdresPodmiotu.Ulica = adressprzedawcy.Ulica;
			jpk.Podmiot1.AdresPodmiotu.NrDomu = adressprzedawcy.NumerDomu;
			jpk.Podmiot1.AdresPodmiotu.NrLokalu = adressprzedawcy.NumerLokalu;
			jpk.Podmiot1.AdresPodmiotu.Miejscowosc = adressprzedawcy.Miasto;
			jpk.Podmiot1.AdresPodmiotu.KodPocztowy = adressprzedawcy.PNA;

			foreach (var faktura in faktury)
			{
				if (!faktura.CzySprzedaz) continue;
				var nipnumer = faktura.NIPNabywcy;
				var nipkraj = "PL";
				if (nipnumer.Length > 2 && Char.IsLetter(nipnumer[0]) && Char.IsLetter(nipnumer[1]))
				{
					nipkraj = nipnumer[0..2];
					nipnumer = nipnumer[2..];
				}
				else if (String.IsNullOrEmpty(nipnumer)) nipnumer = "BRAK";
				var jpkfaktura = new JPKFaktura();
				jpk.Faktura.Add(jpkfaktura);

				jpkfaktura.KodWaluty = TKodWaluty.PLN;
				jpkfaktura.P_1 = faktura.DataWystawienia;
				jpkfaktura.P_2A = faktura.Numer;
				jpkfaktura.P_3A = faktura.NazwaNabywcy;
				jpkfaktura.P_3B = faktura.DaneNabywcy;
				jpkfaktura.P_3C = faktura.NazwaSprzedawcy;
				jpkfaktura.P_3D = faktura.DaneSprzedawcy;
				jpkfaktura.P_4A = TKodyKrajowUE.PL;
				jpkfaktura.P_4B = faktura.NIPSprzedawcy;
				jpkfaktura.P_5A = TKodyKrajowUE.PL;
				jpkfaktura.P_5B = faktura.NIPNabywcy;
				jpkfaktura.P_6 = faktura.DataSprzedazy;
				jpkfaktura.P_15 = faktura.RazemBrutto;
				jpkfaktura.P_18A = faktura.CzyMechanizmPodzielonejPlatnosci;
				if (faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży || faktura.Rodzaj == RodzajFaktury.KorektaVatMarży)
				{
					jpkfaktura.RodzajFaktury = JPKFakturaRodzajFaktury.KOREKTA;
					jpkfaktura.PrzyczynaKorekty = faktura.UwagiPubliczne;
					jpkfaktura.NrFaKorygowanej = faktura.FakturaKorygowana?.Numer;
				}
				if (faktura.ProceduraMarzy == ProceduraMarży.BiuraPodróży) jpkfaktura.P_106E_2 = true;
				if (faktura.ProceduraMarzy == ProceduraMarży.TowaryUżywane) { jpkfaktura.P_106E_3 = true; jpkfaktura.P_106E_3A = "procedura marży - towary używane"; }
				if (faktura.ProceduraMarzy == ProceduraMarży.DziełaSztuki) { jpkfaktura.P_106E_3 = true; jpkfaktura.P_106E_3A = "procedura marży - dzieła sztuki"; }
				if (faktura.ProceduraMarzy == ProceduraMarży.PrzedmiotyKolekcjonerskie) { jpkfaktura.P_106E_3 = true; jpkfaktura.P_106E_3A = "procedura marży - przedmioty kolekcjonerskie i antyki"; }

				foreach (var pozycja in faktura.Pozycje)
				{
					if (pozycja.StawkaVat == null) continue;

					var jpkwiersz = new JPKFakturaWiersz();
					jpk.FakturaWiersz.Add(jpkwiersz);

					jpkwiersz.P_2B = faktura.Numer;
					jpkwiersz.P_7 = pozycja.Opis;
					jpkwiersz.P_8A = pozycja.Towar?.JednostkaMiary?.Nazwa;
					jpkwiersz.P_8B = pozycja.Ilosc;
					jpkwiersz.P_9A = pozycja.CenaNetto;
					jpkwiersz.P_9B = pozycja.CenaBrutto;
					jpkwiersz.P_11 = pozycja.WartoscNetto;
					jpkwiersz.P_11A = pozycja.WartoscBrutto;

					if (faktura.CzyWDT) { jpkfaktura.P_13_5 ??= 0; jpkfaktura.P_13_5 += pozycja.WartoscNetto; jpkwiersz.P_12 = JPKFakturaWierszP_12.np; }
					else if (pozycja.StawkaVat.Skrot.ToLower().Contains("zw")) { jpkfaktura.P_13_7 ??= 0; jpkfaktura.P_13_7 += pozycja.WartoscNetto; jpkwiersz.P_12 = JPKFakturaWierszP_12.zw; }
					else if (pozycja.StawkaVat.Wartosc == 0) { jpkfaktura.P_13_6 ??= 0; jpkfaktura.P_13_6 += pozycja.WartoscNetto; jpkwiersz.P_12 = JPKFakturaWierszP_12.Item0; }
					else if (pozycja.StawkaVat.Wartosc <= 5) { jpkfaktura.P_13_3 ??= 0; jpkfaktura.P_13_3 += pozycja.WartoscNetto; jpkfaktura.P_14_3 += pozycja.WartoscVat; jpkwiersz.P_12 = JPKFakturaWierszP_12.Item5; }
					else if (pozycja.StawkaVat.Wartosc <= 8) { jpkfaktura.P_13_2 ??= 0; jpkfaktura.P_13_2 += pozycja.WartoscNetto; jpkfaktura.P_14_2 += pozycja.WartoscVat; jpkwiersz.P_12 = JPKFakturaWierszP_12.Item8; }
					else { jpkfaktura.P_13_1 ??= 0; jpkfaktura.P_13_1 += pozycja.WartoscNetto; jpkfaktura.P_14_1 ??= 0; jpkfaktura.P_14_1 += pozycja.WartoscVat; jpkwiersz.P_12 = JPKFakturaWierszP_12.Item23; }
				}
			}

			jpk.FakturaCtrl = new JPKFakturaCtrl();
			jpk.FakturaWierszCtrl = new JPKFakturaWierszCtrl();
			jpk.FakturaCtrl.LiczbaFaktur = (ulong)jpk.Faktura.Count;
			jpk.FakturaCtrl.WartoscFaktur = jpk.Faktura.Sum(faktura => faktura.P_15);
			jpk.FakturaWierszCtrl.LiczbaWierszyFaktur = (ulong)jpk.FakturaWiersz.Count;
			jpk.FakturaWierszCtrl.WartoscWierszyFaktur = jpk.FakturaWiersz.Sum(wiersz => wiersz.P_11 ?? 0);

			return jpk;
		}

		private static string Wymagane(string wartosc, string komunikat) => String.IsNullOrWhiteSpace(wartosc) ? throw new ApplicationException(komunikat) : wartosc;
	}
}
