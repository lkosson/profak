using ProFak.DB;

namespace ProFak.UI;

partial class NumeratorEdytor : NumeratorEdytorBase
{
	private readonly SpisZAkcjami<StanNumeratora, StanNumeratoraSpis> stanyNumeratora;

	public NumeratorEdytor()
	{
		InitializeComponent();

		kontroler.Slownik<PrzeznaczenieNumeratora>(comboBoxPrzeznaczenie);

		kontroler.Powiazanie(comboBoxPrzeznaczenie, numerator => numerator.Przeznaczenie);
		kontroler.Powiazanie(comboBoxFormat, numerator => numerator.Format, delegate { textBoxGrupa.PlaceholderText = comboBoxFormat.Text; PrzeliczPrzyklad(); });
		kontroler.Powiazanie(textBoxGrupa, numerator => numerator.Grupa!, PrzeliczPrzyklad);

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

	private void PrzeliczPrzyklad()
	{
		var faktura = new Faktura { DataWystawienia = DateTime.Now.Date };
		try
		{
			textBoxPrzyklad.Text = Rekord.NadajNumer(Kontekst.Baza, faktura.Podstawienie, zwiekszLicznik: false);
		}
		catch (ApplicationException ae)
		{
			textBoxPrzyklad.Text = ae.Message;
		}
	}

	private void linkLabelGrupa_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		MessageBox.Show("Zaawansowany parametr określający kiedy numeracja ma ponownie zaczynać się od 1.\n\nPozostaw to pole puste, by numeracja była restartowana według roku, miesiąca lub dnia zgodnie z wybranym formatem numeru.\n\nJeśli chcesz, by numeracja była resetowana rocznie, ale w numerze faktury występował także numer miesiąca, wprowadź tu wyrażenie zawierające [Rok], ale nie [Miesiac].", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}
}

class NumeratorEdytorBase : Edytor<Numerator>
{
}
