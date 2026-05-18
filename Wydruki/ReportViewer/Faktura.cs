#if REPORTVIEWER
using Microsoft.Reporting.WinForms;

namespace ProFak.Wydruki;

public partial class Faktura
{
	public override void Przygotuj(LocalReport report)
	{
		using var rdlc = WczytajSzablon("Faktura");
		report.DisplayName = String.Join(", ", dane.Select(e => e.Numer).Distinct().Order());
		report.LoadReportDefinition(rdlc);
		report.LoadSubreportDefinition("PozycjeVatRabat", WczytajSzablon("FakturaPozycjeVatRabat"));
		report.LoadSubreportDefinition("PozycjeVat", WczytajSzablon("FakturaPozycjeVat"));
		report.LoadSubreportDefinition("PozycjeRabat", WczytajSzablon("FakturaPozycjeRabat"));
		report.LoadSubreportDefinition("Pozycje", WczytajSzablon("FakturaPozycje"));
		report.SubreportProcessing += SubreportProcessing;
		report.DataSources.Add(new ReportDataSource("DSFaktury", dane));

		void SubreportProcessing(object? sender, SubreportProcessingEventArgs e)
		{
			e.DataSources.Add(report.DataSources[0]);
		}
	}
}
#endif