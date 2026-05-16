#if QUESTPDF
using QuestPDF;
using QuestPDF.Infrastructure;

namespace ProFak.Wydruki;

public abstract class Wydruk
{
	public abstract IDocument Przygotuj();

	public static void WstepneLadowanie()
	{
		Settings.License = LicenseType.Community;
	}
}
#endif