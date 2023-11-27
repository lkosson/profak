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

namespace ProFak.IO.KSEF;

class API
{
	private readonly HttpClient client;
	private readonly string urlBase;
	private readonly string pubkey;

	public API(bool prod)
	{
		client = new HttpClient();
		urlBase = prod ? "" : "https://ksef-test.mf.gov.pl";
		pubkey = prod ? "" : "-----BEGIN PUBLIC KEY-----\r\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAuWosgHSpiRLadA0fQbzshi5TluliZfDsJujPlyYqp6A3qnzS3WmHxtwgO58uTbemQ1HCC2qwrMwuJqR6l8tgA4ilBMDbEEtkzgbjkJ6xoEqBptgxivP/ovOFYYoAnY6brZhXytCamSvjY9KI0g0McRk24pOueXT0cbb0tlwEEjVZ8NveQNKT2c1EEE2cjmW0XB3UlIBqNqiY2rWF86DcuFDTUy+KzSmTJTFvU/ENNyLTh5kkDOmB1SY1Zaw9/Q6+a4VJ0urKZPw+61jtzWmucp4CO2cfXg9qtF6cxFIrgfbtvLofGQg09Bh7Y6ZA5VfMRDVDYLjvHwDYUHg2dPIk0wIDAQAB\r\n-----END PUBLIC KEY-----";
	}

	private async Task<(string challenge, DateTime timestamp)> AuthorisationChallengeAsync(string nip)
	{
		var authorisationChallenge = new { contextIdentifier = new { type = "onip", identifier = nip } };
		var authorisationChallengeContent = JsonContent.Create(authorisationChallenge);
		var authorisationChallengeRequest = new HttpRequestMessage(HttpMethod.Post, urlBase + "/api/online/Session/AuthorisationChallenge") { Content = authorisationChallengeContent };
		var authorisationChallengeResponse = await client.SendAsync(authorisationChallengeRequest);
		var authorisationChallengeResponseBody = await authorisationChallengeResponse.Content.ReadAsStringAsync();
		var authorisationChallengeResponseJson = JsonSerializer.Deserialize<JsonElement>(authorisationChallengeResponseBody);
		var challenge = authorisationChallengeResponseJson.GetProperty("challenge").GetString();
		var timestamp = authorisationChallengeResponseJson.GetProperty("timestamp").GetDateTime().ToUniversalTime();
		if (challenge == null) throw new ApplicationException("Missing challenge.");
		return (challenge, timestamp);
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
				<ns2:SystemCode>FA (1)</ns2:SystemCode>
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
		var sessionToken = initTokenResponseJson.GetProperty("sessionToken").GetProperty("token").GetString();
		return sessionToken ?? throw new ApplicationException("Missin session token.");
	}

	public async Task AuthenticateAsync(string nip, string authorisationToken)
	{
		(var challenge, var timestamp) = await AuthorisationChallengeAsync(nip);
		var sessionToken = await InitTokenAsync(nip, authorisationToken, challenge, timestamp);
		client.DefaultRequestHeaders.Add("SessionToken", sessionToken);
	}

