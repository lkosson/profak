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
		private IDbContextTransaction transakcja;
		private string savepoint;
		private static int sp;

		public Kontekst()
		{
			Baza = new Baza();
			transakcja = Baza.Database.BeginTransaction();
			UtworzSavepoint();
		}

		public Kontekst(Kontekst poprzedni)
		{
			Baza = poprzedni.Baza;
			Poprzedni = poprzedni;
			transakcja = poprzedni.transakcja;
			UtworzSavepoint();
		}

		private void UtworzSavepoint()
		{
			savepoint = "SP" + Interlocked.Increment(ref sp);
			transakcja.CreateSavepoint(savepoint);
		}

		public void Zapisz()
		{
			Baza.SaveChanges();
			transakcja.ReleaseSavepoint(savepoint);
			if (Poprzedni == null) transakcja.Commit();
			savepoint = null;
		}

		public void Dispose()
		{
			if (savepoint != null) transakcja.RollbackToSavepoint(savepoint);
			if (Poprzedni == null) Baza.Dispose();
		}
	}
}
