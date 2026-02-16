using ProFak.DB;

namespace ProFak.UI;

class StanNumeratoraEdytor : EdytorDwieKolumny<StanNumeratora>
{
	public StanNumeratoraEdytor()
	{
		DodajTextBox(stanNumeratora => stanNumeratora.Parametry, "Parametry");
		DodajNumericUpDown(stanNumeratora => stanNumeratora.OstatniaWartosc, "Ostatnia wartość");
		UstawRozmiar();
	}
}
