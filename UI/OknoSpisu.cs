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
	public partial class OknoSpisu : Form
	{
		public string Tytul { get { return Text; } set { Text = value; } }
		public Control Zawartosc { get { return panelSpis.Controls.Cast<Control>().FirstOrDefault(); } set { panelSpis.Controls.Clear(); if (value != null) { panelSpis.Controls.Add(value); value.Dock = DockStyle.Fill; } } }

		public OknoSpisu()
		{
			InitializeComponent();
		}

		public OknoSpisu(string tytul, Control zawartosc)
			: this()
		{
			Tytul = tytul;
			Zawartosc = zawartosc;
		}
	}
}
