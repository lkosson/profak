using ProFak.DB;

namespace ProFak.UI;

class UsunKontrahentaAkcja : UsunRekordAkcja<Kontrahent>
{
	protected override void Usun(Kontekst kontekst, IEnumerable<Kontrahent> zaznaczoneRekordy)
	{
		foreach (var rekord in zaznaczoneRekordy)
		{
			var fakturySprzedazy = kontekst.Baza.Faktury.Where(faktura => faktura.SprzedawcaId == rekord.Id).ToList();
			var fakturyZakupu = kontekst.Baza.Faktury.Where(faktura => faktura.NabywcaId == rekord.Id).ToList();
			foreach (var faktura in fakturySprzedazy) faktura.SprzedawcaRef = default;
			foreach (var faktura in fakturyZakupu) faktura.NabywcaRef = default;
			kontekst.Baza.Zapisz(fakturySprzedazy);
			kontekst.Baza.Zapisz(fakturyZakupu);
		}
		base.Usun(kontekst, zaznaczoneRekordy);
	}
}
