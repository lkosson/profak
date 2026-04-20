using ProFak.DB;
using System.Linq.Expressions;

namespace ProFak.UI;

partial class Spis
{
}

abstract partial class Spis<T> : Spis
	where T : Rekord<T>
{
	private const string WYSOKOSC_WIERSZA = "(wysokość)";
	private IEnumerable<T>? oryginalneRekordy;
	private Func<T, bool> filtr = x => true;
	private readonly List<(string kolumna, bool malejaco, Func<T, IComparable> metoda)> kolumnyKolejnosci = [];
	private readonly Dictionary<int, Func<T, string?>> tooltipyDlaKolumn = [];
	private bool rekordyPodczasZmiany;
	private bool kolumnyZmienione;

	public Kontekst Kontekst { get; set; } = default!;
	public IEnumerable<T> WybraneRekordy
	{
		get => Sortuj(WybraneRekordyImpl);
		set => WybraneRekordyImpl = value;
	}

	public IEnumerable<T> Rekordy
	{
		get => RekordyImpl ?? [];
		set
		{
			var zaznaczoneRekordy = WybraneRekordy.ToList();
			oryginalneRekordy = value;
			var rekordy = Sortuj(value.Where(filtr)).ToList();
			rekordyPodczasZmiany = true;
			RekordyImpl = rekordy;
			rekordyPodczasZmiany = false;
			RekordyZmienione?.Invoke();
			WybraneRekordy = zaznaczoneRekordy;
			ZaznaczPosortowaneKolumny();
		}
	}

	public string? Komunikat
	{
		get => field;
		set
		{
			field = value;
			Invalidate();
		}
	}

	public virtual string Podsumowanie
	{
		get
		{
			var liczbaWszystkich = Rekordy.Count();
			var liczbaWybranych = WybraneRekordy.Count();
			string tekst;
			if (liczbaWszystkich == 0) tekst = "Spis nie zawiera danych";
			else tekst = $"Liczba pozycji: <{liczbaWszystkich}>";
			if (liczbaWybranych > 1) tekst += $"\nLiczba zaznaczonych: <{liczbaWybranych}>";
			return tekst;
		}
	}

	public event Action? RekordyZmienione;
	public event Action? ZaznaczenieZmienione;

	public Ref<T> RekordPoczatkowy { get; set; }

	private void OdswiezWiersze()
	{
		if (oryginalneRekordy == null) return;
		Rekordy = oryginalneRekordy;
	}

	public void PrzeladujBezpiecznie()
	{
		try
		{
			Przeladuj();
		}
		catch (Exception exc)
		{
			OknoBledu.Pokaz(exc);
		}
	}

	public DataGridViewColumn DodajKolumneBool(string wlasciwosc, string naglowek, int? szerokosc = null, Func<T, string>? tooltip = null) => DodajKolumne(wlasciwosc, naglowek, checkbox: true, szerokosc: szerokosc, tooltip: tooltip);
	public DataGridViewColumn DodajKolumneData(string wlasciwosc, string naglowek, string? format = null, int? szerokosc = 120, Func<T, string?>? tooltip = null) => DodajKolumne(wlasciwosc, naglowek, wyrownajDoPrawej: true, format: format ?? Wyglad.FormatDaty, szerokosc: szerokosc, tooltip: tooltip);
	public DataGridViewColumn DodajKolumneKwota(string wlasciwosc, string naglowek, string? format = null, int? szerokosc = 80, Func<T, string?>? tooltip = null) => DodajKolumne(wlasciwosc, naglowek, wyrownajDoPrawej: true, format: format ?? Wyglad.FormatKwoty, szerokosc: szerokosc, tooltip: tooltip);
	public DataGridViewColumn DodajKolumneId() => DodajKolumne("Id", "Id", wyrownajDoPrawej: true, szerokosc: 60);

	protected abstract void Przeladuj();

	protected virtual void UstawStylWiersza(T rekord, string kolumna, DataGridViewCellStyle styl)
	{
	}

	private void PokazKonfiguracjeSpisu()
	{
		if (Kontekst == null) return;
		using var nowyKontekst = new Kontekst(Kontekst);
		using var transakcja = nowyKontekst.Transakcja();

		var spis = GetType().Name;
		var kolumny = Kontekst.Baza.KolumnySpisow.Where(e => e.Spis == spis).ToList();
		foreach (DataGridViewColumn kolumna in Columns)
		{
			if (kolumny.Any(e => e.Kolumna == kolumna.Name)) continue;
			var konfiguracjaKolumny = new KolumnaSpisu { Spis = spis, Kolumna = kolumna.Name };
			konfiguracjaKolumny.Kolejnosc = kolumna.DisplayIndex;
			konfiguracjaKolumny.Szerokosc = kolumna.Visible ? kolumna.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill ? -1 : kolumna.Width : 0;
			kolumny.Add(konfiguracjaKolumny);
		}

		using var edytor = new KonfiguracjaSpisu(kolumny);
		if (!DialogEdycji.Pokaz("Konfiguracja spisu", edytor, nowyKontekst)) return;
		if (edytor.CzyPrzywroc)
		{
			nowyKontekst.Baza.Usun(kolumny.Where(e => e.Id > 0));
			OknoKomunikatu.Informacja("Domyślne ustawienia zostaną załadowane po ponownym wyświetleniu spisu.");
		}
		else nowyKontekst.Baza.Zapisz(kolumny);
		transakcja.Zatwierdz();
		WczytajKonfiguracje();
	}

	public void UstawFiltr(Func<T, bool> filtr)
	{
		this.filtr = filtr;
		Rekordy = oryginalneRekordy ?? [];
	}

	protected virtual Func<T, IComparable>? KolumnaDlaSortowania(string kolumna)
	{
		var getter = typeof(T).GetProperty(kolumna)?.GetGetMethod();
		if (getter == null) return null;
		if (getter.ReturnType.IsAssignableTo(typeof(IComparable)))
		{
			var parametr = Expression.Parameter(typeof(T), "rekord");
			var wartoscExpr = Expression.Call(parametr, getter);
			var wartoscCompExpr = Expression.Convert(wartoscExpr, typeof(IComparable));
			var lambdaExpr = Expression.Lambda<Func<T, IComparable>>(wartoscCompExpr, parametr);
			var metoda = lambdaExpr.Compile();
			return metoda;
		}
		var nullableType = Nullable.GetUnderlyingType(getter.ReturnType);
		if (nullableType != null && nullableType.IsAssignableTo(typeof(IComparable)))
		{
			var parametr = Expression.Parameter(typeof(T), "rekord");
			var wartoscExpr = Expression.Call(parametr, getter);
			var getValueOrDefaultMethod = getter.ReturnType.GetMethod(nameof(Nullable<int>.GetValueOrDefault), Type.EmptyTypes);
			if (getValueOrDefaultMethod == null) return null;
			var wartoscOrDefaultExpr = Expression.Call(wartoscExpr, getValueOrDefaultMethod);
			var wartoscCompExpr = Expression.Convert(wartoscOrDefaultExpr, typeof(IComparable));
			var lambdaExpr = Expression.Lambda<Func<T, IComparable>>(wartoscCompExpr, parametr);
			var metoda = lambdaExpr.Compile();
			return metoda;
		}
		return null;
	}

	public void UstawKolejnosc(string kolumna, bool zastap)
	{
		var getter = KolumnaDlaSortowania(kolumna);
		if (getter == null) return;

		bool dodaj;
		if (zastap)
		{
			for (int i = 0; i < kolumnyKolejnosci.Count; i++)
			{
				if (kolumnyKolejnosci[i].kolumna == kolumna)
				{
					kolumnyKolejnosci[i] = (kolumna, !kolumnyKolejnosci[i].malejaco, kolumnyKolejnosci[i].metoda);
					zastap = false;
					break;
				}
			}

			if (zastap)
			{
				kolumnyKolejnosci.Clear();
				dodaj = true;
			}
			else
			{
				dodaj = false;
			}
		}
		else
		{
			dodaj = true;
			for (int i = 0; i < kolumnyKolejnosci.Count; i++)
			{
				if (kolumnyKolejnosci[i].kolumna == kolumna)
				{
					kolumnyKolejnosci[i] = (kolumna, !kolumnyKolejnosci[i].malejaco, kolumnyKolejnosci[i].metoda);
					dodaj = false;
					break;
				}
			}
		}

		if (dodaj)
		{
			kolumnyKolejnosci.Add((kolumna, false, getter));
		}
	}

	private IEnumerable<T> Sortuj(IEnumerable<T> rekordy)
	{
		if (kolumnyKolejnosci.Count == 0) return rekordy;

		var posortowane = rekordy.OrderBy(r => 0);
		foreach (var kolumna in kolumnyKolejnosci)
		{
			if (kolumna.malejaco) posortowane = posortowane.ThenByDescending(kolumna.metoda);
			else posortowane = posortowane.ThenBy(kolumna.metoda);
		}

		return posortowane;
	}
}
