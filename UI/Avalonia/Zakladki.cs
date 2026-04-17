#if AVALONIA
using Avalonia.Controls;
using Avalonia.Interactivity;
using Control = Avalonia.Controls.Control;
using TabControl = Avalonia.Controls.TabControl;
using KeyEventArgs = Avalonia.Input.KeyEventArgs;
using KeyModifiers = Avalonia.Input.KeyModifiers;
using Avalonia.Input;

namespace ProFak.UI;

class ZakladkiAV : TabControl
{
	public ZakladkiAV()
	{
	}

	public TabItem Dodaj(string etykieta, Control zawartosc)
	{
		if (Wyglad.SkrotyKlawiaturoweZakladek)
		{
			var num = (char)('₁' + Items.Count);
			etykieta += $"   [ᴄᴛʀʟ-ғ{num}]";
		}

		var tabItem = new TabItem();
		tabItem.Header = etykieta;
		tabItem.Content = zawartosc;
		return tabItem;
	}

	protected override void OnLoaded(RoutedEventArgs e)
	{
		TopLevel.GetTopLevel(this)?.KeyDown += TopLevel_KeyDown;
		base.OnLoaded(e);
	}

	private void TopLevel_KeyDown(object? sender, KeyEventArgs e)
	{
		if (e.KeyModifiers == KeyModifiers.Control && e.Key >= Key.F1 && e.Key < (Key.F1 + Items.Count))
		{
			var tabIndex = e.Key - Key.F1;
			SelectedIndex = tabIndex;
			//SelectNextControl(tab, true, true, true, true);
		}
	}
}
#endif