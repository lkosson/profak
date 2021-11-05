using Microsoft.EntityFrameworkCore.Storage;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class Kontekst : IDisposable
	{
		public Baza Baza { get; }
		public Dialog Dialog { get; set; }
		public Kontekst Poprzedni { get; }
		private Transakcja AktywnaTransakcja => lokalnaTransakcja ?? Poprzedni?.AktywnaTransakcja;
		private Transakcja lokalnaTransakcja;

		public Kontekst()
		{
			Baza = new Baza();
		}

		public Kontekst(Kontekst poprzedni)
		{
			Baza = poprzedni.Baza;
			Poprzedni = poprzedni;
		}

		public Transakcja Transakcja()
		{
			if (lokalnaTransakcja != null) throw new InvalidOperationException("Transakcja jest już aktywna.");
			lokalnaTransakcja = AktywnaTransakcja == null ? new Transakcja(Baza) : new Transakcja(AktywnaTransakcja);
			return lokalnaTransakcja;
		}

		public void Dispose()
		{
			if (Poprzedni == null) Baza.Dispose();
		}
	}
}
