using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI;

partial class KonfiguracjaSpisu : UserControl
{
	public KonfiguracjaSpisu()
	{
		InitializeComponent();
	}

	public KonfiguracjaSpisu(IEnumerable<KolumnaSpisu> konfiguracjaKolumn)
		: this()
	{
		listBoxKolumny.DataSource = konfiguracjaKolumn.Where(e => e.Kolejnosc >= 0).OrderBy(e => e.Kolejnosc).ToList();
	}

	private void listBoxKolumny_SelectedIndexChanged(object? sender, EventArgs e)
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		textBoxKolumna.Text = kolumna.Kolumna;
		numericUpDownKolejnosc.Value = kolumna.Kolejnosc;
		numericUpDownSzerokosc.Value = kolumna.Szerokosc;
		numericUpDownPoziomSortowania.Value = kolumna.PoziomSortowania;
		checkBoxUkryta.Checked = kolumna.Szerokosc == 0;
		checkBoxRozciagnij.Checked = kolumna.Szerokosc == -1;
	}

	private void numericUpDownSzerokosc_ValueChanged(object? sender, EventArgs e)
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		kolumna.Szerokosc = (int)numericUpDownSzerokosc.Value;
	}

	private void numericUpDownKolejnosc_ValueChanged(object? sender, EventArgs e)
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		kolumna.Kolejnosc = (int)numericUpDownKolejnosc.Value;
	}

	private void numericUpDownPoziomSortowania_ValueChanged(object? sender, EventArgs e)
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		kolumna.PoziomSortowania = (int)numericUpDownPoziomSortowania.Value;
	}

	private void checkBoxUkryta_CheckedChanged(object? sender, EventArgs e)
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		if (checkBoxUkryta.Checked)
		{
			checkBoxRozciagnij.Enabled = false;
			checkBoxRozciagnij.Checked = false;
			numericUpDownSzerokosc.Enabled = false;
			kolumna.Szerokosc = 0;
		}
		else
		{
			numericUpDownSzerokosc.Enabled = true;
			numericUpDownSzerokosc.Value = 100;
			kolumna.Szerokosc = 100;
		}
	}

	private void checkBoxRozciagnij_CheckedChanged(object? sender, EventArgs e)
	{
		if (listBoxKolumny.SelectedItem is not KolumnaSpisu kolumna) return;
		if (checkBoxRozciagnij.Checked)
		{
			checkBoxUkryta.Enabled = false;
			checkBoxUkryta.Checked = false;
			numericUpDownSzerokosc.Enabled = false;
			kolumna.Szerokosc = -1;
		}
		else
		{
			numericUpDownSzerokosc.Enabled = true;
			numericUpDownSzerokosc.Value = 100;
			kolumna.Szerokosc = 100;
		}
	}
}
