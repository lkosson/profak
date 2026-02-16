using ProFak.DB;

namespace ProFak.UI;

class FakturaZakupuSpis : FakturaSpis
{
	protected override bool CzyWidocznySprzedawca => true;
	protected override RodzajFaktury[] Rodzaje => [RodzajFaktury.Zakup, RodzajFaktury.KorektaZakupu, RodzajFaktury.DowódWewnętrzny];

	public FakturaZakupuSpis()
	{
	}

	public FakturaZakupuSpis(string[]? parametry)
		: base(parametry)
	{
	}
}

class FakturaZakupuBezSprzedawcySpis : FakturaZakupuSpis
{
	protected override bool CzyWidocznySprzedawca => false;
}
