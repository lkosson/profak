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
			if (faktura.CzyZakup && faktura.ProcentKosztow == 0) continue;
			var dto = new PKPiRDTO();
			dto.Tytul = tytul;
			dto.Podmiot = podmiot.PelnaNazwa + "\r\n" + podmiot.AdresRejestrowy;

			dto.LP = lp++;
			dto.NumerKSeF = faktura.NumerKSeF;
			dto.Numer = faktura.Numer;
			dto.Data = faktura.DataSprzedazy;
			var jestTowar = faktura.Pozycje.Any(pozycja => pozycja.Towar != null && pozycja.Towar.Rodzaj == RodzajTowaru.Towar);

			if (faktura.CzySprzedaz)
			{
				dto.NIP = faktura.NIPNabywcy;
				dto.Kontrahent = faktura.NazwaNabywcy;
				dto.Adres = faktura.DaneNabywcy.JakoDwieLinie().Polaczone();
				dto.Opis = String.IsNullOrEmpty(faktura.OpisZdarzenia) ? jestTowar ? "Sprzedaż towarów" : "Sprzedaż usług" : faktura.OpisZdarzenia;
				dto.PrzychodWartosc = faktura.RazemNetto;
				dto.PrzychodRazem = faktura.RazemNetto;
			}
			else
			{
				dto.NIP = faktura.NIPSprzedawcy;
				dto.Kontrahent = faktura.NazwaSprzedawcy;
				dto.Adres = faktura.DaneSprzedawcy.JakoDwieLinie().Polaczone();
				dto.Opis = String.IsNullOrEmpty(faktura.OpisZdarzenia) ? jestTowar ? "Zakup towarów" : "Zakup usług" : faktura.OpisZdarzenia;
				if (jestTowar) dto.KosztyZakup = faktura.Koszty;
				else dto.KosztyPozostale = faktura.Koszty;
				dto.KosztyRazem = faktura.Koszty;
			}
			dto.KosztyBR = "0,00";

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
