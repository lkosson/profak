namespace ProFak.Wydruki
{
	public class PKPiRDTO
	{
		public string? Tytul { get; set; }
		public string? Podmiot { get; set; }

		public int LP { get; set; }
		public DateTime Data { get; set; }
		public string? Numer { get; set; }
		public string? Kontrahent { get; set; }
		public string? Adres { get; set; }
		public string? Opis { get; set; }

		public decimal PrzychodWartosc { get; set; }
		public decimal PrzychodPozostale { get; set; }
		public decimal PrzychodRazem { get; set; }

		public decimal KosztyZakup { get; set; }
		public decimal KosztyUboczne { get; set; }

		public decimal KosztyWynagrodzenia { get; set; }
		public decimal KosztyPozostale { get; set; }
		public decimal KosztyRazem { get; set; }
		public decimal KosztyInne { get; set; }

		public string? Uwagi { get; set; }
	}
}
