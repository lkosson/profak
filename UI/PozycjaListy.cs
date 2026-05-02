using ProFak.DB;

namespace ProFak.UI;

interface IPozycjaListy
{
	object? Wartosc { get; }
	string Opis { get; }
}

class PozycjaListy<T> : IPozycjaListy
{
	public T? Wartosc { get; set; }
	public required string Opis { get; set; }

	object? IPozycjaListy.Wartosc => Wartosc;
	string IPozycjaListy.Opis => Opis;

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
