using ProFak.DB;

namespace ProFak.UI;

class WybierzRekordAkcja<TRekord> : AkcjaNaSpisie<TRekord>
	where TRekord : Rekord<TRekord>
{
	public override string Nazwa => "✔️ Wybierz [ENTER]";
	public TRekord? WybranyRekord { get; private set; }

	public WybierzRekordAkcja()
	{
	}

	public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;

	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => modyfikatory == TKeyModifiers.None && klawisz == TKeys.Enter;

	public override bool PrzeladujPoZakonczeniu => false;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<TRekord> zaznaczoneRekordy)
	{
		if (kontekst.Dialog == null) return;
		WybranyRekord = zaznaczoneRekordy.Single();
		kontekst.Dialog.Zamknij();
	}
}
