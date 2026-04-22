namespace ProFak.UI;

class PanelAkcji : Pionowo
{
	public bool CzyGlownySpis { get; set; }

	private readonly List<(Button przycisk, AdapterAkcji adapter)> przyciski = [];

	public PanelAkcji()
	{
		MinimumSize = new Size(200 * DeviceDpi / 96, 50);
		TabIndex = 100;
	}

	public void DodajKontrolke(Control kontrolka)
	{
		DodajWiersz(kontrolka);
	}

	public void DodajAkcje(AdapterAkcji adapter)
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

	private void AktualizujPrzycisk(Button przycisk, AdapterAkcji adapter)
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
