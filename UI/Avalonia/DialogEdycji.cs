#if AVALONIA
using Avalonia.Controls;
using Avalonia.Input;
using Control = Avalonia.Controls.Control;
using KeyEventArgs = Avalonia.Input.KeyEventArgs;

namespace ProFak.UI;

class DialogEdycjiAV : DialogAV
{
	public bool Wynik { get; private set; }

	public DialogEdycjiAV(string tytul, Control zawartosc, Kontekst kontekst)
		: base(tytul, kontekst)
	{
		var buttonZapisz = KontrolkiAV.Button("Zapisz [F10]", akcja: Zapisz);
		var buttonAnuluj = KontrolkiAV.Button("Anuluj [ESC]", akcja: Close);
		var uklad = new SiatkaAV([0, 0, -1], [-1, 0]);
		uklad.DodajWiersz([(zawartosc, 3)]);
		uklad.DodajWiersz([buttonZapisz, buttonAnuluj]);
		UstawZawartosc(uklad);
	}

	private void Zapisz()
	{
		Wynik = true;
		Close();
	}

	protected override void OnKeyDown(KeyEventArgs e)
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
		return true;
	}
}
#endif