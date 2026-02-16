using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
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

		public void DodajWiersz(Control kontrolka, string? etykieta)
		{
			szerokoscKontrolek = Math.Max(szerokoscKontrolek, kontrolka.Width);
			Height += kontrolka.Height + kontrolka.Margin.Top + kontrolka.Margin.Bottom;

			RowCount++;
			RowStyles.Add(new RowStyle());

			if (!String.IsNullOrEmpty(etykieta))
			{
				var label = new Label();
				label.Anchor = AnchorStyles.Right;
				label.AutoSize = true;
				label.Text = etykieta;
				Controls.Add(label, 0, RowCount - 1);
				szerokoscEtykiet = Math.Max(szerokoscEtykiet, label.GetPreferredSize(default).Width);
			}

			Controls.Add(kontrolka, 1, RowCount - 1);

			var minimalnaSzerokosc = szerokoscEtykiet + szerokoscKontrolek + Margin.Left + Margin.Right + Padding.Left + Padding.Right;
			if (Width < minimalnaSzerokosc) Width = minimalnaSzerokosc;
		}

		public TextBox DodajTextBox(string etykieta, int linie = 1)
		{
			var textBox = new TextBox();
			textBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			textBox.Width = 200 * textBox.DeviceDpi / 96;
			if (linie > 1)
			{
				textBox.AcceptsReturn = true;
				textBox.Multiline = true;
				textBox.Height += (textBox.Height - 8) * (linie - 1);
			}
			DodajWiersz(textBox, etykieta);
			return textBox;
		}

		public CheckBox DodajCheckBox(string etykieta)
		{
			var checkBox = new CheckBox();
			checkBox.Anchor = AnchorStyles.Left;
			checkBox.Text = etykieta;
			checkBox.Size = checkBox.GetPreferredSize(default);
			DodajWiersz(checkBox, null);
			return checkBox;
		}

		public NumericUpDown DodajNumericUpDown(string etykieta, int poprzecinku = 2)
		{
			var numericUpDown = new NumericUpDown();
			numericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			numericUpDown.TextAlign = HorizontalAlignment.Right;
			numericUpDown.DecimalPlaces = poprzecinku;
			numericUpDown.Maximum = 999999999;
			DodajWiersz(numericUpDown, etykieta);
			return numericUpDown;
		}

		public DateTimePicker DodajDatePicker(string etykieta)
		{
			var dateTimePicker = new DateTimePicker();
			dateTimePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			dateTimePicker.Width = 200 * dateTimePicker.DeviceDpi / 96;
			dateTimePicker.CustomFormat = Format.Data;
			dateTimePicker.Format = DateTimePickerFormat.Custom;
			DodajWiersz(dateTimePicker, etykieta);
			return dateTimePicker;
		}

		public ComboBox DodajComboBox(string etykieta)
		{
			var comboBox = new ComboBox();
			comboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			comboBox.Width = 200 * comboBox.DeviceDpi / 96;
			DodajWiersz(comboBox, etykieta);
			return comboBox;
		}
	}
}
