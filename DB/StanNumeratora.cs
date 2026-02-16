namespace ProFak.DB
{
	class StanNumeratora : Rekord<StanNumeratora>
	{
		public int NumeratorId { get; set; }
		public string Parametry { get; set; } = "";
		public int OstatniaWartosc { get; set; }

		public Ref<Numerator> NumeratorRef { get => NumeratorId; set => NumeratorId = value; }

		public Numerator? Numerator { get; set; }

		public override bool CzyPasuje(string fraza)
			=> base.CzyPasuje(fraza)
			|| CzyPasuje(Parametry, fraza)
			|| CzyPasuje(OstatniaWartosc, fraza);
	}
}
