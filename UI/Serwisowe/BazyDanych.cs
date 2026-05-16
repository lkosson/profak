using Microsoft.EntityFrameworkCore;
using ProFak.DB;

namespace ProFak.UI;

class BazyDanych : Edytor, IKontrolkaZKontekstem
{
	public Kontekst Kontekst { get; set; } = default!;

	private bool gotowy;

	private readonly TTextBox textBoxRozmiar;
	private readonly TSuggestBox comboBoxPlik;
	private readonly TButton buttonUtworzKopie;
	private readonly TButton buttonPrzywrocKopie;
	private readonly TTextBox textBoxDataModyfikacji;
	private readonly TButton buttonPrzenies;
	private readonly TButton buttonZapiszJSON;
	private readonly TButton buttonWczytajJSON;

	public BazyDanych()
	{
		textBoxRozmiar = Kontrolki.TextBox();
		comboBoxPlik = Kontrolki.SuggestBox([Baza.PublicznaSciezka, Baza.PrywatnaSciezka, Baza.LokalnaSciezka]);
		textBoxDataModyfikacji = Kontrolki.TextBox();
		buttonPrzenies = Kontrolki.Button("Przenieś", Przenies);
		buttonZapiszJSON = Kontrolki.Button("Zapisz JSON", ZapiszJSON);
		buttonWczytajJSON = Kontrolki.Button("Wczytaj JSON", WczytajJSON);
		buttonUtworzKopie = Kontrolki.Button("Utwórz", UtworzKopie);
		buttonPrzywrocKopie = Kontrolki.Button("Przywróć", PrzywrocKopie);

		textBoxRozmiar.ReadOnly = true;
		textBoxDataModyfikacji.ReadOnly = true;

		var uklad = new Siatka([0, 500, 0, -1], []);
		uklad.DodajWiersz("Plik bazy danych", [comboBoxPlik, buttonPrzenies]);
		uklad.DodajWiersz("Rozmiar bazy", [(textBoxRozmiar, 2)]);
		uklad.DodajWiersz("Ostatnia modyfikacja", [(textBoxDataModyfikacji, 2)]);
		uklad.DodajWiersz("Kopia bezpieczeństwa", [(new Poziomo([buttonUtworzKopie, buttonPrzywrocKopie]), 2)]);
		uklad.DodajWiersz("Eksport danych", [(new Poziomo([buttonZapiszJSON, buttonWczytajJSON]), 2)]);

		UstawZawartosc(uklad);
	}

	protected override void EdytorGotowy()
	{
		base.EdytorGotowy();
		Wypelnij();
	}

	private void Wypelnij()
	{
		if (String.IsNullOrEmpty(Baza.Sciezka))
		{
			comboBoxPlik.Text = "(baza tymczasowa)";
			comboBoxPlik.Enabled = false;
			buttonUtworzKopie.Enabled = false;
			buttonPrzywrocKopie.Enabled = false;
			return;
		}
		gotowy = false;
		var plik = new FileInfo(Baza.Sciezka);
		comboBoxPlik.Text = plik.FullName;
		textBoxRozmiar.Text = plik.Length.ToString("#,##0");
		textBoxDataModyfikacji.Text = plik.LastWriteTime.ToString("d MMMM yyyy, H:mm:ss");
		if (Baza.Sciezka == Baza.PublicznaSciezka) comboBoxPlik.Text = Baza.PublicznaSciezka;
		if (Baza.Sciezka == Baza.PrywatnaSciezka) comboBoxPlik.Text = Baza.PrywatnaSciezka;
		if (Baza.Sciezka == Baza.LokalnaSciezka) comboBoxPlik.Text = Baza.LokalnaSciezka;
		gotowy = true;
	}

	private void Przenies()
	{
		if (!gotowy) return;
		var staryPlik = Baza.Sciezka;
		var nowyPlik = comboBoxPlik.Text;
		if (staryPlik == null)
		{
			OknoKomunikatu.Ostrzezenie("Nie można przenieść tymczasowej bazy danych.");
			return;
		}
		if (String.IsNullOrEmpty(nowyPlik) || staryPlik == nowyPlik)
		{
			OknoKomunikatu.Informacja("Wprowadź ręcznie lub wybierz z listy obok ścieżkę, do której ma zostać przeniesiona baza danych.");
			return;
		}
		if (File.Exists(nowyPlik))
		{
			OknoKomunikatu.Ostrzezenie($"Plik {nowyPlik} już istnieje. Przed przeniesieniem bazy przenieś go w inne miejsce.");
			return;
		}
		if (!OknoKomunikatu.PytanieTakNie("Czy na pewno chcesz przenieść bazę danych do nowego miejsca?", domyslnie: false))
		{
			gotowy = false;
			comboBoxPlik.Text = staryPlik;
			gotowy = true;
			return;
		}
		Baza.ZamknijPolaczenia();
		var katalog = Path.GetDirectoryName(nowyPlik)!;
		Directory.CreateDirectory(katalog);
		File.Move(staryPlik, nowyPlik);
		Baza.Sciezka = nowyPlik;
		Baza.ZapiszOdnosnikDoBazy();
		Wypelnij();
	}

