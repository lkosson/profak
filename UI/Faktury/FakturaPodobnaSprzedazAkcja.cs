using ProFak.DB;

namespace ProFak.UI;

class FakturaPodobnaSprzedazAkcja : FakturaPodobnaAkcja
{
	public override string Nazwa => "➕ Wystaw podobną [SHIFT-INS]";

	protected override Faktura? UtworzRekord(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
	{
		var faktura = base.UtworzRekord(kontekst, zaznaczoneRekordy);
		if (faktura?.Rodzaj == RodzajFaktury.Proforma)
		{
			var wynik = OknoKomunikatu.PytanieTakNieAnuluj("Czy chcesz wystawić zwykłą fakturę na podstawie zaznaczonej faktury pro forma?", domyslnie: false);
			if (wynik is true) faktura.Rodzaj = faktura.ProceduraMarzy == ProceduraMarży.NieDotyczy ? RodzajFaktury.Sprzedaż : RodzajFaktury.VatMarża;
			else if (wynik is null) return null;
		}
		return faktura;
	}
}
