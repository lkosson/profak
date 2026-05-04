using ProFak.DB;

namespace ProFak.UI;

class FakturaPodobnaAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
{
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => modyfikatory == TKeyModifiers.Shift && klawisz == TKeys.Insert;

	protected override Faktura? UtworzRekord(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
	{
		var zaznaczona = zaznaczoneRekordy.Single();
		var podobna = zaznaczona.PrzygotujPodobna(kontekst.Baza);
		return podobna;
	}

	protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
	{
		rekord.ZakonczWystawianie(kontekst.Baza);
		base.ZapiszRekord(kontekst, rekord);
	}
}
