#if WINFORMS
global using TControl = System.Windows.Forms.Control;
global using TLabel = System.Windows.Forms.Label;
global using TLinkLabel = System.Windows.Forms.LinkLabel;
global using TText = System.Windows.Forms.Label;
global using TButton = ProFak.UI.ButtonDPI;
global using TTextBox = System.Windows.Forms.TextBox;
global using TTextArea = System.Windows.Forms.TextBox;
global using TCheckBox = System.Windows.Forms.CheckBox;
global using TRadioButton = System.Windows.Forms.RadioButton;
global using TNumericUpDown = ProFak.UI.NumericUpDownDPI;
global using TComboBox = ProFak.UI.ComboBoxFix;
global using TSuggestBox = ProFak.UI.ComboBoxFix;
global using TListBox = System.Windows.Forms.ListBox;
global using TMenuItem = System.Windows.Forms.ToolStripMenuItem;
global using TDatePicker = ProFak.UI.DateTimePickerFix;
global using TProgressBar = System.Windows.Forms.ProgressBar;
global using TContextMenu = System.Windows.Forms.ContextMenuStrip;

namespace ProFak.UI;

class KontrolkiWF
{
	public static TLabel Label(string tekst)
	{
		var label = new TLabel();
		label.Anchor = AnchorStyles.Right;
		label.TextAlign = ContentAlignment.MiddleRight;
		label.AutoSize = true;
		label.Text = tekst;
		return label;
	}

	public static TLinkLabel Link(string tekst, Action? akcja = null)
	{
		var label = new TLinkLabel();
		label.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		label.TextAlign = ContentAlignment.MiddleLeft;
		label.AutoSize = true;
		label.Text = tekst;
		if (akcja != null) label.LinkClicked += BezpiecznaAkcja(akcja).Invoke;
		return label;
	}

	public static TText Text(string tekst, bool pogrubiony = false, int rozmiar = 9)
	{
		var label = new TText();
		label.Anchor = AnchorStyles.Left;
		label.TextAlign = ContentAlignment.MiddleLeft;
		label.AutoSize = true;
		label.Text = tekst;
		label.Font = new Font(label.Font.FontFamily, rozmiar, pogrubiony ? FontStyle.Bold : FontStyle.Regular);
		return label;
	}

	public static TLinkLabel LinkPomoc(string tresc)
	{
		void Pomoc() => OknoKomunikatu.Informacja(tresc);
		return Link("[?]", Pomoc);
	}

	public static TButton Button(string tekst, Action? akcja = null)
	{
		var button = new TButton();
		button.Text = Wyglad.IkonyAkcji ? tekst : Wyglad.NazwaBezIkony(tekst);
		button.AutoSize = true;
		button.UseVisualStyleBackColor = true;
		button.Anchor = AnchorStyles.Left;
		if (akcja != null) button.Click += BezpiecznaAkcja(akcja);
		return button;
	}

	public static TButton ButtonSlownik(Action? akcja = null)
	{
		var button = Button("...", akcja: akcja);
		button.AutoSize = false;
		button.Width = 25 * button.DeviceDpi / 96;
		button.Height = 25 * button.DeviceDpi / 96;
		return button;
	}

	public static TButton ButtonDodaj(Action akcja)
	{
		var button = Button(Wyglad.IkonyAkcji ? "➕" : "+", akcja: akcja);
		button.AutoSize = false;
		button.Width = 25 * button.DeviceDpi / 96;
		button.Height = 25 * button.DeviceDpi / 96;
		return button;
	}

	public static TButton ButtonMenu(string tekst, ToolStripMenuItem[] pozycje)
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

	public static TTextBox TextBox(bool haslo = false, string? podpowiedz = null, int szerokosc = 200, Action? zmienionaWartosc = null)
	{
		var textBox = new TTextBox();
		textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBox.Width = szerokosc * textBox.DeviceDpi / 96;
		if (haslo) textBox.UseSystemPasswordChar = true;
		if (!String.IsNullOrEmpty(podpowiedz)) textBox.PlaceholderText = podpowiedz;
		if (zmienionaWartosc != null) textBox.TextChanged += BezpiecznaAkcja(zmienionaWartosc);
		return textBox;
	}

