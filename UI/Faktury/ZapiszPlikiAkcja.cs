using ProFak.DB;

namespace ProFak.UI;

class ZapiszPlikiAkcja : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "🖫 Zapisz pliki";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any(e => e.Pliki?.Count > 0);

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		using var nowyKontekst = new Kontekst(kontekst);
		using var transakcja = nowyKontekst.Transakcja();
		using var dialog = new FolderBrowserDialog();
		dialog.AutoUpgradeEnabled = false;
		dialog.Description = "Wybierz folder, do którego mają zostać zapisane pliki.";
		if (dialog.ShowDialog() != DialogResult.OK) return;
		var katalog = dialog.SelectedPath;
		var liczbaPlikow = 0;
		foreach (var faktura in zaznaczoneRekordy)
		{
			foreach (var plik in faktura.Pliki)
			{
				var zawartosc = nowyKontekst.Baza.Znajdz(plik.ZawartoscRef);
				File.WriteAllBytes(Path.Combine(katalog, plik.Nazwa), zawartosc.Dane);
				liczbaPlikow++;
			}
		}
		MessageBox.Show($"Liczba zapisanych plików: {liczbaPlikow}.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}
}
