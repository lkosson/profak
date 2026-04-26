namespace ProFak.UI;

class PanelAkcji : Pionowo
{
	public bool CzyGlownySpis { get; set; }

	private readonly List<(TButton przycisk, AdapterAkcji adapter)> przyciski = [];

	public PanelAkcji()
		: base([])
	{
		MinimumSize = new Size(200 * DeviceDpi / 96, 200 * DeviceDpi / 96);
		TabIndex = 100;
	}

	public void UstawUklad(IEnumerable<TControl> naglowek, IEnumerable<AdapterAkcji> adaptery, IEnumerable<TControl> stopka)
	{
		SuspendLayout();
		foreach (var kontrolka in naglowek) DodajKontrolke(kontrolka);
		foreach (var adapter in adaptery) DodajAkcje(adapter);
		foreach (var kontrolka in stopka) DodajKontrolke(kontrolka);
		ResumeLayout();
	}

	private void DodajKontrolke(TControl kontrolka)
	{
		DodajWiersz(kontrolka);
	}

	private void DodajAkcje(AdapterAkcji adapter)
	{
		TButton przycisk;
		if (adapter.Podrzedne.Count > 0)
		{
			var pozycje = new List<TMenuItem>();
			foreach (var podrzedna in adapter.Podrzedne)
			{
				var pozycja = Kontrolki.MenuItem(Wyglad.NazwaAkcji(podrzedna), podrzedna.Uruchom);
				pozycja.Dock = DockStyle.Fill;
				pozycja.TextAlign = ContentAlignment.MiddleLeft;
				pozycje.Add(pozycja);
			}
			przycisk = Kontrolki.ButtonMenu("", pozycje.ToArray());
		}
		else
		{
			przycisk = Kontrolki.Button("", adapter.Uruchom);
		}

		AktualizujPrzycisk(przycisk, adapter);
		DodajKontrolke(przycisk);
		przyciski.Add((przycisk, adapter));
	}

	public void Aktualizuj()
	{
		foreach ((var przycisk, var adapter) in przyciski)
		{
			AktualizujPrzycisk(przycisk, adapter);
		}
	}

	private void AktualizujPrzycisk(TButton przycisk, AdapterAkcji adapter)
	{
		przycisk.Text = Wyglad.NazwaAkcji(adapter);
		przycisk.Enabled = adapter.CzyDostepna;
	}

	public bool ObsluzKlawisz(Keys klawisz, Keys modyfikatory)
	{
		foreach ((_, var adapter) in przyciski)
		{
			if (adapter.CzyKlawiszSkrotu(klawisz, modyfikatory) && adapter.CzyDostepna)
			{
				adapter.Uruchom();
				return true;
			}
		}
		return false;
	}
}
