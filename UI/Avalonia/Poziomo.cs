#if AVALONIA
using Avalonia.Controls;
using Control = Avalonia.Controls.Control;

namespace ProFak.UI;

class PoziomoAV : WrapPanel
{
	public PoziomoAV()
	{
	}

	public PoziomoAV(Control[] kontrolki)
		: this()
	{
		foreach (var kontrolka in kontrolki)
		{
			Children.Add(kontrolka);
		}
	}
}
#endif