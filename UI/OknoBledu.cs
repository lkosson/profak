using System.Diagnostics;

namespace ProFak.UI;

class OknoBledu : Dialog
{
	private OknoBledu(Exception exc, Kontekst kontekst)
		: base("ProFak - Błąd", kontekst)
	{
		var naglowek = Kontrolki.Text("W trakcie działania aplikacji wystąpił nieoczekiwany błąd. Spróbuj ponownie uruchomić program. Jeśli problem będzie się powtarzał, otwórz poniższy odnośnik i opisz w jakich okolicznościach występuje.\r\n\r\nPoniżej znajdują się techniczne informacje mogące pomóc w ustaleniu przyczyny problemu.");
		var textAreaWyjatek = Kontrolki.TextArea(linie: 20);
		var buttonOK = Kontrolki.Button("OK", akcja: Close);
		var linkURL = Kontrolki.Link("https://github.com/lkosson/profak/issues", akcja: Link);
		textAreaWyjatek.ReadOnly = true;
		textAreaWyjatek.Text = exc.ToString();
		var uklad = new Siatka([0, -1], [0, -1, 0]);
		uklad.DodajWiersz([(naglowek, 2)]);
		uklad.DodajWiersz([(textAreaWyjatek, 2)]);
		uklad.DodajWiersz([buttonOK, linkURL]);
		UstawZawartosc(uklad);
	}

	private void Link()
	{
		Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "https://github.com/lkosson/profak/issues" });
	}

	public static void Pokaz(Exception exc)
	{
		if (exc.GetType() == typeof(ApplicationException))
		{
			OknoKomunikatu.Ostrzezenie(exc.Message);
		}
		else if (exc is OperationCanceledException)
		{
			OknoKomunikatu.Informacja("Operacja została przerwana.");
		}
#if SQLSERVER
		else if (exc is Microsoft.Data.SqlClient.SqlException se && se.Number == 1222)
		{
			OknoKomunikatu.Ostrzezenie("Rekord jest modyfikowany na innym stanowisku.");
		}
#else
		else if (exc is Microsoft.Data.Sqlite.SqliteException se && se.SqliteErrorCode == 5)
		{
			OknoKomunikatu.Ostrzezenie("Baza danych jest używana na innym stanowisku.");
		}
		else if (exc is Microsoft.EntityFrameworkCore.DbUpdateException && exc.InnerException is Microsoft.Data.Sqlite.SqliteException se2 && se2.SqliteErrorCode == 19)
		{
			MessageBox.Show("Z tą pozycją są powiązane inne dane w bazie, blokujące możliwość usunięcia lub edycji.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}
#endif
		else
		{
			using var kontekst = new Kontekst();
			using var okno = new OknoBledu(exc, kontekst);
			okno.Pokaz();
		}
	}
}
