#if AVALONIA
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace ProFak.UI;

class Siatka : Grid
{
	protected override Type StyleKeyOverride => typeof(Grid);

	private int wiersz;

	public Siatka(IEnumerable<int> szerokosci, IEnumerable<int> wysokosci)
	{
		foreach (var szerokosc in szerokosci)
		{
			if (szerokosc > 0) ColumnDefinitions.Add(new ColumnDefinition(szerokosc, GridUnitType.Pixel));
			else if (szerokosc < 0) ColumnDefinitions.Add(new ColumnDefinition(-szerokosc, GridUnitType.Star));
			else ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
		}

		foreach (var wysokosc in wysokosci)
		{
			if (wysokosc > 0) RowDefinitions.Add(new RowDefinition(wysokosc, GridUnitType.Pixel));
			else if (wysokosc < 0) RowDefinitions.Add(new RowDefinition(-wysokosc, GridUnitType.Star));
			else RowDefinitions.Add(new RowDefinition(GridLength.Auto));
		}
	}

	public void OgraniczSzerokosc(int szerokosc)
	{
	}

	protected override void OnLoaded(RoutedEventArgs e)
	{
		var jestWypelnienie = false;
		foreach (var rowDefinition in RowDefinitions)
		{
			if (rowDefinition.Height.IsStar)
			{
				jestWypelnienie = true;
				break;
			}
		}

		if (!jestWypelnienie) RowDefinitions.Add(new RowDefinition(GridLength.Star));

		base.OnLoaded(e);
	}

	public void DodajWiersz(IEnumerable<(TControl? kontrolka, int kolumny)> kontrolki)
	{
		if (wiersz >= RowDefinitions.Count) RowDefinitions.Add(new RowDefinition(GridLength.Auto));

		var kolumna = 0;
		foreach (var (kontrolka, kolumny) in kontrolki)
		{
			if (kontrolka == null)
			{
				kolumna += kolumny;
				continue;
			}

			kontrolka.Width = Double.NaN;
			kontrolka.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
			Children.Add(kontrolka);
			SetColumn(kontrolka, kolumna);
			SetRow(kontrolka, wiersz);
			SetColumnSpan(kontrolka, kolumny);
			kolumna += kolumny;
		}

		wiersz++;
	}

	public void DodajWiersz(IEnumerable<TControl?> kontrolki)
		=> DodajWiersz(kontrolki.Select(kontrolka => (kontrolka, 1)));

	public void DodajWiersz(string etykieta, IEnumerable<(TControl? kontrolka, int kolumny)> kontrolki)
		=> DodajWiersz(kontrolki.Prepend((Kontrolki.Label(etykieta), 1)));

	public void DodajWiersz(string etykieta, IEnumerable<TControl?> kontrolki)
		=> DodajWiersz(etykieta, kontrolki.Select(kontrolka => (kontrolka, 1)));

	public void DodajWiersz(IEnumerable<string?> etykiety)
		=> DodajWiersz(etykiety.Select(etykieta => (String.IsNullOrEmpty(etykieta) ? null : (TControl?)Kontrolki.Label(etykieta), 1)));

	public static Siatka BlokadaRozciagania(TControl kontrolka)
	{
		var siatka = new Siatka([-1, 0, -1], []);
		siatka.DodajWiersz([null, kontrolka, null]);
		return siatka;
	}
}
#endif