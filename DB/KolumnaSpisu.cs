namespace ProFak.DB;

class KolumnaSpisu : Rekord<KolumnaSpisu>
{
	public string Spis { get; set; } = "";
	public string Kolumna { get; set; } = "";
	public int Kolejnosc { get; set; }
	public int Szerokosc { get; set; }
	public int PoziomSortowania { get; set; }

	public override bool CzyPasuje(string fraza)
		=> base.CzyPasuje(fraza)
		|| CzyPasuje(Spis, fraza)
		|| CzyPasuje(Kolumna, fraza);

	public override string ToString() => Kolejnosc + ". " + Kolumna + " (" + (Szerokosc == 0 ? "ukryta" : Szerokosc == -1 ? "rozciągnięta" : Szerokosc + "px") + ")";
}
