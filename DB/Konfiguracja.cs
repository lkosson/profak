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
	}
}
