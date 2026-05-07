#if AVALONIA

namespace ProFak.UI;

class GrupaAV : Avalonia.Controls.GroupBox
{
	protected override Type StyleKeyOverride => typeof(Avalonia.Controls.GroupBox);

	public GrupaAV(string opis, TControl zawartosc)
	{
		Header = opis;
		Content = zawartosc;
	}
}
#endif