#if REPORTVIEWER
using Microsoft.Reporting.WinForms;

namespace ProFak.Wydruki;

public partial class EwidencjaPrzychodow
{
	public override void Przygotuj(LocalReport report)
	{
		using var rdlc = WczytajSzablon("EwidencjaPrzychodow");
		report.LoadReportDefinition(rdlc);
		report.DataSources.Add(new ReportDataSource("DS", dane));
	}
}
#endif
