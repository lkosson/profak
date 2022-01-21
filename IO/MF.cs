using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProFak.IO
{
	class MF
	{
		public static string SprawdzBialaListeVAT(string nip, string nrb)
		{
			if (String.IsNullOrEmpty(nip)) throw new ApplicationException("Nie podano NIPu kontrahenta.");
			if (String.IsNullOrEmpty(nrb)) throw new ApplicationException("Nie podano numeru rachunku bankowego kontrahenta.");
			var url = "https://wl-api.mf.gov.pl/api/check/nip/" + nip + "/bank-account/" + nrb;
			using var client = new HttpClient();
			var wynik = client.GetStringAsync(url).Result;
			var json = JsonDocument.Parse(wynik);
			if (!json.RootElement.TryGetProperty("result", out var jsonResult)) throw new ApplicationException($"Nieprawidłowa struktura odpowiedzi:\n\n{wynik}");
			if (!jsonResult.TryGetProperty("accountAssigned", out var jsonResultAccountAssigned)) throw new ApplicationException($"Nieprawidłowa struktura odpowiedzi:\n\n{wynik}");
			if (jsonResultAccountAssigned.GetString() != "TAK") throw new ApplicationException("Rachunek nie znajduje się na białej liście VAT.");
			if (!jsonResult.TryGetProperty("requestId", out var jsonResultRequestId)) throw new ApplicationException($"Nieprawidłowa struktura odpowiedzi:\n\n{wynik}");
			return jsonResultRequestId.GetString();
		}
	}
}
