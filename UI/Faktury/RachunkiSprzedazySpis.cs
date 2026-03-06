using ProFak.DB;

namespace ProFak.UI;

class RachunkiSprzedazySpis : RachunekSpis
{
	protected override bool CzyWidocznyNabywca => true;
	protected override RodzajFaktury[] Rodzaje => [RodzajFaktury.Rachunek, RodzajFaktury.KorektaRachunku];

	public RachunkiSprzedazySpis()
	{
	}

	public RachunkiSprzedazySpis(string[]? parametry)
		: base(parametry)
	{
	}
}

class RachunkiSprzedazyBezNabywcySpis : RachunkiSprzedazySpis
{
	protected override bool CzyWidocznyNabywca => false;
}
