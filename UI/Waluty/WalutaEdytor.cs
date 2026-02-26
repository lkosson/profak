using ProFak.DB;

namespace ProFak.UI;

class WalutaEdytor : EdytorDwieKolumny<Waluta>
{
	public WalutaEdytor()
	{
		DodajTextBox(waluta => waluta.Skrot, "Skrót", wymagane: true);
		DodajTextBox(waluta => waluta.Nazwa, "Nazwa", wymagane: true);
		DodajCheckBox(waluta => waluta.CzyDomyslna, "Domyślna");
		UstawRozmiar();
	}
}
