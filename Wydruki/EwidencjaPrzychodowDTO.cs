namespace ProFak.Wydruki
{
	public class EwidencjaPrzychodowDTO
	{
		public int LP { get; set; }
		public DateTime DataWpisu { get; set; }
		public DateTime DataPrzychodu { get; set; }
		public string? NumerDowodu { get; set; }
		public decimal Przychod17 { get; set; }
		public decimal Przychod15 { get; set; }
		public decimal Przychod14 { get; set; }
		public decimal Przychod125 { get; set; }
		public decimal Przychod12 { get; set; }
		public decimal Przychod10 { get; set; }
		public decimal Przychod85 { get; set; }
		public decimal Przychod55 { get; set; }
		public decimal Przychod3 { get; set; }
		public decimal PrzychodRazem { get; set; }
		public string? Uwagi { get; set; }
	}
}
