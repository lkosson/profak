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
		listBoxKolumny.DataSource = konfiguracjaKolumn.OrderBy(e => e.Kolejnosc).ToList();
	}

	private void listBoxKolumny_SelectedIndexChanged(object sender, EventArgs e)
	{
		var kolumna = (KolumnaSpisu)listBoxKolumny.SelectedItem;
		textBoxKolumna.Text = kolumna.Kolumna;
		numericUpDownKolejnosc.Value = kolumna.Kolejnosc;
		numericUpDownSzerokosc.Value = kolumna.Szerokosc;
		checkBoxUkryta.Checked = kolumna.Szerokosc == 0;
	}

	private void numericUpDownSzerokosc_ValueChanged(object sender, EventArgs e)
	{
		var kolumna = (KolumnaSpisu)listBoxKolumny.SelectedItem;
		if (kolumna == null) return;
		kolumna.Szerokosc = (int)numericUpDownSzerokosc.Value;
	}

	private void numericUpDownKolejnosc_ValueChanged(object sender, EventArgs e)
	{
		var kolumna = (KolumnaSpisu)listBoxKolumny.SelectedItem;
		if (kolumna == null) return;
		kolumna.Kolejnosc = (int)numericUpDownKolejnosc.Value;
	}

	private void checkBoxUkryta_CheckedChanged(object sender, EventArgs e)
	{
		var kolumna = (KolumnaSpisu)listBoxKolumny.SelectedItem;
		if (kolumna == null) return;
		if (checkBoxUkryta.Checked)
		{
			numericUpDownSzerokosc.Enabled = false;
			kolumna.Szerokosc = 0;
		}
		else
		{
			numericUpDownSzerokosc.Enabled = true;
			kolumna.Szerokosc = 100;
		}
	}
}
