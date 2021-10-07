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
			var kontekst = new UI.Kontekst();
			var pozycja = treeViewMenu.SelectedNode;
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
			okno.MdiParent = this;
			okno.FormClosed += delegate { kontekst.Dispose(); };
			okno.Show();
		}
	}
}
