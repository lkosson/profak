using ProFak.DB;

namespace ProFak.UI
{
	partial class KonfiguracjaEdytor : KonfiguracjaEdytorBase
	{
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
		}

		public static void Wyswietl()
		{
			using var nowyKontekst = new Kontekst();
			using var transakcja = nowyKontekst.Transakcja();
			var rekord = nowyKontekst.Baza.Konfiguracja.FirstOrDefault();
			if (rekord == null) rekord = Konfiguracja.Domyslna;
			nowyKontekst.Dodaj(rekord);
			using var edytor = new KonfiguracjaEdytor();
			using var okno = new Dialog("Edycja konfiguracji", edytor, nowyKontekst);
			edytor.Przygotuj(nowyKontekst, rekord);
			if (okno.ShowDialog() != DialogResult.OK) return;
			nowyKontekst.Baza.Zapisz(rekord);
			transakcja.Zatwierdz();
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
}
