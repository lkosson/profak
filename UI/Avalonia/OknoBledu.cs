#if AVALONIA
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using FontFamily = Avalonia.Media.FontFamily;
using Avalonia.Interactivity;
using Button = Avalonia.Controls.Button;
using Control = Avalonia.Controls.Control;
using KeyEventArgs = Avalonia.Input.KeyEventArgs;
using TextBox = Avalonia.Controls.TextBox;
using System.Diagnostics;

namespace ProFak.UI;

class OknoBleduAV : Window
{
	public OknoBleduAV(Exception exc)
	{
		var uklad = new StackPanel();
		var naglowek = new TextBlock { TextWrapping = TextWrapping.Wrap, Text = "W trakcie działania aplikacji wystąpił nieoczekiwany błąd. Spróbuj ponownie uruchomić program. Jeśli problem będzie się powtarzał, otwórz poniższy odnośnik i opisz w jakich okolicznościach występuje.\r\n\r\nPoniżej znajdują się techniczne informacje mogące pomóc w ustaleniu przyczyny problemu." };
		var wyjatek = new TextBox { IsReadOnly = true, MinLines = 20, AcceptsReturn = true, Text = exc.ToString() };
		var buttonOk = new Button { Content = "OK" };
		buttonOk.Click += delegate { Close(); };
		var linkURL = new HyperlinkButton { Content = "https://github.com/lkosson/profak/issues" };
		linkURL.Click += delegate { Link(); };
		var stopka = new WrapPanel();
		stopka.Children.Add(buttonOk);
		stopka.Children.Add(linkURL);
		uklad.Children.Add(naglowek);
		uklad.Children.Add(wyjatek);
		uklad.Children.Add(stopka);
		Content = uklad;
		SizeToContent = SizeToContent.Height;
		ShowInTaskbar = false;
		Width = 600;
		Title = "ProFak - Błąd";
	}

	private void Link()
	{
		Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "https://github.com/lkosson/profak/issues" });
	}
}
#endif