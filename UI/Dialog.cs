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

		private Dialog()
		{
			InitializeComponent();
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
			var rozmiarPreferowany = zawartosc.GetPreferredSize(zawartosc.Size);
			ClientSize = new Size(rozmiarPreferowany.Width + panelZawartosc.Margin.Left + panelZawartosc.Margin.Right + Padding.Left + Padding.Right, rozmiarPreferowany.Height + buttonZapisz.Height + panelZawartosc.Margin.Top + panelZawartosc.Margin.Bottom + buttonZapisz.Margin.Top + buttonZapisz.Margin.Bottom * 2);
			panelZawartosc.Controls.Add(zawartosc);
			zawartosc.Dock = DockStyle.Fill;
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
