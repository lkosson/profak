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
	partial class Dialog : Form
	{
		public Control Zawartosc { get { return panelZawartosc.Controls.Cast<Control>().FirstOrDefault(); } set { panelZawartosc.Controls.Clear(); if (value != null) UstawZawartosc(value); } }
		public bool CzyPrzyciskiWidoczne { get => flowLayoutPanelPrzyciski.Visible; set { flowLayoutPanelPrzyciski.Visible = value; CancelButton = value ? buttonAnuluj : null; } }
		public FlowLayoutPanel Przyciski => flowLayoutPanelPrzyciski;

		private Dialog()
		{
			InitializeComponent();
			ShowInTaskbar = false;
			Icon = GlowneOkno.Ikona;
		}

		public Dialog(string tytul, Control zawartosc, Kontekst kontekst)
			: this()
		{
			Text = tytul;
			Zawartosc = zawartosc;
			kontekst.Dialog = this;
		}

		private void UstawZawartosc(Control zawartosc)
		{
			panelZawartosc.Controls.Add(zawartosc);
			panelZawartosc.Size = Zawartosc.Size;
			ClientSize = tableLayoutPanelZawartosc.Size;
			MinimumSize = Size;
			Zawartosc.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
			var finalnyRozmiar = tableLayoutPanelZawartosc.Size;
			tableLayoutPanelZawartosc.AutoSize = false;
			tableLayoutPanelZawartosc.Size = finalnyRozmiar;
			tableLayoutPanelZawartosc.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Escape)
			{
				DialogResult = DialogResult.Cancel;
				Close();
			}
			else if (e.KeyCode == Keys.F10 && CzyPrzyciskiWidoczne)
			{
				e.SuppressKeyPress = true;
				DialogResult = DialogResult.OK;
				Close();
			}
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				e.Cancel = !ValidateChildren();
			}
			base.OnFormClosing(e);
		}
	}
}
