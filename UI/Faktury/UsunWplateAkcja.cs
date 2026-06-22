using ProFak.DB;

namespace ProFak.UI;

class UsunWplateAkcja : UsunRekordAkcja<Wplata>
{
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Wplata> zaznaczoneRekordy) => base.CzyDostepnaDlaRekordow(zaznaczoneRekordy) && !zaznaczoneRekordy.Any(faktura => faktura.CzyZaliczka);
}
