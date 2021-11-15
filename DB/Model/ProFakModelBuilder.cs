using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB.Model
{
	static class ProFakModelBuilder
	{
		public static void Configure(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Faktura>(FakturaBuilder.Configure);
			modelBuilder.Entity<JednostkaMiary>(JednostkaMiaryBuilder.Configure);
			modelBuilder.Entity<Kontrahent>(KontrahentBuilder.Configure);
			modelBuilder.Entity<Numerator>(NumeratorBuilder.Configure);
			modelBuilder.Entity<PozycjaFaktury>(PozycjaFakturyBuilder.Configure);
			modelBuilder.Entity<SposobPlatnosci>(SposobPlatnosciBuilder.Configure);
			modelBuilder.Entity<StanNumeratora>(StanNumeratoraBuilder.Configure);
			modelBuilder.Entity<StawkaVat>(StawkaVatBuilder.Configure);
			modelBuilder.Entity<Towar>(TowarBuilder.Configure);
			modelBuilder.Entity<Waluta>(WalutaBuilder.Configure);
			modelBuilder.Entity<Wplata>(WplataBuilder.Configure);
		}
	}
}
