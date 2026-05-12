#if AVALONIA
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.LogicalTree;
using Avalonia.Media;
using ProFak.DB;

namespace ProFak.UI;

// TODO Avalonia
partial class Spis : TDataGrid
{
	protected override Type StyleKeyOverride => typeof(TDataGrid);

	public Spis()
	{
		IsReadOnly = true;
		CanUserResizeColumns = true;
		CanUserReorderColumns = true;
		CanUserSortColumns = true;
		RowHeight = Wyglad.PrzeskalujRozmiar(Wyglad.WysokoscWiersza);
	}
}

abstract partial class Spis<T> : Spis
	where T : Rekord<T>
{
	private IEnumerable<T>? RekordyImpl
	{
		get => ItemsSource as IEnumerable<T>;
		set { ItemsSource = value; Invalidate(); }
	}

	private IEnumerable<T> WybraneRekordyImpl
	{
		get => RekordPoczatkowy.IsNotNull && SelectedItems.Count == 0 ? [] : SelectedItems.Cast<T>();
		set
		{
			var rekordy = RekordyImpl?.ToHashSet() ?? [];
			SelectedItem = null;
			SelectedItems.Clear();
			foreach (var rekord in value)
				if (rekordy.Contains(rekord))
					SelectedItems.Add(rekord);
			var rekordPoczatkowy = RekordyImpl?.FirstOrDefault(r => r.Ref == RekordPoczatkowy);
			if (rekordPoczatkowy != null) SelectedItems.Add(rekordPoczatkowy);
		}
	}

	private void ZaznaczPosortowaneKolumny() { }
	private void WczytajKonfiguracje() { }
	private void ZapiszKonfiguracje() { }

	protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
	{
		base.OnAttachedToLogicalTree(e);
		if (Kontekst != null) PrzeladujBezpiecznie();
		WczytajKonfiguracje();
	}

	protected override void OnSelectionChanged(SelectionChangedEventArgs e)
	{
		if (rekordyPodczasZmiany) return;
		ZaznaczenieZmienione?.Invoke();
		base.OnSelectionChanged(e);
	}

	public virtual int PreferowanaSzerokosc
	{
		get
		{
			var szerokosc = 0;
			foreach (var kolumna in Columns)
			{
				if (!kolumna.IsVisible) continue;
				szerokosc += (int)kolumna.ActualWidth;
			}
			return szerokosc;
		}
	}

	public Spis()
	{
		TabIndex = 50;

		kolumnyKolejnosci = new List<(string kolumna, bool malejaco, Func<T, IComparable> metoda)>();
		filtr = x => true;
		CellPointerPressed += Spis_CellPointerPressed;
	}

	private void Invalidate()
	{
		var komunikat = Komunikat;
		if (String.IsNullOrEmpty(komunikat) && Rekordy.Count() == 0) komunikat = oryginalneRekordy == null || !oryginalneRekordy.Any() ? "Spis nie zawiera danych" : "Nie znaleziono pasujących rekordów";

		if (String.IsNullOrEmpty(komunikat))
		{
			Background = null;
		}
		else
		{
			var tekst = new TText();
			tekst.FontSize = 24;
			tekst.FontWeight = FontWeight.Bold;
			tekst.Text = komunikat;
			tekst.Foreground = new SolidColorBrush(new TColor(200, 50, 50, 50));
			var tlo = new VisualBrush();
			tlo.Stretch = Stretch.None;
			tlo.AlignmentX = AlignmentX.Center;
			tlo.AlignmentY = AlignmentY.Center;
			tlo.Visual = tekst;
			Background = tlo;
		}
	}

	public TDataGridColumn DodajKolumne(string wlasciwosc, string naglowek, bool checkbox = false, bool wyrownajDoPrawej = false, bool rozciagnij = false, string? format = null, int? szerokosc = null, Func<T, string?>? tooltip = null)
	{
		TDataGridColumn kolumna;
		if (checkbox)
		{
			kolumna = new DataGridCheckBoxColumn();
		}
		else
		{
			kolumna = new SpisDataGridTextColumn(wyrownajDoPrawej, PobierzStylKomorki, rekord => tooltip?.Invoke((T)rekord));
		}
		kolumna.Header = naglowek;
		kolumna.HeaderPointerPressed += Kolumna_HeaderPointerPressed;
		kolumna.Name = wlasciwosc;
		kolumna.IsVisible = szerokosc != 0;
		kolumna.Width = rozciagnij ? DataGridLength.Auto : szerokosc.HasValue ? new DataGridLength(szerokosc.Value, DataGridLengthUnitType.Pixel) : DataGridLength.SizeToHeader;
		if (rozciagnij) kolumna.MinWidth = 50;
		var binding = new ReflectionBinding(wlasciwosc);
		binding.Mode = BindingMode.OneWay;
		if (!String.IsNullOrEmpty(format)) binding.StringFormat = format;
		kolumna.Binding = binding;
		kolumna.SortMemberPath = wlasciwosc;
		// TODO Avalonia
		/*
		kolumna.DefaultCellStyle.Alignment = wyrownajDoPrawej ? DataGridViewContentAlignment.MiddleRight : DataGridViewContentAlignment.MiddleLeft;
		if (!String.IsNullOrEmpty(format)) kolumna.DefaultCellStyle.Format = format;
		if (tooltip != null) tooltipyDlaKolumn[kolumna.Index] = tooltip;
		*/
		Columns.Add(kolumna);
		return kolumna;
	}

	private (bool pogrubiona, TColor kolor, TColor tlo) PobierzStylKomorki(SpisDataGridTextColumn kolumna, object dane)
	{
		if (dane is not T rekord) return (false, default, default);
		var pogrubiona = CzyWierszPogrubiony(rekord);
		var kolor = KolorWiersza(rekord);
		return (pogrubiona, kolor, default);
	}

	private void Kolumna_HeaderPointerPressed(object? sender, PointerPressedEventArgs e)
	{
		if (e.Properties.IsRightButtonPressed)
		{
			PokazKonfiguracjeSpisu();
		}
	}

	private void Spis_CellPointerPressed(object? sender, DataGridCellPointerPressedEventArgs e)
	{
		if (e.PointerPressedEventArgs.Properties.IsLeftButtonPressed && e.PointerPressedEventArgs.ClickCount == 2)
		{
			ObsluzKlawisz?.Invoke(TKeys.Enter, TKeyModifiers.None);
		}
		else if (e.PointerPressedEventArgs.Properties.IsRightButtonPressed)
		{
			if (e.PointerPressedEventArgs.KeyModifiers == KeyModifiers.Alt)
			{
				TopLevel.GetTopLevel(this)?.Clipboard?.SetTextAsync((e.Cell.Content as TextBlock)?.Text?.Replace("\u00A0", ""));
			}
			else
			{
				PokazMenuKontekstowe?.Invoke();
			}
		}
	}

	protected override void OnKeyDown(Avalonia.Input.KeyEventArgs e)
	{
		base.OnKeyDown(e);
		ObsluzKlawisz?.Invoke(e.Key, e.KeyModifiers);
	}

	/*

	protected override void OnColumnDisplayIndexChanged(DataGridViewColumnEventArgs e)
	{
		base.OnColumnDisplayIndexChanged(e);
		kolumnyZmienione = true;
	}

	protected override void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
	{
		base.OnColumnWidthChanged(e);
		kolumnyZmienione = true;
	}

	protected override void OnColumnHeadersHeightChanged(EventArgs e)
	{
		base.OnColumnHeadersHeightChanged(e);
		UstawWysokoscWierszy(ColumnHeadersHeight);
	}

	protected override void OnRowHeightChanged(DataGridViewRowEventArgs e)
	{
		base.OnRowHeightChanged(e);
		if (e.Row.Index == -1) return;
		UstawWysokoscWierszy(e.Row.Height);
	}

	private void UstawWysokoscWierszy(int wysokosc)
	{
		if (kolumnyZmienione) return;
		kolumnyZmienione = true;
		ColumnHeadersDefaultCellStyle.WrapMode = wysokosc < FontHeight * 2 ? DataGridViewTriState.False : DataGridViewTriState.True;
		foreach (DataGridViewRow row in Rows) row.Height = wysokosc;
		ColumnHeadersHeight = wysokosc;
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		if (kolumnyZmienione)
		{
			kolumnyZmienione = false;
			ZapiszKonfiguracje();
		}
	}

	private void WczytajKonfiguracje()
	{
		if (Kontekst == null) return;
		var spis = GetType().Name;
		var kolumny = Kontekst.Baza.KolumnySpisow.Where(e => e.Spis == spis).OrderBy(e => e.Kolejnosc);
		kolumnyKolejnosci.Clear();

		foreach (var kolumna in kolumny.Where(e => e.PoziomSortowania != 0).OrderBy(e => Math.Abs(e.PoziomSortowania)))
		{
			UstawKolejnosc(kolumna.Kolumna, false);
			if (kolumna.PoziomSortowania < 0) UstawKolejnosc(kolumna.Kolumna, false);
		}

		if (kolumnyKolejnosci.Any() && oryginalneRekordy != null) OdswiezWiersze();

		foreach (var kolumna in kolumny)
		{
			if (kolumna.Kolumna == WYSOKOSC_WIERSZA)
			{
				foreach (DataGridViewRow row in Rows) row.Height = kolumna.Szerokosc;
				RowTemplate.Height = kolumna.Szerokosc;
				ColumnHeadersHeight = kolumna.Szerokosc;
				continue;
			}
			var kolumnaSpisu = Columns[kolumna.Kolumna];
			if (kolumnaSpisu == null) continue;
			if (kolumna.Szerokosc > 0)
			{
				kolumnaSpisu.AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;
				kolumnaSpisu.Width = kolumna.Szerokosc;
			}
			if (kolumna.Szerokosc == -1) kolumnaSpisu.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			kolumnaSpisu.Visible = kolumna.Szerokosc != 0;
			kolumnaSpisu.DisplayIndex = kolumna.Kolejnosc;
			kolumnaSpisu.HeaderCell.SortGlyphDirection = kolumna.PoziomSortowania < 0 ? SortOrder.Descending : kolumna.PoziomSortowania > 0 ? SortOrder.Ascending : SortOrder.None;
		}
		kolumnyZmienione = false;
	}

	private void ZapiszKonfiguracje()
	{
		if (Kontekst == null) return;
		if (Kontekst.Baza.CzyZablokowana()) return;
		var spis = GetType().Name;
		var stareKolumny = Kontekst.Baza.KolumnySpisow.Where(e => e.Spis == spis).ToList();
		Kontekst.Baza.Usun(stareKolumny);

		var sortowanie = new Dictionary<string, int>();
		for (var i = 0; i < kolumnyKolejnosci.Count; i++)
		{
			sortowanie[kolumnyKolejnosci[i].kolumna] = (i + 1) * (kolumnyKolejnosci[i].malejaco ? -1 : 1);
		}

		var doZapisu = new List<KolumnaSpisu>();
		doZapisu.Add(new KolumnaSpisu { Spis = spis, Kolumna = WYSOKOSC_WIERSZA, Kolejnosc = -1, Szerokosc = ColumnHeadersHeight });
		foreach (DataGridViewColumn kolumna in Columns)
		{
			var konfiguracjaKolumny = new KolumnaSpisu { Spis = spis, Kolumna = kolumna.Name };
			konfiguracjaKolumny.Kolejnosc = kolumna.DisplayIndex;
			konfiguracjaKolumny.Szerokosc = kolumna.Visible ? kolumna.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill ? -1 : kolumna.Width : 0;
			konfiguracjaKolumny.PoziomSortowania = sortowanie.GetValueOrDefault(kolumna.Name);
			doZapisu.Add(konfiguracjaKolumny);
		}
		Kontekst.Baza.Zapisz(doZapisu);
	}
	*/
}

