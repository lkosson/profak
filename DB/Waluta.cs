using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Waluta : Rekord<Waluta>
	{
		public string Skrot { get; set; }
		public string Nazwa { get; set; }
		public bool CzyGlowna { get; set; }
	}
}
