#if AVALONIA
using Avalonia.Controls;

namespace ProFak.UI;

class PoziomoAV : WrapPanel
{
	public PoziomoAV()
	{
	}

	public PoziomoAV(TControl[] kontrolki)
		: this()
	{
		foreach (var kontrolka in kontrolki)
		{
			Children.Add(kontrolka);
		}
	}
}
#endif