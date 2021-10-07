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
	partial class OknoEdycji : Form
	{
		public string Tytul { get { return Text; } set { Text = value; } }
		public Control Zawartosc { get { return panelZawartosc.Controls.Cast<Control>().FirstOrDefault(); } set { panelZawartosc.Controls.Clear(); if (value != null) UstawZawartosc(value); } }

		public OknoEdycji()
		{
			InitializeComponent();
		}

		public OknoEdycji(string tytul, Control zawartosc)
			: this()
		{
			Tytul = tytul;
			Zawartosc = zawartosc;
		}

		private void UstawZawartosc(Control zawartosc)
		{
			var rozmiarPreferowany = zawartosc.GetPreferredSize(zawartosc.Size);
			ClientSize = new Size(rozmiarPreferowany.Width + panelZawartosc.Margin.Left + panelZawartosc.Margin.Right + Padding.Left + Padding.Right, rozmiarPreferowany.Height + buttonZapisz.Height + panelZawartosc.Margin.Top + panelZawartosc.Margin.Bottom + buttonZapisz.Margin.Top + buttonZapisz.Margin.Bottom * 2);
			panelZawartosc.Controls.Add(zawartosc);
			zawartosc.Dock = DockStyle.Fill;
		}
	}
}
