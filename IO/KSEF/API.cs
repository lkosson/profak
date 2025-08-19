using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;
using ProFak.DB;

namespace ProFak.IO.KSEF;

class API : IDisposable
{
	private readonly HttpClient client;
	private readonly string urlBase;
	private readonly string pubkey;
	private bool sesjaAktywna;

	public API(SrodowiskoKSeF srodowisko)
	{
		client = new HttpClient();
		urlBase = srodowisko == SrodowiskoKSeF.Prod ? "https://ksef.mf.gov.pl"
			: srodowisko == SrodowiskoKSeF.Demo ? "https://ksef-demo.mf.gov.pl"
			: "https://ksef-test.mf.gov.pl";
		pubkey = srodowisko == SrodowiskoKSeF.Test ? "-----BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAuWosgHSpiRLadA0fQbzshi5TluliZfDsJujPlyYqp6A3qnzS3WmHxtwgO58uTbemQ1HCC2qwrMwuJqR6l8tgA4ilBMDbEEtkzgbjkJ6xoEqBptgxivP/ovOFYYoAnY6brZhXytCamSvjY9KI0g0McRk24pOueXT0cbb0tlwEEjVZ8NveQNKT2c1EEE2cjmW0XB3UlIBqNqiY2rWF86DcuFDTUy+KzSmTJTFvU/ENNyLTh5kkDOmB1SY1Zaw9/Q6+a4VJ0urKZPw+61jtzWmucp4CO2cfXg9qtF6cxFIrgfbtvLofGQg09Bh7Y6ZA5VfMRDVDYLjvHwDYUHg2dPIk0wIDAQAB\r\n-----END PUBLIC KEY-----"
			: srodowisko == SrodowiskoKSeF.Demo ? "-----BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAwocTwdNgt2+PXJ2fcB7k1kn5eFUTXBeep9pHLx6MlfkmHLvgjVpQy1/hqMTFfZqw6piFOdZMOSLgizRKjb1CtDYhWncg0mML+yhVrPyHT7bkbqfDuM2ku3q8ueEOy40SEl4jRMNvttkWnkvf/VTy2TwA9X9vTd61KJmDDZBLOCVqsyzdnELKUE8iulXwTarDvVTx4irnz/GY+y9qod+XrayYndtU6/kDgasAAQv0pu7esFFPMr83Nkqdu6JD5/0yJOl5RShQXwlmToqvpih2+L92x865/C4f3n+dZ9bgsKDGSkKSqq7Pz+QnhF7jV/JAmtJBCIMylxdxI/xfDHZ5XwIDAQAB\r\n-----END PUBLIC KEY-----"
			: "-----BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtCVoNVHGeaOwmzuFMiScJozTbh+ULVtQYmRNTON+20ilBOqkHrJRUZCtXUg0w+ztYMvWFr4U74ykGMnEYODT7l2F8JGuJeE9YGK8hKqaY5h0YYxJW7fWybZOxQJhwXzuasjKt/OHYWrI6SmL96bSanr6MwGNr6yiNQV3R6EFB/wpZ4scwh8ZfEs0kk29uIgZVEbkq+9n/xRQjbAtaQs6eiDb4AUOBd7nm4+Uis5goHkjTtJwmhcpQq5Vw7lug3FUsn7/luNyCVhaR4BkpB3NVexxepYSByJneFrOgOh/3GilK2a47WPAEVG3hRQAiGBUR0m7Ev7WYboQtA1TI7hc6wIDAQAB\r\n-----END PUBLIC KEY-----";
	}

	private void ProcessException(JsonElement json)
	{
		if (!json.TryGetProperty("exception", out var exceptionNode)) return;
		if (!exceptionNode.TryGetProperty("exceptionDetailList", out var exceptionList)) throw new ApplicationException($"Nieznany błąd podczas komunikacji z KSeF.\n{exceptionNode}");
		var exceptionDetail = exceptionList.EnumerateArray().FirstOrDefault();
		if (!exceptionDetail.TryGetProperty("exceptionDescription", out var exceptionDescription)) throw new ApplicationException($"Nieznany błąd podczas komunikacji z KSeF.\n{exceptionNode}");
		throw new ApplicationException($"Błąd podczas komunikacji z KSeF: {exceptionDescription.GetString()}");
	}

