#if WINFORMS
global using Dialog = ProFak.UI.DialogWF;
global using DialogEdycji = ProFak.UI.DialogEdycjiWF;
global using DwieKolumny = ProFak.UI.DwieKolumnyWF;
global using Grupa = ProFak.UI.GrupaWF;
global using Kontrolki = ProFak.UI.KontrolkiWF;
global using Pionowo = ProFak.UI.PionowoWF;
global using Poziomo = ProFak.UI.PoziomoWF;
global using Siatka = ProFak.UI.SiatkaWF;
global using Zakladki = ProFak.UI.ZakladkiWF;
global using TUI = ProFak.UI.WinFormsUI;

namespace ProFak.UI;

class WinFormsUI
{
	public static void Przygotuj()
	{
		Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
	}
}
#endif
