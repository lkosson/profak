using ProFak.DB;

namespace ProFak.UI
{
	class UsunPozycjeFakturyAkcja : UsunRekordAkcja<PozycjaFaktury>
	{
		public override bool CzyDostepnaDlaRekordow(IEnumerable<PozycjaFaktury> zaznaczoneRekordy) => base.CzyDostepnaDlaRekordow(zaznaczoneRekordy) && !zaznaczoneRekordy.Any(faktura => faktura.CzyPrzedKorekta);
	}
}
