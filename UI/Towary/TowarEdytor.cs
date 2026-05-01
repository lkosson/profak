using ProFak.DB;
using System.Data;

namespace ProFak.UI;

class TowarEdytor : Edytor<Towar>
{
	private readonly ComboBox comboBoxStawkaVat;
	private readonly ComboBox comboBoxJednostkaMiary;
	private readonly Button buttonStawkaVat;
	private readonly Button buttonJednostkaMiary;
	private readonly NumericUpDown numericUpDownCenaNetto;
	private readonly NumericUpDown numericUpDownCenaBrutto;

	public TowarEdytor()
	{
		var textBoxNazwa = Kontrolki.TextBox();
		var comboBoxRodzaj = Kontrolki.DropDownList();
		var comboBoxSposobLiczenia = Kontrolki.DropDownList();
		comboBoxStawkaVat = Kontrolki.DropDownList();
		buttonStawkaVat = Kontrolki.ButtonSlownik();
		numericUpDownCenaNetto = Kontrolki.NumericUpDown();
		numericUpDownCenaBrutto = Kontrolki.NumericUpDown();
		comboBoxJednostkaMiary = Kontrolki.DropDownList();
		buttonJednostkaMiary = Kontrolki.ButtonSlownik();
		var comboBoxWidocznosc = Kontrolki.DropDownList();
		var comboBoxGTU = Kontrolki.DropDownList();
		var comboBoxStawkaRyczaltu = Kontrolki.DropDownList();

		kontroler.Slownik<RodzajTowaru>(comboBoxRodzaj);
		kontroler.Slownik(comboBoxSposobLiczenia, "według brutto", "według netto");
		kontroler.Slownik(comboBoxWidocznosc, "ukryty", "widoczny");
		kontroler.Slownik(comboBoxGTU, "-", "01 - Napoje alkoholowe", "02 - Paliwa", "03 - Oleje opałowe i smarowe", "04 - Wyroby tytoniowe", "05 - Odpady", "06 - Urządzenia elektroniczne", "07 - Pojazdy i części", "08 - Metale szlachetne i nieszlachetne", "09 - Leki i wyroby medyczne", "10 - Budynki, budowle, grunty", "11 - Uprawnienia do emisji", "12 - Usługi niematerialne", "13 - Usługi transportowe");
		kontroler.Slownik<decimal?>(comboBoxStawkaRyczaltu, null, 17m, 15m, 14m, 12.5m, 12m, 10m, 8.5m, 5.5m, 3m, 2m);

		kontroler.Powiazanie(textBoxNazwa, towar => towar.Nazwa);
		kontroler.Powiazanie(comboBoxRodzaj, towar => towar.Rodzaj);
		kontroler.Powiazanie(comboBoxSposobLiczenia, towar => towar.CzyWedlugCenBrutto, PrzeliczCeny);
		kontroler.Powiazanie(comboBoxStawkaVat, towar => towar.StawkaVatRef);
		kontroler.Powiazanie(numericUpDownCenaNetto, towar => towar.CenaNetto, PrzeliczCeny);
		kontroler.Powiazanie(numericUpDownCenaBrutto, towar => towar.CenaBrutto, PrzeliczCeny);
		kontroler.Powiazanie(comboBoxJednostkaMiary, towar => towar.JednostkaMiaryRef);
		kontroler.Powiazanie(comboBoxWidocznosc, towar => towar.CzyArchiwalny);
		kontroler.Powiazanie(comboBoxGTU, towar => towar.GTU);
		kontroler.Powiazanie(comboBoxStawkaRyczaltu, towar => towar.StawkaRyczaltu);

		Wymagane(textBoxNazwa);
		Wymagane(comboBoxStawkaVat);
		Dymek(buttonStawkaVat, "Wyświewtl pełną listę");
		Dymek(buttonJednostkaMiary, "Wyświewtl pełną listę");

		var siatka = new Siatka([0, -1, 0], []);
		siatka.DodajWiersz("Nazwa", [(textBoxNazwa, 2)]);
		siatka.DodajWiersz("Rodzaj", [(comboBoxRodzaj, 2)]);
		siatka.DodajWiersz("Sposób liczenia ceny", [(comboBoxSposobLiczenia, 2)]);
		siatka.DodajWiersz("Stawka VAT", [comboBoxStawkaVat, buttonStawkaVat]);
		siatka.DodajWiersz("Cena jednostkowa netto", [(numericUpDownCenaNetto, 2)]);
		siatka.DodajWiersz("Cena jednostkowa brutto", [(numericUpDownCenaBrutto, 2)]);
		siatka.DodajWiersz("Jednostka miary", [comboBoxJednostkaMiary, buttonJednostkaMiary]);
		siatka.DodajWiersz("Widoczność", [(comboBoxWidocznosc, 2)]);
		siatka.DodajWiersz("GTU", [(comboBoxGTU, 2)]);
		siatka.DodajWiersz("Stawka ryczałtu", [(comboBoxStawkaRyczaltu, 2)]);

		UstawZawartosc(siatka);
	}

