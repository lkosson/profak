#if AVALONIA
global using TControl = Avalonia.Controls.Control;
global using TLabel = Avalonia.Controls.Label;
global using TLinkLabel = Avalonia.Controls.HyperlinkButton;
global using TText = Avalonia.Controls.TextBlock;
global using TButton = Avalonia.Controls.Button;
global using TTextBox = Avalonia.Controls.TextBox;
global using TTextArea = Avalonia.Controls.TextBox;
global using TCheckBox = Avalonia.Controls.CheckBox;
global using TRadioButton = Avalonia.Controls.RadioButton;
global using TNumericUpDown = Avalonia.Controls.NumericUpDown;
global using TComboBox = Avalonia.Controls.ComboBox;
global using TSuggestBox = Avalonia.Controls.AutoCompleteBox;
global using TListBox = Avalonia.Controls.ListBox;
global using TMenuItem = Avalonia.Controls.MenuItem;
global using TDatePicker = Avalonia.Controls.DatePicker;
global using TProgressBar = Avalonia.Controls.ProgressBar;
global using TContextMenu = Avalonia.Controls.ContextMenu;
using Avalonia.Layout;
using HorizontalAlignment = Avalonia.Layout.HorizontalAlignment;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Interactivity;
using System.Globalization;

namespace ProFak.UI;

class KontrolkiAV
{
	public static TLabel Label(string tekst)
	{
		var label = new TLabel();
		label.HorizontalContentAlignment = HorizontalAlignment.Right;
		label.VerticalContentAlignment = VerticalAlignment.Center;
		label.Content = tekst;
		return label;
	}

	public static TLinkLabel Link(string tekst, Action? akcja = null)
	{
		var link = new TLinkLabel();
		link.Content = tekst;
		if (akcja != null) link.Click += BezpiecznaAkcja(akcja).Invoke;
		return link;
	}

	public static TText Text(string tekst, bool pogrubiony = false, int rozmiar = 9)
	{
		var text = new TText();
		text.HorizontalAlignment = HorizontalAlignment.Left;
		text.VerticalAlignment = VerticalAlignment.Center;
		text.Text = tekst;
		text.FontSize = rozmiar * 12 / 9;
		text.FontWeight = pogrubiony ? FontWeight.Bold : FontWeight.Regular;
		return text;
	}

	public static HyperlinkButton LinkPomoc(string tresc)
	{
		void Pomoc() => MessageBox.Show(tresc, "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
		return Link("[?]", Pomoc);
	}

	public static TButton Button(string tekst, Action? akcja = null)
	{
		var button = new TButton();
		button.Content = Wyglad.IkonyAkcji ? tekst : Wyglad.NazwaBezIkony(tekst);
		if (akcja != null) button.Click += BezpiecznaAkcja(akcja);
		return button;
	}

	public static TButton ButtonSlownik(Action? akcja = null)
	{
		var button = Button("...", akcja: akcja);
		button.Width = 25;
		button.Height = 25;
		return button;
	}

	public static TButton ButtonDodaj(Action akcja)
	{
		var button = Button(Wyglad.IkonyAkcji ? "➕" : "+", akcja: akcja);
		button.Width = 25;
		button.Height = 25;
		return button;
	}

	public static TButton ButtonMenu(string tekst, TMenuItem[] pozycje)
	{
		var menu = new TContextMenu();
		menu.ItemsSource = pozycje;
		var button = new DropDownButton();
		button.Content = tekst;
		button.ContextMenu = menu;
		return button;
	}

	private static EventHandler<RoutedEventArgs> BezpiecznaAkcja(Action akcja)
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
		textBox.Width = szerokosc;
		if (haslo) textBox.PasswordChar = '*';
		if (!String.IsNullOrEmpty(podpowiedz)) textBox.PlaceholderText = podpowiedz;
		if (zmienionaWartosc != null) textBox.TextChanged += BezpiecznaAkcja(zmienionaWartosc);
		return textBox;
	}

