using ProFak.DB;

namespace ProFak.IO.KSEF;

class Generator
{
	public static Faktura Zbuduj(InvoiceHeader invoiceHeader)
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
		dbFaktura.Rodzaj = invoiceHeader.Type == "VAT" ? DB.RodzajFaktury.Zakup : DB.RodzajFaktury.KorektaZakupu;
		dbFaktura.DataSprzedazy = invoiceHeader.InvoicingDate;
		dbFaktura.DataWystawienia = invoiceHeader.AcquisitionTimestamp;
		dbFaktura.DataKSeF = invoiceHeader.AcquisitionTimestamp;
		dbFaktura.Waluta = new Waluta { Skrot = invoiceHeader.Currency };
		return dbFaktura;
	}
}
