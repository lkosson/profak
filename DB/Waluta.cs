﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Waluta : Rekord<Waluta>
	{
		public string Skrot { get; set; } = "";
		public string Nazwa { get; set; } = "";
		public bool CzyDomyslna { get; set; }

		public string CzyDomyslnaFmt => CzyDomyslna ? "Tak" : "Nie";

		public override bool CzyPasuje(string fraza)
			=> base.CzyPasuje(fraza)
			|| CzyPasuje(Skrot, fraza)
			|| CzyPasuje(Nazwa, fraza)
			|| CzyPasuje(CzyDomyslna ? "Domyślna" : "", fraza);
	}
}
