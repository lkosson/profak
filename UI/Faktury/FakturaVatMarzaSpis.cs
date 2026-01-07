using ProFak.DB;

namespace ProFak.UI;

class FakturaVatMarzaSpis : FakturaSpis
{
	protected override bool CzyWidocznyNabywca => true;
	protected override RodzajFaktury[] Rodzaje => [RodzajFaktury.VatMarża];

	public FakturaVatMarzaSpis()
	{
	}

	public FakturaVatMarzaSpis(string[] parametry)
		: base(parametry)
	{
	}
}
