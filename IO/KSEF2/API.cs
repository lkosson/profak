﻿#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KSeF.Client;
using KSeF.Client.Api.Builders.Auth;
using KSeF.Client.Api.Builders.X509Certificates;
using KSeF.Client.Core.Interfaces;
using KSeF.Client.Core.Models.Authorization;
using KSeF.Client.Core.Models.Invoices;
using KSeF.Client.Core.Models.Sessions;
using KSeF.Client.DI;
using Microsoft.Extensions.DependencyInjection;
using ProFak.DB;
using ProFak.IO.KSEF;

namespace ProFak.IO.KSEF2;

class API : IDisposable
{
	private static ServiceProvider? serviceProvider;
	private static SrodowiskoKSeF? srodowisko;

	private readonly IKSeFClient ksefClient;
	private readonly ICryptographyService cryptographyService;
	private readonly IVerificationLinkService verificationLinkService;
	private readonly ISignatureService signatureService;

	private TokenInfo accessToken;

	public API(SrodowiskoKSeF srodowisko)
	{
		if (API.srodowisko != srodowisko || serviceProvider == null)
		{
			if (serviceProvider != null)
			{
				serviceProvider.Dispose();
				serviceProvider = null;
			}

			var sc = new ServiceCollection();
			sc.AddKSeFClient(opts =>
			{
				opts.BaseUrl = srodowisko == SrodowiskoKSeF.Prod ? "https://ksef.mf.gov.pl"
					: srodowisko == SrodowiskoKSeF.Demo ? "https://ksef-demo.mf.gov.pl"
					: "https://ksef-test.mf.gov.pl";
				opts.CustomHeaders = [];
			});
			serviceProvider = sc.BuildServiceProvider();
			API.srodowisko = srodowisko;
		}

		ksefClient = serviceProvider.GetRequiredService<IKSeFClient>();
		cryptographyService = serviceProvider.GetRequiredService<ICryptographyService>();
		verificationLinkService = serviceProvider.GetRequiredService<IVerificationLinkService>();
		signatureService = serviceProvider.GetRequiredService<ISignatureService>();
		accessToken = default!;
	}

	private async Task<T> CzekajNaWynikAsync<T>(Func<Task<T>> test, Func<T, bool> wynikOk, TimeSpan czas)
	{
		var granica = DateTime.Now.Add(czas);
		T wynik;
		do
		{
			wynik = await test();
			if (wynikOk(wynik)) return wynik;
			await Task.Delay(TimeSpan.FromSeconds(1));
		}
		while (DateTime.Now < granica);
		throw new ApplicationException($"Nieoczekiwany rezultat: {wynik}");
	}

	public async Task AuthenticateAsync(string nip, string ksefToken)
	{
		await cryptographyService.WarmupAsync();
		var challenge = await ksefClient.GetAuthChallengeAsync();
		var timestamp = challenge.Timestamp.ToUnixTimeMilliseconds();
		var plaintextRequest = ksefToken + "|" + timestamp;
		var plaintextRequestBytes = Encoding.UTF8.GetBytes(plaintextRequest);
		var encryptedRequestBytes = cryptographyService.EncryptKsefTokenWithRSAUsingPublicKey(plaintextRequestBytes);
		var encryptedRequest = Convert.ToBase64String(encryptedRequestBytes);
		var request = new AuthKsefTokenRequest
		{
			Challenge = challenge.Challenge,
			ContextIdentifier = new AuthContextIdentifier
			{
				Type = ContextIdentifierType.Nip,
				Value = nip
			},
			EncryptedToken = encryptedRequest,
			AuthorizationPolicy = null
		};

		var authOperationInfo = await ksefClient.SubmitKsefTokenAuthRequestAsync(request, CancellationToken.None);
		await CzekajNaWynikAsync(() => ksefClient.GetAuthStatusAsync(authOperationInfo.ReferenceNumber, authOperationInfo.AuthenticationToken.Token),
			status => status.Status.Code >= 400 ? throw new ApplicationException($"Wystąpił błąd podczas próby uwierzytelnienia: {status.Status.Description} - {String.Join(", ", status.Status.Details)}") : status.Status.Code == 200,
			TimeSpan.FromSeconds(5));
		var tokens = await ksefClient.GetAccessTokenAsync(authOperationInfo.AuthenticationToken.Token);
		accessToken = tokens.AccessToken;
	}

	public async Task<string> AuthenticateSignatureBeginAsync(string nip)
	{
		var challenge = await ksefClient.GetAuthChallengeAsync();
		var authTokenRequest = AuthTokenRequestBuilder
			.Create()
			.WithChallenge(challenge.Challenge)
			.WithContext(ContextIdentifierType.Nip, nip)
			.WithIdentifierType(SubjectIdentifierTypeEnum.CertificateSubject)
			.Build();
		var xml = AuthTokenRequestSerializer.SerializeToXmlString(authTokenRequest);
		return xml;
	}

