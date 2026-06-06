using ProFak.DB;
using System.Text.Json;

namespace ProFak.IO;

public class NBP
{
	public static async Task<decimal> PobierzSredniKursWaluty(Waluta? waluta, DateTime data, CancellationToken cancellationToken)
	{
		if (waluta == null || waluta.CzyDomyslna) return 1;
		if (data.DayOfWeek == DayOfWeek.Saturday) data = data.AddDays(-1);
		if (data.DayOfWeek == DayOfWeek.Sunday) data = data.AddDays(-2);
		var url = $"https://api.nbp.pl/api/exchangerates/rates/a/{waluta.Skrot}/{data:yyyy-MM-dd}/";
		using var client = new HttpClient();
		var wynik = await client.GetStringAsync(url, cancellationToken);
		var json = JsonDocument.Parse(wynik);
		var rates = json.RootElement.GetProperty("rates");
		if (rates.GetArrayLength() == 0) throw new ApplicationException($"Brak kursu dla waluty {waluta}.");
		var rate = rates[0];
		if (!rate.TryGetProperty("mid", out var mid)) throw new ApplicationException($"Nieprawidłowa struktura odpowiedzi dla waluty {waluta}.");
		return mid.GetDecimal();
	}
}
