namespace ProFak.UI;

class DialogWF : Form
{
	protected DialogWF(string tytul, Kontekst kontekst)
	{
		ShowInTaskbar = false;
		Icon = GlowneOkno.Ikona;
		KeyPreview = true;
		StartPosition = FormStartPosition.CenterParent;
		AutoValidate = AutoValidate.EnableAllowFocusChange;
		Text = tytul;
		kontekst.Dialog = this;
	}

	public DialogWF(string tytul, TControl zawartosc, Kontekst kontekst)
		: this(tytul, kontekst)
	{
		UstawZawartosc(zawartosc);
	}

	protected void UstawZawartosc(TControl zawartosc)
	{
		Controls.Add(zawartosc);
		ClientSize = zawartosc.Size;
		MinimumSize = Size;
		zawartosc.Dock = DockStyle.Fill;
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);
		OknoGotowe();
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (e.KeyCode == Keys.Escape)
		{
			DialogResult = DialogResult.Cancel;
			Zamknij();
		}
	}

	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		if (DialogResult == DialogResult.OK)
		{
			e.Cancel = !ValidateChildren();
		}
		base.OnFormClosing(e);
	}

	protected virtual void OknoGotowe()
	{
	}

	public void Pokaz()
	{
		ShowDialog();
	}

	public void Zamknij()
	{
		if (InvokeRequired) Invoke(Close);
		else Close();
	}

	public static void Pokaz(string tytul, Control zawartosc, Kontekst kontekst)
	{
		using var dialog = new DialogWF(tytul, zawartosc, kontekst);
		dialog.Pokaz();
	}
}
