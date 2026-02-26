using ProFak.DB;

namespace ProFak.UI;

class FakturaSprzedazySpis : FakturaSpis
{
	protected override bool CzyWidocznyNabywca => true;
	protected override RodzajFaktury[] Rodzaje => [RodzajFaktury.Sprzedaż, RodzajFaktury.KorektaSprzedaży, RodzajFaktury.VatMarża, RodzajFaktury.KorektaVatMarży];

	public FakturaSprzedazySpis()
	{
	}

	public FakturaSprzedazySpis(string[]? parametry)
		: base(parametry)
	{
	}
}

class FakturaSprzedazyBezNabywcySpis : FakturaSprzedazySpis
{
	protected override bool CzyWidocznyNabywca => false;
}
