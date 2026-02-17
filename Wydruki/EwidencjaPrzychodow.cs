using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WinForms;
using ProFak.DB;

namespace ProFak.Wydruki;

class EwidencjaPrzychodow : Wydruk
{
	private readonly List<EwidencjaPrzychodowDTO> dane;

	public EwidencjaPrzychodow(Baza baza, IEnumerable<ZaliczkaPit> zaliczki)
	{
		dane = [];

		var zaliczkiIds = zaliczki.Select(e => e.Id).ToList();
		var faktury = baza.Faktury
			.Include(faktura => faktura.Pozycje)
			.Where(faktura => zaliczkiIds.Contains(faktura.ZaliczkaPitId!.Value))
			.OrderBy(faktura => faktura.DataSprzedazy)
			.ToList();

		var tytul = "Ewidencja przychodów, ";
		var podmiot = baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
		if (zaliczki.Count() == 1) tytul += zaliczki.Single().Miesiac.ToString("MMMM yyyy");
		else tytul += zaliczki.Select(e => e.Miesiac).Min().ToString("MMMM yyyy") + " - " + zaliczki.Select(e => e.Miesiac).Max().ToString("MMMM yyyy");

		int lp = 1;
		foreach (var faktura in faktury)
		{
			if (!faktura.CzySprzedaz) continue;
			var dto = new EwidencjaPrzychodowDTO();
			dto.Tytul = tytul;
			dto.Podmiot = podmiot.PelnaNazwa + "\r\n" + podmiot.AdresRejestrowy;
			dto.LP = lp++;
			dto.NumerDowodu = faktura.Numer;
			dto.DataWpisu = faktura.DataWystawienia;
			dto.DataPrzychodu = faktura.DataSprzedazy;

			foreach (var pozycja in faktura.Pozycje)
			{
				if (pozycja.StawkaRyczaltu == 17) dto.Przychod17 += pozycja.WartoscNetto;
				if (pozycja.StawkaRyczaltu == 15) dto.Przychod15 += pozycja.WartoscNetto;
				if (pozycja.StawkaRyczaltu == 14) dto.Przychod14 += pozycja.WartoscNetto;
				if (pozycja.StawkaRyczaltu == 12.5m) dto.Przychod125 += pozycja.WartoscNetto;
				if (pozycja.StawkaRyczaltu == 12) dto.Przychod12 += pozycja.WartoscNetto;
				if (pozycja.StawkaRyczaltu == 10) dto.Przychod10 += pozycja.WartoscNetto;
				if (pozycja.StawkaRyczaltu == 8.5m) dto.Przychod85 += pozycja.WartoscNetto;
				if (pozycja.StawkaRyczaltu == 5.5m) dto.Przychod55 += pozycja.WartoscNetto;
				if (pozycja.StawkaRyczaltu == 3) dto.Przychod3 += pozycja.WartoscNetto;
				dto.PrzychodRazem += pozycja.WartoscNetto;
			}

			dane.Add(dto);
		}
	}

	public override void Przygotuj(LocalReport report)
	{
		using var rdlc = WczytajSzablon("EwidencjaPrzychodow");
		report.LoadReportDefinition(rdlc);
		report.DataSources.Add(new ReportDataSource("DS", dane));
	}
}
