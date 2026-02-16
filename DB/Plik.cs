namespace ProFak.DB
{
	class Plik : Rekord<Plik>
	{
		public int FakturaId { get; set; }
		public string Nazwa { get; set; } = "";
		public int Rozmiar { get; set; }
		public int ZawartoscId { get; set; }

		public Ref<Faktura> FakturaRef { get => FakturaId; set => FakturaId = value; }
		public Ref<Zawartosc> ZawartoscRef { get => ZawartoscId; set => ZawartoscId = value; }

		public Faktura? Faktura { get; set; }
		public Zawartosc? Zawartosc { get; set; }

		public override bool CzyPasuje(string fraza)
			=> base.CzyPasuje(fraza)
			|| CzyPasuje(Nazwa, fraza)
			|| CzyPasuje(Rozmiar, fraza);
	}
}
