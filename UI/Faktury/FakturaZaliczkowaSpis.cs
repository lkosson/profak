using ProFak.DB;

namespace ProFak.UI;

class FakturaZaliczkowaSpis : FakturaSpis
{
	protected override bool CzyWidocznyNabywca => true;
	protected override RodzajFaktury[] Rodzaje => [RodzajFaktury.Zaliczka, RodzajFaktury.KorektaZaliczki, RodzajFaktury.Rozliczenie, RodzajFaktury.KorektaRozliczenia];
}
