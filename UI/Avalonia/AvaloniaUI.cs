#if AVALONIA
global using Dialog = ProFak.UI.DialogAV;
global using DialogEdycji = ProFak.UI.DialogEdycjiAV;
global using DwieKolumny = ProFak.UI.DwieKolumnyAV;
global using Grupa = ProFak.UI.GrupaAV;
global using Kontrolki = ProFak.UI.KontrolkiAV;
global using Pionowo = ProFak.UI.PionowoAV;
global using Poziomo = ProFak.UI.PoziomoAV;
global using Siatka = ProFak.UI.SiatkaAV;
global using Zakladki = ProFak.UI.ZakladkiAV;

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
#endif
