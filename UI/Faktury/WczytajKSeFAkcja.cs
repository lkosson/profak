using ProFak.DB;
using System.Text.RegularExpressions;

namespace ProFak.UI;

class WczytajKSeFAkcja : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "➕ Wczytaj XML KSeF [CTRL-K]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => true;
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => klawisz == TKeys.K && modyfikatory == TKeyModifiers.Control;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		var pliki = OknoWyboruPliku.OtworzWiele("Wybierz e-Faktury do załadowania", "e-Faktura XML", "*.xml");
		if (pliki == null) return;
		var rekordy = new List<Faktura>();
		var pominOkno = false;
		if (pliki.Length > 1)
		{
			var odp = OknoKomunikatu.PytanieTakNieAnuluj("Wybrano więcej niż jeden plik do importu. Czy wczytać faktury w ciemno, bez wyświetlania formularza edycji dla każdej z nich?", domyslnie: false);
			if (odp is null) return;
			if (odp is false) pominOkno = true;
		}

		for (var i = 0; i < pliki.Length; i++)
		{
			var plik = pliki[i];
			using var nowyKontekst = new Kontekst(kontekst);
			using var transakcja = nowyKontekst.Transakcja();
			var faktura = DodajFakture(nowyKontekst, plik, pominOkno);
			if (faktura == null)
			{
				if (i < pliki.Length - 1 && !OknoKomunikatu.PytanieTakNie("Kontynuować dodawanie faktur ze wskazanych plików?"))
					break;
				continue;
			}
			transakcja.Zatwierdz();
			rekordy.Add(faktura);
		}
		zaznaczoneRekordy = rekordy;
	}

	private Faktura? DodajFakture(Kontekst kontekst, string plik, bool pominOkno)
	{
		var xml = File.ReadAllText(plik);
		Faktura faktura;
		try
		{
			faktura = IO.FA_3.Generator.ZbudujDB(kontekst.Baza, xml);
		}
		catch (Exception exc)
		{
			OknoKomunikatu.Ostrzezenie($"Wczytanie faktury z pliku {plik} nie powiodło się ({exc.Message}).");
			return null;
		}
		faktura.DataKSeF = DateTime.Now;
		var numerKsef = Path.GetFileNameWithoutExtension(plik);
		if (Regex.IsMatch(numerKsef, @"\d{10}-\d{8}-[0-9A-Fa-f]{12}-[0-9A-Fa-f]{2}"))
		{
			var istniejaca = kontekst.Baza.Faktury.FirstOrDefault(e => e.NumerKSeF == numerKsef && e.Rodzaj != RodzajFaktury.Usunięta);
			if (istniejaca != null && !OknoKomunikatu.PytanieTakNie($"Faktura {istniejaca.Numer} ({istniejaca.NumerKSeF}) już istnieje w bazie. Czy mimo to chcesz ją dodać ponownie?", domyslnie: false))
				return null;
			faktura.NumerKSeF = numerKsef;
		}
		kontekst.Baza.Zapisz(faktura);

		if (!pominOkno)
		{
			kontekst.Dodaj(faktura);
			using var edytor = new FakturaEdytor();
			edytor.Przygotuj(kontekst, faktura);
			if (!DialogEdycji.Pokaz("Nowa pozycja", edytor, kontekst)) return null;
			edytor.KoniecEdycji();
			kontekst.Baza.Zapisz(faktura);
		}

		return faktura;
	}
}
