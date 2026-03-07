using ProFak.DB;

namespace ProFak.UI;

class RachunkiSprzedazySpis : FakturaSprzedazySpis
{
	protected override bool CzyWidocznyNabywca => true;
	protected override RodzajFaktury[] Rodzaje => [RodzajFaktury.Rachunek, RodzajFaktury.KorektaRachunku];
	protected override bool CzySprzedaz
		=> Rodzaje.Contains(RodzajFaktury.Rachunek)
		|| Rodzaje.Contains(RodzajFaktury.KorektaRachunku);

	public RachunkiSprzedazySpis()
	{
	}

	public RachunkiSprzedazySpis(string[]? parametry)
		: base(parametry)
	{
		Columns[nameof(Faktura.CzyKSeF)].Visible = false;
		Columns[nameof(Faktura.NumerKSeF)].Visible = false;
		Columns[nameof(Faktura.CzyTP)].Visible = false;
		Columns[nameof(Faktura.CzyWDT)].Visible = false;
	}
}

class RachunkiSprzedazyBezNabywcySpis : RachunkiSprzedazySpis
{
	protected override bool CzyWidocznyNabywca => false;
}
