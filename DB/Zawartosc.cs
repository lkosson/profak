namespace ProFak.DB
{
	class Zawartosc : Rekord<Zawartosc>
	{
		public byte[] Dane { get; set; } = default!;
		public int? PlikId { get; set; }

		public Ref<Plik> PlikRef { get => PlikId; set => PlikId = value; }

		public Plik? Plik { get; set; }
	}
}
