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
		var wysokosc = 0;
		var szerokosc = 0;
		foreach (var kontrolka in kontrolki)
		{
			RowCount++;
			RowStyles.Add(new RowStyle(SizeType.AutoSize));
			Controls.Add(kontrolka);
			kontrolka.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			wysokosc += kontrolka.Height + kontrolka.Margin.Top + kontrolka.Margin.Bottom;
			if (szerokosc < kontrolka.Width) szerokosc = kontrolka.Width;
		}
		RowCount++;
		RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
		Height = wysokosc;
		Width = szerokosc;
	}
}
