#if AVALONIA

namespace ProFak.UI;

class Grupa : Avalonia.Controls.GroupBox
{
	protected override Type StyleKeyOverride => typeof(Avalonia.Controls.GroupBox);

	public Grupa(string opis, TControl zawartosc)
	{
		Header = opis;
		Content = zawartosc;
	}
}
#endif