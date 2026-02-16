using ProFak.DB;

namespace ProFak.UI;

class PrzeladujAkcja<TRekord> : AkcjaNaSpisie<TRekord>
	where TRekord : Rekord<TRekord>, new()
{
	public override string Nazwa => "⟳ Przeładuj spis [F5]";

	public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => true;
	public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.None && klawisz == Keys.F5;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<TRekord> zaznaczoneRekordy)
	{
	}
}
