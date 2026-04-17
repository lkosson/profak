using ProFak.DB;
using System.Data;
using System.Diagnostics;

namespace ProFak.UI;

partial class FakturaEdytor : Edytor<Faktura>
{
	private readonly Label labelRodzaj;
	private readonly TextBox textBoxNumer;
	private readonly TextBox textBoxDaneSprzedawcy;
	private readonly TextBox textBoxDaneNabywcy;
	private readonly TextBox textBoxRachunekBankowy;
	private readonly ComboBox comboBoxSposobPlatnosci;
	private readonly Button buttonSprzedawca;
	private readonly Button buttonNabywca;
	private readonly Button buttonSposobPlatnosci;
	private readonly ComboBox comboBoxNIPSprzedawcy;
	private readonly ComboBox comboBoxNIPNabywcy;
	private readonly ComboBox comboBoxNazwaSprzedawcy;
	private readonly ComboBox comboBoxWaluta;
	private readonly ComboBox comboBoxNazwaNabywcy;
	private readonly Button buttonWaluta;
	private readonly NumericUpDown numericUpDownNetto;
	private readonly NumericUpDown numericUpDownVat;
	private readonly NumericUpDown numericUpDownBrutto;
	private readonly TextBox textBoxUwagiPubliczne;
	private readonly TextBox textBoxUwagiWewnetrzne;
	private readonly CheckBox checkBoxTP;
	private readonly ComboBox comboBoxProcentKosztow;
	private readonly ComboBox comboBoxProcentVat;
	private readonly Button buttonNowySprzedawca;
	private readonly Button buttonNowyNabywca;
	private readonly CheckBox checkBoxZakupSrodkowTrwalych;
	private readonly CheckBox checkBoxWDT;
	private readonly CheckBox checkBoxWNT;
	private readonly TextBox textBoxOpisZdarzenia;
	private readonly TextBox textBoxNumerKSeF;
	private readonly TextBox textBoxKSeFXML;
	private readonly DateTimePicker dateTimePickerDataWystawienia;
	private readonly DateTimePicker dateTimePickerDataSprzedazy;
	private readonly DateTimePicker dateTimePickerDataWprowadzenia;
	private readonly DateTimePicker dateTimePickerTerminPlatnosci;
	private readonly NumericUpDown numericUpDownKurs;
	private readonly ComboBox comboBoxProceduraMarzy;
	private readonly CheckBox checkBoxReczneKwoty;
	private readonly LinkLabel linkLabelUwagiPomoc;
	private readonly TextBox textBoxNazwaBanku;
	private readonly TextBox textBoxDataKSeF;
	private readonly Button buttonDropDownKSeF;
	private readonly ToolStripMenuItem menuKSeFZapiszXML;
	private readonly ToolStripMenuItem menuKSeFZapiszWizualizacje;
	private readonly ToolStripMenuItem menuKSeFKopiujOdnosnik;
	private readonly ToolStripMenuItem menuKSeFOtworzOdnosnik;
	private readonly ToolStripMenuItem menuKSeFGenerujXML;
	private readonly Zakladki zakladki;
	private readonly TabPage tabPagePodatki;
	private readonly TabPage tabPageKSeF;

	private readonly SpisZAkcjami<Wplata, WplataSpis> wplaty;
	private readonly SpisZAkcjami<PozycjaFaktury, PozycjaFakturySpis> pozycjeFaktury;
	private readonly SpisZAkcjami<Plik, PlikSpis> pliki;
	private readonly SpisZAkcjami<DodatkowyPodmiot, DodatkowyPodmiotSpis> dodatkowePodmioty;

	private Slownik<Kontrahent> slownikNabywcaNazwa = default!;
	private Slownik<Kontrahent> slownikNabywcaNIP = default!;
	private Slownik<Kontrahent> slownikSprzedawcaNazwa = default!;
	private Slownik<Kontrahent> slownikSprzedawcaNIP = default!;

	public virtual bool CzySprzedaz => true;

