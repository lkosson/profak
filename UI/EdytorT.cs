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

		public T Rekord { get { return bindingSource.DataSource as T; } set { bindingSource.DataSource = value; } }
		public Kontekst Kontekst { get; set; }

		public Edytor()
		{
			container = new Container();
			bindingSource = new BindingSource(container);
			bindingSource.DataSource = typeof(T);

			ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
		}

		public void DodajWiersz(Control kontrolka, string etykieta)
		{
			RowCount++;
			RowStyles.Add(new RowStyle());

			if (!String.IsNullOrEmpty(etykieta))
			{
				var label = new Label();
				label.Anchor = AnchorStyles.Right;
				label.AutoSize = true;
				label.Text = etykieta;
				Controls.Add(label, 0, RowCount - 1);
			}

			Controls.Add(kontrolka, 1, RowCount - 1);
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
			checkbox.Anchor = AnchorStyles.Left;
			checkbox.Text = etykieta;
			checkbox.DataBindings.Add(new Binding("Checked", bindingSource, pole, true, DataSourceUpdateMode.OnValidation, null, null));
			DodajWiersz(checkbox, null);
		}

		public void DodajNumericUpDown(string pole, string etykieta, string format = "")
		{
			var nud = new NumericUpDown();
			nud.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			nud.DataBindings.Add(new Binding("Value", bindingSource, pole, true, DataSourceUpdateMode.OnPropertyChanged, null, format));
			DodajWiersz(nud, etykieta);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) container.Dispose();
			base.Dispose(disposing);
		}
	}
}
