using ProFak.DB;

namespace ProFak.UI;

class SposobPlatnosciSpis : Spis<SposobPlatnosci>
{
	public SposobPlatnosciSpis()
	{
		DodajKolumne(nameof(SposobPlatnosci.Nazwa), "Nazwa", rozciagnij: true);
		DodajKolumne(nameof(SposobPlatnosci.LiczbaDni), "Liczba dni", wyrownajDoPrawej: true);
		DodajKolumne(nameof(SposobPlatnosci.CzyDomyslnyFmt), "Domyślny");
		DodajKolumne(nameof(SposobPlatnosci.CzyZaplaconeFmt), "Zapłacone");
		DodajKolumneId();
	}

	protected override void Przeladuj()
	{
		Rekordy = Kontekst.Baza.SposobyPlatnosci.AsEnumerable().OrderBy(sposob => sposob.Nazwa);
	}

	protected override bool CzyWierszPogrubiony(SposobPlatnosci rekord) => rekord.CzyDomyslny;
}
