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

namespace ProFak.IO.JPK_PKPIR
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
				.Include(faktura => faktura.Pozycje)
				.Include(faktura => faktura.FakturaKorygowana)
				.ToList();

			var jpk = new JPK();
			jpk.Naglowek = new TNaglowek();
			jpk.Naglowek.KodFormularza = new TNaglowekKodFormularza();
			jpk.Naglowek.KodFormularza.kodSystemowy = "JPK_PKPIR (3)";
			jpk.Naglowek.KodFormularza.wersjaSchemy = "1-0";
			jpk.Naglowek.WariantFormularza = 3;
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

			var jpkwiersze = new List<JPKPKPIRWiersz>();
			foreach (var faktura in faktury)
			{
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
					var jpkwiersz = new JPKPKPIRWiersz();
					jpkwiersz.K_1 = (jpkwiersze.Count + 1).ToString();
					jpkwiersz.K_2 = faktura.DataSprzedazy;
					jpkwiersz.K_3A = faktura.Numer;
					jpkwiersz.K_3B = faktura.NumerKSeF;
					jpkwiersz.K_4A = Enum.Parse<TKodKraju>(nipkraj);
					jpkwiersz.K_4B = nipnumer;
					jpkwiersz.K_5A = faktura.CzySprzedaz ? faktura.NazwaNabywcy : faktura.NazwaSprzedawcy;
					jpkwiersz.K_5B = faktura.CzySprzedaz ? faktura.DaneNabywcy : faktura.DaneSprzedawcy;
					jpkwiersz.K_6 = String.IsNullOrEmpty(faktura.OpisZdarzenia) ? faktura.CzySprzedaz ? "Sprzedaż" : "Zakup" : faktura.OpisZdarzenia;
					jpkwiersz.K_7 = faktura.CzySprzedaz ? faktura.RazemNetto : 0;
					jpkwiersz.K_7Specified = faktura.CzySprzedaz;
					jpkwiersz.K_9 = jpkwiersz.K_7 + jpkwiersz.K_8;
					jpkwiersz.K_9Specified = jpkwiersz.K_7Specified;
					jpkwiersz.K_10 = faktura.CzyZakup ? faktura.RazemNetto : 0;
					jpkwiersz.K_10Specified = faktura.CzyZakup;
					jpkwiersze.Add(jpkwiersz);
				}
			}

			jpk.PKPIRWiersz = jpkwiersze.ToArray();

			jpk.PKPIRInfo = new JPKPKPIRInfo();
			jpk.PKPIRInfo.P_1 = 0m;
			jpk.PKPIRInfo.P_2 = 0m;
			jpk.PKPIRInfo.P_3 = jpkwiersze.Sum(wiersz => wiersz.K_10);
			jpk.PKPIRInfo.P_4 = jpkwiersze.Sum(wiersz => wiersz.K_7) - jpkwiersze.Sum(wiersz => wiersz.K_10);

			jpk.PKPIRSpis = new JPKPKPIRSpis[0];


			jpk.PKPIRCtrl = new JPKPKPIRCtrl();
			jpk.PKPIRCtrl.LiczbaWierszy = jpkwiersze.Count.ToString();
			jpk.PKPIRCtrl.SumaPrzychodow = jpkwiersze.Sum(wiersz => wiersz.K_9);

			return jpk;
		}

		private static string Wymagane(string wartosc, string komunikat) => String.IsNullOrWhiteSpace(wartosc) ? throw new ApplicationException(komunikat) : wartosc;
	}
}
