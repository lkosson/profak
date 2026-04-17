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

	public static Label Text(string tekst, bool pogrubiony = false, int rozmiar = 9)
	{
		var label = new Label();
		label.Anchor = AnchorStyles.Left;
		label.TextAlign = ContentAlignment.MiddleLeft;
		label.AutoSize = true;
		label.Text = tekst;
		label.Font = new Font(label.Font.FontFamily, rozmiar, pogrubiony ? FontStyle.Bold : FontStyle.Regular);
		return label;
	}

	public static LinkLabel LinkPomoc(string tresc)
	{
		void Pomoc() => OknoKomunikatu.Informacja(tresc);
		return Link("[?]", Pomoc);
	}

	public static Button Button(string tekst, Action? akcja = null)
	{
		var button = new ButtonDPI();
		button.Text = Wyglad.IkonyAkcji ? tekst : Wyglad.NazwaBezIkony(tekst);
		button.AutoSize = true;
		button.UseVisualStyleBackColor = true;
		button.Anchor = AnchorStyles.Left;
		if (akcja != null) button.Click += BezpiecznaAkcja(akcja);
		return button;
	}

	public static Button ButtonSlownik(Action? akcja = null)
	{
		var button = Button("...", akcja: akcja);
		button.AutoSize = false;
		button.Width = 25 * button.DeviceDpi / 96;
		button.Height = 25; // tutaj bez DPI
		return button;
	}

	public static Button ButtonDodaj(Action akcja)
	{
		var button = Button(Wyglad.IkonyAkcji ? "➕" : "+", akcja: akcja);
		button.AutoSize = false;
		button.Width = 25 * button.DeviceDpi / 96;
		button.Height = 25; // tutaj bez DPI
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

	public static TextBox TextBox(bool haslo = false, string? podpowiedz = null, int szerokosc = 200, Action? zmienionaWartosc = null)
	{
		var textBox = new TextBox();
		textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBox.Width = szerokosc * textBox.DeviceDpi / 96;
		if (haslo) textBox.UseSystemPasswordChar = true;
		if (!String.IsNullOrEmpty(podpowiedz)) textBox.PlaceholderText = podpowiedz;
		if (zmienionaWartosc != null) textBox.TextChanged += BezpiecznaAkcja(zmienionaWartosc);
		return textBox;
	}

	public static TextBox TextArea(int linie = -1, Action? zmienionaWartosc = null)
	{
		var textBox = TextBox();
		textBox.AcceptsReturn = true;
		textBox.Multiline = true;
		if (linie > 1) textBox.Height += (textBox.Height - 8) * (linie - 1);
		if (zmienionaWartosc != null) textBox.TextChanged += BezpiecznaAkcja(zmienionaWartosc);
		return textBox;
	}

	public static CheckBox CheckBox(string? etykieta = null, Action? zmienionaWartosc = null)
	{
		var checkBox = new CheckBox();
		checkBox.AutoSize = true;
		checkBox.Anchor = AnchorStyles.Left;
		checkBox.UseVisualStyleBackColor = true;
		if (!String.IsNullOrEmpty(etykieta)) checkBox.Text = etykieta;
		if (zmienionaWartosc != null) checkBox.CheckedChanged += BezpiecznaAkcja(zmienionaWartosc);
		return checkBox;
	}

	public static RadioButton RadioButton(string? etykieta = null)
	{
		var radioButton = new RadioButton();
		radioButton.AutoSize = true;
		radioButton.UseVisualStyleBackColor = true;
		if (!String.IsNullOrEmpty(etykieta)) radioButton.Text = etykieta;
		return radioButton;
	}

	public static NumericUpDown NumericUpDown(int poPrzecinku = 2, int szerokosc = 100, Action? zmienionaWartosc = null)
	{
		var numericUpDown = new NumericUpDownDPI();
		numericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		numericUpDown.TextAlign = HorizontalAlignment.Right;
		numericUpDown.DecimalPlaces = poPrzecinku;
		numericUpDown.Minimum = -999999999;
		numericUpDown.Maximum = 999999999;
		numericUpDown.Width = szerokosc * numericUpDown.DeviceDpi / 96;
		if (zmienionaWartosc != null) numericUpDown.ValueChanged += BezpiecznaAkcja(zmienionaWartosc);
		return numericUpDown;
	}

	public static DateTimePicker DatePicker(bool tylkoMiesiac = false, int szerokosc = 160)
	{
		var dateTimePicker = new DateTimePickerFix();
		dateTimePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		dateTimePicker.Width = szerokosc * dateTimePicker.DeviceDpi / 96;
		dateTimePicker.CustomFormat = tylkoMiesiac ? "MM-yyyy" : Wyglad.FormatDaty;
		dateTimePicker.Format = DateTimePickerFormat.Custom;
		return dateTimePicker;
	}

	public static ComboBox DropDownList(int szerokosc = 200, Action? zmienionaWartosc = null)
	{
		var comboBox = new ComboBoxFix();
		comboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBox.Width = szerokosc * comboBox.DeviceDpi / 96;
		if (zmienionaWartosc != null) comboBox.SelectedIndexChanged += BezpiecznaAkcja(zmienionaWartosc);
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

	public static ListBox ListBox(Action? zmienionaWartosc = null)
	{
		var listBox= new ListBox();
		listBox.FormattingEnabled = true;
		listBox.ItemHeight = 15;
		if (zmienionaWartosc != null) listBox.SelectedIndexChanged += BezpiecznaAkcja(zmienionaWartosc);
		return listBox;
	}
}
