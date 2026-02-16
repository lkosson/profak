namespace ProFak.Wydruki
{
	public class FakturaDTO
	{
		public string? Numer { get; set; }
		public string? Rodzaj { get; set; }
		public string? Korekta { get; set; }
		public string? DataWystawienia { get; set; }
		public string? DataSprzedazy { get; set; }

		public string? NazwaSprzedawcy { get; set; }
		public string? AdresSprzedawcy { get; set; }
		public string? NIPSprzedawcy { get; set; }

		public string? NazwaNabywcy { get; set; }
		public string? AdresNabywcy { get; set; }
		public string? NIPNabywcy { get; set; }

		public string? DaneOdbiorcy { get; set; }

		public string? Slownie { get; set; }
		public string? TerminPlatnosci { get; set; }
		public string? FormaPlatnosci { get; set; }
		public string? DoZaplaty { get; set; }
		public string? DoZwrotu { get; set; }
		public string? ProceduraMarzy { get; set; }
		public string? NumerRachunku { get; set; }
		public string? NazwaBanku { get; set; }
		public string? Uwagi { get; set; }

		public string? LP { get; set; }
		public string? NaglowekPozycji { get; set; }
		public string? OpisPozycji { get; set; }
		public decimal CenaNetto { get; set; }
		public string? Ilosc { get; set; }
		public decimal WartoscNetto { get; set; }
		public string? StawkaVAT { get; set; }
		public decimal WartoscVat { get; set; }
		public decimal WartoscBrutto { get; set; }
		public decimal RabatRazem { get; set; }
		public string? Rabat { get; set; }
		public string? Waluta { get; set; }
		public string? WalutaVAT { get; set; }
		public decimal KursWaluty { get; set; }
		public bool JestVAT { get; set; }
		public bool JestRabat { get; set; }

		public string? NumerKSeF { get; set; }
		public string? KodKSeF { get; set; }
	}
}
