using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class UrzadSkarbowy : Rekord<UrzadSkarbowy>
	{
		public string Kod { get; set; } = "";
		public string Nazwa { get; set; } = "";

		public override bool CzyPasuje(string fraza)
			=> base.CzyPasuje(fraza)
			|| CzyPasuje(Kod, fraza)
			|| CzyPasuje(Nazwa, fraza);
	}
}
