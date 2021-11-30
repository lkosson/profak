using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	abstract class Spis<T> : DataGridView
		where T : Rekord<T>
	{
		private readonly Container container;
		private readonly BindingSource bindingSource;
		private IEnumerable<T> oryginalneRekordy;
		private string filtr;

		public Kontekst Kontekst { get; set; }
		public IEnumerable<T> WybraneRekordy
		{
			get => SelectedRows.Cast<DataGridViewRow>().Select(row => row.DataBoundItem).Cast<T>();

			set
			{
				foreach (DataGridViewRow row in Rows) if (row.DataBoundItem is T rekord) row.Selected = value.Contains(rekord);
			}
		}

		public IEnumerable<T> Rekordy
		{
			get => bindingSource.DataSource as IEnumerable<T>;
			set { oryginalneRekordy = value; var rekordy = Filtruj(value); bindingSource.DataSource = rekordy.ToList(); if (RekordyZmienione != null) RekordyZmienione(); }
		}

		public event Action RekordyZmienione;

		public Ref<T> RekordPoczatkowy { get; set; }

		public string Filtr { get => filtr; set { filtr = value; Rekordy = oryginalneRekordy; } }

		public Spis()
		{
			DoubleBuffered = true;
			AutoGenerateColumns = false;
			AllowUserToAddRows = false;
			AllowUserToDeleteRows = false;
			AllowUserToResizeRows = false;
			AllowUserToOrderColumns = true;
			RowHeadersVisible = false;
			ShowCellToolTips = true;
			ReadOnly = true;
			SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			EnableHeadersVisualStyles = false;
			TabIndex = 50;

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
				MessageBox.Show($"Nie udało się załadować danych do spisu.\n\n{exc}", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) container.Dispose();
			base.Dispose(disposing);
		}

		public void DodajKolumne(string wlasciwosc, string naglowek, bool wyrownajDoPrawej = false, bool rozciagnij = false, string format = null, int? szerokosc = null)
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
		}

		public void DodajKolumneKwota(string wlasciwosc, string naglowek) => DodajKolumne(wlasciwosc, naglowek, wyrownajDoPrawej: true, format: "#,##0.00", szerokosc: 80);
		public void DodajKolumneId() => DodajKolumne("Id", "Id", wyrownajDoPrawej: true, szerokosc: 40);

		protected abstract void Przeladuj();

		protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
		{
			base.OnCellPainting(e);
			if (e.RowIndex == -1) e.CellStyle.SelectionBackColor = System.Drawing.SystemColors.Control;
			else UstawStylWiersza((T)Rows[e.RowIndex].DataBoundItem, Columns[e.ColumnIndex].DataPropertyName, e.CellStyle);
		}

		protected virtual void UstawStylWiersza(T rekord, string kolumna, DataGridViewCellStyle styl)
		{
		}

		private IEnumerable<T> Filtruj(IEnumerable<T> rekordy)
		{
			if (String.IsNullOrWhiteSpace(Filtr)) return rekordy;
			var fragmenty = Regex.Matches(Filtr, @"(?:[^\s""]+|""[^""]*"")+");
			List<Func<T, bool>> dopasowania = new List<Func<T, bool>>();
			foreach (Match fragment in fragmenty)
			{
				if (!fragment.Success) continue;
				var fraza = fragment.Value;
				Func<T, bool> dopasowanieFragmentu = rekord => rekord.CzyPasuje(fraza);
				dopasowania.Add(dopasowanieFragmentu);
			}
			var dopasowanie = (Func<T, bool>)Delegate.Combine(dopasowania.ToArray());
			return rekordy.Where(dopasowanie);
		}
	}
}
