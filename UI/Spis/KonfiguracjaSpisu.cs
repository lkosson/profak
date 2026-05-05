using ProFak.DB;
using System.Data;

namespace ProFak.UI;

partial class KonfiguracjaSpisu : Edytor
{
	private bool trwaZmianaWartosci;

	private readonly TListBox listBoxKolumny;
	private readonly TTextBox textBoxKolumna;
	private readonly TNumericUpDown numericUpDownSzerokosc;
	private readonly TNumericUpDown numericUpDownKolejnosc;
	private readonly TCheckBox checkBoxUkryta;
	private readonly TCheckBox checkBoxRozciagnij;
	private readonly TNumericUpDown numericUpDownPoziomSortowania;
	private readonly TCheckBox checkBoxPrzywroc;

	public bool CzyPrzywroc => checkBoxPrzywroc.Checked;

	public KonfiguracjaSpisu(IEnumerable<KolumnaSpisu> konfiguracjaKolumn)
	{
		listBoxKolumny = Kontrolki.ListBox(zmienionaWartosc: WybranaKolumna, konfiguracjaKolumn.Where(e => e.Kolejnosc >= 0).OrderBy(e => e.Kolejnosc).ToArray());
		textBoxKolumna = Kontrolki.TextBox();
		numericUpDownSzerokosc = Kontrolki.NumericUpDown(poPrzecinku: 0, zmienionaWartosc: ZmienionaSzerokosc);
		numericUpDownKolejnosc = Kontrolki.NumericUpDown(poPrzecinku: 0, zmienionaWartosc: ZmienionaKolejnosc);
		numericUpDownPoziomSortowania = Kontrolki.NumericUpDown(poPrzecinku: 0, zmienionaWartosc: ZmienioneSortowanie);
		checkBoxUkryta = Kontrolki.CheckBox("Ukryta", zmienionaWartosc: ZmienioneUkrycie);
		checkBoxRozciagnij = Kontrolki.CheckBox("Rozciągnij do pełnej szerokości", zmienionaWartosc: ZmienioneRozciagniecie);
		checkBoxPrzywroc = Kontrolki.CheckBox("Przywróć domyślne ustawienia spisu");

		textBoxKolumna.ReadOnly = true;

		var parametry = new DwieKolumny();
		parametry.DodajWiersz(textBoxKolumna, "Kolumna");
		parametry.DodajWiersz(numericUpDownSzerokosc, "Szerokość");
		parametry.DodajWiersz(numericUpDownKolejnosc, "Kolejność");
		parametry.DodajWiersz(numericUpDownPoziomSortowania, "Sortowanie");
		parametry.DodajWiersz(checkBoxUkryta);
		parametry.DodajWiersz(checkBoxRozciagnij);
		parametry.DodajWiersz(checkBoxPrzywroc);

		listBoxKolumny.Width = 200;
		var uklad = new Siatka([-1, 0], [-1]);
		uklad.DodajWiersz([listBoxKolumny, parametry]);

		UstawZawartosc(uklad);
	}

	private void ZmienWartosci(Action zmianaWartosci)
	{
		if (trwaZmianaWartosci) return;
		trwaZmianaWartosci = true;
		try
		{
			zmianaWartosci();
		}
		finally
		{
			trwaZmianaWartosci = false;
		}
	}

	private void UstawDostepnosc(KolumnaSpisu kolumna)
	{
		numericUpDownSzerokosc.Enabled = kolumna.Szerokosc > 0;
		checkBoxUkryta.Enabled = kolumna.Szerokosc != -1;
		checkBoxRozciagnij.Enabled = kolumna.Szerokosc != 0;
		if (checkBoxUkryta.Checked && !checkBoxUkryta.Enabled) checkBoxUkryta.Checked = false;
		if (checkBoxRozciagnij.Checked && !checkBoxRozciagnij.Enabled) checkBoxRozciagnij.Checked = false;
	}

	private void WybranaKolumna()
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		ZmienWartosci(delegate
		{
			textBoxKolumna.Text = kolumna.Kolumna;
			numericUpDownKolejnosc.Value = kolumna.Kolejnosc;
			numericUpDownSzerokosc.Value = kolumna.Szerokosc;
			numericUpDownPoziomSortowania.Value = kolumna.PoziomSortowania;
			checkBoxUkryta.Checked = kolumna.Szerokosc == 0;
			checkBoxRozciagnij.Checked = kolumna.Szerokosc == -1;
			UstawDostepnosc(kolumna);
		});
	}

	private void ZmienionaSzerokosc()
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		ZmienWartosci(delegate
		{
			kolumna.Szerokosc = (int)numericUpDownSzerokosc.Value;
		});
	}

	private void ZmienionaKolejnosc()
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		ZmienWartosci(delegate
		{
			kolumna.Kolejnosc = (int)numericUpDownKolejnosc.Value;
		});
	}

	private void ZmienioneSortowanie()
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		ZmienWartosci(delegate
		{
			kolumna.PoziomSortowania = (int)numericUpDownPoziomSortowania.Value;
		});
	}

	private void ZmienioneUkrycie()
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		ZmienWartosci(delegate
		{
			if (checkBoxUkryta.Checked)
			{
				kolumna.Szerokosc = 0;
			}
			else
			{
				numericUpDownSzerokosc.Value = 100;
				kolumna.Szerokosc = 100;
			}
			UstawDostepnosc(kolumna);
		});
	}

	private void ZmienioneRozciagniecie()
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		ZmienWartosci(delegate
		{
			if (checkBoxRozciagnij.Checked)
			{
				checkBoxUkryta.Checked = false;
				kolumna.Szerokosc = -1;
			}
			else
			{
				numericUpDownSzerokosc.Value = 100;
				kolumna.Szerokosc = 100;
			}
			UstawDostepnosc(kolumna);
		});
	}
}
