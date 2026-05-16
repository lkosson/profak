#if QUESTPDF
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ProFak.Wydruki;

public partial class PKPiR
{
	public override IDocument Przygotuj()
	{
		var dokument = new Szablon(dane);
		return dokument;
	}

	class Szablon(IEnumerable<PKPiRDTO> pozycje) : IDocument
	{
		private const int RozmiarSpecyfikacji = RozmiarTekst - 2;
		private const int RozmiarTekst = 10;
		private const int RozmiarNaglowek3 = RozmiarTekst;
		private const int RozmiarNaglowek2 = RozmiarNaglowek3 + 2;
		private const int RozmiarNaglowek1 = RozmiarNaglowek2 + 2;

		public void Compose(IDocumentContainer dokument)
		{
			dokument.Page(Strona);
		}

		private void Strona(PageDescriptor strona)
		{
			strona.Size(PageSizes.A4);
			strona.Margin(1.5f, Unit.Centimetre);
			strona.PageColor(Colors.White);
			strona.DefaultTextStyle(Czcionka);
			strona.Content().Column(Zawartosc);
		}

		private TextStyle Czcionka(TextStyle styl)
		{
			return styl.FontFamily("Calibri").FontSize(10);
		}

		private void Zawartosc(ColumnDescriptor zawartosc)
		{
		}
	}
}
#endif