	private async Task<IReadOnlyCollection<InvoiceHeader>> GetInvoicesAsync(string type, string subject, DateTime dateFrom, DateTime dateTo, int pageSize, int pageOffset)
	{
		var invoices = new List<InvoiceHeader>();
		var invoiceSync = new { queryCriteria = new { type, subjectType = subject, acquisitionTimestampThresholdFrom = dateFrom.ToString("s"), acquisitionTimestampThresholdTo = dateTo.ToString("s") } };
		var invoiceSyncContent = JsonContent.Create(invoiceSync);
		var invoiceSyncRequest = new HttpRequestMessage(HttpMethod.Post, urlBase + $"/api/online/Query/Invoice/Sync?PageSize={pageSize}&PageOffset={pageOffset}") { Content = invoiceSyncContent };
		var invoiceSyncResponse = await client.SendAsync(invoiceSyncRequest);
		var invoiceSyncResponseBody = await invoiceSyncResponse.Content.ReadAsStringAsync();
		var invoiceSyncResponseJson = JsonSerializer.Deserialize<JsonElement>(invoiceSyncResponseBody);
		var invoiceHeaderList = invoiceSyncResponseJson.GetProperty("invoiceHeaderList");
		foreach (var invoiceHeaderListElement in invoiceHeaderList.EnumerateArray())
		{
			var invoiceHeader = new InvoiceHeader();
			invoiceHeader.ReferenceNumber = invoiceHeaderListElement.GetProperty("invoiceReferenceNumber").GetString()!;
			invoiceHeader.KsefReferenceNumber = invoiceHeaderListElement.GetProperty("ksefReferenceNumber").GetString()!;
			invoiceHeader.InvoicingDate = invoiceHeaderListElement.GetProperty("invoicingDate").GetDateTime();
			invoiceHeader.AcquisitionTimestamp = invoiceHeaderListElement.GetProperty("acquisitionTimestamp").GetDateTime();
			invoiceHeader.IssuedByNIP = invoiceHeaderListElement.GetProperty("subjectBy").GetProperty("issuedByIdentifier").GetProperty("identifier").GetString()!;
			invoiceHeader.IssuedByName = invoiceHeaderListElement.GetProperty("subjectBy").GetProperty("issuedByName").GetProperty("fullName").GetString()!;
			invoiceHeader.IssuedToNIP = invoiceHeaderListElement.GetProperty("subjectTo").GetProperty("issuedToIdentifier").GetProperty("identifier").GetString()!;
			invoiceHeader.IssuedToName = invoiceHeaderListElement.GetProperty("subjectTo").GetProperty("issuedToName").GetProperty("fullName").GetString()!;
			invoiceHeader.Net = invoiceHeaderListElement.GetProperty("net").GetDecimal();
			invoiceHeader.Gross = invoiceHeaderListElement.GetProperty("gross").GetDecimal();
			invoiceHeader.Vat = invoiceHeaderListElement.GetProperty("vat").GetDecimal();
			invoiceHeader.Currency = invoiceHeaderListElement.GetProperty("currency").GetString()!;
			invoiceHeader.Type = invoiceHeaderListElement.GetProperty("invoiceType").GetString()!;
			invoices.Add(invoiceHeader);

		}
		return invoices;
	}

	public async Task<IReadOnlyCollection<InvoiceHeader>> GetInvoicesAsync(string type, string subject, DateTime dateFrom, DateTime dateTo)
	{
		var pageSize = 100;
		var pageOffset = 0;
		var invoices = new List<InvoiceHeader>();
		while (true)
		{
			var page = await GetInvoicesAsync(type, subject, dateFrom, dateTo, pageSize, pageOffset);
			invoices.AddRange(page);
			pageOffset += page.Count;
			if (page.Count < pageSize) break;
		}
		return invoices;
	}

	public async Task<string> GetInvoiceAsync(string ksefReferenceNumber)
	{
		var fullInvoiceGetResponse = await client.GetAsync("https://ksef-test.mf.gov.pl/api/online/Invoice/Get/" + ksefReferenceNumber);
		var fullInvoiceGetResponseBody = await fullInvoiceGetResponse.Content.ReadAsStringAsync();
		return fullInvoiceGetResponseBody;
	}

	private async Task<string> SendInvoiceAsync(string invoiceXml)
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
		var elementReferenceNumber = invoiceSendResponseJson.GetProperty("elementReferenceNumber").GetString();
		return elementReferenceNumber ?? throw new ApplicationException("Missin session token.");
	}

	private async Task<(int status, string ksefReferenceNumber)> GetInvoiceStatusAsync(string elementReferenceNumber)
	{
		var invoiceStatusResponse = await client.GetAsync(urlBase + "/api/online/Invoice/Status/" + elementReferenceNumber);
		var invoiceStatusResponseBody = await invoiceStatusResponse.Content.ReadAsStringAsync();
		var invoiceStatusResponseJson = JsonSerializer.Deserialize<JsonElement>(invoiceStatusResponseBody);
		var processingCode = invoiceStatusResponseJson.GetProperty("processingCode").GetInt32();
		var ksefReferenceNumber = invoiceStatusResponseJson.GetProperty("invoiceStatus").GetProperty("ksefReferenceNumber").GetString();
		return (processingCode, ksefReferenceNumber);
	}

	public async Task<string> SendInvoiceAsync(string invoiceXml, CancellationToken cancellationToken)
	{
		var elementReferenceNumber = await SendInvoiceAsync(invoiceXml);
		while (!cancellationToken.IsCancellationRequested)
		{
			(var status, var ksefReferenceNumber) = await GetInvoiceStatusAsync(elementReferenceNumber);
			if (status == 200) return ksefReferenceNumber!;
			await Task.Delay(1000, cancellationToken);
		}

		return null!;
	}

	public async Task Terminate()
	{
		var terminateResponse = await client.GetAsync(urlBase + "/api/online/Session/Terminate");
		var terminateResponseBody = await terminateResponse.Content.ReadAsStringAsync();
	}
}
