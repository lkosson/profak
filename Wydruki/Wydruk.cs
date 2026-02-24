using Microsoft.Reporting.WinForms;
using ProFak.DB;
using System.Reflection;

namespace ProFak.Wydruki;

public abstract class Wydruk
{
	public abstract void Przygotuj(LocalReport report);

	protected Stream WczytajSzablon(string nazwa)
	{
		var lokalnyPlik = Path.Combine(Baza.LokalnyKatalog, nazwa + ".rdlc");
		if (File.Exists(lokalnyPlik)) return File.OpenRead(lokalnyPlik);
		var asm = Assembly.GetCallingAssembly();
		return asm.GetManifestResourceStream("ProFak.Wydruki." + nazwa + ".rdlc") ?? throw new InvalidOperationException($"Nie znaleziono szablonu wydruku {nazwa}.");
	}

	public byte[] ZapiszJako(string format = "PDF")
	{
		using var localReport = new LocalReport();
		Przygotuj(localReport);
		var pdf = localReport.Render(format);
		return pdf;
	}
}
