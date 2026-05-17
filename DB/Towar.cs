namespace ProFak.DB;

public class Towar : Rekord<Towar>
{
	public string Nazwa { get; set; } = "";
	public RodzajTowaru Rodzaj { get; set; } = RodzajTowaru.Towar;
	public decimal CenaNetto { get; set; }
	public decimal CenaBrutto { get; set; }
	public SposobLiczeniaCenyTowaru SposobLiczeniaCeny { get; set; }
	public bool CzyArchiwalny { get; set; }
	public int GTU { get; set; }
	public decimal? StawkaRyczaltu { get; set; }

	public int? StawkaVatId { get; set; }
	public int? JednostkaMiaryId { get; set; }

	public Ref<StawkaVat> StawkaVatRef { get => StawkaVatId; set => StawkaVatId = value; }
	public Ref<JednostkaMiary> JednostkaMiaryRef { get => JednostkaMiaryId; set => JednostkaMiaryId = value; }

	public StawkaVat? StawkaVat { get; set; }
	public JednostkaMiary? JednostkaMiary { get; set; }

	public override bool CzyPasuje(string fraza)
		=> base.CzyPasuje(fraza)
		|| CzyPasuje(Nazwa, fraza)
		|| CzyPasuje(Rodzaj, fraza)
		|| CzyPasuje(CenaNetto, fraza)
		|| CzyPasuje(CenaBrutto, fraza)
		|| CzyPasuje(Format(SposobLiczeniaCeny), fraza)
		|| CzyPasuje(CzyArchiwalny ? "Archiwalny" : "", fraza);
}

public enum RodzajTowaru
{
	Towar,
	Usługa
}

public enum SposobLiczeniaCenyTowaru
{
	WedługNetto,
	WedługBrutto,
	NarzutKwotowy,
	NarzutProcentowy
}
