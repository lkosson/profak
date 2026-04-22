#if WINFORMS
namespace ProFak.UI;

class PionowoWF : TableLayoutPanel
{
	public PionowoWF()
	{
		ColumnCount = 1;
		RowCount = 1;
		ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
		RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
	}

	public PionowoWF(TControl[] kontrolki)
		: this()
	{
		SuspendLayout();
		foreach (var kontrolka in kontrolki)
		{
			DodajWiersz(kontrolka);
		}
		ResumeLayout();
		Size = PreferredSize;
	}

	public void DodajWiersz(TControl kontrolka)
	{
		RowStyles.Insert(RowCount - 1, new RowStyle(SizeType.AutoSize));
		Controls.Add(kontrolka, 0, RowCount - 1);
		kontrolka.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		RowCount++;
	}
}
#endif
