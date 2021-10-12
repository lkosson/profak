﻿using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	abstract class Spis<T> : Spis, ISpis<T>
		where T : Rekord<T>
	{
		private readonly Container container;
		private readonly BindingSource bindingSource;

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
			set => bindingSource.DataSource = value;
		}

		public Spis()
		{
			container = new Container();
			bindingSource = new BindingSource(container);
			bindingSource.DataSource = typeof(T);
			DataSource = bindingSource;
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			Przeladuj();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing) container.Dispose();
			base.Dispose(disposing);
		}

		public void DodajKolumne(string wlasciwosc, string naglowek, bool wyrownajDoPrawej = false, bool rozciagnij = false)
		{
			var kolumna = new DataGridViewTextBoxColumn();
			kolumna.HeaderText = naglowek;
			kolumna.DataPropertyName = wlasciwosc;
			kolumna.Name = wlasciwosc;
			kolumna.DefaultCellStyle.Alignment = wyrownajDoPrawej ? DataGridViewContentAlignment.MiddleRight : DataGridViewContentAlignment.MiddleLeft;
			kolumna.AutoSizeMode = rozciagnij ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.NotSet;
			Columns.Add(kolumna);
		}

		public abstract void Przeladuj();
	}
}