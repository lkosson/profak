using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WinForms;
using ProFak.DB;

namespace ProFak.Wydruki;

public class PKPiR : Wydruk
{
	private readonly List<PKPiRDTO> dane;

	public PKPiR(Baza baza, IEnumerable<ZaliczkaPit> zaliczki)
	{
		dane = [];

		var zaliczkiIds = zaliczki.Select(e => e.Id).ToList();
		var faktury = baza.Faktury
			.Where(faktura => zaliczkiIds.Contains(faktura.ZaliczkaPitId!.Value))
			.Include(faktura => faktura.Pozycje).ThenInclude(pozycja => pozycja.Towar)
			.OrderBy(faktura => faktura.DataSprzedazy)
			.ToList();

		var tytul = "Podatkowa księga przychodów i rozchodów, ";
		var podmiot = baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
		if (zaliczki.Count() == 1) tytul += zaliczki.Single().Miesiac.ToString("MMMM yyyy");
		else tytul += zaliczki.Select(e => e.Miesiac).Min().ToString("MMMM yyyy") + " - " + zaliczki.Select(e => e.Miesiac).Max().ToString("MMMM yyyy");

		int lp = 1;
		foreach (var faktura in faktury)
		{
			var dto = new PKPiRDTO();
			dto.Tytul = tytul;
			dto.Podmiot = podmiot.PelnaNazwa + "\r\n" + podmiot.AdresRejestrowy;

			dto.LP = lp++;
			dto.Numer = faktura.Numer;
			dto.Data = faktura.DataSprzedazy;
			var jestTowar = faktura.Pozycje.Any(pozycja => pozycja.Towar != null && pozycja.Towar.Rodzaj == RodzajTowaru.Towar);

			if (faktura.CzySprzedaz)
			{
				dto.Kontrahent = faktura.NazwaNabywcy;
				dto.Adres = faktura.DaneNabywcy.JakoJednaLinia();
				dto.Opis = String.IsNullOrEmpty(faktura.OpisZdarzenia) ? jestTowar ? "Sprzedaż towarów" : "Sprzedaż usług" : faktura.OpisZdarzenia;
				dto.PrzychodWartosc = faktura.RazemNetto;
				dto.PrzychodRazem = faktura.RazemNetto;
			}
			else
			{
				dto.Kontrahent = faktura.NazwaSprzedawcy;
				dto.Adres = faktura.DaneSprzedawcy.JakoJednaLinia();
				dto.Opis = String.IsNullOrEmpty(faktura.OpisZdarzenia) ? jestTowar ? "Zakup towarów" : "Zakup usług" : faktura.OpisZdarzenia;
				if (jestTowar) dto.KosztyZakup = faktura.Koszty;
				else dto.KosztyPozostale = faktura.Koszty;
				dto.KosztyRazem = faktura.Koszty;
			}

			dane.Add(dto);
		}
	}

	public override void Przygotuj(LocalReport report)
	{
		using var rdlc = WczytajSzablon("PKPiR");
		report.LoadReportDefinition(rdlc);
		report.DataSources.Add(new ReportDataSource("DS", dane));
	}
}
