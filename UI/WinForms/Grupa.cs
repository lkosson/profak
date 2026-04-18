#if WINFORMS
namespace ProFak.UI;

class GrupaWF : GroupBox
{
	public GrupaWF(string opis, TControl zawartosc)
	{
		Text = opis;
		Height = zawartosc.Height + 23 * DeviceDpi / 96;
		Width = zawartosc.Width + 6 * DeviceDpi / 96;
		Controls.Add(zawartosc);
		zawartosc.Location = new Point(3 * DeviceDpi / 96, 19 * DeviceDpi / 96);
		zawartosc.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
	}
}
#endif
