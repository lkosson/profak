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
