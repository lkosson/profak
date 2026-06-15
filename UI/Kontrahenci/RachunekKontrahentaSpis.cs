using Microsoft.EntityFrameworkCore;
using ProFak.DB;

namespace ProFak.UI;

class RachunekKontrahentaSpis : Spis<RachunekKontrahenta>
{
	public Ref<Kontrahent> KontrahentRef { get; set; }

	public RachunekKontrahentaSpis()
	{
		DodajKolumne(nameof(RachunekKontrahenta.NumerRachunku), "Numer rachunku", rozciagnij: true);
		DodajKolumne(nameof(RachunekKontrahenta.NazwaBanku), "Nazwa banku");
		DodajKolumne(nameof(RachunekKontrahenta.WalutaFmt), "Waluta");
		DodajKolumneId();
	}

	protected override void Przeladuj()
	{
		IQueryable<RachunekKontrahenta> q = Kontekst.Baza.RachunkiKontrahentow.Include(rachunekKontrahenta => rachunekKontrahenta.Waluta);
		if (KontrahentRef.IsNotNull) q = q.Where(wplata => wplata.KontrahentId == KontrahentRef.Id);
		q = q.OrderBy(rachunekKontrahenta => rachunekKontrahenta.NumerRachunku);
		Rekordy = q.ToList();
	}
}
