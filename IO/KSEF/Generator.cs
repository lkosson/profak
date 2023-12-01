using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using DBFaktura = ProFak.DB.Faktura;
using KSEFFaktura = ProFak.IO.KSEF.Faktura;
using ProFak.DB.Migrations;
using Microsoft.EntityFrameworkCore;
using ProFak.IO.JPK_V7M;

namespace ProFak.IO.KSEF;

class Generator
{
	public static string ZbudujXML(Baza baza, Ref<DBFaktura> dbFakturaRef)
	{
		var dbFaktura = baza.Faktury
			.Include(e => e.Wplaty)
			.Include(e => e.Pozycje).ThenInclude(e => e.Towar).ThenInclude(e => e.JednostkaMiary)
			.Include(e => e.Pozycje).ThenInclude(e => e.StawkaVat)
			.Include(e => e.Sprzedawca)
			.Include(e => e.Nabywca)
			.Include(e => e.Waluta)
			.Where(e => e.Id == dbFakturaRef.Id)
			.FirstOrDefault();
		var ksefFaktura = Zbuduj(dbFaktura);
		var xo = new XmlAttributeOverrides();
		xo.Add(typeof(FakturaFA), "P_15ZK", new XmlAttributes() { XmlIgnore = true });
		var xs = new XmlSerializer(typeof(KSEFFaktura), xo);
		var xml = new StringBuilder();
		using var xw = XmlWriter.Create(xml, new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = true });
		var nss = new XmlSerializerNamespaces();
		xs.Serialize(xw, ksefFaktura, nss);
		return xml.ToString();
	}

	public static DBFaktura ZbudujDB(string xml)
	{
		var xo = new XmlAttributeOverrides();
		var xs = new XmlSerializer(typeof(KSEFFaktura), xo);
		using var xr = XmlReader.Create(xml, new XmlReaderSettings() { });
		var nss = new XmlSerializerNamespaces();
		var ksefFaktura = (KSEFFaktura)xs.Deserialize(xr);
		return Zbuduj(ksefFaktura);
	}

	private static KSEFFaktura Zbuduj(DBFaktura dbFaktura)
	{
		var ksefFaktura = new KSEFFaktura();
		ksefFaktura.Naglowek = new TNaglowek();
		ksefFaktura.Naglowek.KodFormularza = new TNaglowekKodFormularza();
		ksefFaktura.Naglowek.WariantFormularza = 2;
		ksefFaktura.Naglowek.DataWytworzeniaFa = DateTime.Now;
		ksefFaktura.Naglowek.SystemInfo = "ProFak (https://github.com/lkosson/profak)";
		ksefFaktura.Podmiot1 = new FakturaPodmiot1();
		ksefFaktura.Podmiot1.DaneIdentyfikacyjne = new TPodmiot1();
		ksefFaktura.Podmiot1.DaneIdentyfikacyjne.NIP = dbFaktura.NIPSprzedawcy;
		ksefFaktura.Podmiot1.DaneIdentyfikacyjne.Nazwa = dbFaktura.NazwaSprzedawcy;
		ksefFaktura.Podmiot1.Adres = new TAdres();
		ksefFaktura.Podmiot1.Adres.KodKraju = TKodKraju.PL;
		ksefFaktura.Podmiot1.Adres.AdresL1 = dbFaktura.DaneSprzedawcy.JakoDwieLinie().linia1;
		ksefFaktura.Podmiot1.Adres.AdresL2 = dbFaktura.DaneSprzedawcy.JakoDwieLinie().linia2;
		ksefFaktura.Podmiot1.AdresKoresp = new FakturaPodmiot1AdresKoresp();
		ksefFaktura.Podmiot1.AdresKoresp.KodKraju = TKodKraju.PL;
		ksefFaktura.Podmiot1.AdresKoresp.AdresL1 = dbFaktura.Sprzedawca.AdresKorespondencyjny.JakoDwieLinie().linia1;
		ksefFaktura.Podmiot1.AdresKoresp.AdresL2 = dbFaktura.Sprzedawca.AdresKorespondencyjny.JakoDwieLinie().linia2;
		ksefFaktura.Podmiot1.DaneKontaktowe = new FakturaPodmiot1DaneKontaktowe[1];
		ksefFaktura.Podmiot1.DaneKontaktowe[0] = new FakturaPodmiot1DaneKontaktowe();
		ksefFaktura.Podmiot1.DaneKontaktowe[0].Email = dbFaktura.Sprzedawca.EMail;
		ksefFaktura.Podmiot1.DaneKontaktowe[0].Telefon = dbFaktura.Sprzedawca.Telefon;
		ksefFaktura.Podmiot2 = new FakturaPodmiot2();
		ksefFaktura.Podmiot2.DaneIdentyfikacyjne = new TPodmiot2();
		if (String.IsNullOrEmpty(dbFaktura.NIPNabywcy))
		{
			ksefFaktura.Podmiot2.DaneIdentyfikacyjne.Items = new[] { (object)(sbyte)0 };
			ksefFaktura.Podmiot2.DaneIdentyfikacyjne.ItemsElementName = new[] { ItemsChoiceType.BrakID };
		}
		else
		{
			ksefFaktura.Podmiot2.DaneIdentyfikacyjne.Items = new[] { dbFaktura.NIPNabywcy };
			ksefFaktura.Podmiot2.DaneIdentyfikacyjne.ItemsElementName = new[] { ItemsChoiceType.NIP };
		}
		ksefFaktura.Podmiot2.DaneIdentyfikacyjne.Nazwa = dbFaktura.NazwaNabywcy;
		ksefFaktura.Podmiot2.Adres = new TAdres();
		ksefFaktura.Podmiot2.Adres.KodKraju = TKodKraju.PL;
		ksefFaktura.Podmiot2.Adres.AdresL1 = dbFaktura.DaneNabywcy.JakoDwieLinie().linia1;
		ksefFaktura.Podmiot2.Adres.AdresL2 = dbFaktura.DaneNabywcy.JakoDwieLinie().linia2;
		ksefFaktura.Podmiot2.IDNabywcy = dbFaktura.NabywcaRef.Id.ToString();
		ksefFaktura.Fa = new FakturaFA();
		ksefFaktura.Fa.KodWaluty = Enum.Parse<TKodWaluty>(dbFaktura.Waluta.Skrot);
		ksefFaktura.Fa.P_1 = dbFaktura.DataWystawienia;
		ksefFaktura.Fa.P_2 = dbFaktura.Numer;
		ksefFaktura.Fa.Item = dbFaktura.DataSprzedazy; // P_6
		ksefFaktura.Fa.P_15 = dbFaktura.RazemBrutto;
		ksefFaktura.Fa.Adnotacje = new FakturaFAAdnotacje();
		ksefFaktura.Fa.Adnotacje.P_16 = 2;
		ksefFaktura.Fa.Adnotacje.P_17 = 2;
		ksefFaktura.Fa.Adnotacje.P_18 = 2;
		ksefFaktura.Fa.Adnotacje.P_18A = (dbFaktura.OpisSposobuPlatnosci ?? "").Contains("podzielon", StringComparison.CurrentCultureIgnoreCase) ? (sbyte)1 : (sbyte)2;
		ksefFaktura.Fa.Adnotacje.Zwolnienie = new FakturaFAAdnotacjeZwolnienie();
		ksefFaktura.Fa.Adnotacje.Zwolnienie.Items = new[] { (object)(sbyte)1 };
		ksefFaktura.Fa.Adnotacje.Zwolnienie.ItemsElementName = new[] { ItemsChoiceType2.P_19N };
		ksefFaktura.Fa.Adnotacje.NoweSrodkiTransportu = new FakturaFAAdnotacjeNoweSrodkiTransportu();
		ksefFaktura.Fa.Adnotacje.NoweSrodkiTransportu.Items = new[] { (object)(sbyte)1 };
		ksefFaktura.Fa.Adnotacje.NoweSrodkiTransportu.ItemsElementName = new[] { ItemsChoiceType4.P_22N };
		ksefFaktura.Fa.Adnotacje.P_23 = 2;
		ksefFaktura.Fa.Adnotacje.PMarzy = new FakturaFAAdnotacjePMarzy();
		ksefFaktura.Fa.Adnotacje.PMarzy.Items = new[] { (sbyte)1 };
		ksefFaktura.Fa.Adnotacje.PMarzy.ItemsElementName = new[] { ItemsChoiceType5.P_PMarzyN };
		ksefFaktura.Fa.RodzajFaktury = dbFaktura.Rodzaj == DB.RodzajFaktury.Sprzedaż ? TRodzajFaktury.VAT : throw new ApplicationException("Nieobsługiwany rodzaj faktury: " + dbFaktura.RodzajFmt);
		if (dbFaktura.CzyTP) { ksefFaktura.Fa.TP = 1; ksefFaktura.Fa.TPSpecified = true; }
		if (!String.IsNullOrEmpty(dbFaktura.UwagiPubliczne)) ksefFaktura.Fa.DodatkowyOpis = new[] { new TKluczWartosc() { Klucz = "Uwagi", Wartosc = dbFaktura.UwagiPubliczne } };
		ksefFaktura.Fa.Platnosc = new FakturaFAPlatnosc();
		if (dbFaktura.PozostaloDoZaplaty == 0)
		{
			ksefFaktura.Fa.Platnosc.Items = new[] { (object)(sbyte)1, dbFaktura.Wplaty.Last().Data };
			ksefFaktura.Fa.Platnosc.ItemsElementName = new[] { ItemsChoiceType8.Zaplacono, ItemsChoiceType8.DataZaplaty };
		}
		else if (dbFaktura.PozostaloDoZaplaty < dbFaktura.RazemBrutto)
		{
			ksefFaktura.Fa.Platnosc.Items = new[] { (object)(sbyte)1, dbFaktura.Wplaty.Select(e => new FakturaFAPlatnoscZaplataCzesciowa { KwotaZaplatyCzesciowej = e.Kwota, DataZaplatyCzesciowej = e.Data }) };
			ksefFaktura.Fa.Platnosc.ItemsElementName = Enumerable.Repeat(ItemsChoiceType8.ZnacznikZaplatyCzesciowej, 1).Concat(Enumerable.Repeat(ItemsChoiceType8.ZaplataCzesciowa, dbFaktura.Wplaty.Count)).ToArray();
		}
		ksefFaktura.Fa.Platnosc.TerminPlatnosci = new[] { new FakturaFAPlatnoscTerminPlatnosci() };
		ksefFaktura.Fa.Platnosc.TerminPlatnosci[0].Termin = dbFaktura.TerminPlatnosci;
		ksefFaktura.Fa.Platnosc.TerminPlatnosci[0].TerminOpis = dbFaktura.OpisSposobuPlatnosci;
		ksefFaktura.Fa.Platnosc.Items1 = new[] { (object)TFormaPlatnosci.Item6 };
		if (!String.IsNullOrEmpty(dbFaktura.RachunekBankowy))
		{
			ksefFaktura.Fa.Platnosc.RachunekBankowy = new[] { new TRachunekBankowy() };
			ksefFaktura.Fa.Platnosc.RachunekBankowy[0].NrRB = dbFaktura.RachunekBankowy.Replace(" ", "");
		}

		var wiersze = new List<FakturaFAFaWiersz>();
		foreach (var dbPozycja in dbFaktura.Pozycje)
		{
			var ksefWiersz = new FakturaFAFaWiersz();
			ksefWiersz.NrWierszaFa = (wiersze.Count + 1).ToString();
			ksefWiersz.UU_ID = dbPozycja.Id.ToString();
			ksefWiersz.P_7 = dbPozycja.Opis;
			ksefWiersz.Indeks = dbPozycja.Towar.Nazwa;
			ksefWiersz.P_8A = dbPozycja.Towar.JednostkaMiary.Nazwa;
			ksefWiersz.P_8B = dbPozycja.Ilosc;
			ksefWiersz.P_8BSpecified = true;
			ksefWiersz.P_9A = dbPozycja.CenaNetto;
			ksefWiersz.P_9ASpecified = true;
			ksefWiersz.P_9B = dbPozycja.CenaBrutto;
			ksefWiersz.P_9BSpecified = true;
			ksefWiersz.P_11 = dbPozycja.WartoscNetto;
			ksefWiersz.P_11Specified = true;
			ksefWiersz.P_11A = dbPozycja.WartoscBrutto;
			ksefWiersz.P_11ASpecified = true;
			if (dbPozycja.GTU > 0) { ksefWiersz.GTU = Enum.Parse<TGTU>("GTU_" + dbPozycja.GTU.ToString("00")); ksefWiersz.GTUSpecified = true; }

			if (dbFaktura.CzyWDT) { ksefFaktura.Fa.P_13_6_2 += dbPozycja.WartoscNetto; ksefFaktura.Fa.P_13_6_2Specified = true; ksefWiersz.P_12 = TStawkaPodatku.np; }
			else if (dbPozycja.StawkaVat.Skrot.ToLower().Contains("zw")) { ksefFaktura.Fa.P_13_7 += dbPozycja.WartoscNetto; ksefFaktura.Fa.P_13_7Specified = true; ksefWiersz.P_12 = TStawkaPodatku.zw; }
			else if (dbPozycja.StawkaVat.Wartosc == 0) { ksefFaktura.Fa.P_13_6_1 += dbPozycja.WartoscNetto; ksefFaktura.Fa.P_13_6_1Specified = true; ksefWiersz.P_12 = TStawkaPodatku.Item0; }
			else if (dbPozycja.StawkaVat.Wartosc <= 5) { ksefFaktura.Fa.P_13_3 += dbPozycja.WartoscNetto; ksefFaktura.Fa.P_14_3 += dbPozycja.WartoscVat; ksefWiersz.P_12 = TStawkaPodatku.Item5; }
			else if (dbPozycja.StawkaVat.Wartosc <= 8) { ksefFaktura.Fa.P_13_2 += dbPozycja.WartoscNetto; ksefFaktura.Fa.P_14_2 += dbPozycja.WartoscVat; ksefWiersz.P_12 = TStawkaPodatku.Item8; }
			else { ksefFaktura.Fa.P_13_1 += dbPozycja.WartoscNetto; ksefFaktura.Fa.P_14_1 += dbPozycja.WartoscVat; ksefWiersz.P_12 = TStawkaPodatku.Item23; }

			ksefWiersz.P_12Specified = true;

			wiersze.Add(ksefWiersz);
		}
		ksefFaktura.Fa.FaWiersz = wiersze.ToArray();
		ksefFaktura.Stopka = new FakturaStopka();
		return ksefFaktura;
	}

	private static DBFaktura Zbuduj(KSEFFaktura ksefFaktura)
	{
		var dbFaktura = new DBFaktura();
		dbFaktura.Numer = ksefFaktura.Fa.P_2;
		return dbFaktura;
	}

	public static DBFaktura Zbuduj(InvoiceHeader invoiceHeader)
	{
		var dbFaktura = new DBFaktura();
		dbFaktura.Numer = invoiceHeader.ReferenceNumber;
		dbFaktura.NumerKSeF = invoiceHeader.KsefReferenceNumber;
		dbFaktura.NazwaNabywcy = invoiceHeader.IssuedToName;
		dbFaktura.NIPNabywcy = invoiceHeader.IssuedToNIP;
		dbFaktura.NazwaSprzedawcy = invoiceHeader.IssuedByName;
		dbFaktura.NIPSprzedawcy = invoiceHeader.IssuedByNIP;
		dbFaktura.RazemNetto = invoiceHeader.Net;
		dbFaktura.RazemVat = invoiceHeader.Vat;
		dbFaktura.RazemBrutto = invoiceHeader.Gross;
		dbFaktura.Rodzaj = invoiceHeader.Type == "VAT" ? DB.RodzajFaktury.Zakup : DB.RodzajFaktury.KorektaZakupu;
		dbFaktura.DataWystawienia = invoiceHeader.InvoicingDate;
		dbFaktura.DataWprowadzenia = invoiceHeader.AcquisitionTimestamp;
		dbFaktura.Waluta = new Waluta { Skrot = invoiceHeader.Currency };
		return dbFaktura;
	}
}
