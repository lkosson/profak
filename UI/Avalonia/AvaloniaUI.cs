#if AVALONIA
global using TDialog = ProFak.UI.DialogAV;
global using TDwieKolumny = ProFak.UI.DwieKolumnyAV;
global using TGrupa = ProFak.UI.GrupaAV;
global using TKontrolki = ProFak.UI.KontrolkiAV;
global using TPionowo = ProFak.UI.PionowoAV;
global using TPoziomo = ProFak.UI.PoziomoAV;
global using TSiatka = ProFak.UI.SiatkaAV;
global using TZakladki = ProFak.UI.ZakladkiAV;
#else
#endif

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Themes.Simple;
using Application = Avalonia.Application;

namespace ProFak.UI;

class AvaloniaUI
{
	private static Application application = default!;

	public static void Przygotuj()
	{
		var appBuilder = AppBuilder.Configure<Application>().UsePlatformDetect().SetupWithoutStarting();
		application = appBuilder.Instance!;
		application.Styles.Add(new SimpleTheme());
		application.Styles.Add(new StyleInclude(new Uri("avares://ProFak")) { Source = new Uri("avares://Avalonia.Controls.DataGrid/Themes/Simple.xaml") });
	}

	public static void Wyswietl(Window okno)
	{
		application.Run(okno);
	}
}
