namespace ProFak.DB;

public class StawkaVat : Rekord<StawkaVat>
{
	public string Skrot { get; set; } = "";
	public decimal Wartosc { get; set; }
	public bool CzyDomyslna { get; set; }

	public string CzyDomyslnaFmt => CzyDomyslna ? "Tak" : "Nie";
	public bool CzyZW => (Skrot ?? "").Contains("zw", StringComparison.CurrentCultureIgnoreCase);
	public bool CzyNP_I => (Skrot ?? "").Contains("np i", StringComparison.CurrentCultureIgnoreCase) && !CzyNP_II;
	public bool CzyNP_II => (Skrot ?? "").Contains("np ii", StringComparison.CurrentCultureIgnoreCase);
	public bool CzyEX => (Skrot ?? "").Contains("ex", StringComparison.CurrentCultureIgnoreCase);

	public override bool CzyPasuje(string fraza)
		=> base.CzyPasuje(fraza)
		|| CzyPasuje(Skrot, fraza)
		|| CzyPasuje(Wartosc, fraza)
		|| CzyPasuje(CzyDomyslna ? "Domyślna" : "", fraza);
}
