using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class Spis : DataGridView
	{
		public Spis()
		{
			DoubleBuffered = true;
			AllowUserToAddRows = false;
			AllowUserToDeleteRows = false;
			AllowUserToResizeRows = false;
			AllowUserToOrderColumns = true;
			RowHeadersVisible = false;
			ShowCellToolTips = true;
			EnableHeadersVisualStyles = false;
		}
	}

	abstract class Spis<T> : Spis
		where T : Rekord<T>
	{
		private readonly Container container;
		private readonly BindingSource bindingSource;
		private IEnumerable<T> oryginalneRekordy;
		private Func<T, bool> filtr;
		private List<(string kolumna, bool malejaco, Func<T, IComparable> metoda)> kolumnyKolejnosci;
		private Dictionary<int, Func<T, string>> tooltipyDlaKolumn;
		private bool rekordyPodczasZmiany;

		public Kontekst Kontekst { get; set; }
		public IEnumerable<T> WybraneRekordy
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
						CurrentCell = row.Cells[0];
						pierwszy = false;
					}
				}
			}
		}

		public IEnumerable<T> Rekordy
		{
			get => bindingSource.DataSource as IEnumerable<T>;
			set
			{
				var zaznaczoneRekordy = WybraneRekordy.ToList();
				oryginalneRekordy = value; 
				var rekordy = Sortuj(value.Where(filtr)).ToList(); 
				rekordyPodczasZmiany = true; 
				bindingSource.DataSource = rekordy; 
				rekordyPodczasZmiany = false; 
				RekordyZmienione?.Invoke();
				WybraneRekordy = zaznaczoneRekordy;
			}
		}

		public virtual string Podsumowanie
		{
			get
			{
				var liczbaWszystkich = Rekordy.Count();
				var liczbaWybranych = WybraneRekordy.Count();
				string tekst;
				if (liczbaWszystkich == 0) tekst = "Spis nie zawiera danych";
				else tekst = $"Liczba pozycji: {liczbaWszystkich}";
				if (liczbaWybranych > 1) tekst += $"\nLiczba zaznaczonych: {liczbaWybranych}";
				return tekst;
			}
		}

		public event Action RekordyZmienione;
		public event Action ZaznaczenieZmienione;

		public Ref<T> RekordPoczatkowy { get; set; }

		public Spis()
		{
			AutoGenerateColumns = false;
			ReadOnly = true;
			SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			TabIndex = 50;

			kolumnyKolejnosci = new List<(string kolumna, bool malejaco, Func<T, IComparable> metoda)>();
			filtr = x => true;

			container = new Container();
			bindingSource = new BindingSource(container);
			bindingSource.DataSource = typeof(T);
			DataSource = bindingSource;
			MinimumSize = new System.Drawing.Size(500, 100);
			Rows.CollectionChanged += Rows_CollectionChanged;
		}

		private void Rows_CollectionChanged(object sender, CollectionChangeEventArgs e)
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
		}

		public void PrzeladujBezpiecznie()
		{
			try
			{
				Przeladuj();
			}
			catch (Exception exc)
			{
				OknoBledu.Pokaz(exc);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) container.Dispose();
			base.Dispose(disposing);
		}

		public DataGridViewTextBoxColumn DodajKolumne(string wlasciwosc, string naglowek, bool wyrownajDoPrawej = false, bool rozciagnij = false, string format = null, int? szerokosc = null, Func<T, string> tooltip = null)
		{
			var kolumna = new DataGridViewTextBoxColumn();
			kolumna.HeaderText = naglowek;
			kolumna.DataPropertyName = wlasciwosc;
			kolumna.Name = wlasciwosc;
			kolumna.DefaultCellStyle.Alignment = wyrownajDoPrawej ? DataGridViewContentAlignment.MiddleRight : DataGridViewContentAlignment.MiddleLeft;
			kolumna.AutoSizeMode = rozciagnij ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.NotSet;
			if (!String.IsNullOrEmpty(format)) kolumna.DefaultCellStyle.Format = format;
			if (szerokosc.HasValue) kolumna.Width = szerokosc.Value;
			if (rozciagnij) kolumna.MinimumWidth = 50;
			Columns.Add(kolumna);
			if (tooltip != null)
			{
				if (tooltipyDlaKolumn == null) tooltipyDlaKolumn = new Dictionary<int, Func<T, string>>();
				tooltipyDlaKolumn[kolumna.Index] = tooltip;
			}
			return kolumna;
		}

		public DataGridViewCheckBoxColumn DodajKolumneBool(string wlasciwosc, string naglowek, int? szerokosc = null, Func<T, string> tooltip = null)
		{
			var kolumna = new DataGridViewCheckBoxColumn();
			kolumna.HeaderText = naglowek;
			kolumna.DataPropertyName = wlasciwosc;
			kolumna.Name = wlasciwosc;
			if (szerokosc.HasValue) kolumna.Width = szerokosc.Value;
			kolumna.SortMode = DataGridViewColumnSortMode.Programmatic;
			Columns.Add(kolumna);
			if (tooltip != null)
			{
				if (tooltipyDlaKolumn == null) tooltipyDlaKolumn = new Dictionary<int, Func<T, string>>();
				tooltipyDlaKolumn[kolumna.Index] = tooltip;
			}
			return kolumna;
		}

		public DataGridViewTextBoxColumn DodajKolumneKwota(string wlasciwosc, string naglowek, int? szerokosc = 80, Func<T, string> tooltip = null) => DodajKolumne(wlasciwosc, naglowek, wyrownajDoPrawej: true, format: "#,##0.00", szerokosc: szerokosc, tooltip: tooltip);
		public DataGridViewTextBoxColumn DodajKolumneId() => DodajKolumne("Id", "Id", wyrownajDoPrawej: true, szerokosc: 60);

		protected abstract void Przeladuj();

		protected override void OnCellToolTipTextNeeded(DataGridViewCellToolTipTextNeededEventArgs e)
		{
			if (tooltipyDlaKolumn != null && e.RowIndex != -1 && tooltipyDlaKolumn.TryGetValue(e.ColumnIndex, out var tooltip)) e.ToolTipText = tooltip((T)Rows[e.RowIndex].DataBoundItem);
			else base.OnCellToolTipTextNeeded(e);
		}

		protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
		{
			base.OnCellPainting(e);
			if (e.RowIndex == -1)
			{
				e.CellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
			}
			else
			{
				if (Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Ascending) e.CellStyle.BackColor = Color.FromArgb(210, 242, 167);
				else if (Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending) e.CellStyle.BackColor = Color.FromArgb(242, 219, 167);

				UstawStylWiersza((T)Rows[e.RowIndex].DataBoundItem, Columns[e.ColumnIndex].DataPropertyName, e.CellStyle);
			}
		}

		protected virtual void UstawStylWiersza(T rekord, string kolumna, DataGridViewCellStyle styl)
		{
		}

		protected override void OnCellClick(DataGridViewCellEventArgs e)
		{
			base.OnCellClick(e);
			if (e.RowIndex == -1)
			{
				UstawKolejnosc(Columns[e.ColumnIndex].DataPropertyName, ModifierKeys != Keys.Control && ModifierKeys != Keys.Shift);
				foreach (DataGridViewColumn kolumna in Columns)
				{
					kolumna.HeaderCell.SortGlyphDirection = SortOrder.None;
				}
				foreach (var kolumna in kolumnyKolejnosci)
				{
					Columns[kolumna.kolumna].HeaderCell.SortGlyphDirection = kolumna.malejaco ? SortOrder.Descending : SortOrder.Ascending;
				}
			}
		}

		protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
		{
			if ((ModifierKeys & Keys.Alt) == Keys.Alt)
			{
				SelectionMode = DataGridViewSelectionMode.CellSelect;
			}
			base.OnCellMouseDown(e);
		}

		public void UstawFiltr(string wyrazenieFiltra)
		{
			if (String.IsNullOrWhiteSpace(wyrazenieFiltra))
			{
				filtr = rekord => true;
			}
			else
			{
				var fragmenty = Regex.Matches(wyrazenieFiltra, @"(?:[^\s""]+|""[^""]*"")+");
				List<Func<T, bool>> dopasowania = new List<Func<T, bool>>();
				foreach (Match fragment in fragmenty)
				{
					if (!fragment.Success) continue;
					var fraza = fragment.Value;
					Func<T, bool> dopasowanieFragmentu = rekord => rekord.CzyPasuje(fraza);
					dopasowania.Add(dopasowanieFragmentu);
				}
				filtr = (Func<T, bool>)Delegate.Combine(dopasowania.ToArray());
			}

			Rekordy = oryginalneRekordy;
		}

		public void UstawKolejnosc(string kolumna, bool zastap)
		{
			var getter = typeof(T).GetProperty(kolumna)?.GetGetMethod();
			if (getter == null) return;
			if (!getter.ReturnType.IsAssignableTo(typeof(IComparable))) return;

			bool dodaj;
			if (zastap)
			{
				for (int i = 0; i < kolumnyKolejnosci.Count; i++)
				{
					if (kolumnyKolejnosci[i].kolumna == kolumna)
					{
						kolumnyKolejnosci[i] = (kolumna, !kolumnyKolejnosci[i].malejaco, kolumnyKolejnosci[i].metoda);
						zastap = false;
						break;
					}
				}

				if (zastap)
				{
					kolumnyKolejnosci.Clear();
					dodaj = true;
				}
				else
				{
					dodaj = false;
				}
			}
			else
			{
				dodaj = true;
				for (int i = 0; i < kolumnyKolejnosci.Count; i++)
				{
					if (kolumnyKolejnosci[i].kolumna == kolumna)
					{
						kolumnyKolejnosci[i] = (kolumna, !kolumnyKolejnosci[i].malejaco, kolumnyKolejnosci[i].metoda);
						dodaj = false;
						break;
					}
				}
			}

			if (dodaj)
			{
				var parametr = Expression.Parameter(typeof(T), "rekord");
				var wartoscExpr = Expression.Call(parametr, getter);
				var wartoscCompExpr = Expression.Convert(wartoscExpr, typeof(IComparable));
				var lambdaExpr = Expression.Lambda<Func<T, IComparable>>(wartoscCompExpr, parametr);
				var metoda = lambdaExpr.Compile();
				kolumnyKolejnosci.Add((kolumna, false, metoda));
			}

			Rekordy = oryginalneRekordy;
		}

		private IEnumerable<T> Sortuj(IEnumerable<T> rekordy)
		{
			var posortowane = rekordy.OrderBy(r => 0);
			foreach (var kolumna in kolumnyKolejnosci)
			{
				if (kolumna.malejaco) posortowane = posortowane.ThenByDescending(kolumna.metoda);
				else posortowane = posortowane.ThenBy(kolumna.metoda);
			}
			return posortowane;
		}
	}
}
