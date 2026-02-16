using ProFak.DB;

namespace ProFak.UI;

class DodajPozycjeFakturyAkcja : DodajRekordAkcja<PozycjaFaktury, PozycjaFakturyEdytor>
{
	public DodajPozycjeFakturyAkcja(Action<PozycjaFaktury> przygotujRekord)
		: base(przygotujRekord)
	{
	}

	protected override PozycjaFaktury? UtworzRekord(Kontekst kontekst, IEnumerable<PozycjaFaktury> zaznaczoneRekordy)
	{
		var rekord = base.UtworzRekord(kontekst, zaznaczoneRekordy);
		if (rekord == null) return null;
		var ostatniaIstniejacaPozycja = kontekst.Baza.PozycjeFaktur
			.Where(pozycja => pozycja.FakturaId == rekord.FakturaId && pozycja.Id != rekord.Id && !pozycja.CzyPrzedKorekta)
			.OrderByDescending(pozycja => pozycja.LP)
			.FirstOrDefault();
		if (ostatniaIstniejacaPozycja != null) rekord.LP = ostatniaIstniejacaPozycja.LP + 1;
		rekord.Ilosc = 1;
		rekord.CzyWedlugCenBrutto = kontekst.Znajdz<Faktura>() is Faktura faktura && (faktura.Rodzaj == RodzajFaktury.VatMarża || faktura.Rodzaj == RodzajFaktury.KorektaVatMarży);

		return rekord;
	}
}