	private async Task<(string challenge, DateTime timestamp)> AuthorisationChallengeAsync(string nip)
	{
		var authorisationChallenge = new { contextIdentifier = new { type = "onip", identifier = nip } };
		var authorisationChallengeContent = JsonContent.Create(authorisationChallenge);
		var authorisationChallengeRequest = new HttpRequestMessage(HttpMethod.Post, urlBase + "/api/online/Session/AuthorisationChallenge") { Content = authorisationChallengeContent };
		var authorisationChallengeResponse = await client.SendAsync(authorisationChallengeRequest);
		var authorisationChallengeResponseBody = await authorisationChallengeResponse.Content.ReadAsStringAsync();
		var authorisationChallengeResponseJson = JsonSerializer.Deserialize<JsonElement>(authorisationChallengeResponseBody);
		ProcessException(authorisationChallengeResponseJson);
		var challenge = PropertyOrNull(authorisationChallengeResponseJson, "challenge")?.GetString();
		var timestamp = PropertyOrNull(authorisationChallengeResponseJson, "timestamp")?.GetDateTime().ToUniversalTime();
		if (challenge == null || timestamp == null) throw new ApplicationException("Missing challenge.");
		return (challenge, timestamp.Value);
	}

	private async Task<string> InitTokenAsync(string nip, string authorisationToken, string challenge, DateTime timestamp)
	{
		var tokenMessage = authorisationToken + "|" + (long)((timestamp - new DateTime(1970, 1, 1)).TotalMilliseconds + 0.5);

		var rsa = RSA.Create();
		rsa.ImportFromPem(pubkey);

		var encryptedTokenMessage = rsa.Encrypt(Encoding.UTF8.GetBytes(tokenMessage), RSAEncryptionPadding.Pkcs1);
		var encryptedTokenMessageB64 = Convert.ToBase64String(encryptedTokenMessage);

		var initToken = $@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>
<ns3:InitSessionTokenRequest
	xmlns=""http://ksef.mf.gov.pl/schema/gtw/svc/online/types/2021/10/01/0001""
	xmlns:ns2=""http://ksef.mf.gov.pl/schema/gtw/svc/types/2021/10/01/0001""
	xmlns:ns3=""http://ksef.mf.gov.pl/schema/gtw/svc/online/auth/request/2021/10/01/0001"">
	<ns3:Context>
		<Challenge>{challenge}</Challenge>
		<Identifier xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:type=""ns2:SubjectIdentifierByCompanyType"">
			<ns2:Identifier>{nip}</ns2:Identifier>
		</Identifier>
		<DocumentType>
			<ns2:Service>KSeF</ns2:Service>
			<ns2:FormCode>
				<ns2:SystemCode>FA (2)</ns2:SystemCode>
				<ns2:SchemaVersion>1-0E</ns2:SchemaVersion>
				<ns2:TargetNamespace>http://crd.gov.pl/wzor/2021/11/29/11089/</ns2:TargetNamespace>
				<ns2:Value>FA</ns2:Value>
			</ns2:FormCode>
		</DocumentType>
		<Token>{encryptedTokenMessageB64}</Token>
	</ns3:Context>
</ns3:InitSessionTokenRequest>";

		var initTokenContent = new StringContent(initToken, Encoding.UTF8, "application/octet-stream");
		var initTokenRequest = new HttpRequestMessage(HttpMethod.Post, urlBase + "/api/online/Session/InitToken") { Content = initTokenContent };
		var initTokenResponse = await client.SendAsync(initTokenRequest);
		var initTokenResponseBody = await initTokenResponse.Content.ReadAsStringAsync();
		var initTokenResponseJson = JsonSerializer.Deserialize<JsonElement>(initTokenResponseBody);
		ProcessException(initTokenResponseJson);
		var sessionToken = PropertyOrNull(initTokenResponseJson, "sessionToken", "token")?.GetString();
		return sessionToken ?? throw new ApplicationException("Missing session token.");
	}

	public async Task AuthenticateAsync(string nip, string authorisationToken)
	{
		(var challenge, var timestamp) = await AuthorisationChallengeAsync(nip);
		var sessionToken = await InitTokenAsync(nip, authorisationToken, challenge, timestamp);
		client.DefaultRequestHeaders.Add("SessionToken", sessionToken);
		sesjaAktywna = true;
		await Task.Delay(1000); // Inaczej kolejne wywołanie wywala się z błędem
	}

