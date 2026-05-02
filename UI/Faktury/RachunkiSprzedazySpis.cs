using ProFak.DB;

namespace ProFak.UI;

class RachunkiSprzedazySpis : FakturaSprzedazySpis
{
	protected override RodzajFaktury[] Rodzaje => [RodzajFaktury.Rachunek, RodzajFaktury.KorektaRachunku];
}

class RachunkiSprzedazyBezNabywcySpis : RachunkiSprzedazySpis
{
	protected override bool CzyWidocznyNabywca => false;
}
