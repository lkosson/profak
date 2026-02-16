using ProFak.DB;

namespace ProFak.UI;

partial class KonfiguracjaEdytor : KonfiguracjaEdytorBase
{
	public bool PrzywrocUstawieniaSpisow => checkBoxPrzywrocUstawieniaSpisow.Checked;
	public bool PrzywrocUstawieniaMenu => checkBoxPrzywrocUstawieniaMenu.Checked;

	public KonfiguracjaEdytor()
	{
		InitializeComponent();

		kontroler.Powiazanie(textBoxSMTPSerwer, konfiguracja => konfiguracja.SMTPSerwer);
		kontroler.Powiazanie(numericUpDownSMTPort, konfiguracja => konfiguracja.SMTPPort);
		kontroler.Powiazanie(textBoxSMTPLogin, konfiguracja => konfiguracja.SMTPLogin);
		kontroler.Powiazanie(textBoxSMTPHaslo, konfiguracja => konfiguracja.SMTPHaslo);
		kontroler.Powiazanie(textBoxEMailNadawca, konfiguracja => konfiguracja.EMailNadawca);
		kontroler.Powiazanie(textBoxEMailTemat, konfiguracja => konfiguracja.EMailTemat);
		kontroler.Powiazanie(textBoxEMailTresc, konfiguracja => konfiguracja.EMailTresc);

		kontroler.Powiazanie(checkBoxSkrotyKlawiaturoweAkcji, konfiguracja => konfiguracja.SkrotyKlawiaturoweAkcji);
		kontroler.Powiazanie(checkBoxSkrotyKlawiaturoweZakladek, konfiguracja => konfiguracja.SkrotyKlawiaturoweZakladek);
		kontroler.Powiazanie(checkBoxSkrotyKlawiaturowePrzyciskow, konfiguracja => konfiguracja.SkrotyKlawiaturowePrzyciskow);
		kontroler.Powiazanie(checkBoxIkonyAkcji, konfiguracja => konfiguracja.IkonyAkcji);
		kontroler.Powiazanie(checkBoxDomyslnyPodgladStrony, konfiguracja => konfiguracja.DomyslnyPodgladStrony);
		kontroler.Powiazanie(checkBoxPotwierdzanieZamknieciaEdytora, konfiguracja => konfiguracja.PotwierdzanieZamknieciaEdytora);
		kontroler.Powiazanie(checkBoxPotwierdzanieZamknieciaProgramu, konfiguracja => konfiguracja.PotwierdzanieZamknieciaProgramu);
		kontroler.Powiazanie(checkBoxWstepneLadowanieReportingServices, konfiguracja => konfiguracja.WstepneLadowanieReportingServices);
		kontroler.Powiazanie(numericUpDownSzerokoscMenu, konfiguracja => konfiguracja.SzerokoscMenu);
		kontroler.Powiazanie(textBoxRozmiarCzcionki, konfiguracja => konfiguracja.RozmiarCzcionki == 0 ? "" : konfiguracja.RozmiarCzcionki.ToString(), (konfiguracja, wartosc) => konfiguracja.RozmiarCzcionki = Int32.TryParse(wartosc, out var rozmiar) ? rozmiar : 0);
		kontroler.Powiazanie(textBoxNazwaCzcionki, konfiguracja => konfiguracja.NazwaCzcionki);

		textBoxRozmiarCzcionki.PlaceholderText = DefaultFont.Size.ToString();
		textBoxNazwaCzcionki.PlaceholderText = DefaultFont.FontFamily.Name;
	}

	public static void Wyswietl()
	{
		using var nowyKontekst = new Kontekst();
		using var transakcja = nowyKontekst.Transakcja();
		var rekord = nowyKontekst.Baza.Konfiguracja.FirstOrDefault();
		if (rekord == null) rekord = Konfiguracja.Domyslna;
		rekord.AktualizujWersje();
		nowyKontekst.Dodaj(rekord);
		using var edytor = new KonfiguracjaEdytor();
		using var okno = new Dialog("Edycja konfiguracji", edytor, nowyKontekst);
		edytor.Przygotuj(nowyKontekst, rekord);
		if (okno.ShowDialog() != DialogResult.OK) return;
		nowyKontekst.Baza.Zapisz(rekord);
		if (edytor.PrzywrocUstawieniaSpisow) nowyKontekst.Baza.Usun<KolumnaSpisu>();
		if (edytor.PrzywrocUstawieniaMenu) nowyKontekst.Baza.Usun<StanMenu>();
		transakcja.Zatwierdz();
		Wyglad.WczytajZBazy();
	}

	private void buttonWybierzCzcionke_Click(object sender, EventArgs e)
	{
		using var dialog = new FontDialog();
		using var font = new Font(String.IsNullOrEmpty(Rekord.NazwaCzcionki) ? DefaultFont.FontFamily.Name : Rekord.NazwaCzcionki, Rekord.RozmiarCzcionki == 0 ? DefaultFont.Size : Rekord.RozmiarCzcionki);
		dialog.FontMustExist = true;
		dialog.Font = font;
		if (dialog.ShowDialog() != DialogResult.OK) return;
		Rekord.NazwaCzcionki = dialog.Font.FontFamily.Name;
		Rekord.RozmiarCzcionki = (int)Single.Round(dialog.Font.Size);
		kontroler.AktualizujKontrolki();
	}

	private void linkLabelTrescPomoc_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
	{
		MessageBox.Show(@"Lista dostępnych pól:

[NUMER] - pełny numer faktury
[DATA-SPRZEDAZY] - data sprzedaży w formacie rrrr-mm-dd
[DATA-WYSTAWIENIA] - data wystawienia w formacie rrrr-mm-dd
[TERMIN-PLATNOSCI] - termin płatności w formacie rrrr-mm-dd
[SPRZEDAWCA-NAZWA] - pełna nazwa sprzedawcy
[SPRZEDAWCA-ADRES] - adres korespondencyjny sprzedawcy
[SPRZEDAWCA-NIP] - NIP sprzedawcy
[SPRZEDAWCA-EMAIL] - adres e-mail sprzedawcy
[NABYWCA-NAZWA] - pełna nazwa nabywcy
[NABYWCA-ADRES] - adres korespondencyjny nabywcy
[NABYWCA-NIP] - NIP nabywcy
[NABYWCA-EMAIL] - adres e-mail nabywcy
[KWOTA-NETTO] - łączna kwota netto z faktury
[KWOTA-BRUTTO] - łączna kwota brutto z faktury
[KWOTA-VAT] - łączna kwota VAT z faktury
[WALUTA] - kod waluty faktury
[UWAGI] - uwagi do faktury (publiczne)
[RACHUNEK] - numer rachunku wskazany na fakturze", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}
}

class KonfiguracjaEdytorBase : Edytor<Konfiguracja>
{
}
