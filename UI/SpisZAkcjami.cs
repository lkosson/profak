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
	class SpisZAkcjami : TableLayoutPanel
	{
		protected readonly PanelAkcji panelAkcji;
		protected AdapterAkcji domyslnaAkcja;

		public SpisZAkcjami(Spis spis)
		{
			Dock = DockStyle.Fill;

			ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
			ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			RowStyles.Add(new RowStyle(SizeType.Percent, 100));

			panelAkcji = new PanelAkcji();
			Controls.Add(panelAkcji, 1, 0);

			spis.Dock = DockStyle.Fill;
			spis.SelectionChanged += spis_SelectionChanged;
			spis.CellDoubleClick += spis_CellDoubleClick;
			spis.KeyPress += spis_KeyPress;
			Controls.Add(spis, 0, 0);
			MinimumSize = new Size(panelAkcji.MinimumSize.Width + spis.MinimumSize.Width, panelAkcji.MinimumSize.Height + spis.MinimumSize.Height);
		}

		private void spis_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r' && domyslnaAkcja != null) domyslnaAkcja.Uruchom();
		}

		private void spis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex != -1 && domyslnaAkcja != null) domyslnaAkcja.Uruchom();
		}

		private void spis_SelectionChanged(object sender, EventArgs e)
		{
			panelAkcji.Aktualizuj();
		}

		public static SpisZAkcjami<TRekord, TSpis> Utworz<TRekord, TSpis>(TSpis spis, params AkcjaNaSpisie<TRekord>[] akcje)
			where TRekord : Rekord<TRekord>
			where TSpis : Spis<TRekord>
		{
			var okno = new SpisZAkcjami<TRekord, TSpis>(spis);
			okno.Akcje.AddRange(akcje);
			return okno;
		}
	}

	class SpisZAkcjami<TRekord> : SpisZAkcjami
		where TRekord : Rekord<TRekord>
	{
		protected readonly List<AkcjaNaSpisie<TRekord>> akcje;

		public Spis<TRekord> Spis { get; }
		public List<AkcjaNaSpisie<TRekord>> Akcje => akcje;

		public SpisZAkcjami(Spis<TRekord> spis)
			: base(spis)
		{
			Spis = spis;
			akcje = new List<AkcjaNaSpisie<TRekord>>();
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
				if (akcja.CzyDomyslna) domyslnaAkcja = adapter;
				panelAkcji.DodajAkcje(adapter);
			}
			panelAkcji.AktualizujUklad();
			base.OnCreateControl();
		}
	}
}
