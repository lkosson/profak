using ProFak.DB;

namespace ProFak.UI;

class WalutaSpis : Spis<Waluta>
{
	public WalutaSpis()
	{
		DodajKolumne(nameof(Waluta.Skrot), "Skrót");
		DodajKolumne(nameof(Waluta.Nazwa), "Nazwa", rozciagnij: true);
		DodajKolumne(nameof(Waluta.CzyDomyslnaFmt), "Domyślna");
		DodajKolumneId();
	}

	protected override void Przeladuj()
	{
		Rekordy = Kontekst.Baza.Waluty.AsEnumerable().OrderBy(waluta => waluta.Skrot);
	}

	protected override bool CzyWierszPogrubiony(Waluta rekord) => rekord.CzyDomyslna;
}
