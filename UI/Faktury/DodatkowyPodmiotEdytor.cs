using ProFak.DB;

namespace ProFak.UI
{
	class DodatkowyPodmiotEdytor : EdytorDwieKolumny<DodatkowyPodmiot>
	{
		public DodatkowyPodmiotEdytor()
		{
			DodajComboBox(dodatkowyPodmiot => dodatkowyPodmiot.Rodzaj, "Rodzaj");
			DodajTextBox(dodatkowyPodmiot => dodatkowyPodmiot.Nazwa, "Nazwa");
			DodajTextBox(dodatkowyPodmiot => dodatkowyPodmiot.NIP, "NIP");
			DodajTextBox(dodatkowyPodmiot => dodatkowyPodmiot.IDwew, "IDwew");
			DodajTextBox(dodatkowyPodmiot => dodatkowyPodmiot.VatUE, "VatUE");
			DodajTextBox(dodatkowyPodmiot => dodatkowyPodmiot.Adres, "Adres", linie: 2);
			DodajTextBox(dodatkowyPodmiot => dodatkowyPodmiot.EMail, "E-Mail");
			DodajTextBox(dodatkowyPodmiot => dodatkowyPodmiot.Telefon, "Telefon");
			//DodajNumericUpDown(dodatkowyPodmiot => dodatkowyPodmiot.Udzial, "Udział");
			UstawRozmiar();
		}
	}
}
