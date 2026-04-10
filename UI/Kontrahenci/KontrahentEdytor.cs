using ProFak.DB;
using System.Data;
using System.Text.RegularExpressions;

namespace ProFak.UI;

partial class KontrahentEdytor : Edytor<Kontrahent>
{
	private readonly SpisZAkcjami<Faktura, FakturaSprzedazySpis> fakturySprzedazy;
	private readonly SpisZAkcjami<Faktura, FakturaZakupuSpis> fakturyZakupu;

	private readonly TextBox textBoxNazwa;
	private readonly TextBox textBoxPelnaNazwa;
	private readonly TextBox textBoxNIP;
	private readonly TextBox textBoxAdresRejestrowy;
	private readonly TextBox textBoxAdresKorespondencyjny;
	private readonly TextBox textBoxTelefon;
	private readonly TextBox textBoxEMail;
	private readonly TextBox textBoxRachunekBankowy;
	private readonly TextBox textBoxUwagiWewnetrzne;
	private readonly ComboBox comboBoxStan;
	private readonly CheckBox checkBoxTP;
	private readonly ComboBox comboBoxKodUrzedu;
	private readonly TextBox textBoxOsobaFizycznaImie;
	private readonly TextBox textBoxOsobaFizycznaNazwisko;
	private readonly Button buttonUrzadSkarbowy;
	private readonly ComboBox comboBoxFormaOpodatkowania;
	private readonly Button buttonSprawdzMF;
	private readonly Button buttonPobierzGUS;
	private readonly TextBox textBoxTokenKSeF;
	private readonly ComboBox comboBoxSrodowiskoKSeF;
	private readonly Button buttonKSeFAuth;
	private readonly DateTimePicker dateTimePickerOsobaFizycznaDataUrodzenia;
	private readonly Label labelSposobPlatnosci;
	private readonly ComboBox comboBoxSposobPlatnosci;
	private readonly Button buttonSposobPlatnosci;
	private readonly TextBox textBoxUwagiPubliczne;
	private readonly Button buttonCertyfikatKSeF;
	private readonly TextBox textBoxNazwaBanku;
	private readonly Button buttonWaluta;
	private readonly ComboBox comboBoxWaluta;
	private readonly CheckBox checkBoxImportKSeF;
	private readonly TabPage tabPageFakturySprzedazy;
	private readonly TabPage tabPageFakturyZakupu;
	private readonly TabPage tabPagePodatki;
	private readonly Zakladki zakladki;

