namespace ProFak.UI;

class Pionowo : TableLayoutPanel
{
	public Pionowo()
	{
		ColumnCount = 1;
		ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
	}

	public Pionowo(Control[] kontrolki)
		: this()
	{
		foreach (var kontrolka in kontrolki)
		{
			RowCount++;
			RowStyles.Add(new RowStyle(SizeType.AutoSize));
			Controls.Add(kontrolka);
			kontrolka.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		}
		RowCount++;
		RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
		Size = PreferredSize;
	}
}
