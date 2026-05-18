#if REPORTVIEWER
using Microsoft.Reporting.WinForms;

namespace ProFak.Wydruki;

public partial class PKPiR : Wydruk
{
	public override void Przygotuj(LocalReport report)
	{
		using var rdlc = WczytajSzablon("PKPiR");
		report.LoadReportDefinition(rdlc);
		report.DataSources.Add(new ReportDataSource("DS", dane));
	}
}
#endif