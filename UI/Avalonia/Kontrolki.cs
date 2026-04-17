#if AVALONIA
using Avalonia.Layout;
using HorizontalAlignment = Avalonia.Layout.HorizontalAlignment;
using Label = Avalonia.Controls.Label;
using Button = Avalonia.Controls.Button;
using TextBox = Avalonia.Controls.TextBox;
using CheckBox = Avalonia.Controls.CheckBox;
using RadioButton = Avalonia.Controls.RadioButton;
using NumericUpDown = Avalonia.Controls.NumericUpDown;
using ComboBox = Avalonia.Controls.ComboBox;
using ListBox = Avalonia.Controls.ListBox;
using MenuItem = Avalonia.Controls.MenuItem;
using ContextMenu = Avalonia.Controls.ContextMenu;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Interactivity;
using System.Globalization;

namespace ProFak.UI;

class KontrolkiAV
{
	public static Label Label(string tekst)
	{
		var label = new Label();
		label.HorizontalContentAlignment = HorizontalAlignment.Right;
		label.VerticalContentAlignment = VerticalAlignment.Center;
		label.Content = tekst;
		return label;
	}

	public static HyperlinkButton Link(string tekst, Action? akcja = null)
	{
		var link = new HyperlinkButton();
		link.Content = tekst;
		if (akcja != null) link.Click += BezpiecznaAkcja(akcja).Invoke;
		return link;
	}

	public static TextBlock Text(string tekst, bool pogrubiony = false, int rozmiar = 9)
	{
		var text = new TextBlock();
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

	public static Button Button(string tekst, Action? akcja = null)
	{
		var button = new Button();
		button.Content = Wyglad.IkonyAkcji ? tekst : Wyglad.NazwaBezIkony(tekst);
		if (akcja != null) button.Click += BezpiecznaAkcja(akcja);
		return button;
	}

	public static Button ButtonSlownik(Action? akcja = null)
	{
		var button = Button("...", akcja: akcja);
		button.Width = 25;
		button.Height = 25;
		return button;
	}

	public static Button ButtonDodaj(Action akcja)
	{
		var button = Button(Wyglad.IkonyAkcji ? "➕" : "+", akcja: akcja);
		button.Width = 25;
		button.Height = 25;
		return button;
	}

	public static Button ButtonMenu(string tekst, MenuItem[] pozycje)
	{
		var menu = new ContextMenu();
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

	public static TextBox TextBox(bool haslo = false, string? podpowiedz = null, int szerokosc = 200, Action? zmienionaWartosc = null)
	{
		var textBox = new TextBox();
		textBox.Width = szerokosc;
		if (haslo) textBox.PasswordChar = '*';
		if (!String.IsNullOrEmpty(podpowiedz)) textBox.PlaceholderText = podpowiedz;
		if (zmienionaWartosc != null) textBox.TextChanged += BezpiecznaAkcja(zmienionaWartosc);
		return textBox;
	}

	public static TextBox TextArea(int linie = -1, Action? zmienionaWartosc = null)
	{
		var textBox = TextBox();
		textBox.AcceptsReturn = true;
		textBox.MinLines = Math.Max(2, linie);
		if (zmienionaWartosc != null) textBox.TextChanged += BezpiecznaAkcja(zmienionaWartosc);
		return textBox;
	}

	public static CheckBox CheckBox(string? etykieta = null, Action? zmienionaWartosc = null)
	{
		var checkBox = new CheckBox();
		if (!String.IsNullOrEmpty(etykieta)) checkBox.Content = etykieta;
		if (zmienionaWartosc != null) checkBox.IsCheckedChanged += BezpiecznaAkcja(zmienionaWartosc);
		return checkBox;
	}

	public static RadioButton RadioButton(string? etykieta = null)
	{
		var radioButton = new RadioButton();
		if (!String.IsNullOrEmpty(etykieta)) radioButton.Content = etykieta;
		return radioButton;
	}

	public static NumericUpDown NumericUpDown(int poPrzecinku = 2, int szerokosc = 100, Action? zmienionaWartosc = null)
	{
		var numericUpDown = new NumericUpDown();
		numericUpDown.NumberFormat = new NumberFormatInfo { NumberDecimalDigits = poPrzecinku };
		numericUpDown.TextAlignment = TextAlignment.Right;
		numericUpDown.Minimum = -999999999;
		numericUpDown.Maximum = 999999999;
		if (zmienionaWartosc != null) numericUpDown.ValueChanged += BezpiecznaAkcja(zmienionaWartosc);
		return numericUpDown;
	}

	public static DatePicker DatePicker(bool tylkoMiesiac = false, int szerokosc = 160)
	{
		var datePicker = new DatePicker();
		datePicker.DayVisible = !tylkoMiesiac;
		datePicker.DayFormat = "dd";
		datePicker.MonthFormat = "MM";
		datePicker.YearFormat = "yyyy";
		return datePicker;
	}

	public static ComboBox DropDownList(int szerokosc = 200, Action? zmienionaWartosc = null)
	{
		var comboBox = new ComboBox();
		comboBox.IsEditable = false;
		comboBox.Width = szerokosc;
		if (zmienionaWartosc != null) comboBox.SelectionChanged += BezpiecznaAkcja(zmienionaWartosc);
		return comboBox;
	}

	public static AutoCompleteBox SuggestBox(string[]? wartosci = null)
	{
		var autoCompleteBox = new AutoCompleteBox();
		autoCompleteBox.Width = 200;
		if (wartosci != null) autoCompleteBox.ItemsSource = wartosci;
		return autoCompleteBox;
	}

	public static MenuItem PopupMenuItem(string etykieta, Action akcja)
	{
		var pozycja = new MenuItem();
		pozycja.Header = etykieta;
		pozycja.Click += BezpiecznaAkcja(akcja);
		return pozycja;
	}

	public static ListBox ListBox(Action? zmienionaWartosc = null)
	{
		var listBox= new ListBox();
		if (zmienionaWartosc != null) listBox.SelectionChanged += BezpiecznaAkcja(zmienionaWartosc);
		return listBox;
	}
}
#endif