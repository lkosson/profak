namespace ProFak.UI;

class Grupa : GroupBox
{
	public Grupa(string opis, Control zawartosc)
	{
		Text = opis;
		Height = zawartosc.Height + 23 * DeviceDpi / 96;
		Width = zawartosc.Width + 6 * DeviceDpi / 96;
		Controls.Add(zawartosc);
		zawartosc.Location = new Point(3 * DeviceDpi / 96, 19 * DeviceDpi / 96);
		zawartosc.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
	}
}
