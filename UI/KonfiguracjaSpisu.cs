using ProFak.DB;
using System.Data;

namespace ProFak.UI;

partial class KonfiguracjaSpisu : UserControl
{
	private bool trwaZmianaWartosci;

	public KonfiguracjaSpisu()
	{
		InitializeComponent();
	}

	public KonfiguracjaSpisu(IEnumerable<KolumnaSpisu> konfiguracjaKolumn)
		: this()
	{
		listBoxKolumny.DataSource = konfiguracjaKolumn.Where(e => e.Kolejnosc >= 0).OrderBy(e => e.Kolejnosc).ToList();
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

	private void listBoxKolumny_SelectedIndexChanged(object? sender, EventArgs e)
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

	private void numericUpDownSzerokosc_ValueChanged(object? sender, EventArgs e)
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		ZmienWartosci(delegate
		{
			kolumna.Szerokosc = (int)numericUpDownSzerokosc.Value;
		});
	}

	private void numericUpDownKolejnosc_ValueChanged(object? sender, EventArgs e)
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		ZmienWartosci(delegate
		{
			kolumna.Kolejnosc = (int)numericUpDownKolejnosc.Value;
		});
	}

	private void numericUpDownPoziomSortowania_ValueChanged(object? sender, EventArgs e)
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		ZmienWartosci(delegate
		{
			kolumna.PoziomSortowania = (int)numericUpDownPoziomSortowania.Value;
		});
	}

	private void checkBoxUkryta_CheckedChanged(object? sender, EventArgs e)
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

	private void checkBoxRozciagnij_CheckedChanged(object? sender, EventArgs e)
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
