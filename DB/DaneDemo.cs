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
			var sufiksy = new[] { "i wspólnicy", "i spółka", "SpJ", "Sp. z o.o.", "SA", "Sp. K.", "s.c." };
			var ulice = new[] { "Złota", "Jasna", "Piękna", "Plażowa", "Leśna", "Wilgi", "Śnieżna", "Wolna", "Dobra", "Wysoka", "Stalowa", "Żelazna", "Węglarska", "Handlowa", "Roślinna", "Mleczna", "Senna", "Topazowa", "Targowa", "Rzeczna", "Wilcza", "Kadrowa", "Garnizonowa", "Polna", "Zabawna", "Poprawna", "Krzywa", "Prosta", "Wspólna", "Lipowa", "Brzozowa", "Klonowa" };
			var miasta = new[] { "Warszawa", "Kraków", "Łódź", "Wrocław", "Poznań", "Gdańsk", "Szczecin", "Bydgoszcz", "Lublin", "Katowice", "Białystok", "Gdynia", "Częstochowa", "Radom", "Sosnowiec", "Toruń", "Kielce", "Rzeszów", "Gliwice", "Zabrze", "Olsztyn", "Bielsko-Biała", "Bytom", "Ruda Śląska", "Rybnik", "Zielona Góra", "Tychy", "Gorzów Wielkopolski", "Dąbrowa Górnicza", "Elbląg", "Płock", "Opole", "Wałbrzych", "Włocławek", "Tarnów", "Chorzów", "Koszalin", "Kalisz", "Legnica", "Grudziądz" };

			for (int i = 0; i < rnd.Next(glowne.Length / 2, glowne.Length); i++)
			{
				var skrot = glowne[rnd.Next(glowne.Length)];
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
				var tp = rnd.Next(100) > 95;
				var arch = rnd.Next(100) > 85;
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
	}
}
