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
using System.IO;

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
			.Include(e => e.FakturaKorygowana)
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

	public static DBFaktura ZbudujDB(Baza baza, string xml)
	{
		var xo = new XmlAttributeOverrides();
		var xs = new XmlSerializer(typeof(KSEFFaktura), xo);
		using var xr = XmlReader.Create(new StringReader(xml), new XmlReaderSettings() { });
		var nss = new XmlSerializerNamespaces();
		var ksefFaktura = (KSEFFaktura)xs.Deserialize(xr);
		var dbFaktura = Zbuduj(ksefFaktura);
		dbFaktura.XMLKSeF = xml;
		PoprawPowiazaniaPoImporcie(baza, dbFaktura);
		return dbFaktura;
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
		ksefFaktura.Fa.RodzajFaktury = dbFaktura.Rodzaj == DB.RodzajFaktury.Sprzedaż ? TRodzajFaktury.VAT : dbFaktura.Rodzaj == DB.RodzajFaktury.KorektaSprzedaży ? TRodzajFaktury.KOR : throw new ApplicationException("Nieobsługiwany rodzaj faktury: " + dbFaktura.RodzajFmt);
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
		if (dbFaktura.FakturaKorygowana != null)
		{
			ksefFaktura.Fa.PrzyczynaKorekty = dbFaktura.UwagiPubliczne;
			ksefFaktura.Fa.TypKorekty = TTypKorekty.Item2;
			ksefFaktura.Fa.TypKorektySpecified = true;
			ksefFaktura.Fa.DaneFaKorygowanej = new[] { new FakturaFADaneFaKorygowanej() };
			ksefFaktura.Fa.DaneFaKorygowanej[0].DataWystFaKorygowanej = dbFaktura.FakturaKorygowana.DataWystawienia;
			ksefFaktura.Fa.DaneFaKorygowanej[0].NrFaKorygowanej = dbFaktura.FakturaKorygowana.Numer;
			if (String.IsNullOrEmpty(dbFaktura.FakturaKorygowana.NumerKSeF))
			{
				ksefFaktura.Fa.DaneFaKorygowanej[0].Items = new[] { (object)(sbyte)1 };
				ksefFaktura.Fa.DaneFaKorygowanej[0].ItemsElementName = new[] { ItemsChoiceType6.NrKSeFN };
			}
			else
			{
				ksefFaktura.Fa.DaneFaKorygowanej[0].Items = new[] { (object)(sbyte)1, dbFaktura.FakturaKorygowana.NumerKSeF };
				ksefFaktura.Fa.DaneFaKorygowanej[0].ItemsElementName = new[] { ItemsChoiceType6.NrKSeF, ItemsChoiceType6.NrKSeFFaKorygowanej };
			}

			if (dbFaktura.FakturaKorygowana.NazwaSprzedawcy != dbFaktura.NazwaSprzedawcy || dbFaktura.FakturaKorygowana.DaneSprzedawcy != dbFaktura.DaneSprzedawcy)
			{
				ksefFaktura.Fa.Podmiot1K = new FakturaFAPodmiot1K();
				ksefFaktura.Fa.Podmiot1K.DaneIdentyfikacyjne = new TPodmiot1();
				ksefFaktura.Fa.Podmiot1K.DaneIdentyfikacyjne.Nazwa = dbFaktura.FakturaKorygowana.NazwaSprzedawcy;
				ksefFaktura.Fa.Podmiot1K.DaneIdentyfikacyjne.NIP = dbFaktura.FakturaKorygowana.NIPSprzedawcy;
				ksefFaktura.Fa.Podmiot1K.Adres = new TAdres();
				ksefFaktura.Fa.Podmiot1K.Adres.KodKraju = TKodKraju.PL;
				ksefFaktura.Fa.Podmiot1K.Adres.AdresL1 = dbFaktura.FakturaKorygowana.DaneSprzedawcy.JakoDwieLinie().linia1;
				ksefFaktura.Fa.Podmiot1K.Adres.AdresL2 = dbFaktura.FakturaKorygowana.DaneSprzedawcy.JakoDwieLinie().linia2;
			}

			if (dbFaktura.FakturaKorygowana.NazwaNabywcy != dbFaktura.NazwaNabywcy || dbFaktura.FakturaKorygowana.DaneNabywcy != dbFaktura.DaneNabywcy)
			{
				ksefFaktura.Fa.Podmiot2K = new[] { new FakturaFAPodmiot2K() };
				ksefFaktura.Fa.Podmiot2K[0].DaneIdentyfikacyjne = new TPodmiot2();
				ksefFaktura.Fa.Podmiot2K[0].DaneIdentyfikacyjne.Nazwa = dbFaktura.FakturaKorygowana.NazwaNabywcy;
				ksefFaktura.Fa.Podmiot2K[0].DaneIdentyfikacyjne.Items = ksefFaktura.Podmiot2.DaneIdentyfikacyjne.Items;
				ksefFaktura.Fa.Podmiot2K[0].DaneIdentyfikacyjne.ItemsElementName = ksefFaktura.Podmiot2.DaneIdentyfikacyjne.ItemsElementName;
				ksefFaktura.Fa.Podmiot2K[0].Adres = new TAdres();
				ksefFaktura.Fa.Podmiot2K[0].Adres.KodKraju = TKodKraju.PL;
				ksefFaktura.Fa.Podmiot2K[0].Adres.AdresL1 = dbFaktura.FakturaKorygowana.DaneNabywcy.JakoDwieLinie().linia1;
				ksefFaktura.Fa.Podmiot2K[0].Adres.AdresL2 = dbFaktura.FakturaKorygowana.DaneNabywcy.JakoDwieLinie().linia2;
			}
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
			ksefWiersz.P_8B = Math.Abs(dbPozycja.Ilosc);
			ksefWiersz.P_8BSpecified = true;
			if (dbPozycja.CzyWedlugCenBrutto)
			{
				ksefWiersz.P_9B = dbPozycja.CenaBrutto;
				ksefWiersz.P_9BSpecified = true;
				ksefWiersz.P_11A = Math.Abs(dbPozycja.WartoscBrutto);
				ksefWiersz.P_11ASpecified = true;
			}
			else
			{
				ksefWiersz.P_9A = dbPozycja.CenaNetto;
				ksefWiersz.P_9ASpecified = true;
				ksefWiersz.P_11 = Math.Abs(dbPozycja.WartoscNetto);
				ksefWiersz.P_11Specified = true;
			}
			ksefWiersz.P_11Vat = Math.Abs(dbPozycja.WartoscVat);
			ksefWiersz.P_11VatSpecified = true;
			if (dbPozycja.GTU > 0) { ksefWiersz.GTU = Enum.Parse<TGTU>("GTU_" + dbPozycja.GTU.ToString("00")); ksefWiersz.GTUSpecified = true; }

			if (dbFaktura.CzyWDT) { ksefFaktura.Fa.P_13_6_2 += dbPozycja.WartoscNetto; ksefFaktura.Fa.P_13_6_2Specified = true; ksefWiersz.P_12 = TStawkaPodatku.np; }
			else if (dbPozycja.StawkaVat.Skrot.ToLower().Contains("zw")) { ksefFaktura.Fa.P_13_7 += dbPozycja.WartoscNetto; ksefFaktura.Fa.P_13_7Specified = true; ksefWiersz.P_12 = TStawkaPodatku.zw; }
			else if (dbPozycja.StawkaVat.Wartosc == 0) { ksefFaktura.Fa.P_13_6_1 += dbPozycja.WartoscNetto; ksefFaktura.Fa.P_13_6_1Specified = true; ksefWiersz.P_12 = TStawkaPodatku.Item0; }
			else if (dbPozycja.StawkaVat.Wartosc <= 5) { ksefFaktura.Fa.P_13_3 += dbPozycja.WartoscNetto; ksefFaktura.Fa.P_14_3 += dbPozycja.WartoscVat; ksefWiersz.P_12 = TStawkaPodatku.Item5; }
			else if (dbPozycja.StawkaVat.Wartosc <= 8) { ksefFaktura.Fa.P_13_2 += dbPozycja.WartoscNetto; ksefFaktura.Fa.P_14_2 += dbPozycja.WartoscVat; ksefWiersz.P_12 = TStawkaPodatku.Item8; }
			else { ksefFaktura.Fa.P_13_1 += dbPozycja.WartoscNetto; ksefFaktura.Fa.P_14_1 += dbPozycja.WartoscVat; ksefWiersz.P_12 = TStawkaPodatku.Item23; }

			ksefWiersz.P_12Specified = true;
			if (dbPozycja.CzyPrzedKorekta) { ksefWiersz.StanPrzed = 1; ksefWiersz.StanPrzedSpecified = true; }

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
		dbFaktura.Rodzaj = ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.VAT ? DB.RodzajFaktury.Zakup : ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.KOR ? DB.RodzajFaktury.KorektaZakupu : throw new ApplicationException($"Nieobsługiwany rodzaj faktury: {ksefFaktura.Fa.RodzajFaktury}.");
		dbFaktura.DataWystawienia = ksefFaktura.Fa.P_1;
		dbFaktura.DataSprzedazy = (DateTime)ksefFaktura.Fa.Item;
		dbFaktura.Waluta = new Waluta { Skrot = ksefFaktura.Fa.KodWaluty.ToString(), Nazwa = ksefFaktura.Fa.KodWaluty.ToString() };
		dbFaktura.CzyTP = ksefFaktura.Fa.TP > 0;
		dbFaktura.Sprzedawca = new Kontrahent();
		if (ksefFaktura.Podmiot1 != null)
		{
			if (ksefFaktura.Podmiot1.DaneIdentyfikacyjne != null)
			{
				dbFaktura.Sprzedawca.Nazwa = dbFaktura.NazwaSprzedawcy = ksefFaktura.Podmiot1.DaneIdentyfikacyjne.Nazwa;
				dbFaktura.Sprzedawca.NIP = dbFaktura.NIPSprzedawcy = ksefFaktura.Podmiot1.DaneIdentyfikacyjne.NIP;
			}

			if (ksefFaktura.Podmiot1.Adres != null) dbFaktura.Sprzedawca.AdresRejestrowy = dbFaktura.DaneSprzedawcy = ksefFaktura.Podmiot1.Adres.AdresL1 + "\r\n" + ksefFaktura.Podmiot1.Adres.AdresL2;
			if (ksefFaktura.Podmiot1.AdresKoresp != null) dbFaktura.Sprzedawca.AdresKorespondencyjny = ksefFaktura.Podmiot1.AdresKoresp.AdresL1 + "\r\n" + ksefFaktura.Podmiot1.AdresKoresp.AdresL2;
			if (ksefFaktura.Podmiot1.DaneKontaktowe != null && ksefFaktura.Podmiot1.DaneKontaktowe.Length > 0)
			{
				dbFaktura.Sprzedawca.Telefon = ksefFaktura.Podmiot1.DaneKontaktowe[0].Telefon;
				dbFaktura.Sprzedawca.EMail = ksefFaktura.Podmiot1.DaneKontaktowe[0].Email;
			}
		}
		dbFaktura.Nabywca = new Kontrahent();
		if (ksefFaktura.Podmiot2 != null)
		{
			if (ksefFaktura.Podmiot2.DaneIdentyfikacyjne != null)
			{
				for (int i = 0; i < ksefFaktura.Podmiot2.DaneIdentyfikacyjne.Items.Length; i++)
				{
					if (ksefFaktura.Podmiot2.DaneIdentyfikacyjne.ItemsElementName[i] == ItemsChoiceType.NIP) dbFaktura.Nabywca.NIP = (string)ksefFaktura.Podmiot2.DaneIdentyfikacyjne.Items[i];
				}
				dbFaktura.Sprzedawca.Nazwa = ksefFaktura.Podmiot2.DaneIdentyfikacyjne.Nazwa;
			}

			if (ksefFaktura.Podmiot2.Adres != null) dbFaktura.Nabywca.AdresRejestrowy = ksefFaktura.Podmiot2.Adres.AdresL1 + "\r\n" + ksefFaktura.Podmiot2.Adres.AdresL2;
			if (ksefFaktura.Podmiot2.AdresKoresp != null) dbFaktura.Nabywca.AdresKorespondencyjny = ksefFaktura.Podmiot2.AdresKoresp.AdresL1 + "\r\n" + ksefFaktura.Podmiot2.AdresKoresp.AdresL2;
			if (ksefFaktura.Podmiot2.DaneKontaktowe != null && ksefFaktura.Podmiot2.DaneKontaktowe.Length > 0)
			{
				dbFaktura.Nabywca.Telefon = ksefFaktura.Podmiot2.DaneKontaktowe[0].Telefon;
				dbFaktura.Nabywca.EMail = ksefFaktura.Podmiot2.DaneKontaktowe[0].Email;
			}
		}
		if (ksefFaktura.Fa.Platnosc != null)
		{
			if (ksefFaktura.Fa.Platnosc.TerminPlatnosci != null && ksefFaktura.Fa.Platnosc.TerminPlatnosci.Length > 0)
			{
				dbFaktura.TerminPlatnosci = ksefFaktura.Fa.Platnosc.TerminPlatnosci[0].Termin;
				dbFaktura.OpisSposobuPlatnosci = ksefFaktura.Fa.Platnosc.TerminPlatnosci[0].TerminOpis;
			}

			if (ksefFaktura.Fa.Platnosc.RachunekBankowy != null && ksefFaktura.Fa.Platnosc.RachunekBankowy.Length > 0)
			{
				dbFaktura.RachunekBankowy = ksefFaktura.Fa.Platnosc.RachunekBankowy[0].NrRB;
			}

			foreach (var platnosc in ksefFaktura.Fa.Platnosc.Items1)
			{
				if (platnosc is string opis) dbFaktura.SposobPlatnosci = new SposobPlatnosci { Nazwa = opis };
			}

			foreach (var platnosc in ksefFaktura.Fa.Platnosc.Items)
			{
				if (platnosc is DateTime dataZaplaty)
				{
					dbFaktura.Wplaty = new List<DB.Wplata>();
					dbFaktura.Wplaty.Add(new DB.Wplata { Data = dataZaplaty, Kwota = ksefFaktura.Fa.P_15 });
				}
			}
		}
		dbFaktura.Pozycje = new List<PozycjaFaktury>();
		foreach (var pozycja in ksefFaktura.Fa.FaWiersz)
		{
			var dbPozycja = new PozycjaFaktury();
			dbPozycja.LP = Int32.Parse(pozycja.NrWierszaFa);
			dbPozycja.Opis = pozycja.P_7;
			dbPozycja.Ilosc = pozycja.P_8B;
			dbPozycja.CenaNetto = pozycja.P_9A;
			dbPozycja.CenaBrutto = pozycja.P_9B;
			dbPozycja.WartoscNetto = pozycja.P_11;
			dbPozycja.WartoscBrutto = pozycja.P_11A;
			dbPozycja.Towar = new Towar();
			dbPozycja.Towar.Nazwa = pozycja.P_7;
			dbPozycja.Towar.JednostkaMiary = new JednostkaMiary { Nazwa = pozycja.P_8A, Skrot = pozycja.P_8A };
			dbPozycja.StawkaVat = new StawkaVat();
			if (pozycja.P_12 == TStawkaPodatku.Item23) dbPozycja.StawkaVat = new StawkaVat { Wartosc = 23, Skrot = "23" };
			else if (pozycja.P_12 == TStawkaPodatku.Item22) dbPozycja.StawkaVat = new StawkaVat { Wartosc = 22, Skrot = "22" };
			else if (pozycja.P_12 == TStawkaPodatku.Item8) dbPozycja.StawkaVat = new StawkaVat { Wartosc = 8, Skrot = "8" };
			else if (pozycja.P_12 == TStawkaPodatku.Item7) dbPozycja.StawkaVat = new StawkaVat { Wartosc = 7, Skrot = "7" };
			else if (pozycja.P_12 == TStawkaPodatku.Item5) dbPozycja.StawkaVat = new StawkaVat { Wartosc = 5, Skrot = "5" };
			else if (pozycja.P_12 == TStawkaPodatku.Item4) dbPozycja.StawkaVat = new StawkaVat { Wartosc = 4, Skrot = "4" };
			else if (pozycja.P_12 == TStawkaPodatku.Item3) dbPozycja.StawkaVat = new StawkaVat { Wartosc = 3, Skrot = "3" };
			else if (pozycja.P_12 == TStawkaPodatku.Item0) dbPozycja.StawkaVat = new StawkaVat { Wartosc = 2, Skrot = "2" };
			else dbPozycja.StawkaVat = new StawkaVat { Wartosc = 23, Skrot = "23" };
			dbPozycja.CenaVat = dbPozycja.CenaBrutto - dbPozycja.CenaNetto;
			dbPozycja.WartoscVat = dbPozycja.WartoscBrutto - dbPozycja.WartoscNetto;
			dbFaktura.Pozycje.Add(dbPozycja);
		}
		return dbFaktura;
	}

	private static void PoprawPowiazaniaPoImporcie(Baza baza, DB.Faktura faktura)
	{
		var sprzedawca = baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.NIP == faktura.Sprzedawca.NIP);
		if (sprzedawca == null) baza.Zapisz(sprzedawca = faktura.Sprzedawca);
		faktura.SprzedawcaRef = sprzedawca;
		faktura.Sprzedawca = null;

		var nabywca = baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.NIP == faktura.Nabywca.NIP);
		if (nabywca == null) baza.Zapisz(nabywca = faktura.Nabywca);
		faktura.NabywcaRef = nabywca;
		faktura.Nabywca = null;
		faktura.NIPNabywcy = nabywca.NIP;
		faktura.NazwaNabywcy = nabywca.Nazwa;
		faktura.DaneNabywcy = nabywca.AdresRejestrowy;

		var waluta = baza.Waluty.FirstOrDefault(waluta => waluta.Skrot == faktura.Waluta.Skrot);
		if (waluta == null) baza.Zapisz(waluta = faktura.Waluta);
		faktura.WalutaRef = waluta;
		faktura.Waluta = null;

		foreach (var pozycja in faktura.Pozycje)
		{
			var stawkaVat = baza.StawkiVat.FirstOrDefault(stawkaVat => stawkaVat.Wartosc == pozycja.StawkaVat.Wartosc);
			if (stawkaVat == null) baza.Zapisz(stawkaVat = pozycja.StawkaVat);
			pozycja.StawkaVatRef = stawkaVat;
			pozycja.StawkaVat = null;

			var towar = baza.Towary.FirstOrDefault(towar => towar.Nazwa == pozycja.Opis);
			pozycja.TowarRef = towar;
			pozycja.Towar = null;
		}
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
		dbFaktura.DataSprzedazy = invoiceHeader.InvoicingDate;
		dbFaktura.DataWystawienia = invoiceHeader.AcquisitionTimestamp;
		dbFaktura.DataKSeF = invoiceHeader.AcquisitionTimestamp;
		dbFaktura.Waluta = new Waluta { Skrot = invoiceHeader.Currency };
		return dbFaktura;
	}
}
