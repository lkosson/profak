namespace ProFak.UI;

class DwieKolumny : TableLayoutPanel
{
	private int szerokoscEtykiet;
	private int szerokoscKontrolek;

	public DwieKolumny()
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

	public void DodajWiersz(Control kontrolka, string? etykieta, bool pelnaSzerokosc = false)
	{
		szerokoscKontrolek = Math.Max(szerokoscKontrolek, kontrolka.Width);
		Height += kontrolka.Height + kontrolka.Margin.Top + kontrolka.Margin.Bottom;

		RowCount++;
		RowStyles.Add(new RowStyle());

		if (!String.IsNullOrEmpty(etykieta))
		{
			var label = Kontrolki.Label(etykieta);
			Controls.Add(label, 0, RowCount - 1);
			szerokoscEtykiet = Math.Max(szerokoscEtykiet, label.GetPreferredSize(default).Width);
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

		var minimalnaSzerokosc = szerokoscEtykiet + szerokoscKontrolek + Margin.Left + Margin.Right + Padding.Left + Padding.Right;
		if (Width < minimalnaSzerokosc) Width = minimalnaSzerokosc;
	}

	public TextBox DodajTextBox(string etykieta)
	{
		var textBox = Kontrolki.TextBox();
		DodajWiersz(textBox, etykieta);
		return textBox;
	}

	public TextBox DodajTextArea(string etykieta, int linie)
	{
		var textArea = Kontrolki.TextArea(linie);
		DodajWiersz(textArea, etykieta);
		return textArea;
	}

	public CheckBox DodajCheckBox(string etykieta, bool pelnaSzerokosc = false)
	{
		var checkBox = Kontrolki.CheckBox(etykieta);
		DodajWiersz(checkBox, null, pelnaSzerokosc);
		return checkBox;
	}

	public NumericUpDown DodajNumericUpDown(string etykieta, int poPrzecinku = 2)
	{
		var numericUpDown = Kontrolki.NumericUpDown(poPrzecinku);
		DodajWiersz(numericUpDown, etykieta);
		return numericUpDown;
	}

	public DateTimePicker DodajDatePicker(string etykieta)
	{
		var dateTimePicker = Kontrolki.DatePicker();
		DodajWiersz(dateTimePicker, etykieta);
		return dateTimePicker;
	}

	public ComboBox DodajDropDownList(string etykieta)
	{
		var comboBox = Kontrolki.DropDownList();
		comboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBox.Width = 200 * comboBox.DeviceDpi / 96;
		DodajWiersz(comboBox, etykieta);
		return comboBox;
	}
}
