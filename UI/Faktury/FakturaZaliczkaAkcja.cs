using ProFak.DB;

namespace ProFak.UI;

class FakturaZaliczkaAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
{
	public override string Nazwa => "➕ Wystaw zaliczkę";
	public FakturaZaliczkaAkcja()
		: base(faktura => faktura.Rodzaj = RodzajFaktury.Zaliczka)
	{
	}

	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => false;

	protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
	{
		rekord.ZakonczWystawianie(kontekst.Baza);
		base.ZapiszRekord(kontekst, rekord);
	}
}
