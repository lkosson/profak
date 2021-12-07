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

			if (pozycja.Name == "JednostkiMiar") Wyswietl(Spisy.JednostkiMiar(), pozycja.Name);
			else if (pozycja.Name == "Kontrahenci") Wyswietl(Spisy.Kontrahenci(), pozycja.Name);
			else if (pozycja.Name == "SposobyPlatnosci") Wyswietl(Spisy.SposobyPlatnosci(), pozycja.Name);
			else if (pozycja.Name == "StawkiVat") Wyswietl(Spisy.StawkiVat(), pozycja.Name);
			else if (pozycja.Name == "Waluty") Wyswietl(Spisy.Waluty(), pozycja.Name);
			else if (pozycja.Name == "Towary") Wyswietl(Spisy.Towary(), pozycja.Name);
			else if (pozycja.Name == "FakturyZakupu") Wyswietl(Spisy.FakturyZakupu(), pozycja.Name);
			else if (pozycja.Name == "FakturySprzedazy") Wyswietl(Spisy.FakturySprzedazy(), pozycja.Name);
			else if (pozycja.Name == "PozycjeFaktur") Wyswietl(Spisy.PozycjeFaktur(), pozycja.Name);
			else if (pozycja.Name == "Wplaty") Wyswietl(Spisy.Wplaty(), pozycja.Name);
			else if (pozycja.Name == "Numeratory") Wyswietl(Spisy.Numeratory(), pozycja.Name);
			else if (pozycja.Name == "StanyNumeratorow") Wyswietl(Spisy.StanyNumeratorow(), pozycja.Name);
			else if (pozycja.Name == "SQL") Wyswietl(new EkranSQL(), pozycja.Name);
			else if (pozycja.Name == "Tabele") Wyswietl(new EdytorTabeli(), pozycja.Name);
		}

		private void Wyswietl<TKontrolka>(TKontrolka kontrolka, string nazwa)
			where TKontrolka : Control, IKontrolkaZKontekstem
		{
			var kontekst = new Kontekst();
			kontrolka.Kontekst = kontekst;
			kontrolka.Name = nazwa;
			kontrolka.Disposed += delegate { panelZawartosc.Controls.Remove(kontrolka); treeViewMenu.Focus(); kontekst.Dispose(); };
			kontrolka.Dock = DockStyle.Fill;
			panelZawartosc.Controls.Add(kontrolka);
			kontrolka.BringToFront();
			kontrolka.Focus();
		}
	}
}
