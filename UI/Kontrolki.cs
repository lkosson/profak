using System;
using System.Collections.Generic;
using System.Text;

namespace ProFak.UI;

class Kontrolki
{
	public static Label Label(string tekst)
	{
		var label = new Label();
		label.Anchor = AnchorStyles.Right;
		label.TextAlign = ContentAlignment.MiddleRight;
		label.AutoSize = true;
		label.Text = tekst;
		return label;
	}

	public static LinkLabel Link(string tekst, Action? akcja = null)
	{
		var label = new LinkLabel();
		label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		label.TextAlign = ContentAlignment.MiddleLeft;
		label.AutoSize = true;
		label.Text = tekst;
		if (akcja != null) label.LinkClicked += BezpiecznaAkcja(akcja).Invoke;
		return label;
	}

	public static Label Text(string tekst)
	{
		var label = new Label();
		label.Anchor = AnchorStyles.Left;
		label.TextAlign = ContentAlignment.MiddleLeft;
		label.AutoSize = true;
		label.Text = tekst;
		return label;
	}

	public static LinkLabel LinkPomoc(string tresc)
	{
		void Pomoc() => MessageBox.Show(tresc, "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
		return Link("[?]", Pomoc);
	}

	public static Button Button(string tekst, Action? akcja = null)
	{
		var button = new ButtonDPI();
		button.Text = tekst;
		button.AutoSize = true;
		button.UseVisualStyleBackColor = true;
		button.Anchor = AnchorStyles.Left;
		button.PoprawWymiary();
		if (akcja != null) button.Click += BezpiecznaAkcja(akcja);
		return button;
	}

	public static Button ButtonMenu(string tekst, ToolStripMenuItem[] pozycje)
	{
		var menu = new ContextMenuStrip();
		menu.Items.AddRange(pozycje);
		var button = new ButtonDropDown();
		button.Text = tekst + "    ";
		button.AutoSize = true;
		button.UseVisualStyleBackColor = true;
		button.Anchor = AnchorStyles.Left;
		button.OpenMenuOnClick = true;
		button.Menu = menu;
		return button;
	}

	private static EventHandler BezpiecznaAkcja(Action akcja)
	{
		void Uruchom(object? sender, EventArgs e)
		{
			try
			{
				akcja();
			}
			catch (Exception exc)
			{
				OknoBledu.Pokaz(exc);
			}
		}

		return Uruchom;
	}

	public static TextBox TextBox(bool haslo = false)
	{
		var textBox = new TextBox();
		textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBox.Width = 200 * textBox.DeviceDpi / 96;
		if (haslo) textBox.UseSystemPasswordChar = true;
		return textBox;
	}

	public static TextBox TextArea(int linie = -1)
	{
		var textBox = TextBox();
		textBox.AcceptsReturn = true;
		textBox.Multiline = true;
		if (linie > 1) textBox.Height += (textBox.Height - 8) * (linie - 1);
		return textBox;
	}

	public static CheckBox CheckBox(string? etykieta = null)
	{
		var checkBox = new CheckBox();
		checkBox.AutoSize = true;
		checkBox.Anchor = AnchorStyles.Left;
		if (!String.IsNullOrEmpty(etykieta)) checkBox.Text = etykieta;
		return checkBox;
	}

	public static NumericUpDown NumericUpDown(int poPrzecinku = 2, int szerokosc = -1)
	{
		var numericUpDown = new NumericUpDownDPI();
		numericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		numericUpDown.TextAlign = HorizontalAlignment.Right;
		numericUpDown.DecimalPlaces = poPrzecinku;
		numericUpDown.Minimum = -999999999;
		numericUpDown.Maximum = 999999999;
		if (szerokosc > 0) numericUpDown.Width = szerokosc;
		return numericUpDown;
	}

	public static DateTimePicker DatePicker(bool tylkoMiesiac = false)
	{
		var dateTimePicker = new DateTimePickerFix();
		dateTimePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		dateTimePicker.Width = 200 * dateTimePicker.DeviceDpi / 96;
		dateTimePicker.CustomFormat = tylkoMiesiac ? "MM-yyyy" : Wyglad.FormatDaty;
		dateTimePicker.Format = DateTimePickerFormat.Custom;
		return dateTimePicker;
	}

	public static ComboBox DropDownList()
	{
		var comboBox = new ComboBoxFix();
		comboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBox.Width = 200 * comboBox.DeviceDpi / 96;
		return comboBox;
	}

	public static ComboBox SuggestBox(string[]? wartosci = null)
	{
		var comboBox = new ComboBoxFix();
		comboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBox.DropDownStyle = ComboBoxStyle.DropDown;
		comboBox.Width = 200 * comboBox.DeviceDpi / 96;
		if (wartosci != null) comboBox.Items.AddRange(wartosci);
		return comboBox;
	}

	public static ToolStripMenuItem PopupMenuItem(string etykieta, Action akcja)
	{
		var pozycja = new ToolStripMenuItem();
		pozycja.Text = etykieta;
		pozycja.Click += BezpiecznaAkcja(akcja);
		return pozycja;
	}
}