	public KontrahentEdytor()
	{
		textBoxNazwa = Kontrolki.TextBox(zmienionaWartosc: ZmienionaNazwa);
		textBoxPelnaNazwa = Kontrolki.TextBox();
		textBoxNIP = Kontrolki.TextBox();
		buttonPobierzGUS = Kontrolki.Button("Pobierz dane z GUS", akcja: PobierzGUS);
		textBoxAdresRejestrowy = Kontrolki.TextArea(linie: 3, zmienionaWartosc: ZmienionyAdresRejestrowy);
		textBoxAdresKorespondencyjny = Kontrolki.TextArea(linie: 3);
		textBoxTelefon = Kontrolki.TextBox();
		textBoxEMail = Kontrolki.TextBox();
		textBoxRachunekBankowy = Kontrolki.TextBox();
		textBoxNazwaBanku = Kontrolki.TextBox(podpowiedz: "Nazwa banku");
		buttonSprawdzMF = Kontrolki.Button("Sprawdź na białej liście VAT", akcja: SprawdzMF);
		comboBoxStan = Kontrolki.DropDownList();
		labelSposobPlatnosci = Kontrolki.Label("Sposób płatności");
		comboBoxSposobPlatnosci = Kontrolki.DropDownList();
		buttonSposobPlatnosci = Kontrolki.ButtonSlownik();
		checkBoxTP = Kontrolki.CheckBox("Podmiot powiązany");
		checkBoxImportKSeF = Kontrolki.CheckBox("Pobrany z KSeF (ukryty przy ręcznym wprowadzaniu faktury)");
		comboBoxWaluta = Kontrolki.SuggestBox();
		buttonWaluta = Kontrolki.ButtonSlownik();

		textBoxUwagiWewnetrzne = Kontrolki.TextArea();
		textBoxUwagiPubliczne = Kontrolki.TextArea();

		comboBoxKodUrzedu = Kontrolki.SuggestBox();
		textBoxOsobaFizycznaImie = Kontrolki.TextBox();
		textBoxOsobaFizycznaNazwisko = Kontrolki.TextBox();
		buttonUrzadSkarbowy = Kontrolki.ButtonSlownik();
		comboBoxFormaOpodatkowania = Kontrolki.DropDownList();
		textBoxTokenKSeF = Kontrolki.TextBox();
		comboBoxSrodowiskoKSeF = Kontrolki.DropDownList();
		buttonKSeFAuth = Kontrolki.Button("Uzyskaj dostęp", akcja: KSeFAuth);
		dateTimePickerOsobaFizycznaDataUrodzenia = Kontrolki.DatePicker();
		buttonCertyfikatKSeF = Kontrolki.Button("Importuj certyfikat", akcja: CertyfikatKSeF);

		fakturySprzedazy = new SpisZAkcjami<Faktura, FakturaSprzedazySpis>(new FakturaSprzedazyBezNabywcySpis(), new AkcjaNaSpisie<Faktura>[] { new EdytujRekordAkcja<Faktura, FakturaEdytor>(), new WydrukFakturyAkcja(), new PrzeladujAkcja<Faktura>() });
		fakturyZakupu = new SpisZAkcjami<Faktura, FakturaZakupuSpis>(new FakturaZakupuBezSprzedawcySpis(), new AkcjaNaSpisie<Faktura>[] { new EdytujRekordAkcja<Faktura, FakturaEdytor>(), new PrzeladujAkcja<Faktura>() });

		kontroler.Slownik(comboBoxStan, "archiwalny", "aktywny");
		kontroler.Slownik<FormaOpodatkowania>(comboBoxFormaOpodatkowania, dopuscPuste: true);
		kontroler.Slownik<SrodowiskoKSeF>(comboBoxSrodowiskoKSeF);

		kontroler.Powiazanie(textBoxNazwa, kontrahent => kontrahent.Nazwa);
		kontroler.Powiazanie(textBoxPelnaNazwa, kontrahent => kontrahent.PelnaNazwa);
		kontroler.Powiazanie(textBoxNIP, kontrahent => kontrahent.NIP);
		kontroler.Powiazanie(textBoxAdresRejestrowy, kontrahent => kontrahent.AdresRejestrowy);
		kontroler.Powiazanie(textBoxAdresKorespondencyjny, kontrahent => kontrahent.AdresKorespondencyjny);
		kontroler.Powiazanie(textBoxTelefon, kontrahent => kontrahent.Telefon);
		kontroler.Powiazanie(textBoxEMail, kontrahent => kontrahent.EMail);
		kontroler.Powiazanie(textBoxRachunekBankowy, kontrahent => kontrahent.RachunekBankowy);
		kontroler.Powiazanie(textBoxNazwaBanku, kontrahent => kontrahent.NazwaBanku);
		kontroler.Powiazanie(textBoxUwagiPubliczne, kontrahent => kontrahent.UwagiPubliczne);
		kontroler.Powiazanie(textBoxUwagiWewnetrzne, kontrahent => kontrahent.UwagiWewnetrzne);
		kontroler.Powiazanie(comboBoxStan, kontrahent => kontrahent.CzyArchiwalny);
		kontroler.Powiazanie(checkBoxImportKSeF, kontrahent => kontrahent.CzyImportKSeF);
		kontroler.Powiazanie(checkBoxTP, kontrahent => kontrahent.CzyTP);
		kontroler.Powiazanie(comboBoxSposobPlatnosci, kontrahent => kontrahent.SposobPlatnosciRef);
		kontroler.Powiazanie(comboBoxWaluta, kontrahent => kontrahent.DomyslnaWalutaRef);

		kontroler.Powiazanie(comboBoxKodUrzedu, kontrahent => kontrahent.KodUrzedu);
		kontroler.Powiazanie(textBoxOsobaFizycznaImie, kontrahent => kontrahent.OsobaFizycznaImie);
		kontroler.Powiazanie(textBoxOsobaFizycznaNazwisko, kontrahent => kontrahent.OsobaFizycznaNazwisko);
		kontroler.Powiazanie(dateTimePickerOsobaFizycznaDataUrodzenia, kontrahent => kontrahent.OsobaFizycznaDataUrodzenia);
		kontroler.Powiazanie(comboBoxFormaOpodatkowania, kontrahent => kontrahent.FormaOpodatkowania);
		kontroler.Powiazanie(textBoxTokenKSeF, kontrahent => kontrahent.TokenKSeF);
		kontroler.Powiazanie(comboBoxSrodowiskoKSeF, kontrahent => kontrahent.SrodowiskoKSeF);

		Wymagane(textBoxNazwa);
		Walidacja(textBoxNIP, WalidacjaNIP, true);
		Walidacja(textBoxNazwa, WalidacjaNazwy, true);
		Walidacja(textBoxPelnaNazwa, WalidacjaPelnejNazwy, true);

		var danePodstawowe = new Siatka([0, -1, 0, 0], []);
		danePodstawowe.DodajWiersz("Pełna nazwa", [(textBoxPelnaNazwa, 3)]);
		danePodstawowe.DodajWiersz("NIP", [(textBoxNIP, 2), (buttonPobierzGUS, 1)]);
		danePodstawowe.DodajWiersz("Adres rejestrowy", [(textBoxAdresRejestrowy, 3)]);
		danePodstawowe.DodajWiersz("Adres korespondencyjny", [(textBoxAdresKorespondencyjny, 3)]);
		danePodstawowe.DodajWiersz("Telefon", [(textBoxTelefon, 3)]);
		danePodstawowe.DodajWiersz("E-Mail", [(textBoxEMail, 3)]);
		danePodstawowe.DodajWiersz("Rachunek bankowy", [textBoxRachunekBankowy, textBoxNazwaBanku, buttonSprawdzMF]);
		danePodstawowe.DodajWiersz("Stan", [(comboBoxStan, 3)]);
		danePodstawowe.DodajWiersz([(labelSposobPlatnosci, 1), (comboBoxSposobPlatnosci, 2), (new Poziomo([buttonSposobPlatnosci]), 1)]);
		danePodstawowe.DodajWiersz([(null, 1), (checkBoxTP, 3)]);
		danePodstawowe.DodajWiersz([(null, 1), (checkBoxImportKSeF, 3)]);
		danePodstawowe.DodajWiersz("Domyślna waluta", [comboBoxWaluta, new Poziomo([buttonWaluta])]);

		var uwagi = new Siatka([-1], [-1, -1]);
		uwagi.DodajWiersz([
			new Grupa("Uwagi (wewnętrzne)", textBoxUwagiWewnetrzne),
			new Grupa("Uwagi (drukowane na fakturze)", textBoxUwagiPubliczne)
			]);

		comboBoxSrodowiskoKSeF.Width = 80;
		dateTimePickerOsobaFizycznaDataUrodzenia.ShowCheckBox = true;
		var daneUrzedowe = new Siatka([0, 0, -1, 0, 0], []);
		daneUrzedowe.DodajWiersz("Kod urzędu skarbowego", [(comboBoxKodUrzedu, 3), (buttonUrzadSkarbowy, 1)]);
		daneUrzedowe.DodajWiersz("Pierwsze imię", [(textBoxOsobaFizycznaImie, 4)]);
		daneUrzedowe.DodajWiersz("Nazwisko", [(textBoxOsobaFizycznaNazwisko, 4)]);
		daneUrzedowe.DodajWiersz("Data urodzenia", [(dateTimePickerOsobaFizycznaDataUrodzenia, 4)]);
		daneUrzedowe.DodajWiersz("Forma opodatkowania", [(comboBoxFormaOpodatkowania, 4)]);
		daneUrzedowe.DodajWiersz("Token KSeF", [(comboBoxSrodowiskoKSeF, 1), (textBoxTokenKSeF, 1), (new Poziomo([buttonKSeFAuth, buttonCertyfikatKSeF]), 2)]);

		zakladki = new Zakladki();
		zakladki.Dodaj("Dane podstawowe", danePodstawowe);
		zakladki.Dodaj("Uwagi", uwagi);
		tabPageFakturySprzedazy = zakladki.Dodaj("Sprzedaż do", fakturySprzedazy);
		tabPageFakturyZakupu = zakladki.Dodaj("Zakup od", fakturyZakupu);
		tabPagePodatki = zakladki.Dodaj("Dane urzędowe", daneUrzedowe);

		var uklad = new Siatka([0, -1], [0, -1]);
		uklad.DodajWiersz("Nazwa", [textBoxNazwa]);
		uklad.DodajWiersz([(zakladki, 2)]);

		UstawZawartosc(uklad);
	}

