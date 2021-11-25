using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	abstract class Edytor<TRekord> : TableLayoutPanel, IEdytor<TRekord>
		where TRekord : Rekord<TRekord>
	{
		private readonly Kontroler<TRekord> kontroler;
		private readonly Container container;
		private readonly ErrorProvider errorProvider;
		private int szerokoscEtykiet;
		private int szerokoscKontrolek;

		public TRekord Rekord { get => kontroler.Model; set => kontroler.Model = value; }
		public Kontekst Kontekst { get; private set; }

		public Edytor()
		{
			container = new Container();
			kontroler = new Kontroler<TRekord>();
			errorProvider = new ErrorProvider(container);

			ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
		}

		public void Przygotuj(Kontekst kontekst, TRekord rekord)
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

		public void DodajTextBox(Expression<Func<TRekord, string>> wlasciwosc, string etykieta, bool wymagane = false)
		{
			var textbox = new TextBox();
			textbox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			kontroler.Powiazanie(textbox, wlasciwosc);
			DodajWiersz(textbox, etykieta);
			if (wymagane) Wymagane(textbox);
		}

		public void DodajCheckBox(Expression<Func<TRekord, bool>> wlasciwosc, string etykieta)
		{
			var checkbox = new CheckBox();
			checkbox.AutoSize = true;
			checkbox.Anchor = AnchorStyles.Left;
			checkbox.Text = etykieta;
			kontroler.Powiazanie(checkbox, wlasciwosc);
			DodajWiersz(checkbox, null);
		}

		public void DodajNumericUpDown(Expression<Func<TRekord, decimal>> wlasciwosc, string etykieta, int poprzecinku = 2)
		{
			var numericUpDown = new NumericUpDown();
			numericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			numericUpDown.TextAlign = HorizontalAlignment.Right;
			kontroler.Powiazanie(numericUpDown, wlasciwosc);
			numericUpDown.DecimalPlaces = poprzecinku;
			DodajWiersz(numericUpDown, etykieta);
		}

		public void DodajNumericUpDown(Expression<Func<TRekord, int>> wlasciwosc, string etykieta)
		{
			var numericUpDown = new NumericUpDown();
			numericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			numericUpDown.TextAlign = HorizontalAlignment.Right;
			kontroler.Powiazanie(numericUpDown, wlasciwosc);
			numericUpDown.DecimalPlaces = 0;
			DodajWiersz(numericUpDown, etykieta);
		}

		public void DodajDatePicker(Expression<Func<TRekord, DateTime>> wlasciwosc, string etykieta)
		{
			var dateTimePicker = new DateTimePicker();
			dateTimePicker.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			kontroler.Powiazanie(dateTimePicker, wlasciwosc);
			DodajWiersz(dateTimePicker, etykieta);
		}

		public void Wymagane(TextBox textBox)
		{
			textBox.Validating += TextBox_Wymagane_Validating;
		}

		private void TextBox_Wymagane_Validating(object sender, CancelEventArgs e)
		{
			var textBox = (TextBox)sender;
			if (String.IsNullOrEmpty(textBox.Text))
			{
				errorProvider.SetIconAlignment(textBox, ErrorIconAlignment.MiddleLeft);
				errorProvider.SetError(textBox, "Należy uzupełnić pole.");
				e.Cancel = true;
			}
			else
			{
				errorProvider.SetError(textBox, "");
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) container.Dispose();
			base.Dispose(disposing);
		}
	}
}
