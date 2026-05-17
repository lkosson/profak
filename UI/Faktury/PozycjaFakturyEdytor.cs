using ProFak.DB;
using System.Data;

namespace ProFak.UI;

partial class PozycjaFakturyEdytor : Edytor<PozycjaFaktury>
{
	private Slownik<Towar> slownikTowarow = default!;
	private bool trwaPrzeliczanieCen;

	private readonly TSuggestBox comboBoxTowar;
	private readonly TButton buttonTowar;
	private readonly TNumericUpDown numericUpDownIlosc;
	private readonly TNumericUpDown numericUpDownCenaNetto;
	private readonly TNumericUpDown numericUpDownCenaVat;
	private readonly TNumericUpDown numericUpDownCenaBrutto;
	private readonly TNumericUpDown numericUpDownWartoscNetto;
	private readonly TNumericUpDown numericUpDownWartoscVat;
	private readonly TNumericUpDown numericUpDownWartoscBrutto;
	private readonly TNumericUpDown numericUpDownCenaZakupu;
	private readonly TCheckBox checkBoxWedlugBrutto;
	private readonly TCheckBox checkBoxRecznie;
	private readonly TComboBox comboBoxStawkaVat;
	private readonly TButton buttonStawkaVat;
	private readonly TComboBox comboBoxJM;
	private readonly TNumericUpDown numericUpDownRabatProcent;
	private readonly TNumericUpDown numericUpDownRabatCena;
	private readonly TNumericUpDown numericUpDownRabatWartosc;
	private readonly TLabel labelCenaZakupu;

