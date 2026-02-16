using ProFak.DB;

namespace ProFak.UI
{
	class KorektaSprzedazyAkcja : KorektaAkcja<FakturaEdytor>
	{
		public override string Nazwa => "➕ Wystaw korektę";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1 && zaznaczoneRekordy.Single().Rodzaj != RodzajFaktury.Proforma;

		protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
		{
			rekord.NadajNumer(kontekst.Baza);
			base.ZapiszRekord(kontekst, rekord);
		}
	}
}
