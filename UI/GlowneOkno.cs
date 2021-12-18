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

		private void Wyswietl(TreeNode pozycja, string[] parametry = null)
		{
			if (pozycja.Nodes.Count > 0 && parametry == null) return;

			if (pozycja.Name.StartsWith("#"))
			{
				Wyswietl(pozycja.Parent, (parametry ?? Enumerable.Empty<string>()).Append(pozycja.Name[1..]).ToArray());
				return;
			}

			/*
			foreach (Control istniejace in panelZawartosc.Controls)
			{
				if (istniejace.Name == pozycja.Name)
				{
					istniejace.BringToFront();
					istniejace.Focus();
					return;
				}
			}
			*/

			if (pozycja.Name == "JednostkiMiar") Wyswietl(Spisy.JednostkiMiar(), pozycja.Name);
			else if (pozycja.Name == "Kontrahenci") Wyswietl(Spisy.Kontrahenci(), pozycja.Name);
			else if (pozycja.Name == "SposobyPlatnosci") Wyswietl(Spisy.SposobyPlatnosci(), pozycja.Name);
			else if (pozycja.Name == "StawkiVat") Wyswietl(Spisy.StawkiVat(), pozycja.Name);
			else if (pozycja.Name == "Waluty") Wyswietl(Spisy.Waluty(), pozycja.Name);
			else if (pozycja.Name == "Towary") Wyswietl(Spisy.Towary(), pozycja.Name);
			else if (pozycja.Name == "FakturyZakupu") Wyswietl(Spisy.FakturyZakupu(parametry), pozycja.Name);
			else if (pozycja.Name == "FakturySprzedazy") Wyswietl(Spisy.FakturySprzedazy(parametry), pozycja.Name);
			else if (pozycja.Name == "Numeratory") Wyswietl(Spisy.Numeratory(), pozycja.Name);
			else if (pozycja.Name == "StanyNumeratorow") Wyswietl(Spisy.StanyNumeratorow(), pozycja.Name);
			else if (pozycja.Name == "SQL") Wyswietl(new EkranSQL(), pozycja.Name);
			else if (pozycja.Name == "Tabele") Wyswietl(new EdytorTabeli(), pozycja.Name);
			else if (pozycja.Name == "Baza") Wyswietl(new BazyDanych(), pozycja.Name);
			else if (pozycja.Name == "OProgramie") Wyswietl(new OProgramie(), pozycja.Name);
		}

		private void Wyswietl<TKontrolka>(TKontrolka kontrolka, string nazwa)
			where TKontrolka : Control, IKontrolkaZKontekstem
		{
			var doUsuniecia = panelZawartosc.Controls.Cast<Control>().ToList();

			var kontekst = new Kontekst();
			kontrolka.Kontekst = kontekst;
			kontrolka.Name = nazwa;
			kontrolka.Disposed += delegate { panelZawartosc.Controls.Remove(kontrolka); treeViewMenu.Focus(); kontekst.Dispose(); };
			kontrolka.Dock = DockStyle.Fill;
			panelZawartosc.Controls.Add(kontrolka);
			kontrolka.BringToFront();

			foreach (var istniejace in doUsuniecia)
			{
				istniejace.Dispose();
			}

			kontrolka.Focus();
		}

		private void treeViewMenu_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node.Name == "FakturySprzedazy") WypelnijDatyFakturSprzedazy(e.Node);
			else if (e.Node.Name == "FakturyZakupu") WypelnijDatyFakturZakupu(e.Node);
		}

		private void WypelnijDatyFakturSprzedazy(TreeNode treeNode)
		{
			while (treeNode.Nodes.Count > 1) treeNode.Nodes.RemoveAt(1);

			using var kontekst = new Kontekst();
			var daty = kontekst.Baza.Faktury
				.Where(faktura => faktura.Rodzaj == DB.RodzajFaktury.Sprzedaż || faktura.Rodzaj == DB.RodzajFaktury.KorektaSprzedaży || faktura.Rodzaj == DB.RodzajFaktury.Proforma)
				.Select(faktura => faktura.DataSprzedazy)
				.Distinct()
				.ToList();

			var lata = daty.OrderBy(data => data).GroupBy(data => data.Year).Select(rok => (rok.Key, miesiace: rok.Select(data => data.Month).Distinct().ToList())).ToList();
			foreach (var (rok, miesiace) in lata)
			{
				var treeNodeRok = new TreeNode { Name = "#" + rok.ToString(), Text = rok.ToString() };
				var treeNodeWszystko = new TreeNode { Name = "#" + rok.ToString(), Text = "(wszystkie)" };
				treeNodeRok.Nodes.Add(treeNodeWszystko);
				foreach (var miesiac in miesiace)
				{
					var treeNodeMiesiac = new TreeNode { Name = "#" + miesiac.ToString(), Text = new DateTime(rok, miesiac, 1).ToString("MMMM") };
					treeNodeRok.Nodes.Add(treeNodeMiesiac);
				}
				treeNode.Nodes.Add(treeNodeRok);
			}
		}

		private void WypelnijDatyFakturZakupu(TreeNode treeNode)
		{
			while (treeNode.Nodes.Count > 1) treeNode.Nodes.RemoveAt(1);

			using var kontekst = new Kontekst();
			var daty = kontekst.Baza.Faktury
				.Where(faktura => faktura.Rodzaj == DB.RodzajFaktury.Zakup || faktura.Rodzaj == DB.RodzajFaktury.KorektaZakupu)
				.Select(faktura => faktura.DataSprzedazy)
				.Distinct()
				.ToList();

			var lata = daty.OrderBy(data => data).GroupBy(data => data.Year).Select(rok => (rok.Key, miesiace: rok.Select(data => data.Month).Distinct().ToList())).ToList();
			foreach (var (rok, miesiace) in lata)
			{
				var treeNodeRok = new TreeNode { Name = "#" + rok.ToString(), Text = rok.ToString() };
				var treeNodeWszystko = new TreeNode { Name = "#" + rok.ToString(), Text = "(cały rok)" };
				treeNodeRok.Nodes.Add(treeNodeWszystko);
				foreach (var miesiac in miesiace)
				{
					var treeNodeMiesiac = new TreeNode { Name = "#" + miesiac.ToString(), Text = new DateTime(rok, miesiac, 1).ToString("MMMM") };
					treeNodeRok.Nodes.Add(treeNodeMiesiac);
				}
				treeNode.Nodes.Add(treeNodeRok);
			}
		}
	}
}
