using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using ProFak.UI.Faktury;

namespace ProFak.UI
{
	class WyslijMailAkcja : AkcjaNaSpisie<Faktura>
	{
		public override string Nazwa => "✉ Wyślij e-mailem [CTRL-E]";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any();
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => klawisz == Keys.E && modyfikatory == Keys.Control;

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
		{
			using var nowyKontekst = new Kontekst(kontekst);
			using var edytor = new WysylkaFakturEdytor();
			using var okno = new Dialog("Wysyłka faktur", edytor, nowyKontekst);
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
			okno.CzyPrzyciskiWidoczne = false;
			edytor.Kontekst = nowyKontekst;
			edytor.Faktury = faktury;
			okno.ShowDialog();
		}
	}
}
