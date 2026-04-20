using ProFak.DB;

namespace ProFak.UI;

class StanNumeratoraSpis : Spis<StanNumeratora>
{
	public Ref<Numerator> NumeratorRef { get; set; }

	public StanNumeratoraSpis()
	{
		DodajKolumne(nameof(StanNumeratora.Parametry), "Grupa numeracji", rozciagnij: true);
		DodajKolumne(nameof(StanNumeratora.OstatniaWartosc), "Ostatnia wartość");
		DodajKolumneId();
	}

	protected override void Przeladuj()
	{
		IQueryable<StanNumeratora> q = Kontekst.Baza.StanyNumeratorow;
		if (NumeratorRef.IsNotNull) q = q.Where(stanNumeratora => stanNumeratora.NumeratorId == NumeratorRef.Id);
		q = q.OrderBy(stanNumeratora => stanNumeratora.Parametry).ThenBy(stanNumeratora => stanNumeratora.Id);
		Rekordy = q.ToList();
	}
}
