namespace ProFak.UI;

class SiatkaWF : TableLayoutPanel
{
	public SiatkaWF(IEnumerable<int> szerokosci, IEnumerable<int> wysokosci)
	{
		foreach (var szerokosc in szerokosci)
		{
			if (szerokosc > 0) ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, szerokosc * DeviceDpi / 96));
			else if (szerokosc < 0) ColumnStyles.Add(new ColumnStyle(SizeType.Percent, -szerokosc));
			else ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
		}

		foreach (var wysokosc in wysokosci)
		{
			if (wysokosc > 0) RowStyles.Add(new RowStyle(SizeType.Absolute, wysokosc * DeviceDpi / 96));
			else if (wysokosc < 0) RowStyles.Add(new RowStyle(SizeType.Percent, -wysokosc));
			else RowStyles.Add(new RowStyle(SizeType.AutoSize));
		}

		ColumnCount = ColumnStyles.Count;
		Height = 0;
		Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
	}

	protected override void OnCreateControl()
	{
		var jestWypelnienie = false;
		foreach (RowStyle rowStyle in RowStyles)
		{
			if (rowStyle.SizeType == SizeType.Percent)
			{
				jestWypelnienie = true;
				break;
			}
		}

		if (!jestWypelnienie)
		{
			RowCount++;
			RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
		}
		base.OnCreateControl();
	}

	public void DodajWiersz(IEnumerable<(TControl? kontrolka, int kolumny)> kontrolki)
	{
		RowCount++;
		if (RowStyles.Count < RowCount) RowStyles.Add(new RowStyle());

		var kolumna = 0;
		var wiersz = RowCount - 1;

		foreach (var (kontrolka, kolumny) in kontrolki)
		{
			if (kontrolka == null)
			{
				kolumna += kolumny;
				continue;
			}

			Controls.Add(kontrolka, kolumna, wiersz);
			SetColumnSpan(kontrolka, kolumny);
			kolumna += kolumny;
			if (kontrolka is NumericUpDown or ComboBox) kontrolka.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			else kontrolka.Dock = DockStyle.Fill;
		}

		Size = GetPreferredSize(default);
	}

	public void DodajWiersz(IEnumerable<TControl?> kontrolki)
		=> DodajWiersz(kontrolki.Select(kontrolka => (kontrolka, 1)));

	public void DodajWiersz(string etykieta, IEnumerable<(TControl? kontrolka, int kolumny)> kontrolki)
		=> DodajWiersz(kontrolki.Prepend((TKontrolki.Label(etykieta), 1)));

	public void DodajWiersz(string etykieta, IEnumerable<TControl?> kontrolki)
		=> DodajWiersz(etykieta, kontrolki.Select(kontrolka => (kontrolka, 1)));

	public void DodajWiersz(IEnumerable<string?> etykiety)
		=> DodajWiersz(etykiety.Select(etykieta => (String.IsNullOrEmpty(etykieta) ? null : (TControl?)TKontrolki.Label(etykieta), 1)));
}
