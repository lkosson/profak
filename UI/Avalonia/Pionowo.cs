using Avalonia.Controls;
using Control = Avalonia.Controls.Control;

namespace ProFak.UI;

class PionowoAV : StackPanel
{
	public PionowoAV()
	{
	}

	public PionowoAV(Control[] kontrolki)
		: this()
	{
		foreach (var kontrolka in kontrolki)
		{
			Children.Add(kontrolka);
		}
	}
}
