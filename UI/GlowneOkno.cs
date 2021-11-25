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
			Wyswietl(treeViewMenu.SelectedNode);
		}

		private void treeViewMenu_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				e.Handled = true;
				Wyswietl(treeViewMenu.SelectedNode);
			}
		}

		private void Wyswietl(TreeNode pozycja)
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

			if (pozycja.Name == "JednostkiMiar") Wyswietl(Spis.JednostkiMiar(), pozycja.Name);
			else if (pozycja.Name == "Kontrahenci") Wyswietl(Spis.Kontrahenci(), pozycja.Name);
			else if (pozycja.Name == "SposobyPlatnosci") Wyswietl(Spis.SposobyPlatnosci(), pozycja.Name);
			else if (pozycja.Name == "StawkiVat") Wyswietl(Spis.StawkiVat(), pozycja.Name);
			else if (pozycja.Name == "Waluty") Wyswietl(Spis.Waluty(), pozycja.Name);
			else if (pozycja.Name == "Towary") Wyswietl(Spis.Towary(), pozycja.Name);
			else if (pozycja.Name == "FakturyZakupu") Wyswietl(Spis.FakturyZakupu(), pozycja.Name);
			else if (pozycja.Name == "FakturySprzedazy") Wyswietl(Spis.FakturySprzedazy(), pozycja.Name);
			else if (pozycja.Name == "PozycjeFaktur") Wyswietl(Spis.PozycjeFaktur(), pozycja.Name);
			else if (pozycja.Name == "Wplaty") Wyswietl(Spis.Wplaty(), pozycja.Name);
			else if (pozycja.Name == "Numeratory") Wyswietl(Spis.Numeratory(), pozycja.Name);
			else if (pozycja.Name == "StanyNumeratorow") Wyswietl(Spis.StanyNumeratorow(), pozycja.Name);
		}

		private void Wyswietl<TRekord>(SpisZAkcjami<TRekord> kontrolka, string nazwa)
			where TRekord : DB.Rekord<TRekord>
		{
			var kontekst = new Kontekst();
			kontrolka.Spis.Kontekst = kontekst;
			kontrolka.Name = nazwa;
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