	public PozycjaFakturyEdytor()
	{
		var numericUpDownLP = Kontrolki.NumericUpDown(poPrzecinku: 0, szerokosc: 50);
		comboBoxTowar = Kontrolki.SuggestBox();
		buttonTowar = Kontrolki.ButtonSlownik();
		var buttonNowyTowar = Kontrolki.ButtonDodaj(NowyTowar);
		numericUpDownIlosc = Kontrolki.NumericUpDown(poPrzecinku: 4, szerokosc: 50);
		numericUpDownCenaNetto = Kontrolki.NumericUpDown(poPrzecinku: 2);
		numericUpDownCenaVat = Kontrolki.NumericUpDown(poPrzecinku: 2);
		numericUpDownCenaBrutto = Kontrolki.NumericUpDown(poPrzecinku: 2);
		numericUpDownWartoscNetto = Kontrolki.NumericUpDown(poPrzecinku: 2);
		numericUpDownWartoscVat = Kontrolki.NumericUpDown(poPrzecinku: 2);
		numericUpDownWartoscBrutto = Kontrolki.NumericUpDown(poPrzecinku: 2);
		numericUpDownCenaZakupu = Kontrolki.NumericUpDown(poPrzecinku: 2);
		checkBoxWedlugBrutto = Kontrolki.CheckBox("Według ceny brutto");
		checkBoxRecznie = Kontrolki.CheckBox("Ustaw kwoty ręcznie");
		comboBoxStawkaVat = Kontrolki.DropDownList();
		buttonStawkaVat = Kontrolki.ButtonSlownik();
		comboBoxJM = Kontrolki.DropDownList(szerokosc: 80);
		var comboBoxGTU = Kontrolki.DropDownList();
		var comboBoxStawkaRyczaltu = Kontrolki.DropDownList();
		numericUpDownRabatProcent = Kontrolki.NumericUpDown(poPrzecinku: 2, szerokosc: 90);
		numericUpDownRabatCena = Kontrolki.NumericUpDown(poPrzecinku: 2, szerokosc: 90);
		numericUpDownRabatWartosc = Kontrolki.NumericUpDown(poPrzecinku: 2, szerokosc: 90);

		kontroler.Slownik<decimal?>(comboBoxStawkaRyczaltu, null, 17m, 15m, 14m, 12.5m, 12m, 10m, 8.5m, 5.5m, 3m, 2m);
		kontroler.Slownik(comboBoxGTU, "-", "01 - Napoje alkoholowe", "02 - Paliwa", "03 - Oleje opałowe i smarowe", "04 - Wyroby tytoniowe", "05 - Odpady", "06 - Urządzenia elektroniczne", "07 - Pojazdy i części", "08 - Metale szlachetne i nieszlachetne", "09 - Leki i wyroby medyczne", "10 - Budynki, budowle, grunty", "11 - Uprawnienia do emisji", "12 - Usługi niematerialne", "13 - Usługi transportowe");

		kontroler.Powiazanie(numericUpDownLP, pozycja => pozycja.LP);
		kontroler.Powiazanie(comboBoxTowar, pozycja => pozycja.Opis);
		kontroler.Powiazanie(numericUpDownIlosc, pozycja => pozycja.Ilosc, PrzeliczCeny);
		kontroler.Powiazanie(numericUpDownCenaNetto, pozycja => pozycja.CenaNetto, PrzeliczCeny);
		kontroler.Powiazanie(numericUpDownCenaVat, pozycja => pozycja.CenaVat, PrzeliczCeny);
		kontroler.Powiazanie(numericUpDownCenaBrutto, pozycja => pozycja.CenaBrutto, PrzeliczCeny);
		kontroler.Powiazanie(numericUpDownWartoscNetto, pozycja => pozycja.WartoscNetto, PrzeliczCeny);
		kontroler.Powiazanie(numericUpDownWartoscVat, pozycja => pozycja.WartoscVat, PrzeliczCeny);
		kontroler.Powiazanie(numericUpDownWartoscBrutto, pozycja => pozycja.WartoscBrutto, PrzeliczCeny);
		kontroler.Powiazanie(numericUpDownCenaZakupu, pozycja => pozycja.CenaZakupuDlaMarzy, PrzeliczCeny);
		kontroler.Powiazanie(checkBoxWedlugBrutto, pozycja => pozycja.CzyWedlugCenBrutto, KonfigurujCeny);
		kontroler.Powiazanie(checkBoxRecznie, pozycja => pozycja.CzyWartosciReczne, KonfigurujCeny);
		kontroler.Powiazanie(comboBoxStawkaVat, pozycja => pozycja.StawkaVatRef);
		kontroler.Powiazanie(comboBoxJM, pozycja => pozycja.JednostkaMiaryRef);
		kontroler.Powiazanie(comboBoxGTU, pozycja => pozycja.GTU);
		kontroler.Powiazanie(comboBoxStawkaRyczaltu, pozycja => pozycja.StawkaRyczaltu);
		kontroler.Powiazanie(numericUpDownRabatProcent, pozycja => pozycja.RabatProcent, PrzeliczCeny);
		kontroler.Powiazanie(numericUpDownRabatCena, pozycja => pozycja.RabatCena, PrzeliczCeny);
		kontroler.Powiazanie(numericUpDownRabatWartosc, pozycja => pozycja.RabatWartosc, PrzeliczCeny);

		Wymagane(comboBoxTowar);
		Wymagane(comboBoxStawkaVat);
		Wymagane(comboBoxJM);
		Dymek(buttonNowyTowar, "Dodaj do słownika towarów");
		Dymek(buttonTowar, "Wyświetl pełną listę");
		Dymek(buttonStawkaVat, "Wyświetl pełną listę");

		var naglowek = new Siatka([0, 0, 20, 0, -1, 0, 0, 20, 0, 0], []);
		naglowek.DodajWiersz([Kontrolki.Label("LP"), numericUpDownLP, null, Kontrolki.Label("Towar/opis"), comboBoxTowar, buttonTowar, buttonNowyTowar, null, numericUpDownIlosc, comboBoxJM]);
		numericUpDownLP.TabIndex = 999;
		numericUpDownIlosc.TabIndex = comboBoxJM.TabIndex + 1;

		var ceny = new DwieKolumny();
		ceny.DodajWiersz(numericUpDownCenaNetto, "Netto");
		ceny.DodajWiersz(numericUpDownCenaVat, "Vat");
		ceny.DodajWiersz(numericUpDownCenaBrutto, "Brutto");
		ceny.DodajWiersz(checkBoxWedlugBrutto, null, pelnaSzerokosc: true);

		var wartosci = new DwieKolumny();
		wartosci.DodajWiersz(numericUpDownWartoscNetto, "Netto");
		wartosci.DodajWiersz(numericUpDownWartoscVat, "Vat");
		wartosci.DodajWiersz(numericUpDownWartoscBrutto, "Brutto");
		wartosci.DodajWiersz(checkBoxRecznie, null, pelnaSzerokosc: true);

		var rabaty = new DwieKolumny();
		rabaty.DodajWiersz(numericUpDownRabatProcent, "Procentowy");
		rabaty.DodajWiersz(numericUpDownRabatCena, "Od ceny jedn.");
		rabaty.DodajWiersz(numericUpDownRabatWartosc, "Od wartości");

		comboBoxStawkaVat.Width = 60;
		comboBoxGTU.Width = 60;
		comboBoxStawkaRyczaltu.Width = 60;

		var podatki = new Siatka([0, -1, 0], []);
		podatki.DodajWiersz("Vat", [comboBoxStawkaVat, buttonStawkaVat]);
		podatki.DodajWiersz("GTU", [(comboBoxGTU, 2)]);
		podatki.DodajWiersz("Ryczałt", [(comboBoxStawkaRyczaltu, 2)]);
		podatki.DodajWiersz([(labelCenaZakupu = Kontrolki.Label("Cena zakupu"), 1), (numericUpDownCenaZakupu, 2)]);

		var uklad = new Siatka([0, 0, 0, 0, -1], [0, 0]);
		uklad.DodajWiersz([(naglowek, 5)]);
		uklad.DodajWiersz([
			new Grupa("Cena jednostkowa", ceny),
			new Grupa("Łączna wartość", wartosci),
			new Grupa("Rabat", rabaty),
			new Grupa("Podatki", podatki),
			]);

		KontrolkaStartowa(comboBoxTowar);

		UstawZawartosc(uklad);
	}

