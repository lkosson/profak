using ProFak.DB;

namespace ProFak.UI;

class GenerujJPK_FAAkcja : AkcjaNaSpisie<DeklaracjaVat>
{
	public override string Nazwa => "Generuj JPK_FA";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<DeklaracjaVat> zaznaczoneRekordy) => zaznaczoneRekordy.Any();

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<DeklaracjaVat> zaznaczoneRekordy)
	{
		using var dialog = new SaveFileDialog();
		dialog.Filter = "Deklaracja JPK_FA (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
		dialog.Title = "Wybierz miejsce do zapisu JPK";
		dialog.RestoreDirectory = true;
		if (zaznaczoneRekordy.Count() == 1) dialog.FileName = $"jpk-fa-{zaznaczoneRekordy.Single().Miesiac:yyyy-MM}.xml";
		else dialog.FileName = $"jpk-fa-{zaznaczoneRekordy.Min(e => e.Miesiac):yyyy-MM}-{zaznaczoneRekordy.Max(e => e.Miesiac):yyyy-MM}.xml";
		if (dialog.ShowDialog() != DialogResult.OK) return;

		using var nowyKontekst = new Kontekst(kontekst);
		foreach (var deklaracja in zaznaczoneRekordy) nowyKontekst.Dodaj(deklaracja);
		IO.JPK_FA.Generator.Utworz(dialog.FileName, nowyKontekst.Baza, zaznaczoneRekordy);
		MessageBox.Show("Plik został zapisany pomyślnie.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
	}
}
