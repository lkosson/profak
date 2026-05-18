#if QUESTPDF
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ProFak.Wydruki;

public partial class EwidencjaPrzychodow
{
	public override IDocument Przygotuj()
	{
		var dokument = new Szablon(dane.First(), dane);
		return dokument;
	}

	class Szablon(EwidencjaPrzychodowDTO naglowek, IEnumerable<EwidencjaPrzychodowDTO> pozycje) : IDocument
	{
		private const int RozmiarNumerowKolumn = RozmiarSpecyfikacji - 2;
		private const int RozmiarSpecyfikacji = RozmiarTekst - 2;
		private const int RozmiarTekst = 10;
		private const int RozmiarNaglowek3 = RozmiarTekst;
		private const int RozmiarNaglowek2 = RozmiarNaglowek3 + 2;
		private const int RozmiarNaglowek1 = RozmiarNaglowek2 + 2;

		public void Compose(IDocumentContainer dokument)
		{
			dokument.Page(StronaLewa);
			dokument.Page(StronaPrawa);
		}

		private void StronaLewa(PageDescriptor strona)
		{
			strona.Size(PageSizes.A4.Landscape());
			strona.Margin(1.5f, Unit.Centimetre);
			strona.PageColor(Colors.White);
			strona.DefaultTextStyle(Czcionka);
			strona.Content().Column(ZawartoscLewa);
		}

		private void StronaPrawa(PageDescriptor strona)
		{
			strona.Size(PageSizes.A4.Landscape());
			strona.Margin(1.5f, Unit.Centimetre);
			strona.PageColor(Colors.White);
			strona.DefaultTextStyle(Czcionka);
			strona.Content().Column(ZawartoscPrawa);
		}

		private TextStyle Czcionka(TextStyle styl)
		{
			return styl.FontFamily("Calibri").FontSize(10);
		}

		private void ZawartoscLewa(ColumnDescriptor zawartosc)
		{
			zawartosc.Item().Row(Tytul);
			zawartosc.Item().DefaultTextStyle(styl => styl.FontSize(RozmiarSpecyfikacji)).Table(TabelaLewa);
		}

		private void ZawartoscPrawa(ColumnDescriptor zawartosc)
		{
			zawartosc.Item().Row(Tytul);
			zawartosc.Item().DefaultTextStyle(styl => styl.FontSize(RozmiarSpecyfikacji)).Table(TabelaPrawa);
		}

		private void Tytul(RowDescriptor tytul)
		{
			tytul.RelativeItem(1).Text(naglowek.Podmiot).AlignCenter();
			tytul.RelativeItem(3).Text(naglowek.Tytul).FontSize(RozmiarNaglowek1).Bold().AlignCenter();
			tytul.RelativeItem(1).Text("");
		}

		private void TabelaLewa(TableDescriptor tabela)
		{
			tabela.ColumnsDefinition(kolumny =>
			{
				kolumny.ConstantColumn(1, Unit.Centimetre); // LP
				kolumny.ConstantColumn(3, Unit.Centimetre); // Data wpisu
				kolumny.ConstantColumn(3, Unit.Centimetre); // Data przychodu
				kolumny.RelativeColumn(3); // Numer KSeF
				kolumny.RelativeColumn(2); // Numer dowodu
				kolumny.RelativeColumn(2); // NIP
				kolumny.ConstantColumn(0.1f, Unit.Centimetre); // Ukryta, do ustalenia wysokości wiersza
			});

			tabela.Header(wiersz =>
			{
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Lp.");
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Data wpływu");
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Data uzyskania przychodu");
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Numer KSeF");
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Numer dowodu");
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("NIP kontrahenta");
				wiersz.Cell().Text("");

				wiersz.Cell().Text("");

				wiersz.Cell().Element(StylNaglowkaNumer).Text("1");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("2");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("3");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("4");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("5");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("6");
				wiersz.Cell().Text("");
			});

			foreach (var pozycja in pozycje)
			{
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.LP.ToString());
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.DataWpisu.ToString("yyyy-MM-dd"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.DataPrzychodu.ToString("yyyy-MM-dd"));
				tabela.Cell().Element(StylSpecyfikacjiLewo).Text(pozycja.NumerKSeF);
				tabela.Cell().Element(StylSpecyfikacjiLewo).Text(pozycja.NumerDowodu);
				tabela.Cell().Element(StylSpecyfikacjiLewo).Text(pozycja.NIP);
				tabela.Cell();
			}
		}