	protected override void KontekstGotowy()
	{
		base.KontekstGotowy();

		slownikTowarow = new Slownik<Towar>(
			Kontekst, comboBoxTowar, buttonTowar,
			Kontekst.Baza.Towary.OrderBy(towar => towar.Nazwa).ToList,
			towar => towar.Nazwa,
			UstawTowar,
			Spisy.Towary);
		slownikTowarow.Zainstaluj();

		new Slownik<StawkaVat>(
			Kontekst, comboBoxStawkaVat, buttonStawkaVat,
			Kontekst.Baza.StawkiVat.OrderBy(stawka => stawka.Skrot).ToList,
			stawka => stawka.Skrot,
			stawka => { PrzeliczCeny(); },
			Spisy.StawkiVat)
			.Zainstaluj();

		new Slownik<JednostkaMiary>(
			Kontekst, comboBoxJM, null,
			Kontekst.Baza.JednostkiMiar.OrderBy(jm => jm.CzyDomyslna ? 0 : 1).ThenBy(jm => jm.Nazwa).ToList,
			jm => jm.Skrot,
			jm => { KonfigurujPoleIlosci(); },
			Spisy.JednostkiMiar)
			.Zainstaluj();

		var vatMarza = Kontekst.Znajdz<Faktura>() is Faktura faktura && (faktura.Rodzaj == RodzajFaktury.VatMarża || faktura.Rodzaj == RodzajFaktury.KorektaVatMarży || faktura.ProceduraMarzy != ProceduraMarży.NieDotyczy);

		numericUpDownCenaZakupu.Visible = vatMarza;
		labelCenaZakupu.Visible = vatMarza;
		checkBoxWedlugBrutto.Enabled = !vatMarza;
		checkBoxRecznie.Enabled = !vatMarza;
		numericUpDownRabatCena.Enabled = !vatMarza;
		numericUpDownRabatProcent.Enabled = !vatMarza;
		numericUpDownRabatWartosc.Enabled = !vatMarza;
	}

