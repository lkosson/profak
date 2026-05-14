using ProFak.DB;

namespace ProFak.UI;

class Edytor<TRekord> : Edytor
	where TRekord : Rekord<TRekord>
{
	protected readonly Kontroler<TRekord> kontroler;

	public TRekord Rekord { get => kontroler.Model; private set => kontroler.Model = value; }
	public Kontekst Kontekst { get; private set; } = default!;
	public override bool CzyModelZmieniony => kontroler.CzyModelZmieniony;
#if AVALONIA
	public override bool CzyModelPoprawny => kontroler.CzyModelPoprawny;
#endif

	public Edytor()
	{
		kontroler = new Kontroler<TRekord>();
	}

	public void Przygotuj(Kontekst kontekst, TRekord rekord)
	{
		Kontekst = kontekst;
		KontekstGotowy();
		PrzygotujRekord(rekord);
		Rekord = rekord;
		RekordGotowy();
	}

	protected virtual void PrzygotujRekord(TRekord rekord)
	{
	}

	protected virtual void KontekstGotowy()
	{
	}

	protected virtual void RekordGotowy()
	{
	}

	public virtual void KoniecEdycji()
	{
	}
}
