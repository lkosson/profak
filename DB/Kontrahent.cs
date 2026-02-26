namespace ProFak.DB;

public class Kontrahent : Rekord<Kontrahent>
{
	public string Nazwa { get; set; } = "";
	public string PelnaNazwa { get; set; } = "";
	public string NIP { get; set; } = "";
	public string AdresRejestrowy { get; set; } = "";
	public string AdresKorespondencyjny { get; set; } = "";
	public string RachunekBankowy { get; set; } = "";
	public string NazwaBanku { get; set; } = "";
	public string Telefon { get; set; } = "";
	public string EMail { get; set; } = "";
	public string UwagiWewnetrzne { get; set; } = "";
	public string UwagiPubliczne { get; set; } = "";
	public bool CzyArchiwalny { get; set; }
	public bool CzyPodmiot { get; set; }
	public bool CzyTP { get; set; }
	public int? SposobPlatnosciId { get; set; }

	public string KodUrzedu { get; set; } = "";
	public string OsobaFizycznaImie { get; set; } = "";
	public string OsobaFizycznaNazwisko { get; set; } = "";
	public DateTime? OsobaFizycznaDataUrodzenia { get; set; }
	public FormaOpodatkowania? FormaOpodatkowania { get; set; }
	public string TokenKSeF { get; set; } = "";
	public SrodowiskoKSeF SrodowiskoKSeF { get; set; }

	public Ref<SposobPlatnosci> SposobPlatnosciRef { get => SposobPlatnosciId; set => SposobPlatnosciId = value; }

	public SposobPlatnosci? SposobPlatnosci { get; set; }

	public string AdresRejestrowyFmt => AdresRejestrowy.JakoJednaLinia();
	public string PelnaNazwaLubNazwa => String.IsNullOrEmpty(PelnaNazwa) ? Nazwa : PelnaNazwa;

	public override bool CzyPasuje(string fraza)
		=> base.CzyPasuje(fraza)
		|| CzyPasuje(Nazwa, fraza)
		|| CzyPasuje(PelnaNazwa, fraza)
		|| CzyPasuje(NIP, fraza)
		|| CzyPasuje(AdresRejestrowy, fraza)
		|| CzyPasuje(AdresKorespondencyjny, fraza)
		|| CzyPasuje(RachunekBankowy, fraza)
		|| CzyPasuje(Telefon, fraza)
		|| CzyPasuje(EMail, fraza)
		|| CzyPasuje(UwagiWewnetrzne, fraza)
		|| CzyPasuje(CzyArchiwalny ? "Archiwalny" : "", fraza)
		|| CzyPasuje(CzyPodmiot ? "Podmiot" : "", fraza);
}

public enum FormaOpodatkowania
{
	Liniowy,
	Skala,
	Ryczałt
}

public enum SrodowiskoKSeF
{
	Test,
	Demo,
	Prod
}
