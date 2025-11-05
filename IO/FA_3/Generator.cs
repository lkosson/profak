using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using DBFaktura = ProFak.DB.Faktura;
using KSEFFaktura = ProFak.IO.FA_3.Faktura;

namespace ProFak.IO.FA_3;

class Generator
{
	public static string ZbudujXML(Baza baza, Ref<DBFaktura> dbFakturaRef)
	{
		var dbFaktura = baza.Faktury
			.Include(e => e.Wplaty)
			.Include(e => e.Pozycje).ThenInclude(e => e.JednostkaMiary)
			.Include(e => e.Pozycje).ThenInclude(e => e.StawkaVat)
			.Include(e => e.Sprzedawca)
			.Include(e => e.Nabywca)
			.Include(e => e.Waluta)
			.Include(e => e.FakturaKorygowana)
			.Where(e => e.Id == dbFakturaRef.Id)
			.FirstOrDefault();
		var ksefFaktura = Zbuduj(dbFaktura);
		var xo = new XmlAttributeOverrides();
		var xs = new XmlSerializer(typeof(KSEFFaktura), xo);
		var xml = new StringBuilder();
		using var xw = XmlWriter.Create(xml, new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = true });
		var nss = new XmlSerializerNamespaces();
		xs.Serialize(xw, ksefFaktura, nss);
		return xml.ToString();
	}

	public static DBFaktura ZbudujDB(Baza baza, string xml)
	{
		if (xml.Contains("kodSystemowy=\"FA (2)\"") && xml.Contains("<WariantFormularza>2</WariantFormularza>")) return FA_2.Generator.ZbudujDB(baza, xml);
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
		ksefFaktura.Naglowek.WariantFormularza = TNaglowekWariantFormularza.Item3;
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
		ksefFaktura.Podmiot1.DaneKontaktowe.Add(new FakturaPodmiot1DaneKontaktowe { Email = String.IsNullOrWhiteSpace(dbFaktura.Sprzedawca.EMail) ? null : dbFaktura.Sprzedawca.EMail, Telefon = String.IsNullOrWhiteSpace(dbFaktura.Sprzedawca.Telefon) ? null : dbFaktura.Sprzedawca.Telefon });
		ksefFaktura.Podmiot2 = new FakturaPodmiot2();
		ksefFaktura.Podmiot2.DaneIdentyfikacyjne = new TPodmiot2();
		if (String.IsNullOrEmpty(dbFaktura.NIPNabywcy))
		{
			ksefFaktura.Podmiot2.DaneIdentyfikacyjne.BrakID = TWybor1.Item1;
		}
		else
		{
			ksefFaktura.Podmiot2.DaneIdentyfikacyjne.NIP = dbFaktura.NIPNabywcy;
		}
		ksefFaktura.Podmiot2.DaneIdentyfikacyjne.Nazwa = dbFaktura.NazwaNabywcy;
		ksefFaktura.Podmiot2.Adres = new TAdres();
		ksefFaktura.Podmiot2.Adres.KodKraju = TKodKraju.PL;
		ksefFaktura.Podmiot2.Adres.AdresL1 = dbFaktura.DaneNabywcy.JakoDwieLinie().linia1;
		ksefFaktura.Podmiot2.Adres.AdresL2 = dbFaktura.DaneNabywcy.JakoDwieLinie().linia2;
		ksefFaktura.Podmiot2.IDNabywcy = dbFaktura.NabywcaRef.Id.ToString();
		ksefFaktura.Podmiot2.GV = FakturaPodmiot2GV.Item2;
		ksefFaktura.Podmiot2.JST = FakturaPodmiot2JST.Item2;
		ksefFaktura.Fa = new FakturaFa();
		ksefFaktura.Fa.KodWaluty = dbFaktura.Waluta.Skrot == "zł" ? TKodWaluty.PLN : Enum.Parse<TKodWaluty>(dbFaktura.Waluta.Skrot);
		ksefFaktura.Fa.P_1 = dbFaktura.DataWystawienia;
		ksefFaktura.Fa.P_2 = dbFaktura.Numer;
		ksefFaktura.Fa.P_6 = dbFaktura.DataSprzedazy;
		ksefFaktura.Fa.P_15 = dbFaktura.RazemBrutto;
		ksefFaktura.Fa.Adnotacje = new FakturaFaAdnotacje();
		ksefFaktura.Fa.Adnotacje.P_16 = TWybor1_2.Item2;
		ksefFaktura.Fa.Adnotacje.P_17 = TWybor1_2.Item2;
		ksefFaktura.Fa.Adnotacje.P_18 = TWybor1_2.Item2;
		ksefFaktura.Fa.Adnotacje.P_18A = (dbFaktura.OpisSposobuPlatnosci ?? "").Contains("podzielon", StringComparison.CurrentCultureIgnoreCase) ? TWybor1_2.Item1 : TWybor1_2.Item2;
		ksefFaktura.Fa.Adnotacje.Zwolnienie = new FakturaFaAdnotacjeZwolnienie();
		ksefFaktura.Fa.Adnotacje.Zwolnienie.P_19N = TWybor1.Item1;
		ksefFaktura.Fa.Adnotacje.NoweSrodkiTransportu = new FakturaFaAdnotacjeNoweSrodkiTransportu();
		ksefFaktura.Fa.Adnotacje.NoweSrodkiTransportu.P_22N = TWybor1.Item1;
		ksefFaktura.Fa.Adnotacje.P_23 = TWybor1_2.Item2;
		ksefFaktura.Fa.Adnotacje.PMarzy = new FakturaFaAdnotacjePMarzy();
		ksefFaktura.Fa.Adnotacje.PMarzy.P_PMarzyN = TWybor1.Item1;
		ksefFaktura.Fa.RodzajFaktury = dbFaktura.Rodzaj == DB.RodzajFaktury.Sprzedaż ? TRodzajFaktury.VAT : dbFaktura.Rodzaj == DB.RodzajFaktury.KorektaSprzedaży ? TRodzajFaktury.KOR : throw new ApplicationException("Nieobsługiwany rodzaj faktury: " + dbFaktura.RodzajFmt);
		if (dbFaktura.CzyTP) { ksefFaktura.Fa.TP = TWybor1.Item1; }
		if (!String.IsNullOrEmpty(dbFaktura.UwagiPubliczne)) ksefFaktura.Fa.DodatkowyOpis.Add(new TKluczWartosc() { Klucz = "Uwagi", Wartosc = dbFaktura.UwagiPubliczne });
		ksefFaktura.Fa.Platnosc = new FakturaFaPlatnosc();
		if (dbFaktura.PozostaloDoZaplaty == 0)
		{
			ksefFaktura.Fa.Platnosc.Zaplacono = TWybor1.Item1;
			ksefFaktura.Fa.Platnosc.DataZaplaty = dbFaktura.Wplaty.Last().Data;
		}
		else if (dbFaktura.PozostaloDoZaplaty < dbFaktura.RazemBrutto)
		{
			ksefFaktura.Fa.Platnosc.ZnacznikZaplatyCzesciowej = TWybor1_2.Item1;
			foreach (var wplata in dbFaktura.Wplaty)
				ksefFaktura.Fa.Platnosc.ZaplataCzesciowa.Add(new FakturaFaPlatnoscZaplataCzesciowa { KwotaZaplatyCzesciowej = wplata.Kwota, DataZaplatyCzesciowej = wplata.Data });
		}
		ksefFaktura.Fa.Platnosc.TerminPlatnosci.Add(new FakturaFaPlatnoscTerminPlatnosci { Termin = dbFaktura.TerminPlatnosci });
		ksefFaktura.Fa.Platnosc.FormaPlatnosci = TFormaPlatnosci.Item6;
		if (!String.IsNullOrEmpty(dbFaktura.RachunekBankowy))
		{
			ksefFaktura.Fa.Platnosc.RachunekBankowy.Add(new TRachunekBankowy { NrRB = dbFaktura.RachunekBankowy.Replace(" ", "") });
		}
		if (dbFaktura.FakturaKorygowana != null)
		{
			ksefFaktura.Fa.PrzyczynaKorekty = dbFaktura.UwagiPubliczne;
			ksefFaktura.Fa.TypKorekty = TTypKorekty.Item2;
			var ksefDaneKorygowanej = new FakturaFaDaneFaKorygowanej();
			ksefDaneKorygowanej.DataWystFaKorygowanej = dbFaktura.FakturaKorygowana.DataWystawienia;
			ksefDaneKorygowanej.NrFaKorygowanej = dbFaktura.FakturaKorygowana.Numer;
			if (String.IsNullOrEmpty(dbFaktura.FakturaKorygowana.NumerKSeF))
			{
				ksefDaneKorygowanej.NrKSeFN = TWybor1.Item1;
			}
			else
			{
				ksefDaneKorygowanej.NrKSeF = TWybor1.Item1;
				ksefDaneKorygowanej.NrKSeFFaKorygowanej = dbFaktura.FakturaKorygowana.NumerKSeF;
			}
			ksefFaktura.Fa.DaneFaKorygowanej.Add(ksefDaneKorygowanej);

			if (dbFaktura.FakturaKorygowana.NazwaSprzedawcy != dbFaktura.NazwaSprzedawcy || dbFaktura.FakturaKorygowana.DaneSprzedawcy != dbFaktura.DaneSprzedawcy)
			{
				ksefFaktura.Fa.Podmiot1K = new FakturaFaPodmiot1K();
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
				var podmiot2k = new FakturaFaPodmiot2K();
				podmiot2k.DaneIdentyfikacyjne = new TPodmiot2();
				podmiot2k.DaneIdentyfikacyjne.Nazwa = dbFaktura.FakturaKorygowana.NazwaNabywcy;
				podmiot2k.DaneIdentyfikacyjne = ksefFaktura.Podmiot2.DaneIdentyfikacyjne;
				podmiot2k.Adres = new TAdres();
				podmiot2k.Adres.KodKraju = TKodKraju.PL;
				podmiot2k.Adres.AdresL1 = dbFaktura.FakturaKorygowana.DaneNabywcy.JakoDwieLinie().linia1;
				podmiot2k.Adres.AdresL2 = dbFaktura.FakturaKorygowana.DaneNabywcy.JakoDwieLinie().linia2;
				ksefFaktura.Fa.Podmiot2K.Add(podmiot2k);
			}
		}

		var wiersze = new List<FakturaFaFaWiersz>();
		foreach (var dbPozycja in dbFaktura.Pozycje)
		{
			var ksefWiersz = new FakturaFaFaWiersz();
			ksefWiersz.NrWierszaFa = (ulong)dbPozycja.LP;
			ksefWiersz.UU_ID = dbPozycja.Id.ToString();
			ksefWiersz.P_7 = dbPozycja.Opis;
			ksefWiersz.Indeks = dbPozycja.Towar == null ? ksefWiersz.UU_ID : dbPozycja.Towar.Id.ToString();
			ksefWiersz.P_8A = dbPozycja.JednostkaMiary?.Nazwa ?? "szt";
			ksefWiersz.P_8B = Math.Abs(dbPozycja.Ilosc);
			if (dbPozycja.CzyWedlugCenBrutto)
			{
				ksefWiersz.P_9B = dbPozycja.CenaBrutto;
				ksefWiersz.P_11A = Math.Abs(dbPozycja.WartoscBrutto);
			}
			else
			{
				ksefWiersz.P_9A = dbPozycja.CenaNetto;
				ksefWiersz.P_11 = Math.Abs(dbPozycja.WartoscNetto);
			}
			ksefWiersz.P_11Vat = Math.Abs(dbPozycja.WartoscVat);
			if (dbPozycja.GTU > 0) ksefWiersz.GTU = Enum.Parse<TGTU>("GTU_" + dbPozycja.GTU.ToString("00"));

			if (dbFaktura.CzyWDT)
			{
				ksefFaktura.Fa.P_13_6_2 ??= 0;
				ksefFaktura.Fa.P_13_6_2 += dbPozycja.WartoscNetto;
				ksefWiersz.P_12 = TStawkaPodatku.Item0_WDT;
			}
			else if (dbPozycja.StawkaVat.Skrot.ToLower().Contains("zw"))
			{
				ksefFaktura.Fa.P_13_7 ??= 0;
				ksefFaktura.Fa.P_13_7 += dbPozycja.WartoscNetto;
				ksefWiersz.P_12 = TStawkaPodatku.zw;
			}
			else if (dbPozycja.StawkaVat.Wartosc == 0)
			{
				ksefFaktura.Fa.P_13_6_1 ??= 0;
				ksefFaktura.Fa.P_13_6_1 += dbPozycja.WartoscNetto;
				ksefWiersz.P_12 = TStawkaPodatku.Item0_KR;
			}
			else if (dbPozycja.StawkaVat.Wartosc <= 5)
			{
				ksefFaktura.Fa.P_13_3 ??= 0;
				ksefFaktura.Fa.P_14_3 ??= 0;
				ksefFaktura.Fa.P_13_3 += dbPozycja.WartoscNetto;
				ksefFaktura.Fa.P_14_3 += dbPozycja.WartoscVat;
				ksefWiersz.P_12 = TStawkaPodatku.Item5;
			}
			else if (dbPozycja.StawkaVat.Wartosc <= 8)
			{
				ksefFaktura.Fa.P_13_2 ??= 0;
				ksefFaktura.Fa.P_14_2 ??= 0;
				ksefFaktura.Fa.P_13_2 += dbPozycja.WartoscNetto;
				ksefFaktura.Fa.P_14_2 += dbPozycja.WartoscVat;
				ksefWiersz.P_12 = TStawkaPodatku.Item8;
			}
			else {
				ksefFaktura.Fa.P_13_1 ??= 0;
				ksefFaktura.Fa.P_14_1 ??= 0;
				ksefFaktura.Fa.P_13_1 += dbPozycja.WartoscNetto;
				ksefFaktura.Fa.P_14_1 += dbPozycja.WartoscVat;
				ksefWiersz.P_12 = TStawkaPodatku.Item23;
			}
			if (dbPozycja.CzyPrzedKorekta) ksefWiersz.StanPrzed = TWybor1.Item1;

			ksefFaktura.Fa.FaWiersz.Add(ksefWiersz);
		}
		ksefFaktura.Stopka = new FakturaStopka();
		return ksefFaktura;
	}

	private static DBFaktura Zbuduj(KSEFFaktura ksefFaktura)
	{
		var dbFaktura = new DBFaktura();
		dbFaktura.Numer = ksefFaktura.Fa.P_2;
		dbFaktura.Rodzaj = ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.VAT ? DB.RodzajFaktury.Zakup : ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.KOR ? DB.RodzajFaktury.KorektaZakupu : throw new ApplicationException($"Nieobsługiwany rodzaj faktury: {ksefFaktura.Fa.RodzajFaktury}.");
		dbFaktura.DataWystawienia = ksefFaktura.Fa.P_1;
		if (ksefFaktura.Fa.P_6.HasValue) dbFaktura.DataSprzedazy = ksefFaktura.Fa.P_6.Value;
		else dbFaktura.DataSprzedazy = dbFaktura.DataWystawienia;
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
			if (ksefFaktura.Podmiot1.DaneKontaktowe != null && ksefFaktura.Podmiot1.DaneKontaktowe.Count > 0)
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
				dbFaktura.Nabywca.NIP = ksefFaktura.Podmiot2.DaneIdentyfikacyjne.NIP;
				dbFaktura.Nabywca.Nazwa = ksefFaktura.Podmiot2.DaneIdentyfikacyjne.Nazwa;
			}

			if (ksefFaktura.Podmiot2.Adres != null) dbFaktura.Nabywca.AdresRejestrowy = ksefFaktura.Podmiot2.Adres.AdresL1 + "\r\n" + ksefFaktura.Podmiot2.Adres.AdresL2;
			if (ksefFaktura.Podmiot2.AdresKoresp != null) dbFaktura.Nabywca.AdresKorespondencyjny = ksefFaktura.Podmiot2.AdresKoresp.AdresL1 + "\r\n" + ksefFaktura.Podmiot2.AdresKoresp.AdresL2;
			if (ksefFaktura.Podmiot2.DaneKontaktowe != null && ksefFaktura.Podmiot2.DaneKontaktowe.Count > 0)
			{
				dbFaktura.Nabywca.Telefon = ksefFaktura.Podmiot2.DaneKontaktowe[0].Telefon;
				dbFaktura.Nabywca.EMail = ksefFaktura.Podmiot2.DaneKontaktowe[0].Email;
			}
		}
		if (ksefFaktura.Fa.Platnosc != null)
		{
			if (ksefFaktura.Fa.Platnosc.TerminPlatnosci != null && ksefFaktura.Fa.Platnosc.TerminPlatnosci.Count > 0 && ksefFaktura.Fa.Platnosc.TerminPlatnosci[0].Termin.HasValue) dbFaktura.TerminPlatnosci = ksefFaktura.Fa.Platnosc.TerminPlatnosci[0].Termin.Value;
			if (ksefFaktura.Fa.Platnosc.RachunekBankowy != null && ksefFaktura.Fa.Platnosc.RachunekBankowy.Count > 0) dbFaktura.RachunekBankowy = ksefFaktura.Fa.Platnosc.RachunekBankowy[0].NrRB;

			dbFaktura.OpisSposobuPlatnosci = ksefFaktura.Fa.Platnosc.FormaPlatnosci switch
			{
				TFormaPlatnosci.Item1 => "Gotówka",
				TFormaPlatnosci.Item2 => "Karta",
				TFormaPlatnosci.Item3 => "Bon",
				TFormaPlatnosci.Item4 => "Czek",
				TFormaPlatnosci.Item5 => "Kredyt",
				TFormaPlatnosci.Item6 => "Przelew",
				TFormaPlatnosci.Item7 => "Mobilna",
				_ => "",
			};

			if (ksefFaktura.Fa.Platnosc.DataZaplaty.HasValue) dbFaktura.Wplaty = [new Wplata { Data = ksefFaktura.Fa.Platnosc.DataZaplaty.Value, Kwota = ksefFaktura.Fa.P_15 }];
		}
		dbFaktura.Pozycje = new List<PozycjaFaktury>();
		foreach (var pozycja in ksefFaktura.Fa.FaWiersz)
		{
			var dbPozycja = new PozycjaFaktury();
			dbPozycja.LP = (int)pozycja.NrWierszaFa;
			dbPozycja.Opis = pozycja.P_7;
			dbPozycja.Ilosc = pozycja.P_8B.GetValueOrDefault();
			if (pozycja.P_9B.HasValue && pozycja.P_11A.HasValue)
			{
				dbPozycja.CzyWedlugCenBrutto = true;
				dbPozycja.CenaBrutto = pozycja.P_9B.Value;
				dbPozycja.WartoscBrutto = pozycja.P_11A.Value;
			}
			else
			{
				dbPozycja.CenaNetto = pozycja.P_9A.GetValueOrDefault();
				dbPozycja.WartoscNetto = pozycja.P_11.GetValueOrDefault();
			}
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
			else dbPozycja.StawkaVat = new StawkaVat { Wartosc = 23, Skrot = "23" };
			if (dbPozycja.CzyWedlugCenBrutto)
			{
				dbPozycja.CenaNetto = (dbPozycja.CenaBrutto * 100m / (100 + dbPozycja.StawkaVat.Wartosc)).Zaokragl();
				dbPozycja.CenaVat = (dbPozycja.CenaBrutto - dbPozycja.CenaNetto).Zaokragl();
				dbPozycja.WartoscNetto = (dbPozycja.WartoscBrutto * 100m / (100 + dbPozycja.StawkaVat.Wartosc)).Zaokragl();
				dbPozycja.WartoscVat = (dbPozycja.WartoscBrutto - dbPozycja.WartoscNetto).Zaokragl();
				var wartoscBrutto = (dbPozycja.Ilosc * dbPozycja.CenaBrutto).Zaokragl();
				if (wartoscBrutto != dbPozycja.WartoscBrutto) dbPozycja.CzyWartosciReczne = true;
			}
			else
			{
				dbPozycja.CenaVat = (dbPozycja.CenaNetto * dbPozycja.StawkaVat.Wartosc / 100).Zaokragl();
				dbPozycja.CenaBrutto = (dbPozycja.CenaNetto + dbPozycja.CenaVat).Zaokragl();
				dbPozycja.WartoscVat = (dbPozycja.WartoscNetto * dbPozycja.StawkaVat.Wartosc / 100).Zaokragl();
				dbPozycja.WartoscBrutto = (dbPozycja.WartoscNetto + dbPozycja.WartoscVat).Zaokragl();
				var wartoscNetto = (dbPozycja.Ilosc * dbPozycja.CenaNetto).Zaokragl();
				if (wartoscNetto != dbPozycja.WartoscNetto) dbPozycja.CzyWartosciReczne = true;
			}
			dbFaktura.Pozycje.Add(dbPozycja);
		}
		return dbFaktura;
	}

	private static void PoprawPowiazaniaPoImporcie(Baza baza, DBFaktura faktura)
	{
		var sprzedawca = baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.NIP == faktura.Sprzedawca.NIP);
		if (sprzedawca == null) baza.Zapisz(sprzedawca = faktura.Sprzedawca);
		faktura.SprzedawcaRef = sprzedawca;
		faktura.Sprzedawca = null;
		if (sprzedawca.CzyPodmiot) faktura.Rodzaj = faktura.Rodzaj == RodzajFaktury.KorektaZakupu ? RodzajFaktury.KorektaSprzedaży : RodzajFaktury.Sprzedaż;

		var nabywca = baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.NIP == faktura.Nabywca.NIP);
		if (nabywca == null) baza.Zapisz(nabywca = faktura.Nabywca);
		faktura.NabywcaRef = nabywca;
		faktura.Nabywca = null;
		faktura.NIPNabywcy = nabywca.NIP;
		faktura.NazwaNabywcy = nabywca.Nazwa;
		faktura.DaneNabywcy = nabywca.AdresRejestrowy;

		var sposobyPlatnosci = baza.SposobyPlatnosci.ToList();
		faktura.SposobPlatnosciRef = sposobyPlatnosci
			.OrderBy(sposob => sposob.Nazwa.Contains(faktura.OpisSposobuPlatnosci, StringComparison.CurrentCultureIgnoreCase))
			.ThenBy(sposob => Math.Abs(sposob.LiczbaDni - (faktura.TerminPlatnosci - faktura.DataWystawienia).TotalDays))
			.ThenBy(sposob => sposob.CzyDomyslny ? 0 : 1)
			.FirstOrDefault();

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
}