	public FakturaEdytor()
	{
		labelRodzaj = Kontrolki.Text("");
		textBoxNumer = Kontrolki.TextBox();
		textBoxDaneSprzedawcy = Kontrolki.TextArea(3);
		textBoxDaneNabywcy = Kontrolki.TextArea(3);
		textBoxRachunekBankowy = Kontrolki.TextBox();
		comboBoxSposobPlatnosci = Kontrolki.SuggestBox();
		buttonSprzedawca = Kontrolki.ButtonSlownik();
		buttonNowySprzedawca = Kontrolki.ButtonDodaj(NowySprzedawca);
		buttonNabywca = Kontrolki.ButtonSlownik();
		buttonNowyNabywca = Kontrolki.ButtonDodaj(NowyNabywca);
		buttonSposobPlatnosci = Kontrolki.ButtonSlownik();
		comboBoxNIPSprzedawcy = Kontrolki.SuggestBox();
		comboBoxNIPNabywcy = Kontrolki.SuggestBox();
		comboBoxNazwaSprzedawcy = Kontrolki.SuggestBox();
		comboBoxNazwaNabywcy = Kontrolki.SuggestBox();
		comboBoxWaluta = Kontrolki.DropDownList(szerokosc: 70);
		buttonWaluta = Kontrolki.ButtonSlownik();
		numericUpDownNetto = Kontrolki.NumericUpDown();
		numericUpDownVat = Kontrolki.NumericUpDown();
		numericUpDownBrutto = Kontrolki.NumericUpDown();
		textBoxUwagiPubliczne = Kontrolki.TextArea();
		textBoxUwagiWewnetrzne = Kontrolki.TextArea();
		checkBoxTP = Kontrolki.CheckBox("Podmiot powiązany");
		comboBoxProcentKosztow = Kontrolki.SuggestBox(["100%", "75%", "20%", "0%"]);
		comboBoxProcentVat = Kontrolki.SuggestBox(["100%", "50%", "0%"]);
		checkBoxZakupSrodkowTrwalych = Kontrolki.CheckBox("Zakup środków trwałych");
		checkBoxWDT = Kontrolki.CheckBox("WDT");
		checkBoxWNT = Kontrolki.CheckBox("WNT");
		textBoxOpisZdarzenia = Kontrolki.TextBox();
		textBoxNumerKSeF = Kontrolki.TextBox(szerokosc: 220);
		textBoxDataKSeF = Kontrolki.TextBox();
		textBoxKSeFXML = Kontrolki.TextArea();
		dateTimePickerDataWystawienia = Kontrolki.DatePicker();
		dateTimePickerDataSprzedazy = Kontrolki.DatePicker();
		dateTimePickerDataWprowadzenia = Kontrolki.DatePicker();
		dateTimePickerTerminPlatnosci = Kontrolki.DatePicker();
		numericUpDownKurs = Kontrolki.NumericUpDown(poPrzecinku: 4, szerokosc: 70);
		comboBoxProceduraMarzy = Kontrolki.DropDownList();
		checkBoxReczneKwoty = Kontrolki.CheckBox("Kwota \"razem\" ustawiona ręcznie");
		linkLabelUwagiPomoc = Kontrolki.LinkPomoc(PomocUwagiPubliczne);
		textBoxNazwaBanku = Kontrolki.TextBox(podpowiedz: "Nazwa banku", szerokosc: 120);
		menuKSeFGenerujXML = Kontrolki.PopupMenuItem("Generuj XML", GenerujXML);
		menuKSeFZapiszXML = Kontrolki.PopupMenuItem("Zapisz XML", ZapiszXML);
		menuKSeFZapiszWizualizacje = Kontrolki.PopupMenuItem("Zapisz PDF z wizualizacją", ZapiszWizualizacje);
		menuKSeFKopiujOdnosnik = Kontrolki.PopupMenuItem("Kopiuj odnośnik do schowka", KopiujOdnosnik);
		menuKSeFOtworzOdnosnik = Kontrolki.PopupMenuItem("Otwórz odnośnik w przeglądarce", OtworzOdnosnik);
		buttonDropDownKSeF = Kontrolki.ButtonMenu("e-Faktura", [menuKSeFGenerujXML, menuKSeFZapiszXML, menuKSeFZapiszWizualizacje, menuKSeFKopiujOdnosnik, menuKSeFOtworzOdnosnik]);

		textBoxKSeFXML.Font = new Font("Consolas", 9);
		textBoxKSeFXML.ScrollBars = ScrollBars.Both;

		kontroler.Slownik<ProceduraMarży>(comboBoxProceduraMarzy);

		kontroler.Powiazanie(textBoxNumer, faktura => faktura.Numer);
		kontroler.Powiazanie(comboBoxWaluta, faktura => faktura.WalutaRef);
		kontroler.Powiazanie(numericUpDownKurs, faktura => faktura.KursWaluty);

		kontroler.Powiazanie(comboBoxNIPSprzedawcy, faktura => faktura.NIPSprzedawcy);
		kontroler.Powiazanie(comboBoxNazwaSprzedawcy, faktura => faktura.NazwaSprzedawcy);
		kontroler.Powiazanie(textBoxDaneSprzedawcy, faktura => faktura.DaneSprzedawcy);

		kontroler.Powiazanie(comboBoxNIPNabywcy, faktura => faktura.NIPNabywcy);
		kontroler.Powiazanie(comboBoxNazwaNabywcy, faktura => faktura.NazwaNabywcy);
		kontroler.Powiazanie(textBoxDaneNabywcy, faktura => faktura.DaneNabywcy);

		kontroler.Powiazanie(dateTimePickerDataWystawienia, faktura => faktura.DataWystawienia, UstawDateWystawienia);
		kontroler.Powiazanie(dateTimePickerDataSprzedazy, faktura => faktura.DataSprzedazy);
		kontroler.Powiazanie(dateTimePickerDataWprowadzenia, faktura => faktura.DataWprowadzenia);
		kontroler.Powiazanie(dateTimePickerTerminPlatnosci, faktura => faktura.TerminPlatnosci);

		kontroler.Powiazanie(comboBoxSposobPlatnosci, faktura => faktura.OpisSposobuPlatnosci);
		kontroler.Powiazanie(textBoxRachunekBankowy, faktura => faktura.RachunekBankowy);
		kontroler.Powiazanie(textBoxNazwaBanku, faktura => faktura.NazwaBanku);

		kontroler.Powiazanie(numericUpDownNetto, faktura => faktura.RazemNetto);
		kontroler.Powiazanie(numericUpDownVat, faktura => faktura.RazemVat);
		kontroler.Powiazanie(numericUpDownBrutto, faktura => faktura.RazemBrutto);

		kontroler.Powiazanie(textBoxUwagiPubliczne, faktura => faktura.UwagiPubliczne);
		kontroler.Powiazanie(textBoxUwagiWewnetrzne, faktura => faktura.UwagiWewnetrzne);

		kontroler.Powiazanie(checkBoxTP, faktura => faktura.CzyTP);
		kontroler.Powiazanie(comboBoxProcentKosztow, faktura => faktura.ProcentKosztow.ToString("0") + "%", (faktura, wartosc) => faktura.ProcentKosztow = Int32.TryParse(wartosc.TrimEnd(' ', '%'), out var liczba) ? liczba : 100);
		kontroler.Powiazanie(comboBoxProcentVat, faktura => faktura.ProcentVatNaliczonego.ToString("0") + "%", (faktura, wartosc) => faktura.ProcentVatNaliczonego = Int32.TryParse(wartosc.TrimEnd(' ', '%'), out var liczba) ? liczba : 100);
		kontroler.Powiazanie(checkBoxZakupSrodkowTrwalych, faktura => faktura.CzyZakupSrodkowTrwalych);
		kontroler.Powiazanie(checkBoxReczneKwoty, faktura => faktura.CzyWartosciReczne, UstawRazem);
		kontroler.Powiazanie(checkBoxWDT, faktura => faktura.CzyWDT);
		kontroler.Powiazanie(checkBoxWNT, faktura => faktura.CzyWNT);
		kontroler.Powiazanie(textBoxOpisZdarzenia, faktura => faktura.OpisZdarzenia);
		kontroler.Powiazanie(comboBoxProceduraMarzy, faktura => faktura.ProceduraMarzy);

		kontroler.Powiazanie(textBoxKSeFXML, faktura => faktura.XMLKSeFFmt);
		kontroler.Powiazanie(textBoxNumerKSeF, faktura => faktura.NumerKSeF);

		Wymagane(textBoxDaneNabywcy);
		Wymagane(textBoxDaneSprzedawcy);
		Wymagane(comboBoxNazwaNabywcy);
		Wymagane(comboBoxNazwaSprzedawcy);
		Wymagane(comboBoxSposobPlatnosci);
		Wymagane(comboBoxWaluta);
		Walidacja<ProceduraMarży>(comboBoxProceduraMarzy, WalidacjaProceduryMarzy, false);
		Walidacja(textBoxNumer, WalidacjaNumer, false);

		var naglowek = new Siatka([100, 0, -1, 20, 0, 0, 0, 20, 0, 0], []);
		naglowek.DodajWiersz([labelRodzaj, Kontrolki.Label("Numer"), textBoxNumer, null, Kontrolki.Label("Waluta"), comboBoxWaluta, buttonWaluta, null, Kontrolki.Label("Kurs"), numericUpDownKurs]);

		var sprzedawca = new Siatka([0, -1, 0, 0], []);
		sprzedawca.DodajWiersz("NIP", [comboBoxNIPSprzedawcy, buttonSprzedawca, buttonNowySprzedawca]);
		sprzedawca.DodajWiersz("Nazwa", [(comboBoxNazwaSprzedawcy, 3)]);
		sprzedawca.DodajWiersz("Adres", [(textBoxDaneSprzedawcy, 3)]);

		var nabywca = new Siatka([0, -1, 0, 0], []);
		nabywca.DodajWiersz("NIP", [comboBoxNIPNabywcy, buttonNabywca, buttonNowyNabywca]);
		nabywca.DodajWiersz("Nazwa", [(comboBoxNazwaNabywcy, 3)]);
		nabywca.DodajWiersz("Adres", [(textBoxDaneNabywcy, 3)]);

		var kontrahenci = new Siatka([-1, -1], []);
		kontrahenci.DodajWiersz([
			new Grupa("Sprzedawca", sprzedawca),
			new Grupa("Nabywca", nabywca)
			]);

		var daty = new DwieKolumny();
		daty.DodajWiersz(dateTimePickerDataWystawienia, "Data wystawienia");
		daty.DodajWiersz(dateTimePickerDataSprzedazy, "Data sprzedaży");
		daty.DodajWiersz(dateTimePickerDataWprowadzenia, "Data wprowadzenia");

		var platnosc = new Siatka([0, -1, 0, 0], []);
		platnosc.DodajWiersz("Sposób płatności", [(comboBoxSposobPlatnosci, 2), (buttonSposobPlatnosci, 1)]);
		platnosc.DodajWiersz("Termin płatności", [(dateTimePickerTerminPlatnosci, 3)]);
		platnosc.DodajWiersz("Numer rachunku", [(textBoxRachunekBankowy, 1), (textBoxNazwaBanku, 2)]);

		var razem = new DwieKolumny();
		razem.DodajWiersz(numericUpDownNetto, "Netto");
		razem.DodajWiersz(numericUpDownVat, "VAT");
		razem.DodajWiersz(numericUpDownBrutto, "Brutto");

		var datyPlatnoscKwoty = new Siatka([0, -1, 0], []);
		datyPlatnoscKwoty.DodajWiersz([
			new Grupa("Daty", daty),
			new Grupa("Płatności", platnosc),
			new Grupa("Razem", razem)
			]);

		pozycjeFaktury = Spisy.PozycjeFaktur();
		wplaty = Spisy.Wplaty();
		pliki = Spisy.Pliki();
		dodatkowePodmioty = Spisy.DodatkowePodmioty();

		Grupa gruppaUwagiPubliczne;
		linkLabelUwagiPomoc.Anchor = AnchorStyles.Bottom;
		var uwagi = new Siatka([-1, -1], [-1]);
		uwagi.DodajWiersz([
			gruppaUwagiPubliczne = new Grupa("Uwagi (drukowane na fakturze)", textBoxUwagiPubliczne),
			new Grupa("Uwagi (wewnętrzne)", textBoxUwagiWewnetrzne)
			]);

		gruppaUwagiPubliczne.Controls.Add(linkLabelUwagiPomoc);
		linkLabelUwagiPomoc.Location = new Point(textBoxUwagiPubliczne.Width - 20, textBoxUwagiPubliczne.Height);
		linkLabelUwagiPomoc.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
		linkLabelUwagiPomoc.BringToFront();

		var podatki = new Siatka([0, 0, 20, 0, -1], []);
		podatki.DodajWiersz("Udział w kosztach", [comboBoxProcentKosztow, null, checkBoxWDT]);
		podatki.DodajWiersz("Udział w VA naliczonym", [comboBoxProcentVat, null, checkBoxWNT]);
		podatki.DodajWiersz("Opis zdarzenia na PKPiR", [textBoxOpisZdarzenia, null, checkBoxTP]);
		podatki.DodajWiersz("Procedura marży", [comboBoxProceduraMarzy, null, checkBoxZakupSrodkowTrwalych]);
		podatki.DodajWiersz([null, null, null, checkBoxReczneKwoty]);

		textBoxDataKSeF.ReadOnly = true;
		buttonDropDownKSeF.Width = 120;
		var ksef = new Siatka([0, 0, 20, 0, 0, 0, -1], [0, -1]);
		ksef.DodajWiersz("Numer KSeF", [textBoxNumerKSeF, null, Kontrolki.Label("Data KSeF"), textBoxDataKSeF, buttonDropDownKSeF]);
		ksef.DodajWiersz([(textBoxKSeFXML, 7)]);

		pozycjeFaktury.Height = 200;
		zakladki = new Zakladki();
		zakladki.Dodaj("Pozycje", pozycjeFaktury);
		zakladki.Dodaj("Wpłaty", wplaty);
		zakladki.Dodaj("Pliki", pliki);
		zakladki.Dodaj("Uwagi", uwagi);
		zakladki.Dodaj("Dodatkowe podmioty", dodatkowePodmioty);
		tabPagePodatki = zakladki.Dodaj("Podatki", podatki);
		tabPageKSeF = zakladki.Dodaj("KSeF", ksef);

		var uklad = new Siatka([-1], [0, 0, 0, -1]);
		uklad.DodajWiersz([naglowek]);
		uklad.DodajWiersz([kontrahenci]);
		uklad.DodajWiersz([datyPlatnoscKwoty]);
		uklad.DodajWiersz([zakladki]);

		pozycjeFaktury.Spis.RekordyZmienione += pozycjeFakturySpis_RekordyZmienione;

		UstawZawartosc(uklad);
	}

