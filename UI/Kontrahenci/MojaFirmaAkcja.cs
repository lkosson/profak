using ProFak.DB;

namespace ProFak.UI
{
	class MojaFirmaAkcja : AkcjaNaSpisie<Kontrahent>
	{
		public override string Nazwa => "🏢 Moja firma";

		public override bool CzyDostepnaDlaRekordow(IEnumerable<Kontrahent> zaznaczoneRekordy) => true;

		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => false;

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<Kontrahent> zaznaczoneRekordy)
		{
			using var nowyKontekst = new Kontekst(kontekst);
			using var transakcja = nowyKontekst.Transakcja();
			var rekord = nowyKontekst.Baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot);
			if (rekord == null) rekord = new Kontrahent { CzyPodmiot = true };
			nowyKontekst.Dodaj(rekord);
			using var edytor = new KontrahentEdytor();
			using var okno = new Dialog("Edycja danych", edytor, nowyKontekst);
			edytor.Przygotuj(nowyKontekst, rekord);
			if (okno.ShowDialog() != DialogResult.OK) return;
			nowyKontekst.Baza.Zapisz(rekord);
			transakcja.Zatwierdz();
		}
	}
}
