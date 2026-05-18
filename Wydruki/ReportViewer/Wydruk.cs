#if REPORTVIEWER
using Microsoft.Reporting.WinForms;
using ProFak.DB;
using ProFak.UI;
using System.Reflection;

namespace ProFak.Wydruki;

public abstract class Wydruk
{
	public abstract void Przygotuj(LocalReport report);

	protected Stream WczytajSzablon(string nazwa)
	{
		var lokalnyPlik = Path.Combine(Baza.LokalnyKatalog, nazwa + ".rdlc");
		if (File.Exists(lokalnyPlik)) return File.OpenRead(lokalnyPlik);
		var asm = Assembly.GetExecutingAssembly();
		return asm.GetManifestResourceStream("ProFak.Wydruki." + nazwa + ".rdlc") ?? throw new InvalidOperationException($"Nie znaleziono szablonu wydruku {nazwa}.");
	}

	public void Uruchom()
	{
		using var okno = new OknoWydruku(this);
		okno.ShowDialog();
	}

	public byte[] ZapiszJako(string format = "PDF")
	{
		using var localReport = new LocalReport();
		Przygotuj(localReport);
		var pdf = localReport.Render(format);
		return pdf;
	}

	public static void WstepneLadowanie()
	{
		if (!Wyglad.WstepneLadowanieReportingServices) return;
		_ = Task.Run(async delegate
		{
			try
			{
				await Task.Delay(TimeSpan.FromSeconds(3));
				using var kontekst = new Kontekst();
				var faktura = kontekst.Baza.Faktury.Where(e => e.Rodzaj == DB.RodzajFaktury.Sprzedaż).OrderByDescending(e => e.Id).FirstOrDefault();
				if (faktura == null) return;
				var wydruk = new Wydruki.Faktura(kontekst.Baza, [faktura.Ref]);
				using var reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
				wydruk.Przygotuj(reportViewer.LocalReport);
				reportViewer.RefreshReport();
			}
			catch
			{
				// ignored
			}
		});
	}
}
#endif