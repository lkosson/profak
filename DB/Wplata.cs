namespace ProFak.DB;

public class Wplata : Rekord<Wplata>
{
	public int FakturaId { get; set; }
	public DateTime Data { get; set; } = DateTime.Now.Date;
	public decimal Kwota { get; set; }
	public string Uwagi { get; set; } = "";
	public bool CzyRozliczenie { get; set; }

	public Ref<Faktura> FakturaRef { get => FakturaId; set => FakturaId = value; }

	public Faktura? Faktura { get; set; }

	public bool CzyZaliczka => CzyRozliczenie && Uwagi != null && Uwagi.StartsWith("Zaliczka ");

	internal static string UwagiDlaZaliczki(Faktura zaliczka) => $"Zaliczka {zaliczka.Numer} z dnia {zaliczka.DataWystawienia.ToString(UI.Wyglad.FormatDaty)}";

	public override bool CzyPasuje(string fraza)
		=> base.CzyPasuje(fraza)
		|| CzyPasuje(Data, fraza)
		|| CzyPasuje(Kwota, fraza);
}
