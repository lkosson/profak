using ProFak.DB;
using System.Data;
using System.Diagnostics;

namespace ProFak.UI;

partial class FakturaEdytor : FakturaEdytorBase
{
	private readonly SpisZAkcjami<Wplata, WplataSpis> wplaty;
	private readonly SpisZAkcjami<PozycjaFaktury, PozycjaFakturySpis> pozycjeFaktury;
	private readonly SpisZAkcjami<Plik, PlikSpis> pliki;
	private readonly SpisZAkcjami<DodatkowyPodmiot, DodatkowyPodmiotSpis> dodatkowePodmioty;

	private Slownik<Kontrahent> slownikNabywcaNazwa = default!;
	private Slownik<Kontrahent> slownikNabywcaNIP = default!;
	private Slownik<Kontrahent> slownikSprzedawcaNazwa = default!;
	private Slownik<Kontrahent> slownikSprzedawcaNIP = default!;

	private string uwagiNabywcy = "";
	private string uwagiSprzedawcy = "";

	public virtual bool CzySprzedaz => true;

	public FakturaEdytor()
	{
		InitializeComponent();

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

		tabPageWplaty.Controls.Add(wplaty = Spisy.Wplaty());
		tabPagePozycje.Controls.Add(pozycjeFaktury = Spisy.PozycjeFaktur());
		tabPagePliki.Controls.Add(pliki = Spisy.Pliki());
		tabPageDodatkowePodmioty.Controls.Add(dodatkowePodmioty = Spisy.DodatkowePodmioty());
		pozycjeFaktury.Spis.RekordyZmienione += pozycjeFakturySpis_RekordyZmienione;

		dateTimePickerDataSprzedazy.CustomFormat = Format.Data;
		dateTimePickerDataWprowadzenia.CustomFormat = Format.Data;
		dateTimePickerDataWystawienia.CustomFormat = Format.Data;
		dateTimePickerTerminPlatnosci.CustomFormat = Format.Data;

		dateTimePickerDataSprzedazy.Format = DateTimePickerFormat.Custom;
		dateTimePickerDataWprowadzenia.Format = DateTimePickerFormat.Custom;
		dateTimePickerDataWystawienia.Format = DateTimePickerFormat.Custom;
		dateTimePickerTerminPlatnosci.Format = DateTimePickerFormat.Custom;

		Wyglad.UsunSkrotyZakladek(tabControl1);
	}

	private string? WalidacjaProceduryMarzy(ProceduraMarży wartosc)
	{
		if (Rekord.Rodzaj != RodzajFaktury.VatMarża && Rekord.Rodzaj != RodzajFaktury.KorektaVatMarży) return null;
		if (wartosc == ProceduraMarży.NieDotyczy)
		{
			tabControl1.SelectedTab = tabPagePodatki;
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


	protected override void OnHandleCreated(EventArgs e)
	{
		base.OnHandleCreated(e);
		ParentForm?.KeyDown += Form_OnKeyDown;
	}

	private void Form_OnKeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1 && e.Modifiers == Keys.Control)
		{
			tabControl1.SelectedTab = tabPagePozycje;
			pozycjeFaktury.Focus();
		}
		else if (e.KeyCode == Keys.F2 && e.Modifiers == Keys.Control)
		{
			tabControl1.SelectedTab = tabPageWplaty;
			wplaty.Focus();
		}
		else if (e.KeyCode == Keys.F3 && e.Modifiers == Keys.Control)
		{
			tabControl1.SelectedTab = tabPagePliki;
			pliki.Focus();
		}
		else if (e.KeyCode == Keys.F4 && e.Modifiers == Keys.Control)
		{
			tabControl1.SelectedTab = tabPageUwagi;
			textBoxUwagiPubliczne.Focus();
		}
		else if (e.KeyCode == Keys.F5 && e.Modifiers == Keys.Control)
		{
			tabControl1.SelectedTab = tabPageDodatkowePodmioty;
			wplaty.Focus();
		}
		else if (e.KeyCode == Keys.F6 && e.Modifiers == Keys.Control)
		{
			tabControl1.SelectedTab = tabPagePodatki;
			wplaty.Focus();
		}
		else if (e.KeyCode == Keys.F7 && e.Modifiers == Keys.Control)
		{
			tabControl1.SelectedTab = tabPageKSeF;
			textBoxNumerKSeF.Focus();
		}
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
			Kontekst.Baza.Kontrahenci.Where(kontrahent => !kontrahent.CzyArchiwalny && kontrahent.CzyPodmiot == !CzySprzedaz).OrderBy(kontrahent => kontrahent.NIP).ToList,
			kontrahent => kontrahent.NIP,
			kontrahent => { if (UstawNabywce(Rekord, kontrahent)) kontroler.AktualizujKontrolki(); },
			Spisy.Kontrahenci);
		slownikNabywcaNIP.Zainstaluj();

