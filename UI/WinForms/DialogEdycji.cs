#if WINFORMS
namespace ProFak.UI;

class DialogEdycjiWF : DialogWF
{
	public DialogEdycjiWF(string tytul, TControl zawartosc, Kontekst kontekst)
		: base(tytul, kontekst)
	{
		var buttonZapisz = Kontrolki.Button("Zapisz [F10]");
		var buttonAnuluj = Kontrolki.Button("Anuluj [ESC]");
		buttonZapisz.DialogResult = DialogResult.OK;
		buttonAnuluj.DialogResult = DialogResult.Cancel;
		var uklad = new Siatka([0, 0, -1], [-1, 0]);
		uklad.DodajWiersz([(zawartosc, 3)]);
		uklad.DodajWiersz([buttonZapisz, buttonAnuluj]);
		AcceptButton = buttonZapisz;
		CancelButton = buttonAnuluj;
		UstawZawartosc(uklad);
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (e.KeyCode == Keys.F10)
		{
			e.SuppressKeyPress = true;
			DialogResult = DialogResult.OK;
			Zamknij();
		}
	}

	public static bool Pokaz(string tytul, Control zawartosc, Kontekst kontekst, bool pelnyEkran = false)
	{
		using var dialog = new DialogEdycjiWF(tytul, zawartosc, kontekst);
		if (pelnyEkran) dialog.WindowState = FormWindowState.Maximized;
		return dialog.ShowDialog() == DialogResult.OK;
	}
}
#endif