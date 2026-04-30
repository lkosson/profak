using ProFak.DB;

namespace ProFak.UI;

class NumeratorEdytor : Edytor<Numerator>
{
	private readonly TextBox textBoxPrzyklad;
	private readonly SpisZAkcjami<StanNumeratora, StanNumeratoraSpis> stanyNumeratora;

	public NumeratorEdytor()
	{
		var comboBoxPrzeznaczenie = Kontrolki.DropDownList();
		var comboBoxFormat = Kontrolki.SuggestBox(["F/[Numer]", "F/[Numer:000000]", "F/[Numer:0000]/[Rok]", "F/[Numer:0000]/[Miesiac:00]/[Rok]", "F/[Numer]/[Data:yy/MM]", "F/[Numer:0000]/[Data:yyMMdd]"]);
		var textBoxGrupa = Kontrolki.TextBox();
		var linkLabelGrupa = Kontrolki.LinkPomoc(PomocFormatGrupy);
		textBoxPrzyklad = Kontrolki.TextBox();
		stanyNumeratora = Spisy.StanyNumeratorow();

		textBoxPrzyklad.ReadOnly = true;

		kontroler.Slownik<PrzeznaczenieNumeratora>(comboBoxPrzeznaczenie);

		kontroler.Powiazanie(comboBoxPrzeznaczenie, numerator => numerator.Przeznaczenie);
		kontroler.Powiazanie(comboBoxFormat, numerator => numerator.Format, delegate { textBoxGrupa.PlaceholderText = comboBoxFormat.Text; PrzeliczPrzyklad(); });
		kontroler.Powiazanie(textBoxGrupa, numerator => numerator.Grupa!, PrzeliczPrzyklad);

		Wymagane(comboBoxPrzeznaczenie);
		Wymagane(comboBoxFormat);

		var siatka = new Siatka([0, -1, 0], [0, 0, 0, 0, -1]);
		siatka.DodajWiersz("Przeznaczenie", [(comboBoxPrzeznaczenie, 2)]);
		siatka.DodajWiersz("Format", [(comboBoxFormat, 2)]);
		siatka.DodajWiersz("Grupa", [textBoxGrupa, linkLabelGrupa]);
		siatka.DodajWiersz("Przykładowy numer", [(textBoxPrzyklad, 2)]);
		siatka.DodajWiersz([(stanyNumeratora, 3)]);

		UstawZawartosc(siatka, new Size(800, 350));
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

	private const string PomocFormatGrupy = @"Zaawansowany parametr określający kiedy numeracja ma ponownie zaczynać się od 1.

Pozostaw to pole puste, by numeracja była restartowana według roku, miesiąca lub dnia zgodnie z wybranym formatem numeru.

Jeśli chcesz, by numeracja była resetowana rocznie, ale w numerze faktury występował także numer miesiąca, wprowadź tu wyrażenie zawierające [Rok], ale nie [Miesiac].";
}