		slownikNabywcaNazwa = new Slownik<Kontrahent>(
			Kontekst, comboBoxNazwaNabywcy, null,
			Kontekst.Baza.Kontrahenci.Where(kontrahent => !kontrahent.CzyArchiwalny && kontrahent.CzyPodmiot == !CzySprzedaz).OrderBy(kontrahent => kontrahent.Nazwa).ToList,
			kontrahent => kontrahent.PelnaNazwaLubNazwa,
			kontrahent => { if (UstawNabywce(Rekord, kontrahent)) kontroler.AktualizujKontrolki(); },
			Spisy.Kontrahenci);
		slownikNabywcaNazwa.Zainstaluj();

		slownikSprzedawcaNazwa = new Slownik<Kontrahent>(
			Kontekst, comboBoxNIPSprzedawcy, buttonSprzedawca,
			Kontekst.Baza.Kontrahenci.Where(kontrahent => !kontrahent.CzyArchiwalny && kontrahent.CzyPodmiot == CzySprzedaz).OrderBy(kontrahent => kontrahent.NIP).ToList,
			kontrahent => kontrahent.NIP,
			kontrahent => { if (UstawSprzedawce(Rekord, kontrahent)) kontroler.AktualizujKontrolki(); },
			Spisy.Kontrahenci);
		slownikSprzedawcaNazwa.Zainstaluj();

