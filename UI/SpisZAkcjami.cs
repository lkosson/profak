using ProFak.DB;
using System.ComponentModel;
using System.Data;

namespace ProFak.UI
{
	class SpisZAkcjami<TRekord> : TableLayoutPanel, IKontrolkaZKontekstem
		where TRekord : Rekord<TRekord>
	{
		protected readonly PanelAkcji panelAkcji;
		protected readonly Wyszukiwarka wyszukiwarka;
		protected readonly Podsumowanie podsumowanie;
		protected AdapterAkcji? domyslnaAkcja;
		protected readonly List<AkcjaNaSpisie<TRekord>> akcje;
		protected readonly List<AdapterAkcji> adapteryAkcji;

		public Spis<TRekord> Spis { get; }
		public List<AkcjaNaSpisie<TRekord>> Akcje => akcje;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Kontekst Kontekst { get => Spis.Kontekst; set => Spis.Kontekst = value; }
		public int PreferowanaSzerokosc => Spis.PreferowanaSzerokosc + Spis.Margin.Right + panelAkcji.Width + panelAkcji.Margin.Right;

		public SpisZAkcjami(Spis<TRekord> spis)
		{
			akcje = new List<AkcjaNaSpisie<TRekord>>();
			adapteryAkcji = [];
			panelAkcji = new PanelAkcji();
			wyszukiwarka = new Wyszukiwarka();
			podsumowanie = new Podsumowanie();

			Spis = spis;

			Dock = DockStyle.Fill;

			ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
			ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			RowStyles.Add(new RowStyle(SizeType.Percent, 100));

			Controls.Add(panelAkcji, 1, 0);

			panelAkcji.DodajKontrolke(wyszukiwarka);
			wyszukiwarka.TextChanged += wyszukiwarka_TextChanged;
			wyszukiwarka.KeyDown += wyszukiwarka_KeyDown;

			spis.Dock = DockStyle.Fill;
			spis.ZaznaczenieZmienione += spis_ZaznaczenieZmienione;
			spis.RekordyZmienione += spis_RekordyZmienione;
			spis.CellDoubleClick += spis_CellDoubleClick;
			spis.CellMouseDown += spis_CellMouseDown;
			spis.KeyDown += spis_KeyDown;
			Controls.Add(spis, 0, 0);
			MinimumSize = new Size(panelAkcji.MinimumSize.Width + spis.MinimumSize.Width + panelAkcji.Margin.Left + spis.Margin.Right, panelAkcji.MinimumSize.Height + spis.MinimumSize.Height + Math.Max(panelAkcji.Margin.Top, spis.Margin.Top) + Math.Max(panelAkcji.Margin.Bottom, spis.Margin.Bottom));
		}

		private void wyszukiwarka_KeyDown(object? sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				wyszukiwarka.Text = "";
				Spis.Focus();
				e.Handled = true;
			}
			else if (e.KeyCode == Keys.Enter)
			{
				if (Spis.Rekordy.Any()) Spis.Focus();
				e.Handled = true;
			}
		}

		private void wyszukiwarka_TextChanged(object? sender, EventArgs e)
		{
			Spis.UstawFiltr(wyszukiwarka.Text);
		}

		private void spis_KeyDown(object? sender, KeyEventArgs e)
		{
			e.Handled = ObsluzKlawisz(e.KeyCode, e.Modifiers);
		}

		private void spis_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex != -1 && e.ColumnIndex != -1 && domyslnaAkcja != null) domyslnaAkcja.Uruchom();
		}

		private void spis_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && e.RowIndex != -1) PokazMenuKontekstowe();
		}

		private void spis_ZaznaczenieZmienione()
		{
			podsumowanie.Text = Spis.Podsumowanie;
			panelAkcji.Aktualizuj();
		}

		private void spis_RekordyZmienione()
		{
			podsumowanie.Text = Spis.Podsumowanie;
		}

		protected virtual bool ObsluzKlawisz(Keys klawisz, Keys modyfikatory)
		{
			if (klawisz == Keys.Escape) { Dispose(); return true; }
			else if (klawisz == Keys.F3 || (klawisz == Keys.F && modyfikatory == Keys.Control)) { wyszukiwarka.Focus(); return true; }
			else if (klawisz == Keys.Home && Spis.Rekordy.FirstOrDefault() is TRekord pierwszyRekord) { Spis.WybraneRekordy = [pierwszyRekord]; return true; }
			else if (klawisz == Keys.End && Spis.Rekordy.LastOrDefault() is TRekord ostatniRekord) { Spis.WybraneRekordy = [ostatniRekord]; return true; }
			else if (klawisz == Keys.Apps || (klawisz == Keys.F10 && modyfikatory == Keys.Shift)) { PokazMenuKontekstowe(); return true; }
			else return panelAkcji.ObsluzKlawisz(klawisz, modyfikatory);
		}

		private void PokazMenuKontekstowe()
		{
			var menu = ZbudujMenuKontekstowe();
			menu.Closed += delegate
			{
				BeginInvoke(delegate { menu.Dispose(); });
			};
			menu.Show(Cursor.Position);
		}

		protected virtual ContextMenuStrip ZbudujMenuKontekstowe()
		{
			var menu = new ContextMenuStrip();
			menu.ShowImageMargin = false;
			foreach (var adapter in adapteryAkcji.OrderBy(e => e.CzyDomyslna ? 0 : 1))
			{
				if (adapter.CzyGlobalna) continue;
				if (!adapter.CzyDostepna) continue;
				var pozycja = new ToolStripMenuItem();
				pozycja.Text = adapter.NazwaBezSkrotu;
				pozycja.ShortcutKeyDisplayString = adapter.Skrot;
				pozycja.Click += delegate
				{
					adapter.Uruchom();
				};
				menu.Items.Add(pozycja);
			}
			return menu;
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			Spis.Focus();
		}

		protected override void OnCreateControl()
		{
			panelAkcji.CzyGlownySpis = Spis.Kontekst.Dialog == null || !Spis.Kontekst.Dialog.CzyPrzyciskiWidoczne;
			foreach (var akcja in akcje)
			{
				var adapter = akcja.UtworzAdapter(Spis);
				if (adapter.CzyDomyslna && domyslnaAkcja == null) domyslnaAkcja = adapter;
				panelAkcji.DodajAkcje(adapter);
				adapteryAkcji.Add(adapter);
			}
			panelAkcji.DodajKontrolke(podsumowanie);
			panelAkcji.AktualizujUklad();
			base.OnCreateControl();
		}
	}

	class SpisZAkcjami<TRekord, TSpis> : SpisZAkcjami<TRekord>
		where TRekord : Rekord<TRekord>
		where TSpis : Spis<TRekord>
	{
		public new TSpis Spis { get; }

		public SpisZAkcjami(TSpis spis, IEnumerable<AkcjaNaSpisie<TRekord>>? akcje = null)
			: base(spis)
		{
			Spis = spis;
			if (akcje != null) Akcje.AddRange(akcje);
		}
	}
}
