#if AVALONIA
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;

namespace ProFak.UI;

class Zakladki : TabControl
{
	protected override Type StyleKeyOverride => typeof(Avalonia.Controls.TabControl);

	public Zakladki()
	{
	}

	public TTabPage Dodaj(string etykieta, TControl zawartosc)
	{
		if (Wyglad.SkrotyKlawiaturoweZakladek)
		{
			var num = (char)('₁' + Items.Count);
			etykieta += $"   [ᴄᴛʀʟ-{num}]";
		}

		var tabItem = new TTabPage();
		tabItem.Header = etykieta;
		tabItem.Content = zawartosc;
		Items.Add(tabItem);
		return tabItem;
	}

	public void Usun(TTabPage zakladka)
	{
		Items.Remove(zakladka);
	}

	protected override void OnLoaded(RoutedEventArgs e)
	{
		TopLevel.GetTopLevel(this)?.KeyDown += TopLevel_KeyDown;
		base.OnLoaded(e);
	}

	private void TopLevel_KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
	{
		if (e.KeyModifiers == Avalonia.Input.KeyModifiers.Control && e.Key >= Key.D1 && e.Key < (Key.D1+ Items.Count))
		{
			var tabIndex = e.Key - Key.D1;
			SelectedIndex = tabIndex;
		}
	}

	public TTabPage? SelectedTab { get => (TTabPage?)Items[SelectedIndex]; set => SelectedIndex = Items.IndexOf(value); }
}
#endif