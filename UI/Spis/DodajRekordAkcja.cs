using ProFak.DB;

namespace ProFak.UI;

class DodajRekordAkcja<TRekord, TEdytor> : AkcjaNaSpisie<TRekord>
	where TRekord : Rekord<TRekord>, new()
	where TEdytor : Edytor<TRekord>, new()
{
	private readonly Action<TRekord>? przygotujRekord;
	private readonly bool pelnyEkran;

	public override string Nazwa => "➕ Dodaj [INS]";

	public DodajRekordAkcja(Action<TRekord>? przygotujRekord = null, bool pelnyEkran = false)
	{
		this.przygotujRekord = przygotujRekord;
		this.pelnyEkran = pelnyEkran;
	}

	public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => true;
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => modyfikatory == TKeyModifiers.None && klawisz == TKeys.Insert;

	protected virtual TRekord? UtworzRekord(Kontekst kontekst, IEnumerable<TRekord> zaznaczoneRekordy)
	{
		var rekord = new TRekord();
		if (przygotujRekord != null) przygotujRekord(rekord);
		kontekst.Baza.Zapisz(rekord);
		return rekord;
	}

	protected virtual void ZapiszRekord(Kontekst kontekst, TRekord rekord)
	{
		kontekst.Baza.Zapisz(rekord);
	}

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<TRekord> zaznaczoneRekordy)
	{
		using var nowyKontekst = new Kontekst(kontekst);
		using var transakcja = nowyKontekst.Transakcja();
		var rekord = UtworzRekord(nowyKontekst, zaznaczoneRekordy);
		if (rekord == null) return;
		nowyKontekst.Dodaj(rekord);
		using var edytor = new TEdytor();
		edytor.Przygotuj(nowyKontekst, rekord);
		if (!DialogEdycji.Pokaz("Nowa pozycja", edytor, nowyKontekst, pelnyEkran)) return;
		edytor.KoniecEdycji();
		ZapiszRekord(nowyKontekst, rekord);
		transakcja.Zatwierdz();
		zaznaczoneRekordy = new[] { rekord };
	}
}