	protected override void PrzygotujRekord(PozycjaFaktury rekord)
	{
		base.PrzygotujRekord(rekord);
		if (rekord.StawkaVatRef.IsNull)
		{
			var domyslna = Kontekst.Baza.StawkiVat.OrderByDescending(stawka => stawka.CzyDomyslna).ThenBy(stawka => stawka.Id).FirstOrDefault();
			if (domyslna != null)
			{
				rekord.StawkaVat = domyslna;
				rekord.StawkaVatRef = domyslna;
			}
		}
		if (rekord.JednostkaMiaryRef.IsNull)
		{
			var towar = Kontekst.Baza.ZnajdzLubNull(rekord.TowarRef);
			if (towar != null && towar.JednostkaMiaryRef.IsNotNull)
			{
				rekord.JednostkaMiaryRef = towar.JednostkaMiaryRef;
			}
			else
			{
				rekord.JednostkaMiaryRef = Kontekst.Baza.JednostkiMiar.OrderByDescending(jm => jm.CzyDomyslna).ThenBy(jm => jm.Id).FirstOrDefault();
			}
		}
	}

	protected override void RekordGotowy()
	{
		base.RekordGotowy();
		KonfigurujPoleIlosci();
		KonfigurujCeny();
	}

	private void KonfigurujPoleIlosci()
	{
		var jm = Kontekst.Baza.ZnajdzLubNull(Rekord.JednostkaMiaryRef);
		if (jm == null)
		{
			numericUpDownIlosc.DecimalPlaces = 0;
		}
		else
		{

			numericUpDownIlosc.DecimalPlaces = jm.LiczbaMiescPoPrzecinku;
		}
	}

	private void KonfigurujCeny()
	{
		numericUpDownCenaNetto.Enabled = Rekord.CzyWartosciReczne || !Rekord.CzyWedlugCenBrutto;
		numericUpDownCenaVat.Enabled = Rekord.CzyWartosciReczne;
		numericUpDownCenaBrutto.Enabled = Rekord.CzyWartosciReczne || Rekord.CzyWedlugCenBrutto;
		numericUpDownWartoscNetto.Enabled = Rekord.CzyWartosciReczne;
		numericUpDownWartoscVat.Enabled = Rekord.CzyWartosciReczne;
		numericUpDownWartoscBrutto.Enabled = Rekord.CzyWartosciReczne;
	}

	private void PrzeliczCeny()
	{
		if (trwaPrzeliczanieCen) return;
		try
		{
			trwaPrzeliczanieCen = true;
			Rekord.PrzeliczCeny(Kontekst.Baza);
			kontroler.AktualizujKontrolki();
		}
		finally
		{
			trwaPrzeliczanieCen = false;
		}
	}

	private void UstawTowar(Towar? towar)
	{
		if (towar == null || Rekord.TowarRef == towar.Ref) return;
		Rekord.UstawTowar(towar);
		KonfigurujPoleIlosci();
		KonfigurujCeny();
		PrzeliczCeny();
	}

	private void NowyTowar()
	{
		var towar = new Towar
		{
			Nazwa = comboBoxTowar.Text ?? "",
			StawkaVatRef = Rekord.StawkaVatRef,
			CenaNetto = Rekord.CenaNetto,
			CenaBrutto = Rekord.CenaBrutto,
			GTU = Rekord.GTU,
			SposobLiczeniaCeny = Rekord.CzyWedlugCenBrutto ? SposobLiczeniaCenyTowaru.WedługBrutto : SposobLiczeniaCenyTowaru.WedługNetto,
			JednostkaMiaryRef = Rekord.JednostkaMiaryRef,
			StawkaRyczaltu = Rekord.StawkaRyczaltu
		};
		using var nowyKontekst = new Kontekst(Kontekst);
		using var transakcja = nowyKontekst.Transakcja();
		nowyKontekst.Dodaj(towar);
		nowyKontekst.Baza.Zapisz(towar);
		using var edytor = new TowarEdytor();
		edytor.Przygotuj(nowyKontekst, towar);
		if (!DialogEdycji.Pokaz("Nowy towar", edytor, nowyKontekst)) return;
		edytor.KoniecEdycji();
		nowyKontekst.Baza.Zapisz(towar);
		transakcja.Zatwierdz();
		slownikTowarow.Przeladuj();
		UstawTowar(towar);
	}
}
