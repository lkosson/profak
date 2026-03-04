using ProFak.DB;

namespace ProFak.UI;

class RachunekAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
{
	public override string Nazwa => "➕ Wystaw rachunek";

	public RachunekAkcja()
		: base(faktura => faktura.Rodzaj = RodzajFaktury.Rachunek)
	{
	}

	protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
	{
		rekord.NadajNumer(kontekst.Baza);
		base.ZapiszRekord(kontekst, rekord);
	}
}
