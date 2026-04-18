#if WINFORMS
namespace ProFak.UI;

class DwieKolumnyWF : TableLayoutPanel
{
	public DwieKolumnyWF()
	{
		ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
		ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
		Height = 0;
	}

	protected override void OnCreateControl()
	{
		RowCount++;
		RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
		base.OnCreateControl();
	}

	public void DodajWiersz(Control kontrolka, string? etykieta = null, bool pelnaSzerokosc = false)
	{
		RowCount++;
		RowStyles.Add(new RowStyle());

		if (!String.IsNullOrEmpty(etykieta))
		{
			var label = Kontrolki.Label(etykieta);
			Controls.Add(label, 0, RowCount - 1);
			//szerokoscEtykiet = Math.Max(szerokoscEtykiet, label.Width);
		}

		if (pelnaSzerokosc)
		{
			Controls.Add(kontrolka, 0, RowCount - 1);
			SetColumnSpan(kontrolka, 2);
		}
		else
		{
			Controls.Add(kontrolka, 1, RowCount - 1);
		}

		Size = GetPreferredSize(default);
	}
}
#endif