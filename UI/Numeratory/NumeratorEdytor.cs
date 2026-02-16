using ProFak.DB;

namespace ProFak.UI
{
	partial class NumeratorEdytor : NumeratorEdytorBase
	{
		private readonly SpisZAkcjami<StanNumeratora, StanNumeratoraSpis> stanyNumeratora;

		public NumeratorEdytor()
		{
			InitializeComponent();

			kontroler.Slownik<PrzeznaczenieNumeratora>(comboBoxPrzeznaczenie);

			kontroler.Powiazanie(comboBoxPrzeznaczenie, numerator => numerator.Przeznaczenie);
			kontroler.Powiazanie(comboBoxFormat, numerator => numerator.Format);

			Wymagane(comboBoxPrzeznaczenie);
			Wymagane(comboBoxFormat);

			panelStan.Controls.Add(stanyNumeratora = Spisy.StanyNumeratorow());
		}

		protected override void RekordGotowy()
		{
			base.RekordGotowy();
			stanyNumeratora.Spis.NumeratorRef = Rekord;
			stanyNumeratora.Spis.Kontekst = Kontekst;
		}

		private void comboBoxFormat_TextChanged(object? sender, EventArgs e)
		{
			var faktura = new Faktura { DataWystawienia = DateTime.Now.Date };
			try
			{
				textBoxPrzyklad.Text = String.Format(Numerator.PrzygotujWzorzec(comboBoxFormat.Text, faktura.Podstawienie), 123);
			}
			catch (ApplicationException ae)
			{
				textBoxPrzyklad.Text = ae.Message;
			}
		}
	}

	class NumeratorEdytorBase : Edytor<Numerator>
	{
	}
}