	private string? WalidacjaNIP(string nip)
	{
		if (String.IsNullOrWhiteSpace(nip)) return null;
		nip = nip.Replace("-", "");

		var innyKontrahent = Kontekst.Baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.NIP.Replace("-", "") == nip && kontrahent.Id != Rekord.Id);

		if (innyKontrahent != null) return "Istnieje już kontrahent z takim samym NIPem.";

		if (!Regex.IsMatch(nip, @"^(\w\w)?\d{10}$")) return "NIP ma nieprawidłowy format.";
		if (nip.Length == 12)
		{
			if (nip.StartsWith("PL")) nip = nip.Substring(2);
			else return null;
		}

		int[] wagi = [6, 5, 7, 2, 3, 4, 5, 6, 7, 0];
		int suma = 0;
		for (int i = 0; i < wagi.Length; i++) suma += (nip[i] - '0') * wagi[i];
		if (suma % 11 != (nip[9] - '0')) return "NIP nie jest poprawny.";

		return null;
	}

	private string? WalidacjaNazwy(string nazwa)
	{
		if (String.IsNullOrWhiteSpace(nazwa)) return "Należy podać nazwę firmy"; // Potrzebne (żeby się wyświeliła ikona błędu) mimo Wymagane(textBoxNazwa) w konstrukorze
		static string TrzonNazwy(string nazwa) => String.Join("", nazwa.Where(Char.IsLetterOrDigit).Select(Char.ToLower));
		var szukanaNazwa = TrzonNazwy(nazwa);
		if (szukanaNazwa.Length < 3) return null;

		var inneNazwy = Kontekst.Baza.Kontrahenci.Where(e => e.Id != Rekord.Id).Select(e => e.Nazwa).ToList();

		foreach (var _innaNazwa in inneNazwy)
		{
			var innaNazwa = TrzonNazwy(_innaNazwa);
			if (innaNazwa.Length < 3) continue;

			if (innaNazwa.Contains(szukanaNazwa) || szukanaNazwa.Contains(innaNazwa)) return $"Istnieje już kontrahent o podobnej nazwie \"{_innaNazwa}\"";
		}

		return null;
	}

	private string? WalidacjaPelnejNazwy(string pelnaNazwa)
	{
		if (String.IsNullOrWhiteSpace(pelnaNazwa)) return null;
		var innyKontrahent = Kontekst.Baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.PelnaNazwa == pelnaNazwa && kontrahent.Id != Rekord.Id);
		if (innyKontrahent != null) return "Istnieje już kontrahent z taką samą nazwą.";
		return null;
	}

	private void ZmienionaNazwa()
	{
		textBoxPelnaNazwa.PlaceholderText = textBoxNazwa.Text;
	}

	private void ZmienionyAdresRejestrowy()
	{
		textBoxAdresKorespondencyjny.PlaceholderText = textBoxAdresRejestrowy.Text;
	}

	protected override void KontekstGotowy()
	{
		base.KontekstGotowy();

		new Slownik<SposobPlatnosci>(
			Kontekst, comboBoxSposobPlatnosci, buttonSposobPlatnosci,
			Kontekst.Baza.SposobyPlatnosci.ToList,
			sposobPlatnosci => sposobPlatnosci.Nazwa,
			sposobPlatnosci => { },
			Spisy.SposobyPlatnosci,
			dopuscPustaWartosc: true)
			.Zainstaluj();

		new Slownik<Waluta>(
			Kontekst, comboBoxWaluta, buttonWaluta,
			Kontekst.Baza.Waluty.ToList,
			domyslnaWaluta => domyslnaWaluta.Nazwa,
			domyslnaWaluta => { },
			Spisy.Waluty,
			dopuscPustaWartosc: true)
			.Zainstaluj();

		new Slownik<UrzadSkarbowy>(
			Kontekst, comboBoxKodUrzedu, buttonUrzadSkarbowy,
			Kontekst.Baza.UrzedySkarbowe.OrderBy(urzad => urzad.Kod).ToList,
			urzad => urzad.Kod,
			urzad => { if (urzad == null || Rekord.KodUrzedu == urzad.Kod) return; Rekord.KodUrzedu = urzad.Kod; kontroler.AktualizujKontrolki(); },
			Spisy.UrzedySkarbowe)
			.Zainstaluj();
	}

	protected override void RekordGotowy()
	{
		base.RekordGotowy();
		checkBoxTP.Visible = !Rekord.CzyPodmiot;
		checkBoxImportKSeF.Visible = !Rekord.CzyPodmiot;
		labelSposobPlatnosci.Visible = comboBoxSposobPlatnosci.Visible = buttonSposobPlatnosci.Visible = !Rekord.CzyPodmiot;

		fakturySprzedazy.Spis.KontrahentRef = Rekord;
		fakturySprzedazy.Spis.Kontekst = Kontekst;
		fakturyZakupu.Spis.KontrahentRef = Rekord;
		fakturyZakupu.Spis.Kontekst = Kontekst;

		if (Rekord.CzyPodmiot)
		{
			zakladki.TabPages.Remove(tabPageFakturySprzedazy);
			zakladki.TabPages.Remove(tabPageFakturyZakupu);
		}
		else
		{
			zakladki.TabPages.Remove(tabPagePodatki);
		}
	}

	public override void KoniecEdycji()
	{
		base.KoniecEdycji();
		if (String.IsNullOrEmpty(Rekord.PelnaNazwa)) Rekord.PelnaNazwa = Rekord.Nazwa;
		if (String.IsNullOrEmpty(Rekord.AdresKorespondencyjny)) Rekord.AdresKorespondencyjny = Rekord.AdresRejestrowy;
	}

	private void SprawdzMF()
	{
		var wynik = "";
		OknoPostepu.Uruchom(async cancellationToken =>
		{
			wynik = await IO.MF.SprawdzBialaListeVAT(Rekord.NIP, Rekord.RachunekBankowy.Replace(" ", ""), cancellationToken);
		});

		MessageBox.Show($"Rachunek znajduje się na białej liście VAT, RequestId: {wynik}", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}

	private void PobierzGUS()
	{
		OknoPostepu.Uruchom(async cancellationToken =>
		{
			await IO.GUS.PobierzGUS(Rekord, cancellationToken);
		});

		kontroler.AktualizujKontrolki();
	}

	private void KSeFAuth()
	{
		var nip = Rekord.NIP;
		string? token = null;
		if (Rekord.SrodowiskoKSeF == SrodowiskoKSeF.Test)
		{
			OknoPostepu.Uruchom(async cancellationToken =>
			{
				var api = new IO.KSEF2.API(Rekord.SrodowiskoKSeF);
				var unsignedXml = await api.PobierzZadanieDostepuDoPodpisuAsync(nip);
				cancellationToken.ThrowIfCancellationRequested();
				var signedXml = api.PodpiszZadanieDlaSrodowiskaTestowego(unsignedXml, nip);
				await api.PrzeslijZadanieDostepuAsync(signedXml, cancellationToken);
				cancellationToken.ThrowIfCancellationRequested();
				token = await api.UtworzTokenAsync(cancellationToken);
			});
		}
		else
		{
			using var nowyKontekst = new Kontekst(Kontekst);
			using var edytor = new DostepKSeFEdytor();
			edytor.SrodowiskoKSeF = Rekord.SrodowiskoKSeF;
			edytor.NIP = nip;
			using var okno = new Dialog("Dostęp do KSeF v2", edytor, nowyKontekst);
			okno.CzyPrzyciskiWidoczne = false;
			okno.ShowDialog();
			token = edytor.Token;
		}

		if (token == null) return;
		Rekord.TokenKSeF = token;
		kontroler.AktualizujKontrolki();
	}

	private void CertyfikatKSeF()
	{
		using var nowyKontekst = new Kontekst(Kontekst);
		using var edytor = new ImportCertyfikatuKSeFEdytor();
		edytor.SrodowiskoKSeF = Rekord.SrodowiskoKSeF;
		edytor.NIP = Rekord.NIP;
		using var okno = new Dialog("Dostęp do KSeF v2", edytor, nowyKontekst);
		okno.CzyPrzyciskiWidoczne = false;
		okno.ShowDialog();
		if (!String.IsNullOrEmpty(edytor.Certyfikat)) Rekord.TokenKSeF = edytor.Certyfikat;
		kontroler.AktualizujKontrolki();
	}
}
