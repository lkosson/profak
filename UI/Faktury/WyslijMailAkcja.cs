using Microsoft.EntityFrameworkCore;
using ProFak.DB;

namespace ProFak.UI;

class WyslijMailAkcja : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "✉ Wyślij e-mailem [CTRL-E]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any();
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => klawisz == TKeys.E && modyfikatory == TKeyModifiers.Control;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		using var nowyKontekst = new Kontekst(kontekst);
		using var edytor = new WysylkaFakturEdytor();
		var faktury = new List<Faktura>();
		foreach (var rekord in zaznaczoneRekordy)
		{
			var faktura = nowyKontekst.Baza.Faktury
				.Include(e => e.Sprzedawca)
				.Include(e => e.Nabywca)
				.Include(e => e.Waluta)
				.Include(e => e.SposobPlatnosci)
				.Where(e => e.Id == rekord.Id)
				.First();
			faktury.Add(faktura);
		}
		edytor.Kontekst = nowyKontekst;
		edytor.Faktury = faktury;
		Dialog.Pokaz("Wysyłka faktur", edytor, nowyKontekst);
	}
}
