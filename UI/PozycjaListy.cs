using ProFak.DB;

namespace ProFak.UI;

class PozycjaListy<T>
{
	public T? Wartosc { get; set; }
	public required string Opis { get; set; }

	public PozycjaListy()
	{
	}
}

class PozycjaListyRekordu<T>
	where T : Rekord<T>
{
	public T? Wartosc { get; set; }
	public Ref<T> Ref => Wartosc == null ? default : Wartosc.Ref;
	public required string Opis { get; set; }

	public PozycjaListyRekordu()
	{
	}
}
