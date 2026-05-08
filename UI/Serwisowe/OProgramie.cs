using System.Diagnostics;
using System.Text.Json;

namespace ProFak.UI;

class OProgramie : Edytor, IKontrolkaZKontekstem
{
	public Kontekst Kontekst { get; set; } = default!;

	public OProgramie()
	{
		var labelNaglowek = Kontrolki.Text("ProFak", rozmiar: 16, wysrodkowany: true);
		var labelWersja = Kontrolki.Text(GetType().Assembly.GetName().Version!.ToString(), wysrodkowany: true);
		var labelSciezka = Kontrolki.Text(Environment.ProcessPath!, wysrodkowany: true);
		var labelData = Kontrolki.Text(File.GetLastWriteTime(Environment.ProcessPath!).ToString("d MMMM yyyy, H:mm:ss"), wysrodkowany: true);
		var linkStrona = Kontrolki.Link("https://github.com/lkosson/profak", OtworzStrone, wysrodkowany: true);
		var buttonAktualizacje = Kontrolki.Button("Sprawdź aktualizacje", SprawdzAktualizacje);
		var labelLicencja = Kontrolki.Text("Twórcą i właścicielem praw autorskich do programu jest Łukasz Kosson. Program jest dostępny bezpłatnie i bezterminowo. Masz prawo korzystać z programu na dowolnej liczbie stanowisk, ale ponosisz pełną i wyłączną odpowiedzialność za wszelkie skutki korzystania z programu.", wysrodkowany: true);

		labelNaglowek.Margin = labelWersja.Margin = labelSciezka.Margin = labelData.Margin = buttonAktualizacje.Margin = linkStrona.Margin = labelLicencja.Margin = new TPadding(10);
		var uklad = new Pionowo([labelNaglowek, labelWersja, labelSciezka, labelData, linkStrona, Siatka.BlokadaRozciagania(buttonAktualizacje), labelLicencja]);
		uklad.OgraniczSzerokosc(700);

		UstawZawartosc(uklad);
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
			if (!OknoKomunikatu.PytanieTakNie("Dostępna jest nowa wersja " + wersjaGitHub.ToString() + ".\r\nCzy chcesz przejść do strony pobierania?")) return;
			Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = json.RootElement.GetProperty("html_url").ToString() });
		}
		else
		{
			OknoKomunikatu.Informacja("Nie znaleziono nowej wersji programu");
		}
	}
}
