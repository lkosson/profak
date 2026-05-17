#if QUESTPDF
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ProFak.Wydruki;

public partial class Faktura
{
	public override IDocument Przygotuj()
	{
		var dokumenty = new List<IDocument>();
		foreach (var faktura in dane.GroupBy(e => e.Numer).OrderBy(g => g.Key))
		{
			var dokument = new Szablon(faktura.First(e => String.IsNullOrEmpty(e.LP)), faktura.Where(e => !String.IsNullOrEmpty(e.LP)));
			dokumenty.Add(dokument);
		}

		var zbiorczy = Document.Merge(dokumenty);
		return zbiorczy;
	}

	class Szablon(FakturaDTO naglowek, IEnumerable<FakturaDTO> pozycje) : IDocument
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
			return styl.FontFamily("Calibri").FontSize(RozmiarTekst);
		}

		private void Zawartosc(ColumnDescriptor zawartosc)
		{
			zawartosc.Item().Row(Naglowek);
			zawartosc.Item().DefaultTextStyle(styl => styl.FontSize(RozmiarSpecyfikacji)).Table(Specyfikacja);
			zawartosc.Item().Column(Stopka);
		}

		private void Naglowek(RowDescriptor naglowek)
		{
			naglowek.RelativeItem().Column(Kontrahenci);
			naglowek.RelativeItem().Column(NumeryDaty);
		}

		private void Kontrahenci(ColumnDescriptor kontrahenci)
		{
			kontrahenci.Item().Element(NaglowekKontrahenta("Sprzedawca"));
			kontrahenci.Item().Text(naglowek.NazwaSprzedawcy);
			kontrahenci.Item().Text(naglowek.AdresSprzedawcy);
			kontrahenci.Item().Element(NaglowekPlusTekst("NIP", naglowek.NIPSprzedawcy));
			kontrahenci.Item().Height(0.6f, Unit.Centimetre);

			kontrahenci.Item().Element(NaglowekKontrahenta("Nabywca"));
			kontrahenci.Item().Text(naglowek.NazwaNabywcy);
			kontrahenci.Item().Text(naglowek.AdresNabywcy);
			kontrahenci.Item().Element(NaglowekPlusTekst("NIP", naglowek.NIPNabywcy));
			kontrahenci.Item().Height(0.6f, Unit.Centimetre);

			if (!String.IsNullOrEmpty(naglowek.DaneOdbiorcy))
			{
				kontrahenci.Item().Element(NaglowekKontrahenta("Odbiorca"));
				kontrahenci.Item().Element(SekcjaSformatowana(naglowek.DaneOdbiorcy));
				kontrahenci.Item().Height(0.6f, Unit.Centimetre);
			}
		}

		private Action<IContainer> NaglowekKontrahenta(string naglowek)
		{
			return container => container.Text(naglowek).FontSize(RozmiarNaglowek2).Bold();
		}

		private Action<IContainer> NaglowekPlusTekst(string naglowek, string? tresc)
		{
			return String.IsNullOrEmpty(tresc) ? _ => { } : container => container.Text(tekst =>
			{
				tekst.Span(naglowek + ": ").FontSize(RozmiarNaglowek3).Bold();
				tekst.Span(tresc);
			});
		}

		private Action<IContainer> SekcjaSformatowana(string? tekst, bool doPrawej = false)
		{
			return container =>
			{
				if (String.IsNullOrEmpty(tekst)) return;
				container.Column(kolumna =>
				{
					var linie = tekst.Split("<br/>");
					foreach (var linia in linie)
					{
						var liniap = kolumna.Item();
						if (doPrawej) liniap = liniap.AlignRight();
						liniap.Text(tekst =>
						{
							var pozycja = 0;
							var gruby = false;
							while (pozycja < linia.Length)
							{
								if (gruby)
								{
									var znakKonca = linia.IndexOf("</b>", pozycja);
									if (znakKonca >= 0)
									{
										tekst.Span(linia[pozycja..znakKonca]).Bold();
										gruby = false;
										pozycja = znakKonca + 4;
									}
									else
									{
										tekst.Span(linia[pozycja..]).Bold();
										pozycja = linia.Length;
									}
								}
								else
								{
									var znakPoczatku = linia.IndexOf("<b>", pozycja);
									if (znakPoczatku >= 0)
									{
										tekst.Span(linia[pozycja..znakPoczatku]);
										gruby = true;
										pozycja = znakPoczatku + 3;
									}
									else
									{
										tekst.Span(linia[pozycja..]);
										pozycja = linia.Length;
									}
								}
							}
						});
					}
				});
			};
		}

		private void NumeryDaty(ColumnDescriptor descriptor)
		{
			descriptor.Item().AlignRight().Text(naglowek.Rodzaj).FontSize(RozmiarNaglowek1).Bold();
			descriptor.Item().AlignRight().Text(naglowek.Numer).FontSize(RozmiarNaglowek1);
			if (!String.IsNullOrEmpty(naglowek.Korekta)) descriptor.Item().AlignRight().DefaultTextStyle(styl => styl.FontSize(RozmiarNaglowek2)).Element(SekcjaSformatowana(naglowek.Korekta, doPrawej: true));
			descriptor.Item().AlignRight().Height(0.6f, Unit.Centimetre);
			descriptor.Item().AlignRight().Element(NaglowekPlusTekst("Data wystawienia", naglowek.DataWystawienia));
			descriptor.Item().AlignRight().Element(NaglowekPlusTekst("Data sprzedaży", naglowek.DataSprzedazy));
		}

		private void Specyfikacja(TableDescriptor specyfikacja)
		{
			specyfikacja.ColumnsDefinition(kolumny =>
			{
				kolumny.ConstantColumn(0.8f, Unit.Centimetre); // LP
				kolumny.RelativeColumn(); // Nazwa
				kolumny.ConstantColumn(2.0f, Unit.Centimetre); // Cena netto
				kolumny.ConstantColumn(1.0f, Unit.Centimetre); // Ilość
				if (naglowek.JestVAT) kolumny.ConstantColumn(2.0f, Unit.Centimetre); // Wartość netto
				if (naglowek.JestRabat) kolumny.ConstantColumn(2.0f, Unit.Centimetre); // Rabat
				if (naglowek.JestVAT) kolumny.ConstantColumn(0.8f, Unit.Centimetre); // VAT
				if (naglowek.JestVAT) kolumny.ConstantColumn(2.0f, Unit.Centimetre); // Wartość VAT
				kolumny.ConstantColumn(2.2f, Unit.Centimetre); // Wartość brutto
			});

			specyfikacja.Header(wiersz =>
			{
				wiersz.Cell().Element(StylNaglowkaSpecyfikacji).Text("LP");
				wiersz.Cell().Element(StylNaglowkaSpecyfikacji).Text("Nazwa towaru lub usługi");
				wiersz.Cell().Element(StylNaglowkaSpecyfikacji).Text(naglowek.JestVAT ? "Cena netto" : "Cena");
				wiersz.Cell().Element(StylNaglowkaSpecyfikacji).Text("Ilość");
				if (naglowek.JestVAT) wiersz.Cell().Element(StylNaglowkaSpecyfikacji).Text("Wartość netto");
				if (naglowek.JestRabat) wiersz.Cell().Element(StylNaglowkaSpecyfikacji).Text("Rabat");
				if (naglowek.JestVAT) wiersz.Cell().Element(StylNaglowkaSpecyfikacji).Text("VAT");
				if (naglowek.JestVAT) wiersz.Cell().Element(StylNaglowkaSpecyfikacji).Text("Wartość VAT");
				wiersz.Cell().Element(StylNaglowkaSpecyfikacji).Text(naglowek.JestVAT ? "Wartość brutto" : "Wartość");
			});

			foreach (var grupa in pozycje.GroupBy(e => e.NaglowekPozycji).OrderBy(g => g.Key))
			{
				if (!String.IsNullOrEmpty(grupa.Key))
				{
					specyfikacja.Cell().ColumnSpan(5u + (naglowek.JestRabat ? 1u : 0u) + (naglowek.JestVAT ? 3u : 0u)).Element(StylNaglowkaPozycji).Text(grupa.Key);
				}

				foreach (var pozycja in grupa.OrderBy(e => e.LP))
				{
					specyfikacja.Cell().Element(StylSpecyfikacji).Text(pozycja.LP);
					specyfikacja.Cell().Element(StylSpecyfikacjiLewo).Text(pozycja.OpisPozycji);
					specyfikacja.Cell().Element(StylSpecyfikacji).Text($"{pozycja.CenaNetto:n2} {pozycja.Waluta}");
					specyfikacja.Cell().Element(StylSpecyfikacji).Text(pozycja.Ilosc);
					if (naglowek.JestVAT) specyfikacja.Cell().Element(StylSpecyfikacji).Text($"{Math.Abs(pozycja.WartoscNetto):n2} {pozycja.Waluta}");
					if (naglowek.JestRabat) specyfikacja.Cell().Element(StylSpecyfikacji).Text(pozycja.Rabat);
					if (naglowek.JestVAT) specyfikacja.Cell().Element(StylSpecyfikacjiSrodek).Text(pozycja.StawkaVAT);
					if (naglowek.JestVAT) specyfikacja.Cell().Element(StylSpecyfikacji).Text($"{Math.Abs(pozycja.WartoscVat):n2} {pozycja.WalutaVAT}");
					specyfikacja.Cell().Element(StylSpecyfikacji).Text($"{Math.Abs(pozycja.WartoscBrutto):n2} {pozycja.Waluta}");
				}
			}

			if (naglowek.JestVAT)
			{
				specyfikacja.Cell().ColumnSpan(5u + (naglowek.JestRabat ? 1u : 0u) + (naglowek.JestVAT ? 3u : 0u)).Text("");

				specyfikacja.Cell().ColumnSpan(4);
				specyfikacja.Cell().Element(StylNaglowkaSpecyfikacji).Text("Wartość netto");
				if (naglowek.JestRabat) specyfikacja.Cell().Element(StylNaglowkaSpecyfikacji).Text("Rabat");
				specyfikacja.Cell().Element(StylNaglowkaSpecyfikacji).Text("VAT");
				specyfikacja.Cell().Element(StylNaglowkaSpecyfikacji).Text("Wartość VAT");
				specyfikacja.Cell().Element(StylNaglowkaSpecyfikacji).Text("Wartość brutto");

				foreach (var grupa in pozycje.GroupBy(e => e.StawkaVAT))
				{
					specyfikacja.Cell().ColumnSpan(3);
					specyfikacja.Cell().Element(StylNaglowkaSpecyfikacji).Text("W tym");
					specyfikacja.Cell().Element(StylSpecyfikacji).Text($"{grupa.Sum(e => e.WartoscNetto):n2} {naglowek.Waluta}");
					if (naglowek.JestRabat) specyfikacja.Cell().Element(StylSpecyfikacji);
					specyfikacja.Cell().Element(StylSpecyfikacjiSrodek).Text(grupa.Key);
					specyfikacja.Cell().Element(StylSpecyfikacji).Text($"{grupa.Sum(e => e.WartoscVat):n2} {naglowek.WalutaVAT}");
					specyfikacja.Cell().Element(StylSpecyfikacji).Text($"{grupa.Sum(e => e.WartoscBrutto):n2} {naglowek.Waluta}");
				}

				specyfikacja.Cell().ColumnSpan(3);
				specyfikacja.Cell().Element(StylNaglowkaSpecyfikacji).Text("Razem");
				specyfikacja.Cell().Element(StylSpecyfikacji).Text($"{pozycje.Sum(e => e.WartoscNetto):n2} {naglowek.Waluta}");
				if (naglowek.JestRabat) specyfikacja.Cell().Element(StylSpecyfikacji).Text($"{pozycje.Sum(e => e.RabatRazem):n2} {naglowek.Waluta}");
				specyfikacja.Cell().Element(StylSpecyfikacjiSrodek).Text("-");
				specyfikacja.Cell().Element(StylSpecyfikacji).Text($"{pozycje.Sum(e => e.WartoscVat):n2} {naglowek.WalutaVAT}");
				specyfikacja.Cell().Element(StylSpecyfikacji).Text($"{pozycje.Sum(e => e.WartoscBrutto):n2} {naglowek.Waluta}");
			}
			else
			{
				specyfikacja.Cell().ColumnSpan(3);
				specyfikacja.Cell().Element(StylNaglowkaSpecyfikacji).Text("Razem");
				if (naglowek.JestRabat) specyfikacja.Cell().Element(StylSpecyfikacji).Text($"{pozycje.Sum(e => e.RabatRazem):n2} {naglowek.Waluta}");
				specyfikacja.Cell().Element(StylSpecyfikacji).Text($"{pozycje.Sum(e => e.WartoscBrutto):n2} {naglowek.Waluta}");
			}
		}

		private IContainer StylNaglowkaSpecyfikacji(IContainer komorka) => komorka.Background(Colors.Grey.Lighten1).Border(0.1f, Colors.Black).Padding(2).AlignCenter().AlignMiddle();
		private IContainer StylNaglowkaPozycji(IContainer komorka) => komorka.Background(Colors.Grey.Lighten3).Border(0.1f, Colors.Black).Padding(2).AlignCenter().AlignMiddle();
		private IContainer StylSpecyfikacji(IContainer komorka) => komorka.Border(0.1f, Colors.Black).Padding(2).AlignRight().AlignMiddle();
		private IContainer StylSpecyfikacjiLewo(IContainer komorka) => komorka.Border(0.1f, Colors.Black).Padding(2).AlignLeft().AlignMiddle();
		private IContainer StylSpecyfikacjiSrodek(IContainer komorka) => komorka.Border(0.1f, Colors.Black).Padding(2).AlignCenter().AlignMiddle();

		private void Stopka(ColumnDescriptor stopka)
		{
			stopka.Item().Element(SekcjaSformatowana(naglowek.Rozliczenia));
			stopka.Item().Element(NaglowekPlusTekst("Do zapłaty", naglowek.DoZaplaty));
			stopka.Item().Element(NaglowekPlusTekst("Do zwrotu", naglowek.DoZwrotu));
			stopka.Item().Element(NaglowekPlusTekst("Słownie", naglowek.Slownie));
			if (naglowek.KursWaluty != 1) stopka.Item().Element(NaglowekPlusTekst("Kurs waluty", $"1 {naglowek.Waluta} = {naglowek.KursWaluty:0.0000} {naglowek.WalutaVAT}"));
			stopka.Item().Element(NaglowekPlusTekst("Termin płatności", naglowek.TerminPlatnosci));
			stopka.Item().Element(NaglowekPlusTekst("Forma płatności", naglowek.FormaPlatnosci));
			stopka.Item().Element(NaglowekPlusTekst("Numer rachunku", naglowek.NumerRachunku));
			stopka.Item().Element(NaglowekPlusTekst("Nazwa banku", naglowek.NazwaBanku));
			stopka.Item().Element(NaglowekPlusTekst("Procedura marży", naglowek.ProceduraMarzy));
			stopka.Item().Element(SekcjaSformatowana(naglowek.Uwagi));
			stopka.Item().Height(0.6f, Unit.Centimetre);
			stopka.Item().AlignRight().Width(5, Unit.Centimetre).Column(KodQR);
		}

		private void KodQR(ColumnDescriptor kodQR)
		{
			if (String.IsNullOrEmpty(naglowek.KodKSeF)) return;
			kodQR.Item().Image(Convert.FromBase64String(naglowek.KodKSeF));
			kodQR.Item().Text(naglowek.NumerKSeF).AlignCenter().FontFamily("Consolas", "Courier New");
		}
	}
}
#endif