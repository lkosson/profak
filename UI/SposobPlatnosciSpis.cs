using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class SposobPlatnosciSpis : Spis<SposobPlatnosci>
	{
		public SposobPlatnosciSpis()
		{
			DodajKolumne(nameof(SposobPlatnosci.Nazwa), "Nazwa", rozciagnij: true);
			DodajKolumne(nameof(SposobPlatnosci.LiczbaDni), "Liczba dni", wyrownajDoPrawej: true);
			DodajKolumne(nameof(SposobPlatnosci.CzyDomyslnyFmt), "Domyślny");
			DodajKolumne(nameof(SposobPlatnosci.Id), "Id", wyrownajDoPrawej: true);
		}

		public override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.SposobyPlatnosci.ToList();
		}
	}
}
