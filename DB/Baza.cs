#define nLOG_SQL
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
		public static string LokalnyKatalog => Path.GetDirectoryName(Environment.ProcessPath)!;
		public static string PrywatnaSciezka => Path.Combine(PrywatnyKatalog, NazwaKataloguProgramu, NazwaPlikuBazy);
		public static string PublicznaSciezka => Path.Combine(PublicznyKatalog, NazwaKataloguProgramu, NazwaPlikuBazy);
		public static string LokalnaSciezka => Path.Combine(LokalnyKatalog, NazwaPlikuBazy);
		public static string OdnosnikDoBazy => Path.Combine(LokalnyKatalog, NazwaOdnosnikaDoBazy);
		public static string AktywnyKatalog => Path.GetDirectoryName(Sciezka)!;
		public static string KatalogKopiiZapasowych => Path.Combine(AktywnyKatalog, NazwaKataloguKopiiZapasowych);

		public static string? Sciezka { get; set; }

		private static SqliteConnection? bazaTymczasowa;

		public IQueryable<DeklaracjaVat> DeklaracjeVat => Set<DeklaracjaVat>();
		public IQueryable<DodatkowyPodmiot> DodatkowePodmioty => Set<DodatkowyPodmiot>();
		public IQueryable<Faktura> Faktury => Set<Faktura>();
		public IQueryable<JednostkaMiary> JednostkiMiar => Set<JednostkaMiary>();
		public IQueryable<KolumnaSpisu> KolumnySpisow => Set<KolumnaSpisu>();
		public IQueryable<Konfiguracja> Konfiguracja => Set<Konfiguracja>();
		public IQueryable<Kontrahent> Kontrahenci => Set<Kontrahent>();
		public IQueryable<Numerator> Numeratory => Set<Numerator>();
		public IQueryable<Plik> Pliki => Set<Plik>();
		public IQueryable<PozycjaFaktury> PozycjeFaktur => Set<PozycjaFaktury>();
		public IQueryable<SkladkaZus> SkladkiZus => Set<SkladkaZus>();
		public IQueryable<SposobPlatnosci> SposobyPlatnosci => Set<SposobPlatnosci>();
		public IQueryable<StanMenu> StanyMenu => Set<StanMenu>();
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

		public static void Przygotuj()
		{
			WykonajAutomatycznaKopieBazy();
#if SQLSERVER
			using var baza = new DB.BazaSqlServer();
#else
			using var baza = new DB.Baza();
#endif
			baza.Database.Migrate();
			DaneStartowe.Zaladuj(baza);
		}

		private static void WykonajAutomatycznaKopieBazy()
		{
			try
			{
				if (String.IsNullOrEmpty(Sciezka) || !File.Exists(Sciezka)) return;
				var sciezkaKopiiDziennej = Sciezka + "~";
				var sciezkaKopiiMiesiecznej = Sciezka + "~~";
				var dzis = DateTime.Now.Date;
				var miesiac = dzis.AddDays(1 - dzis.Day);
				var dataBazy = File.GetLastWriteTime(Sciezka);
				var dataKopiiDziennej = File.Exists(sciezkaKopiiDziennej) ? File.GetLastWriteTime(sciezkaKopiiDziennej) : DateTime.MinValue;
				var dataKopiiMiesiecznej = File.Exists(sciezkaKopiiMiesiecznej) ? File.GetLastWriteTime(sciezkaKopiiMiesiecznej) : DateTime.MinValue;

				if (dataBazy <= dataKopiiDziennej) return;
				if (dzis == dataKopiiDziennej.Date) return;

				if (dataKopiiMiesiecznej < miesiac && dataKopiiDziennej > miesiac)
				{
					File.Delete(sciezkaKopiiMiesiecznej);
					File.Move(sciezkaKopiiDziennej, sciezkaKopiiMiesiecznej);
				}

				WykonajKopie(sciezkaKopiiDziennej);
			}
			catch
			{
			}
		}

		public static void WykonajKopie(string plikDocelowy)
		{
			string? plikNieaktualny = null;
			if (File.Exists(plikDocelowy))
			{
				plikNieaktualny = plikDocelowy + "-del";
				File.Move(plikDocelowy, plikNieaktualny);
			}
			using (var zrodlo = new SqliteConnection(PrzygotujParametryPolaczenia()))
			{
				zrodlo.Open();
				using var cel = new SqliteConnection($"Data Source={plikDocelowy};Pooling=false");
				zrodlo.BackupDatabase(cel);
			}
			if (plikNieaktualny != null) File.Delete(plikNieaktualny);
		}

		public static void UstalSciezkeBazy()
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

		private static string PrzygotujParametryPolaczenia()
		{
#if SQLSERVER
			return Sciezka;
#else
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
			return polaczenie;
#endif
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
#if SQLSERVER
			optionsBuilder.UseSqlServer(PrzygotujParametryPolaczenia());
#else
			optionsBuilder.UseSqlite(PrzygotujParametryPolaczenia(), opts => opts.CommandTimeout(5));
#endif
#if LOG_SQL
			optionsBuilder.EnableSensitiveDataLogging();
			optionsBuilder.LogTo((evt, level) => evt == RelationalEventId.CommandExecuting || evt == RelationalEventId.CommandExecuted, LogCommand);
#endif
		}