	private string? WalidacjaProceduryMarzy(ProceduraMarży wartosc)
	{
		if (Rekord.Rodzaj != RodzajFaktury.VatMarża && Rekord.Rodzaj != RodzajFaktury.KorektaVatMarży) return null;
		if (wartosc == ProceduraMarży.NieDotyczy)
		{
			zakladki.SelectedTab = tabPagePodatki;
			return "Należy wybrać procedurę marży dla faktury Vat marża.";
		}
		return null;
	}

	private string? WalidacjaNumer(string numer)
	{
		if (String.IsNullOrWhiteSpace(numer)) return Rekord.Numerator.HasValue ? null : "Należy uzupełnić numer faktury.";
		var innaFaktura = Kontekst.Baza.Faktury.FirstOrDefault(faktura => faktura.Numer == Rekord.Numer && faktura.NIPSprzedawcy == Rekord.NIPSprzedawcy && faktura.Id != Rekord.Id);
		if (innaFaktura != null) return Rekord.CzySprzedaz ? $"Faktura z takim numerem była już wystawiona {innaFaktura.DataWystawienia:d MMMM yyyy}." : $"Faktura od tego kontrahenta z takim samym numerem była wprowadzona {innaFaktura.DataWprowadzenia:d MMMM yyyy}.";
		return null;
	}

