using ProFak.DB;

namespace ProFak.UI;

partial class KonfiguracjaEdytor : Edytor<Konfiguracja>
{
	public bool PrzywrocUstawieniaSpisow => checkBoxPrzywrocUstawieniaSpisow.Checked;
	public bool PrzywrocUstawieniaMenu => checkBoxPrzywrocUstawieniaMenu.Checked;

	private readonly CheckBox checkBoxPrzywrocUstawieniaSpisow;
	private readonly CheckBox checkBoxPrzywrocUstawieniaMenu;
	private readonly TextBox textBoxPrzykladDaty;
	private readonly TextBox textBoxPrzykladCzasu;
	private readonly TextBox textBoxPrzykladKwoty;

	public KonfiguracjaEdytor()
	{
		var textBoxSMTPSerwer = Kontrolki.TextBox();
		var numericUpDownSMTPort = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var textBoxSMTPLogin = Kontrolki.TextBox();
		var textBoxSMTPHaslo = Kontrolki.TextBox(haslo: true);
		var textBoxEMailNadawca = Kontrolki.TextBox();
		var textBoxEMailTemat = Kontrolki.TextBox();
		var textBoxEMailTresc = Kontrolki.TextArea(linie: 10);

		var checkBoxSkrotyKlawiaturoweAkcji = Kontrolki.CheckBox("Pokaż skróty klawiaturowe akcji na spisie");
		var checkBoxSkrotyKlawiaturoweZakladek = Kontrolki.CheckBox("Pokaż skróty klawiaturowe do przełączania zakładek");
		var checkBoxSkrotyKlawiaturowePrzyciskow = Kontrolki.CheckBox("Pokaż skróty klawiaturowe przycisków");
		var checkBoxIkonyAkcji = Kontrolki.CheckBox("Pokaż piktogramy akcji na spisie");
		var checkBoxDomyslnyPodgladStrony = Kontrolki.CheckBox("Wyświetlaj domyślnie widok strony jako podgląd wydruku");
		var checkBoxPotwierdzanieZamknieciaEdytora = Kontrolki.CheckBox("Pytaj o potwierdzenie porzucenia zmian");
		var checkBoxPotwierdzanieZamknieciaProgramu = Kontrolki.CheckBox("Pytaj o potwierdzenie zamknięcia programu");
		var checkBoxWstepneLadowanieReportingServices = Kontrolki.CheckBox("Załaduj w tle moduł wydruków przy starcie programu");
		checkBoxPrzywrocUstawieniaSpisow = Kontrolki.CheckBox("Przywróć domyślne ustawienia spisów");
		checkBoxPrzywrocUstawieniaMenu = Kontrolki.CheckBox("Przywróć domyślne ustawienia menu");
		var numericUpDownSzerokoscMenu = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var textBoxRozmiarCzcionki = Kontrolki.TextBox();
		var textBoxNazwaCzcionki = Kontrolki.TextBox();

		var numericUpDownWysokoscWierszy = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var comboBoxFormatDaty = Kontrolki.SuggestBox(["yyyy-MM-dd", "dd.MM.yyyy", "d MMMM yyyy"]);
		var comboBoxFormatCzasu = Kontrolki.SuggestBox(["yyyy-MM-dd HH:mm:ss", "dd.MM.yyyy HH:mm:ss", "d MMMM yyyy H:mm:ss"]);
		var comboBoxFormatKwoty = Kontrolki.SuggestBox(["#,##0.00", "0.00", "0.##"]);

		textBoxPrzykladDaty = Kontrolki.TextBox();
		textBoxPrzykladCzasu = Kontrolki.TextBox();
		textBoxPrzykladKwoty = Kontrolki.TextBox();

		textBoxPrzykladDaty.ReadOnly = true;
		textBoxPrzykladCzasu.ReadOnly = true;
		textBoxPrzykladKwoty.ReadOnly = true;

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

		var siatkaEMail = new Siatka([0, -1, 0], []);
		siatkaEMail.DodajWiersz("Serwer SMTP", [(textBoxSMTPSerwer, 2)]);
		siatkaEMail.DodajWiersz("Port", [(numericUpDownSMTPort, 2)]);
		siatkaEMail.DodajWiersz("Login", [(textBoxSMTPLogin, 2)]);
		siatkaEMail.DodajWiersz("Hasło", [(textBoxSMTPHaslo, 2)]);
		siatkaEMail.DodajWiersz("Adres nadawcy", [(textBoxEMailNadawca, 2)]);
		siatkaEMail.DodajWiersz("Temat wiadomości", [(textBoxEMailTemat, 2)]);
		siatkaEMail.DodajWiersz("Treść wiadomości", [textBoxEMailTresc, Kontrolki.LinkPomoc(PomocTrescWiadomosci)]);

		var siatkaWyglad = new Siatka([0, 0, 0, 50, 0, 0, -1], []);
		siatkaWyglad.DodajWiersz([(checkBoxSkrotyKlawiaturoweAkcji, 7)]);
		siatkaWyglad.DodajWiersz([(checkBoxSkrotyKlawiaturoweZakladek, 7)]);
		siatkaWyglad.DodajWiersz([(checkBoxSkrotyKlawiaturowePrzyciskow, 7)]);
		siatkaWyglad.DodajWiersz([(checkBoxIkonyAkcji, 7)]);
		siatkaWyglad.DodajWiersz([(checkBoxDomyslnyPodgladStrony, 7)]);
		siatkaWyglad.DodajWiersz([(checkBoxPotwierdzanieZamknieciaEdytora, 7)]);
		siatkaWyglad.DodajWiersz([(checkBoxPotwierdzanieZamknieciaProgramu, 7)]);
		siatkaWyglad.DodajWiersz([(checkBoxWstepneLadowanieReportingServices, 7)]);
		siatkaWyglad.DodajWiersz([(checkBoxPrzywrocUstawieniaSpisow, 7)]);
		siatkaWyglad.DodajWiersz([(checkBoxPrzywrocUstawieniaMenu, 7)]);
		siatkaWyglad.DodajWiersz("Szerokość menu głównego", [numericUpDownSzerokoscMenu]);
		siatkaWyglad.DodajWiersz("Czcionka", [textBoxNazwaCzcionki, null, textBoxRozmiarCzcionki, Kontrolki.ButtonSlownik(WybierzCzcionke)]);
		siatkaWyglad.DodajWiersz("Wysokość wierszy spisu", [numericUpDownWysokoscWierszy]);

		siatkaWyglad.DodajWiersz("Format daty", [(comboBoxFormatDaty, 1), (Kontrolki.LinkPomoc(PomocFormatDaty), 1), (textBoxPrzykladDaty, 3)]);
		siatkaWyglad.DodajWiersz("Format czasu", [(comboBoxFormatCzasu, 1), (Kontrolki.LinkPomoc(PomocFormatCzasu), 1), (textBoxPrzykladCzasu, 3)]);
		siatkaWyglad.DodajWiersz("Format kwot", [(comboBoxFormatKwoty, 1), (Kontrolki.LinkPomoc(PomocFormatKwoty), 1), (textBoxPrzykladKwoty, 3)]);
		siatkaWyglad.DodajWiersz([(Kontrolki.Text("Zmiana czcionki i szerokości menu będzie obowiązywać od kolejnego uruchomienia programu"), 7)]);

		var zakladki = new Zakladki();
		zakladki.Dodaj("Konfiguracja e-mail", siatkaEMail);
		zakladki.Dodaj("Wygląd", siatkaWyglad);

		UstawZawartosc(zakladki, new Size(800, 500));
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
		edytor.Przygotuj(nowyKontekst, rekord);
		if (!DialogEdycji.Pokaz("Edycja konfiguracji", edytor, nowyKontekst)) return;
		nowyKontekst.Baza.Zapisz(rekord);
		if (edytor.PrzywrocUstawieniaSpisow) nowyKontekst.Baza.Usun<KolumnaSpisu>();
		if (edytor.PrzywrocUstawieniaMenu) nowyKontekst.Baza.Usun<StanMenu>();
		transakcja.Zatwierdz();
		Wyglad.WczytajZBazy();
	}

	private void WybierzCzcionke()
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

	private const string PomocTrescWiadomosci = @"Lista dostępnych pól:

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
[RACHUNEK] - numer rachunku wskazany na fakturze";

	private const string PomocFormatDaty = @"Lista dostępnych pól:

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
yyyy - pełny rok, np. 2026";

	private const string PomocFormatCzasu = @"Lista dostępnych pól jak dla formatu daty oraz:

h - godzina, od 1 do 12
H - godzina, od 0 do 23
hh - godzina z wiodącym zerem, od 01 do 12
HH - godzina z wiodącym zerem, od 00 do 23
tt - znacznik am/pm

m - minuta, od 0 do 59
mm - minuta z wiodącym zerem, od 00 do 59

s - sekunda, od 0 do 59
ss - sekunda z wiodącym zerem, od 00 do 59";

	private const string PomocFormatKwoty = @"Znaczenie symboli:

0 - cyfra dziesiętna, wystąpienie obowiązkowe
# - cyfra dziesiętna, tylko jeśli niezerowa
, - separator tysięcy
. - seperator części dziesiętnej";
}
