using ProFak.DB;

namespace ProFak.UI;

class WczytajJPK_FAAkcja : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "➕ Wczytaj JPK_FA";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => true;
	public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => false;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		using var dialog = new OpenFileDialog();
		dialog.Filter = "Zestawienie JPK_FA (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
		dialog.Title = "Wybierz zestawienie do załadowania";
		dialog.RestoreDirectory = true;
		if (dialog.ShowDialog() != DialogResult.OK) return;

		using var nowyKontekst = new Kontekst(kontekst);
		using var transakcja = nowyKontekst.Transakcja();

		using var fs = File.OpenRead(dialog.FileName);
		IO.JPK_FA.Importer.Wczytaj(fs, nowyKontekst);

		transakcja.Zatwierdz();
	}
}
