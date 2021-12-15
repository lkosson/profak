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
	public partial class OknoBledu : Form
	{
		public OknoBledu()
		{
			InitializeComponent();
		}

		public OknoBledu(Exception exc)
			: this()
		{
			textBoxWyjatek.Text = exc.ToString();
		}

		private void linkLabelURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			// https://github.com/lkosson/profak/issues
		}
	}
}
