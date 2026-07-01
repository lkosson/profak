using ProFak.DB;

namespace ProFak.UI;

class FakturaSprzedazySpis : FakturaSpis
{
	protected override bool CzyWidocznyNabywca => true;
	protected override RodzajFaktury[] Rodzaje => [RodzajFaktury.Sprzedaż,
		RodzajFaktury.KorektaSprzedaży,
		RodzajFaktury.VatMarża,
		RodzajFaktury.KorektaVatMarży,
		RodzajFaktury.Rozliczenie,
		RodzajFaktury.KorektaRozliczenia
	];
}

class FakturaSprzedazyBezNabywcySpis : FakturaSprzedazySpis
{
	protected override bool CzyWidocznyNabywca => false;
	protected override RodzajFaktury[] Rodzaje => [..base.Rodzaje,
		RodzajFaktury.Rachunek,
		RodzajFaktury.KorektaRachunku,
		RodzajFaktury.Zaliczka,
		RodzajFaktury.KorektaZaliczki,
		RodzajFaktury.Rozliczenie,
		RodzajFaktury.KorektaRozliczenia
	];
}
