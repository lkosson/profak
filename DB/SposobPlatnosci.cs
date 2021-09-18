using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class SposobPlatnosci : Rekord<SposobPlatnosci>
	{
		public string Nazwa { get; set; }
		public int LiczbaDni { get; set; }
		public bool CzyDomyslny { get; set; }
	}
}
