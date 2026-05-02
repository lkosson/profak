using ProFak.DB;
using System.Data;

namespace ProFak.UI;

partial class StawkaVatSpis : Spis<StawkaVat>
{
	public StawkaVatSpis()
	{
		DodajKolumne(nameof(StawkaVat.Skrot), "Skrót", rozciagnij: true);
		DodajKolumne(nameof(StawkaVat.Wartosc), "Wartość", wyrownajDoPrawej: true, format: "0");
		DodajKolumne(nameof(StawkaVat.CzyDomyslnaFmt), "Domyślna");
		DodajKolumneId();
	}

	protected override void Przeladuj()
	{
		Rekordy = Kontekst.Baza.StawkiVat.AsEnumerable().OrderBy(stawka => stawka.Skrot);
	}

	protected override bool CzyWierszPogrubiony(StawkaVat rekord) => rekord.CzyDomyslna;
}
