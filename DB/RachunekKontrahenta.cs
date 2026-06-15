namespace ProFak.DB;

public class RachunekKontrahenta : Rekord<RachunekKontrahenta>
{
	public int KontrahentId { get; set; }
	public string NumerRachunku { get; set; } = "";
	public string NazwaBanku { get; set; } = "";
	public int? WalutaId { get; set; }

	public Ref<Kontrahent> KontrahentRef { get => KontrahentId; set => KontrahentId = value; }
	public Ref<Waluta> WalutaRef { get => WalutaId; set => WalutaId = value; }

	public Kontrahent? Kontrahent { get; set; }
	public Waluta? Waluta { get; set; }

	public string WalutaFmt => Waluta?.Skrot ?? "";

	public override bool CzyPasuje(string fraza)
		=> base.CzyPasuje(fraza)
		|| CzyPasuje(NumerRachunku, fraza)
		|| CzyPasuje(NazwaBanku, fraza)
		|| (Waluta?.CzyPasuje(fraza) ?? false);
}
