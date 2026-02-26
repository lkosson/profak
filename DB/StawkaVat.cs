namespace ProFak.DB;

public class StawkaVat : Rekord<StawkaVat>
{
	public string Skrot { get; set; } = "";
	public decimal Wartosc { get; set; }
	public bool CzyDomyslna { get; set; }

	public string CzyDomyslnaFmt => CzyDomyslna ? "Tak" : "Nie";

	public override bool CzyPasuje(string fraza)
		=> base.CzyPasuje(fraza)
		|| CzyPasuje(Skrot, fraza)
		|| CzyPasuje(Wartosc, fraza)
		|| CzyPasuje(CzyDomyslna ? "Domyślna" : "", fraza);
}
