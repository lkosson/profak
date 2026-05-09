#if WINFORMS
namespace ProFak.UI;

class Poziomo : FlowLayoutPanel
{
	public Poziomo()
	{
		FlowDirection = FlowDirection.LeftToRight;
		WrapContents = false;
		AutoSize = true;
		Margin = new Padding(0);
	}

	public Poziomo(TControl[] kontrolki)
		: this()
	{
		foreach (var kontrolka in kontrolki)
		{
			Controls.Add(kontrolka);
			kontrolka.Anchor = AnchorStyles.Left;
		}

		Size = PreferredSize;
	}
}
#endif
