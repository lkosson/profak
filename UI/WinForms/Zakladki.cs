namespace ProFak.UI;

class ZakladkiWF : TabControl
{
	public TabPage Dodaj(string etykieta, TControl zawartosc)
	{
		if (Wyglad.SkrotyKlawiaturoweZakladek)
		{
			var num = (char)('₁' + TabPages.Count);
			etykieta += $"   [ᴄᴛʀʟ-ғ{num}]";
		}
		var wymiary = zawartosc.Size;
		var szerokosc = wymiary.Width + 14; // ?? Dla DeklaracjaVatEdytor i Konfiguracja przy 150%
		var wysokosc = wymiary.Height;
		var tabPage = new TabPage();
		tabPage.Text = etykieta;
		tabPage.Padding = new Padding(3);
		tabPage.UseVisualStyleBackColor = true;
		tabPage.Controls.Add(zawartosc);
		TabPages.Add(tabPage);
		zawartosc.Dock = DockStyle.Fill;
		wysokosc += tabPage.Padding.Top + tabPage.Padding.Bottom;
		wysokosc += 31 * DeviceDpi / 96; //zakladka.Height;
		if (Width < szerokosc) Width = szerokosc;
		if (Height < wysokosc) Height = wysokosc;
		return tabPage;
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();
		var form = FindForm();
		if (form == null) return;
		form.KeyDown += Form_KeyDown;
	}

	private void Form_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.Modifiers == Keys.Control && e.KeyCode >= Keys.F1 && e.KeyCode < (Keys.F1 + TabPages.Count))
		{
			var tabIndex = e.KeyCode - Keys.F1;
			var tab = TabPages[tabIndex];
			SelectedTab = tab;
			SelectNextControl(tab, true, true, true, true);
		}
	}
}