	private void UtworzKopie()
	{
		var katalog = "";
		try
		{
			if (!Directory.Exists(Baza.KatalogKopiiZapasowych)) Directory.CreateDirectory(Baza.KatalogKopiiZapasowych);
			katalog = Baza.KatalogKopiiZapasowych;
		}
		catch
		{
		}

		var plik = OknoWyboruPliku.Zapisz("Wybierz gdzie zapisać kopię bazy", "Kopia zapasowa programu ProFak", "*.probak", $"profak-{DateTime.Now:yyyyMMdd}.probak", katalog);
		if (plik == null) return;
		try
		{
			Baza.WykonajKopie(plik);
			OknoKomunikatu.Informacja("Kopia bazy danych została zapisana.");
		}
		catch (Exception exc)
		{
			OknoBledu.Pokaz(exc);
		}
	}

	private void PrzywrocKopie()
	{
		if (String.IsNullOrEmpty(Baza.Sciezka))
		{
			OknoKomunikatu.Ostrzezenie("Nie można odtworzyć kopii zapasowej do tymczasowej bazy danych.");
			return;
		}
		var plik = OknoWyboruPliku.OtworzJeden("Wybierz kopię zapasową do załadowania", "Kopia zapasowa programu ProFak", "*.probak", Directory.Exists(Baza.KatalogKopiiZapasowych) ? Baza.KatalogKopiiZapasowych : null);
		if (plik == null) return;

		if (!OknoKomunikatu.PytanieTakNie("Dotychczasowe dane zostaną nadpisane. Czy na pewno chcesz kontynuować?", domyslnie: false)) return;
		var bazaRatunkowa = Baza.Sciezka + "-bak";
		if (File.Exists(bazaRatunkowa)) File.Delete(bazaRatunkowa);
		try
		{
			File.Move(Baza.Sciezka, bazaRatunkowa);
			// Tu nie potrzeba korzystać z mechanizmów SQLite'a - plik źródłowy nie jest aktywną bazą
			File.Copy(plik, Baza.Sciezka);
			using var baza = new DB.Baza();
			baza.Database.Migrate();
		}
		catch (Exception exc)
		{
			if (File.Exists(bazaRatunkowa))
			{
				if (File.Exists(Baza.Sciezka)) File.Delete(Baza.Sciezka);
				File.Move(bazaRatunkowa, Baza.Sciezka);
			}

			OknoBledu.Pokaz(exc);
			return;
		}
		OknoKomunikatu.Informacja("Baza danych została odtworzona.");
		Wypelnij();
	}

	private void ZapiszJSON()
	{
		var plik = OknoWyboruPliku.Zapisz("Wybierz gdzie zapisać kopię danych", "Dane programu ProFak", "*.json", $"profak-{DateTime.Now:yyyyMMdd}.json");
		if (plik == null) return;
		using var nowyKontekst = new Kontekst(Kontekst);
		var json = IO.Eksport.Generator.Zbuduj(nowyKontekst.Baza);
		File.WriteAllText(plik, json);
		OknoKomunikatu.Informacja("Dane programu zostały zapisane.");
	}

	private void WczytajJSON()
	{
		var plik = OknoWyboruPliku.OtworzJeden("Wybierz kopię danych do wczytania", "Dane programu ProFak", "*.json");
		if (plik == null) return;
		using var nowyKontekst = new Kontekst(Kontekst);
		var json = File.ReadAllText(plik);
		if (!OknoKomunikatu.PytanieTakNie("Dotychczasowe dane zostaną nadpisane. Czy na pewno chcesz kontynuować?", domyslnie: false)) return;
		using var tx = nowyKontekst.Transakcja();
		IO.Eksport.Generator.Wczytaj(nowyKontekst.Baza, json);
		tx.Zatwierdz();
		OknoKomunikatu.Informacja("Dane programu zostały wczytane.");
	}
}
