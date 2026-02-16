using ProFak.DB;

namespace ProFak.UI;

class JednostkaMiaryEdytor : EdytorDwieKolumny<JednostkaMiary>
{
	public JednostkaMiaryEdytor()
	{
		DodajTextBox(jednostkaMiary => jednostkaMiary.Skrot, "Skrót", wymagane: true);
		DodajTextBox(jednostkaMiary => jednostkaMiary.Nazwa, "Nazwa", wymagane: true);
		DodajCheckBox(jednostkaMiary => jednostkaMiary.CzyDomyslna, "Domyślna");
		DodajNumericUpDown(jednostkaMiary => jednostkaMiary.LiczbaMiescPoPrzecinku, "Liczba miejsc po przecinku");
		UstawRozmiar();
	}
}
