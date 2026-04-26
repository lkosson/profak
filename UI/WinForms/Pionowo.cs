#if WINFORMS
namespace ProFak.UI;

class PionowoWF : TableLayoutPanel
{
	private readonly bool wysrodkowane;

	public PionowoWF(TControl[] kontrolki, bool wysrodkowane = false)
	{
		this.wysrodkowane = wysrodkowane;
		ColumnCount = 1;
		RowCount = 1;
		ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
		RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

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
		kontrolka.Anchor = wysrodkowane ? AnchorStyles.Top : AnchorStyles.Left | AnchorStyles.Right;
		RowCount++;
	}

	public void OgraniczSzerokosc(int szerokosc)
	{
		Size = GetPreferredSize(new Size(szerokosc * DeviceDpi / 96, 0));
	}
}
#endif
