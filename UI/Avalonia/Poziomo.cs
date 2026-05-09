#if AVALONIA
using Avalonia.Controls;

namespace ProFak.UI;

class Poziomo : WrapPanel
{
	protected override Type StyleKeyOverride => typeof(WrapPanel);

	public Poziomo()
	{
	}

	public Poziomo(TControl[] kontrolki)
		: this()
	{
		foreach (var kontrolka in kontrolki)
		{
			Children.Add(kontrolka);
		}
	}
}
#endif