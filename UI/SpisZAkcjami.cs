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
	class SpisZAkcjami<TRekord> : TableLayoutPanel
		where TRekord : Rekord<TRekord>
	{
		protected readonly PanelAkcji panelAkcji;
		protected readonly Wyszukiwarka wyszukiwarka;
		protected AdapterAkcji domyslnaAkcja; 
		protected readonly List<AkcjaNaSpisie<TRekord>> akcje;

		public Spis<TRekord> Spis { get; }
		public List<AkcjaNaSpisie<TRekord>> Akcje => akcje;

		public SpisZAkcjami(Spis<TRekord> spis)
		{
			akcje = new List<AkcjaNaSpisie<TRekord>>();
			panelAkcji = new PanelAkcji();
			wyszukiwarka = new Wyszukiwarka();

			Spis = spis;

			Dock = DockStyle.Fill;

			ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
			ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			RowStyles.Add(new RowStyle(SizeType.Percent, 100));

			Controls.Add(panelAkcji, 1, 0);

			panelAkcji.DodajKontrolke(wyszukiwarka);
			wyszukiwarka.TextChanged += wyszukiwarka_TextChanged;

			spis.Dock = DockStyle.Fill;
			spis.SelectionChanged += spis_SelectionChanged;
			spis.CellDoubleClick += spis_CellDoubleClick;
			spis.KeyDown += spis_KeyDown;
			Controls.Add(spis, 0, 0);
			MinimumSize = new Size(panelAkcji.MinimumSize.Width + spis.MinimumSize.Width, panelAkcji.MinimumSize.Height + spis.MinimumSize.Height);
		}

		private void wyszukiwarka_TextChanged(object sender, EventArgs e)
		{
			Spis.UstawFiltr(wyszukiwarka.Text);
		}

		private void spis_KeyDown(object sender, KeyEventArgs e)
		{
			ObsluzKlawisz(e.KeyCode, e.Modifiers);
		}

		private void spis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex != -1 && domyslnaAkcja != null) domyslnaAkcja.Uruchom();
		}

		private void spis_SelectionChanged(object sender, EventArgs e)
		{
			panelAkcji.Aktualizuj();
		}

		protected virtual void ObsluzKlawisz(Keys klawisz, Keys modyfikatory)
		{
			if (klawisz == Keys.F3 || (klawisz == Keys.F && modyfikatory == Keys.Control)) wyszukiwarka.Focus();
			else if (klawisz == Keys.F5) Spis.PrzeladujBezpiecznie();
			else panelAkcji.ObsluzKlawisz(klawisz, modyfikatory);
		}

		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			Spis.Focus();
		}
	}

	class SpisZAkcjami<TRekord, TSpis> : SpisZAkcjami<TRekord>
		where TRekord : Rekord<TRekord>
		where TSpis : Spis<TRekord>
	{
		public new TSpis Spis { get; }

		public SpisZAkcjami(TSpis spis)
			: base(spis)
		{
			Spis = spis;
		}

		protected override void OnCreateControl()
		{
			panelAkcji.CzyGlownySpis = Spis.Kontekst.Dialog == null || !Spis.Kontekst.Dialog.CzyPrzyciskiWidoczne;
			foreach (var akcja in akcje)
			{
				var adapter = akcja.UtworzAdapter(Spis);
				if (adapter.CzyDomyslna && domyslnaAkcja == null) domyslnaAkcja = adapter;
				panelAkcji.DodajAkcje(adapter);
			}
			panelAkcji.AktualizujUklad();
			base.OnCreateControl();
		}
	}
}
