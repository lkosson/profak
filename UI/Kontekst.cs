using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class Kontekst : IDisposable
	{
		public Baza Baza { get; }

		public Kontekst()
		{
			Baza = new Baza();
		}

		public void Dispose()
		{
			Baza.Dispose();
		}
	}
}
