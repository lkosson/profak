using ProFak.DB;
using ProFak.UI;
using System.Globalization;
using System.Text;

namespace ProFak;

public static class Program
{
	[STAThread]
	public static void Main(string[] args)
	{
		try
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			Wyglad.ZaladujDomyslny();
			CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("pl-PL");
			Baza.UstalSciezkeBazy();
			Interfejs.Przygotuj();
#if !SQLSERVER
			if (!PierwszyStartBaza.Uruchom()) return;
#endif
			Baza.Przygotuj();
			Wyglad.WczytajZBazy();

			if (args.Length > 0)
			{
				if (args[0] == "xsd") Wydruki.GeneratorXSD.Utworz();
				if (args[0] == "sql")
				{
					using var kontekst = new Kontekst();
					Dialog.Pokaz("ProFak", new EkranSQL() { Kontekst = kontekst }, kontekst);
				}
				if (args[0] == "db")
				{
					using var kontekst = new Kontekst();
					Dialog.Pokaz("ProFak", new BazyDanych() { Kontekst = kontekst }, kontekst);
				}
				return;
			}

			using (var kontekst = new UI.Kontekst())
			{
				var podmiot = kontekst.Baza.Kontrahenci.FirstOrDefault(kontrahent => kontrahent.CzyPodmiot);
				if (podmiot == null)
				{
					OknoKomunikatu.Informacja("Przed rozpoczêciem korzystania z programu nale¿y uzupe³niæ dane firmy.");
					var _ = Enumerable.Empty<DB.Kontrahent>();
					new UI.MojaFirmaAkcja().Uruchom(kontekst, ref _);
				}
			}

			if (Wyglad.WstepneLadowanieReportingServices) OknoWydruku.ZaladujWstepnieReportViewer();
			GlowneOkno.Pokaz();
		}
		catch (Exception exc)
		{
			OknoBledu.Pokaz(exc);
		}
	}
}
