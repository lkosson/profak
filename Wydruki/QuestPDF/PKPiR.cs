#if QUESTPDF
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ProFak.Wydruki;

public partial class PKPiR
{
	public override IDocument Przygotuj()
	{
		var dokument = new Szablon(dane.First(), dane);
		return dokument;
	}

	class Szablon(PKPiRDTO naglowek, IEnumerable<PKPiRDTO> pozycje) : IDocument
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
				kolumny.ConstantColumn(3, Unit.Centimetre); // Data zdarzenia
				kolumny.ConstantColumn(3, Unit.Centimetre); // Numer KSeF
				kolumny.ConstantColumn(3, Unit.Centimetre); // Numer dowodu
				kolumny.ConstantColumn(2, Unit.Centimetre); // NIP
				kolumny.RelativeColumn(3); // Nazwa
				kolumny.RelativeColumn(3); // Adres
				kolumny.RelativeColumn(3); // Opis
			});

			tabela.Header(wiersz =>
			{
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Lp.");
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Data zdarzenia");
				wiersz.Cell().ColumnSpan(2).Element(StylNaglowka).Text("Numer dowodu");
				wiersz.Cell().ColumnSpan(3).Element(StylNaglowka).Text("Kontrahent");
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Opis zdarzenia");

				wiersz.Cell().Element(StylNaglowka).Text("KSeF");
				wiersz.Cell().Element(StylNaglowka).Text("księgowego");
				wiersz.Cell().Element(StylNaglowka).Text("NIP");
				wiersz.Cell().Element(StylNaglowka).Text("Nazwa");
				wiersz.Cell().Element(StylNaglowka).Text("Adres");

				wiersz.Cell().Element(StylNaglowkaNumer).Text("1");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("2");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("3");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("4");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("5");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("6");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("7");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("8");
			});

			foreach (var pozycja in pozycje)
			{
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.LP.ToString());
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.Data.ToString("yyyy-MM-dd"));
				tabela.Cell().Element(StylSpecyfikacjiLewo).Text(pozycja.NumerKSeF);
				tabela.Cell().Element(StylSpecyfikacjiLewo).Text(pozycja.Numer);
				tabela.Cell().Element(StylSpecyfikacjiLewo).Text(pozycja.NIP);
				tabela.Cell().Element(StylSpecyfikacjiLewo).Text(pozycja.Kontrahent);
				tabela.Cell().Element(StylSpecyfikacjiLewo).Text(pozycja.Adres);
				tabela.Cell().Element(StylSpecyfikacjiLewo).Text(pozycja.Opis);
			}
		}

		private void TabelaPrawa(TableDescriptor tabela)
		{
			tabela.ColumnsDefinition(kolumny =>
			{
				kolumny.ConstantColumn(1, Unit.Centimetre); // LP
				kolumny.ConstantColumn(2f, Unit.Centimetre); // Sprzedaż
				kolumny.ConstantColumn(2f, Unit.Centimetre); // Pozostałe
				kolumny.ConstantColumn(2f, Unit.Centimetre); // Razem
				kolumny.ConstantColumn(2f, Unit.Centimetre); // Zakupy
				kolumny.ConstantColumn(2f, Unit.Centimetre); // Koszty
				kolumny.ConstantColumn(2f, Unit.Centimetre); // Wynagrodzenia
				kolumny.ConstantColumn(2f, Unit.Centimetre); // Pozostałe
				kolumny.ConstantColumn(2f, Unit.Centimetre); // Razem
				kolumny.ConstantColumn(2f, Unit.Centimetre); // Inne
				kolumny.ConstantColumn(2f, Unit.Centimetre); // BiR
				kolumny.RelativeColumn(); // Uwagi
			});

			tabela.Header(wiersz =>
			{
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Lp.");
				wiersz.Cell().ColumnSpan(3).Element(StylNaglowka).Text("Przychody");
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Zakupy towarów");
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Koszty uboczne");
				wiersz.Cell().ColumnSpan(4).Element(StylNaglowka).Text("Wydatki");
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Koszty badań i rozwoju");
				wiersz.Cell().RowSpan(2).Element(StylNaglowka).Text("Uwagi");

				wiersz.Cell().Element(StylNaglowka).Text("Sprzedaż");
				wiersz.Cell().Element(StylNaglowka).Text("Pozostałe");
				wiersz.Cell().Element(StylNaglowka).Text("Razem");
				wiersz.Cell().Element(StylNaglowka).Text("Wynagrodzenia");
				wiersz.Cell().Element(StylNaglowka).Text("Pozostałe");
				wiersz.Cell().Element(StylNaglowka).Text("Razem");
				wiersz.Cell().Element(StylNaglowka).Text("Inne");

				wiersz.Cell().Element(StylNaglowkaNumer).Text("1");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("9");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("10");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("11");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("12");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("13");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("14");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("15");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("16");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("17");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("18");
				wiersz.Cell().Element(StylNaglowkaNumer).Text("19");
			});

			foreach (var pozycja in pozycje)
			{
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.LP.ToString());
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.PrzychodWartosc.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.PrzychodPozostale.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.PrzychodRazem.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.KosztyZakup.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.KosztyUboczne.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.KosztyWynagrodzenia.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.KosztyPozostale.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.KosztyRazem.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.KosztyInne.ToString("n2"));
				tabela.Cell().Element(StylSpecyfikacji).Text(pozycja.KosztyBR);
				tabela.Cell().Element(StylSpecyfikacjiLewo).Text(pozycja.Uwagi);
			}

			tabela.Cell().Element(StylNaglowka).Text("Suma").AlignRight();
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.PrzychodWartosc).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.PrzychodPozostale).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.PrzychodRazem).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.KosztyZakup).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.KosztyUboczne).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.KosztyWynagrodzenia).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.KosztyPozostale).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.KosztyRazem).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text(pozycje.Sum(e => e.KosztyInne).ToString("n2"));
			tabela.Cell().Element(StylSpecyfikacji).Text("0,00");
			tabela.Cell().Text("");
		}

		private IContainer StylNaglowka(IContainer komorka) => komorka.Background(Colors.Grey.Lighten1).Border(0.1f, Colors.Black).Padding(2).AlignCenter().AlignMiddle();
		private IContainer StylNaglowkaNumer(IContainer komorka) => komorka.Background(Colors.Grey.Lighten3).Border(0.1f, Colors.Black).Padding(2).AlignCenter().AlignMiddle().DefaultTextStyle(styl => styl.FontSize(RozmiarNumerowKolumn));
		private IContainer StylSpecyfikacji(IContainer komorka) => komorka.MinHeight(1, Unit.Centimetre).Border(0.1f, Colors.Black).Padding(2).AlignRight().AlignMiddle();
		private IContainer StylSpecyfikacjiLewo(IContainer komorka) => komorka.Border(0.1f, Colors.Black).Padding(2).AlignLeft().AlignMiddle();
	}
}
#endif