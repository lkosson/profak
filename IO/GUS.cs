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
	}
}
