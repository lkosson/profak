namespace ProFak.UI;

class Zakladki : TabControl
{
	public void Dodaj(string etykieta, Control zawartosc)
	{
		var wymiary = zawartosc.Size;
		var szerokosc = wymiary.Width;
		var wysokosc = wymiary.Height;
		var tabPage = new TabPage();
		tabPage.Text = etykieta;
		tabPage.Padding = new Padding(3);
		tabPage.UseVisualStyleBackColor = true;
		tabPage.Controls.Add(zawartosc);
		TabPages.Add(tabPage);
		zawartosc.Dock = DockStyle.Fill;
		//var zakladka = GetTabRect(TabPages.Count - 1);
		wysokosc += tabPage.Padding.Top + tabPage.Padding.Bottom;
		wysokosc += 31; //zakladka.Height;
		//if (szerokosc < zakladka.Width) szerokosc = zakladka.Width;
		if (Width < szerokosc) Width = szerokosc;
		if (Height < wysokosc) Height = wysokosc;
	}
}