		slownikSprzedawcaNIP = new Slownik<Kontrahent>(
			Kontekst, comboBoxNazwaSprzedawcy, null,
			Kontekst.Baza.Kontrahenci.Where(kontrahent => !kontrahent.CzyArchiwalny && kontrahent.CzyPodmiot == CzySprzedaz).OrderBy(kontrahent => kontrahent.Nazwa).ToList,
			kontrahent => kontrahent.PelnaNazwaLubNazwa,
			kontrahent => { if (UstawSprzedawce(Rekord, kontrahent)) kontroler.AktualizujKontrolki(); },
			Spisy.Kontrahenci);
		slownikSprzedawcaNIP.Zainstaluj();
	}

	private bool UstawNabywce(Faktura rekord, Kontrahent? kontrahent)
	{
		if (kontrahent == null || rekord.NabywcaRef == kontrahent.Ref) return false;
		rekord.NabywcaRef = kontrahent;
		rekord.NIPNabywcy = kontrahent.NIP;
		rekord.NazwaNabywcy = kontrahent.PelnaNazwaLubNazwa;
		rekord.DaneNabywcy = kontrahent.AdresRejestrowy;
		rekord.CzyTP = kontrahent.CzyTP;
		if (rekord.CzySprzedaz)
		{
			if (!String.IsNullOrWhiteSpace(uwagiNabywcy)) rekord.UwagiPubliczne = (rekord.UwagiPubliczne ?? "").Replace(uwagiNabywcy, "").Trim('\r', '\n');
			if (!String.IsNullOrWhiteSpace(kontrahent.UwagiPubliczne))
			{
				uwagiNabywcy = kontrahent.UwagiPubliczne.Trim('\r', '\n');
				rekord.UwagiPubliczne = (rekord.UwagiPubliczne + "\r\n" + uwagiNabywcy).Trim('\r', '\n');
			}
		}
		if (kontrahent.SposobPlatnosciRef.IsNotNull) comboBoxSposobPlatnosci.SelectedValue = kontrahent.SposobPlatnosciRef;
		return true;
	}

	private bool UstawSprzedawce(Faktura rekord, Kontrahent? kontrahent)
	{
		if (kontrahent == null || rekord.SprzedawcaRef == kontrahent.Ref) return false;
		rekord.SprzedawcaRef = kontrahent;
		rekord.NIPSprzedawcy = kontrahent.NIP;
		rekord.NazwaSprzedawcy = kontrahent.PelnaNazwaLubNazwa;
		rekord.DaneSprzedawcy = kontrahent.AdresRejestrowy;
		rekord.RachunekBankowy = kontrahent.RachunekBankowy;
		rekord.NazwaBanku = kontrahent.NazwaBanku;
		if (rekord.CzySprzedaz)
		{
			if (!String.IsNullOrWhiteSpace(uwagiSprzedawcy)) rekord.UwagiPubliczne = (rekord.UwagiPubliczne ?? "").Replace(uwagiSprzedawcy, "").Trim('\r', '\n');
			if (!String.IsNullOrWhiteSpace(kontrahent.UwagiPubliczne))
			{
				uwagiSprzedawcy = kontrahent.UwagiPubliczne.Trim('\r', '\n');
				rekord.UwagiPubliczne = (uwagiSprzedawcy + "\r\n" + rekord.UwagiPubliczne).Trim('\r', '\n');
			}
		}
		if (kontrahent.SposobPlatnosciRef.IsNotNull) comboBoxSposobPlatnosci.SelectedValue = kontrahent.SposobPlatnosciRef;
		return true;
	}

	private void UstawDateWystawienia(Faktura rekord, DateTime dataWystawienia)
	{
		if (rekord.DataWystawienia == dataWystawienia) return;
		rekord.DataWystawienia = dataWystawienia;
		var sposobPlatnosci = Kontekst.Baza.Znajdz(Rekord.SposobPlatnosciRef);
		if (sposobPlatnosci == null) return;
		Rekord.TerminPlatnosci = Rekord.DataWystawienia.AddDays(sposobPlatnosci.LiczbaDni);
		kontroler.AktualizujKontrolki();
	}

	private bool UstawSposobPlatnosci(Faktura rekord, SposobPlatnosci? sposobPlatnosci)
	{
		if (sposobPlatnosci == null || rekord == null || rekord.SposobPlatnosciRef == sposobPlatnosci.Ref) return false;
		rekord.SposobPlatnosciRef = sposobPlatnosci;
		rekord.OpisSposobuPlatnosci = sposobPlatnosci.Nazwa;
		rekord.TerminPlatnosci = rekord.DataWystawienia.AddDays(sposobPlatnosci.LiczbaDni);
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
		linkLabelKSeFUrl.Text = Rekord.URLKSeF;

		var fakturaKorygowana = Rekord.FakturaKorygowanaRef.IsNull ? null : Kontekst.Baza.Znajdz(Rekord.FakturaKorygowanaRef);

		if (Rekord.Rodzaj == RodzajFaktury.Sprzedaż) labelRodzaj.Text = "Sprzedaż";
		else if (Rekord.Rodzaj == RodzajFaktury.Zakup) labelRodzaj.Text = "Zakup";
		else if (Rekord.Rodzaj == RodzajFaktury.KorektaSprzedaży) labelRodzaj.Text = "Korekta sprzedaży " + fakturaKorygowana?.Numer;
		else if (Rekord.Rodzaj == RodzajFaktury.KorektaZakupu) labelRodzaj.Text = "Korekta zakupu " + fakturaKorygowana?.Numer;
		else if (Rekord.Rodzaj == RodzajFaktury.Proforma) labelRodzaj.Text = "Proforma";
		else if (Rekord.Rodzaj == RodzajFaktury.DowódWewnętrzny) labelRodzaj.Text = "Dowód wewnętrzny";
		else if (Rekord.Rodzaj == RodzajFaktury.VatMarża) labelRodzaj.Text = "Vat marża";
		else if (Rekord.Rodzaj == RodzajFaktury.KorektaVatMarży) labelRodzaj.Text = "Korekta vat marży";
		else labelRodzaj.Text = Rekord.Rodzaj.ToString();

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

			buttonKSeFGeneruj.Enabled = false;
			textBoxKSeFXML.ReadOnly = true;
		}
	}

	private void buttonNowySprzedawca_Click(object? sender, EventArgs e)
	{
		var kontrahent = new Kontrahent { Nazwa = comboBoxNazwaSprzedawcy.Text, NIP = comboBoxNIPSprzedawcy.Text, AdresRejestrowy = textBoxDaneSprzedawcy.Text };
		if (!EdytorNowegoKontrahenta(kontrahent)) return;
		slownikSprzedawcaNazwa.Przeladuj();
		slownikSprzedawcaNIP.Przeladuj();
		UstawSprzedawce(Rekord, kontrahent);
		kontroler.AktualizujKontrolki();
	}

	private void buttonNowyNabywca_Click(object? sender, EventArgs e)
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
		using var okno = new Dialog("Nowy kontrahent", edytor, nowyKontekst);
		edytor.Przygotuj(nowyKontekst, kontrahent);
		if (okno.ShowDialog() != DialogResult.OK) return false;
		edytor.KoniecEdycji();
		nowyKontekst.Baza.Zapisz(kontrahent);
		transakcja.Zatwierdz();
		return true;
	}

	private void buttonKSeFGeneruj_Click(object? sender, EventArgs e)
	{
		if (String.IsNullOrEmpty(Rekord.Numer))
		{
			MessageBox.Show("Przed wygenerowaniem postaci ustrukturyzowanej należy zapisać fakturę w celu nadania jej numeru.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return;
		}
		if (ModifierKeys == Keys.Shift && !String.IsNullOrEmpty(Rekord.XMLKSeF))
		{
			using var api = new IO.KSEF2.API(SrodowiskoKSeF.Prod);
			var url = api.ZbudujUrl(Rekord.XMLKSeF, Rekord.NIPSprzedawcy, Rekord.DataWystawienia);
			Rekord.URLKSeF = url;
			linkLabelKSeFUrl.Text = url;
			return;
		}
		if (!String.IsNullOrWhiteSpace(Rekord.XMLKSeF) && MessageBox.Show("Faktura ma już wygenerowaną postać ustrukturyzowaną. Czy na pewno chcesz ją wygenerować ponownie?", "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
		Kontekst.Baza.Zapisz(Rekord);
		var xml = IO.FA_3.Generator.ZbudujXML(Kontekst.Baza, Rekord);
		Rekord.XMLKSeF = xml;
		kontroler.AktualizujKontrolki();
	}

	private void linkLabelUwagiPomoc_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
	{
		MessageBox.Show(@"Następujące frazy zostaną rozpoznane i automatycznie umieszczone w odpowiednich polach w momencie wysyłki do KSeF:

BDO: XXXXXXXXX
KRS: XXXXXXXXXX
REGON: XXXXXXXXX
Zamówienie: XXXX lub Numer zamówienia: XXXX
Data zamówienia: XXXX-XX-XX
Przyczyna korekty: XXXX
Mechanizm podzielonej płatności lub Split payment

Pozostałe elementy tekstowe zostaną przekazane jako dodatkowy opis.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}

	public override void KoniecEdycji()
	{
		Rekord.PoprawNumeracjePozycji(Kontekst.Baza);
		base.KoniecEdycji();
	}

	private void linkLabelKSeFUrl_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
	{
		if (String.IsNullOrWhiteSpace(Rekord.URLKSeF)) return;
		if (e.Button == MouseButtons.Right) Clipboard.SetText(Rekord.URLKSeF);
		else Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = Rekord.URLKSeF });
	}
}

class FakturaEdytorBase : Edytor<Faktura>
{
}
