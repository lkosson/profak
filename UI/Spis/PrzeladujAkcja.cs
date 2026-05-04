using ProFak.DB;

namespace ProFak.UI;

class PrzeladujAkcja<TRekord> : AkcjaNaSpisie<TRekord>
	where TRekord : Rekord<TRekord>, new()
{
	public override string Nazwa => "⟳ Przeładuj spis [F5]";

	public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => true;
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => modyfikatory == TKeyModifiers.None && klawisz == TKeys.F5;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<TRekord> zaznaczoneRekordy)
	{
	}
}
