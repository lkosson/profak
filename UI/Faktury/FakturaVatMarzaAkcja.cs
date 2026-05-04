using ProFak.DB;

namespace ProFak.UI;

class FakturaVatMarzaAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
{
	public override string Nazwa => "➕ Wystaw vat marża";
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => false;

	public FakturaVatMarzaAkcja()
		: base(faktura => faktura.Rodzaj = RodzajFaktury.VatMarża)
	{
	}

	protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
	{
		rekord.ZakonczWystawianie(kontekst.Baza);
		base.ZapiszRekord(kontekst, rekord);
	}
}
