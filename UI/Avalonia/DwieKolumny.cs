#if AVALONIA
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ProFak.UI;

class DwieKolumnyAV : Grid
{
	public DwieKolumnyAV()
	{
		ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
		ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
	}

	protected override void OnLoaded(RoutedEventArgs e)
	{
		RowDefinitions.Add(new RowDefinition(GridLength.Star));
		base.OnLoaded(e);
	}

	public void DodajWiersz(TControl kontrolka, string? etykieta = null, bool pelnaSzerokosc = false)
	{
		RowDefinitions.Add(new RowDefinition(GridLength.Auto));

		if (!String.IsNullOrEmpty(etykieta))
		{
			var label = KontrolkiAV.Label(etykieta);
			Children.Add(label);
			SetColumn(label, 0);
			SetRow(label, RowDefinitions.Count - 1);
		}

		if (pelnaSzerokosc)
		{
			Children.Add(kontrolka);
			SetColumn(kontrolka, 0);
			SetRow(kontrolka, RowDefinitions.Count - 1);
			SetColumnSpan(kontrolka, 2);
		}
		else
		{
			Children.Add(kontrolka);
			SetColumn(kontrolka, 1);
			SetRow(kontrolka, RowDefinitions.Count - 1);
		}
	}
}
#endif