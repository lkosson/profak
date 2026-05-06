using ProFak.DB;

namespace ProFak.UI;

class DodajPlikAkcja : AkcjaNaSpisie<Plik>
{
	private readonly PlikSpis spis;

	public override string Nazwa => "➕ Dołącz plik [INS]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Plik> zaznaczoneRekordy) => true;
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => modyfikatory == TKeyModifiers.None && klawisz == TKeys.Insert;

	public DodajPlikAkcja(PlikSpis spis)
	{
		this.spis = spis;
	}

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Plik> zaznaczoneRekordy)
	{
		var pliki = OknoWyboruPliku.OtworzWiele("Wybierz pliki do dołączenia do faktury");
		if (pliki == null) return;

		using var nowyKontekst = new Kontekst(kontekst);
		using var transakcja = nowyKontekst.Transakcja();

		foreach (var sciezka in pliki)
		{
			var dane = File.ReadAllBytes(sciezka);
			var nazwa = Path.GetFileName(sciezka);
			var zawartosc = new Zawartosc { Dane = dane };
			nowyKontekst.Baza.Zapisz(zawartosc);
			var plik = new Plik { FakturaId = spis.FakturaRef, Nazwa = nazwa, Rozmiar = dane.Length, ZawartoscRef = zawartosc };
			nowyKontekst.Baza.Zapisz(plik);
			zawartosc.PlikRef = plik;
			nowyKontekst.Baza.Zapisz(zawartosc);
		}

		transakcja.Zatwierdz();
	}
}
