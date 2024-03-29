﻿using Microsoft.EntityFrameworkCore.Storage;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ProFak
{
	class Transakcja : IDisposable
	{
		private readonly Transakcja poprzednia;
		private readonly IDbContextTransaction dbContextTransaction;
		private readonly string savepoint;
		private readonly Baza baza;
		private bool zatwierdzona;
		private bool zakonczona;
		private static int sp;

		public bool CzyZatwierdzona => zatwierdzona;
		public bool CzyZamknieta => zakonczona;

		public Transakcja(Baza baza)
		{
			this.baza = baza;
			dbContextTransaction = baza.Database.BeginTransaction();
		}

		public Transakcja(Transakcja poprzednia)
		{
			this.poprzednia = poprzednia;
			savepoint = "SP" + Interlocked.Increment(ref sp);
			dbContextTransaction = poprzednia.dbContextTransaction;
			dbContextTransaction.CreateSavepoint(savepoint);
		}

		public void Zatwierdz()
		{
			if (zatwierdzona) throw new InvalidOperationException("Transakcja już jest zatwierdzona.");
			if (savepoint != null) dbContextTransaction.ReleaseSavepoint(savepoint);
			if (poprzednia == null) dbContextTransaction.Commit();
			zatwierdzona = true;
		}

		public void Dispose()
		{
			if (zatwierdzona) return;
			if (savepoint != null) dbContextTransaction.RollbackToSavepoint(savepoint);
			if (poprzednia == null) dbContextTransaction.Dispose();
			zakonczona = true;
		}
	}
}