	private void pozycjeFakturySpis_RekordyZmienione()
	{
		Rekord.PrzeliczRazem(Kontekst.Baza);
		kontroler.AktualizujKontrolki();
	}

	protected override void KontekstGotowy()
	{
		base.KontekstGotowy();

		new Slownik<Waluta>(
			Kontekst, comboBoxWaluta, buttonWaluta,
			Kontekst.Baza.Waluty.OrderBy(waluta => waluta.Nazwa).ToList,
			waluta => waluta.Skrot,
			waluta => { if (waluta == null) return; numericUpDownKurs.Enabled = !waluta.CzyDomyslna; if (waluta.CzyDomyslna && Rekord.KursWaluty != 1) numericUpDownKurs.Value = 1; },
			Spisy.Waluty)
			.Zainstaluj();

		new Slownik<SposobPlatnosci>(
			Kontekst, comboBoxSposobPlatnosci, buttonSposobPlatnosci,
			Kontekst.Baza.SposobyPlatnosci.OrderBy(sposobPlatnosci => sposobPlatnosci.Nazwa).ToList,
			sposobPlatnosci => sposobPlatnosci.Nazwa,
			sposobPlatnosci => { if (UstawSposobPlatnosci(Rekord, sposobPlatnosci)) kontroler.AktualizujKontrolki(); },
			Spisy.SposobyPlatnosci)
			.Zainstaluj();

		slownikNabywcaNIP = new Slownik<Kontrahent>(
			Kontekst, comboBoxNIPNabywcy, buttonNabywca,
			Kontekst.Baza.Kontrahenci.Where(kontrahent => !kontrahent.CzyArchiwalny && !kontrahent.CzyImportKSeF && kontrahent.CzyPodmiot == !CzySprzedaz).OrderBy(kontrahent => kontrahent.NIP).ToList,
			kontrahent => kontrahent.NIP,
			kontrahent => { if (UstawNabywce(Rekord, kontrahent)) kontroler.AktualizujKontrolki(); },
			Spisy.Kontrahenci);
		slownikNabywcaNIP.Zainstaluj();

		slownikNabywcaNazwa = new Slownik<Kontrahent>(
			Kontekst, comboBoxNazwaNabywcy, null,
			Kontekst.Baza.Kontrahenci.Where(kontrahent => !kontrahent.CzyArchiwalny && !kontrahent.CzyImportKSeF && kontrahent.CzyPodmiot == !CzySprzedaz).OrderBy(kontrahent => kontrahent.Nazwa).ToList,
			kontrahent => kontrahent.PelnaNazwaLubNazwa,
			kontrahent => { if (UstawNabywce(Rekord, kontrahent)) kontroler.AktualizujKontrolki(); },
			Spisy.Kontrahenci);
		slownikNabywcaNazwa.Zainstaluj();

		slownikSprzedawcaNazwa = new Slownik<Kontrahent>(
			Kontekst, comboBoxNIPSprzedawcy, buttonSprzedawca,
			Kontekst.Baza.Kontrahenci.Where(kontrahent => !kontrahent.CzyArchiwalny && !kontrahent.CzyImportKSeF && kontrahent.CzyPodmiot == CzySprzedaz).OrderBy(kontrahent => kontrahent.NIP).ToList,
			kontrahent => kontrahent.NIP,
			kontrahent => { if (UstawSprzedawce(Rekord, kontrahent)) kontroler.AktualizujKontrolki(); },
			Spisy.Kontrahenci);
		slownikSprzedawcaNazwa.Zainstaluj();

		slownikSprzedawcaNIP = new Slownik<Kontrahent>(
			Kontekst, comboBoxNazwaSprzedawcy, null,
			Kontekst.Baza.Kontrahenci.Where(kontrahent => !kontrahent.CzyArchiwalny && !kontrahent.CzyImportKSeF && kontrahent.CzyPodmiot == CzySprzedaz).OrderBy(kontrahent => kontrahent.Nazwa).ToList,
			kontrahent => kontrahent.PelnaNazwaLubNazwa,
			kontrahent => { if (UstawSprzedawce(Rekord, kontrahent)) kontroler.AktualizujKontrolki(); },
			Spisy.Kontrahenci);
		slownikSprzedawcaNIP.Zainstaluj();
	}

