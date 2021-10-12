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

			foreach (Control istniejace in panelZawartosc.Controls)
			{
				if (istniejace.Name == pozycja.Name)
				{
					istniejace.BringToFront();
					istniejace.Focus();
					return;
				}
			}

			var kontekst = new UI.Kontekst();
			UserControl kontrolka = null;
			if (pozycja.Name == "JednostkiMiar") kontrolka = Spis.JednostkiMiar(kontekst);
			else if (pozycja.Name == "Kontrahenci") kontrolka = Spis.Kontrahenci(kontekst);
			else if (pozycja.Name == "SposobyPlatnosci") kontrolka = Spis.SposobyPlatnosci(kontekst);
			else if (pozycja.Name == "StawkiVat") kontrolka = Spis.StawkiVat(kontekst);
			else if (pozycja.Name == "Waluty") kontrolka = Spis.Waluty(kontekst);
			if (kontrolka == null)
			{
				kontekst.Dispose();
				return;
			}
			kontrolka.Name = pozycja.Name;
			kontrolka.Disposed += delegate { kontekst.Dispose(); };
			kontrolka.Dock = DockStyle.Fill;
			panelZawartosc.Controls.Add(kontrolka);
			kontrolka.BringToFront();
			kontrolka.Focus();
		}

		private void GlowneOkno_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 27)
			{
				var kontrolka = ActiveControl;
				if (kontrolka.Parent != panelZawartosc) return;
				e.Handled = true;
				panelZawartosc.Controls.Remove(kontrolka);
				kontrolka.Dispose();
			}
		}
	}
}
