using ProFak.DB;

namespace ProFak.UI;

class DodajWplateAkcja : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "💰 Dodaj wpłatę [CTRL-W]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any(faktura => !faktura.CzyZaplacona);
	public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => klawisz == Keys.W && modyfikatory == Keys.Control;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		using var nowyKontekst = new Kontekst(kontekst);
		using var transakcja = nowyKontekst.Transakcja();
		var wplata = new Wplata { Data = DateTime.Now.Date };
		if (zaznaczoneRekordy.Count() == 1)
		{
			var faktura = zaznaczoneRekordy.Single();
			wplata.FakturaRef = faktura;
			wplata.Kwota = faktura.PozostaloDoZaplaty;
			nowyKontekst.Dodaj(faktura);
			nowyKontekst.Baza.Zapisz(wplata);
		}
		nowyKontekst.Dodaj(wplata);
		using var edytor = new WplataEdytor();
		using var okno = new Dialog("Nowa wpłata", edytor, nowyKontekst);
		edytor.Przygotuj(nowyKontekst, wplata);
		if (okno.ShowDialog() != DialogResult.OK) return;
		edytor.KoniecEdycji();
		if (wplata.Id > 0)
		{
			nowyKontekst.Baza.Zapisz(wplata);
		}
		else
		{
			foreach (var faktura in zaznaczoneRekordy)
			{
				if (faktura.PozostaloDoZaplaty <= 0) continue;
				wplata.Id = 0;
				wplata.FakturaRef = faktura;
				wplata.Kwota = faktura.PozostaloDoZaplaty;
				nowyKontekst.Baza.Zapisz(wplata);
			}
		}
		transakcja.Zatwierdz();
	}
}
