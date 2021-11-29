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
	class Edytor<TRekord> : UserControl, IEdytor<TRekord>
		where TRekord : Rekord<TRekord>
	{
		protected readonly Kontroler<TRekord> kontroler;
		private readonly Container container;
		private readonly ErrorProvider errorProvider;

		public TRekord Rekord { get => kontroler.Model; private set => kontroler.Model = value; }
		public Kontekst Kontekst { get; private set; }

		public Edytor()
		{
			container = new Container();
			kontroler = new Kontroler<TRekord>();
			errorProvider = new ErrorProvider(container);
		}

		public void Przygotuj(Kontekst kontekst, TRekord rekord)
		{
			Kontekst = kontekst;
			KontekstGotowy();
			PrzygotujRekord(rekord);
			Rekord = rekord;
			RekordGotowy();
		}

		protected virtual void PrzygotujRekord(TRekord rekord)
		{
		}

		protected virtual void KontekstGotowy()
		{
		}

		protected virtual void RekordGotowy()
		{
		}

		public void Wymagane(TextBox textBox)
		{
			textBox.Validating += TextBox_Wymagane_Validating;
		}

		public void Wymagane(ComboBox comboBox)
		{
			comboBox.Validating += ComboBox_Wymagane_Validating;
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

		private void ComboBox_Wymagane_Validating(object sender, CancelEventArgs e)
		{
			var comboBox = (ComboBox)sender;
			if ((comboBox.DropDownStyle == ComboBoxStyle.DropDownList && comboBox.SelectedIndex == -1)
				|| (comboBox.DropDownStyle != ComboBoxStyle.DropDownList) && String.IsNullOrEmpty(comboBox.Text))
			{
				errorProvider.SetIconAlignment(comboBox, ErrorIconAlignment.MiddleLeft);
				errorProvider.SetError(comboBox, "Należy uzupełnić pole.");
				e.Cancel = true;
			}
			else
			{
				errorProvider.SetError(comboBox, "");
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) container.Dispose();
			base.Dispose(disposing);
		}
	}
}
