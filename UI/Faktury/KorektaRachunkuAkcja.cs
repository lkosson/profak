using ProFak.DB;

namespace ProFak.UI;

class KorektaRachunkuAkcja : KorektaAkcja<FakturaEdytor>
{
	public override string Nazwa => "➕ Wystaw korektę rachunku";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;//&& zaznaczoneRekordy.Single().Rodzaj ==  RodzajFaktury.Rachunek;

	protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
	{
		rekord.NadajNumer(kontekst.Baza);
		base.ZapiszRekord(kontekst, rekord);
	}
}
