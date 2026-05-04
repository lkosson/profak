using ProFak.DB;

namespace ProFak.UI;

class UsunRekordAkcja<TRekord> : AkcjaNaSpisie<TRekord>
	where TRekord : Rekord<TRekord>, new()
{
	public override string Nazwa => "❌ Usuń [DEL]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => zaznaczoneRekordy.Count() >= 1;
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => modyfikatory == TKeyModifiers.None && (klawisz == TKeys.Delete || klawisz == TKeys.F8);

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<TRekord> zaznaczoneRekordy)
	{
		using var nowyKontekst = new Kontekst(kontekst);
		var liczba = zaznaczoneRekordy.Count();
		if (!OknoKomunikatu.PytanieTakNie(liczba > 1 ? $"Czy na pewno chcesz usunąć wszystkie ({liczba}) zaznaczone pozycje?" : "Czy na pewno chcesz usunąć zaznaczoną pozycję?", domyslnie: false)) return;
		using var transakcja = nowyKontekst.Transakcja();
		Usun(nowyKontekst, zaznaczoneRekordy);
		transakcja.Zatwierdz();
	}

	protected virtual void Usun(Kontekst kontekst, IEnumerable<TRekord> zaznaczoneRekordy)
	{
		kontekst.Baza.Usun(zaznaczoneRekordy);
	}
}
