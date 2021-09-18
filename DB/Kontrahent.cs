using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Kontrahent : Rekord
	{
		public string Nazwa { get; set; }
		public string PelnaNazwa { get; set; }
		public string NIP { get; set; }
		public string AdresRejestrowy { get; set; }
		public string AdresKorespondencyjny { get; set; }
		public string RachunekBankowy { get; set; }
		public string Telefon { get; set; }
		public string EMail { get; set; }
		public string Uwagi { get; set; }
		public bool CzyArchiwalny { get; set; }
		public bool CzyPodmiot { get; set; }
	}
}
