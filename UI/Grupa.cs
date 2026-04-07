namespace ProFak.UI;

class Grupa : GroupBox
{
	public Grupa(string opis, Control zawartosc)
	{
		Text = opis;
		Size = zawartosc.GetPreferredSize(default);
		Controls.Add(zawartosc);
		zawartosc.Dock = DockStyle.Fill;
	}
}
