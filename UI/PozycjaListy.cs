using ProFak.DB;

namespace ProFak.UI;

interface IPozycjaListy
{
	object? Wartosc { get; }
	string Opis { get; }
	bool CzyPasuje(object? wartosc);
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

	public bool CzyPasuje(object? wartosc)
	{
		if (wartosc == null) return Wartosc == null;
		return wartosc.Equals(Wartosc);
	}
}

class PozycjaListyRekordu<T> : IPozycjaListy
	where T : Rekord<T>
{
	public T? Wartosc { get; set; }
	public Ref<T> Ref => Wartosc == null ? default : Wartosc.Ref;
	public required string Opis { get; set; }
	object? IPozycjaListy.Wartosc => Wartosc;

	public PozycjaListyRekordu()
	{
	}

	public bool CzyPasuje(object? wartosc)
	{
		if (wartosc == null) return Wartosc == null;
		return wartosc.Equals(Wartosc) || Ref.Equals(wartosc);
	}
}
