namespace ProFak.UI;

class Grupa : GroupBox
{
	public Grupa(string opis, Control zawartosc)
	{
		Text = opis;
		Controls.Add(zawartosc);
		zawartosc.Location = new Point(3, 19);
		Size = zawartosc.Size + new Size(3, 3);
		zawartosc.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
	}
}
