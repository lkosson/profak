#if AVALONIA
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Themes.Simple;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

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

		var stylCheckbox = new Style(x => x.OfType<TCheckBox>().Class(":checked").Template().OfType<Avalonia.Controls.Shapes.Path>());
		stylCheckbox.Setters.Add(new Setter(Shape.FillProperty, Brushes.Black));
		application.Styles.Add(stylCheckbox);

		var stylDataGridWybrany = new Style(x => x.OfType<DataGridRow>().Template().OfType<Rectangle>().Name("BackgroundRectangle"));
		stylDataGridWybrany.Setters.Add(new Setter(Shape.FillProperty, new SolidColorBrush(TColor.FromRgb(50, 160, 245))));
		application.Styles.Add(stylDataGridWybrany);

		var stylDataGridTlo = new Style(x => x.OfType<DataGridRow>());
		stylDataGridTlo.Setters.Add(new Setter(DataGridRow.BackgroundProperty, Brushes.White));
		application.Styles.Add(stylDataGridTlo);
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

	public static T Uruchom<T>(Task<T> zadanie)
	{
		var cts = new CancellationTokenSource();
		zadanie.ContinueWith(delegate { cts.Cancel(); });
		application.Run(cts.Token);
		return zadanie.GetAwaiter().GetResult();
	}
}
#endif
