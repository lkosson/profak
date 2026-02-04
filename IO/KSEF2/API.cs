#nullable enable
using KSeF.Client.Api.Builders.Auth;
using KSeF.Client.Api.Builders.Online;
using KSeF.Client.Api.Builders.X509Certificates;
using KSeF.Client.Api.Services;
using KSeF.Client.Core.Exceptions;
using KSeF.Client.Core.Interfaces.Clients;
using KSeF.Client.Core.Interfaces.Services;
using KSeF.Client.Core.Models.Authorization;
using KSeF.Client.Core.Models.Invoices;
using KSeF.Client.Core.Models.Sessions;
using KSeF.Client.DI;
using Microsoft.Extensions.DependencyInjection;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProFak.IO.KSEF2;

class API : IDisposable
{
	private static ServiceProvider? serviceProvider;
	private static SrodowiskoKSeF? srodowisko;

	private readonly IKSeFClient ksefClient;
	private readonly ICryptographyService cryptographyService;
	private readonly IVerificationLinkService verificationLinkService;

	private TokenInfo accessToken;

	private static (TokenInfo accessToken, SrodowiskoKSeF srodowisko, string nip) cachedAuth;

	public API(SrodowiskoKSeF srodowisko)
	{
		if (API.srodowisko != srodowisko || serviceProvider == null)
		{
			serviceProvider?.Dispose();
			serviceProvider = null;

			var sc = new ServiceCollection();
			sc.AddKSeFClient(opts =>
			{
				opts.BaseUrl = srodowisko == SrodowiskoKSeF.Prod ? "https://api.ksef.mf.gov.pl"
					: srodowisko == SrodowiskoKSeF.Demo ? "https://api-demo.ksef.mf.gov.pl"
					: "https://api-test.ksef.mf.gov.pl";
				opts.CustomHeaders = [];
			});
			sc.AddCryptographyClient();
			serviceProvider = sc.BuildServiceProvider();
			API.srodowisko = srodowisko;
		}

		ksefClient = serviceProvider.GetRequiredService<IKSeFClient>();
		cryptographyService = serviceProvider.GetRequiredService<ICryptographyService>();
		verificationLinkService = serviceProvider.GetRequiredService<IVerificationLinkService>();
		accessToken = default!;
	}

	private async Task<T> CzekajNaWynikAsync<T>(Func<Task<T>> test, Func<T, bool> wynikOk, CancellationToken cancellationToken)
	{
	ponow:
		var wynik = await test();
		if (wynikOk(wynik)) return wynik;
		if (!cancellationToken.IsCancellationRequested)
		{
			await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
			goto ponow;
		}
		throw new ApplicationException($"Nieoczekiwany rezultat: {wynik}");
	}

	private async Task<T> UruchomUwzgledniajacLimitAsync<T>(Func<Task<T>> akcja, CancellationToken cancellationToken)
	{
	powtorz:
		try
		{
			return await akcja();
		}
		catch (KsefRateLimitException exc)
		{
			var retry = exc.RetryAfterSeconds ?? 10;
			if (retry > 10) throw new ApplicationException($"Przekroczony limit wywołań API KSeF. Spróbuj ponownie za {(retry > 300 ? retry / 60 + " minut" : retry + " sekund")}.");
			await Task.Delay(TimeSpan.FromSeconds(retry), cancellationToken);
			goto powtorz;
		}
	}

	public async Task UwierzytelnijAsync(string nip, string ksefToken, CancellationToken cancellationToken)
	{
		nip = nip.Replace("-", "");
		if (cachedAuth.nip == nip && cachedAuth.srodowisko == srodowisko && cachedAuth.accessToken.ValidUntil > DateTime.Now.AddMinutes(1))
		{
			accessToken = cachedAuth.accessToken;
			return;
		}
		await cryptographyService.WarmupAsync(cancellationToken);
		var challenge = await ksefClient.GetAuthChallengeAsync(cancellationToken);
		var timestamp = challenge.Timestamp.ToUnixTimeMilliseconds();
		var plaintextRequest = ksefToken + "|" + timestamp;
		var plaintextRequestBytes = Encoding.UTF8.GetBytes(plaintextRequest);
		var encryptedRequestBytes = cryptographyService.EncryptKsefTokenWithRSAUsingPublicKey(plaintextRequestBytes);
		var encryptedRequest = Convert.ToBase64String(encryptedRequestBytes);
		var request = new AuthenticationKsefTokenRequest
		{
			Challenge = challenge.Challenge,
			ContextIdentifier = new AuthenticationTokenContextIdentifier
			{
				Type = AuthenticationTokenContextIdentifierType.Nip,
				Value = nip
			},
			EncryptedToken = encryptedRequest,
			AuthorizationPolicy = null
		};

		var authOperationInfo = await ksefClient.SubmitKsefTokenAuthRequestAsync(request, cancellationToken);
		await CzekajNaWynikAsync(() => ksefClient.GetAuthStatusAsync(authOperationInfo.ReferenceNumber, authOperationInfo.AuthenticationToken.Token, cancellationToken),
			status => status.Status.Code >= 400 ? throw new ApplicationException($"Wystąpił błąd podczas próby uwierzytelnienia: {status.Status.Description} - {String.Join(", ", status.Status.Details)}") : status.Status.Code == 200,
			cancellationToken);
		var tokens = await ksefClient.GetAccessTokenAsync(authOperationInfo.AuthenticationToken.Token, cancellationToken);
		accessToken = tokens.AccessToken;
		cachedAuth = (accessToken, srodowisko!.Value, nip);
	}

