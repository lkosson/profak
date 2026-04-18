#if WINFORMS
namespace ProFak.UI;

class PionowoWF : TableLayoutPanel
{
	public PionowoWF()
	{
		ColumnCount = 1;
		ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
	}

	public PionowoWF(TControl[] kontrolki)
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
#endif
