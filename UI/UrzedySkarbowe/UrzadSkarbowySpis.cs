using ProFak.DB;

namespace ProFak.UI
{
	class UrzadSkarbowySpis : Spis<UrzadSkarbowy>
	{
		public UrzadSkarbowySpis()
		{
			DodajKolumne(nameof(UrzadSkarbowy.Kod), "Kod");
			DodajKolumne(nameof(UrzadSkarbowy.Nazwa), "Nazwa", rozciagnij: true);
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.UrzedySkarbowe.AsEnumerable().OrderBy(urzad => urzad.Kod);
		}
	}
}