	public async Task<string> PobierzZadanieDostepuDoPodpisuAsync(string nip)
	{
		var challenge = await ksefClient.GetAuthChallengeAsync();
		var authTokenRequest = AuthTokenRequestBuilder
			.Create()
			.WithChallenge(challenge.Challenge)
			.WithContext(AuthenticationTokenContextIdentifierType.Nip, nip.Replace("-", ""))
			.WithIdentifierType(AuthenticationTokenSubjectIdentifierTypeEnum.CertificateSubject)
			.Build();
		var xml = AuthenticationTokenRequestSerializer.SerializeToXmlString(authTokenRequest);
		return xml;
	}

	public string PodpiszZadanieDlaSrodowiskaTestowego(string xml, string nip)
	{
		var certificate = SelfSignedCertificateForSignatureBuilder
			.Create()
			.WithGivenName("ProFak")
			.WithSurname("Test")
			.WithSerialNumber("TINPL-" + nip.Replace("-", ""))
			.WithCommonName("ProFak")
			.Build();
		var signedXml = SignatureService.Sign(xml, certificate);
		return signedXml;
	}

	public async Task PrzeslijZadanieDostepuAsync(string podpisanyXml, CancellationToken cancellationToken)
	{
		var authOperationInfo = await ksefClient.SubmitXadesAuthRequestAsync(podpisanyXml, verifyCertificateChain: false, cancellationToken: cancellationToken);
		await CzekajNaWynikAsync(() => ksefClient.GetAuthStatusAsync(authOperationInfo.ReferenceNumber, authOperationInfo.AuthenticationToken.Token, cancellationToken: cancellationToken),
			status => status.Status.Code >= 400 ? throw new ApplicationException($"Wystąpił błąd podczas próby uwierzytelnienia: {status.Status.Description} - {String.Join(", ", status.Status.Details)}") : status.Status.Code == 200,
			cancellationToken);
		var tokens = await ksefClient.GetAccessTokenAsync(authOperationInfo.AuthenticationToken.Token);
		accessToken = tokens.AccessToken;
	}

	public async Task<string> UtworzTokenAsync(CancellationToken cancellationToken)
	{
		var request = new KsefTokenRequest { Permissions = [KsefTokenPermissionType.InvoiceRead, KsefTokenPermissionType.InvoiceWrite], Description = "ProFak" };
		var token = await ksefClient.GenerateKsefTokenAsync(request, accessToken.Token);
		await CzekajNaWynikAsync(() => ksefClient.GetKsefTokenAsync(token.ReferenceNumber, accessToken.Token, cancellationToken: cancellationToken),
			status => status.Status == AuthenticationKsefTokenStatus.Failed ? throw new ApplicationException($"Wystąpił błąd podczas generowania tokena: {String.Join(", ", status.StatusDetails)}") : status.Status == AuthenticationKsefTokenStatus.Active,
			cancellationToken);
		return token.Token;
	}

	public async Task<(string sessionReferenceNumber, EncryptionData encryptionData)> RozpocznijSesjeAsync(CancellationToken cancellationToken)
	{
		var encryptionData = cryptographyService.GetEncryptionData();
		var openOnlineSessionRequest = OpenOnlineSessionRequestBuilder
			.Create()
			.WithFormCode(systemCode: "FA (3)", schemaVersion: "1-0E", value: "FA")
			.WithEncryption(
				encryptedSymmetricKey: encryptionData.EncryptionInfo.EncryptedSymmetricKey,
				initializationVector: encryptionData.EncryptionInfo.InitializationVector)
		 .Build();

		var openSessionResponse = await UruchomUwzgledniajacLimitAsync(() => ksefClient.OpenOnlineSessionAsync(openOnlineSessionRequest, accessToken.Token, cancellationToken: cancellationToken), cancellationToken);
		return (openSessionResponse.ReferenceNumber, encryptionData);
	}

