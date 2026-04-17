#if AVALONIA
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Button = Avalonia.Controls.Button;
using Control = Avalonia.Controls.Control;
using KeyEventArgs = Avalonia.Input.KeyEventArgs;

namespace ProFak.UI;

class DialogAV : Window
{
	public bool? Wynik { get; private set; }

	public DialogAV(string tytul, Control zawartosc, Kontekst kontekst, bool przyciskiWidoczne = true)
	{
		if (przyciskiWidoczne)
		{
			var buttonZapisz = new Button { Content = "Zapisz [F10]" };
			buttonZapisz.Click += delegate { Zapisz(); };
			var buttonAnuluj = new Button { Content = "Anuluj [ESC]" };
			buttonAnuluj.Click += delegate { Anuluj(); };
			var wrapPanelPrzyciski = new WrapPanel();
			wrapPanelPrzyciski.Children.Add(buttonZapisz);
			wrapPanelPrzyciski.Children.Add(buttonAnuluj);
			var stackPanelZawartosc = new StackPanel();
			stackPanelZawartosc.Children.Add(zawartosc);
			stackPanelZawartosc.Children.Add(wrapPanelPrzyciski);
			Content = stackPanelZawartosc;
		}
		else
		{
			Content = zawartosc;
		}

		SizeToContent = SizeToContent.WidthAndHeight;
		ShowInTaskbar = false;

		//Icon = GlowneOkno.Ikona;
		//Wyglad.UsunSkrotPrzycisku(buttonZapisz);
		//Wyglad.UsunSkrotPrzycisku(buttonAnuluj);
		Title = tytul;
		//kontekst.Dialog = this;
	}

	protected override void OnLoaded(RoutedEventArgs e)
	{
		MinWidth = Width;
		MinHeight = Height;
		SizeToContent = SizeToContent.Manual;
		base.OnLoaded(e);
	}

	private void Zapisz()
	{
		Wynik = true;
		Close();
	}

	private void Anuluj()
	{
		Wynik = false;
		Close();
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (e.Key == Key.Escape) Anuluj();
		else if (e.Key == Key.F10) Zapisz();
	}

	protected override void OnClosing(WindowClosingEventArgs e)
	{
		if (Wynik is true)
		{
			e.Cancel = !SprawdzPoprawnosc();
		}
		base.OnClosing(e);
	}

	private bool SprawdzPoprawnosc()
	{
		return true;
	}
}
#endif