#if QUESTPDF
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ProFak.Wydruki;

public abstract class Wydruk
{
	public abstract IDocument Przygotuj();

	public static void WstepneLadowanie()
	{
		Settings.License = LicenseType.Community;
	}

	public byte[] ZapiszJako()
	{
		var dokument = Przygotuj();
		return dokument.GeneratePdf();
	}

	public void Uruchom()
	{
		var dokument = Przygotuj();
		dokument.GeneratePdfAndShow();
	}
}
#endif