	protected override void KontekstGotowy()
	{
		base.KontekstGotowy();

		new Slownik<JednostkaMiary>(
			Kontekst, comboBoxJednostkaMiary, buttonJednostkaMiary,
			Kontekst.Baza.JednostkiMiar.OrderBy(jednostka => jednostka.Skrot).ToList,
			jednostka => jednostka.Skrot,
			jednostka => { if (jednostka == null) return; Rekord.JednostkaMiary = jednostka; },
			Spisy.JednostkiMiar)
			.Zainstaluj();

		new Slownik<StawkaVat>(
			Kontekst, comboBoxStawkaVat, buttonStawkaVat,
			Kontekst.Baza.StawkiVat.OrderBy(stawka => stawka.Skrot).ToList,
			stawka => stawka.Skrot,
			stawka => { if (stawka == null) return; Rekord.StawkaVat = stawka; PrzeliczCeny(); },
			Spisy.StawkiVat)
			.Zainstaluj();
	}

	protected override void PrzygotujRekord(Towar rekord)
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
		else
		{
			rekord.StawkaVat = Kontekst.Baza.StawkiVat.Single(stawka => stawka.Id == rekord.StawkaVatId);
		}

		if (rekord.JednostkaMiaryRef.IsNull)
		{
			var domyslna = Kontekst.Baza.JednostkiMiar.OrderByDescending(jednostka => jednostka.CzyDomyslna).ThenBy(jednostka => jednostka.Id).FirstOrDefault();
			rekord.JednostkaMiary = domyslna;
			rekord.JednostkaMiaryRef = domyslna;
		}
		else
		{
			rekord.JednostkaMiary = Kontekst.Baza.JednostkiMiar.Single(jednostka => jednostka.Id == rekord.JednostkaMiaryId);
		}
	}

	protected override void RekordGotowy()
	{
		base.RekordGotowy();
		PrzeliczCeny();
	}

	private void PrzeliczCeny()
	{
		if (Rekord == null) return;
		ArgumentNullException.ThrowIfNull(Rekord.StawkaVat);

		numericUpDownCenaBrutto.Enabled = Rekord.CzyWedlugCenBrutto;
		numericUpDownCenaNetto.Enabled = !Rekord.CzyWedlugCenBrutto;

		if (Rekord.CzyWedlugCenBrutto)
		{
			var cenaNetto = (Rekord.CenaBrutto * 100m / (100 + Rekord.StawkaVat.Wartosc)).Zaokragl();
			if (Rekord.CenaNetto == cenaNetto) return; // Powodowało ustawienie Kontroler.modelZmieniony
			numericUpDownCenaNetto.Value = cenaNetto;
		}
		else
		{
			var cenaBrutto = (Rekord.CenaNetto * (100 + Rekord.StawkaVat.Wartosc) / 100m).Zaokragl();
			if (Rekord.CenaBrutto == cenaBrutto) return; // Powodowało ustawienie Kontroler.modelZmieniony
			numericUpDownCenaBrutto.Value = cenaBrutto;
		}
	}
}
