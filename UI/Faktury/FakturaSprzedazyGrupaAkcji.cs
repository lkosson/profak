using ProFak.DB;

namespace ProFak.UI
{
	class FakturaSprzedazyGrupaAkcji : FakturaSprzedazyAkcja
	{
		public override IReadOnlyCollection<AkcjaNaSpisie<Faktura>> Podrzedne => [new FakturaPodobnaSprzedazAkcja(), new FakturaVatMarzaAkcja(), new FakturaProformaAkcja(), new KorektaSprzedazyAkcja()];
	}
}