	private async Task<IReadOnlyCollection<InvoiceHeader>> GetInvoicesAsync(bool przyrostowo, bool sprzedaz, DateTime dateFrom, DateTime dateTo, int pageSize, int pageOffset)
	{
		var type = przyrostowo ? "incremental" : "range";
		var subject = sprzedaz ? "subject1" : "subject2";
		var invoices = new List<InvoiceHeader>();
		JsonContent invoiceSyncContent;
		if (type == "range") invoiceSyncContent = JsonContent.Create(new { queryCriteria = new { type, subjectType = subject, invoicingDateFrom = dateFrom.ToString("s"), invoicingDateTo = dateTo.ToString("s") } });
		else invoiceSyncContent = JsonContent.Create(new { queryCriteria = new { type, subjectType = subject, acquisitionTimestampThresholdFrom = dateFrom.ToString("s"), acquisitionTimestampThresholdTo = dateTo.ToString("s") } });
		var invoiceSyncRequest = new HttpRequestMessage(HttpMethod.Post, urlBase + $"/api/online/Query/Invoice/Sync?PageSize={pageSize}&PageOffset={pageOffset}") { Content = invoiceSyncContent };
		var invoiceSyncResponse = await client.SendAsync(invoiceSyncRequest);
		var invoiceSyncResponseBody = await invoiceSyncResponse.Content.ReadAsStringAsync();
		var invoiceSyncResponseJson = JsonSerializer.Deserialize<JsonElement>(invoiceSyncResponseBody);
		ProcessException(invoiceSyncResponseJson);
		var invoiceHeaderList = PropertyOrNull(invoiceSyncResponseJson, "invoiceHeaderList") ?? throw new ApplicationException("Brak faktur w odpowiedzi.");
		foreach (var invoiceHeaderListElement in invoiceHeaderList.EnumerateArray())
		{
			var invoiceHeader = new InvoiceHeader();
			invoiceHeader.ReferenceNumber = PropertyOrNull(invoiceHeaderListElement, "invoiceReferenceNumber")?.GetString();
			invoiceHeader.KsefReferenceNumber = PropertyOrNull(invoiceHeaderListElement, "ksefReferenceNumber")?.GetString()!;
			invoiceHeader.InvoicingDate = PropertyOrNull(invoiceHeaderListElement, "invoicingDate")?.GetDateTime() ?? default;
			invoiceHeader.AcquisitionTimestamp = PropertyOrNull(invoiceHeaderListElement, "acquisitionTimestamp")?.GetDateTime() ?? default;
			invoiceHeader.IssuedByNIP = PropertyOrNull(invoiceHeaderListElement, "subjectBy", "issuedByIdentifier", "identifier")?.GetString();
			invoiceHeader.IssuedByName = PropertyOrNull(invoiceHeaderListElement, "subjectBy", "issuedByName", "fullName")?.GetString();
			invoiceHeader.IssuedToNIP = PropertyOrNull(invoiceHeaderListElement, "subjectTo", "issuedToIdentifier", "identifier")?.GetString();
			invoiceHeader.IssuedToName = PropertyOrNull(invoiceHeaderListElement, "subjectTo", "issuedToName", "fullName")?.GetString() ?? PropertyOrNull(invoiceHeaderListElement, "subjectTo", "issuedToName", "firstName")?.GetString() + " " + PropertyOrNull(invoiceHeaderListElement, "subjectTo", "issuedToName", "surame")?.GetString();
			invoiceHeader.Net = Decimal.Parse(PropertyOrNull(invoiceHeaderListElement, "net")?.GetString(), CultureInfo.InvariantCulture);
			invoiceHeader.Gross = Decimal.Parse(PropertyOrNull(invoiceHeaderListElement, "gross")?.GetString(), CultureInfo.InvariantCulture);
			invoiceHeader.Vat = Decimal.Parse(PropertyOrNull(invoiceHeaderListElement, "vat")?.GetString(), CultureInfo.InvariantCulture);
			invoiceHeader.Currency = PropertyOrNull(invoiceHeaderListElement, "currency")?.GetString();
			invoiceHeader.Type = PropertyOrNull(invoiceHeaderListElement, "invoiceType")?.GetString();
			invoices.Add(invoiceHeader);
		}
		return invoices;
	}

	public async Task<IReadOnlyCollection<InvoiceHeader>> GetInvoicesAsync(bool przyrostowo, bool sprzedaz, DateTime dateFrom, DateTime dateTo)
	{
		var pageSize = 100;
		var pageOffset = 0;
		var invoices = new List<InvoiceHeader>();
		while (true)
		{
			var page = await GetInvoicesAsync(przyrostowo, sprzedaz, dateFrom, dateTo, pageSize, pageOffset);
			invoices.AddRange(page);
			pageOffset += page.Count;
			if (page.Count < pageSize) break;
		}
		return invoices;
	}

	public async Task<string> GetInvoiceAsync(string ksefReferenceNumber)
	{
		var fullInvoiceGetResponse = await client.GetAsync(urlBase + "/api/online/Invoice/Get/" + ksefReferenceNumber);
		var fullInvoiceGetResponseBody = await fullInvoiceGetResponse.Content.ReadAsStringAsync();
		return fullInvoiceGetResponseBody;
	}

