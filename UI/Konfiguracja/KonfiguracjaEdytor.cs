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

		kontroler.Powiazanie(numericUpDownWysokoscWierszy, konfiguracja => konfiguracja.WysokoscWiersza);
		kontroler.Powiazanie(comboBoxFormatDaty, konfiguracja => konfiguracja.FormatDaty, PrzeliczPrzykladDaty);
		kontroler.Powiazanie(comboBoxFormatCzasu, konfiguracja => konfiguracja.FormatCzasu, PrzeliczPrzykladCzasu);
		kontroler.Powiazanie(comboBoxFormatKwoty, konfiguracja => konfiguracja.FormatKwoty, PrzeliczPrzykladKwoty);

		textBoxRozmiarCzcionki.PlaceholderText = DefaultFont.Size.ToString();
		textBoxNazwaCzcionki.PlaceholderText = DefaultFont.FontFamily.Name;
	}

	private void PrzeliczPrzykladDaty()
	{
		try
		{
			textBoxPrzykladDaty.Text = DateTime.Now.ToString(Rekord.FormatDaty);
		}
		catch (FormatException)
		{
		}
	}

	private void PrzeliczPrzykladCzasu()
	{
		try
		{
			textBoxPrzykladCzasu.Text = DateTime.Now.ToString(Rekord.FormatCzasu);
		}
		catch (FormatException)
		{
		}
	}

	private void PrzeliczPrzykladKwoty()
	{
		try
		{
			textBoxPrzykladKwoty.Text = 1234567.89m.ToString(Rekord.FormatKwoty);
		}
		catch (FormatException)
		{
		}
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

	private void linkLabelFormatDaty_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		MessageBox.Show(@"Lista dostępnych pól:

d - dzień miesiąca, od 1 do 31
dd - dzień miesiąca z wiodącym zerem, od 01 do 31
ddd - skrócony dzień tygodnia, np. pon
dddd - dzień tygodnia, np. poniedziałek

M - miesiąc, od 1 do 12
MM - miesiąc z wiodącym zerem, od 01 do 12
MMM - skrócona nazwa miesiąca, np. Mar
MMMM - nazwa miesiąca, np. Marzec

y - rok, od 1 do 99
yy - rok z wiodącym zerem, od 01 do 99
yyyy - pełny rok, np. 2026", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}

	private void linkLabelFormatCzasu_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		MessageBox.Show(@"Lista dostępnych pól jak dla formatu daty oraz:

h - godzina, od 1 do 12
H - godzina, od 0 do 23
hh - godzina z wiodącym zerem, od 01 do 12
HH - godzina z wiodącym zerem, od 00 do 23
tt - znacznik am/pm

m - minuta, od 0 do 59
mm - minuta z wiodącym zerem, od 00 do 59

s - sekunda, od 0 do 59
ss - sekunda z wiodącym zerem, od 00 do 59", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}

	private void linkLabelFormatKwoty_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		MessageBox.Show(@"Znaczenie symboli:

0 - cyfra dziesiętna, wystąpienie obowiązkowe
# - cyfra dziesiętna, tylko jeśli niezerowa
, - separator tysięcy
. - seperator części dziesiętnej", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}
}

class KonfiguracjaEdytorBase : Edytor<Konfiguracja>
{
}
