#if AVALONIA
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Control = Avalonia.Controls.Control;
using KeyEventArgs = Avalonia.Input.KeyEventArgs;

namespace ProFak.UI;

class DialogAV : Window, IDisposable
{
	protected DialogAV(string tytul, Kontekst kontekst)
	{
		SizeToContent = SizeToContent.WidthAndHeight;
		ShowInTaskbar = false;
		//Icon = GlowneOkno.Ikona;
		Title = tytul;
		//kontekst.Dialog = this;
	}

	public DialogAV(string tytul, Control zawartosc, Kontekst kontekst)
		: this(tytul, kontekst)
	{
		UstawZawartosc(zawartosc);
	}

	protected void UstawZawartosc(Control zawartosc)
	{
		Content = zawartosc;
	}

	protected override void OnLoaded(RoutedEventArgs e)
	{
		MinWidth = Width;
		MinHeight = Height;
		SizeToContent = SizeToContent.Manual;
		base.OnLoaded(e);
		OknoGotowe();
	}

	protected override void OnKeyDown(KeyEventArgs e)
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
		AvaloniaUI.Wyswietl(this);
	}

	public void Zamknij()
	{
		if (Dispatcher.CheckAccess()) Close();
		else Dispatcher.Post(Close);
	}
}
#endif