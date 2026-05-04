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
			SelectedItems.Clear();
			foreach (var rekord in value)
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
		TDataGridColumn kolumna = checkbox ? new DataGridCheckBoxColumn() : new DataGridTextColumn();
		kolumna.Header = naglowek;
		kolumna.HeaderPointerPressed += Kolumna_HeaderPointerPressed;
		kolumna.Name = wlasciwosc;
		kolumna.IsVisible = szerokosc != 0;
		kolumna.Width = rozciagnij ? new DataGridLength(1, DataGridLengthUnitType.Star) : szerokosc.HasValue ? new DataGridLength(szerokosc.Value, DataGridLengthUnitType.Pixel) : DataGridLength.SizeToHeader;
		if (rozciagnij) kolumna.MinWidth = 50;
		var binding = new ReflectionBinding(wlasciwosc);
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

	

	/*
protected override void OnCellToolTipTextNeeded(DataGridViewCellToolTipTextNeededEventArgs e)
{
if (e.RowIndex != -1
&& tooltipyDlaKolumn.TryGetValue(e.ColumnIndex, out var tooltip)
&& Rows[e.RowIndex].DataBoundItem is T rekord) e.ToolTipText = tooltip(rekord);
else base.OnCellToolTipTextNeeded(e);
}

protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
{
base.OnCellPainting(e);
if (e.RowIndex == -1)
{
e.CellStyle?.SelectionBackColor = System.Drawing.SystemColors.Control;
}
else if (e.ColumnIndex != -1 && e.CellStyle != null)
{
if (Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending) e.CellStyle.BackColor = Color.FromArgb(210, 242, 167);
else if (Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending) e.CellStyle.BackColor = Color.FromArgb(242, 219, 167);
if (Rows[e.RowIndex].DataBoundItem is T rekord) UstawStylWiersza(rekord, Columns[e.ColumnIndex].DataPropertyName, e.CellStyle);
}
}
*/

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
			ObsluzKlawisz?.Invoke(Keys.Enter, Keys.None);
		}
		else if (e.PointerPressedEventArgs.Properties.IsRightButtonPressed)
		{
			TopLevel.GetTopLevel(this)?.Clipboard?.SetTextAsync((e.Cell.Content as TextBlock)?.Text?.Replace("\u00A0", ""));
		}
	}

	protected override void OnKeyDown(Avalonia.Input.KeyEventArgs e)
	{
		base.OnKeyDown(e);
		//ObsluzKlawisz(e.Key, e.KeyModifiers);
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
#endif