	public async Task<string> AuthenticateSignatureTestAsync(string unsignedXml, string nip)
	{
		var certificate = SelfSignedCertificateForSignatureBuilder
					.Create()
					.WithGivenName("ProFak")
					.WithSurname("Test")
					.WithSerialNumber("TINPL-" + nip)
					.WithCommonName("ProFak")
					.Build();
		var signedXml = await signatureService.SignAsync(unsignedXml, certificate);
		return signedXml;
	}

	public async Task AuthenticateSignatureEndAsync(string signedXml)
	{
		var authOperationInfo = await ksefClient.SubmitXadesAuthRequestAsync(signedXml, verifyCertificateChain: false);
		await CzekajNaWynikAsync(() => ksefClient.GetAuthStatusAsync(authOperationInfo.ReferenceNumber, authOperationInfo.AuthenticationToken.Token),
			status => status.Status.Code >= 400 ? throw new ApplicationException($"Wystąpił błąd podczas próby uwierzytelnienia: {status.Status.Description} - {String.Join(", ", status.Status.Details)}") : status.Status.Code == 200,
			TimeSpan.FromSeconds(10));
		var tokens = await ksefClient.GetAccessTokenAsync(authOperationInfo.AuthenticationToken.Token);
		accessToken = tokens.AccessToken;
	}

	public async Task<string> GenerateToken()
	{
		var request = new KsefTokenRequest { Permissions = [KsefTokenPermissionType.InvoiceRead, KsefTokenPermissionType.InvoiceWrite], Description = "ProFak" };
		var token = await ksefClient.GenerateKsefTokenAsync(request, accessToken.Token);
		await CzekajNaWynikAsync(() => ksefClient.GetKsefTokenAsync(token.ReferenceNumber, accessToken.Token),
			status => status.Status == AuthenticationKsefTokenStatus.Failed ? throw new ApplicationException($"Wystąpił błąd podczas generowania tokena: {String.Join(", ", status.StatusDetails)}") : status.Status == AuthenticationKsefTokenStatus.Active,
			TimeSpan.FromSeconds(10));
		return token.Token;
	}

	public async Task<(string sessionReferenceNumber, EncryptionData encryptionData)> OpenSessionAsync()
	{
		var encryptionData = cryptographyService.GetEncryptionData();
		var openOnlineSessionRequest = OpenOnlineSessionRequestBuilder
			.Create()
#if FA_3
			.WithFormCode(systemCode: "FA (3)", schemaVersion: "1-0E", value: "FA")
#else
			.WithFormCode(systemCode: "FA (2)", schemaVersion: "1-0E", value: "FA")
#endif
			.WithEncryption(
				encryptedSymmetricKey: encryptionData.EncryptionInfo.EncryptedSymmetricKey,
				initializationVector: encryptionData.EncryptionInfo.InitializationVector)
		 .Build();

		var openSessionResponse = await ksefClient.OpenOnlineSessionAsync(openOnlineSessionRequest, accessToken.Token, CancellationToken.None);
		return (openSessionResponse.ReferenceNumber, encryptionData);
	}

	public async Task CloseSessionAsync(string sessionReferenceNumber)
	{
		await ksefClient.CloseOnlineSessionAsync(sessionReferenceNumber, accessToken.Token);
	}

	public async Task<IReadOnlyCollection<InvoiceHeader>> GetInvoicesAsync(bool przyrostowo, bool sprzedaz, DateTime dateFrom, DateTime dateTo)
	{
		var pageSize = 100;
		var pageOffset = 0;
		var invoices = new List<InvoiceHeader>();
		while (true)
		{
			var query = new InvoiceQueryFilters
			{
				DateRange = new DateRange { DateType = przyrostowo ? DateType.PermanentStorage : DateType.Issue, From = dateFrom, To = dateTo },
				SubjectType = sprzedaz ? SubjectType.Subject1 : SubjectType.Subject2
			};

			var pagedInvoiceResponse = await ksefClient.QueryInvoiceMetadataAsync(query, accessToken.Token, pageOffset, pageSize, CancellationToken.None);
			foreach (var invoice in pagedInvoiceResponse.Invoices)
			{
				var invoiceHeader = new InvoiceHeader();
				invoiceHeader.KsefReferenceNumber = invoice.KsefNumber;
				invoiceHeader.ReferenceNumber = invoice.InvoiceNumber;
				invoiceHeader.InvoicingDate = invoice.InvoicingDate.LocalDateTime;
				invoiceHeader.AcquisitionTimestamp = invoice.AcquisitionDate.LocalDateTime;
				invoiceHeader.IssuedByNIP = invoice.Seller.Identifier;
				invoiceHeader.IssuedByName = invoice.Seller.Name;
				invoiceHeader.IssuedToNIP = invoice.Buyer.Identifier.Value;
				invoiceHeader.IssuedToName = invoice.Buyer.Name;
				invoiceHeader.Net = (decimal)invoice.NetAmount;
				invoiceHeader.Gross = (decimal)invoice.GrossAmount;
				invoiceHeader.Vat = (decimal)invoice.VatAmount;
				invoiceHeader.Currency = invoice.Currency;
				invoiceHeader.Type = invoice.InvoiceType.ToString();
				invoices.Add(invoiceHeader);
			}
			pageOffset++;
			if (pagedInvoiceResponse.Invoices.Count < pageSize) break;
		}
		return invoices;
	}

