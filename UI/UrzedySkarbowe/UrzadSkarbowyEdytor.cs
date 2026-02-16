using ProFak.DB;

namespace ProFak.UI
{
	class UrzadSkarbowyEdytor : EdytorDwieKolumny<UrzadSkarbowy>
	{
		public UrzadSkarbowyEdytor()
		{
			DodajTextBox(urzad => urzad.Kod, "Kod", wymagane: true);
			DodajTextBox(urzad => urzad.Nazwa, "Nazwa", wymagane: true);
			UstawRozmiar();
		}
	}
}
