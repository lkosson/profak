namespace ProFak.UI;

class Poziomo : FlowLayoutPanel
{
	public Poziomo()
	{
		FlowDirection = FlowDirection.LeftToRight;
		WrapContents = false;
		AutoSize = true;
	}

	public Poziomo(Control[] kontrolki)
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
