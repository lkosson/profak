namespace ProFak.DB;

class DodatkowyPodmiot : Rekord<DodatkowyPodmiot>
{
	public int FakturaId { get; set; }
	public RodzajDodatkowegoPodmiotu Rodzaj { get; set; }
	public string IDwew { get; set; }
	public string Nazwa { get; set; } = "";
	public string NIP { get; set; } = "";
	public string VatUE { get; set; } = "";
	public string Adres { get; set; } = "";
	public string EMail { get; set; } = "";
	public string Telefon { get; set; } = "";
	public decimal? Udzial { get; set; }

	public string RodzajFmt => Format(Rodzaj);

	public Ref<Faktura> FakturaRef { get => FakturaId; set => FakturaId = value; }

	public Faktura Faktura { get; set; }

	public override bool CzyPasuje(string fraza)
		=> base.CzyPasuje(fraza)
		|| CzyPasuje(IDwew, fraza)
		|| CzyPasuje(Nazwa, fraza)
		|| CzyPasuje(NIP, fraza)
		|| CzyPasuje(VatUE, fraza)
		|| CzyPasuje(Adres, fraza)
		|| CzyPasuje(EMail, fraza)
		|| CzyPasuje(Telefon, fraza)
		|| CzyPasuje(Udzial, fraza);
}

enum RodzajDodatkowegoPodmiotu
{
	Faktor,
	Odbiorca,
	PodmiotPierwotny,
	DodatkowyNabywca,
	WystawcaFaktury,
	DokonującyPłatności,
	JSTWystawca,
	JSTOdbiorca,
	CzłonekGrupyVATWystawca,
	CzłonekGrupyVATOdbiorca,
	Pracownik
}
