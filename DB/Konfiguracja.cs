namespace ProFak.DB;

class Konfiguracja : Rekord<Konfiguracja>
{
	public int Wersja { get; set; }

	// Werjsa 0
	public string SMTPSerwer { get; set; } = "";
	public string SMTPLogin { get; set; } = "";
	public string SMTPHaslo { get; set; } = "";
	public int SMTPPort { get; set; }

	public string EMailNadawca { get; set; } = "";
	public string EMailTemat { get; set; } = "";
	public string EMailTresc { get; set; } = "";

	// Wersja 1
	public bool SkrotyKlawiaturoweAkcji { get; set; }
	public bool SkrotyKlawiaturoweZakladek { get; set; }
	public bool SkrotyKlawiaturowePrzyciskow { get; set; }
	public bool IkonyAkcji { get; set; }
	public bool DomyslnyPodgladStrony { get; set; }
	public bool PotwierdzanieZamknieciaEdytora { get; set; }
	public bool PotwierdzanieZamknieciaProgramu { get; set; }
	public bool WstepneLadowanieReportingServices { get; set; }
	public int SzerokoscMenu { get; set; }
	public int RozmiarCzcionki { get; set; }
	public string NazwaCzcionki { get; set; } = "";

	public bool CzyDomyslna => SMTPSerwer == Domyslna.SMTPSerwer || String.IsNullOrEmpty(SMTPSerwer);

	public override bool CzyPasuje(string fraza) => false;

	public void AktualizujWersje()
	{
		if (Wersja < 0)
		{
			SMTPSerwer = "smtp.example.com";
			SMTPPort = 465;
			SMTPLogin = "biuro";
			SMTPHaslo = "tajnehaslo";
			EMailNadawca = "[SPRZEDAWCA-NAZWA] <[SPRZEDAWCA-EMAIL]>";
			EMailTemat = "Faktura - [NUMER]";
			EMailTresc = "Dzień dobry,\r\n\r\nw załączniku znajduje się faktura numer [NUMER] z dnia [DATA-SPRZEDAZY] na kwotę [KWOTA-BRUTTO] [WALUTA].\r\n\r\nWiadomość wygenerowana automatycznie.\r\n\r\n-- \r\n[SPRZEDAWCA-NAZWA]\r\n[SPRZEDAWCA-ADRES]";
			Wersja = 0;
		}
		if (Wersja < 1)
		{
			SkrotyKlawiaturoweAkcji = true;
			SkrotyKlawiaturoweZakladek = true;
			SkrotyKlawiaturowePrzyciskow = true;
			IkonyAkcji = true;
			DomyslnyPodgladStrony = true;
			PotwierdzanieZamknieciaEdytora = true;
			PotwierdzanieZamknieciaProgramu = false;
			WstepneLadowanieReportingServices = true;
			SzerokoscMenu = 270;
			RozmiarCzcionki = 0;
			NazwaCzcionki = "";
			Wersja = 1;
		}
	}

	public static Konfiguracja Domyslna { get; private set; }

	static Konfiguracja()
	{
		Domyslna = new Konfiguracja { Wersja = -1 };
		Domyslna.AktualizujWersje();
	}
}
