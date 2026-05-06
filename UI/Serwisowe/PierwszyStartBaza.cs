using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace ProFak.UI;

class PierwszyStartBaza : Dialog
{
	private const string ZnacznikPierwszegoUruchomienia = "pierwsze-uruchomienie.txt";
	private const string BazaDemo = "(demo)";
	private string? bazaZrodlowa;
	private string? bazaDocelowa;
	private bool sukces;

	private readonly TRadioButton radioButtonNowaPrywatnaBaza;
	private readonly TRadioButton radioButtonNowaPublicznaBaza;
	private readonly TRadioButton radioButtonNowaLokalnaBaza;
	private readonly TRadioButton radioButtonZewnetrznaBaza;
	private readonly TRadioButton radioButtonBazaDemo;
	private readonly TRadioButton radioButtonOdtworzKopie;
	private readonly TButton buttonDalej;
	private readonly TProgressBar progressBar;
	private readonly TLabel labelStatus;
	private readonly BackgroundWorker backgroundWorker;

	private PierwszyStartBaza(Kontekst kontekst)
		: base("ProFak - Pierwsze uruchomienie", kontekst)
	{
		var labelNaglowek1 = Kontrolki.Text("Wygląda na to, że program jeszcze nie był uruchamiany na tym komputerze. Przed rozpoczęciem pracy konieczne jest przygotowanie bazy danych.");
		var labelNaglowek2 = Kontrolki.Text("Zaznacz jeden z poniższych punktów i kliknij \"Dalej\". Jeśli nie wiesz co wybrać i chcesz po prostu zacząć korzystać z programu, zostaw domyślny wybór bez zmian i kliknij \"Dalej\".");
		radioButtonNowaPrywatnaBaza = Kontrolki.RadioButton("Utwórz nową, pustą bazę danych, dostępną tylko dla bieżącego użytkownika komputera.");
		radioButtonNowaPublicznaBaza = Kontrolki.RadioButton("Utwórz nową, pustą bazę danych, dostępną dla wszystkich użytkowników tego komputera.");
		radioButtonNowaLokalnaBaza = Kontrolki.RadioButton("Utwórz nową, pustą bazę danych w katalogu w którym został uruchomiony program.");
		radioButtonZewnetrznaBaza = Kontrolki.RadioButton("Uruchom program korzystając z zewnętrznej bazy danych we wskazanym katalogu.");
		radioButtonBazaDemo = Kontrolki.RadioButton("Uruchom program w trybie demonstracyjnym z przykładowymi danymi, w tymczasowej bazie danych.");
		radioButtonOdtworzKopie = Kontrolki.RadioButton("Odtwórz bazę danych ze wskazanego pliku z kopią zapasową.");
		buttonDalej = Kontrolki.Button("Dalej", Dalej);
		progressBar = Kontrolki.ProgressBar();
		labelStatus = Kontrolki.Label("");
		backgroundWorker = new BackgroundWorker();

		radioButtonNowaPrywatnaBaza.Enabled = CzySciezkaDostepna(DB.Baza.PrywatnaSciezka);
		radioButtonNowaPublicznaBaza.Enabled = CzySciezkaDostepna(DB.Baza.PublicznaSciezka);
		radioButtonNowaLokalnaBaza.Enabled = CzySciezkaDostepna(DB.Baza.LokalnaSciezka);
		labelNaglowek1.Margin = new TPadding(3);
		labelNaglowek2.Margin = new TPadding(3);
		progressBar.Visible = false;
		backgroundWorker.WorkerReportsProgress = true;
		backgroundWorker.DoWork += backgroundWorker_DoWork;
		backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
		backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;

		if (radioButtonNowaPrywatnaBaza.Enabled) radioButtonNowaPrywatnaBaza.Checked = true;
		else if (radioButtonNowaPublicznaBaza.Enabled) radioButtonNowaPublicznaBaza.Checked = true;
		else if (radioButtonNowaLokalnaBaza.Enabled) radioButtonNowaLokalnaBaza.Checked = true;
		else radioButtonBazaDemo.Checked = true;

		var uklad = new Pionowo([
			labelNaglowek1,
			labelNaglowek2,
			radioButtonNowaPrywatnaBaza,
			radioButtonNowaPublicznaBaza,
			radioButtonNowaLokalnaBaza,
			radioButtonZewnetrznaBaza,
			radioButtonBazaDemo,
			radioButtonOdtworzKopie,
			new Poziomo([buttonDalej, progressBar, labelStatus])
			]);
		uklad.Margin = new TPadding(10);
		uklad.OgraniczSzerokosc(600);

		UstawZawartosc(uklad);

		//AcceptButton = buttonDalej;
	}

