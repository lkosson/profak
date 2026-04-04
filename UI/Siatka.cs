namespace ProFak.UI;

class Siatka : TableLayoutPanel
{
	public Siatka(IEnumerable<int> szerokosci, IEnumerable<int> wysokosci)
	{
		foreach (var szerokosc in szerokosci)
		{
			if (szerokosc > 0) ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, szerokosc));
			else if (szerokosc < 0) ColumnStyles.Add(new ColumnStyle(SizeType.Percent, -szerokosc));
			else ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
		}

		foreach (var wysokosc in wysokosci)
		{
			if (wysokosc > 0) RowStyles.Add(new RowStyle(SizeType.Absolute, wysokosc));
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

	public void DodajWiersz(IEnumerable<(Control? kontrolka, int kolumny)> kontrolki)
	{
		RowCount++;
		if (RowStyles.Count < RowCount) RowStyles.Add(new RowStyle());

		var kolumna = 0;
		var wiersz = RowCount - 1;
		var szerokoscKontrolek = 0;
		var wysokoscKontrolek = 0;

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
			kontrolka.Dock = DockStyle.Fill;

			var szerokosc = kontrolka.GetPreferredSize(default).Width;
			szerokoscKontrolek += Math.Max(Padding.Left, kontrolka.Margin.Left) + szerokosc + Math.Max(Padding.Right, kontrolka.Margin.Right);
			wysokoscKontrolek = Math.Max(wysokoscKontrolek, kontrolka.Height);
		}

		szerokoscKontrolek += Margin.Left + Margin.Right;
		if (Width < szerokoscKontrolek) Width = szerokoscKontrolek;
		Height += wysokoscKontrolek;
	}

	public void DodajWiersz(IEnumerable<Control?> kontrolki)
		=> DodajWiersz(kontrolki.Select(kontrolka => (kontrolka, 1)));

	public void DodajWiersz(string etykieta, IEnumerable<(Control? kontrolka, int kolumny)> kontrolki)
		=> DodajWiersz(kontrolki.Prepend((Kontrolki.Label(etykieta), 1)));

	public void DodajWiersz(string etykieta, IEnumerable<Control?> kontrolki)
		=> DodajWiersz(etykieta, kontrolki.Select(kontrolka => (kontrolka, 1)));

	public void DodajWiersz(IEnumerable<string?> etykiety)
		=> DodajWiersz(etykiety.Select(etykieta => (String.IsNullOrEmpty(etykieta) ? null : (Control?)Kontrolki.Label(etykieta), 1)));
}
