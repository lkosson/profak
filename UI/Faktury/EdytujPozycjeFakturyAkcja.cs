using ProFak.DB;

namespace ProFak.UI
{
	class EdytujPozycjeFakturyAkcja : EdytujRekordAkcja<PozycjaFaktury, PozycjaFakturyEdytor>
	{
		public override bool CzyDostepnaDlaRekordow(IEnumerable<PozycjaFaktury> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1 && zaznaczoneRekordy.Single().Ilosc >= 0;
	}
}
