using ProFak.DB;

namespace ProFak.UI;

class WczytajJPK_FAAkcja : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "➕ Wczytaj JPK_FA";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => true;
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => false;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		var plik = OknoWyboruPliku.OtworzJeden("Wybierz zestawienie do załadowania", "Zestawienie JPK_FA", "*.xml");
		if (plik == null) return;

		using var nowyKontekst = new Kontekst(kontekst);
		using var transakcja = nowyKontekst.Transakcja();

		using var fs = File.OpenRead(plik);
		IO.JPK_FA.Importer.Wczytaj(fs, nowyKontekst.Baza);

		transakcja.Zatwierdz();
	}
}
