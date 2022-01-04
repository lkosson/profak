using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak
{
	static class Rozszerzenia
	{
		public static string JakoJednaLinia(this string wejscie) => String.Join(", ", (wejscie ?? "").Split('\r', '\n').Where(linia => !String.IsNullOrWhiteSpace(linia)));
	}
}
