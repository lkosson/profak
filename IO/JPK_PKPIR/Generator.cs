using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using ProFak.IO.JPK_PKPIR.DefinicjeTypy;
using ProFak.IO.JPK_PKPIR.KodyUrzedowSkarbowych;
using System.Xml;
using System.Xml.Serialization;

namespace ProFak.IO.JPK_PKPIR;

public class Generator
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
			.Include(faktura => faktura.Pozycje).ThenInclude(pozycja => pozycja.Towar)
			.Include(faktura => faktura.FakturaKorygowana)
			.ToList();

		var jpk = new JPK();
		jpk.Naglowek = new TNaglowek();
		jpk.Naglowek.KodFormularza = new TNaglowekKodFormularza();
		jpk.Naglowek.KodFormularza.kodSystemowy = "JPK_PKPIR (3)";
		jpk.Naglowek.KodFormularza.wersjaSchemy = "1-0";
		jpk.Naglowek.WariantFormularza = TNaglowekWariantFormularza.Item3;
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
			if (faktura.CzyZakup && faktura.ProcentKosztow == 0) continue;
			var jestTowar = faktura.Pozycje.Any(pozycja => pozycja.Towar != null && pozycja.Towar.Rodzaj == RodzajTowaru.Towar);
			var nipnumer = faktura.NIPNabywcy;
			var nipkraj = "PL";
			if (nipnumer.Length > 2 && Char.IsLetter(nipnumer[0]) && Char.IsLetter(nipnumer[1]))
			{
				nipkraj = nipnumer[0..2];
				nipnumer = nipnumer[2..];
			}
			else if (String.IsNullOrEmpty(nipnumer)) nipnumer = "BRAK";

			var jpkwiersz = new JPKPKPIRWiersz();
			jpkwiersz.K_1 = (ulong)(jpk.PKPIRWiersz.Count + 1);
			jpkwiersz.K_2 = faktura.DataSprzedazy;
			jpkwiersz.K_3A = faktura.Numer;
			jpkwiersz.K_3B = faktura.NumerKSeF;
			jpkwiersz.K_4A = Enum.Parse<TKodKraju>(nipkraj);
			jpkwiersz.K_4B = nipnumer;
			jpkwiersz.K_5A = faktura.CzySprzedaz ? faktura.NazwaNabywcy : faktura.NazwaSprzedawcy;
			jpkwiersz.K_5B = faktura.CzySprzedaz ? faktura.DaneNabywcy : faktura.DaneSprzedawcy;
			jpkwiersz.K_6 = String.IsNullOrEmpty(faktura.OpisZdarzenia) ? ((faktura.CzySprzedaz ? "Sprzedaż" : "Zakup") + (jestTowar ? " towarów" : " usług")) : faktura.OpisZdarzenia;
			if (faktura.CzySprzedaz)
			{
				jpkwiersz.K_7 = faktura.RazemNetto;
				jpkwiersz.K_9 = jpkwiersz.K_7.GetValueOrDefault() + jpkwiersz.K_8.GetValueOrDefault();
			}
			if (faktura.CzyZakup)
			{
				if (jestTowar) jpkwiersz.K_10 = faktura.RazemNetto;
				else jpkwiersz.K_13 = faktura.RazemNetto;
				jpkwiersz.K_14 = jpkwiersz.K_12.GetValueOrDefault() + jpkwiersz.K_13.GetValueOrDefault();
			}
			jpk.PKPIRWiersz.Add(jpkwiersz);
		}

		jpk.PKPIRInfo = new JPKPKPIRInfo();
		jpk.PKPIRInfo.P_1 = 0m;
		jpk.PKPIRInfo.P_2 = 0m;
		jpk.PKPIRInfo.P_3 = jpk.PKPIRWiersz.Sum(wiersz => wiersz.K_10 ?? 0 + wiersz.K_14 ?? 0);
		jpk.PKPIRInfo.P_4 = jpk.PKPIRWiersz.Sum(wiersz => wiersz.K_7 ?? 0) - jpk.PKPIRInfo.P_3;

		jpk.PKPIRCtrl = new JPKPKPIRCtrl();
		jpk.PKPIRCtrl.LiczbaWierszy = (ulong)jpk.PKPIRWiersz.Count;
		jpk.PKPIRCtrl.SumaPrzychodow = jpk.PKPIRWiersz.Sum(wiersz => wiersz.K_9 ?? 0);

		return jpk;
	}

	private static string Wymagane(string wartosc, string komunikat) => String.IsNullOrWhiteSpace(wartosc) ? throw new ApplicationException(komunikat) : wartosc;
}