	public static TTextBox TextArea(int linie = -1, Action? zmienionaWartosc = null)
	{
		var textBox = TextBox();
		textBox.AcceptsReturn = true;
		textBox.Multiline = true;
		if (linie > 1) textBox.Height += (textBox.Height - 8 * textBox.DeviceDpi / 96) * textBox.DeviceDpi / 96 * (linie - 1);
		if (zmienionaWartosc != null) textBox.TextChanged += BezpiecznaAkcja(zmienionaWartosc);
		return textBox;
	}

	public static TCheckBox CheckBox(string? etykieta = null, Action? zmienionaWartosc = null)
	{
		var checkBox = new TCheckBox();
		checkBox.AutoSize = true;
		checkBox.Anchor = AnchorStyles.Left;
		checkBox.UseVisualStyleBackColor = true;
		if (!String.IsNullOrEmpty(etykieta)) checkBox.Text = etykieta;
		if (zmienionaWartosc != null) checkBox.CheckedChanged += BezpiecznaAkcja(zmienionaWartosc);
		return checkBox;
	}

	public static TRadioButton RadioButton(string? etykieta = null)
	{
		var radioButton = new TRadioButton();
		radioButton.AutoSize = true;
		radioButton.UseVisualStyleBackColor = true;
		if (!String.IsNullOrEmpty(etykieta)) radioButton.Text = etykieta;
		return radioButton;
	}

	public static TNumericUpDown NumericUpDown(int poPrzecinku = 2, int szerokosc = 100, Action? zmienionaWartosc = null)
	{
		var numericUpDown = new TNumericUpDown();
		numericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		numericUpDown.TextAlign = HorizontalAlignment.Right;
		numericUpDown.DecimalPlaces = poPrzecinku;
		numericUpDown.Minimum = -999999999;
		numericUpDown.Maximum = 999999999;
		numericUpDown.Width = szerokosc * numericUpDown.DeviceDpi / 96;
		if (zmienionaWartosc != null) numericUpDown.ValueChanged += BezpiecznaAkcja(zmienionaWartosc);
		return numericUpDown;
	}

	public static TDatePicker DatePicker(bool tylkoMiesiac = false, int szerokosc = 160)
	{
		var dateTimePicker = new TDatePicker();
		dateTimePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		dateTimePicker.Width = szerokosc * dateTimePicker.DeviceDpi / 96;
		dateTimePicker.CustomFormat = tylkoMiesiac ? "MM-yyyy" : Wyglad.FormatDaty;
		dateTimePicker.Format = DateTimePickerFormat.Custom;
		return dateTimePicker;
	}

	public static TComboBox DropDownList(int szerokosc = 200, Action? zmienionaWartosc = null)
	{
		var comboBox = new TComboBox();
		comboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBox.Width = szerokosc * comboBox.DeviceDpi / 96;
		if (zmienionaWartosc != null) comboBox.SelectedIndexChanged += BezpiecznaAkcja(zmienionaWartosc);
		return comboBox;
	}

	public static TSuggestBox SuggestBox(string[]? wartosci = null)
	{
		var comboBox = new TSuggestBox();
		comboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBox.DropDownStyle = ComboBoxStyle.DropDown;
		comboBox.Width = 200 * comboBox.DeviceDpi / 96;
		if (wartosci != null) comboBox.Items.AddRange(wartosci);
		return comboBox;
	}

	public static TMenuItem PopupMenuItem(string etykieta, Action akcja)
	{
		var pozycja = new TMenuItem();
		pozycja.Text = etykieta;
		pozycja.Click += BezpiecznaAkcja(akcja);
		return pozycja;
	}

	public static TListBox ListBox(Action? zmienionaWartosc = null)
	{
		var listBox= new TListBox();
		listBox.FormattingEnabled = true;
		listBox.ItemHeight = 15;
		if (zmienionaWartosc != null) listBox.SelectedIndexChanged += BezpiecznaAkcja(zmienionaWartosc);
		return listBox;
	}

	public static TProgressBar ProgressBar()
	{
		var progressBar = new TProgressBar();
		progressBar.Style = ProgressBarStyle.Marquee;
		return progressBar;
	}
}
#endif
