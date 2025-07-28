#nullable enable
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KSeF.Client.Api.Services;
using KSeF.Client.Core.Interfaces;
using KSeF.Client.Core.Models.Invoices;
using KSeF.Client.Core.Models.Sessions;
using KSeFClient;
using KSeFClient.Core.Interfaces;
using KSeFClient.Http;
using ProFak.DB;
using ProFak.IO.KSEF;

namespace ProFak.IO.KSEF2;

class API : IDisposable
{
	private readonly HttpClient httpClient;
	private readonly IRestClient restClient;
	private readonly IKSeFClient ksefClient;
	private readonly ICryptographyService cryptographyService;
	private string? referenceNumber;
	private string accessToken;
	private static EncryptionData? encryptionData;

	public API(SrodowiskoKSeF srodowisko)
	{
		httpClient = new HttpClient();
		httpClient.BaseAddress = new Uri(srodowisko == SrodowiskoKSeF.Prod ? "https://ksef.mf.gov.pl"
			: srodowisko == SrodowiskoKSeF.Demo ? "https://ksef-demo.mf.gov.pl"
			: "https://ksef-test.mf.gov.pl");
		restClient = new RestClient(httpClient);
		ksefClient = new KSeFClient.Http.KSeFClient(restClient);
		cryptographyService = new CryptographyService(ksefClient, restClient);
	}

	public async Task AuthenticateAsync(string nip, string accessToken)
	{
		encryptionData = cryptographyService.GetEncryptionData();
		var openOnlineSessionRequest = OpenOnlineSessionRequestBuilder
			.Create()
			.WithFormCode(systemCode: "FA (2)", schemaVersion: "1-0E", value: "FA")
			.WithEncryption(
				encryptedSymmetricKey: encryptionData.EncryptionInfo.EncryptedSymmetricKey,
				initializationVector: encryptionData.EncryptionInfo.InitializationVector)
		 .Build();

		var openSessionResponse = await ksefClient.OpenOnlineSessionAsync(openOnlineSessionRequest, accessToken, CancellationToken.None);

		referenceNumber = openSessionResponse.ReferenceNumber;
		this.accessToken = accessToken;
	}

	public async Task<IReadOnlyCollection<InvoiceHeader>> GetInvoicesAsync(bool przyrostowo, bool sprzedaz, DateTime dateFrom, DateTime dateTo)
	{
		var pageSize = 100;
		var pageOffset = 0;
		var invoices = new List<InvoiceHeader>();
		while (true)
		{
			var body = new QueryInvoiceRequest
			{
				DateRange = new DateRange { DateType = przyrostowo ? DateType.Delivery : DateType.Issue, From = dateFrom, To = dateTo },
				SubjectType = sprzedaz ? SubjectType.Subject1 : SubjectType.Subject2
			};

			var pagedInvoiceResponse = await ksefClient.QueryInvoicesAsync(body, accessToken, pageOffset, pageSize, CancellationToken.None);
			foreach (var invoice in pagedInvoiceResponse.Invoices)
			{
				var invoiceHeader = new InvoiceHeader();
				invoiceHeader.KsefReferenceNumber = invoice.KsefNumber;
				invoiceHeader.ReferenceNumber = invoice.InvoiceNumber;
				invoiceHeader.InvoicingDate = invoice.InvoiceDate;
				invoiceHeader.AcquisitionTimestamp = invoice.AcquisitionDate;
				invoiceHeader.IssuedByNIP = invoice.Seller.Identifier;
				invoiceHeader.IssuedByName = invoice.Seller.Name;
				invoiceHeader.IssuedToNIP = invoice.Buyer.Identifier;
				invoiceHeader.IssuedToName = invoice.Buyer.Name;
				invoiceHeader.Net = invoice.NetAmount;
				invoiceHeader.Gross = invoice.GrossAmount;
				invoiceHeader.Vat = invoice.VatAmount;
				invoiceHeader.Currency = invoice.Currency;
				invoiceHeader.Type = invoice.InvoiceType.ToString();
				invoices.Add(invoiceHeader);
			}
			pageOffset += pagedInvoiceResponse.Invoices.Count;
			if (pagedInvoiceResponse.Invoices.Count < pageSize) break;
		}
		return invoices;
	}

	public async Task<string> GetInvoiceAsync(string ksefReferenceNumber)
	{
		return await ksefClient.GetInvoiceAsync(ksefReferenceNumber, accessToken, CancellationToken.None);
	}

	public async Task<(string ksefReferenceNumber, DateTime acquisitionTimestamp, string urlKSeF)> SendInvoiceAsync(string invoiceXml, CancellationToken cancellationToken)
	{
		var invoice = Encoding.UTF8.GetBytes(invoiceXml);
		var encryptedInvoice = cryptographyService.EncryptBytesWithAES256(invoice, encryptionData!.CipherKey, encryptionData!.CipherIv);

		var invoiceMetadata = cryptographyService.GetMetaData(invoice);
		var encryptedInvoiceMetadata = cryptographyService.GetMetaData(encryptedInvoice);

		var sendOnlineInvoiceRequest = SendInvoiceOnlineSessionRequestBuilder
			.Create()
			.WithDocumentHash(invoiceMetadata.HashSHA, invoiceMetadata.FileSize)
			.WithEncryptedDocumentHash(
			   encryptedInvoiceMetadata.HashSHA, encryptedInvoiceMetadata.FileSize)
			.WithEncryptedDocumentContent(Convert.ToBase64String(encryptedInvoice))
			.Build();

		var sendInvoiceResponse = await ksefClient.SendOnlineSessionInvoiceAsync(sendOnlineInvoiceRequest, referenceNumber, accessToken, cancellationToken)
			.ConfigureAwait(false);

		return (sendInvoiceResponse.ReferenceNumber, DateTime.Now, default);
	}

	public async Task Terminate()
	{
		await ksefClient.CloseOnlineSessionAsync(referenceNumber, accessToken, CancellationToken.None);
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!String.IsNullOrEmpty(referenceNumber))
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
