#if WINFORMS
namespace ProFak.UI;

class Interfejs
{
	public static void Przygotuj()
	{
		Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
	}
}
#endif
