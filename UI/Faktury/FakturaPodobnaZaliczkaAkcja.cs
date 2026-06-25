using ProFak.DB;

namespace ProFak.UI;

class FakturaPodobnaZaliczkaAkcja : FakturaPodobnaAkcja
{
	public override string Nazwa => "➕ Wystaw podobną";
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => false;
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1 && zaznaczoneRekordy.All(e => e.Rodzaj == RodzajFaktury.Zaliczka);
}
