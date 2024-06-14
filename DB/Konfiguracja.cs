using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Konfiguracja : Rekord<Konfiguracja>
	{
		public string SMTPSerwer { get; set; }
		public string SMTPLogin { get; set; }
		public string SMTPHaslo { get; set; }
		public int SMTPPort { get; set; }

		public string EMailNadawca { get; set; }
		public string EMailTemat { get; set; }
		public string EMailTresc { get; set; }

		public override bool CzyPasuje(string fraza) => false;

		public static Konfiguracja Domyslna => new()
		{
			Id = 1,
			SMTPSerwer = "smtp.example.com",
			SMTPPort = 465,
			SMTPLogin = "biuro",
			SMTPHaslo = "tajnehaslo",
			EMailNadawca = "[SPRZEDAWCA-NAZWA] <[SPRZEDAWCA-EMAIL]>",
			EMailTemat = "Faktura - [NUMER]",
			EMailTresc = "Dzień dobry,\r\n\r\nw załączniku znajduje się faktura numer [NUMER] z dnia [DATA-SPRZEDAZY] na kwotę [KWOTA-BRUTTO].\r\n\r\nWiadomość wygenerowana automatycznie.\r\n\r\n-- \r\n[SPRZEDAWCA-NAZWA]\r\n[SPRZEDAWCA-ADRES]"
		};
	}
}
