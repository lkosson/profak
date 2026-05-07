#if WINFORMS
using ProFak.DB;
using System.ComponentModel;

namespace ProFak.UI;

partial class Spis : DataGridView
{
	public Spis()
	{
		DoubleBuffered = true;
		AllowUserToAddRows = false;
		AllowUserToDeleteRows = false;
		AllowUserToResizeRows = true;
		AllowUserToOrderColumns = true;
		RowHeadersVisible = true;
		RowHeadersWidth = 16;
		ColumnHeadersHeight = Wyglad.PrzeskalujRozmiar(Wyglad.WysokoscWiersza) * DeviceDpi / 96;
		RowTemplate.Height = ColumnHeadersHeight;
		ShowCellToolTips = true;
		EnableHeadersVisualStyles = false;
		ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
	}

	protected override void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		if (e.Button != MouseButtons.Left) return;
		var hit = HitTest(e.X, e.Y);
		if (hit.Type == DataGridViewHitTestType.ColumnHeader || hit.Type == DataGridViewHitTestType.RowHeader) DoubleBuffered = false;
	}

	protected override void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		if (!DoubleBuffered) DoubleBuffered = true;
	}
}

abstract partial class Spis<T> : Spis
	where T : Rekord<T>
{
	private readonly Container container;
	private readonly BindingSource bindingSource;

	private IEnumerable<T> WybraneRekordyImpl
	{
		get => SelectedRows.Cast<DataGridViewRow>().Select(row => row.DataBoundItem).Cast<T>();

		set
		{
			var pierwszy = true;
			foreach (DataGridViewRow row in Rows)
			{
				if (!(row.DataBoundItem is T rekord)) continue;
				var wybrany = value.Contains(rekord);
				row.Selected = wybrany;
				if (pierwszy && wybrany)
				{
					CurrentCell = row.Cells.Cast<DataGridViewCell>().FirstOrDefault(e => e.Visible);
					pierwszy = false;
				}
			}
		}
	}

	private IEnumerable<T>? RekordyImpl
	{
		get => bindingSource.DataSource as IEnumerable<T>;
		set => bindingSource.DataSource = value;
	}

	public virtual int PreferowanaSzerokosc
	{
		get
		{
			var szerokosc = 0;
			foreach (DataGridViewColumn kolumna in Columns)
			{
				if (!kolumna.Visible) continue;
				if (kolumna.AutoSizeMode == DataGridViewAutoSizeColumnMode.Fill) szerokosc += 400;
				else szerokosc += kolumna.Width;
			}
			return szerokosc;
		}
	}

	public Spis()
	{
		AutoGenerateColumns = false;
		ReadOnly = true;
		SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		TabIndex = 50;

		container = new Container();
		bindingSource = new BindingSource(container);
		bindingSource.DataSource = typeof(T);
		DataSource = bindingSource;
		MinimumSize = new Size(500, 100);
		Rows.CollectionChanged += Rows_CollectionChanged;
	}

	private void Rows_CollectionChanged(object? sender, CollectionChangeEventArgs e)
	{
		if (RekordPoczatkowy == default) return;

		foreach (DataGridViewRow row in Rows)
		{
			if (row.DataBoundItem is not T rekord) continue;
			if (rekord.Ref != RekordPoczatkowy) continue;
			bindingSource.Position = row.Index;
			break;
		}
	}

	protected override void OnBindingContextChanged(EventArgs e)
	{
		base.OnBindingContextChanged(e);
		ZaznaczPosortowaneKolumny();
	}

	protected override void OnSelectionChanged(EventArgs e)
	{
		if (rekordyPodczasZmiany) return;
		if (bindingSource.DataSource == null) return;
		ZaznaczenieZmienione?.Invoke();
		base.OnSelectionChanged(e);
	}

	protected override void OnCreateControl()
	{
		base.OnCreateControl();
		if (Kontekst != null) PrzeladujBezpiecznie();
		bindingSource.ResetBindings(true);
		WczytajKonfiguracje();
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		var komunikat = Komunikat;
		if (String.IsNullOrEmpty(komunikat) && Rekordy.Count() == 0) komunikat = oryginalneRekordy == null || !oryginalneRekordy.Any() ? "Spis nie zawiera danych" : "Nie znaleziono pasujących rekordów";
		if (!String.IsNullOrEmpty(komunikat))
		{
			using var font = new Font(Font.FontFamily, 24, FontStyle.Bold);
			using var brush = new SolidBrush(Color.FromArgb(200, 50, 50, 50));
			using var sf = new StringFormat(StringFormat.GenericTypographic) { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
			e.Graphics.DrawString(komunikat, font, brush, new RectangleF(0, 0, Width, Height), sf);
		}
	}

	protected override void OnResize(EventArgs e)
	{
		if (!String.IsNullOrWhiteSpace(Komunikat)) Invalidate();
		base.OnResize(e);
	}

	protected override void OnScroll(ScrollEventArgs e)
	{
		if (!String.IsNullOrWhiteSpace(Komunikat)) Invalidate();
		base.OnScroll(e);
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing) container.Dispose();
		base.Dispose(disposing);
	}

	public TDataGridColumn DodajKolumne(string wlasciwosc, string naglowek, bool checkbox = false, bool wyrownajDoPrawej = false, bool rozciagnij = false, string? format = null, int? szerokosc = null, Func<T, string?>? tooltip = null)
	{
		TDataGridColumn kolumna = checkbox ? new DataGridViewCheckBoxColumn() : new DataGridViewTextBoxColumn();
		kolumna.HeaderText = naglowek;
		kolumna.DataPropertyName = wlasciwosc;
		kolumna.Name = wlasciwosc;
		kolumna.DefaultCellStyle.Alignment = wyrownajDoPrawej ? DataGridViewContentAlignment.MiddleRight : DataGridViewContentAlignment.MiddleLeft;
		kolumna.AutoSizeMode = rozciagnij ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.NotSet;
		kolumna.Visible = szerokosc != 0;
		if (!String.IsNullOrEmpty(format)) kolumna.DefaultCellStyle.Format = format;
		if (szerokosc.HasValue) kolumna.Width = Wyglad.PrzeskalujRozmiar(szerokosc.Value) * DeviceDpi / 96;
		if (rozciagnij) kolumna.MinimumWidth = 50;
		Columns.Add(kolumna);
		if (tooltip != null) tooltipyDlaKolumn[kolumna.Index] = tooltip;
		return kolumna;
	}

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

	private void UstawStylWiersza(T rekord, string kolumna, DataGridViewCellStyle styl)
	{
		if (CzyWierszPogrubiony(rekord)) styl.Font = new Font(styl.Font!, FontStyle.Bold);
		var kolor = KolorWiersza(rekord);
		if (kolor != default) styl.ForeColor = kolor;
	}

	protected override void OnCellClick(DataGridViewCellEventArgs e)
	{
		base.OnCellClick(e);
		if (e.ColumnIndex == -1)
		{
			SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			if (e.RowIndex != -1) Rows[e.RowIndex].Selected = true;
		}
		else if (e.RowIndex == -1)
		{
			UstawKolejnosc(Columns[e.ColumnIndex].DataPropertyName, ModifierKeys != TKeys.Control && ModifierKeys != TKeyModifiers.Shift);
			OdswiezWiersze();
			ZaznaczPosortowaneKolumny();
			kolumnyZmienione = true;
		}
	}

	protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
	{
		if ((ModifierKeys & Keys.Alt) == Keys.Alt)
		{
			SelectionMode = DataGridViewSelectionMode.CellSelect;
		}
		if (e.Button == MouseButtons.Right)
		{
			if (e.RowIndex == -1) PokazKonfiguracjeSpisu();
			else PokazMenuKontekstowe?.Invoke();
		}
		base.OnCellMouseDown(e);
	}

	protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
	{
		if (e.RowIndex != -1 && e.ColumnIndex != -1) ObsluzKlawisz?.Invoke(TKeys.Enter, TKeyModifiers.None);
		base.OnCellDoubleClick(e);
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		e.Handled = ObsluzKlawisz?.Invoke(e.KeyCode, e.Modifiers) ?? false;
		base.OnKeyDown(e);
	}

	public override DataObject? GetClipboardContent()
	{
		var dataObject = base.GetClipboardContent();
		if (dataObject == null) return null;

		var poprawiony = new DataObject();
		void PoprawFormatowanie(TextDataFormat format)
		{
			if (!dataObject.ContainsText(format)) return;
			var tekst = dataObject.GetText(format);
			if (tekst == null) return;
			tekst = tekst.Replace("\u00A0", "");
			poprawiony.SetText(tekst, format);
		}

		PoprawFormatowanie(TextDataFormat.Text);
		PoprawFormatowanie(TextDataFormat.UnicodeText);
		PoprawFormatowanie(TextDataFormat.CommaSeparatedValue);
		// Bez HTML

		return poprawiony;
	}

	private void ZaznaczPosortowaneKolumny()
	{
		foreach (DataGridViewColumn kolumna in Columns)
		{
			kolumna.HeaderCell.SortGlyphDirection = SortOrder.None;
		}

		foreach (var kolumna in kolumnyKolejnosci)
		{
			Columns[kolumna.kolumna]?.HeaderCell.SortGlyphDirection = kolumna.malejaco ? SortOrder.Descending : SortOrder.Ascending;
		}
	}

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
}

class SpisEdytowalny : Spis
{
	private Action<object> edycja;
	private Action<object[]> usuniecie;

	public SpisEdytowalny(Action<object> edycja, Action<object[]> usuniecie)
	{
		this.edycja = edycja;
		this.usuniecie = usuniecie;
		AutoGenerateColumns = true;
		AllowUserToDeleteRows = true;
		ReadOnly = false;
	}

	protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
	{
		base.OnCellEndEdit(e);
		var rekord = Rows[e.RowIndex].DataBoundItem;
		if (rekord != null) edycja(rekord);
	}

	protected override void OnKeyUp(KeyEventArgs e)
	{
		base.OnKeyUp(e);
		if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.None) usuniecie(SelectedRows.Cast<DataGridViewRow>().Where(e => e.DataBoundItem != null).Select(e => e.DataBoundItem!).ToArray());
	}
}
#endif