using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	partial class SpisZAkcjami<T> : TableLayoutPanel
		where T : Rekord<T>
	{
		private readonly List<AkcjaNaSpisie<T>> akcje;
		private readonly Spis<T> spis;
		private readonly PanelAkcji panelAkcji;

		public List<AkcjaNaSpisie<T>> Akcje => akcje;
		public Spis<T> Spis => spis;

		private SpisZAkcjami()
		{
			akcje = new List<AkcjaNaSpisie<T>>();
			ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
			ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			RowStyles.Add(new RowStyle(SizeType.Percent, 100));

			panelAkcji = new PanelAkcji();
			Controls.Add(panelAkcji, 1, 0);
		}

		private SpisZAkcjami(Spis<T> spis)
			: this()
		{
			this.spis = spis;
			spis.Dock = DockStyle.Fill;
			spis.SelectionChanged += spis_SelectionChanged;
			spis.CellDoubleClick += spis_CellDoubleClick;
			spis.KeyPress += spis_KeyPress;
			Controls.Add(spis, 0, 0);
			MinimumSize = new Size(panelAkcji.MinimumSize.Width + spis.MinimumSize.Width, panelAkcji.MinimumSize.Height + spis.MinimumSize.Height);
		}

		private void spis_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r') DomyslnaAkcja();
		}

		private void spis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == -1) return;
			DomyslnaAkcja();
		}

		private void spis_SelectionChanged(object sender, EventArgs e)
		{
			panelAkcji.Aktualizuj();
		}

		private void DomyslnaAkcja()
		{
			AkcjaNaSpisie<T> domyslnaAkcja = null;
			foreach (var akcja in akcje)
			{
				if (akcja.CzyDomyslna) domyslnaAkcja = akcja;
			}
			if (domyslnaAkcja == null) return;
			domyslnaAkcja.UtworzAdapter(spis).Uruchom();
		}

		protected override void OnCreateControl()
		{
			foreach (var akcja in akcje)
			{
				panelAkcji.DodajAkcje(akcja.UtworzAdapter(spis));
			}
			panelAkcji.AktualizujUklad();
			base.OnCreateControl();
		}

		public static SpisZAkcjami<T> Utworz(Spis<T> spis, params AkcjaNaSpisie<T>[] akcje)
		{
			var okno = new SpisZAkcjami<T>(spis);
			okno.Akcje.AddRange(akcje);
			return okno;
		}
	}
}
