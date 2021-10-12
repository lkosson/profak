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
	public partial class GlowneOkno : Form
	{
		public GlowneOkno()
		{
			InitializeComponent();
			treeViewMenu.ExpandAll();
		}

		private void treeViewMenu_DoubleClick(object sender, EventArgs e)
		{
			WyswietlPozycje(treeViewMenu.SelectedNode);
		}

		private void treeViewMenu_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				e.Handled = true;
				WyswietlPozycje(treeViewMenu.SelectedNode);
			}
		}

		private void WyswietlPozycje(TreeNode pozycja)
		{

			foreach (var istniejace in MdiChildren)
			{
				if (istniejace.Name == pozycja.Name)
				{
					istniejace.WindowState = FormWindowState.Minimized;
					istniejace.Focus();
					istniejace.WindowState = FormWindowState.Maximized;
					return;
				}
			}

			var kontekst = new UI.Kontekst();
			Form okno = null;
			if (pozycja.Name == "JednostkiMiar") okno = Spis.JednostkiMiar(kontekst);
			else if (pozycja.Name == "Kontrahenci") okno = Spis.Kontrahenci(kontekst);
			else if (pozycja.Name == "SposobyPlatnosci") okno = Spis.SposobyPlatnosci(kontekst);
			else if (pozycja.Name == "StawkiVat") okno = Spis.StawkiVat(kontekst);
			else if (pozycja.Name == "Waluty") okno = Spis.Waluty(kontekst);
			if (okno == null)
			{
				kontekst.Dispose();
				return;
			}
			okno.Name = pozycja.Name;
			okno.MdiParent = this;
			okno.WindowState = FormWindowState.Maximized;
			okno.FormClosed += delegate { kontekst.Dispose(); };
			okno.Show();
		}
	}
}
