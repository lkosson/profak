using ProFak.DB;

namespace ProFak.UI;

class RachunekGrupaAkcji : FakturaSprzedazyAkcja
{
	public override IReadOnlyCollection<AkcjaNaSpisie<Faktura>> Podrzedne => [new FakturaPodobnaSprzedazAkcja(), new KorektaRachunkuAkcja()];
}
