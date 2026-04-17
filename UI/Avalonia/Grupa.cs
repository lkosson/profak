#if AVALONIA
using Avalonia.Controls;
using Control = Avalonia.Controls.Control;
using GroupBox = Avalonia.Controls.GroupBox;

namespace ProFak.UI;

class GrupaAV : GroupBox
{
	public GrupaAV(string opis, Control zawartosc)
	{
		Header = opis;
		Content = zawartosc;
	}
}
#endif