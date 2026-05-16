#if QUESTPDF
using QuestPDF.Infrastructure;

namespace ProFak.Wydruki;

public abstract class Wydruk
{
	public abstract IDocument Przygotuj();
}
#endif