	public async Task ZakonczSesjeAsync(string sessionReferenceNumber, CancellationToken cancellationToken)
	{
		await ksefClient.CloseOnlineSessionAsync(sessionReferenceNumber, accessToken.Token, cancellationToken);
	}

	public async Task<IReadOnlyCollection<InvoiceHeader>> PobierzFakturyAsync(bool przyrostowo, bool sprzedaz, DateTime dateFrom, DateTime dateTo, CancellationToken cancellationToken)
	{
		var pageSize = 100;
		var pageOffset = 0;
		var invoices = new List<InvoiceHeader>();
		while (true)
		{
			if (cancellationToken.IsCancellationRequested) break;
			var query = new InvoiceQueryFilters
			{
				DateRange = new DateRange { DateType = przyrostowo ? DateType.PermanentStorage : DateType.Issue, From = DateTime.SpecifyKind(dateFrom, DateTimeKind.Local), To = DateTime.SpecifyKind(dateTo, DateTimeKind.Local) },
				SubjectType = sprzedaz ? InvoiceSubjectType.Subject1 : InvoiceSubjectType.Subject2
			};

			var pagedInvoiceResponse = await UruchomUwzgledniajacLimitAsync(() => ksefClient.QueryInvoiceMetadataAsync(query, accessToken.Token, pageOffset, pageSize, cancellationToken: cancellationToken), cancellationToken);

			foreach (var invoice in pagedInvoiceResponse.Invoices)
			{
				var invoiceHeader = new InvoiceHeader();
				invoiceHeader.KsefReferenceNumber = invoice.KsefNumber;
				invoiceHeader.ReferenceNumber = invoice.InvoiceNumber;
				invoiceHeader.InvoicingDate = invoice.InvoicingDate.LocalDateTime;
				invoiceHeader.AcquisitionTimestamp = invoice.AcquisitionDate.LocalDateTime;
				invoiceHeader.PermanentStorageTimestamp = invoice.PermanentStorageDate.LocalDateTime;
				invoiceHeader.IssuedByNIP = invoice.Seller.Nip;
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

	public async Task<string> PobierzFaktureAsync(string ksefReferenceNumber, CancellationToken cancellationToken)
	{
		var xml = await UruchomUwzgledniajacLimitAsync(() => ksefClient.GetInvoiceAsync(ksefReferenceNumber, accessToken.Token, cancellationToken), cancellationToken);
		return xml;
	}

	public async Task<string> WyslijFaktureAsync(string sessionReferenceNumber, EncryptionData encryptionData, string invoiceXml, CancellationToken cancellationToken)
	{
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

		var sendInvoiceResponse = await UruchomUwzgledniajacLimitAsync(() => ksefClient.SendOnlineSessionInvoiceAsync(sendOnlineInvoiceRequest, sessionReferenceNumber, accessToken.Token, cancellationToken), cancellationToken);

		return sendInvoiceResponse.ReferenceNumber;
	}

	public string ZbudujUrl(string invoiceXml, string nip, DateTime issueDate)
	{
		var invoice = Encoding.UTF8.GetBytes(invoiceXml);
		var invoiceMetadata = cryptographyService.GetMetaData(invoice);
		var url = verificationLinkService.BuildInvoiceVerificationUrl(nip, issueDate, invoiceMetadata.HashSHA);
		return url;
	}

	public async Task SprawdzStanWysylkiAsync(string sessionReferenceNumber, IEnumerable<(Faktura faktura, string invoiceReferenceNumber)> faktury, CancellationToken cancellationToken)
	{
		var sessionInvoices = await CzekajNaWynikAsync(() => ksefClient.GetSessionInvoicesAsync(sessionReferenceNumber, accessToken.Token),
			invoices => invoices.Invoices.All(invoice => invoice.Status.Code != 150),
			cancellationToken);

		foreach (var sessionInvoice in sessionInvoices.Invoices)
		{
			if (sessionInvoice.Status.Code >= 400) throw new ApplicationException($"Wystąpił błąd podczas wysyłki: {sessionInvoice.Status.Description} - {String.Join(", ", sessionInvoice.Status.Details)}");
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
		dbFaktura.DataKSeF = invoiceHeader.PermanentStorageTimestamp;
		dbFaktura.Waluta = new Waluta { Skrot = invoiceHeader.Currency };
		return dbFaktura;
	}

	public void Dispose()
	{
	}
}
