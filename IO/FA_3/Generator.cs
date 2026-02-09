using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using ProFak.IO.FA_3.DefinicjeTypy;
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
			.Include(e => e.FakturaPierwotna)
			.Include(e => e.DodatkowePodmioty)
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

	private static T ZbudujAdres<T>(string adres) where T : TAdres, new()
	{
		var linie = adres.JakoDwieLinie();
		var ksefAdres = new T();
		ksefAdres.KodKraju = TKodKraju.PL;
		if (!String.IsNullOrWhiteSpace(linie.linia1)) ksefAdres.AdresL1 = linie.linia1;
		if (!String.IsNullOrWhiteSpace(linie.linia2)) ksefAdres.AdresL2 = linie.linia2;
		return ksefAdres;
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
		ksefFaktura.Podmiot1.DaneIdentyfikacyjne.NIP = dbFaktura.NIPSprzedawcy.Replace("-", "");
		ksefFaktura.Podmiot1.DaneIdentyfikacyjne.Nazwa = dbFaktura.NazwaSprzedawcy;
		ksefFaktura.Podmiot1.Adres = ZbudujAdres<TAdres>(dbFaktura.DaneSprzedawcy);
		if (!String.IsNullOrEmpty(dbFaktura.Sprzedawca.AdresKorespondencyjny)) ksefFaktura.Podmiot1.AdresKoresp = ZbudujAdres<FakturaPodmiot1AdresKoresp>(dbFaktura.Sprzedawca.AdresKorespondencyjny);
		ksefFaktura.Podmiot1.DaneKontaktowe.Add(new FakturaPodmiot1DaneKontaktowe { Email = String.IsNullOrWhiteSpace(dbFaktura.Sprzedawca.EMail) ? null : dbFaktura.Sprzedawca.EMail, Telefon = String.IsNullOrWhiteSpace(dbFaktura.Sprzedawca.Telefon) ? null : dbFaktura.Sprzedawca.Telefon });
		ksefFaktura.Podmiot2 = new FakturaPodmiot2();
		ksefFaktura.Podmiot2.DaneIdentyfikacyjne = new TPodmiot2();
		if (String.IsNullOrEmpty(dbFaktura.NIPNabywcy))
		{
			ksefFaktura.Podmiot2.DaneIdentyfikacyjne.BrakID = TWybor1.Item1;
		}
		else
		{
			ksefFaktura.Podmiot2.DaneIdentyfikacyjne.NIP = dbFaktura.NIPNabywcy.Replace("-", "");
		}
		ksefFaktura.Podmiot2.DaneIdentyfikacyjne.Nazwa = dbFaktura.NazwaNabywcy;
		ksefFaktura.Podmiot2.Adres = ZbudujAdres<TAdres>(dbFaktura.DaneNabywcy);
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
		if (dbFaktura.Rodzaj != RodzajFaktury.VatMarża && dbFaktura.Rodzaj != RodzajFaktury.KorektaVatMarży) ksefFaktura.Fa.Adnotacje.PMarzy.P_PMarzyN = TWybor1.Item1;
		else if (dbFaktura.ProceduraMarzy == ProceduraMarży.TowaryUżywane) ksefFaktura.Fa.Adnotacje.PMarzy.P_PMarzy_3_1 = TWybor1.Item1;
		else if (dbFaktura.ProceduraMarzy == ProceduraMarży.DziełaSztuki) ksefFaktura.Fa.Adnotacje.PMarzy.P_PMarzy_3_2 = TWybor1.Item1;
		else if (dbFaktura.ProceduraMarzy == ProceduraMarży.BiuraPodróży) ksefFaktura.Fa.Adnotacje.PMarzy.P_PMarzy_2 = TWybor1.Item1;
		else if (dbFaktura.ProceduraMarzy == ProceduraMarży.PrzedmiotyKolekcjonerskie) ksefFaktura.Fa.Adnotacje.PMarzy.P_PMarzy_3_3 = TWybor1.Item1;
		ksefFaktura.Fa.RodzajFaktury = dbFaktura.Rodzaj == RodzajFaktury.Sprzedaż || dbFaktura.Rodzaj == RodzajFaktury.VatMarża ? TRodzajFaktury.VAT
			: dbFaktura.Rodzaj == DB.RodzajFaktury.KorektaSprzedaży || dbFaktura.Rodzaj == RodzajFaktury.KorektaVatMarży ? TRodzajFaktury.KOR
			: throw new ApplicationException("Nieobsługiwany rodzaj faktury: " + dbFaktura.RodzajFmt);
		if (dbFaktura.CzyTP) { ksefFaktura.Fa.TP = TWybor1.Item1; }
		ksefFaktura.Fa.Platnosc = new FakturaFaPlatnosc();
		if (dbFaktura.PozostaloDoZaplaty == 0 && dbFaktura.Wplaty.Count > 0)
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
		if (dbFaktura.PozostaloDoZaplaty > 0) ksefFaktura.Fa.Platnosc.TerminPlatnosci.Add(new FakturaFaPlatnoscTerminPlatnosci { Termin = dbFaktura.TerminPlatnosci });
		if ((dbFaktura.OpisSposobuPlatnosci ?? "").Contains("gotówk", StringComparison.InvariantCultureIgnoreCase)) ksefFaktura.Fa.Platnosc.FormaPlatnosci = TFormaPlatnosci.Item1;
		else if ((dbFaktura.OpisSposobuPlatnosci ?? "").Contains("karta", StringComparison.InvariantCultureIgnoreCase)) ksefFaktura.Fa.Platnosc.FormaPlatnosci = TFormaPlatnosci.Item2;
		else if ((dbFaktura.OpisSposobuPlatnosci ?? "").Contains("kredyt", StringComparison.InvariantCultureIgnoreCase)) ksefFaktura.Fa.Platnosc.FormaPlatnosci = TFormaPlatnosci.Item5;
		else if ((dbFaktura.OpisSposobuPlatnosci ?? "").Contains("mobiln", StringComparison.InvariantCultureIgnoreCase)) ksefFaktura.Fa.Platnosc.FormaPlatnosci = TFormaPlatnosci.Item7;
		else ksefFaktura.Fa.Platnosc.FormaPlatnosci = TFormaPlatnosci.Item6;
		if (!String.IsNullOrEmpty(dbFaktura.RachunekBankowy))
		{
			var ksefRachunek = new TRachunekBankowy { NrRB = dbFaktura.RachunekBankowy.Replace(" ", "") };
			if (!String.IsNullOrEmpty(dbFaktura.NazwaBanku)) ksefRachunek.NazwaBanku = dbFaktura.NazwaBanku;
			ksefFaktura.Fa.Platnosc.RachunekBankowy.Add(ksefRachunek);
		}
		if (dbFaktura.FakturaKorygowana != null && dbFaktura.FakturaPierwotna != null)
		{
			ksefFaktura.Fa.TypKorekty = TTypKorekty.Item2;
			var ksefDaneKorygowanej = new FakturaFaDaneFaKorygowanej();
			ksefDaneKorygowanej.DataWystFaKorygowanej = dbFaktura.FakturaPierwotna.DataWystawienia;
			ksefDaneKorygowanej.NrFaKorygowanej = dbFaktura.FakturaPierwotna.Numer;
			if (String.IsNullOrEmpty(dbFaktura.FakturaPierwotna.NumerKSeF))
			{
				ksefDaneKorygowanej.NrKSeFN = TWybor1.Item1;
			}
			else
			{
				ksefDaneKorygowanej.NrKSeF = TWybor1.Item1;
				ksefDaneKorygowanej.NrKSeFFaKorygowanej = dbFaktura.FakturaPierwotna.NumerKSeF;
			}
			ksefFaktura.Fa.DaneFaKorygowanej.Add(ksefDaneKorygowanej);

			if (dbFaktura.FakturaKorygowana.NazwaSprzedawcy != dbFaktura.NazwaSprzedawcy || dbFaktura.FakturaKorygowana.DaneSprzedawcy != dbFaktura.DaneSprzedawcy)
			{
				ksefFaktura.Fa.Podmiot1K = new FakturaFaPodmiot1K();
				ksefFaktura.Fa.Podmiot1K.DaneIdentyfikacyjne = new TPodmiot1();
				ksefFaktura.Fa.Podmiot1K.DaneIdentyfikacyjne.Nazwa = dbFaktura.FakturaKorygowana.NazwaSprzedawcy;
				ksefFaktura.Fa.Podmiot1K.DaneIdentyfikacyjne.NIP = dbFaktura.FakturaKorygowana.NIPSprzedawcy.Replace("-", "");
				ksefFaktura.Fa.Podmiot1K.Adres = ZbudujAdres<TAdres>(dbFaktura.FakturaKorygowana.DaneSprzedawcy);
			}

			if (dbFaktura.FakturaKorygowana.NazwaNabywcy != dbFaktura.NazwaNabywcy || dbFaktura.FakturaKorygowana.DaneNabywcy != dbFaktura.DaneNabywcy)
			{
				var podmiot2k = new FakturaFaPodmiot2K();
				podmiot2k.DaneIdentyfikacyjne = new TPodmiot2();
				podmiot2k.DaneIdentyfikacyjne.Nazwa = dbFaktura.FakturaKorygowana.NazwaNabywcy;
				podmiot2k.DaneIdentyfikacyjne = ksefFaktura.Podmiot2.DaneIdentyfikacyjne;
				podmiot2k.Adres = ZbudujAdres<TAdres>(dbFaktura.FakturaKorygowana.DaneNabywcy);
				ksefFaktura.Fa.Podmiot2K.Add(podmiot2k);
			}
		}

		foreach (var dbPodmiot3 in dbFaktura.DodatkowePodmioty)
		{
			var ksefPodmiot3 = new FakturaPodmiot3();
			if (dbPodmiot3.Rodzaj == RodzajDodatkowegoPodmiotu.Inny) { ksefPodmiot3.RolaInna = TWybor1.Item1; ksefPodmiot3.OpisRoli = "Inny podmiot"; }
			else if (dbPodmiot3.Rodzaj == RodzajDodatkowegoPodmiotu.Faktor) ksefPodmiot3.Rola = TRolaPodmiotu3.Item1;
			else if (dbPodmiot3.Rodzaj == RodzajDodatkowegoPodmiotu.Odbiorca) ksefPodmiot3.Rola = TRolaPodmiotu3.Item2;
			else if (dbPodmiot3.Rodzaj == RodzajDodatkowegoPodmiotu.PodmiotPierwotny) ksefPodmiot3.Rola = TRolaPodmiotu3.Item3;
			else if (dbPodmiot3.Rodzaj == RodzajDodatkowegoPodmiotu.DodatkowyNabywca) ksefPodmiot3.Rola = TRolaPodmiotu3.Item4;
			else if (dbPodmiot3.Rodzaj == RodzajDodatkowegoPodmiotu.WystawcaFaktury) ksefPodmiot3.Rola = TRolaPodmiotu3.Item5;
			else if (dbPodmiot3.Rodzaj == RodzajDodatkowegoPodmiotu.DokonującyPłatności) ksefPodmiot3.Rola = TRolaPodmiotu3.Item6;
			else if (dbPodmiot3.Rodzaj == RodzajDodatkowegoPodmiotu.JSTWystawca) ksefPodmiot3.Rola = TRolaPodmiotu3.Item7;
			else if (dbPodmiot3.Rodzaj == RodzajDodatkowegoPodmiotu.JSTOdbiorca) ksefPodmiot3.Rola = TRolaPodmiotu3.Item8;
			else if (dbPodmiot3.Rodzaj == RodzajDodatkowegoPodmiotu.CzłonekGrupyVATWystawca) ksefPodmiot3.Rola = TRolaPodmiotu3.Item9;
			else if (dbPodmiot3.Rodzaj == RodzajDodatkowegoPodmiotu.CzłonekGrupyVATOdbiorca) ksefPodmiot3.Rola = TRolaPodmiotu3.Item10;
			else if (dbPodmiot3.Rodzaj == RodzajDodatkowegoPodmiotu.Pracownik) ksefPodmiot3.Rola = TRolaPodmiotu3.Item11;

			ksefPodmiot3.DaneIdentyfikacyjne = new TPodmiot3();
			if (!String.IsNullOrEmpty(dbPodmiot3.Nazwa)) ksefPodmiot3.DaneIdentyfikacyjne.Nazwa = dbPodmiot3.Nazwa;
			if (!String.IsNullOrEmpty(dbPodmiot3.NIP)) ksefPodmiot3.DaneIdentyfikacyjne.NIP = dbPodmiot3.NIP;
			if (!String.IsNullOrEmpty(dbPodmiot3.VatUE)) ksefPodmiot3.DaneIdentyfikacyjne.NrVatUE = dbPodmiot3.VatUE;
			if (!String.IsNullOrEmpty(dbPodmiot3.IDwew)) ksefPodmiot3.DaneIdentyfikacyjne.IDWew = dbPodmiot3.IDwew;
			if (!String.IsNullOrEmpty(dbPodmiot3.Adres)) ksefPodmiot3.Adres = ZbudujAdres<TAdres>(dbPodmiot3.Adres);
			dbPodmiot3.Udzial = ksefPodmiot3.Udzial;

			ksefFaktura.Podmiot3.Add(ksefPodmiot3);
		}

		foreach (var dbPozycja in dbFaktura.Pozycje)
		{
			var ksefWiersz = new FakturaFaFaWiersz();
			ksefWiersz.NrWierszaFa = (ulong)dbPozycja.LP;
			//ksefWiersz.UU_ID = dbPozycja.Id.ToString();
			ksefWiersz.P_7 = dbPozycja.Opis;
			//ksefWiersz.Indeks = dbPozycja.Towar == null ? ksefWiersz.UU_ID : dbPozycja.Towar.Id.ToString();
			ksefWiersz.P_8A = dbPozycja.JednostkaMiary?.Nazwa ?? "szt";
			ksefWiersz.P_8B = Math.Abs(dbPozycja.Ilosc);
			if (dbPozycja.RabatRazem != 0 && ksefWiersz.P_8B != 0) ksefWiersz.P_10 = Math.Min(dbPozycja.CzyWedlugCenBrutto ? dbPozycja.CenaBrutto : dbPozycja.CenaNetto, -dbPozycja.RabatRazem / ksefWiersz.P_8B.Value).Zaokragl();
			if (dbFaktura.KursWaluty != 0 && dbFaktura.KursWaluty != 1) ksefWiersz.KursWaluty = dbFaktura.KursWaluty;
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
			if (dbFaktura.ProceduraMarzy == ProceduraMarży.NieDotyczy) ksefWiersz.P_11Vat = Math.Abs(dbPozycja.WartoscVat);
			if (dbPozycja.GTU > 0) ksefWiersz.GTU = Enum.Parse<TGTU>("GTU_" + dbPozycja.GTU.ToString("00"));

			if (dbFaktura.ProceduraMarzy != ProceduraMarży.NieDotyczy)
			{
				ksefFaktura.Fa.P_13_11 ??= 0;
				ksefFaktura.Fa.P_13_11 += dbPozycja.WartoscBrutto;
			}
			else if (dbFaktura.CzyWDT)
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
			else
			{
				ksefFaktura.Fa.P_13_1 ??= 0;
				ksefFaktura.Fa.P_14_1 ??= 0;
				ksefFaktura.Fa.P_13_1 += dbPozycja.WartoscNetto;
				ksefFaktura.Fa.P_14_1 += dbPozycja.WartoscVat;
				ksefWiersz.P_12 = TStawkaPodatku.Item23;
			}
			if (dbPozycja.CzyPrzedKorekta) ksefWiersz.StanPrzed = TWybor1.Item1;

			ksefFaktura.Fa.FaWiersz.Add(ksefWiersz);
		}

		var rejestry = new FakturaStopkaRejestry();
		var uwagi = dbFaktura.UwagiPubliczne;
		var zamowienie = new FakturaFaWarunkiTransakcjiZamowienia();
		uwagi = Regex.Replace(uwagi, @"BDO: (?<numer>\d{1,9})", m => { rejestry.BDO = m.Groups["numer"].Value; return ""; });
		uwagi = Regex.Replace(uwagi, @"KRS: (?<numer>\d{10})", m => { rejestry.KRS = m.Groups["numer"].Value; return ""; });
		uwagi = Regex.Replace(uwagi, @"(REGON|Regon|regon): (?<numer>(\d{9}|\d{14}))", m => { rejestry.REGON = m.Groups["numer"].Value; return ""; });
		uwagi = Regex.Replace(uwagi, @"(Zamówienie|Nr zamówienia|Numer zamówienia): (?<numer>.+)", m => { zamowienie.NrZamowienia = m.Groups["numer"].Value.Trim(); return ""; });
		uwagi = Regex.Replace(uwagi, @"Data zamówienia: (?<data>[0-9./\-]{8,10})", m => { if (!DateTime.TryParse(m.Groups["data"].Value, out var data)) return "Numer zamówienia: " + m.Groups["data"].Value; zamowienie.DataZamowienia = data; return ""; });
		uwagi = Regex.Replace(uwagi, @"Przyczyna korekty: (?<tekst>.+)", m => { ksefFaktura.Fa.PrzyczynaKorekty = m.Groups["tekst"].Value.Trim(); return ""; });
		uwagi = Regex.Replace(uwagi, @"(Mechanizm podzielonej płatności|Split payment)", m => { ksefFaktura.Fa.Adnotacje.P_18A = TWybor1_2.Item1; return ""; });
		uwagi = Regex.Replace(uwagi, @"(?<nazwa>.+): (?<tekst>.+)", m => { ksefFaktura.Fa.DodatkowyOpis.Add(new TKluczWartosc() { Klucz = m.Groups["nazwa"].Value, Wartosc = m.Groups["tekst"].Value.Trim() }); return ""; });

		uwagi = uwagi.Trim(' ', '\r', '\n', '\t');
		if (!String.IsNullOrEmpty(uwagi)) ksefFaktura.Fa.DodatkowyOpis.Add(new TKluczWartosc() { Klucz = "Uwagi", Wartosc = uwagi });
		if (!String.IsNullOrEmpty(zamowienie.NrZamowienia) || zamowienie.DataZamowieniaValueSpecified)
		{
			ksefFaktura.Fa.WarunkiTransakcji = new FakturaFaWarunkiTransakcji();
			ksefFaktura.Fa.WarunkiTransakcji.Zamowienia.Add(zamowienie);
		}
		if (!String.IsNullOrEmpty(rejestry.BDO) || !String.IsNullOrEmpty(rejestry.KRS) || !String.IsNullOrEmpty(rejestry.REGON))
		{
			ksefFaktura.Stopka = new FakturaStopka();
			ksefFaktura.Stopka.Rejestry.Add(rejestry);
		}

		return ksefFaktura;
	}

	private static DBFaktura Zbuduj(KSEFFaktura ksefFaktura)
	{
		var dbFaktura = new DBFaktura();
		dbFaktura.Numer = ksefFaktura.Fa.P_2;
		dbFaktura.Rodzaj = ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.VAT || ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.ROZ || ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.UPR ? DB.RodzajFaktury.Zakup 
			: ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.KOR || ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.KOR_ROZ ? DB.RodzajFaktury.KorektaZakupu 
			: ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.ZAL || ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.KOR_ZAL ? throw new ApplicationException("Faktury zaliczkowe nie są obsługiwane")
			: throw new ApplicationException($"Nieobsługiwany rodzaj faktury: {ksefFaktura.Fa.RodzajFaktury}.");
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
				dbFaktura.Sprzedawca.Nazwa = dbFaktura.Sprzedawca.PelnaNazwa = dbFaktura.NazwaSprzedawcy = ksefFaktura.Podmiot1.DaneIdentyfikacyjne.Nazwa;
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
				dbFaktura.Nabywca.Nazwa = dbFaktura.Nabywca.PelnaNazwa = dbFaktura.NazwaNabywcy = ksefFaktura.Podmiot2.DaneIdentyfikacyjne.Nazwa;
				dbFaktura.Nabywca.NIP = dbFaktura.NIPNabywcy = ksefFaktura.Podmiot2.DaneIdentyfikacyjne.NIP;
			}

			if (ksefFaktura.Podmiot2.Adres != null) dbFaktura.Nabywca.AdresRejestrowy = dbFaktura.DaneNabywcy = ksefFaktura.Podmiot2.Adres.AdresL1 + "\r\n" + ksefFaktura.Podmiot2.Adres.AdresL2;
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
			if (ksefFaktura.Fa.Platnosc.RachunekBankowy != null && ksefFaktura.Fa.Platnosc.RachunekBankowy.Count > 0)
			{
				dbFaktura.RachunekBankowy = ksefFaktura.Fa.Platnosc.RachunekBankowy[0].NrRB;
				dbFaktura.NazwaBanku = ksefFaktura.Fa.Platnosc.RachunekBankowy[0].NazwaBanku;
			}

			dbFaktura.OpisSposobuPlatnosci = ksefFaktura.Fa.Platnosc.FormaPlatnosci switch
			{
				TFormaPlatnosci.Item1 => "Gotówka",
				TFormaPlatnosci.Item2 => "Karta",
				TFormaPlatnosci.Item3 => "Bon",
				TFormaPlatnosci.Item4 => "Czek",
				TFormaPlatnosci.Item5 => "Kredyt",
				TFormaPlatnosci.Item6 => "Przelew",
				TFormaPlatnosci.Item7 => "Mobilna",
				_ => ksefFaktura.Fa.Platnosc.OpisPlatnosci ?? "",
			};

			if (ksefFaktura.Fa.Platnosc.DataZaplaty.HasValue) dbFaktura.Wplaty = [new Wplata { Data = ksefFaktura.Fa.Platnosc.DataZaplaty.Value, Kwota = ksefFaktura.Fa.P_15 }];
		}
		dbFaktura.Pozycje = new List<PozycjaFaktury>();
		foreach (var pozycja in ksefFaktura.Fa.FaWiersz)
		{
			var dbPozycja = new PozycjaFaktury();
			if (pozycja.NrWierszaFa > 100 && (int)pozycja.NrWierszaFa > ksefFaktura.Fa.FaWiersz.Count) dbPozycja.LP = dbFaktura.Pozycje.Count + 1;
			else dbPozycja.LP = (int)pozycja.NrWierszaFa;
			dbPozycja.Opis = pozycja.P_7 ?? "";
			dbPozycja.Ilosc = pozycja.P_8B ?? 1;
			dbPozycja.RabatCena = pozycja.P_10Value;
			if (ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.UPR && dbPozycja.LP == 1)
			{
				dbPozycja.CzyWedlugCenBrutto = true;
				dbPozycja.CenaBrutto = ksefFaktura.Fa.P_15;
				dbPozycja.WartoscBrutto = ksefFaktura.Fa.P_15;
			}
			else if (pozycja.P_9B.HasValue && pozycja.P_11A.HasValue)
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
			dbPozycja.Towar.JednostkaMiary = dbPozycja.JednostkaMiary = new JednostkaMiary { Nazwa = pozycja.P_8A, Skrot = pozycja.P_8A };
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
			if (pozycja.StanPrzed == TWybor1.Item1)
			{
				dbPozycja.CzyPrzedKorekta = true;
				dbPozycja.Ilosc = -dbPozycja.Ilosc;
				dbPozycja.WartoscNetto = -dbPozycja.WartoscNetto;
				dbPozycja.WartoscVat = -dbPozycja.WartoscVat;
				dbPozycja.WartoscBrutto = -dbPozycja.WartoscBrutto;
			}
			dbFaktura.Pozycje.Add(dbPozycja);
			dbFaktura.RazemNetto += dbPozycja.WartoscNetto;
			dbFaktura.RazemVat += dbPozycja.WartoscVat;
			dbFaktura.RazemBrutto += dbPozycja.WartoscBrutto;
			if (pozycja.KursWalutyValueSpecified) dbFaktura.KursWaluty = pozycja.KursWalutyValue;
		}

		if (dbFaktura.RazemBrutto != ksefFaktura.Fa.P_15)
		{
			dbFaktura.RazemBrutto = ksefFaktura.Fa.P_15;
			dbFaktura.CzyWartosciReczne = true;
		}

		var uwagi = new StringBuilder();

		if (ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.UPR) uwagi.AppendLine("Faktura uproszczona");
		else if (ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.ROZ || ksefFaktura.Fa.RodzajFaktury == TRodzajFaktury.KOR_ROZ) uwagi.AppendLine("Faktura rozliczająca");

		foreach (var opis in ksefFaktura.Fa.DodatkowyOpis)
		{
			uwagi.AppendLine($"{opis.Klucz}: {opis.Wartosc}");
		}

		foreach (var ksefFakturaKorygowana in ksefFaktura.Fa.DaneFaKorygowanej)
		{
			dbFaktura.FakturaKorygowana = new DBFaktura();
			if (!String.IsNullOrEmpty(ksefFakturaKorygowana.NrKSeFFaKorygowanej)) dbFaktura.FakturaKorygowana.NumerKSeF = ksefFakturaKorygowana.NrKSeFFaKorygowanej;
			if (!String.IsNullOrEmpty(ksefFakturaKorygowana.NrFaKorygowanej)) dbFaktura.FakturaKorygowana.Numer = ksefFakturaKorygowana.NrFaKorygowanej;
			dbFaktura.DataWystawienia = ksefFakturaKorygowana.DataWystFaKorygowanej;
		}

		if (!String.IsNullOrEmpty(ksefFaktura.Fa.PrzyczynaKorekty)) uwagi.AppendLine($"Przyczyna korekty: {ksefFaktura.Fa.PrzyczynaKorekty}");

		foreach (var fakturaZaliczkowa in ksefFaktura.Fa.FakturaZaliczkowa)
		{
			if (fakturaZaliczkowa.NrKSeFZN == TWybor1.Item1) uwagi.AppendLine($"Numer faktury zaliczkowej: {fakturaZaliczkowa.NrFaZaliczkowej}");
			else uwagi.AppendLine($"Numer faktury zaliczkowej: {fakturaZaliczkowa.NrKSeFFaZaliczkowej}");
		}

		dbFaktura.DodatkowePodmioty = new List<DodatkowyPodmiot>();
		foreach (var ksefPodmiot3 in ksefFaktura.Podmiot3)
		{
			var dbPodmiot3 = new DodatkowyPodmiot();
			if (ksefPodmiot3.Rola == TRolaPodmiotu3.Item1) dbPodmiot3.Rodzaj = RodzajDodatkowegoPodmiotu.Faktor;
			if (ksefPodmiot3.Rola == TRolaPodmiotu3.Item2) dbPodmiot3.Rodzaj = RodzajDodatkowegoPodmiotu.Odbiorca;
			if (ksefPodmiot3.Rola == TRolaPodmiotu3.Item3) dbPodmiot3.Rodzaj = RodzajDodatkowegoPodmiotu.PodmiotPierwotny;
			if (ksefPodmiot3.Rola == TRolaPodmiotu3.Item4) dbPodmiot3.Rodzaj = RodzajDodatkowegoPodmiotu.DodatkowyNabywca;
			if (ksefPodmiot3.Rola == TRolaPodmiotu3.Item5) dbPodmiot3.Rodzaj = RodzajDodatkowegoPodmiotu.WystawcaFaktury;
			if (ksefPodmiot3.Rola == TRolaPodmiotu3.Item6) dbPodmiot3.Rodzaj = RodzajDodatkowegoPodmiotu.DokonującyPłatności;
			if (ksefPodmiot3.Rola == TRolaPodmiotu3.Item7) dbPodmiot3.Rodzaj = RodzajDodatkowegoPodmiotu.JSTWystawca;
			if (ksefPodmiot3.Rola == TRolaPodmiotu3.Item8) dbPodmiot3.Rodzaj = RodzajDodatkowegoPodmiotu.JSTOdbiorca;
			if (ksefPodmiot3.Rola == TRolaPodmiotu3.Item9) dbPodmiot3.Rodzaj = RodzajDodatkowegoPodmiotu.CzłonekGrupyVATWystawca;
			if (ksefPodmiot3.Rola == TRolaPodmiotu3.Item10) dbPodmiot3.Rodzaj = RodzajDodatkowegoPodmiotu.CzłonekGrupyVATOdbiorca;
			if (ksefPodmiot3.Rola == TRolaPodmiotu3.Item11) dbPodmiot3.Rodzaj = RodzajDodatkowegoPodmiotu.Pracownik;
			dbPodmiot3.Nazwa = ksefPodmiot3.DaneIdentyfikacyjne.Nazwa;
			dbPodmiot3.NIP = ksefPodmiot3.DaneIdentyfikacyjne.NIP;
			dbPodmiot3.VatUE = ksefPodmiot3.DaneIdentyfikacyjne.NrVatUE;
			dbPodmiot3.IDwew = ksefPodmiot3.DaneIdentyfikacyjne.IDWew;
			if (ksefPodmiot3.Adres != null) dbPodmiot3.Adres = ksefPodmiot3.Adres.AdresL1 + "\n" + ksefPodmiot3.Adres.AdresL2;
			dbPodmiot3.Udzial = ksefPodmiot3.Udzial;
			dbFaktura.DodatkowePodmioty.Add(dbPodmiot3);
		}

		if (ksefFaktura.Fa.Adnotacje != null)
		{
			if (ksefFaktura.Fa.Adnotacje.Zwolnienie != null)
			{
				if (ksefFaktura.Fa.Adnotacje.Zwolnienie.P_19 == TWybor1.Item1) uwagi.AppendLine("Dostawa towarów lub świadczenie usług zwolnionych od podatku na podstawie art. 43 ust. 1, art. 113 ust. 1 i 9 albo przepisów wydanych na podstawie art. 82 ust. 3 lub na podstawie innych przepisów");
				if (!String.IsNullOrEmpty(ksefFaktura.Fa.Adnotacje.Zwolnienie.P_19A)) uwagi.AppendLine($"Podstawa zwolnienia od podatku: {ksefFaktura.Fa.Adnotacje.Zwolnienie.P_19A}");
				if (!String.IsNullOrEmpty(ksefFaktura.Fa.Adnotacje.Zwolnienie.P_19B)) uwagi.AppendLine($"Podstawa zwolnienia od podatku: {ksefFaktura.Fa.Adnotacje.Zwolnienie.P_19B}");
				if (!String.IsNullOrEmpty(ksefFaktura.Fa.Adnotacje.Zwolnienie.P_19C)) uwagi.AppendLine($"Podstawa zwolnienia od podatku: {ksefFaktura.Fa.Adnotacje.Zwolnienie.P_19C}");
			}

			if (ksefFaktura.Fa.Adnotacje.PMarzy != null)
			{
				if (ksefFaktura.Fa.Adnotacje.PMarzy.P_PMarzy_3_1ValueSpecified) { dbFaktura.ProceduraMarzy = ProceduraMarży.TowaryUżywane; uwagi.AppendLine("Procedura marży: towary używane"); }
				if (ksefFaktura.Fa.Adnotacje.PMarzy.P_PMarzy_3_2ValueSpecified) { dbFaktura.ProceduraMarzy = ProceduraMarży.DziełaSztuki; uwagi.AppendLine("Procedura marży: dzieła sztuki"); }
				if (ksefFaktura.Fa.Adnotacje.PMarzy.P_PMarzy_2ValueSpecified) { dbFaktura.ProceduraMarzy = ProceduraMarży.BiuraPodróży; uwagi.AppendLine("Procedura marży: biura podróży"); }
				if (ksefFaktura.Fa.Adnotacje.PMarzy.P_PMarzy_3_3ValueSpecified) { dbFaktura.ProceduraMarzy = ProceduraMarży.PrzedmiotyKolekcjonerskie; uwagi.AppendLine("Procedura marży: przedmioty kolekcjonerskie i antyki"); }
			}

			if (ksefFaktura.Fa.Adnotacje.P_18A == TWybor1_2.Item1) uwagi.AppendLine("Mechanizm podzielonej płatności");
			if (ksefFaktura.Fa.Adnotacje.P_16 == TWybor1_2.Item1) uwagi.AppendLine("Metoda kasowa");
			if (ksefFaktura.Fa.Adnotacje.P_17 == TWybor1_2.Item1) uwagi.AppendLine("Samofakturowanie");
			if (ksefFaktura.Fa.Adnotacje.P_18 == TWybor1_2.Item1) uwagi.AppendLine("Odwrotne obciążenie");
			if (ksefFaktura.Fa.Adnotacje.P_23 == TWybor1_2.Item1) uwagi.AppendLine("Procedura trójstronna uproszczona");

			// Pominięte: NoweSrodkiTransportu
		}

		if (ksefFaktura.Fa.Rozliczenie != null)
		{
			foreach (var obciazenie in ksefFaktura.Fa.Rozliczenie.Obciazenia)
			{
				if (obciazenie.Kwota != 0)
				{
					uwagi.AppendLine($"Obciążenie - {obciazenie.Powod}: {obciazenie.Kwota:0.00} {ksefFaktura.Fa.KodWaluty}");
					dbFaktura.RazemBrutto += obciazenie.Kwota;
					dbFaktura.CzyWartosciReczne = true;
				}
			}
			foreach (var odliczenie in ksefFaktura.Fa.Rozliczenie.Odliczenia)
			{
				if (odliczenie.Kwota != 0)
				{
					uwagi.AppendLine($"Odliczenie - {odliczenie.Powod}: {odliczenie.Kwota:0.00} {ksefFaktura.Fa.KodWaluty}");
					dbFaktura.RazemBrutto -= odliczenie.Kwota;
					dbFaktura.CzyWartosciReczne = true;
				}
			}

			if (ksefFaktura.Fa.Rozliczenie.DoZaplatyValueSpecified && ksefFaktura.Fa.Rozliczenie.DoZaplatyValue != dbFaktura.RazemBrutto) uwagi.AppendLine($"Do zapłaty: {ksefFaktura.Fa.Rozliczenie.DoZaplatyValue:0.00} {ksefFaktura.Fa.KodWaluty}");
			if (ksefFaktura.Fa.Rozliczenie.DoRozliczeniaValueSpecified) uwagi.AppendLine($"Do rozliczenia: {ksefFaktura.Fa.Rozliczenie.DoRozliczeniaValue:0.00} {ksefFaktura.Fa.KodWaluty}");
		}

		if (ksefFaktura.Fa.WarunkiTransakcji != null)
		{
			foreach (var zamowienie in ksefFaktura.Fa.WarunkiTransakcji.Zamowienia)
			{
				if (zamowienie.DataZamowieniaValueSpecified) uwagi.AppendLine($"Data zamówienia: {zamowienie.DataZamowieniaValue:yyyy-MM-dd}");
				uwagi.AppendLine($"Numer zamówienia: {zamowienie.NrZamowienia}");
			}

			foreach (var partia in ksefFaktura.Fa.WarunkiTransakcji.NrPartiiTowaru)
			{
				uwagi.AppendLine($"Numer partii: {partia}");
			}

			if (!String.IsNullOrEmpty(ksefFaktura.Fa.WarunkiTransakcji.WarunkiDostawy)) uwagi.AppendLine($"Warunki dostawy: {ksefFaktura.Fa.WarunkiTransakcji.WarunkiDostawy}");

			foreach (var transport in ksefFaktura.Fa.WarunkiTransakcji.Transport)
			{
				uwagi.AppendLine(transport.RodzajTransportu switch
				{
					TRodzajTransportu.Item1 => "Rodzaj transportu: Transport morski",
					TRodzajTransportu.Item2 => "Rodzaj transportu: Transport kolejowy",
					TRodzajTransportu.Item3 => "Rodzaj transportu: Transport drogowy",
					TRodzajTransportu.Item4 => "Rodzaj transportu: Transport lotniczy",
					TRodzajTransportu.Item5 => "Rodzaj transportu: Przesyłka pocztowa",
					TRodzajTransportu.Item7 => "Rodzaj transportu: Stałe instalacje przesyłowe",
					TRodzajTransportu.Item8 => "Rodzaj transportu: Żegluga śródlądowa",
					_ => "Rodzaj transportu: " + transport.RodzajTransportu
				});
				if (!String.IsNullOrEmpty(transport.OpisInnegoTransportu)) uwagi.AppendLine($"Rodzaj transportu: {transport.OpisInnegoTransportu}");

				if (transport.Przewoznik != null)
				{
					if (transport.Przewoznik.DaneIdentyfikacyjne != null) uwagi.AppendLine($"Przewoźnik: {transport.Przewoznik.DaneIdentyfikacyjne.Nazwa}");
					// Pominięte: DaneIdentyfikacyjne.NIP, AdresPrzewoznika
				}

				uwagi.AppendLine(transport.OpisLadunku switch
				{
					TLadunek.Item1 => "Opis ładunku: Bańka",
					TLadunek.Item2 => "Opis ładunku: Beczka",
					TLadunek.Item3 => "Opis ładunku: Butla",
					TLadunek.Item4 => "Opis ładunku: Karton",
					TLadunek.Item5 => "Opis ładunku: Kanister",
					TLadunek.Item6 => "Opis ładunku: Klatka",
					TLadunek.Item7 => "Opis ładunku: Kontener",
					TLadunek.Item8 => "Opis ładunku: Kosz/koszyk",
					TLadunek.Item9 => "Opis ładunku: Łubianka",
					TLadunek.Item10 => "Opis ładunku: Opakowanie zbiorcze",
					TLadunek.Item11 => "Opis ładunku: Paczka",
					TLadunek.Item12 => "Opis ładunku: Pakiet",
					TLadunek.Item13 => "Opis ładunku: Paleta",
					TLadunek.Item14 => "Opis ładunku: Pojemnik",
					TLadunek.Item15 => "Opis ładunku: Pojemnik do ładunków masowych stałych",
					TLadunek.Item16 => "Opis ładunku: Pojemnik do ładunków masowych w postaci płynnej",
					TLadunek.Item17 => "Opis ładunku: Pudełko",
					TLadunek.Item18 => "Opis ładunku: Puszka",
					TLadunek.Item19 => "Opis ładunku: Skrzynia",
					TLadunek.Item20 => "Opis ładunku: Worek",
					_ => "Opis ładunku: " + transport.OpisLadunku
				});

				if (!String.IsNullOrEmpty(transport.OpisInnegoLadunku)) uwagi.AppendLine($"Opis ładunku: {transport.OpisInnegoLadunku}");
				if (!String.IsNullOrEmpty(transport.JednostkaOpakowania)) uwagi.AppendLine($"Jednostka opakowania: {transport.JednostkaOpakowania}");
				if (transport.DataGodzRozpTransportuValueSpecified) uwagi.AppendLine($"Czas rozpoczęcia transportu: {transport.DataGodzRozpTransportuValue:yyyy-MM-dd HH:mm}");

				// Pominięte: WysylkaZ, WysylkaDo
			}
		}

		if (ksefFaktura.Stopka != null)
		{
			foreach (var informacja in ksefFaktura.Stopka.Informacje)
			{
				uwagi.AppendLine(informacja.StopkaFaktury);
			}

			foreach (var rejestr in ksefFaktura.Stopka.Rejestry)
			{
				if (!String.IsNullOrEmpty(rejestr.PelnaNazwa)) uwagi.AppendLine($"Pełna nazwa: {rejestr.PelnaNazwa}");
				if (!String.IsNullOrEmpty(rejestr.KRS)) uwagi.AppendLine($"KRS: {rejestr.KRS}");
				if (!String.IsNullOrEmpty(rejestr.REGON)) uwagi.AppendLine($"REGON: {rejestr.REGON}");
				if (!String.IsNullOrEmpty(rejestr.BDO)) uwagi.AppendLine($"BDO: {rejestr.BDO}");
			}
		}

		dbFaktura.UwagiPubliczne = uwagi.ToString();

		return dbFaktura;
	}

	private static Kontrahent ZnajdzLubUtworzKontrahenta(Baza baza, Kontrahent kontrahent)
	{
		if (kontrahent == null) return null;
		if (kontrahent.Id > 0) return kontrahent;
		var nip = kontrahent.NIP;
		if (String.IsNullOrEmpty(nip)) return kontrahent;
		if (nip.StartsWith("PL")) nip = nip.Substring(2);
		var nipPL = $"PL{nip}";
		var kontrahentDb = baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.NIP.Replace("-", "") == nip || kontrahent.NIP.Replace("-", "") == nipPL);
		if (kontrahentDb != null) return kontrahentDb;
		baza.Zapisz(kontrahent);
		return kontrahent;
	}

	private static void PoprawPowiazaniaPoImporcie(Baza baza, DBFaktura faktura)
	{
		var sprzedawca = ZnajdzLubUtworzKontrahenta(baza, faktura.Sprzedawca);
		faktura.SprzedawcaRef = sprzedawca;
		faktura.Sprzedawca = null;

		var nabywca = ZnajdzLubUtworzKontrahenta(baza, faktura.Nabywca);
		faktura.NabywcaRef = nabywca;
		faktura.Nabywca = null;

		if (sprzedawca.CzyPodmiot) faktura.Rodzaj = faktura.ProceduraMarzy == ProceduraMarży.NieDotyczy
			? faktura.Rodzaj == RodzajFaktury.KorektaZakupu ? RodzajFaktury.KorektaSprzedaży : RodzajFaktury.Sprzedaż
			: faktura.Rodzaj == RodzajFaktury.KorektaZakupu ? RodzajFaktury.KorektaVatMarży: RodzajFaktury.VatMarża;

		if (String.IsNullOrEmpty(faktura.NIPNabywcy)) faktura.NIPNabywcy = nabywca.NIP;
		if (String.IsNullOrEmpty(faktura.NazwaNabywcy)) faktura.NazwaNabywcy = nabywca.PelnaNazwa;
		if (String.IsNullOrEmpty(faktura.DaneNabywcy)) faktura.DaneNabywcy = nabywca.AdresRejestrowy;

		if (faktura.FakturaKorygowana != null)
		{
			var fakturaKorygowana = String.IsNullOrEmpty(faktura.FakturaKorygowana.NumerKSeF) ? null : baza.Faktury.FirstOrDefault(f => f.NumerKSeF == faktura.FakturaKorygowana.NumerKSeF && f.Rodzaj != RodzajFaktury.Usunięta);
			fakturaKorygowana ??= String.IsNullOrEmpty(faktura.FakturaKorygowana.Numer) ? null : baza.Faktury.FirstOrDefault(f => f.Numer == f.FakturaKorygowana.Numer && f.Sprzedawca == faktura.Sprzedawca && f.Rodzaj != RodzajFaktury.Usunięta);
			if (fakturaKorygowana == null) faktura.UwagiPubliczne = $"Korekta do {faktura.FakturaKorygowana.Numer} z dnia {faktura.FakturaKorygowana.DataWystawienia:yyyy-MM-dd}\r\n{faktura.UwagiPubliczne}";
			else faktura.FakturaKorygowanaRef = fakturaKorygowana;
			faktura.FakturaKorygowana = null;
		}

		var sposobyPlatnosci = baza.SposobyPlatnosci.ToList();
		faktura.SposobPlatnosciRef = sposobyPlatnosci
			.OrderBy(sposob => sposob.Nazwa.Contains(faktura.OpisSposobuPlatnosci, StringComparison.CurrentCultureIgnoreCase))
			.ThenBy(sposob => Math.Abs(sposob.LiczbaDni - (faktura.TerminPlatnosci - faktura.DataWystawienia).TotalDays))
			.ThenBy(sposob => sposob.CzyDomyslny ? 0 : 1)
			.FirstOrDefault();

		var waluta = baza.Waluty.FirstOrDefault(waluta => waluta.Skrot.ToLower() == faktura.Waluta.Skrot.ToLower());
		if (waluta == null) baza.Zapisz(waluta = faktura.Waluta);
		faktura.WalutaRef = waluta;
		faktura.Waluta = null;

		foreach (var pozycja in faktura.Pozycje)
		{
			var stawkaVat = baza.StawkiVat.FirstOrDefault(stawkaVat => stawkaVat.Wartosc == pozycja.StawkaVat.Wartosc);
			if (stawkaVat == null) baza.Zapisz(stawkaVat = pozycja.StawkaVat);
			pozycja.StawkaVatRef = stawkaVat;
			pozycja.StawkaVat = null;

			var jednostka = String.IsNullOrEmpty(pozycja.JednostkaMiary.Nazwa)
				? baza.JednostkiMiar.FirstOrDefault(jednostka => jednostka.CzyDomyslna)
				: baza.JednostkiMiar.FirstOrDefault(jednostka => jednostka.Nazwa.ToLower() == pozycja.JednostkaMiary.Nazwa.ToLower()
					|| jednostka.Skrot.ToLower() == pozycja.JednostkaMiary.Skrot.ToLower()
					|| jednostka.Skrot.ToLower() == pozycja.JednostkaMiary.Skrot.ToLower().TrimEnd('.'));
			if (jednostka == null) baza.Zapisz(jednostka = pozycja.JednostkaMiary);
			pozycja.JednostkaMiaryRef = jednostka;
			pozycja.JednostkaMiary = null;

			var towar = baza.Towary.FirstOrDefault(towar => towar.Nazwa.ToLower() == pozycja.Opis.ToLower());
			pozycja.TowarRef = towar;
			pozycja.Towar = null;
			if (towar != null) pozycja.JednostkaMiaryRef = towar.JednostkaMiaryRef;
		}
	}

	public static void PoprawPowiazaniaPoZapisie(Baza baza, DBFaktura faktura)
	{
		if (faktura.FakturaKorygowanaRef.IsNotNull)
		{
			var fakturaKorygowana = baza.Znajdz(faktura.FakturaKorygowanaRef);
			fakturaKorygowana.FakturaKorygujacaRef = faktura;
			baza.Zapisz(fakturaKorygowana);
		}
	}
}
