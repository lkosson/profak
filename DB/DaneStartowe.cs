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
	class DaneStartowe
	{
		public static void Zaladuj(Baza baza)
		{
			if (!baza.JednostkiMiar.Any())
			{
				baza.Zapisz(new JednostkaMiary { CzyDomyslna = true, LiczbaMiescPoPrzecinku = 0, Nazwa = "Sztuka", Skrot = "szt" });
				baza.Zapisz(new JednostkaMiary { CzyDomyslna = false, LiczbaMiescPoPrzecinku = 0, Nazwa = "Komplet", Skrot = "kpl" });
				baza.Zapisz(new JednostkaMiary { CzyDomyslna = false, LiczbaMiescPoPrzecinku = 0, Nazwa = "Godzina", Skrot = "h" });
				baza.Zapisz(new JednostkaMiary { CzyDomyslna = false, LiczbaMiescPoPrzecinku = 3, Nazwa = "Kilogram", Skrot = "kg" });
				baza.Zapisz(new JednostkaMiary { CzyDomyslna = false, LiczbaMiescPoPrzecinku = 3, Nazwa = "Litr", Skrot = "l" });
			}

			if (!baza.Numeratory.Any())
			{
				baza.Zapisz(new Numerator { Przeznaczenie = PrzeznaczenieNumeratora.Faktura, Format = "FV/[Numer]/[Rok]" });
				baza.Zapisz(new Numerator { Przeznaczenie = PrzeznaczenieNumeratora.Korekta, Format = "FK/[Numer]/[Rok]" });
				baza.Zapisz(new Numerator { Przeznaczenie = PrzeznaczenieNumeratora.Proforma, Format = "FP/[Numer]/[Rok]" });
			}

			if (!baza.SposobyPlatnosci.Any())
			{
				baza.Zapisz(new SposobPlatnosci { CzyDomyslny = true, LiczbaDni = 7, Nazwa = "Przelew 7" });
				baza.Zapisz(new SposobPlatnosci { CzyDomyslny = false, LiczbaDni = 14, Nazwa = "Przelew 14" });
				baza.Zapisz(new SposobPlatnosci { CzyDomyslny = false, LiczbaDni = 30, Nazwa = "Przelew 30" });
				baza.Zapisz(new SposobPlatnosci { CzyDomyslny = false, LiczbaDni = 0, Nazwa = "Gotówka" });
				baza.Zapisz(new SposobPlatnosci { CzyDomyslny = false, LiczbaDni = 0, Nazwa = "Karta" });
			}

			if (!baza.StawkiVat.Any())
			{
				baza.Zapisz(new StawkaVat { CzyDomyslna = true, Wartosc = 23, Skrot = "23%" });
				baza.Zapisz(new StawkaVat { CzyDomyslna = false, Wartosc = 8, Skrot = "8%" });
				baza.Zapisz(new StawkaVat { CzyDomyslna = false, Wartosc = 5, Skrot = "5%" });
				baza.Zapisz(new StawkaVat { CzyDomyslna = false, Wartosc = 0, Skrot = "0%" });
				baza.Zapisz(new StawkaVat { CzyDomyslna = false, Wartosc = 0, Skrot = "NP" });
				baza.Zapisz(new StawkaVat { CzyDomyslna = false, Wartosc = 0, Skrot = "ZW" });
			}

			if (!baza.Waluty.Any())
			{
				baza.Zapisz(new Waluta { CzyDomyslna = true, Skrot = "PLN", Nazwa = "Polski złoty" });
			}
		}
	}
}
