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

		public IQueryable<Faktura> Faktury => Set<Faktura>();
		public IQueryable<JednostkaMiary> JednostkiMiar => Set<JednostkaMiary>();
		public IQueryable<Kontrahent> Kontrahenci => Set<Kontrahent>();
		public IQueryable<Numerator> Numeratory => Set<Numerator>();
		public IQueryable<Plik> Pliki => Set<Plik>();
		public IQueryable<PozycjaFaktury> PozycjeFaktur => Set<PozycjaFaktury>();
		public IQueryable<StawkaVat> StawkiVat => Set<StawkaVat>();
		public IQueryable<Towar> Towary => Set<Towar>();
		public IQueryable<Waluta> Waluty => Set<Waluta>();
		public IQueryable<SposobPlatnosci> SposobyPlatnosci => Set<SposobPlatnosci>();
		public IQueryable<StanNumeratora> StanyNumeratorow => Set<StanNumeratora>();
		public IQueryable<Wplata> Wplaty => Set<Wplata>();
		public IQueryable<Zawartosc> Zawartosci => Set<Zawartosc>();

		public Baza()
		{
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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

		public void Zapisz<TRekord>(TRekord rekord)
			where TRekord : Rekord<TRekord>
			=> Zapisz(new[] { rekord });

		public void Zapisz<TRekord>(IEnumerable<TRekord> rekordy)
			where TRekord : Rekord<TRekord>
		{
			var set = Set<TRekord>();
			foreach (var rekord in rekordy)
			{
				if (rekord.Id <= 0)
				{
					rekord.Id = 0;
					set.Add(rekord);
				}
				else
				{
					Entry(rekord).State = EntityState.Modified;
				}
			}
			SaveChanges();
			foreach (var rekord in rekordy)
			{
				Entry(rekord).State = EntityState.Detached;
			}
		}

		public void Usun<TRekord>(TRekord rekord)
			where TRekord : Rekord<TRekord>
			=> Usun(new[] { rekord });

		public void Usun<TRekord>(IEnumerable<TRekord> rekordy)
			where TRekord : Rekord<TRekord>
		{
			Set<TRekord>().RemoveRange(rekordy);
			SaveChanges();
		}

		public TRekord Znajdz<TRekord>(Ref<TRekord> rekordRef)
			where TRekord : Rekord<TRekord>
			=> Set<TRekord>().FirstOrDefault(r => r.Id == rekordRef.Id);
	}
}
