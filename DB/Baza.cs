using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Baza : DbContext
	{
		public static string NazwaKataloguProgramu => "ProFak";
		public static string NazwaPlikuBazy => "profak.sqlite3";
		public static string NazwaPlikuDemo => "profak-demo.probak";
		public static string NazwaPlikuStartowego => "profak-start.probak";
		public static string PrywatnyKatalog => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		public static string PublicznyKatalog => Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
		public static string LokalnyKatalog => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		public static string PrywatnaSciezka => Path.Combine(PrywatnyKatalog, NazwaKataloguProgramu, NazwaPlikuBazy);
		public static string PublicznaSciezka => Path.Combine(PublicznyKatalog, NazwaKataloguProgramu, NazwaPlikuBazy);
		public static string LokalnaSciezka => Path.Combine(LokalnyKatalog, NazwaPlikuBazy);
		public static string BazaDemo = Path.Combine(LokalnyKatalog, NazwaPlikuDemo);
		public static string BazaStartowa = Path.Combine(LokalnyKatalog, NazwaPlikuStartowego);

		public static string Sciezka { get; set; }

		public DbSet<Faktura> Faktury => Set<Faktura>();
		public DbSet<JednostkaMiary> JednostkiMiar => Set<JednostkaMiary>();
		public DbSet<Kontrahent> Kontrahenci => Set<Kontrahent>();
		public DbSet<PozycjaFaktury> PozycjeFaktur => Set<PozycjaFaktury>();
		public DbSet<StawkaVat> StawkiVat => Set<StawkaVat>();
		public DbSet<Towar> Towary => Set<Towar>();
		public DbSet<Waluta> Waluty => Set<Waluta>();
		public DbSet<SposobPlatnosci> SposobyPlatnosci => Set<SposobPlatnosci>();

		public Baza()
		{
		}

		public static bool Przygotuj()
		{
			DB.Baza.UstalSciezkeBazy();
			if (String.IsNullOrEmpty(Sciezka)) return false;
			using var baza = new DB.Baza();
			baza.Database.Migrate();
			return true;
		}

		private static void UstalSciezkeBazy()
		{
			if (File.Exists(LokalnaSciezka)) Sciezka = LokalnaSciezka;
			else if (File.Exists(PrywatnaSciezka)) Sciezka = PrywatnaSciezka;
			else if (File.Exists(PublicznaSciezka)) Sciezka = PublicznaSciezka;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlite($"Data Source={Sciezka}");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			DB.Model.ProFakModelBuilder.Configure(modelBuilder);
		}
	}
}