#if LOG_SQL
		private void LogCommand(EventData eventData)
		{
			if (eventData.EventId == RelationalEventId.CommandExecuting && eventData is CommandEventData ced)
			{
				Debug.WriteLine($"Uruchamianie polecenia\n{ced.Command.CommandText}");
				if (ced.Command.Parameters.Count > 0) Debug.WriteLine($"Parametry:\n{String.Join("\n", ced.Command.Parameters.Cast<System.Data.Common.DbParameter>().Select(p => p.ParameterName + "='" + p.Value + "'"))}");
			}
			else if (eventData.EventId == RelationalEventId.CommandExecuted && eventData is CommandExecutedEventData ceed)
			{
				Debug.WriteLine($"Polecenie wykonane w {ceed.Duration.TotalMilliseconds} ms");
			}
		}
#endif
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

		public void Usun(object rekord)
		{
			Entry(rekord).State = EntityState.Deleted;
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

		public void Zablokuj<TRekord>(Ref<TRekord> rekordRef)
			where TRekord : Rekord<TRekord>
		{
#if SQLSERVER
			Database.ExecuteSqlRaw($"SET LOCK_TIMEOUT 1000; SELECT Id FROM {typeof(TRekord).Name} WITH (ROWLOCK, UPDLOCK) WHERE Id={rekordRef.Id}");
#endif
		}

		public void Zablokuj<TRekord>()
			where TRekord : Rekord<TRekord>
		{
#if SQLSERVER
			Database.ExecuteSqlRaw($"SET LOCK_TIMEOUT 1000; SELECT Id FROM {typeof(TRekord).Name} WITH (TABLOCK, UPDLOCK)");
#endif
		}

		public bool CzyZablokowana()
		{
#if !SQLSERVER
			if (Database.CurrentTransaction != null) return false;
			if (Sciezka == null) return false;
			try
			{
				using var connection = new SqliteConnection(PrzygotujParametryPolaczenia());
				connection.Open();
				using (var command = connection.CreateCommand())
				{
					command.CommandText = "BEGIN IMMEDIATE TRANSACTION";
					command.CommandTimeout = -1;
					command.ExecuteNonQuery();
				}
				using (var command = connection.CreateCommand())
				{
					command.CommandText = "ROLLBACK";
					command.ExecuteNonQuery();
				}
			}
			catch (SqliteException se) when (se.SqliteErrorCode == 5)
			{
				return true;
			}
#endif
			return false;
		}

		public TRekord Znajdz<TRekord>(Ref<TRekord> rekordRef)
			where TRekord : Rekord<TRekord>
			=> Set<TRekord>().FirstOrDefault(r => r.Id == rekordRef.Id) ?? throw new ApplicationException($"Nie znaleziono rekordu {rekordRef}.");

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
			var sql = String.Format(zapytanie.Format, nazwyParametrow.ToArray());
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

		public static void ZamknijPolaczenia()
		{
			SqliteConnection.ClearAllPools();
		}
	}
#if SQLSERVER
	class BazaSqlServer : Baza
	{
	}
#endif
}
