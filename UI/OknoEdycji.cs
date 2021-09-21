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
		public Control Zawartosc { get { return panelZawartosc.Controls.Cast<Control>().FirstOrDefault(); } set { panelZawartosc.Controls.Clear(); if (value != null) panelZawartosc.Controls.Add(value); } }

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
	}
}
