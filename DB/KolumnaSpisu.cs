using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class KolumnaSpisu : Rekord<KolumnaSpisu>
	{
		public string Spis { get; set; }
		public string Kolumna { get; set; }
		public int Kolejnosc { get; set; }
		public int Szerokosc { get; set; }

		public override bool CzyPasuje(string fraza)
			=> base.CzyPasuje(fraza)
			|| CzyPasuje(Spis, fraza)
			|| CzyPasuje(Kolumna, fraza);
	}
}