	private bool UstawNabywce(Faktura rekord, Kontrahent? kontrahent)
	{
		if (kontrahent == null || rekord.NabywcaRef == kontrahent.Ref) return false;
		rekord.UstawNabywce(kontrahent);
		if (kontrahent.SposobPlatnosciRef.IsNotNull) comboBoxSposobPlatnosci.SelectedValue = kontrahent.SposobPlatnosciRef;
		if (kontrahent.DomyslnaWalutaRef.IsNotNull) comboBoxWaluta.SelectedValue = kontrahent.DomyslnaWalutaRef;
		return true;
	}

	private bool UstawSprzedawce(Faktura rekord, Kontrahent? kontrahent)
	{
		if (kontrahent == null || rekord.SprzedawcaRef == kontrahent.Ref) return false;
		rekord.UstawSprzedawce(kontrahent);
		if (kontrahent.SposobPlatnosciRef.IsNotNull) comboBoxSposobPlatnosci.SelectedValue = kontrahent.SposobPlatnosciRef;
		return true;
	}

	private void UstawDateWystawienia(Faktura rekord, DateTime dataWystawienia)
	{
		if (rekord.DataWystawienia == dataWystawienia) return;
		rekord.DataWystawienia = dataWystawienia;
		var sposobPlatnosci = Kontekst.Baza.ZnajdzLubNull(Rekord.SposobPlatnosciRef);
		if (sposobPlatnosci == null) return;
		Rekord.TerminPlatnosci = Rekord.DataWystawienia.AddDays(sposobPlatnosci.LiczbaDni);
		kontroler.AktualizujKontrolki();
	}

