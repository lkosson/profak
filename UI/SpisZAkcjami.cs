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
	class SpisZAkcjami<TRekord> : TableLayoutPanel, IKontrolkaZKontekstem
		where TRekord : Rekord<TRekord>
	{
		protected readonly PanelAkcji panelAkcji;
		protected readonly Wyszukiwarka wyszukiwarka;
		protected readonly Podsumowanie podsumowanie;
		protected AdapterAkcji domyslnaAkcja; 
		protected readonly List<AkcjaNaSpisie<TRekord>> akcje;

		public Spis<TRekord> Spis { get; }
		public List<AkcjaNaSpisie<TRekord>> Akcje => akcje;

		public Kontekst Kontekst { get => Spis.Kontekst; set => Spis.Kontekst = value; }

		public SpisZAkcjami(Spis<TRekord> spis)
		{
			akcje = new List<AkcjaNaSpisie<TRekord>>();
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
			spis.KeyDown += spis_KeyDown;
			Controls.Add(spis, 0, 0);
			MinimumSize = new Size(panelAkcji.MinimumSize.Width + spis.MinimumSize.Width, panelAkcji.MinimumSize.Height + spis.MinimumSize.Height);
		}

		private void wyszukiwarka_KeyDown(object sender, KeyEventArgs e)
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

		private void wyszukiwarka_TextChanged(object sender, EventArgs e)
		{
			Spis.UstawFiltr(wyszukiwarka.Text);
		}

		private void spis_KeyDown(object sender, KeyEventArgs e)
		{
			e.Handled = ObsluzKlawisz(e.KeyCode, e.Modifiers);
		}

		private void spis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex != -1 && e.ColumnIndex != -1 && domyslnaAkcja != null) domyslnaAkcja.Uruchom();
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
			else if (klawisz == Keys.Home) { Spis.WybraneRekordy = new[] { Spis.Rekordy.FirstOrDefault() }; return true; }
			else if (klawisz == Keys.End) { Spis.WybraneRekordy = new[] { Spis.Rekordy.LastOrDefault() }; return true; }
			else return panelAkcji.ObsluzKlawisz(klawisz, modyfikatory);
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

		public SpisZAkcjami(TSpis spis, IEnumerable<AkcjaNaSpisie<TRekord>> akcje = null)
			: base(spis)
		{
			Spis = spis;
			if (akcje != null) Akcje.AddRange(akcje);
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
			panelAkcji.DodajKontrolke(podsumowanie);
			panelAkcji.AktualizujUklad();
			base.OnCreateControl();
		}
	}
}
