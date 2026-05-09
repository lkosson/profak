#if AVALONIA
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Themes.Simple;
using Application = Avalonia.Application;
using Avalonia.Platform.Storage;

namespace ProFak.UI;

class Interfejs
{
	private static Application application = default!;
	private static Window? aktywneOkno;
	public static IStorageProvider StorageProvider => aktywneOkno?.StorageProvider ?? new Window().StorageProvider;

	public static void Przygotuj()
	{
		var appBuilder = AppBuilder.Configure<Application>().UsePlatformDetect().SetupWithoutStarting();
		application = appBuilder.Instance!;
		application.Styles.Add(new SimpleTheme());
		application.Styles.Add(new StyleInclude(new Uri("avares://ProFak")) { Source = new Uri("avares://Avalonia.Controls.DataGrid/Themes/Simple.xaml") });
	}

	public static void Wyswietl(Window okno)
	{
		var poprzednieOkno = aktywneOkno;
		aktywneOkno = okno;
		try
		{
			if (poprzednieOkno != null) okno.ShowDialog(poprzednieOkno);
			application.Run(okno);
		}
		finally
		{
			aktywneOkno = poprzednieOkno;
		}
	}
}
#endif