	public static TTextArea TextArea(int linie = -1, Action? zmienionaWartosc = null)
	{
		var textBox = new TTextArea();
		textBox.AcceptsReturn = true;
		textBox.MinLines = Math.Max(2, linie);
		if (zmienionaWartosc != null) textBox.TextChanged += BezpiecznaAkcja(zmienionaWartosc);
		return textBox;
	}

	public static TCheckBox CheckBox(string? etykieta = null, Action? zmienionaWartosc = null)
	{
		var checkBox = new TCheckBox();
		if (!String.IsNullOrEmpty(etykieta)) checkBox.Content = etykieta;
		if (zmienionaWartosc != null) checkBox.IsCheckedChanged += BezpiecznaAkcja(zmienionaWartosc);
		return checkBox;
	}

	public static TRadioButton RadioButton(string? etykieta = null)
	{
		var radioButton = new TRadioButton();
		if (!String.IsNullOrEmpty(etykieta)) radioButton.Content = etykieta;
		return radioButton;
	}

	public static TNumericUpDown NumericUpDown(int poPrzecinku = 2, int szerokosc = 100, Action? zmienionaWartosc = null)
	{
		var numericUpDown = new TNumericUpDown();
		numericUpDown.NumberFormat = new NumberFormatInfo { NumberDecimalDigits = poPrzecinku };
		numericUpDown.TextAlignment = TextAlignment.Right;
		numericUpDown.Minimum = -999999999;
		numericUpDown.Maximum = 999999999;
		if (zmienionaWartosc != null) numericUpDown.ValueChanged += BezpiecznaAkcja(zmienionaWartosc);
		return numericUpDown;
	}

	public static TDatePicker DatePicker(bool tylkoMiesiac = false, int szerokosc = 160)
	{
		var datePicker = new TDatePicker();
		datePicker.DayVisible = !tylkoMiesiac;
		datePicker.DayFormat = "dd";
		datePicker.MonthFormat = "MM";
		datePicker.YearFormat = "yyyy";
		return datePicker;
	}

	public static TComboBox DropDownList(int szerokosc = 200, Action? zmienionaWartosc = null)
	{
		var comboBox = new TComboBox();
		comboBox.IsEditable = false;
		comboBox.Width = szerokosc;
		if (zmienionaWartosc != null) comboBox.SelectionChanged += BezpiecznaAkcja(zmienionaWartosc);
		return comboBox;
	}

	public static TSuggestBox SuggestBox(string[]? wartosci = null)
	{
		var autoCompleteBox = new AutoCompleteBox();
		autoCompleteBox.Width = 200;
		if (wartosci != null) autoCompleteBox.ItemsSource = wartosci;
		return autoCompleteBox;
	}

	public static TMenuItem MenuItem(string etykieta, Action akcja)
	{
		var pozycja = new TMenuItem();
		pozycja.Header = etykieta;
		pozycja.Click += BezpiecznaAkcja(akcja);
		return pozycja;
	}

	public static TListBox ListBox(Action? zmienionaWartosc = null)
	{
		var listBox= new TListBox();
		if (zmienionaWartosc != null) listBox.SelectionChanged += BezpiecznaAkcja(zmienionaWartosc);
		return listBox;
	}

	public static TProgressBar ProgressBar()
	{
		var progressBar = new TProgressBar();
		progressBar.IsIndeterminate = true;
		return progressBar;
	}

	public TTreeNode TreeNode(string tekst, TTreeNode[]? podrzedne = null)
	{
		var wezel = new TTreeNode();
		wezel.Text = tekst;
		if (podrzedne != null) wezel.Children.AddRange(podrzedne);
		return wezel;
	}
}

class TTreeNode
{
	public string Text { get; set; } = "";
	public List<TTreeNode> Children { get; set; } = [];
}

static class RozszerzeniaKontrolek
{
	extension(TControl kontrolka)
	{
		public bool Enabled { get => kontrolka.IsEnabled; set => kontrolka.IsEnabled = value; }
	}

	extension(TTextBox kontrolka)
	{
		public bool ReadOnly { get => kontrolka.IsReadOnly; set => kontrolka.IsReadOnly = value; }
	}
}
#endif