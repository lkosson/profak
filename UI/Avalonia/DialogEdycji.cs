#if AVALONIA
using Avalonia.Controls;
using Avalonia.Input;

namespace ProFak.UI;

class DialogEdycji : Dialog
{
	private TControl zawartosc;

	public bool Wynik { get; private set; }

	public DialogEdycji(string tytul, TControl zawartosc, Kontekst kontekst)
		: base(tytul, kontekst)
	{
		this.zawartosc = zawartosc;
		var buttonZapisz = Kontrolki.Button("Zapisz [F10]", akcja: Zapisz);
		var buttonAnuluj = Kontrolki.Button("Anuluj [ESC]", akcja: Zamknij);
		var uklad = new Siatka([0, 0, -1], [-1, 0]);
		uklad.DodajWiersz([(zawartosc, 3)]);
		uklad.DodajWiersz([buttonZapisz, buttonAnuluj]);
		UstawZawartosc(uklad);
	}

	private void Zapisz()
	{
		Wynik = true;
		Zamknij();
	}

	protected override void OnKeyDown(Avalonia.Input.KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (e.Key == Key.F10) Zapisz();
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
		if (zawartosc is not Edytor edytor) return true;
		return edytor.CzyModelPoprawny;
	}

	public static bool Pokaz(string tytul, TControl zawartosc, Kontekst kontekst, bool pelnyEkran = false)
	{
		using var dialog = new DialogEdycji(tytul, zawartosc, kontekst);
		if (pelnyEkran) dialog.WindowState = WindowState.Maximized;
		Interfejs.Wyswietl(dialog);
		return dialog.Wynik;
	}
}
#endif