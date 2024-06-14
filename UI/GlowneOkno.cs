using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	public partial class GlowneOkno : Form
	{
		private TreeNode ostatnioWybrany;

		public GlowneOkno()
		{
			InitializeComponent();
		}

		public static Icon Ikona => (Icon)new ComponentResourceManager(typeof(GlowneOkno)).GetObject("$this.Icon");

		protected override void OnLoad(EventArgs e)
		{
			menu.Nodes["Faktury"].Expand();
			menu.Nodes["Faktury"].Nodes["FakturySprzedazy"].Expand();
			menu.Nodes["Faktury"].Nodes["FakturySprzedazy"].Nodes["WedlugDaty"].Expand();
			menu.Nodes["Faktury"].Nodes["FakturySprzedazy"].Nodes["WedlugDaty"].Nodes.Cast<TreeNode>().LastOrDefault()?.Expand();
			menu.Nodes["Faktury"].Nodes["FakturySprzedazy"].Nodes["KSeFSprzedaz"].Expand();
			menu.Nodes["Faktury"].Nodes["FakturyZakupu"].Expand();
			menu.Nodes["Faktury"].Nodes["FakturyZakupu"].Nodes["WedlugDaty"].Expand();
			menu.Nodes["Faktury"].Nodes["FakturyZakupu"].Nodes["WedlugDaty"].Nodes.Cast<TreeNode>().LastOrDefault()?.Expand();
			menu.Nodes["Faktury"].Nodes["FakturyZakupu"].Nodes["KSeFZakup"].Expand();
			menu.Nodes["Slowniki"].Expand();
			menu.Nodes["Podatki"].Expand();
			var doWybrania = menu.Nodes["Faktury"].Nodes["FakturySprzedazy"].Nodes["WedlugDaty"].Nodes.Cast<TreeNode>().LastOrDefault()?.Nodes?.Cast<TreeNode>()?.LastOrDefault();
			if (doWybrania == null) doWybrania = menu.Nodes["Faktury"].Nodes["FakturySprzedazy"].Nodes["Wszystkie"];
			Wyswietl(doWybrania);
			base.OnLoad(e);
		}

		private void menu_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if ((e.Action & TreeViewAction.ByKeyboard) == TreeViewAction.ByKeyboard) return;
			var wybrany = menu.SelectedNode;
			if (wybrany == null) return;
			if (ostatnioWybrany == null || !ostatnioWybrany.FullPath.StartsWith(wybrany.FullPath)) while (wybrany.Nodes.Count > 0) wybrany = wybrany.Nodes[0];
			if (menu.SelectedNode == wybrany) Wyswietl(wybrany);
			else menu.SelectedNode = wybrany;
		}

		private void menu_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				e.Handled = true;
				Wyswietl(menu.SelectedNode);
			}
		}

		private void Wyswietl(TreeNode pozycja, string[] parametry = null)
		{
			if (parametry == null)
			{
				if (ostatnioWybrany != null)
				{
					ostatnioWybrany.BackColor = Color.Empty;
					ostatnioWybrany.ForeColor = Color.Empty;
				}
				ostatnioWybrany = pozycja;
				pozycja.BackColor = SystemColors.Highlight;
				pozycja.ForeColor = SystemColors.HighlightText;
			}

			if (pozycja.Name == "JednostkiMiar") Wyswietl(Spisy.JednostkiMiar(), pozycja.Name);
			else if (pozycja.Name == "Kontrahenci") Wyswietl(Spisy.Kontrahenci(), pozycja.Name);
			else if (pozycja.Name == "SposobyPlatnosci") Wyswietl(Spisy.SposobyPlatnosci(), pozycja.Name);
			else if (pozycja.Name == "StawkiVat") Wyswietl(Spisy.StawkiVat(), pozycja.Name);
			else if (pozycja.Name == "Waluty") Wyswietl(Spisy.Waluty(), pozycja.Name);
			else if (pozycja.Name == "Towary") Wyswietl(Spisy.Towary(), pozycja.Name);
			else if (pozycja.Name == "DeklaracjeVat") Wyswietl(Spisy.DeklaracjeVat(parametry), pozycja.Name);
			else if (pozycja.Name == "SkladkiZus") Wyswietl(Spisy.SkladkiZus(parametry), pozycja.Name);
			else if (pozycja.Name == "ZaliczkiPit") Wyswietl(Spisy.ZaliczkiPit(parametry), pozycja.Name);
			else if (pozycja.Name == "UrzedySkarbowe") Wyswietl(Spisy.UrzedySkarbowe(), pozycja.Name);
			else if (pozycja.Name == "FakturyZakupu") Wyswietl(Spisy.FakturyZakupu(parametry), pozycja.Name);
			else if (pozycja.Name == "KSeFZakup") Wyswietl(Spisy.KSeFZakup(parametry), pozycja.Name);
			else if (pozycja.Name == "KSeFSprzedaz") Wyswietl(Spisy.KSeFSprzedaz(parametry), pozycja.Name);
			else if (pozycja.Name == "FakturySprzedazy") Wyswietl(Spisy.FakturySprzedazy(parametry), pozycja.Name);
			else if (pozycja.Name == "Konfiguracja") KonfiguracjaEdytor.Wyswietl();
			else if (pozycja.Name == "Numeratory") Wyswietl(Spisy.Numeratory(), pozycja.Name);
			else if (pozycja.Name == "StanyNumeratorow") Wyswietl(Spisy.StanyNumeratorow(), pozycja.Name);
			else if (pozycja.Name == "SQL") Wyswietl(new EkranSQL(), pozycja.Name);
			else if (pozycja.Name == "Tabele") Wyswietl(new EdytorTabeli(), pozycja.Name);
			else if (pozycja.Name == "Baza") Wyswietl(new BazyDanych(), pozycja.Name);
			else if (pozycja.Name == "OProgramie") Wyswietl(new OProgramie(), pozycja.Name);
			else if (pozycja.Parent != null) Wyswietl(pozycja.Parent, (parametry ?? Enumerable.Empty<string>()).Append(pozycja.Name).ToArray());
		}

		private void Wyswietl<TKontrolka>(TKontrolka kontrolka, string nazwa)
			where TKontrolka : Control, IKontrolkaZKontekstem
		{
			var doUsuniecia = panelZawartosc.Controls.Cast<Control>().ToList();

			var kontekst = new Kontekst();
			kontrolka.Kontekst = kontekst;
			kontrolka.Name = nazwa;
			kontrolka.Disposed += delegate { panelZawartosc.Controls.Remove(kontrolka); menu.Focus(); kontekst.Dispose(); };
			kontrolka.Dock = DockStyle.Fill;
			panelZawartosc.Controls.Add(kontrolka);
			kontrolka.BringToFront();

			if (ModifierKeys != Keys.Control)
			{
				foreach (var istniejace in doUsuniecia)
				{
					istniejace.Dispose();
				}
			}

			kontrolka.Focus();
		}

		private void menu_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node.Name == "WedlugDaty" && e.Node.Parent.Name == "FakturySprzedazy") WypelnijDatyFaktur(e.Node, faktura => faktura.Rodzaj == RodzajFaktury.Sprzedaż || faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży || faktura.Rodzaj == RodzajFaktury.Proforma);
			else if (e.Node.Name == "WedlugDaty" && e.Node.Parent.Name == "FakturyZakupu") WypelnijDatyFaktur(e.Node, faktura => faktura.Rodzaj == RodzajFaktury.Zakup || faktura.Rodzaj == RodzajFaktury.KorektaZakupu);
			else if (e.Node.Name == "WedlugKontrahenta" && e.Node.Parent.Name == "FakturySprzedazy") WypelnijKontrahentowFaktur(e.Node, faktura => faktura.Rodzaj == RodzajFaktury.Sprzedaż || faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży || faktura.Rodzaj == RodzajFaktury.Proforma, faktura => faktura.Nabywca);
			else if (e.Node.Name == "WedlugKontrahenta" && e.Node.Parent.Name == "FakturyZakupu") WypelnijKontrahentowFaktur(e.Node, faktura => faktura.Rodzaj == RodzajFaktury.Zakup || faktura.Rodzaj == RodzajFaktury.KorektaZakupu, faktura => faktura.Sprzedawca);
			else if (e.Node.Name == "WedlugTowaru" && e.Node.Parent.Name == "FakturySprzedazy") WypelnijTowaryFaktur(e.Node, pozycja => pozycja.Faktura.Rodzaj == RodzajFaktury.Sprzedaż || pozycja.Faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży || pozycja.Faktura.Rodzaj == RodzajFaktury.Proforma);
			else if (e.Node.Name == "WedlugTowaru" && e.Node.Parent.Name == "FakturyZakupu") WypelnijTowaryFaktur(e.Node, pozycja => pozycja.Faktura.Rodzaj == RodzajFaktury.Zakup || pozycja.Faktura.Rodzaj == RodzajFaktury.KorektaZakupu);
			else if (e.Node.Name == "DeklaracjeVat") WypelnijDeklaracjeVat(e.Node);
			else if (e.Node.Name == "SkladkiZus") WypelnijSkladkiZus(e.Node);
			else if (e.Node.Name == "ZaliczkiPit") WypelnijZaliczkiPit(e.Node);
		}

		private void WypelnijDatyFaktur(TreeNode treeNode, Expression<Func<Faktura, bool>> warunek)
		{
			using var kontekst = new Kontekst();
			var daty = kontekst.Baza.Faktury
				.Where(warunek)
				.Select(faktura => faktura.DataSprzedazy)
				.Distinct()
				.ToList();

			while (treeNode.Nodes.Count > 0) treeNode.Nodes.RemoveAt(0);

			var lata = daty.OrderBy(data => data).GroupBy(data => data.Year).Select(rok => (rok.Key, miesiace: rok.Select(data => data.Month).Distinct().ToList())).ToList();
			foreach (var (rok, miesiace) in lata)
			{
				var treeNodeRok = new TreeNode { Name = "R:" + rok, Text = rok.ToString() };
				var treeNodeWszystko = new TreeNode { Name = "R:" + rok, Text = "(cały rok)" };
				treeNodeRok.Nodes.Add(treeNodeWszystko);
				foreach (var miesiac in miesiace)
				{
					var treeNodeMiesiac = new TreeNode { Name = "M:" + miesiac, Text = new DateTime(rok, miesiac, 1).ToString("MMMM") };
					treeNodeRok.Nodes.Add(treeNodeMiesiac);
				}
				treeNode.Nodes.Add(treeNodeRok);
			}
		}

		private void WypelnijKontrahentowFaktur(TreeNode treeNode, Expression<Func<Faktura, bool>> warunek, Expression<Func<Faktura, Kontrahent>> pole)
		{
			using var kontekst = new Kontekst();
			var kontrahenci = kontekst.Baza.Faktury
				.Where(warunek)
				.Include(pole)
				.Select(pole)
				.Distinct()
				.Where(kontrahent => !kontrahent.CzyArchiwalny)
				.OrderBy(kontrahent => kontrahent.Nazwa)
				.ToList();

			while (treeNode.Nodes.Count > 0) treeNode.Nodes.RemoveAt(0);

			foreach (var kontrahent in kontrahenci)
			{
				treeNode.Nodes.Add(new TreeNode { Name = "K:" + kontrahent.Id, Text = kontrahent.Nazwa.ToString() });
			}
		}

		private void WypelnijTowaryFaktur(TreeNode treeNode, Expression<Func<PozycjaFaktury, bool>> warunek)
		{
			using var kontekst = new Kontekst();
			var towary = kontekst.Baza.PozycjeFaktur
				.Where(warunek)
				.Include(pozycja => pozycja.Towar)
				.Select(pozycja => pozycja.Towar)
				.Distinct()
				.Where(towar => !towar.CzyArchiwalny)
				.OrderBy(towar => towar.Nazwa)
				.ToList();

			while (treeNode.Nodes.Count > 0) treeNode.Nodes.RemoveAt(0);

			foreach (var towar in towary)
			{
				treeNode.Nodes.Add(new TreeNode { Name = "T:" + towar.Id, Text = towar.Nazwa.ToString() });
			}
		}

		private void WypelnijDeklaracjeVat(TreeNode treeNode)
		{
			using var kontekst = new Kontekst();
			var daty = kontekst.Baza.DeklaracjeVat
				.Select(faktura => faktura.Miesiac)
				.Distinct()
				.ToList();

			while (treeNode.Nodes.Count > 0) treeNode.Nodes.RemoveAt(0);

			var lata = daty.Select(data => data.Year).Distinct().OrderBy(rok => rok).ToList();
			var treeNodeWszystko = new TreeNode { Name = "", Text = "(wszystkie)" };
			treeNode.Nodes.Add(treeNodeWszystko);
			foreach (var rok in lata)
			{
				var treeNodeRok = new TreeNode { Name = "R:" + rok, Text = rok.ToString() };
				treeNode.Nodes.Add(treeNodeRok);
			}
		}

		private void WypelnijSkladkiZus(TreeNode treeNode)
		{
			using var kontekst = new Kontekst();
			var daty = kontekst.Baza.SkladkiZus
				.Select(faktura => faktura.Miesiac)
				.Distinct()
				.ToList();

			while (treeNode.Nodes.Count > 0) treeNode.Nodes.RemoveAt(0);

			var lata = daty.Select(data => data.Year).Distinct().OrderBy(rok => rok).ToList();
			var treeNodeWszystko = new TreeNode { Name = "", Text = "(wszystkie)" };
			treeNode.Nodes.Add(treeNodeWszystko);
			foreach (var rok in lata)
			{
				var treeNodeRok = new TreeNode { Name = "R:" + rok, Text = rok.ToString() };
				treeNode.Nodes.Add(treeNodeRok);
			}
		}

		private void WypelnijZaliczkiPit(TreeNode treeNode)
		{
			using var kontekst = new Kontekst();
			var daty = kontekst.Baza.ZaliczkiPit
				.Select(faktura => faktura.Miesiac)
				.Distinct()
				.ToList();

			while (treeNode.Nodes.Count > 0) treeNode.Nodes.RemoveAt(0);

			var lata = daty.Select(data => data.Year).Distinct().OrderBy(rok => rok).ToList();
			var treeNodeWszystko = new TreeNode { Name = "", Text = "(wszystkie)" };
			treeNode.Nodes.Add(treeNodeWszystko);
			foreach (var rok in lata)
			{
				var treeNodeRok = new TreeNode { Name = "R:" + rok, Text = rok.ToString() };
				treeNode.Nodes.Add(treeNodeRok);
			}
		}
	}
}