	private async Task<(string elementReferenceNumber, string invoiceHash)> SendInvoiceAsync(string invoiceXml)
	{
		var invoiceUTF8 = Encoding.UTF8.GetBytes(invoiceXml);
		var invoiceUTF8B64 = Convert.ToBase64String(invoiceUTF8);
		var invoiceUTF8Hash = SHA256.HashData(invoiceUTF8);
		var invoiceUTF8HashB64 = Convert.ToBase64String(invoiceUTF8Hash);

		var invoiceSend = new { invoiceHash = new { hashSHA = new { algorithm = "SHA-256", encoding = "Base64", value = invoiceUTF8HashB64 }, fileSize = invoiceUTF8.Length }, invoicePayload = new { type = "plain", invoiceBody = invoiceUTF8B64 } };
		var invoiceSendContent = JsonContent.Create(invoiceSend);
		var invoiceSendRequest = new HttpRequestMessage(HttpMethod.Put, urlBase + "/api/online/Invoice/Send") { Content = invoiceSendContent };
		var invoiceSendResponse = await client.SendAsync(invoiceSendRequest);
		var invoiceSendResponseBody = await invoiceSendResponse.Content.ReadAsStringAsync();
		var invoiceSendResponseJson = JsonSerializer.Deserialize<JsonElement>(invoiceSendResponseBody);
		ProcessException(invoiceSendResponseJson);
		var elementReferenceNumber = PropertyOrNull(invoiceSendResponseJson, "elementReferenceNumber")?.GetString();
		return (elementReferenceNumber ?? throw new ApplicationException("Missing reference number."), invoiceUTF8HashB64);
	}

	private async Task<(int status, string ksefReferenceNumber, DateTime acquisitionTimestamp)> GetInvoiceStatusAsync(string elementReferenceNumber)
	{
		var invoiceStatusResponse = await client.GetAsync(urlBase + "/api/online/Invoice/Status/" + elementReferenceNumber);
		var invoiceStatusResponseBody = await invoiceStatusResponse.Content.ReadAsStringAsync();
		var invoiceStatusResponseJson = JsonSerializer.Deserialize<JsonElement>(invoiceStatusResponseBody);
		ProcessException(invoiceStatusResponseJson);
		var processingCode = PropertyOrNull(invoiceStatusResponseJson, "processingCode")?.GetInt32() ?? throw new ApplicationException("Brak statusu wysyłki faktury.");
		var processingDescription = PropertyOrNull(invoiceStatusResponseJson, "processingDescription")?.GetString();
		if (processingCode >= 200 && processingCode <= 299)
		{
			var ksefReferenceNumber = PropertyOrNull(invoiceStatusResponseJson, "invoiceStatus", "ksefReferenceNumber")?.GetString();
			var acquisitionTimestamp = PropertyOrNull(invoiceStatusResponseJson, "invoiceStatus", "acquisitionTimestamp")?.GetDateTime();
			return (processingCode, ksefReferenceNumber, acquisitionTimestamp.Value);
		}
		else if (processingCode >= 300 && processingCode <= 399) return (processingCode, null, default);
		else throw new ApplicationException(processingDescription);
	}

	public async Task<(string ksefReferenceNumber, DateTime acquisitionTimestamp, string urlKSeF)> SendInvoiceAsync(string invoiceXml, string nip, DateTime issueDate, CancellationToken cancellationToken)
	{
		(var elementReferenceNumber, var invoiceHash) = await SendInvoiceAsync(invoiceXml);
		while (!cancellationToken.IsCancellationRequested)
		{
			await Task.Delay(1000, cancellationToken);
			(var status, var ksefReferenceNumber, var acquisitionTimestamp) = await GetInvoiceStatusAsync(elementReferenceNumber);
			if (status == 200) return (ksefReferenceNumber!, acquisitionTimestamp!, $"{urlBase}/web/verify/{ksefReferenceNumber}/{invoiceHash.Replace("=", "%3D").Replace("/", "%2F").Replace("+", "%2B")}");
		}

		return (default, default, default);
	}

	public async Task Terminate()
	{
		var terminateResponse = await client.GetAsync(urlBase + "/api/online/Session/Terminate");
		var terminateResponseBody = await terminateResponse.Content.ReadAsStringAsync();
		sesjaAktywna = false;
	}

	private JsonElement? PropertyOrNull(JsonElement element, params string[] nodes)
	{
		foreach (var node in nodes)
		{
			if (!element.TryGetProperty(node, out element)) return null;
		}
		return element;
	}

	protected virtual void Dispose(bool disposing)
	{
		if (sesjaAktywna)
		{
			Terminate().ConfigureAwait(false).GetAwaiter().GetResult();
		}
	}

	~API()
	{
		Dispose(disposing: false);
	}

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
