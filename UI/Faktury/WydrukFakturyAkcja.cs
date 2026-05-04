using ProFak.DB;

namespace ProFak.UI;

class WydrukFakturyAkcja : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "🖶 Drukuj [CTRL-P]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any() && !zaznaczoneRekordy.Any(e => !e.Numerator.HasValue);
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => klawisz == TKeys.P && modyfikatory == TKeyModifiers.Control;
	protected virtual bool CzyDuplikat => false;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		var wydruk = new Wydruki.Faktura(kontekst.Baza, zaznaczoneRekordy.Select(faktura => faktura.Ref), CzyDuplikat);
		using var okno = new OknoWydruku(wydruk);
		okno.ShowDialog();
	}
}
