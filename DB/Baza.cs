using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		public static string NazwaOdnosnikaDoBazy => "baza.txt";
		public static string NazwaKataloguKopiiZapasowych => "Kopie zapasowe";
		public static string PrywatnyKatalog => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		public static string PublicznyKatalog => Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
		public static string LokalnyKatalog => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
		public static string PrywatnaSciezka => Path.Combine(PrywatnyKatalog, NazwaKataloguProgramu, NazwaPlikuBazy);
		public static string PublicznaSciezka => Path.Combine(PublicznyKatalog, NazwaKataloguProgramu, NazwaPlikuBazy);
		public static string LokalnaSciezka => Path.Combine(LokalnyKatalog, NazwaPlikuBazy);
		public static string OdnosnikDoBazy => Path.Combine(LokalnyKatalog, NazwaOdnosnikaDoBazy);
		public static string AktywnyKatalog => Path.GetDirectoryName(Sciezka);
		public static string KatalogKopiiZapasowych => Path.Combine(AktywnyKatalog, NazwaKataloguKopiiZapasowych);

		public static string Sciezka { get; set; }

		private static SqliteConnection bazaTymczasowa;

		public IQueryable<DeklaracjaVat> DeklaracjeVat => Set<DeklaracjaVat>();
		public IQueryable<Faktura> Faktury => Set<Faktura>();
		public IQueryable<JednostkaMiary> JednostkiMiar => Set<JednostkaMiary>();
		public IQueryable<Kontrahent> Kontrahenci => Set<Kontrahent>();
		public IQueryable<Numerator> Numeratory => Set<Numerator>();
		public IQueryable<Plik> Pliki => Set<Plik>();
		public IQueryable<PozycjaFaktury> PozycjeFaktur => Set<PozycjaFaktury>();
		public IQueryable<SkladkaZus> SkladkiZus => Set<SkladkaZus>();
		public IQueryable<SposobPlatnosci> SposobyPlatnosci => Set<SposobPlatnosci>();
		public IQueryable<StanNumeratora> StanyNumeratorow => Set<StanNumeratora>();
		public IQueryable<StawkaVat> StawkiVat => Set<StawkaVat>();
		public IQueryable<Towar> Towary => Set<Towar>();
		public IQueryable<UrzadSkarbowy> UrzedySkarbowe => Set<UrzadSkarbowy>();
		public IQueryable<Waluta> Waluty => Set<Waluta>();
		public IQueryable<Wplata> Wplaty => Set<Wplata>();
		public IQueryable<Zawartosc> Zawartosci => Set<Zawartosc>();
		public IQueryable<ZaliczkaPit> ZaliczkiPit => Set<ZaliczkaPit>();

		public Baza()
		{
			// IdentityResolution potrzebne, żeby dało się jednocześnie skasować dwie faktury z dołączoną taką samą walutą;
			// bez tego RemoveRange próbuje dodać dwie takie same waluty do konktekstu i wywala się na duplikacji.
			ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
		}

		public static bool Przygotuj()
		{
			DB.Baza.UstalSciezkeBazy();
			if (String.IsNullOrEmpty(Sciezka)) return false;
			using var baza = new DB.Baza();
			baza.Database.Migrate();
			DaneStartowe.Zaladuj(baza);
			return true;
		}

		private static void UstalSciezkeBazy()
		{
			if (File.Exists(OdnosnikDoBazy)) Sciezka = File.ReadAllLines(OdnosnikDoBazy)[0];
			else if (File.Exists(LokalnaSciezka)) Sciezka = LokalnaSciezka;
			else if (File.Exists(PrywatnaSciezka)) Sciezka = PrywatnaSciezka;
			else if (File.Exists(PublicznaSciezka)) Sciezka = PublicznaSciezka;
		}

		public static void ZapiszOdnosnikDoBazy()
		{
			try
			{
				if (Sciezka == PublicznaSciezka || Sciezka == PrywatnaSciezka || Sciezka == Baza.LokalnaSciezka)
				{
					if (File.Exists(OdnosnikDoBazy)) File.Delete(OdnosnikDoBazy);
				}
				else
				{
					File.WriteAllText(OdnosnikDoBazy, Sciezka);
				}
			}
			catch
			{
			}
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			string polaczenie;
			if (Sciezka == null)
			{
				polaczenie = "Data Source=ProFak;Mode=Memory;Cache=Shared";
				if (bazaTymczasowa == null)
				{
					bazaTymczasowa = new SqliteConnection(polaczenie);
					bazaTymczasowa.Open();
				}
			}
			else
			{
				polaczenie = $"Data Source={Sciezka}";
			}
			optionsBuilder.UseSqlite(polaczenie);
			//if (Debugger.IsAttached) optionsBuilder.LogTo(message => Debug.WriteLine(message), new[] { RelationalEventId.CommandExecuting }).EnableSensitiveDataLogging();
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
			ZapiszZmiany();
		}

		public void Zapisz(object rekord)
		{
			Entry(rekord).State = EntityState.Modified;
			ZapiszZmiany();
		}

		public void Usun<TRekord>(TRekord rekord)
			where TRekord : Rekord<TRekord>
			=> Usun(new[] { rekord });

		public void Usun<TRekord>(IEnumerable<TRekord> rekordy)
			where TRekord : Rekord<TRekord>
		{
			Set<TRekord>().RemoveRange(rekordy);
			ZapiszZmiany();
		}

		private void ZapiszZmiany()
		{
			try
			{
				SaveChanges();
			}
			finally
			{
				ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);
			}
		}

		public TRekord Znajdz<TRekord>(Ref<TRekord> rekordRef)
			where TRekord : Rekord<TRekord>
			=> Set<TRekord>().FirstOrDefault(r => r.Id == rekordRef.Id);

		public IEnumerable<Dictionary<string, object>> Zapytanie(FormattableString zapytanie)
		{
			using var polaczenie = Database.GetDbConnection();
			polaczenie.Open();
			using var polecenie = polaczenie.CreateCommand();
			var nazwyParametrow = new List<string>();
			for (int i = 0; i < zapytanie.ArgumentCount; i++)
			{
				var nazwaParametru = "@P" + i;
				nazwyParametrow.Add(nazwaParametru);
				var wartoscParametru = zapytanie.GetArgument(i);
				if (wartoscParametru == null) wartoscParametru = DBNull.Value;
				var parametr = polecenie.CreateParameter();
				parametr.ParameterName = nazwaParametru;
				parametr.Value = wartoscParametru;
				polecenie.Parameters.Add(parametr);
			}
			var sql = String.Format(zapytanie.Format, nazwyParametrow);
			polecenie.CommandText = sql;
			using var reader = polecenie.ExecuteReader();
			var wynik = new List<Dictionary<string, object>>();
			while (reader.Read())
			{
				var wiersz = new Dictionary<string, object>();
				for (int i = 0; i < reader.FieldCount; i++)
				{
					wiersz[reader.GetName(i)] = reader.GetValue(i);
				}
				wynik.Add(wiersz);
			}
			return wynik;
		}
	}
}
