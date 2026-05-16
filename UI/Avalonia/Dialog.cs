#if AVALONIA
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace ProFak.UI;

class Dialog : Window, IDisposable
{
	protected override Type StyleKeyOverride => typeof(Window);

	protected Dialog(string tytul, Kontekst kontekst)
	{
		Wyglad.UstawCzcionke(this);
		SizeToContent = SizeToContent.WidthAndHeight;
		ShowInTaskbar = false;
		Icon = GlowneOkno.Ikona;
		Title = tytul;
		kontekst.Dialog = this;
		SetValue(XYFocus.NavigationModesProperty, XYFocusNavigationModes.Enabled);
	}

	public Dialog(string tytul, TControl zawartosc, Kontekst kontekst)
		: this(tytul, kontekst)
	{
		UstawZawartosc(zawartosc);
	}

	protected void UstawZawartosc(TControl zawartosc)
	{
		zawartosc.Margin = new TPadding(3);
		Content = zawartosc;
	}

	protected override void OnLoaded(RoutedEventArgs e)
	{
		if (!Double.IsNaN(Width)) MinWidth = Width;
		if (!Double.IsNaN(Height)) MinHeight = Height;
		base.OnLoaded(e);
		OknoGotowe();
	}

	protected override void OnKeyDown(Avalonia.Input.KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (e.Key == Key.Escape) Zamknij();
	}

	void IDisposable.Dispose()
	{
	}

	protected virtual void OknoGotowe()
	{
	}

	public void Pokaz()
	{
		Interfejs.Wyswietl(this);
	}

	public void Zamknij()
	{
		if (Dispatcher.CheckAccess()) Close();
		else Dispatcher.Post(Close);
	}

	public static void Pokaz(string tytul, TControl zawartosc, Kontekst kontekst)
	{
		using var dialog = new Dialog(tytul, zawartosc, kontekst);
		dialog.Pokaz();
	}
}
#endif