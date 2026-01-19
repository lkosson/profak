using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using ProFak.IO.JPK_V7M.DefinicjeTypy;
using ProFak.IO.JPK_V7M.KodyUrzedowSkarbowych;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ProFak.IO.JPK_V7M
{
	class Generator
	{
		public static void Utworz(string plik, Baza baza, DeklaracjaVat deklaracja)
		{
			var jpk = Zbuduj(baza, deklaracja);
			var xo = new XmlAttributeOverrides();
			var xs = new XmlSerializer(typeof(JPK), xo);
			using var xw = XmlWriter.Create(plik, new XmlWriterSettings() { OmitXmlDeclaration = false, Indent = true });
			var nss = new XmlSerializerNamespaces();
			xs.Serialize(xw, jpk, nss);
		}

		private static JPK Zbuduj(Baza baza, DeklaracjaVat deklaracja)
		{
			var podmiot = baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot);
			var faktury = baza.Faktury.Where(faktura => faktura.DeklaracjaVatId == deklaracja.Id)
				.Include(faktura => faktura.Pozycje).ThenInclude(pozycja => pozycja.StawkaVat)
				.ToList();

			var jpk = new JPK();
			jpk.Naglowek = new JPKNaglowek();
			jpk.Naglowek.KodFormularza = new TNaglowekKodFormularza();
			jpk.Naglowek.KodFormularza.kodSystemowy = "JPK_V7M (3)";
			jpk.Naglowek.KodFormularza.wersjaSchemy = "1-0E";
			jpk.Naglowek.WariantFormularza = TNaglowekWariantFormularza.Item3;
			jpk.Naglowek.DataWytworzeniaJPK = DateTime.Now;
			jpk.Naglowek.NazwaSystemu = "ProFak (https://github.com/lkosson/profak)";
			jpk.Naglowek.CelZlozenia = new TNaglowekCelZlozenia();
			jpk.Naglowek.CelZlozenia.Value = TCelZlozenia.Item1;
			jpk.Naglowek.KodUrzedu = Enum.Parse<TKodUS>("Item" + Wymagane(podmiot.KodUrzedu, "Nie uzupełniono kodu urzędu w karcie podmiotu."));
			jpk.Naglowek.Rok = deklaracja.Miesiac.Year.ToString();
			jpk.Naglowek.Miesiac = (sbyte)deklaracja.Miesiac.Month;

			jpk.Podmiot1 = new JPKPodmiot1();
			var jpkpodmiot = new TPodmiotDowolnyBezAdresuOsobaFizyczna();
			jpk.Podmiot1.OsobaFizyczna = jpkpodmiot;
			jpkpodmiot.NIP = Wymagane(podmiot.NIP, "Nie uzupełniono NIPu firmy.");
			jpkpodmiot.ImiePierwsze = Wymagane(podmiot.OsobaFizycznaImie, "Nie podano imienia w karcie podmiotu.");
			jpkpodmiot.Nazwisko = Wymagane(podmiot.OsobaFizycznaNazwisko, "Nie podano nazwiska w karcie podmiotu.");
			jpkpodmiot.DataUrodzenia = podmiot.OsobaFizycznaDataUrodzenia.HasValue ? podmiot.OsobaFizycznaDataUrodzenia.Value : throw new ApplicationException("Nie podano daty urodzenia w karcie podmiotu.");
			jpkpodmiot.Email = Wymagane(podmiot.EMail, "Nie podano adresu e-mail w karcie podmiotu.");
			jpkpodmiot.Telefon = podmiot.Telefon;

			jpk.Ewidencja = new JPKEwidencja();
			jpk.Ewidencja.SprzedazCtrl = new JPKEwidencjaSprzedazCtrl();
			jpk.Ewidencja.ZakupCtrl = new JPKEwidencjaZakupCtrl();
			foreach (var faktura in faktury)
			{
				if (faktura.CzySprzedaz)
				{
					var nipnumer = faktura.NIPNabywcy;
					var nipkraj = "PL";
					if (nipnumer.Length > 2 && Char.IsLetter(nipnumer[0]) && Char.IsLetter(nipnumer[1]))
					{
						nipkraj = nipnumer[0..2];
						nipnumer = nipnumer[2..];
					}
					else if (String.IsNullOrEmpty(nipnumer)) nipnumer = "BRAK";
					var jpksprzedaz = new JPKEwidencjaSprzedazWiersz();
					jpk.Ewidencja.SprzedazWiersz.Add(jpksprzedaz);
					jpksprzedaz.LpSprzedazy = (ulong)jpk.Ewidencja.SprzedazWiersz.Count;
					jpksprzedaz.KodKrajuNadaniaTIN = nipkraj;
					jpksprzedaz.NrKontrahenta = nipnumer;
					jpksprzedaz.NazwaKontrahenta = faktura.NazwaNabywcy;
					jpksprzedaz.DowodSprzedazy = faktura.Numer;
					jpksprzedaz.DataWystawienia = faktura.DataWystawienia;
					jpksprzedaz.DataSprzedazy = faktura.DataSprzedazy;
					if (String.IsNullOrEmpty(faktura.NumerKSeF)) jpksprzedaz.BFK = TWybor1.Item1;
					else jpksprzedaz.NrKSeF = faktura.NumerKSeF;

					foreach (var pozycja in faktura.Pozycje)
					{
						if (pozycja.GTU == 1) { jpksprzedaz.GTU_01 = TWybor1.Item1; }
						else if (pozycja.GTU == 2) { jpksprzedaz.GTU_02 = TWybor1.Item1; }
						else if (pozycja.GTU == 3) { jpksprzedaz.GTU_03 = TWybor1.Item1; }
						else if (pozycja.GTU == 4) { jpksprzedaz.GTU_04 = TWybor1.Item1; }
						else if (pozycja.GTU == 5) { jpksprzedaz.GTU_05 = TWybor1.Item1; }
						else if (pozycja.GTU == 6) { jpksprzedaz.GTU_06 = TWybor1.Item1; }
						else if (pozycja.GTU == 7) { jpksprzedaz.GTU_07 = TWybor1.Item1; }
						else if (pozycja.GTU == 8) { jpksprzedaz.GTU_08 = TWybor1.Item1; }
						else if (pozycja.GTU == 9) { jpksprzedaz.GTU_09 = TWybor1.Item1; }
						else if (pozycja.GTU == 10) { jpksprzedaz.GTU_10 = TWybor1.Item1; }
						else if (pozycja.GTU == 11) { jpksprzedaz.GTU_11 = TWybor1.Item1; }
						else if (pozycja.GTU == 12) { jpksprzedaz.GTU_12 = TWybor1.Item1; }
						else if (pozycja.GTU == 13) { jpksprzedaz.GTU_13 = TWybor1.Item1; }

						if (pozycja.StawkaVat == null) continue;
						if (faktura.CzyWDT) { jpksprzedaz.K_21 ??= 0; jpksprzedaz.K_21 += pozycja.WartoscNetto; }
						else if (pozycja.StawkaVat.Skrot.ToLower().Contains("zw")) { jpksprzedaz.K_10 ??= 0;  jpksprzedaz.K_10 += pozycja.WartoscNetto; }
						else if (pozycja.StawkaVat.Wartosc == 0) { jpksprzedaz.K_13 ??= 0; jpksprzedaz.K_13 += pozycja.WartoscNetto; }
						else if (pozycja.StawkaVat.Wartosc <= 5) { jpksprzedaz.K_15 ??= 0; jpksprzedaz.K_15 += pozycja.WartoscNetto; }
						else if (pozycja.StawkaVat.Wartosc <= 8) { jpksprzedaz.K_17 ??= 0; jpksprzedaz.K_17 += pozycja.WartoscNetto; }
						else { jpksprzedaz.K_19 ??= 0; jpksprzedaz.K_19 += pozycja.WartoscNetto; jpksprzedaz.K_20 ??= 0; jpksprzedaz.K_20 += pozycja.WartoscVat; }
					}

					if (faktura.CzyTP) { jpksprzedaz.TP = TWybor1.Item1; }
					if (faktura.ProceduraMarzy == ProceduraMarży.BiuraPodróży) { jpksprzedaz.MR_T = TWybor1.Item1; }
					if (faktura.ProceduraMarzy == ProceduraMarży.TowaryUżywane || faktura.ProceduraMarzy == ProceduraMarży.DziełaSztuki || faktura.ProceduraMarzy == ProceduraMarży.PrzedmiotyKolekcjonerskie) { jpksprzedaz.MR_UZ = TWybor1.Item1; }
					if (faktura.ProceduraMarzy != ProceduraMarży.NieDotyczy) { jpksprzedaz.SprzedazVAT_Marza ??= 0; jpksprzedaz.SprzedazVAT_Marza += faktura.RazemBrutto; }

					jpk.Ewidencja.SprzedazCtrl.PodatekNalezny += faktura.RazemVat;
				}
				if (faktura.CzyZakup)
				{
					var nipnumer = faktura.NIPSprzedawcy;
					var nipkraj = "PL";
					if (nipnumer.Length > 2 && Char.IsLetter(nipnumer[0]) && Char.IsLetter(nipnumer[1]))
					{
						nipkraj = nipnumer[0..2];
						nipnumer = nipnumer[2..];
					}
					else if (String.IsNullOrEmpty(nipnumer)) nipnumer = "BRAK";

					if (faktura.CzyWNT)
					{
						var jpksprzedaz = new JPKEwidencjaSprzedazWiersz();
						jpk.Ewidencja.SprzedazWiersz.Add(jpksprzedaz);
						jpksprzedaz.LpSprzedazy = (ulong)jpk.Ewidencja.SprzedazWiersz.Count;
						jpksprzedaz.KodKrajuNadaniaTIN = nipkraj;
						jpksprzedaz.NrKontrahenta = nipnumer;
						jpksprzedaz.NazwaKontrahenta = faktura.NazwaSprzedawcy;
						jpksprzedaz.DowodSprzedazy = faktura.Numer;
						if (faktura.Rodzaj == RodzajFaktury.DowódWewnętrzny) jpksprzedaz.DI = TWybor1.Item1;
						else if (String.IsNullOrEmpty(faktura.NumerKSeF)) jpksprzedaz.BFK = TWybor1.Item1;
						else if (faktura.NumerKSeF == "OFFLINE") jpksprzedaz.OFF = TWybor1.Item1;
						else jpksprzedaz.NrKSeF = faktura.NumerKSeF;
						jpksprzedaz.DataWystawienia = faktura.DataWystawienia;
						jpksprzedaz.DataSprzedazy = faktura.DataSprzedazy;
						jpksprzedaz.K_23 = faktura.RazemNetto;
						jpksprzedaz.K_24 = faktura.VatNaliczony;
						jpk.Ewidencja.SprzedazCtrl.PodatekNalezny += faktura.RazemVat;
					}
					else
					{
						var jpkzakup = new JPKEwidencjaZakupWiersz();
						jpk.Ewidencja.ZakupWiersz.Add(jpkzakup);
						jpkzakup.LpZakupu = (ulong)jpk.Ewidencja.ZakupWiersz.Count;
						jpkzakup.KodKrajuNadaniaTIN = nipkraj;
						jpkzakup.NrDostawcy = nipnumer;
						jpkzakup.NazwaDostawcy = faktura.NazwaSprzedawcy;
						jpkzakup.DowodZakupu = faktura.Numer;
						if (faktura.Rodzaj == RodzajFaktury.DowódWewnętrzny) jpkzakup.DI = TWybor1.Item1;
						else if (String.IsNullOrEmpty(faktura.NumerKSeF)) jpkzakup.BFK = TWybor1.Item1;
						else if (faktura.NumerKSeF == "OFFLINE") jpkzakup.OFF = TWybor1.Item1;
						else jpkzakup.NrKSeF = faktura.NumerKSeF;
						jpkzakup.DataZakupu = faktura.DataWystawienia;
						jpkzakup.DataWplywu = faktura.DataWprowadzenia;
						if (faktura.ProceduraMarzy != ProceduraMarży.NieDotyczy) { jpkzakup.ZakupVAT_Marza ??= 0; jpkzakup.ZakupVAT_Marza += faktura.RazemBrutto; }

						if (faktura.CzyZakupSrodkowTrwalych)
						{
							jpkzakup.K_40 = faktura.RazemNetto;
							jpkzakup.K_41 = faktura.VatNaliczony;
						}
						else
						{
							jpkzakup.K_42 = faktura.RazemNetto;
							jpkzakup.K_43 = faktura.VatNaliczony;
						}

						jpk.Ewidencja.ZakupCtrl.PodatekNaliczony += faktura.VatNaliczony;
					}
				}
			}

			jpk.Ewidencja.SprzedazCtrl.LiczbaWierszySprzedazy = (ulong)jpk.Ewidencja.SprzedazWiersz.Count;
			jpk.Ewidencja.ZakupCtrl.LiczbaWierszyZakupow = (ulong)jpk.Ewidencja.ZakupWiersz.Count;

			jpk.Deklaracja = new JPKDeklaracja();
			jpk.Deklaracja.Naglowek = new JPKDeklaracjaNaglowek();
			jpk.Deklaracja.Naglowek.KodFormularzaDekl = new JPKDeklaracjaNaglowekKodFormularzaDekl();
			jpk.Deklaracja.Naglowek.KodFormularzaDekl.Value = TKodFormularzaVAT7.VAT_7;
			jpk.Deklaracja.Naglowek.KodFormularzaDekl.wersjaSchemy = "1-0E";
			jpk.Deklaracja.Naglowek.KodFormularzaDekl.kodSystemowy = "VAT-7 (23)";
			jpk.Deklaracja.Naglowek.WariantFormularzaDekl = JPKDeklaracjaNaglowekWariantFormularzaDekl.Item23;
			jpk.Deklaracja.Pouczenia = 1;
			jpk.Deklaracja.PozycjeSzczegolowe = new JPKDeklaracjaPozycjeSzczegolowe();
			jpk.Deklaracja.PozycjeSzczegolowe.P_10 = LiczbaLubNull(deklaracja.NettoZW);
			jpk.Deklaracja.PozycjeSzczegolowe.P_13 = LiczbaLubNull(deklaracja.Netto0);
			jpk.Deklaracja.PozycjeSzczegolowe.P_15 = LiczbaLubNull(deklaracja.Netto5);
			jpk.Deklaracja.PozycjeSzczegolowe.P_16 = LiczbaLubNull(deklaracja.Nalezny5);
			jpk.Deklaracja.PozycjeSzczegolowe.P_17 = LiczbaLubNull(deklaracja.Netto8);
			jpk.Deklaracja.PozycjeSzczegolowe.P_18 = LiczbaLubNull(deklaracja.Nalezny8);
			jpk.Deklaracja.PozycjeSzczegolowe.P_19 = LiczbaLubNull(deklaracja.Netto23);
			jpk.Deklaracja.PozycjeSzczegolowe.P_20 = LiczbaLubNull(deklaracja.Nalezny23);
			jpk.Deklaracja.PozycjeSzczegolowe.P_21 = LiczbaLubNull(deklaracja.NettoWDT);
			jpk.Deklaracja.PozycjeSzczegolowe.P_23 = LiczbaLubNull(deklaracja.NettoWNT);
			jpk.Deklaracja.PozycjeSzczegolowe.P_24 = LiczbaLubNull(deklaracja.NaleznyWNT);
			jpk.Deklaracja.PozycjeSzczegolowe.P_37 = LiczbaLubNull(deklaracja.NettoRazem);
			jpk.Deklaracja.PozycjeSzczegolowe.P_38 = (long)deklaracja.NaleznyRazem;
			jpk.Deklaracja.PozycjeSzczegolowe.P_39 = LiczbaLubNull(deklaracja.NaliczonyPrzeniesiony);
			jpk.Deklaracja.PozycjeSzczegolowe.P_40 = LiczbaLubNull(deklaracja.NettoSrodkiTrwale);
			jpk.Deklaracja.PozycjeSzczegolowe.P_41 = LiczbaLubNull(deklaracja.NaliczonySrodkiTrwale);
			jpk.Deklaracja.PozycjeSzczegolowe.P_42 = LiczbaLubNull(deklaracja.NettoPozostale);
			jpk.Deklaracja.PozycjeSzczegolowe.P_43 = LiczbaLubNull(deklaracja.NaliczonyPozostale);
			jpk.Deklaracja.PozycjeSzczegolowe.P_48 = LiczbaLubNull(deklaracja.NaliczonyRazem);
			jpk.Deklaracja.PozycjeSzczegolowe.P_51 = (long)deklaracja.DoWplaty;
			jpk.Deklaracja.PozycjeSzczegolowe.P_62 = LiczbaLubNull(deklaracja.DoPrzeniesienia);

			return jpk;
		}

		private static string Wymagane(string wartosc, string komunikat) => String.IsNullOrWhiteSpace(wartosc) ? throw new ApplicationException(komunikat) : wartosc;
		private static long? LiczbaLubNull(decimal wartosc) => wartosc > 0 ? (long?)wartosc : null;
	}
}
