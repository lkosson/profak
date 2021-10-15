using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class Kontekst : IDisposable
	{
		public Baza Baza { get; }
		public Dialog Dialog { get; set; }
		public Kontekst Poprzedni { get; }

		public Kontekst()
		{
			Baza = new Baza();
		}

		public Kontekst(Kontekst poprzedni)
		{
			Baza = poprzedni.Baza;
			Poprzedni = poprzedni;
		}

		public void Dispose()
		{
			if (Poprzedni == null) Baza.Dispose();
		}
	}
}
