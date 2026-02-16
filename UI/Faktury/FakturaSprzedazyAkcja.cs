using ProFak.DB;

namespace ProFak.UI;

class FakturaSprzedazyAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
{
	public override string Nazwa => "➕ Wystaw fakturę [INS]";

	public FakturaSprzedazyAkcja()
		: base(faktura => faktura.Rodzaj = RodzajFaktury.Sprzedaż)
	{
	}

	protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
	{
		rekord.NadajNumer(kontekst.Baza);
		base.ZapiszRekord(kontekst, rekord);
	}
}
