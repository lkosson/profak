#if AVALONIA
using Avalonia.Controls;

namespace ProFak.UI;

class PionowoAV : StackPanel
{
	public PionowoAV()
	{
	}

	public PionowoAV(TControl[] kontrolki)
		: this()
	{
		foreach (var kontrolka in kontrolki)
		{
			Children.Add(kontrolka);
		}
	}
}
#endif