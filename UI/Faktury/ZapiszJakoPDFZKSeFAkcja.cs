using ProFak.DB;
using System.Diagnostics;

namespace ProFak.UI;

class ZapiszJakoPDFZKSeFAkcja : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "🖫 Zapisz jako PDF KSeF";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) 
		=> zaznaczoneRekordy.Any(e => !String.IsNullOrEmpty(e.XMLKSeF));

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		var faktury = zaznaczoneRekordy.Where(e => !String.IsNullOrEmpty(e.XMLKSeF)).ToList();
		if (faktury.Count == 0)
		{
			MessageBox.Show("Żadna z wybranych faktur nie posiada danych KSeF.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return;
		}

		if (faktury.Count == 1)
		{
			var faktura = faktury[0];
			using var dialog = new SaveFileDialog();
			dialog.Filter = "Dokument PDF (*.pdf)|*.pdf";
			dialog.Title = "Zapisywanie wizualizacji faktury KSeF";
			dialog.RestoreDirectory = true;
			dialog.FileName = faktura.NumerKSeFJakoNazwaPliku + ".pdf";
			if (dialog.ShowDialog() != DialogResult.OK) return;
			
			byte[] pdf = [];
			OknoPostepu.Uruchom(cancellationToken =>
			{
				pdf = IO.KSEFPDF.Generator.ZbudujPDF(faktura.XMLKSeF, faktura.NumerKSeF, cancellationToken);
				return Task.CompletedTask;
			});
			File.WriteAllBytes(dialog.FileName, pdf);
			Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = dialog.FileName });
		}
		else
		{
			using var dialog = new FolderBrowserDialog();
			dialog.AutoUpgradeEnabled = false;
			dialog.Description = "Wybierz folder, do którego mają zostać zapisane pliki PDF.";
			if (dialog.ShowDialog() != DialogResult.OK) return;
			var katalog = dialog.SelectedPath;
			
			var liczbaPlikow = 0;
			OknoPostepu.Uruchom(cancellationToken =>
			{
				foreach (var faktura in faktury)
				{
					var pdf = IO.KSEFPDF.Generator.ZbudujPDF(faktura.XMLKSeF, faktura.NumerKSeF, cancellationToken);
					var nazwaPliku = faktura.NumerKSeFJakoNazwaPliku + ".pdf";
					File.WriteAllBytes(Path.Combine(katalog, nazwaPliku), pdf);
					liczbaPlikow++;
				}
				return Task.CompletedTask;
			});
			MessageBox.Show($"Liczba wygenerowanych plików PDF: {liczbaPlikow}.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
