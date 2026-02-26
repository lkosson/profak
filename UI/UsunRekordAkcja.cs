using ProFak.DB;

namespace ProFak.UI;

class UsunRekordAkcja<TRekord> : AkcjaNaSpisie<TRekord>
	where TRekord : Rekord<TRekord>, new()
{
	public override string Nazwa => "❌ Usuń [DEL]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => zaznaczoneRekordy.Count() >= 1;
	public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.None && (klawisz == Keys.Delete || klawisz == Keys.F8);

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<TRekord> zaznaczoneRekordy)
	{
		using var nowyKontekst = new Kontekst(kontekst);
		var liczba = zaznaczoneRekordy.Count();
		if (MessageBox.Show(liczba > 1 ? $"Czy na pewno chcesz usunąć wszystkie ({liczba}) zaznaczone pozycje?" : "Czy na pewno chcesz usunąć zaznaczoną pozycję?", "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
		using var transakcja = nowyKontekst.Transakcja();
		Usun(nowyKontekst, zaznaczoneRekordy);
		transakcja.Zatwierdz();
	}

	protected virtual void Usun(Kontekst kontekst, IEnumerable<TRekord> zaznaczoneRekordy)
	{
		kontekst.Baza.Usun(zaznaczoneRekordy);
	}
}
