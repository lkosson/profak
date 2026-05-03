#if AVALONIA
using Avalonia.Controls;
using Panel = Avalonia.Controls.Panel;

namespace ProFak.UI;

partial class GlowneOkno : Window
{
	protected override Type StyleKeyOverride => typeof(Window);

	private Panel panelZawartosc;

	public GlowneOkno()
	{
		menu = new Menu(ZbudujMenu);

		panelZawartosc = new Panel();

		var uklad = new Siatka([Wyglad.SzerokoscMenu, -1], [-1]);
		uklad.DodajWiersz([menu, panelZawartosc]);

		WindowState = WindowState.Maximized;
		Title = "ProFak";
		//TODO Avalonia
		//Icon = Ikona;
		Content = uklad;
	}

	protected override void OnClosing(WindowClosingEventArgs e)
	{
		if (e.CloseReason == WindowCloseReason.WindowClosing && Wyglad.PotwierdzanieZamknieciaProgramu)
		{
			if (!OknoKomunikatu.PytanieTakNie("Czy na pewno chcesz zamknąć program?", domyslnie: false))
			{
				e.Cancel = true;
			}
		}
		base.OnClosing(e);
	}

	private void Wyswietl<TKontrolka>(TKontrolka kontrolka)
		where TKontrolka : TControl, IKontrolkaZKontekstem
	{
		var kontekst = new Kontekst();
		kontrolka.Kontekst = kontekst;
		kontrolka.DetachedFromVisualTree += delegate { menu.Focus(); kontekst.Dispose(); };
		panelZawartosc.Children.Clear();
		panelZawartosc.Children.Add(kontrolka);
		kontrolka.Focus();
	}

	public static void Pokaz()
	{
		AvaloniaUI.Wyswietl(new GlowneOkno());
	}
}
#endif