using ProFak.DB;

namespace ProFak.UI;

class SposobPlatnosciEdytor : EdytorDwieKolumny<SposobPlatnosci>
{
	public SposobPlatnosciEdytor()
	{
		DodajTextBox(sposobPlatnosci => sposobPlatnosci.Nazwa, "Nazwa", wymagane: true);
		DodajNumericUpDown(sposobPlatnosci => sposobPlatnosci.LiczbaDni, "Liczba dni");
		DodajCheckBox(sposobPlatnosci => sposobPlatnosci.CzyDomyslny, "Domyślny");
		UstawRozmiar();
	}
}
