using ProFak.DB;

namespace ProFak.UI;

class FakturaProformaSpis : FakturaSpis
{
	protected override bool CzyWidocznyNabywca => true;
	protected override RodzajFaktury[] Rodzaje => [RodzajFaktury.Proforma];
}
