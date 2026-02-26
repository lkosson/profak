using ProFak.DB;

namespace ProFak.UI;

class StawkaVatEdytor : EdytorDwieKolumny<StawkaVat>
{
	public StawkaVatEdytor()
	{
		DodajTextBox(stawkaVat => stawkaVat.Skrot, "Skrót", wymagane: true);
		DodajNumericUpDown(stawkaVat => stawkaVat.Wartosc, "Wartość");
		DodajCheckBox(stawkaVat => stawkaVat.CzyDomyslna, "Domyślna");
		UstawRozmiar();
	}
}
