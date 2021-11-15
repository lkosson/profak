using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class StawkaVat : Rekord<StawkaVat>
	{
		public string Skrot { get; set; } = "";
		public decimal Wartosc { get; set; }
		public bool CzyDomyslna { get; set; }

		public string CzyDomyslnaFmt => CzyDomyslna ? "Tak" : "Nie";
	}
}
