using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class DaneDemo
	{
		private static Random rnd = new Random();

		public static void Zaladuj(Baza baza)
		{
			ZaladujPodmiot(baza);
			ZaladujKontrahentow(baza);
			ZaladujTowary(baza);
			ZaladujFaktury(baza);
		}

		private static void ZaladujPodmiot(Baza baza)
		{
			baza.Zapisz(new Kontrahent
			{
				Nazwa = "ProFak",
				PelnaNazwa = "ProFak Technologie Sp. z o.o.",
				NIP = "928-046-10-04",
				AdresKorespondencyjny = "ul. Tulipanów 42\r\n03-019 Warszawa",
				AdresRejestrowy = "ul. Tulipanów 42\r\n03-019 Warszawa",
				EMail = "profak@lukasz.kosson.net",
				RachunekBankowy = "92 5823 0295 0295 0320 1204 2442",
				Telefon = "902-042-120",
				CzyPodmiot = true
			});
		}

		private static void ZaladujKontrahentow(Baza baza)
		{
			var prefiksy = new[] { "PHU", "FH", "FHU", "Firma", "Przedsiębiorstwo", "Zakład" };
			var glowne = new[] { "Maxi", "Bud", "Arco", "Togi", "Solid", "Import", "Dystro", "Rolem", "Forte", "Mezzo", "Inter", "Posi", "Zeta", "Alfa", "Delta", "Handlo", "Olsen", "Aqua", "Hydro", "Kontekst", "Digi", "Efekt", "Meritum", "Medik", "Komp", "Kardio", "Nowa", "Premium", "Trick", "Chemia" };
			var infiksy = new[] { "Fach", "Pol", "Ex", "Waw" };
			var sufiksy = new[] { "i wspólnicy", "i spółka", "SpJ", "Sp. z o.o.", "SA", "Sp. K.", "s.c." };
			var ulice = new[] { "Złota", "Jasna", "Piękna", "Plażowa", "Leśna", "Wilgi", "Śnieżna", "Wolna", "Dobra", "Wysoka", "Stalowa", "Żelazna", "Węglarska", "Handlowa", "Roślinna", "Mleczna", "Senna", "Topazowa", "Targowa", "Rzeczna", "Wilcza", "Kadrowa", "Garnizonowa", "Polna", "Zabawna", "Poprawna", "Krzywa", "Prosta", "Wspólna", "Lipowa", "Brzozowa", "Klonowa" };
			var miasta = new[] { "Warszawa", "Kraków", "Łódź", "Wrocław", "Poznań", "Gdańsk", "Szczecin", "Bydgoszcz", "Lublin", "Katowice", "Białystok", "Gdynia", "Częstochowa", "Radom", "Sosnowiec", "Toruń", "Kielce", "Rzeszów", "Gliwice", "Zabrze", "Olsztyn", "Bielsko-Biała", "Bytom", "Ruda Śląska", "Rybnik", "Zielona Góra", "Tychy", "Gorzów Wielkopolski", "Dąbrowa Górnicza", "Elbląg", "Płock", "Opole", "Wałbrzych", "Włocławek", "Tarnów", "Chorzów", "Koszalin", "Kalisz", "Legnica", "Grudziądz" };
			var max = rnd.Next(glowne.Length / 2, glowne.Length);

			for (int i = 0; i < max; i++)
			{
				var skrot = glowne[rnd.Next(glowne.Length)];
				if (rnd.Next(100) < 20) skrot = skrot + (rnd.Next(100) < 50 ? "-" : "") + (rnd.Next(100) < 70 ? infiksy[rnd.Next(infiksy.Length)].ToLower() : infiksy[rnd.Next(infiksy.Length)]);
				var nazwa = skrot;
				if (rnd.Next(100) < 30) nazwa = prefiksy[rnd.Next(prefiksy.Length)] + " " + nazwa;
				if (rnd.Next(100) < 70) nazwa = nazwa + " " + sufiksy[rnd.Next(sufiksy.Length)];
				var adres = "ul. " + ulice[rnd.Next(ulice.Length)] + " " + rnd.Next(1, 100);
				if (rnd.Next(100) < 20) adres += " / " + rnd.Next(1, 200);
				adres += $"\r\n{rnd.Next(100):00}-{rnd.Next(1000):000} {miasta[rnd.Next(miasta.Length)]}";
				var nip = $"{rnd.Next(1000):000}-{rnd.Next(1000):000}-{rnd.Next(100):00}-{rnd.Next(100):00}";
				var tel = $"{rnd.Next(1000):000}-{rnd.Next(1000):000}-{rnd.Next(1000):000}";
				if (rnd.Next(100) < 50) tel = "+48" + tel;
				if (rnd.Next(100) < 10) tel = tel.Replace(" ", "");
				if (rnd.Next(100) < 30) tel = tel.Replace("-", "");
				var rach = $"{rnd.Next(100):00} {rnd.Next(1000):0000} {rnd.Next(1000):0000} {rnd.Next(1000):0000} {rnd.Next(1000):0000} {rnd.Next(1000):0000} {rnd.Next(1000):0000}";
				var tp = rnd.Next(100) < 5;
				var arch = rnd.Next(100) < 15;
				var kontrahent = new Kontrahent
				{
					PelnaNazwa = nazwa,
					Nazwa = skrot,
					NIP = nip,
					AdresKorespondencyjny = adres,
					AdresRejestrowy = adres,
					CzyTP = tp,
					CzyArchiwalny = arch,
					Telefon = tel,
					RachunekBankowy = rach
				};
				baza.Zapisz(kontrahent);
			}
		}

		private static void ZaladujTowary(Baza baza)
		{
			var towary = new HashSet<string> { "Komplet pościeli 200x220", "Waga łazienkowa 180kg", "Oczyszczacz powietrza", "Pochłaniacz wilgoci 365 dni", "Kosz na śmieci 135l", "Deska do krojenia 50x30", "Lustro okrągłe podświetlane 50cm", "Mały czajnik z gwizdkiem", "Zestaw sitek", "Solniczka i młynek do pieprzu", "Kosz na owoce 23cm", "Chlebak 10l", "Komplet sztućców", "Zegar ścienny 3D", "Radio z budzikiem", "Mop parowy 1300W", "Myjka do okien", "Zestaw śrubokrętów 18 el.", "Klucz nasadkowy uniwersalny", "Wkrętarko-wiertarka 18V", "Migomat 300A", "Poziomnica laserowa", "Suwmiarka elektroniczna", "Lutownica gazowa" };
			var uslugi = new HashSet<string> { "Dostawa UPS", "Kurier DPD", "Dostawa Paczkomat", "Usługa instalacji", "Przedłużona gwarancja" };

			var jednostka = baza.JednostkiMiar.Single(jednostka => jednostka.CzyDomyslna);
			var vat = baza.StawkiVat.Single(stawka => stawka.CzyDomyslna);
			var max = rnd.Next(towary.Count / 2, towary.Count);
			for (int i = 0; i < max; i++)
			{
				var nazwa = towary.Skip(rnd.Next(towary.Count)).First();
				towary.Remove(nazwa);
				var towar = new Towar
				{
					Nazwa = nazwa,
					CzyArchiwalny = rnd.Next(100) < 5,
					CzyWedlugCenBrutto = rnd.Next(100) < 30,
					JednostkaMiaryRef = jednostka,
					StawkaVatRef = vat,
					Rodzaj = RodzajTowaru.Towar
				};

				if (towar.CzyWedlugCenBrutto)
				{
					towar.CenaBrutto = rnd.Next(50, 200);
					towar.CenaNetto = towar.CenaBrutto / (100 + vat.Wartosc) * 100;
				}
				else
				{
					towar.CenaNetto = rnd.Next(50, 200);
					towar.CenaBrutto = towar.CenaNetto * (100 + vat.Wartosc) / 100;
				}

				baza.Zapisz(towar);
			}

			foreach (var usluga in uslugi)
			{
				var towar = new Towar
				{
					Nazwa = usluga,
					CzyArchiwalny = rnd.Next(100) < 5,
					CzyWedlugCenBrutto = false,
					JednostkaMiaryRef = jednostka,
					StawkaVatRef = vat,
					Rodzaj = RodzajTowaru.Usługa
				};

				towar.CenaNetto = rnd.Next(50, 200);
				towar.CenaBrutto = towar.CenaNetto * (100 + vat.Wartosc) / 100;

				baza.Zapisz(towar);
			}
		}

		private static void ZaladujFaktury(Baza baza)
		{
			var faktury = new List<Faktura>();
			var waluta = baza.Waluty.Single(waluta => waluta.CzyDomyslna);
			var towary = baza.Towary.ToList();
			var platnosci = baza.SposobyPlatnosci.ToList();
			var kontrahenci = baza.Kontrahenci.ToList();
			var firma = kontrahenci.Single(kontrahent => kontrahent.CzyPodmiot);
			var bezVat = baza.StawkiVat.Single(stawka => stawka.Skrot == "ZW");
			kontrahenci = kontrahenci.Where(kontrahent => !kontrahent.CzyPodmiot).ToList();
			var dataStartowa = DateTime.Now.Date.AddYears(-5);
			var dataKoncowa = DateTime.Now;
			var data = dataStartowa;
			var dniRazem = (int)(dataKoncowa - dataStartowa).TotalDays;
			while (data < dataKoncowa)
			{
				data = data.AddDays(1);
				var dniTemu = (int)(dataKoncowa - data).TotalDays;
				var szansaNaFakture = 30;
				szansaNaFakture += dniTemu * 100 / dniRazem;
				if (data.DayOfWeek == DayOfWeek.Saturday) szansaNaFakture -= 10;
				if (data.DayOfWeek == DayOfWeek.Sunday) szansaNaFakture -= 40;
				if (data.Month > 10) szansaNaFakture += 20;
				if (data.Month < 3) szansaNaFakture -= 10;
				if (rnd.Next(100) >= szansaNaFakture) continue;

				var liczbaFaktur = 1 + rnd.Next(1 + szansaNaFakture / 70);

				for (int i = 0; i < liczbaFaktur; i++)
				{
					var platnosc = rnd.Next(100) < 20 ? platnosci[rnd.Next(platnosci.Count)] : platnosci.Single(platnosc => platnosc.CzyDomyslny);
					var kontrahent = kontrahenci[rnd.Next(kontrahenci.Count)];

					var faktura = new Faktura
					{
						DataSprzedazy = data.AddDays(-rnd.Next(3)),
						DataWystawienia = data,
						DataWprowadzenia = data,
						KursWaluty = 1,
						WalutaRef = waluta,
						ProcentKosztow = 100,
						ProcentVatNaliczonego = 100,
						SposobPlatnosciRef = platnosc,
						TerminPlatnosci = data.AddDays(platnosc.LiczbaDni),
						OpisSposobuPlatnosci = platnosc.Nazwa,
						Rodzaj = rnd.Next(100) < 10 ? RodzajFaktury.Zakup : rnd.Next(100) < 10 ? RodzajFaktury.Proforma : rnd.Next(100) < 10 ? RodzajFaktury.VatMarża : RodzajFaktury.Sprzedaż
					};

					if (faktura.CzyZakup)
					{
						var numer = "";
						if (rnd.Next(100) < 50) numer += "FV";
						else if (rnd.Next(100) < 50) numer += "FS";
						if (numer.Length > 0)
						{
							if (rnd.Next(100) < 50) numer += "-";
							else if (rnd.Next(100) < 50) numer += "/";
							else if (rnd.Next(100) < 50) numer += " ";
						}

						if (rnd.Next(100) < 50) faktura.Numer += rnd.Next(1000).ToString("0000");
						else if (rnd.Next(100) < 50) faktura.Numer += rnd.Next(1000).ToString();
						else if (rnd.Next(100) < 50) faktura.Numer += rnd.Next(100000).ToString("000000");
						else faktura.Numer += rnd.Next(100000).ToString();

						if (rnd.Next(100) < 80)
						{
							if (rnd.Next(100) < 50) faktura.Numer += "/" + faktura.DataWystawienia.Month;
							faktura.Numer += "/" + faktura.DataWystawienia.Year;
						}
					}

					if (rnd.Next(100) < 5)
					{
						var korygowana = faktury[rnd.Next(faktury.Count)];

						if (korygowana.Rodzaj == RodzajFaktury.Proforma) continue;
						var korekta = korygowana.PrzygotujKorekte(baza);
						var pozycje = baza.PozycjeFaktur.Where(pozycja => pozycja.FakturaId == korekta.Id).ToList();
						foreach (var pozycja in pozycje)
						{
							if (pozycja.CzyPrzedKorekta) continue;

							if (rnd.Next(100) < 20)
							{
								pozycja.Ilosc += rnd.Next(-5, 5);
								if (pozycja.Ilosc < 0) pozycja.Ilosc = 0;
							}
							if (rnd.Next(100) < 20)
							{
								if (pozycja.CzyWedlugCenBrutto) pozycja.CenaBrutto += rnd.Next(-10, 10);
								else pozycja.CenaNetto += rnd.Next(-10, 10);
								pozycja.PrzeliczCeny(baza);
							}
						}
						korekta.PrzeliczRazem(pozycje);
						if (korekta.Numerator.HasValue) korekta.Numer = Numerator.NadajNumer(baza, korekta.Numerator.Value, korekta.Podstawienie);
						baza.Zapisz(korekta);
						baza.Zapisz(pozycje);

						faktura = korekta;
					}
					else
					{
						if (faktura.Rodzaj == RodzajFaktury.Sprzedaż || faktura.Rodzaj == RodzajFaktury.Proforma || faktura.Rodzaj == RodzajFaktury.VatMarża)
						{
							faktura.SprzedawcaRef = firma;
							faktura.NIPSprzedawcy = firma.NIP;
							faktura.DaneSprzedawcy = firma.AdresRejestrowy;
							faktura.NazwaSprzedawcy = firma.PelnaNazwa;
							faktura.NabywcaRef = kontrahent;
							faktura.NIPNabywcy = kontrahent.NIP;
							faktura.DaneNabywcy = kontrahent.AdresRejestrowy;
							faktura.NazwaNabywcy = kontrahent.PelnaNazwa;
							faktura.RachunekBankowy = platnosc.LiczbaDni == 0 ? "" : firma.RachunekBankowy;
						}
						else
						{
							faktura.SprzedawcaRef = kontrahent;
							faktura.NIPSprzedawcy = kontrahent.NIP;
							faktura.DaneSprzedawcy = kontrahent.AdresRejestrowy;
							faktura.NazwaSprzedawcy = kontrahent.PelnaNazwa;
							faktura.NabywcaRef = firma;
							faktura.NIPNabywcy = firma.NIP;
							faktura.DaneNabywcy = firma.AdresRejestrowy;
							faktura.NazwaNabywcy = firma.PelnaNazwa;
							faktura.RachunekBankowy = platnosc.LiczbaDni == 0 ? "" : kontrahent.RachunekBankowy;
						}

						if (faktura.Rodzaj == RodzajFaktury.Zakup && rnd.Next(100) < 10) faktura.DataWprowadzenia = faktura.DataWprowadzenia.AddDays(rnd.Next(10));
						if (faktura.Numerator.HasValue) faktura.Numer = Numerator.NadajNumer(baza, faktura.Numerator.Value, faktura.Podstawienie);

						faktura.Pozycje = new List<PozycjaFaktury>();
						var lp = rnd.Next(1, 10);
						var fvBezVat = rnd.Next(0, 20) == 0;
						for (int j = 0; j < lp; j++)
						{
							var towar = towary[rnd.Next(towary.Count)];
							var pozycja = new PozycjaFaktury
							{
								TowarRef = towar,
								CenaBrutto = towar.CenaBrutto,
								CenaNetto = towar.CenaNetto,
								StawkaVatRef = fvBezVat ? bezVat : towar.StawkaVatRef,
								CzyWedlugCenBrutto = towar.CzyWedlugCenBrutto,
								LP = j + 1,
								Opis = towar.Nazwa,
								FakturaRef = faktura,
								Ilosc = faktura.CzyZakup ? rnd.Next(10, 100) : rnd.Next(1, 20),
								RabatProcent = faktura.CzyZakup ? 0 : rnd.Next(0, 10) == 0 ? rnd.Next(0, 90) : 0,
								RabatCena = faktura.CzyZakup ? 0 : rnd.Next(0, 20) == 0 ? rnd.Next(0, (int)towar.CenaNetto) : 0,
								RabatWartosc = faktura.CzyZakup ? 0 : rnd.Next(0, 30) == 0 ? rnd.Next(0, (int)towar.CenaNetto) : 0,
							};
							pozycja.PrzeliczCeny(baza);

							faktura.Pozycje.Add(pozycja);
						}

						faktura.PrzeliczRazem(faktura.Pozycje);
					}

					faktury.Add(faktura);

					faktura.Wplaty = new List<Wplata>();
					if (rnd.Next(100) < 95)
					{
						var wplata = new Wplata
						{
							Data = faktura.TerminPlatnosci.AddDays(rnd.Next(-5, 5)),
							Kwota = faktura.PozostaloDoZaplaty
						};
						faktura.Wplaty.Add(wplata);
					}

					baza.Zapisz(faktura);
				}
			}
		}
	}
}
