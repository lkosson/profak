using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ProFak.IO.JPK_EWP
{
	class Generator
	{
		public static void Utworz(string plik, Baza baza, IEnumerable<ZaliczkaPit> zaliczki)
		{
			var jpk = Zbuduj(baza, zaliczki);
			var xo = new XmlAttributeOverrides();
			var xs = new XmlSerializer(typeof(JPK), xo);
			using var xw = XmlWriter.Create(plik, new XmlWriterSettings() { OmitXmlDeclaration = false, Indent = true });
			var nss = new XmlSerializerNamespaces();
			xs.Serialize(xw, jpk, nss);
		}

		private static JPK Zbuduj(Baza baza, IEnumerable<ZaliczkaPit> zaliczki)
		{
			var podmiot = baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot);
			var faktury = baza.Faktury.Where(faktura => zaliczki.Contains(faktura.ZaliczkaPit))
				//.Include(faktura => faktura.Pozycje).ThenInclude(pozycja => pozycja.StawkaVat)
				//.Include(faktura => faktura.Pozycje).ThenInclude(pozycja => pozycja.Towar).ThenInclude(towar => towar.JednostkaMiary)
				.Include(faktura => faktura.FakturaKorygowana)
				.ToList();

			var jpk = new JPK();
			jpk.Naglowek = new TNaglowek();
			jpk.Naglowek.KodFormularza = new TNaglowekKodFormularza();
			jpk.Naglowek.KodFormularza.kodSystemowy = "JPK_EWP (4)";
			jpk.Naglowek.KodFormularza.wersjaSchemy = "1-0";
			jpk.Naglowek.WariantFormularza = 4;
			jpk.Naglowek.CelZlozenia = 1;
			jpk.Naglowek.DataWytworzeniaJPK = DateTime.Now;
			jpk.Naglowek.DataOd = zaliczki.Min(e => e.Miesiac);
			jpk.Naglowek.DataDo = zaliczki.Max(e => e.Miesiac).AddMonths(1).AddDays(-1);
			jpk.Naglowek.KodUrzedu = Enum.Parse<TKodUS>("Item" + Wymagane(podmiot.KodUrzedu, "Nie uzupełniono kodu urzędu w karcie podmiotu."));


			jpk.Podmiot1 = new JPKPodmiot1();
			var podmiotOF = new TPodmiotDowolnyBezAdresuOsobaFizyczna();
			podmiotOF.NIP = podmiot.NIP;
			podmiotOF.Email = podmiot.EMail;
			podmiotOF.Telefon = podmiot.Telefon;
			podmiotOF.ImiePierwsze = podmiot.OsobaFizycznaImie;
			podmiotOF.Nazwisko = podmiot.OsobaFizycznaNazwisko;
			podmiotOF.DataUrodzenia = podmiot.OsobaFizycznaDataUrodzenia.Value;

			var jpkwiersze = new List<JPKEWPWiersz>();
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

				foreach (var pozycje in faktura.Pozycje.Where(e => e.StawkaRyczaltu.HasValue).GroupBy(e => e.StawkaRyczaltu.Value))
				{
					var jpkwiersz = new JPKEWPWiersz();
					jpkwiersz.K_1 = (jpkwiersze.Count + 1).ToString();
					jpkwiersz.K_2 = faktura.DataWystawienia;
					jpkwiersz.K_3 = faktura.DataSprzedazy;
					jpkwiersz.K_4 = faktura.Numer;
					jpkwiersz.K_5 = faktura.NumerKSeF;
					jpkwiersz.K_6 = Enum.Parse<TKodKraju>(nipkraj);
					jpkwiersz.K_7 = nipnumer;
					jpkwiersz.K_8 = pozycje.Sum(e => e.WartoscNetto);
					jpkwiersz.K_9 = Enum.Parse<TStawkaPodatku>(pozycje.Key.ToString(CultureInfo.InvariantCulture).Replace(".", ""));
					jpkwiersze.Add(jpkwiersz);
				}
			}

			jpk.EWPWiersz = jpkwiersze.ToArray();

			jpk.EWPCtrl = new JPKEWPCtrl();
			jpk.EWPCtrl.LiczbaWierszy = jpkwiersze.Count.ToString();
			jpk.EWPCtrl.SumaPrzychodow = jpkwiersze.Sum(wiersz => wiersz.K_8);

			return jpk;
		}

		private static string Wymagane(string wartosc, string komunikat) => String.IsNullOrWhiteSpace(wartosc) ? throw new ApplicationException(komunikat) : wartosc;
	}
}
