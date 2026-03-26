using KSeF.Client.Core.Models.Invoices;

namespace ProFak.IO.KSEF2;

public class InvoiceHeader
{
	public string ReferenceNumber { get; set; } = "";
	public string KsefReferenceNumber { get; set; } = "";
	public DateTime InvoicingDate { get; set; }
	public DateTime IssueDate { get; set; }
	public DateTime AcquisitionTimestamp { get; set; }
	public DateTime PermanentStorageTimestamp { get; set; }
	public string IssuedByName { get; set; } = "";
	public string IssuedByNIP { get; set; } = "";
	public string IssuedToName { get; set; } = "";
	public string IssuedToNIP { get; set; } = "";
	public decimal Net { get; set; }
	public decimal Gross { get; set; }
	public decimal Vat { get; set; }
	public string Currency { get; set; } = "";
	public string Type { get; set; } = "";

	public InvoiceHeader()
	{
	}

	public InvoiceHeader(InvoiceSummary summary, DateTimeOffset? hwm)
	{
		KsefReferenceNumber = summary.KsefNumber;
		ReferenceNumber = summary.InvoiceNumber;
		InvoicingDate = summary.InvoicingDate.LocalDateTime;
		IssueDate = summary.IssueDate.LocalDateTime;
		AcquisitionTimestamp = summary.AcquisitionDate.LocalDateTime;
		if (hwm.HasValue && summary.PermanentStorageDate > hwm.Value) PermanentStorageTimestamp = hwm.Value.LocalDateTime;
		else PermanentStorageTimestamp = summary.PermanentStorageDate.LocalDateTime;
		IssuedByNIP = summary.Seller.Nip;
		IssuedByName = summary.Seller.Name;
		IssuedToNIP = summary.Buyer.Identifier.Value;
		IssuedToName = summary.Buyer.Name;
		Net = summary.NetAmount;
		Gross = summary.GrossAmount;
		Vat = summary.VatAmount;
		Currency = summary.Currency;
		Type = summary.InvoiceType.ToString();
	}
}