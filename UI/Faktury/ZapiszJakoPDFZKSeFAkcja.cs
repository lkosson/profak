using ProFak.DB;
using System.Diagnostics;

namespace ProFak.UI;

class ZapiszJakoPDFZKSeFAkcja(bool spisKSeF = false) : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "🖫 Zapisz PDF KSeF [CTRL-P]";
	public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.Control && klawisz == Keys.P;
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) 
		=> zaznaczoneRekordy.Any(e => !String.IsNullOrEmpty(e.NumerKSeF));
	public override bool PrzeladujPoZakonczeniu => false;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		var faktury = zaznaczoneRekordy.Where(e => !String.IsNullOrEmpty(e.NumerKSeF)).ToList();
		if (faktury.Count == 0)
		{
			OknoKomunikatu.Ostrzezenie("Żadna z wybranych faktur nie posiada danych KSeF.");
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
			OknoPostepu.Uruchom(async cancellationToken =>
			{
				if (spisKSeF && String.IsNullOrEmpty(faktura.XMLKSeF))
				{
					var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
					using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
					await api.UwierzytelnijAsync(podmiot.NIP, podmiot.TokenKSeF, cancellationToken);
					faktura.XMLKSeF = await api.PobierzFaktureAsync(faktura.NumerKSeF, cancellationToken);
				}
				pdf = IO.KSEFPDF.Generator.ZbudujPDF(faktura.XMLKSeF, faktura.NumerKSeF, cancellationToken);
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
			OknoPostepu.Uruchom(async cancellationToken =>
			{
				if (spisKSeF)
				{
					var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
					using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
					await api.UwierzytelnijAsync(podmiot.NIP, podmiot.TokenKSeF, cancellationToken);
					foreach (var naglowek in faktury)
					{
						if (!String.IsNullOrEmpty(naglowek.XMLKSeF)) continue;
						naglowek.XMLKSeF = await api.PobierzFaktureAsync(naglowek.NumerKSeF, cancellationToken);
						cancellationToken.ThrowIfCancellationRequested();
					}
				}

				foreach (var faktura in faktury)
				{
					var pdf = IO.KSEFPDF.Generator.ZbudujPDF(faktura.XMLKSeF, faktura.NumerKSeF, cancellationToken);
					var nazwaPliku = faktura.NumerKSeFJakoNazwaPliku + ".pdf";
					File.WriteAllBytes(Path.Combine(katalog, nazwaPliku), pdf);
					liczbaPlikow++;
				}
			});
			OknoKomunikatu.Informacja($"Liczba wygenerowanych plików PDF: {liczbaPlikow}.");
		}
	}
}