		private void TabelaPrawa(TableDescriptor tabela)
		{
			tabela.ColumnsDefinition(kolumny =>
			{
				kolumny.ConstantColumn(1, Unit.Centimetre); // LP
				kolumny.ConstantColumn(2f, Unit.Centimetre); // 17%
				kolumny.ConstantColumn(2f, Unit.Centimetre); // 15%
				kolumny.ConstantColumn(2f, Unit.Centimetre); // 14%
				kolumny.ConstantColumn(2f, Unit.Centimetre); // 12,5%
				kolumny.ConstantColumn(2f, Unit.Centimetre); // 12%
				kolumny.ConstantColumn(2f, Unit.Centimetre); // 10%
				kolumny.ConstantColumn(2f, Unit.Centimetre); // 8,5%
				kolumny.ConstantColumn(2f, Unit.Centimetre); // 5,5%
				kolumny.ConstantColumn(2f, Unit.Centimetre); // 3%
				kolumny.ConstantColumn(2.5f, Unit.Centimetre); // Ogółem
				kolumny.RelativeColumn(); // Uwagi
			});

			tabela.Header(wiersz =>
			{
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Lp.");
				wiersz.Cell().ColumnSpan(9).Element(StylNaglowka).Text("Przychody objęte ryczałtem od przychodów ewidencjonowanych według stawki");
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Ogółem przychody");
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Uwagi");

				wiersz.Cell().Element(StylNaglowka).Text("17%");
				wiersz.Cell().Element(StylNaglowka).Text("15%");
				wiersz.Cell().Element(StylNaglowka).Text("14%");
				wiersz.Cell().Element(StylNaglowka).Text("12,5%");
				wiersz.Cell().Element(StylNaglowka).Text("12%");
				wiersz.Cell().Element(StylNaglowka).Text("10%");
				wiersz.Cell().Element(StylNaglowka).Text("8,5%");
				wiersz.Cell().Element(StylNaglowka).Text("5,5%");
				wiersz.Cell().Element(StylNaglowka).Text("3%");

				wiersz.Cell().Element(StylNaglowkaNumer).Text("1");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("7");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("8");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("9");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("10");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("11");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("12");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("13");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("14");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("15");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("16");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("17");
			});

			foreach (var pozycja in pozycje)
			{
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.LP.ToString());
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.Przychod17.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.Przychod15.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.Przychod14.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.Przychod125.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.Przychod12.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.Przychod10.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.Przychod85.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.Przychod55.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.Przychod3.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.PrzychodRazem.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacjiLewo).Text(pozycja.Uwagi);
			}

			tabela.Cell().Element(StylNaglowka).Text("Suma").AlignRight();
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.Przychod17).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.Przychod15).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.Przychod14).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.Przychod125).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.Przychod12).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.Przychod10).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.Przychod85).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.Przychod55).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.Przychod3).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.PrzychodRazem).ToString("n2"));
			tabela.Cell().Text("");
		}

		private IContainer StylNaglowka(IContainer komorka) => komorka.Background(Colors.Grey.Lighten1).Border(0.1f, Colors.Black).Padding(2).AlignCenter().AlignMiddle();
		private IContainer StylNaglowkaNumer(IContainer komorka) => komorka.Background(Colors.Grey.Lighten3).Border(0.1f, Colors.Black).Padding(2).AlignCenter().AlignMiddle().DefaultTextStyle(styl => styl.FontSize(RozmiarNumerowKolumn));
		private IContainer StylSpecyfikacji(IContainer komorka) => komorka.Border(0.1f, Colors.Black).Padding(2).AlignRight().AlignMiddle();
		private IContainer StylSpecyfikacjiLewo(IContainer komorka) => komorka.Border(0.1f, Colors.Black).Padding(2).AlignLeft().AlignMiddle();
	}
}
#endif