using System.Text.RegularExpressions;

namespace ProFak.DB;

public class DaneAdresowe
{
	public string Ulica { get; } = "";
	public string NumerDomu { get; } = "";
	public string NumerLokalu { get; } = "";
	public string PNA { get; } = "";
	public string Miasto { get; } = "";

	private static readonly Regex RX_1 = new Regex(@"(?<ulica>[\w\s\-\.""]+)\s(?<numerdomu>\d+\w?)[/\s]*(?<numerlokalu>\d+\w?)\s+(?<pna>\d\d-\d\d\d)\s+(?<miasto>\w+)");
	private static readonly Regex RX_2 = new Regex(@"(?<ulica>.+?)[/\s]*(?<numerdomu>\d+\w?)\s+(?<pna>\d\d-\d\d\d)\s+(?<miasto>\w+)");
	private static readonly Regex RX_3 = new Regex(@"(?<ulica>.+?)\s+(?<pna>\d\d-\d\d\d)\s+(?<miasto>\w+)");

	public DaneAdresowe(string adres)
	{
		var match = RX_1.Match(adres);
		if (match.Success)
		{
			Ulica = match.Groups["ulica"].Value;
			NumerDomu = match.Groups["numerdomu"].Value;
			NumerLokalu = match.Groups["numerlokalu"].Value;
			PNA = match.Groups["pna"].Value;
			Miasto = match.Groups["miasto"].Value;
			return;
		}

		match = RX_2.Match(adres);
		if (match.Success)
		{
			Ulica = match.Groups["ulica"].Value;
			NumerDomu = match.Groups["numerdomu"].Value;
			PNA = match.Groups["pna"].Value;
			Miasto = match.Groups["miasto"].Value;
			return;
		}

		match = RX_3.Match(adres);
		if (match.Success)
		{
			Ulica = match.Groups["ulica"].Value;
			PNA = match.Groups["pna"].Value;
			Miasto = match.Groups["miasto"].Value;
			return;
		}

		Ulica = adres;
	}
}
