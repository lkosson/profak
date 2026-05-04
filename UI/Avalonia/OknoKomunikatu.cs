#if AVALONIA
using Avalonia;
using Avalonia.Controls;

namespace ProFak.UI;

class OknoKomunikatu
{
	public static void Informacja(string komunikat)
	{
		Pokaz("🛈", komunikat, ["OK"]);
	}

	public static void Ostrzezenie(string komunikat)
	{
		Pokaz("⚠️", komunikat, ["OK"]);
		MessageBox.Show(komunikat, "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
	}

	public static bool PytanieTakNie(string komunikat, bool domyslnie = true)
	{
		var wynik = Pokaz("❔", komunikat, ["Tak", "Nie"], domyslny: domyslnie ? 0 : 1);
		return wynik == 0;
	}

	public static bool? PytanieTakNieAnuluj(string komunikat, bool? domyslnie = true)
	{
		var wynik = Pokaz("❔", komunikat, ["Tak", "Nie", "Anuluj"], domyslny: domyslnie is null ? 2 : domyslnie is true ? 0 : 1);
		if (wynik is null) return null;
		return wynik == 0;
	}

	private static int? Pokaz(string ikona, string komunikat, string[] przyciski, int domyslny = 0)
	{
		int? wynik = null;
		var window = new Window { Title = "ProFak" };
		var textBlockIkona = new TText { Text = ikona, FontSize = 30 };
		var textBlockKomunikat = new TText { Text = komunikat };
		var panelKomunikat = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 8 };
		var panelPrzyciski = new StackPanel { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 8, HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center };

		panelKomunikat.Children.Add(textBlockIkona);
		panelKomunikat.Children.Add(textBlockKomunikat);

		for (var i = 0; i < przyciski.Length; i++)
		{
			var _i = i;
			var tekst = przyciski[i];
			var przycisk = Kontrolki.Button(tekst, delegate { wynik = _i; window.Close(); });
			przycisk.IsCancel = tekst == "Nie" || tekst == "Anuluj" || tekst == "OK";
			panelPrzyciski.Children.Add(przycisk);
			if (i == domyslny)
			{
				przycisk.AttachedToVisualTree += delegate { przycisk.Focus(); };
				przycisk.IsDefault = true;
			}
		}

		var uklad = new StackPanel { Spacing = 8, Margin = new Thickness(8) };
		uklad.Children.Add(panelKomunikat);
		uklad.Children.Add(panelPrzyciski);

		window.Content = uklad;
		window.SizeToContent = SizeToContent.WidthAndHeight;

		AvaloniaUI.Wyswietl(window);

		return wynik;
	}
}
#endif