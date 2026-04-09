using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;

namespace ProFak.UI;

class OProgramie : Edytor, IKontrolkaZKontekstem
{
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Kontekst Kontekst { get; set; } = default!;

	public OProgramie()
	{
		var labelNaglowek = Kontrolki.Text("ProFak", rozmiar: 16);
		var labelWersja = Kontrolki.Text(GetType().Assembly.GetName().Version!.ToString());
		var labelSciezka = Kontrolki.Text(Environment.ProcessPath!);
		var labelData = Kontrolki.Text(File.GetLastWriteTime(Environment.ProcessPath!).ToString("d MMMM yyyy, H:mm:ss"));
		var linkStrona = Kontrolki.Link("https://github.com/lkosson/profak", OtworzStrone);
		var buttonAktualizacje = Kontrolki.Button("Sprawdź aktualizacje", SprawdzAktualizacje);
		var labelLicencja = Kontrolki.Text("Twórcą i właścicielem praw autorskich do programu jest Łukasz Kosson. Program jest dostępny bezpłatnie i bezterminowo. Masz prawo korzystać z programu na dowolnej liczbie stanowisk, ale ponosisz pełną i wyłączną odpowiedzialność za wszelkie skutki korzystania z programu.");

		labelNaglowek.Margin = labelWersja.Margin = labelSciezka.Margin = labelData.Margin = buttonAktualizacje.Margin = linkStrona.Margin = labelLicencja.Margin = new Padding(10);
		labelNaglowek.TextAlign = labelWersja.TextAlign = labelSciezka.TextAlign = labelData.TextAlign = linkStrona.TextAlign = labelLicencja.TextAlign = ContentAlignment.MiddleCenter;
		var uklad = new Pionowo([labelNaglowek, labelWersja, labelSciezka, labelData, linkStrona, buttonAktualizacje, labelLicencja]);
		buttonAktualizacje.Anchor = AnchorStyles.None;
		buttonAktualizacje.Width = 120;

		UstawZawartosc(uklad, new Size(400, 0));
	}

	private void OtworzStrone()
	{
		Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "https://github.com/lkosson/profak/" });
	}

	private void SprawdzAktualizacje()
	{
		string? response = null;
		OknoPostepu.Uruchom(async cancellationToken =>
		{
			using var wb = new HttpClient();
			wb.DefaultRequestHeaders.UserAgent.ParseAdd("ProFak (https://github.com/lkosson/profak)");
			response = await wb.GetStringAsync("https://api.github.com/repos/lkosson/profak/releases/latest", cancellationToken);
			cancellationToken.ThrowIfCancellationRequested();
		});

		var json = JsonDocument.Parse(response!);
		Version.TryParse(json.RootElement.GetProperty("tag_name").ToString().Replace("v", ""), out var wersjaGitHub);
		var wersjaAplikacji = GetType().Assembly.GetName().Version;
		if (wersjaGitHub != null && wersjaGitHub > wersjaAplikacji)
		{
			if (MessageBox.Show("Dostępna jest nowa wersja " + wersjaGitHub.ToString() + ".\r\nCzy chcesz przejść do strony pobierania?", "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = json.RootElement.GetProperty("html_url").ToString() });
			}
		}
		else
		{
			MessageBox.Show("Nie znaleziono nowej wersji programu", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
