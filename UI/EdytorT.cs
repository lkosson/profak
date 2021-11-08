using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	abstract class Edytor<T> : TableLayoutPanel, IEdytor<T>
		where T : Rekord<T>
	{
		private readonly BindingSource bindingSource;
		private readonly Container container;
		private int szerokoscEtykiet;
		private int szerokoscKontrolek;

		public T Rekord { get { return bindingSource.DataSource as T; } private set { bindingSource.DataSource = value; } }
		public Kontekst Kontekst { get; private set; }

		public Edytor()
		{
			container = new Container();
			bindingSource = new BindingSource(container);
			bindingSource.DataSource = typeof(T);

			ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
		}

		public void Przygotuj(Kontekst kontekst, T rekord)
		{
			Kontekst = kontekst;
			Rekord = rekord;
		}

		protected override void OnCreateControl()
		{
			RowCount++;
			RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			base.OnCreateControl();
		}

		public void DodajWiersz(Control kontrolka, string etykieta)
		{
			szerokoscKontrolek = Math.Max(szerokoscKontrolek, kontrolka.GetPreferredSize(default).Width);

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

		public void DodajTextBox(string pole, string etykieta, string format = "")
		{
			var textbox = new TextBox();
			textbox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			textbox.DataBindings.Add(new Binding("Text", bindingSource, pole, true, DataSourceUpdateMode.OnValidation, null, format));
			DodajWiersz(textbox, etykieta);
		}

		public void DodajCheckBox(string pole, string etykieta)
		{
			var checkbox = new CheckBox();
			checkbox.AutoSize = true;
			checkbox.Anchor = AnchorStyles.Left;
			checkbox.Text = etykieta;
			checkbox.DataBindings.Add(new Binding("Checked", bindingSource, pole, true, DataSourceUpdateMode.OnValidation, null, null));
			DodajWiersz(checkbox, null);
		}

		public void DodajNumericUpDown(string pole, string etykieta, string format = "", int poprzecinku = 2)
		{
			var nud = new NumericUpDown();
			nud.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			nud.TextAlign = HorizontalAlignment.Right;
			nud.DataBindings.Add(new Binding("Value", bindingSource, pole, true, DataSourceUpdateMode.OnPropertyChanged, null, format));
			nud.DecimalPlaces = poprzecinku;
			DodajWiersz(nud, etykieta);
		}

		public void DodajDatePicker(string pole, string etykieta)
		{
			var dateTimePicker = new DateTimePicker();
			dateTimePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			dateTimePicker.DataBindings.Add(new Binding("Text", bindingSource, pole, true, DataSourceUpdateMode.OnValidation));
			DodajWiersz(dateTimePicker, etykieta);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) container.Dispose();
			base.Dispose(disposing);
		}
	}
}