	private bool UstawSposobPlatnosci(Faktura rekord, SposobPlatnosci? sposobPlatnosci)
	{
		if (sposobPlatnosci == null || rekord == null || rekord.SposobPlatnosciRef == sposobPlatnosci.Ref) return false;
		rekord.UstawSposobPlatnosci(sposobPlatnosci);
		return true;
	}

	private void UstawRazem()
	{
		if (!Rekord.CzyWartosciReczne)
		{
			Rekord.PrzeliczRazem(Kontekst.Baza);
			kontroler.AktualizujKontrolki();
		}
		numericUpDownNetto.Enabled = Rekord.CzyWartosciReczne;
		numericUpDownVat.Enabled = Rekord.CzyWartosciReczne;
		numericUpDownBrutto.Enabled = Rekord.CzyWartosciReczne;
	}

	protected override void PrzygotujRekord(Faktura rekord)
	{
		base.PrzygotujRekord(rekord);
		if (rekord.WalutaRef.IsNull) rekord.WalutaRef = Kontekst.Baza.Waluty.FirstOrDefault(waluta => waluta.CzyDomyslna);
		if (rekord.SposobPlatnosciRef.IsNull) UstawSposobPlatnosci(rekord, Kontekst.Baza.SposobyPlatnosci.FirstOrDefault(sposobPlatnosci => sposobPlatnosci.CzyDomyslny));
		if (rekord.SprzedawcaRef.IsNull && rekord.CzySprzedaz) UstawSprzedawce(rekord, Kontekst.Baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot && !kontrahent.CzyArchiwalny));
		if (rekord.NabywcaRef.IsNull && rekord.CzyZakup) UstawNabywce(rekord, Kontekst.Baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot && !kontrahent.CzyArchiwalny));
		if (rekord.CzyZakup)
		{
			var domyslnaStawkaVat = Kontekst.Baza.StawkiVat.FirstOrDefault(vat => vat.CzyDomyslna);
			if (domyslnaStawkaVat != null && domyslnaStawkaVat.Wartosc == 0 && domyslnaStawkaVat.Skrot != null && domyslnaStawkaVat.Skrot.Contains("zw", StringComparison.CurrentCultureIgnoreCase)) rekord.ProcentVatNaliczonego = 0;
		}
	}

	protected override void RekordGotowy()
	{
		base.RekordGotowy();
		UstawRazem();
		wplaty.Spis.FakturaRef = Rekord;
		wplaty.Spis.Kontekst = Kontekst;
		pozycjeFaktury.Spis.FakturaRef = Rekord;
		pozycjeFaktury.Spis.Kontekst = Kontekst;
		pliki.Spis.FakturaRef = Rekord;
		pliki.Spis.Kontekst = Kontekst;
		dodatkowePodmioty.Spis.FakturaRef = Rekord;
		dodatkowePodmioty.Spis.Kontekst = Kontekst;
		menuKSeFKopiujOdnosnik.Enabled = !String.IsNullOrEmpty(Rekord.URLKSeF) || !String.IsNullOrEmpty(Rekord.XMLKSeF);
		menuKSeFOtworzOdnosnik.Enabled = !String.IsNullOrEmpty(Rekord.URLKSeF) || !String.IsNullOrEmpty(Rekord.XMLKSeF);
		menuKSeFZapiszXML.Enabled = !String.IsNullOrEmpty(Rekord.XMLKSeF) || CzySprzedaz;
		menuKSeFZapiszWizualizacje.Enabled = !String.IsNullOrEmpty(Rekord.XMLKSeF);

		var fakturaKorygowana = Kontekst.Baza.ZnajdzLubNull(Rekord.FakturaKorygowanaRef);

		if (Rekord.Rodzaj == RodzajFaktury.Sprzedaż) labelRodzaj.Text = "Sprzedaż";
		else if (Rekord.Rodzaj == RodzajFaktury.Zakup) labelRodzaj.Text = "Zakup";
		else if (Rekord.Rodzaj == RodzajFaktury.KorektaSprzedaży) labelRodzaj.Text = "Korekta sprzedaży " + fakturaKorygowana?.Numer;
		else if (Rekord.Rodzaj == RodzajFaktury.KorektaZakupu) labelRodzaj.Text = "Korekta zakupu " + fakturaKorygowana?.Numer;
		else if (Rekord.Rodzaj == RodzajFaktury.Proforma) labelRodzaj.Text = "Proforma";
		else if (Rekord.Rodzaj == RodzajFaktury.DowódWewnętrzny) labelRodzaj.Text = "Dowód wewnętrzny";
		else if (Rekord.Rodzaj == RodzajFaktury.VatMarża) labelRodzaj.Text = "Vat marża";
		else if (Rekord.Rodzaj == RodzajFaktury.KorektaVatMarży) labelRodzaj.Text = "Korekta vat marży";
		else if (Rekord.Rodzaj == RodzajFaktury.KorektaRachunku) labelRodzaj.Text = "Korekta rachunku";
		else labelRodzaj.Text = Rekord.Rodzaj.ToString();

		textBoxDataKSeF.Text = Rekord.DataKSeF == null ? "" : Rekord.DataKSeF.Value.ToString(Wyglad.FormatCzasu);

		if (String.IsNullOrWhiteSpace(Rekord.Numer) && Rekord.Numerator.HasValue)
		{
			var numer = Numerator.NadajNumer(Kontekst.Baza, Rekord.Numerator.Value, Rekord.Podstawienie, zwiekszLicznik: false);
			textBoxNumer.PlaceholderText = numer;
			comboBoxNazwaNabywcy.Focus();
			ActiveControl = comboBoxNazwaNabywcy;
		}
		else
		{
			textBoxNumer.Focus();
			ActiveControl = textBoxNumer;
		}

		if (Rekord.CzySprzedaz)
		{
			checkBoxTP.Enabled = true;
			comboBoxProcentKosztow.Enabled = false;
			comboBoxProcentVat.Enabled = false;
			checkBoxZakupSrodkowTrwalych.Enabled = false;
			checkBoxWDT.Enabled = true;
			checkBoxWNT.Enabled = false;
			comboBoxProceduraMarzy.Enabled = Rekord.Rodzaj == RodzajFaktury.VatMarża || Rekord.Rodzaj == RodzajFaktury.KorektaVatMarży || Rekord.Rodzaj == RodzajFaktury.Proforma || Rekord.Rodzaj == RodzajFaktury.Zakup || Rekord.Rodzaj == RodzajFaktury.KorektaZakupu;

			comboBoxNIPSprzedawcy.Enabled = false;
			comboBoxNazwaSprzedawcy.Enabled = false;
			buttonSprzedawca.Enabled = false;
			buttonNowySprzedawca.Enabled = false;
			textBoxDaneSprzedawcy.Enabled = false;

			textBoxNumerKSeF.ReadOnly = true;
		}
		else
		{
			checkBoxTP.Enabled = false;
			comboBoxProcentKosztow.Enabled = true;
			comboBoxProcentVat.Enabled = true;
			checkBoxZakupSrodkowTrwalych.Enabled = true;
			checkBoxWDT.Enabled = false;
			checkBoxWNT.Enabled = true;
			comboBoxProceduraMarzy.Enabled = false;

			comboBoxNIPNabywcy.Enabled = false;
			comboBoxNazwaNabywcy.Enabled = false;
			buttonNabywca.Enabled = false;
			buttonNowyNabywca.Enabled = false;
			textBoxDaneNabywcy.Enabled = false;

			menuKSeFGenerujXML.Enabled = false;
			textBoxKSeFXML.ReadOnly = true;
		}

		if (Rekord.Rodzaj == RodzajFaktury.Rachunek || Rekord.Rodzaj == RodzajFaktury.KorektaRachunku)
		{
			zakladki.TabPages.Remove(tabPageKSeF);
			zakladki.TabPages.Remove(tabPagePodatki);
		}
	}

	private void NowySprzedawca()
	{
		var kontrahent = new Kontrahent { Nazwa = comboBoxNazwaSprzedawcy.Text, NIP = comboBoxNIPSprzedawcy.Text, AdresRejestrowy = textBoxDaneSprzedawcy.Text };
		if (!EdytorNowegoKontrahenta(kontrahent)) return;
		slownikSprzedawcaNazwa.Przeladuj();
		slownikSprzedawcaNIP.Przeladuj();
		UstawSprzedawce(Rekord, kontrahent);
		kontroler.AktualizujKontrolki();
	}

	private void NowyNabywca()
	{
		var kontrahent = new Kontrahent { Nazwa = comboBoxNazwaNabywcy.Text, NIP = comboBoxNIPNabywcy.Text, AdresRejestrowy = textBoxDaneNabywcy.Text };
		if (!EdytorNowegoKontrahenta(kontrahent)) return;
		slownikNabywcaNazwa.Przeladuj();
		slownikNabywcaNIP.Przeladuj();
		UstawNabywce(Rekord, kontrahent);
		kontroler.AktualizujKontrolki();
	}

	private bool EdytorNowegoKontrahenta(Kontrahent kontrahent)
	{
		using var nowyKontekst = new Kontekst(Kontekst);
		using var transakcja = nowyKontekst.Transakcja();
		nowyKontekst.Dodaj(kontrahent);
		nowyKontekst.Baza.Zapisz(kontrahent);
		using var edytor = new KontrahentEdytor();
		edytor.Przygotuj(nowyKontekst, kontrahent);
		if (!DialogEdycji.Pokaz("Nowy kontrahent", edytor, nowyKontekst)) return false;
		edytor.KoniecEdycji();
		nowyKontekst.Baza.Zapisz(kontrahent);
		transakcja.Zatwierdz();
		return true;
	}

	private void GenerujXML()
	{
		if (String.IsNullOrEmpty(Rekord.Numer))
		{
			OknoKomunikatu.Ostrzezenie("Przed wygenerowaniem postaci ustrukturyzowanej należy zapisać fakturę w celu nadania jej numeru.");
			return;
		}
		if (!String.IsNullOrWhiteSpace(Rekord.XMLKSeF) && !OknoKomunikatu.PytanieTakNie("Faktura ma już wygenerowaną postać ustrukturyzowaną. Czy na pewno chcesz ją wygenerować ponownie?", domyslnie: false)) return;
		Kontekst.Baza.Zapisz(Rekord);
		var xml = IO.FA_3.Generator.ZbudujXML(Kontekst.Baza, Rekord);
		Rekord.XMLKSeF = xml;
		kontroler.AktualizujKontrolki();
	}

	private void ZapiszXML()
	{
		var nowyKontekst = new Kontekst(Kontekst);
		var akcja = new ZapiszJakoXMLLokalneAkcja();
		IEnumerable<Faktura> rekord = [ Rekord ];
		akcja.Uruchom(nowyKontekst, ref rekord);
	}

	private void ZapiszWizualizacje()
	{
		using var dialog = new SaveFileDialog();
		dialog.Filter = "Dokument PDF (*.pdf)|*.pdf";
		dialog.Title = "Zapisywanie wizualizacji faktury";
		dialog.RestoreDirectory = true;
		dialog.FileName = Rekord.NumerKSeFJakoNazwaPliku + ".pdf";
		if (dialog.ShowDialog() != DialogResult.OK) return;
		byte[] pdf = [];
		OknoPostepu.Uruchom(cancellationToken =>
		{
			pdf = IO.KSEFPDF.Generator.ZbudujPDF(Rekord.XMLKSeF, Rekord.NumerKSeF, cancellationToken);
			return Task.CompletedTask;
		});
		File.WriteAllBytes(dialog.FileName, pdf);
		Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = dialog.FileName });
	}

	private void KopiujOdnosnik()
	{
		var url = Rekord.URLKSeF;
		if (String.IsNullOrEmpty(url))
		{
			var podmiot = Kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
			using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
			url = api.ZbudujUrl(Rekord.XMLKSeF, Rekord.NIPSprzedawcy, Rekord.DataWystawienia);
		}
		Clipboard.SetText(url);
	}

	private void OtworzOdnosnik()
	{
		var url = Rekord.URLKSeF;
		if (String.IsNullOrEmpty(url))
		{
			var podmiot = Kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
			using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
			url = api.ZbudujUrl(Rekord.XMLKSeF, Rekord.NIPSprzedawcy, Rekord.DataWystawienia);
		}
		Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = url });
	}

	private const string PomocUwagiPubliczne = @"Następujące frazy zostaną rozpoznane i automatycznie umieszczone w odpowiednich polach w momencie wysyłki do KSeF:

BDO: XXXXXXXXX
KRS: XXXXXXXXXX
REGON: XXXXXXXXX
Zamówienie: XXXX lub Numer zamówienia: XXXX
Data zamówienia: XXXX-XX-XX
Przyczyna korekty: XXXX
Mechanizm podzielonej płatności lub Split payment

Pozostałe elementy tekstowe zostaną przekazane jako dodatkowy opis.";

	public override void KoniecEdycji()
	{
		Rekord.PoprawNumeracjePozycji(Kontekst.Baza);
		base.KoniecEdycji();
	}
}
