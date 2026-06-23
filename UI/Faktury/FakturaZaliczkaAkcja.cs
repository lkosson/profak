using ProFak.DB;

namespace ProFak.UI;

class FakturaZaliczkaAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
{
	public override string Nazwa => "➕ Wystaw zaliczkę [INS]";
	public FakturaZaliczkaAkcja()
		: base(faktura => faktura.Rodzaj = RodzajFaktury.Zaliczka)
	{
	}

	protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
	{
		rekord.ZakonczWystawianie(kontekst.Baza);
		base.ZapiszRekord(kontekst, rekord);
	}
}
