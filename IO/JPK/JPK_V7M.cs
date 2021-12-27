using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.IO.JPK
{
	class JPK_V7M
	{
		public static void Zbuduj(Baza baza, DeklaracjaVat deklaracja)
		{
			var podmiot = baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot);
			var faktury = baza.Faktury.Where(faktura => faktura.DeklaracjaVatId == deklaracja.Id)
				.Include(faktura => faktura.Pozycje).ThenInclude(pozycja => pozycja.StawkaVat)
				.ToList();

			var jpk = new JPK();
			jpk.Naglowek = new JPKNaglowek();
			jpk.Naglowek.KodFormularza = new TNaglowekKodFormularza();
			jpk.Naglowek.KodFormularza.kodSystemowy = "JPK_V7M (1)";
			jpk.Naglowek.KodFormularza.wersjaSchemy = "1-2E";
			jpk.Naglowek.WariantFormularza = 1;
			jpk.Naglowek.DataWytworzeniaJPK = DateTime.Now;
			jpk.Naglowek.NazwaSystemu = "ProFak (https://github.com/lkosson/profak)";
			jpk.Naglowek.CelZlozenia = new TNaglowekCelZlozenia();
			jpk.Naglowek.CelZlozenia.Value = 1;
			jpk.Naglowek.KodUrzedu = Enum.Parse<TKodUS>("Item" + Wymagane(podmiot.KodUrzedu, "Nie uzupełniono kodu urzędu w karcie podmiotu."));
			jpk.Naglowek.Rok = deklaracja.Miesiac.Year.ToString();
			jpk.Naglowek.Miesiac = (sbyte)deklaracja.Miesiac.Month;

			jpk.Podmiot1 = new JPKPodmiot1();
			var jpkpodmiot = new TPodmiotDowolnyBezAdresuOsobaFizyczna();
			jpk.Podmiot1.Item = jpkpodmiot;
			jpkpodmiot.NIP = Wymagane(podmiot.NIP, "Nie uzupełniono NIPu firmy.");
			jpkpodmiot.ImiePierwsze = Wymagane(podmiot.OsobaFizycznaImie, "Nie podano imienia w karcie podmiotu.");
			jpkpodmiot.Nazwisko = Wymagane(podmiot.OsobaFizycznaImie, "Nie podano nazwiska w karcie podmiotu.");
			jpkpodmiot.DataUrodzenia = podmiot.OsobaFizycznaDataUrodzenia.HasValue ? podmiot.OsobaFizycznaDataUrodzenia.Value : throw new ApplicationException("Nie podano daty urodzenia w karcie podmiotu.");
			jpkpodmiot.Email = Wymagane(podmiot.EMail, "Nie podano adresu e-mail w karcie podmiotu.");
			jpkpodmiot.Telefon = podmiot.Telefon;

			jpk.Ewidencja = new JPKEwidencja();
			jpk.Ewidencja.SprzedazCtrl = new JPKEwidencjaSprzedazCtrl();
			jpk.Ewidencja.ZakupCtrl = new JPKEwidencjaZakupCtrl();
			var jpksprzedaze = new List<JPKEwidencjaSprzedazWiersz>();
			var jpkzakupy = new List<JPKEwidencjaZakupWiersz>();
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
					jpksprzedaze.Add(jpksprzedaz);
					jpksprzedaz.LpSprzedazy = jpksprzedaze.Count.ToString();
					jpksprzedaz.KodKrajuNadaniaTIN = nipkraj;
					jpksprzedaz.NrKontrahenta = nipnumer;
					jpksprzedaz.NazwaKontrahenta = faktura.NazwaNabywcy;
					jpksprzedaz.DowodSprzedazy = faktura.Numer;
					jpksprzedaz.DataWystawienia = faktura.DataWystawienia;
					jpksprzedaz.DataSprzedazy = faktura.DataSprzedazy;

					foreach (var pozycja in faktura.Pozycje)
					{
						if (pozycja.GTU == 1) { jpksprzedaz.GTU_01 = 1; jpksprzedaz.GTU_01Specified = true; }
						else if (pozycja.GTU == 2) { jpksprzedaz.GTU_02 = 1; jpksprzedaz.GTU_02Specified = true; }
						else if (pozycja.GTU == 3) { jpksprzedaz.GTU_03 = 1; jpksprzedaz.GTU_03Specified = true; }
						else if (pozycja.GTU == 4) { jpksprzedaz.GTU_04 = 1; jpksprzedaz.GTU_04Specified = true; }
						else if (pozycja.GTU == 5) { jpksprzedaz.GTU_05 = 1; jpksprzedaz.GTU_05Specified = true; }
						else if (pozycja.GTU == 6) { jpksprzedaz.GTU_06 = 1; jpksprzedaz.GTU_06Specified = true; }
						else if (pozycja.GTU == 7) { jpksprzedaz.GTU_07 = 1; jpksprzedaz.GTU_07Specified = true; }
						else if (pozycja.GTU == 8) { jpksprzedaz.GTU_08 = 1; jpksprzedaz.GTU_08Specified = true; }
						else if (pozycja.GTU == 9) { jpksprzedaz.GTU_09 = 1; jpksprzedaz.GTU_09Specified = true; }
						else if (pozycja.GTU == 10) { jpksprzedaz.GTU_10 = 1; jpksprzedaz.GTU_10Specified = true; }
						else if (pozycja.GTU == 11) { jpksprzedaz.GTU_11 = 1; jpksprzedaz.GTU_11Specified = true; }
						else if (pozycja.GTU == 12) { jpksprzedaz.GTU_12 = 1; jpksprzedaz.GTU_12Specified = true; }
						else if (pozycja.GTU == 13) { jpksprzedaz.GTU_13 = 1; jpksprzedaz.GTU_13Specified = true; }

						if (pozycja.StawkaVat == null) continue;
						if (faktura.CzyWDT) { jpksprzedaz.K_21 += pozycja.WartoscNetto; }
						else if (pozycja.StawkaVat.Skrot.ToLower().Contains("zw")) { jpksprzedaz.K_10 += pozycja.WartoscNetto; jpksprzedaz.K_10Specified = true; }
						else if (pozycja.StawkaVat.Wartosc == 0) { jpksprzedaz.K_13 += pozycja.WartoscNetto; jpksprzedaz.K_13Specified = true; }
						else if (pozycja.StawkaVat.Wartosc <= 5) { jpksprzedaz.K_15 += pozycja.WartoscNetto; jpksprzedaz.K_16 += pozycja.WartoscVat; }
						else if (pozycja.StawkaVat.Wartosc <= 8) { jpksprzedaz.K_17 += pozycja.WartoscNetto; jpksprzedaz.K_18 += pozycja.WartoscVat; }
						else { jpksprzedaz.K_19 += pozycja.WartoscNetto; jpksprzedaz.K_20 += pozycja.WartoscVat; }
					}

					if (faktura.CzyTP) { jpksprzedaz.TP = 1; jpksprzedaz.TPSpecified = true; }

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
						jpksprzedaze.Add(jpksprzedaz);
						jpksprzedaz.LpSprzedazy = jpksprzedaze.Count.ToString();
						jpksprzedaz.KodKrajuNadaniaTIN = nipkraj;
						jpksprzedaz.NrKontrahenta = nipnumer;
						jpksprzedaz.NazwaKontrahenta = faktura.NazwaSprzedawcy;
						jpksprzedaz.DowodSprzedazy = faktura.Numer;
						jpksprzedaz.DataWystawienia = faktura.DataWystawienia;
						jpksprzedaz.DataSprzedazy = faktura.DataSprzedazy;
						jpksprzedaz.K_23 = faktura.RazemNetto;
						jpksprzedaz.K_24 = faktura.VatNaliczony;
						jpk.Ewidencja.SprzedazCtrl.PodatekNalezny += faktura.RazemVat;
					}
					else
					{
						var jpkzakup = new JPKEwidencjaZakupWiersz();
						jpkzakupy.Add(jpkzakup);
						jpkzakup.LpZakupu = jpkzakupy.Count.ToString();
						jpkzakup.KodKrajuNadaniaTIN = nipkraj;
						jpkzakup.NrDostawcy = nipnumer;
						jpkzakup.NazwaDostawcy = faktura.NazwaSprzedawcy;
						jpkzakup.DowodZakupu = faktura.Numer;
						jpkzakup.DataZakupu = faktura.DataSprzedazy;
						jpkzakup.DataWplywu = faktura.DataWprowadzenia;

						if (faktura.CzyZakupSrodkowTrwalych)
						{
							jpkzakup.K_40 += faktura.RazemNetto;
							jpkzakup.K_41 += faktura.VatNaliczony;
						}
						else
						{
							jpkzakup.K_42 += faktura.RazemNetto;
							jpkzakup.K_43 += faktura.VatNaliczony;
						}

						jpk.Ewidencja.ZakupCtrl.PodatekNaliczony += faktura.VatNaliczony;
					}
				}
			}

			jpk.Ewidencja.SprzedazWiersz = jpksprzedaze.ToArray();
			jpk.Ewidencja.SprzedazCtrl.LiczbaWierszySprzedazy = jpksprzedaze.Count.ToString();
			jpk.Ewidencja.ZakupWiersz = jpkzakupy.ToArray();
			jpk.Ewidencja.ZakupCtrl.LiczbaWierszyZakupow = jpkzakupy.Count.ToString();

			jpk.Deklaracja = new JPKDeklaracja();
			jpk.Deklaracja.Naglowek = new JPKDeklaracjaNaglowek();
			jpk.Deklaracja.Naglowek.KodFormularzaDekl = new JPKDeklaracjaNaglowekKodFormularzaDekl();
			jpk.Deklaracja.Naglowek.KodFormularzaDekl.Value = TKodFormularzaVAT7.VAT7;
			jpk.Deklaracja.Naglowek.KodFormularzaDekl.wersjaSchemy = "1-2E";
			jpk.Deklaracja.Naglowek.WariantFormularzaDekl = 21;
			jpk.Deklaracja.Pouczenia = 1;
			jpk.Deklaracja.PozycjeSzczegolowe = new JPKDeklaracjaPozycjeSzczegolowe();
			jpk.Deklaracja.PozycjeSzczegolowe.P_10 = deklaracja.NettoZW > 0 ? deklaracja.NettoZW.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_13 = deklaracja.Netto0 > 0 ? deklaracja.Netto0.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_15 = deklaracja.Netto5 > 0 ? deklaracja.Netto5.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_16 = deklaracja.Nalezny5 > 0 ? deklaracja.Nalezny5.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_17 = deklaracja.Netto8 > 0 ? deklaracja.Netto8.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_18 = deklaracja.Nalezny8 > 0 ? deklaracja.Nalezny8.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_19 = deklaracja.Netto23 > 0 ? deklaracja.Netto23.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_20 = deklaracja.Nalezny23 > 0 ? deklaracja.Nalezny23.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_21 = deklaracja.NettoWDT > 0 ? deklaracja.NettoWDT.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_23 = deklaracja.NettoWNT > 0 ? deklaracja.NettoWNT.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_24 = deklaracja.NaleznyWNT > 0 ? deklaracja.NaleznyWNT.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_37 = deklaracja.NettoRazem > 0 ? deklaracja.NettoRazem.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_38 = deklaracja.NaleznyRazem > 0 ? deklaracja.NaleznyRazem.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_39 = deklaracja.NaliczonyPrzeniesiony > 0 ? deklaracja.NaliczonyPrzeniesiony.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_40 = deklaracja.NettoSrodkiTrwale > 0 ? deklaracja.NettoSrodkiTrwale.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_41 = deklaracja.NaliczonySrodkiTrwale > 0 ? deklaracja.NaliczonySrodkiTrwale.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_42 = deklaracja.NettoPozostale > 0 ? deklaracja.NettoPozostale.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_43 = deklaracja.NaliczonyPozostale > 0 ? deklaracja.NaliczonyPozostale.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_48 = deklaracja.NaliczonyRazem > 0 ? deklaracja.NaliczonyRazem.ToString() : null;
			jpk.Deklaracja.PozycjeSzczegolowe.P_51 = deklaracja.DoWplaty.ToString();
			jpk.Deklaracja.PozycjeSzczegolowe.P_62 = deklaracja.DoPrzeniesienia > 0 ? deklaracja.DoPrzeniesienia.ToString() : null;
		}

		private static string Wymagane(string wartosc, string komunikat) => String.IsNullOrWhiteSpace(wartosc) ? throw new ApplicationException(komunikat) : wartosc;
	}
}
