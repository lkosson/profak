using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Kontrahent : Rekord<Kontrahent>
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

		public Kontrahent()
		{
			Nazwa = "";
			PelnaNazwa = "";
			NIP = "";
			AdresRejestrowy = "";
			AdresKorespondencyjny = "";
			RachunekBankowy = "";
			Telefon = "";
			EMail = "";
			Uwagi = "";
		}
	}
}