	public async Task<string> GetInvoiceAsync(string ksefReferenceNumber)
	{
		return await ksefClient.GetInvoiceAsync(ksefReferenceNumber, accessToken.Token, CancellationToken.None);
	}

	public async Task<(string ksefReferenceNumber, string verificationLink)> SendInvoiceAsync(string sessionReferenceNumber, EncryptionData encryptionData, string invoiceXml, string nip, DateTime issueDate, CancellationToken cancellationToken)
	{
		await OpenSessionAsync();

		var invoice = Encoding.UTF8.GetBytes(invoiceXml);
		var encryptedInvoice = cryptographyService.EncryptBytesWithAES256(invoice, encryptionData.CipherKey, encryptionData.CipherIv);

		var invoiceMetadata = cryptographyService.GetMetaData(invoice);
		var encryptedInvoiceMetadata = cryptographyService.GetMetaData(encryptedInvoice);

		var sendOnlineInvoiceRequest = SendInvoiceOnlineSessionRequestBuilder
			.Create()
			.WithInvoiceHash(invoiceMetadata.HashSHA, invoiceMetadata.FileSize)
			.WithEncryptedDocumentHash(encryptedInvoiceMetadata.HashSHA, encryptedInvoiceMetadata.FileSize)
			.WithEncryptedDocumentContent(Convert.ToBase64String(encryptedInvoice))
			.Build();

		var sendInvoiceResponse = await ksefClient.SendOnlineSessionInvoiceAsync(sendOnlineInvoiceRequest, sessionReferenceNumber, accessToken.Token, cancellationToken)
			.ConfigureAwait(false);

		var url = verificationLinkService.BuildInvoiceVerificationUrl(nip, issueDate, encryptedInvoiceMetadata.HashSHA);

		return (sendInvoiceResponse.ReferenceNumber, url);
	}

	public async Task FillSessionInvoiceMetadata(string sessionReferenceNumber, IEnumerable<(Faktura faktura, string invoiceReferenceNumber)> faktury)
	{
		var sessionInvoices = await ksefClient.GetSessionInvoicesAsync(sessionReferenceNumber, accessToken.Token);
		foreach (var sessionInvoice in sessionInvoices.Invoices)
		{
			var faktura = faktury.Where(e => e.invoiceReferenceNumber == sessionInvoice.ReferenceNumber).Select(e => e.faktura).FirstOrDefault();
			if (faktura == null) continue;
			faktura.NumerKSeF = sessionInvoice.KsefNumber;
			faktura.DataKSeF = sessionInvoice.AcquisitionDate!.Value.ToLocalTime().DateTime;
		}
	}

	public Faktura WczytajNaglowek(InvoiceHeader invoiceHeader)
	{
		var dbFaktura = new Faktura();
		dbFaktura.Numer = invoiceHeader.ReferenceNumber;
		dbFaktura.NumerKSeF = invoiceHeader.KsefReferenceNumber;
		dbFaktura.NazwaNabywcy = invoiceHeader.IssuedToName;
		dbFaktura.NIPNabywcy = invoiceHeader.IssuedToNIP;
		dbFaktura.NazwaSprzedawcy = invoiceHeader.IssuedByName;
		dbFaktura.NIPSprzedawcy = invoiceHeader.IssuedByNIP;
		dbFaktura.RazemNetto = invoiceHeader.Net;
		dbFaktura.RazemVat = invoiceHeader.Vat;
		dbFaktura.RazemBrutto = invoiceHeader.Gross;
		dbFaktura.Rodzaj = invoiceHeader.Type == "Vat" ? DB.RodzajFaktury.Zakup : DB.RodzajFaktury.KorektaZakupu;
		dbFaktura.DataSprzedazy = invoiceHeader.InvoicingDate;
		dbFaktura.DataWystawienia = invoiceHeader.AcquisitionTimestamp;
		dbFaktura.DataKSeF = invoiceHeader.AcquisitionTimestamp;
		dbFaktura.Waluta = new Waluta { Skrot = invoiceHeader.Currency };
		return dbFaktura;
	}

	public Task Terminate()
	{
		return Task.CompletedTask;
	}

	protected virtual void Dispose(bool disposing)
	{
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
