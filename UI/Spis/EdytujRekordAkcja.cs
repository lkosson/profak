using ProFak.DB;

namespace ProFak.UI;

class EdytujRekordAkcja<TRekord, TEdytor> : AkcjaNaSpisie<TRekord>
	where TRekord : Rekord<TRekord>, new()
	where TEdytor : Edytor<TRekord>, new()
{
	private readonly bool pelnyEkran;

	public override string Nazwa => "✎ Wyświetl [F2]";

	public EdytujRekordAkcja(bool pelnyEkran = false)
	{
		this.pelnyEkran = pelnyEkran;
	}

	public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;

	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => modyfikatory == TKeyModifiers.None && (klawisz == TKeys.Enter || klawisz == TKeys.F2);

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<TRekord> zaznaczoneRekordy)
	{
		using var nowyKontekst = new Kontekst(kontekst);
		using var transakcja = nowyKontekst.Transakcja();
		var rekordRef = zaznaczoneRekordy.Single().Ref;
		nowyKontekst.Baza.Zablokuj(rekordRef);
		var rekord = nowyKontekst.Baza.Znajdz(rekordRef);
		nowyKontekst.Dodaj(rekord);
		using var edytor = new TEdytor();
		edytor.Przygotuj(nowyKontekst, rekord);
		if (!DialogEdycji.Pokaz("Edycja danych", edytor, nowyKontekst, pelnyEkran)) return;
		edytor.KoniecEdycji();
		nowyKontekst.Baza.Zapisz(rekord);
		transakcja.Zatwierdz();
	}
}