class SpisEdytowalny : Spis
{
	private Action<object> edycja;
	private Action<object[]> usuniecie;

	public SpisEdytowalny(Action<object> edycja, Action<object[]> usuniecie)
	{
		this.edycja = edycja;
		this.usuniecie = usuniecie;
		IsReadOnly = false;
		AutoGenerateColumns = true;
	}

	protected override void OnCellEditEnding(DataGridCellEditEndingEventArgs e)
	{
		base.OnCellEditEnding(e);
		if (e.EditAction == DataGridEditAction.Commit && e.Row.DataContext != null)
		{
			edycja(e.Row.DataContext);
		}
	}

	protected override void OnKeyDown(Avalonia.Input.KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (e.Key == Key.Delete && e.KeyModifiers == KeyModifiers.None)
		{
			usuniecie(SelectedItems.Cast<object>().ToArray());
		}
	}
}

class SpisDataGridTextColumn : DataGridTextColumn
{
	private readonly bool wyrownajDoPrawej;
	private readonly Func<SpisDataGridTextColumn, object, (bool pogrubiona, TColor kolor, TColor tlo)> pobierzStylKomorki;
	private readonly Func<object, string?> pobierzTooltip;

	public SpisDataGridTextColumn(bool wyrownajDoPrawej, Func<SpisDataGridTextColumn, object, (bool pogrubiona, TColor kolor, TColor tlo)> pobierzStylKomorki, Func<object, string?> pobierzTooltip)
	{
		this.wyrownajDoPrawej = wyrownajDoPrawej;
		this.pobierzStylKomorki = pobierzStylKomorki;
		this.pobierzTooltip = pobierzTooltip;
	}

	protected override TControl GenerateElement(Avalonia.Controls.DataGridCell cell, object dataItem)
	{
		var element = (TextBlock)base.GenerateElement(cell, dataItem);
		if (wyrownajDoPrawej) element.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right;
		var styl = pobierzStylKomorki(this, dataItem);
		if (styl.pogrubiona) element.FontWeight = FontWeight.Bold;
		if (styl.kolor != default) element.Foreground = new SolidColorBrush(styl.kolor);
		if (styl.tlo != default) element.Background = new SolidColorBrush(styl.tlo);
		var tooltip = pobierzTooltip(dataItem);
		if (tooltip != null) element.SetValue(Avalonia.Controls.ToolTip.TipProperty, tooltip);
		return element;
	}
}

#endif