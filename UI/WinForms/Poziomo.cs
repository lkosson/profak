namespace ProFak.UI;

class PoziomoWF : FlowLayoutPanel
{
	public PoziomoWF()
	{
		FlowDirection = FlowDirection.LeftToRight;
		WrapContents = false;
		AutoSize = true;
		Margin = new Padding(0);
	}

	public PoziomoWF(TControl[] kontrolki)
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