	private bool CzySciezkaDostepna(string sciezka)
	{
		try
		{
			if (File.Exists(sciezka))
			{
				using var f = new FileStream(sciezka, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				return true;
			}
			else
			{
				Directory.CreateDirectory(Path.GetDirectoryName(sciezka)!);
				using var f = new FileStream(sciezka, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite, 4096, FileOptions.DeleteOnClose);
				return true;
			}
		}
		catch
		{
			return false;
		}
	}

	private void Dalej()
	{
		if (radioButtonNowaPrywatnaBaza.Checked)
		{
			bazaZrodlowa = null;
			bazaDocelowa = DB.Baza.PrywatnaSciezka;
		}
		else if (radioButtonNowaPublicznaBaza.Checked)
		{
			bazaZrodlowa = null;
			bazaDocelowa = DB.Baza.PublicznaSciezka;
		}
		else if (radioButtonNowaLokalnaBaza.Checked)
		{
			bazaZrodlowa = null;
			bazaDocelowa = DB.Baza.LokalnaSciezka;
		}
		else if (radioButtonZewnetrznaBaza.Checked)
		{
			var plik = OknoWyboruPliku.OtworzJeden("Wybierz zewnętrzną bazę danych", "Baza danych ProFak", "profak.sqlite3");
			if (plik == null) return;
			bazaZrodlowa = plik;
			bazaDocelowa = null;
		}
		else if (radioButtonBazaDemo.Checked)
		{
			bazaZrodlowa = BazaDemo;
			bazaDocelowa = null;
		}
		else if (radioButtonOdtworzKopie.Checked)
		{
			var plik = OknoWyboruPliku.OtworzJeden("Wybierz kopię zapasową bazę danych", "Kopia zapasowa programu ProFak", "*.probak");
			if (plik == null) return;
			bazaZrodlowa = plik;
			bazaDocelowa = DB.Baza.PrywatnaSciezka;
		}
		else return;

		if (File.Exists(bazaDocelowa) && !OknoKomunikatu.PytanieTakNie($"Baza {bazaDocelowa} już istnieje. Czy na pewno chcesz ją nadpisać i utworzyć w jej miejsce pustą bazę?\n\nTen proces jest nieodwracalny i STRACISZ WSZYSTKIE ISTNIEJĄCE DANE.", domyslnie: false)) return;

		buttonDalej.Enabled = false;
		progressBar.Visible = true;
		labelStatus.Visible = true;
		labelStatus.Text = "Inicjalizacja";
		backgroundWorker.RunWorkerAsync();
	}

	private void backgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
	{
		backgroundWorker.ReportProgress(0, "Weryfikacja bazy źródłowej");
		var zrodloJestPlikiem = !String.IsNullOrEmpty(bazaZrodlowa) && bazaZrodlowa != BazaDemo;
		if (zrodloJestPlikiem && !File.Exists(bazaZrodlowa)) throw new ApplicationException($"Nie znaleziono pliku \"{bazaZrodlowa}\" z bazą danych.");

		if (String.IsNullOrEmpty(bazaDocelowa))
		{
			if (zrodloJestPlikiem)
			{
				DB.Baza.Sciezka = bazaZrodlowa;
				DB.Baza.ZapiszOdnosnikDoBazy();
			}
			else DB.Baza.Sciezka = null;
		}
		else
		{
			backgroundWorker.ReportProgress(0, "Przygotowanie miejsca na bazę docelową");
			if (File.Exists(bazaDocelowa))
			{
				var kopiaBazyDocelowej = bazaDocelowa + "-bak";
				if (File.Exists(kopiaBazyDocelowej))
				{
					backgroundWorker.ReportProgress(0, "Kasowanie starej kopii zapasowej");
					File.Delete(kopiaBazyDocelowej);
				}
				backgroundWorker.ReportProgress(0, "Tworzenie kopii zapasowej");
				File.Move(bazaDocelowa, kopiaBazyDocelowej);
			}

			var katalogDocelowy = Path.GetDirectoryName(bazaDocelowa)!;
			Directory.CreateDirectory(katalogDocelowy);

			if (zrodloJestPlikiem)
			{
				backgroundWorker.ReportProgress(0, "Kopiowanie bazy");
				// Tu nie potrzeba korzystać z mechanizmów SQLite'a - plik źródłowy nie jest aktywną bazą
				File.Copy(bazaZrodlowa!, bazaDocelowa);
			}

			DB.Baza.Sciezka = bazaDocelowa;
		}

		backgroundWorker.ReportProgress(0, "Podłączanie bazy");
		using var db = new DB.Baza();

		backgroundWorker.ReportProgress(0, "Aktualizacja struktury");
		db.Database.Migrate();

		backgroundWorker.ReportProgress(0, "Tworzenie danych startowych");
		DB.DaneStartowe.Zaladuj(db);

		if (bazaZrodlowa == BazaDemo)
		{
			backgroundWorker.ReportProgress(0, "Tworzenie danych demo");
			DB.DaneDemo.Zaladuj(db);
		}

		backgroundWorker.ReportProgress(0, "Baza gotowa");
	}

	private void backgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
	{
		if (e.Error == null)
		{
			sukces = true;
			Zamknij();
			return;
		}

		OknoBledu.Pokaz(e.Error);
		buttonDalej.Enabled = true;
		progressBar.Visible = false;
		labelStatus.Visible = false;
	}

	private void backgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
	{
		labelStatus.Text = (string?)e.UserState ?? "";
	}

	public static bool Uruchom()
	{
		var pierwszeUruchomienieWersjiPrzenosnej = false;
		var plikPierwszegoUruchomienia = Path.Combine(DB.Baza.LokalnyKatalog, ZnacznikPierwszegoUruchomienia);
		if (!File.Exists(plikPierwszegoUruchomienia))
		{
			try
			{
				File.WriteAllText(plikPierwszegoUruchomienia, "Ten plik jest znacznikiem, że program był już uruchamiany z tego katalogu.\r\nUsuń go, jeśli chcesz móc ponownie wybrać lokalizację bazy danych.");
				pierwszeUruchomienieWersjiPrzenosnej = true;
			}
			catch
			{
			}
		}

		if (File.Exists(DB.Baza.Sciezka))
		{
			if (!pierwszeUruchomienieWersjiPrzenosnej) return true;
			var ret = OknoKomunikatu.PytanieTakNieAnuluj($"Została znaleziona istniejąca baza danych: {DB.Baza.Sciezka}\n\nCzy chcesz jej użyć?", domyslnie: true);
			if (ret is true) return true;
			if (ret is null)
			{
				File.Delete(plikPierwszegoUruchomienia);
				return false;
			}
		}

		using var kontekst = new Kontekst();
		using var pierwszyStart = new PierwszyStartBaza(kontekst);
		pierwszyStart.Pokaz();
		if (pierwszeUruchomienieWersjiPrzenosnej && (!pierwszyStart.sukces || String.IsNullOrEmpty(DB.Baza.Sciezka))) File.Delete(plikPierwszegoUruchomienia);
		return pierwszyStart.sukces;
	}
}
