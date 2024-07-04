using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProFak.IO
{
	class GUS
	{
		private static readonly string[] FragmentyKodu = ["y1y", "yy2", "yy3", "yy4", "yy5", "yy6", "yy7", "yy8", "yy9", "y20", "y2y", "y22", "y23", "y24", "y25", "y26", "y27", "y28", "y29", "y30", "!3!", "!32", "!33", "!34", "!35", "!36", "!37", "!38", "!39", "!40", "!4!", "!42", "!43", "!44", "!45", "!46", "!47", "!48", "!49", "!50", "!5!", "!52", "!53", "1b4", "1bb", "1ba", "1b7", "1b8", "1b9", "1a0", "1a1", "1a2", "1a3", "1a4", "1ab", "1aa", "1a7", "1a8", "1a9", "170", "171", "172", "173", "174", "17b", "71a"];
		private static readonly string ZnakiB64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

		public static async Task PobierzGUS(Kontrahent kontrahent)
		{
			var nip = kontrahent.NIP?.Trim()?.Replace("-", "");
			if (String.IsNullOrEmpty(nip)) throw new ApplicationException("Należy podać NIP.");
			using var client = new HttpClient();
			client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0");
			client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("pl,en-US;q=0.7,en;q=0.3");

			var html = await client.GetStringAsync("https://wyszukiwarkaregon.stat.gov.pl/appBIR/index.aspx");
			var liczbyKlucza = Regex.Match(html, @"'String\.fromCharCode\(((?<znak>\d+)[,\)])+");
			var wyrazenieKlucza = new String(liczbyKlucza.Groups["znak"].Captures.Select(znak => (char)Byte.Parse(znak.Value)).ToArray());
			var klucz = wyrazenieKlucza.Substring(wyrazenieKlucza.IndexOf('\'') + 1, wyrazenieKlucza.LastIndexOf('\'') - wyrazenieKlucza.IndexOf('\'') - 1);
			var zalogujContent = JsonContent.Create(new { pKluczUzytkownika = klucz });
			var zalogujRequest = new HttpRequestMessage(HttpMethod.Post, "https://wyszukiwarkaregon.stat.gov.pl/wsBIR/UslugaBIRzewnPubl.svc/ajaxEndpoint/Zaloguj");
			zalogujRequest.Headers.Referrer = new Uri("https://wyszukiwarkaregon.stat.gov.pl/appBIR/index.aspx");
			zalogujRequest.Content = zalogujContent;
			var zalogujResponse = await client.SendAsync(zalogujRequest);
			var zalogujOdpowiedz = await zalogujResponse.Content.ReadAsStringAsync();
			var zalogujOdpowiedzJson = JsonSerializer.Deserialize<JsonElement>(zalogujOdpowiedz);
			var zalogujSid = zalogujOdpowiedzJson.GetProperty("d").GetString();

			var szukajContent = JsonContent.Create(new { pParametryWyszukiwania = new { Nip = nip }, jestWojPowGmnMiej = true }, options: new JsonSerializerOptions { PropertyNamingPolicy = null });
			var s = await szukajContent.ReadAsStringAsync();
			var szukajRequest = new HttpRequestMessage(HttpMethod.Post, "https://wyszukiwarkaregon.stat.gov.pl/wsBIR/UslugaBIRzewnPubl.svc/ajaxEndpoint/daneSzukaj");
			szukajRequest.Headers.Referrer = new Uri("https://wyszukiwarkaregon.stat.gov.pl/appBIR/index.aspx");
			szukajRequest.Headers.Add("sid", zalogujSid);
			szukajRequest.Content = szukajContent;
			var szukajResponse = await client.SendAsync(szukajRequest);
			var szukajOdpowiedz = await szukajResponse.Content.ReadAsStringAsync();
			var szukajOdpowiedzJson = JsonSerializer.Deserialize<JsonElement>(szukajOdpowiedz);
			var podmioty = szukajOdpowiedzJson.GetProperty("d").GetString();
			if (String.IsNullOrEmpty(podmioty)) throw new ApplicationException("Nie znaleziono firmy w bazie GUS.");
			if (podmioty.StartsWith("enc")) podmioty = DekodujGUS(podmioty);
			var podmiotyJson = JsonSerializer.Deserialize<JsonElement>(podmioty);
			if (podmiotyJson.GetArrayLength() == 0) throw new ApplicationException("Nie znaleziono firmy w bazie GUS.");
			var podmiot = podmiotyJson[0];

			var regon = podmiot.GetProperty("Regon").GetString();
			var nazwa = podmiot.GetProperty("Nazwa").GetString();
			var wojewodztwo = podmiot.GetProperty("Wojewodztwo").GetString();
			var powiat = podmiot.GetProperty("Powiat").GetString();
			var gmina = podmiot.GetProperty("Gmina").GetString();
			var kodpocztowy = podmiot.GetProperty("KodPocztowy").GetString();
			var miejscowosc = podmiot.GetProperty("Miejscowosc").GetString();
			var ulica = podmiot.GetProperty("Ulica").GetString();
			var numer = podmiot.GetProperty("Numer_Nieruchomosci").GetString();

			kontrahent.Nazwa = nazwa;
			kontrahent.PelnaNazwa = nazwa;
			kontrahent.AdresRejestrowy = ulica + "\r\n" + kodpocztowy + " " + miejscowosc;
			kontrahent.AdresKorespondencyjny = kontrahent.AdresRejestrowy;
		}

		private static string DekodujGUS(string wejscie)
		{
			var output = "";
			for (var i = 3; i < wejscie.Length; i += 3)
			{
				var c = wejscie.Substring(i, 3);
				output += ZnakiB64[Array.IndexOf(FragmentyKodu, c)];
			};

			return Encoding.UTF8.GetString(Convert.FromBase64String(output));
		}
	}
}
