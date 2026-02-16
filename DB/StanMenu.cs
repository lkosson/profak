using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class StanMenu : Rekord<StanMenu>
	{
		public string Pozycja { get; set; } = "";
		public bool CzyZwinieta { get; set; }
		public bool CzyUkryta { get; set; }
		public bool CzyAktywna { get; set; }

		public override string ToString() => Pozycja;
	}
}
