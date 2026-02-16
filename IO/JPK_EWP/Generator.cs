using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using ProFak.IO.JPK_EWP.DefinicjeTypy;
using ProFak.IO.JPK_EWP.KodyUrzedowSkarbowych;
using System.Xml;
using System.Xml.Serialization;

namespace ProFak.IO.JPK_EWP;

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
		if (podmiot == null) throw new ApplicationException("Nie uzupełniono danych firmy.");
		var faktury = baza.Faktury.Where(faktura => zaliczki.Contains(faktura.ZaliczkaPit))
			.Include(faktura => faktura.Pozycje)
			.Include(faktura => faktura.FakturaKorygowana)
			.ToList();

		var jpk = new JPK();
		jpk.Naglowek = new TNaglowek();
		jpk.Naglowek.KodFormularza = new TNaglowekKodFormularza();
		jpk.Naglowek.KodFormularza.kodSystemowy = "JPK_EWP (4)";
		jpk.Naglowek.KodFormularza.wersjaSchemy = "1-0";
		jpk.Naglowek.WariantFormularza = TNaglowekWariantFormularza.Item4;
		jpk.Naglowek.CelZlozenia = TCelZlozenia.Item1;
		jpk.Naglowek.DataWytworzeniaJPK = DateTime.Now;
		jpk.Naglowek.DataOd = zaliczki.Min(e => e.Miesiac);
		jpk.Naglowek.DataDo = zaliczki.Max(e => e.Miesiac).AddMonths(1).AddDays(-1);
		jpk.Naglowek.KodUrzedu = Enum.Parse<TKodUS>("Item" + Wymagane(podmiot.KodUrzedu, "Nie uzupełniono kodu urzędu w karcie podmiotu."));

		jpk.Podmiot1 = new JPKPodmiot1();
		var jpkpodmiot = new TPodmiotDowolnyBezAdresuOsobaFizyczna();
		jpk.Podmiot1.OsobaFizyczna = jpkpodmiot;
		jpkpodmiot.NIP = Wymagane(podmiot.NIP, "Nie uzupełniono NIPu firmy.");
		jpkpodmiot.ImiePierwsze = Wymagane(podmiot.OsobaFizycznaImie, "Nie podano imienia w danych urzędowych firmy.");
		jpkpodmiot.Nazwisko = Wymagane(podmiot.OsobaFizycznaNazwisko, "Nie podano nazwiska w danych urzędowych firmy.");
		jpkpodmiot.DataUrodzenia = podmiot.OsobaFizycznaDataUrodzenia.HasValue ? podmiot.OsobaFizycznaDataUrodzenia.Value : throw new ApplicationException("Nie podano daty urodzenia w danych urzędowych firmy.");
		jpkpodmiot.Email = Wymagane(podmiot.EMail, "Nie podano adresu e-mail w danych urzędowych firmy.");
		jpkpodmiot.Telefon = podmiot.Telefon;

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

			foreach (var pozycje in faktura.Pozycje.Where(e => e.StawkaRyczaltu.HasValue).GroupBy(e => e.StawkaRyczaltu!.Value))
			{
				var jpkwiersz = new JPKEWPWiersz();
				jpkwiersz.K_1 = (ulong)(jpk.EWPWiersz.Count + 1);
				jpkwiersz.K_2 = faktura.DataWystawienia;
				jpkwiersz.K_3 = faktura.DataSprzedazy;
				jpkwiersz.K_4 = faktura.Numer;
				jpkwiersz.K_5 = faktura.NumerKSeF;
				jpkwiersz.K_6 = Enum.Parse<TKodKraju>(nipkraj);
				jpkwiersz.K_7 = nipnumer;
				jpkwiersz.K_8 = pozycje.Sum(e => e.WartoscNetto);
				if (pozycje.Key == 17) jpkwiersz.K_9 = TStawkaPodatku.Item17;
				else if (pozycje.Key == 15) jpkwiersz.K_9 = TStawkaPodatku.Item15;
				else if (pozycje.Key == 14) jpkwiersz.K_9 = TStawkaPodatku.Item14;
				else if (pozycje.Key == 12.5m) jpkwiersz.K_9 = TStawkaPodatku.Item12Period5;
				else if (pozycje.Key == 12) jpkwiersz.K_9 = TStawkaPodatku.Item12;
				else if (pozycje.Key == 10) jpkwiersz.K_9 = TStawkaPodatku.Item10;
				else if (pozycje.Key == 8.5m) jpkwiersz.K_9 = TStawkaPodatku.Item8Period5;
				else if (pozycje.Key == 5.5m) jpkwiersz.K_9 = TStawkaPodatku.Item5Period5;
				else if (pozycje.Key == 3) jpkwiersz.K_9 = TStawkaPodatku.Item3;
				jpk.EWPWiersz.Add(jpkwiersz);
			}
		}

		jpk.EWPCtrl = new JPKEWPCtrl();
		jpk.EWPCtrl.LiczbaWierszy = (ulong)jpk.EWPWiersz.Count;
		jpk.EWPCtrl.SumaPrzychodow = jpk.EWPWiersz.Sum(wiersz => wiersz.K_8);

		return jpk;
	}

	private static string Wymagane(string wartosc, string komunikat) => String.IsNullOrWhiteSpace(wartosc) ? throw new ApplicationException(komunikat) : wartosc;
}
