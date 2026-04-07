namespace ProFak.UI;

class Grupa : GroupBox
{
	public Grupa(string opis, Control zawartosc)
	{
		Text = opis;
		Height = zawartosc.Height + 21;
		Width = zawartosc.Width + 6;
		Controls.Add(zawartosc);
		zawartosc.Location = new Point(3, 19);
		zawartosc.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
	}
}
