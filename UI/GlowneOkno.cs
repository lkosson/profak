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
		private TreeNode? ostatnioWybrany;
		private bool trwaAktualizacjaMenu;
		private bool menuGotowe;

		public GlowneOkno()
		{
			InitializeComponent();
			ZbudujMenu();
			panelMenu.Width = Wyglad.SzerokoscMenu;
		}

		public static Icon? Ikona => (Icon?)new ComponentResourceManager(typeof(GlowneOkno)).GetObject("$this.Icon");

		protected override void OnLoad(EventArgs e)
		{
			RozwinMenu();
			menuGotowe = true;
			base.OnLoad(e);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing && Wyglad.PotwierdzanieZamknieciaProgramu)
			{
				if (MessageBox.Show("Czy na pewno chcesz zamknąć program?", "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
				{
					e.Cancel = true;
				}
			}
			base.OnFormClosing(e);
		}

		private void ZbudujMenu()
		{
			TreeNode Wezel(string tekst, string? nazwa = null, TreeNode[]? podrzedne = null)
			{
				var wezel = new TreeNode(tekst);
				if (!String.IsNullOrEmpty(nazwa)) wezel.Name = nazwa;
				if (podrzedne != null) wezel.Nodes.AddRange(podrzedne);
				return wezel;
			}

			TreeNode Ladowanie() => Wezel("(ładowanie)");

			var fakturySprzedazyWszystkie = Wezel("Wszystkie", "Wszystkie");
			var fakturySprzedazyDoZaplaty = Wezel("Do zapłaty", "DoZaplaty");
			var fakturySprzedazyZaplacone = Wezel("Zapłacone", "Zaplacone");
			var fakturySprzedazyKSeFDzis = Wezel("Dzisiejsze", "Dzis");
			var fakturySprzedazyKSeFMiesiac = Wezel("Z tego miesiąca", "Miesiac");
			var fakturySprzedazyKSeFPoprzedni = Wezel("Z tego i poprzedniego miesiąca", "Poprzedni");
			var fakturySprzedazyKSeFRok = Wezel("Z tego roku", "Rok");
			var fakturySprzedazyKSeFWszystkie = Wezel("Wszystkie");
			var fakturySprzedazyKSeF = Wezel("KSeF", "KSeFSprzedaz", [fakturySprzedazyKSeFDzis, fakturySprzedazyKSeFMiesiac, fakturySprzedazyKSeFPoprzedni, fakturySprzedazyKSeFRok, fakturySprzedazyKSeFWszystkie]);
			var fakturySprzedazyWedlugDaty = Wezel("Według daty", "WedlugDaty", [Ladowanie()]);
			var fakturySprzedazyWedlugNabywcy = Wezel("Według nabywcy", "WedlugKontrahenta", [Ladowanie()]);
			var fakturySprzedazyWedlugTowaru = Wezel("Według towaru", "WedlugTowaru", [Ladowanie()]);
			var fakturySprzedazy = Wezel("Faktury sprzedaży", "FakturySprzedazy", [fakturySprzedazyWszystkie, fakturySprzedazyDoZaplaty, fakturySprzedazyZaplacone, fakturySprzedazyKSeF, fakturySprzedazyWedlugDaty, fakturySprzedazyWedlugNabywcy, fakturySprzedazyWedlugTowaru]);
			var fakturyProformaWszystkie = Wezel("Wszystkie", "Wszystkie");
			var fakturyProformaDoZaplaty = Wezel("Do zapłaty", "DoZaplaty");
			var fakturyProformaZaplacone = Wezel("Zapłacone", "Zaplacone");
			var fakturyProformaWedlugDaty = Wezel("Według daty", "WedlugDaty", [Ladowanie()]);
			var fakturyProformaWedlugNabywcy = Wezel("Według nabywcy", "WedlugKontrahenta", [Ladowanie()]);
			var fakturyProformaWedlugTowaru = Wezel("Według towaru", "WedlugTowaru", [Ladowanie()]);
			var fakturyProforma = Wezel("Faktury proforma", "FakturyProforma", [fakturyProformaWszystkie, fakturyProformaDoZaplaty, fakturyProformaZaplacone, fakturyProformaWedlugDaty, fakturyProformaWedlugNabywcy, fakturyProformaWedlugTowaru]);
			var fakturyZakupuWszystkie = Wezel("Wszystkie", "Wszystkie");
			var fakturyZakupuDoZaplaty = Wezel("Do zapłaty", "DoZaplaty");
			var fakturyZakupuZaplacone = Wezel("Zapłacone", "Zaplacone");
			var fakturyZakupuKSeFPrzyrostowo = Wezel("Przyrostowo", "Przyrostowo");
			var fakturyZakupuKSeFDzis = Wezel("Dzisiejsze", "Dzis");
			var fakturyZakupuKSeFWczoraj = Wezel("Od wczoraj", "Wczoraj");
			var fakturyZakupuKSeFMiesiac = Wezel("Z tego miesiąca", "Miesiac");
			var fakturyZakupuKSeFPoprzedni = Wezel("Z tego i poprzedniego miesiąca", "Poprzedni");
			var fakturyZakupuKSeFRok = Wezel("Z tego roku", "Rok");
			var fakturyZakupuKSeFWszystkie = Wezel("Wszystkie");
			var fakturyZakupuKSeF = Wezel("KSeF", "KSeFZakup", [fakturyZakupuKSeFPrzyrostowo, fakturyZakupuKSeFDzis, fakturyZakupuKSeFWczoraj, fakturyZakupuKSeFMiesiac, fakturyZakupuKSeFPoprzedni, fakturyZakupuKSeFRok, fakturyZakupuKSeFWszystkie]);
			var fakturyZakupuWedlugDaty = Wezel("Według daty", "WedlugDaty", [Ladowanie()]);
			var fakturyZakupuWedlugSprzedawcy = Wezel("Według sprzedawcy", "WedlugKontrahenta", [Ladowanie()]);
			var fakturyZakupuWedlugTowaru = Wezel("Według towaru", "WedlugTowaru", [Ladowanie()]);
			var fakturyZakupu = Wezel("Faktury zakupu", "FakturyZakupu", [fakturyZakupuWszystkie, fakturyZakupuDoZaplaty, fakturyZakupuZaplacone, fakturyZakupuKSeF, fakturyZakupuWedlugDaty, fakturyZakupuWedlugSprzedawcy, fakturyZakupuWedlugTowaru]);
			var faktury = Wezel("Faktury", "Faktury", [fakturySprzedazy, fakturyProforma, fakturyZakupu]);
			var deklaracjeVat = Wezel("Deklaracje Vat", "DeklaracjeVat", [Ladowanie()]);
			var skladkiZus = Wezel("Składki Zus", "SkladkiZus", [Ladowanie()]);
			var zaliczkiPit = Wezel("Zaliczki Pit", "ZaliczkiPit", [Ladowanie()]);
			var podatki = Wezel("Podatki", "Podatki", [deklaracjeVat, skladkiZus, zaliczkiPit]);
			var kontrahenci = Wezel("Kontrahenci", "Kontrahenci");
			var towary = Wezel("Towary", "Towary");
			var jednostkiMiar = Wezel("Jednostki miar", "JednostkiMiar");
			var sposobyPlatnosci = Wezel("Sposoby płatności", "SposobyPlatnosci");
			var stawkiVat = Wezel("Stawki VAT", "StawkiVat");
			var urzedySkarbowe = Wezel("Urzędy skarbowe", "UrzedySkarbowe");
			var waluty = Wezel("Waluty", "Waluty");
			var slowniki = Wezel("Słowniki", "Slowniki", [jednostkiMiar, sposobyPlatnosci, stawkiVat, urzedySkarbowe, waluty]);
			var numeracja = Wezel("Numeracja", "Numeratory");
			var konfiguracja = Wezel("Konfiguracja", "Konfiguracja");
#if !SQLSERVER
			var bazaDanych = Wezel("Baza danych", "Baza");
#endif
			var usunieteFaktury = Wezel("Usunięte faktury", "FakturyUsuniete");
			var polecenieSQL = Wezel("Polecenie SQL", "SQL");
			var bezposredniaEdycja = Wezel("Bezpośrednia edycja", "Tabele");
			var oProgramie = Wezel("O programie", "OProgramie");
#if SQLSERVER
			var serwisowe = Wezel("Serwisowe", "Serwisowe", [numeracja, konfiguracja, usunieteFaktury, polecenieSQL, bezposredniaEdycja, oProgramie]);
#else
			var serwisowe = Wezel("Serwisowe", "Serwisowe", [numeracja, konfiguracja, bazaDanych, usunieteFaktury, polecenieSQL, bezposredniaEdycja, oProgramie]);
#endif
			menu.Nodes.Clear();
			menu.Nodes.AddRange([faktury, podatki, kontrahenci, towary, slowniki, serwisowe]);
		}

		private void PokazMenuKontekstowe(TreeNode wezel)
		{
			var menuKontekstowe = new ContextMenuStrip();
			var menuPokaz = new ToolStripMenuItem("Pokaż");
			var menuUkryj = new ToolStripMenuItem("Ukryj");
			var menuPokazUkryte = new ToolStripMenuItem("Pokaż ukryte");
			menuPokaz.Click += delegate
			{
				ZapiszStanPozycji(wezel, ukryta: false);
				wezel.ForeColor = SystemColors.ControlText;
			};
			menuUkryj.Click += delegate
			{
				ZapiszStanPozycji(wezel, ukryta: true);
				if (wezel.Parent == null) menu.Nodes.Remove(wezel);
				else wezel.Parent.Nodes.Remove(wezel);
			};
			menuPokazUkryte.Click += delegate
			{
				ZbudujMenu();
				RozwinMenu(pokazUkryte: true);
			};
			if (wezel.ForeColor == SystemColors.GrayText) menuKontekstowe.Items.Add(menuPokaz);
			else menuKontekstowe.Items.Add(menuUkryj);
			menuKontekstowe.Items.Add(menuPokazUkryte);
			menuKontekstowe.Closed += delegate
			{
				BeginInvoke(delegate { menuKontekstowe.Dispose(); });
			};
			menuKontekstowe.Show(Cursor.Position);
		}

		private void RozwinMenu(bool pokazUkryte = false)
		{
			using var kontekst = new Kontekst();
			var stany = kontekst.Baza.StanyMenu.ToList();
			if (stany.Count == 0)
			{
				RozwinMenuDomyslne();
				return;
			}

			var stanyWedlugNazwy = stany.ToDictionary(stan => stan.Pozycja);
			TreeNode? doWyswietlenia = null;
			RozwinMenu(menu.Nodes.Cast<TreeNode>(), stanyWedlugNazwy, pokazUkryte, ref doWyswietlenia);

			if (doWyswietlenia == null) WyswietlDomyslny();
			else menu.SelectedNode = doWyswietlenia;
		}

		private void RozwinMenu(IEnumerable<TreeNode> wezly, Dictionary<string, StanMenu> stany, bool pokazUkryte, ref TreeNode? wybrany)
		{
			var doUsuniecia = new List<TreeNode>();
			foreach (var wezel in wezly)
			{
				if (stany.TryGetValue(wezel.FullPath, out var stan))
				{
					if (!stan.CzyZwinieta) wezel.Expand();
					if (stan.CzyAktywna) wybrany = wezel;
					if (stan.CzyUkryta) doUsuniecia.Add(wezel);
				}

				RozwinMenu(wezel.Nodes.Cast<TreeNode>(), stany, pokazUkryte, ref wybrany);
			}

			foreach (var wezel in doUsuniecia)
			{
				if (pokazUkryte) wezel.ForeColor = SystemColors.GrayText;
				else wezel.Remove();
			}
		}

		private TreeNode? WezelMenu(string pierwszy, params string[] sciezka)
		{
			var wezel = menu.Nodes[pierwszy];
			if (wezel == null) return null;
			foreach (var nazwa in sciezka)
			{
				if (nazwa == "*") wezel = wezel.Nodes.Cast<TreeNode>().LastOrDefault();
				else wezel = wezel.Nodes[nazwa];
				if (wezel == null) return null;
			}
			return wezel;
		}

		private void RozwinMenuDomyslne()
		{
			WezelMenu("Faktury")?.Expand();
			WezelMenu("Faktury", "FakturySprzedazy")?.Expand();
			WezelMenu("Faktury", "FakturySprzedazy", "WedlugDaty")?.Expand();
			WezelMenu("Faktury", "FakturySprzedazy", "WedlugDaty", "*")?.Expand();
			WezelMenu("Faktury", "FakturySprzedazy", "KSeFSprzedaz")?.Expand();
			WezelMenu("Faktury", "FakturyProforma")?.Expand();
			WezelMenu("Faktury", "FakturyZakupu")?.Expand();
			WezelMenu("Faktury", "FakturyZakupu", "WedlugDaty")?.Expand();
			WezelMenu("Faktury", "FakturyZakupu", "WedlugDaty", "*")?.Expand();
			WezelMenu("Faktury", "FakturyZakupu", "KSeFZakup")?.Expand();
			WezelMenu("Slowniki")?.Expand();
			WezelMenu("Podatki")?.Expand();
			WyswietlDomyslny();
		}

		private void WyswietlDomyslny()
		{
			var doWybrania = WezelMenu("Faktury", "FakturySprzedazy", "WedlugDaty", "*", "*");
			if (doWybrania == null) doWybrania = WezelMenu("Faktury", "FakturySprzedazy", "Wszystkie");
			Wyswietl(doWybrania);
		}

		private void menu_AfterSelect(object? sender, TreeViewEventArgs e)
		{
			if (trwaAktualizacjaMenu) return;
			if ((e.Action & TreeViewAction.ByKeyboard) == TreeViewAction.ByKeyboard) return;
			var wybrany = menu.SelectedNode;
			if (wybrany == null) return;
			if (ostatnioWybrany == null || ostatnioWybrany.TreeView == null || !ostatnioWybrany.FullPath.StartsWith(wybrany.FullPath)) while (wybrany.Nodes.Count > 0) wybrany = wybrany.Nodes[0];
			if (menu.SelectedNode == wybrany) Wyswietl(wybrany);
			else menu.SelectedNode = wybrany;

			ZapiszStanPozycji(menu.SelectedNode /* dla spisu według kontrahentów/towarów/dat mogło się zmienić */);
		}

		private void menu_KeyPress(object? sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				e.Handled = true;
				Wyswietl(menu.SelectedNode);
			}
			else if (e.KeyChar == '/')
			{
				e.Handled = true;
				menu.SelectedNode?.Collapse(false);
			}
		}

		private void Wyswietl(TreeNode? pozycja, string[]? parametry = null)
		{
			if (pozycja == null) return;
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
			else if (pozycja.Name == "FakturyProforma") Wyswietl(Spisy.FakturyProforma(parametry), pozycja.Name);
			else if (pozycja.Name == "Konfiguracja") KonfiguracjaEdytor.Wyswietl();
			else if (pozycja.Name == "Numeratory") Wyswietl(Spisy.Numeratory(), pozycja.Name);
			else if (pozycja.Name == "StanyNumeratorow") Wyswietl(Spisy.StanyNumeratorow(), pozycja.Name);
			else if (pozycja.Name == "SQL") Wyswietl(new EkranSQL(), pozycja.Name);
			else if (pozycja.Name == "Tabele") Wyswietl(new EdytorTabeli(), pozycja.Name);
			else if (pozycja.Name == "Baza") Wyswietl(new BazyDanych(), pozycja.Name);
			else if (pozycja.Name == "OProgramie") Wyswietl(new OProgramie(), pozycja.Name);
			else if (pozycja.Name == "FakturyUsuniete") Wyswietl(Spisy.FakturyUsuniete(), pozycja.Name);
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

		private void menu_BeforeExpand(object? sender, TreeViewCancelEventArgs e)
		{
			if (e.Node == null) return;
			if (trwaAktualizacjaMenu) return;
			trwaAktualizacjaMenu = true;
			try
			{				
				if (e.Node.Name == "WedlugDaty" && e.Node.Parent?.Name == "FakturySprzedazy") WypelnijDatyFaktur(e.Node, faktura => faktura.Rodzaj == RodzajFaktury.Sprzedaż || faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży);
				else if (e.Node.Name == "WedlugDaty" && e.Node.Parent?.Name == "FakturyProforma") WypelnijDatyFaktur(e.Node, faktura => faktura.Rodzaj == RodzajFaktury.Proforma);
				else if (e.Node.Name == "WedlugDaty" && e.Node.Parent?.Name == "FakturyZakupu") WypelnijDatyFaktur(e.Node, faktura => faktura.Rodzaj == RodzajFaktury.Zakup || faktura.Rodzaj == RodzajFaktury.KorektaZakupu || faktura.Rodzaj == RodzajFaktury.DowódWewnętrzny);
				else if (e.Node.Name == "WedlugDaty" && e.Node.Parent?.Name == "FakturyVatMarza") WypelnijDatyFaktur(e.Node, faktura => faktura.Rodzaj == RodzajFaktury.VatMarża || faktura.Rodzaj == RodzajFaktury.KorektaVatMarży);
				else if (e.Node.Name == "WedlugKontrahenta" && e.Node.Parent?.Name == "FakturySprzedazy") WypelnijKontrahentowFaktur(e.Node, faktura => faktura.Rodzaj == RodzajFaktury.Sprzedaż || faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży, faktura => faktura.Nabywca!);
				else if (e.Node.Name == "WedlugKontrahenta" && e.Node.Parent?.Name == "FakturyProforma") WypelnijKontrahentowFaktur(e.Node, faktura => faktura.Rodzaj == RodzajFaktury.Proforma, faktura => faktura.Nabywca!);
				else if (e.Node.Name == "WedlugKontrahenta" && e.Node.Parent?.Name == "FakturyZakupu") WypelnijKontrahentowFaktur(e.Node, faktura => faktura.Rodzaj == RodzajFaktury.Zakup || faktura.Rodzaj == RodzajFaktury.KorektaZakupu || faktura.Rodzaj == RodzajFaktury.DowódWewnętrzny, faktura => faktura.Sprzedawca!);
				else if (e.Node.Name == "WedlugKontrahenta" && e.Node.Parent?.Name == "FakturyVatMarza") WypelnijKontrahentowFaktur(e.Node, faktura => faktura.Rodzaj == RodzajFaktury.VatMarża || faktura.Rodzaj == RodzajFaktury.KorektaVatMarży, faktura => faktura.Nabywca!);
				else if (e.Node.Name == "WedlugTowaru" && e.Node.Parent?.Name == "FakturySprzedazy") WypelnijTowaryFaktur(e.Node, pozycja => pozycja.Faktura!.Rodzaj == RodzajFaktury.Sprzedaż || pozycja.Faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży);
				else if (e.Node.Name == "WedlugTowaru" && e.Node.Parent?.Name == "FakturyProforma") WypelnijTowaryFaktur(e.Node, pozycja => pozycja.Faktura!.Rodzaj == RodzajFaktury.Proforma);
				else if (e.Node.Name == "WedlugTowaru" && e.Node.Parent?.Name == "FakturyZakupu") WypelnijTowaryFaktur(e.Node, pozycja => pozycja.Faktura!.Rodzaj == RodzajFaktury.Zakup || pozycja.Faktura.Rodzaj == RodzajFaktury.KorektaZakupu || pozycja.Faktura.Rodzaj == RodzajFaktury.DowódWewnętrzny);
				else if (e.Node.Name == "WedlugTowaru" && e.Node.Parent?.Name == "FakturyVatMarza") WypelnijTowaryFaktur(e.Node, pozycja => pozycja.Faktura!.Rodzaj == RodzajFaktury.VatMarża || pozycja.Faktura.Rodzaj == RodzajFaktury.KorektaVatMarży);
				else if (e.Node.Name == "DeklaracjeVat") WypelnijDeklaracjeVat(e.Node);
				else if (e.Node.Name == "SkladkiZus") WypelnijSkladkiZus(e.Node);
				else if (e.Node.Name == "ZaliczkiPit") WypelnijZaliczkiPit(e.Node);
			}
			finally
			{
				trwaAktualizacjaMenu = false;
			}
		}

		private void menu_AfterExpand(object? sender, TreeViewEventArgs e)
		{
			ZapiszStanPozycji(e.Node);
		}

		private void menu_AfterCollapse(object? sender, TreeViewEventArgs e)
		{
			ZapiszStanPozycji(e.Node);
		}

		private void ZapiszStanPozycji(TreeNode? treeNode, bool ukryta = false)
		{
			if (!menuGotowe) return;
			if (treeNode == null) return;
			if (String.IsNullOrEmpty(treeNode.Name)) return;
			using var kontekst = new Kontekst();
			if (kontekst.Baza.CzyZablokowana()) return;
			using var transakcja = kontekst.Transakcja();
			var stan = kontekst.Baza.StanyMenu.FirstOrDefault(e => e.Pozycja == treeNode.FullPath);
			if (stan == null) stan = new StanMenu { Pozycja = treeNode.FullPath };
			stan.CzyZwinieta = !treeNode.IsExpanded;
			stan.CzyAktywna = treeNode.IsSelected;
			stan.CzyUkryta = ukryta || treeNode.ForeColor == SystemColors.GrayText;
			if (stan.CzyAktywna)
			{
				var aktywne = kontekst.Baza.StanyMenu.Where(e => e.CzyAktywna).ToList();
				foreach (var aktywna in aktywne)
				{
					aktywna.CzyAktywna = false;
					kontekst.Baza.Zapisz(aktywna);
				}
			}
			kontekst.Baza.Zapisz(stan);
			transakcja.Zatwierdz();
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

			var lata = daty.Append(DateTime.Now.Date).OrderBy(data => data).GroupBy(data => data.Year).Select(rok => (rok.Key, miesiace: rok.Select(data => data.Month).Distinct().ToList())).ToList();
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
				.Select(pozycja => pozycja.Towar!)
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
			WypelnijWedlugLat(treeNode, daty);
		}

		private void WypelnijSkladkiZus(TreeNode treeNode)
		{
			using var kontekst = new Kontekst();
			var daty = kontekst.Baza.SkladkiZus
				.Select(faktura => faktura.Miesiac)
				.Distinct()
				.ToList();
			WypelnijWedlugLat(treeNode, daty);
		}

		private void WypelnijZaliczkiPit(TreeNode treeNode)
		{
			using var kontekst = new Kontekst();
			var daty = kontekst.Baza.ZaliczkiPit
				.Select(faktura => faktura.Miesiac)
				.Distinct()
				.ToList();
			WypelnijWedlugLat(treeNode, daty);
		}

		private void WypelnijWedlugLat(TreeNode treeNode, IEnumerable<DateTime> daty)
		{
			while (treeNode.Nodes.Count > 0) treeNode.Nodes.RemoveAt(0);

			var lata = daty.Append(DateTime.Now.Date).Select(data => data.Year).Distinct().OrderBy(rok => rok).ToList();
			var treeNodeWszystko = new TreeNode { Name = "", Text = "(wszystkie)" };
			treeNode.Nodes.Add(treeNodeWszystko);
			foreach (var rok in lata)
			{
				var treeNodeRok = new TreeNode { Name = "R:" + rok, Text = rok.ToString() };
				treeNode.Nodes.Add(treeNodeRok);
			}
		}

		private void menu_NodeMouseClick(object? sender, TreeNodeMouseClickEventArgs e)
		{
			if (e.Button == MouseButtons.Right && e.Node != null) PokazMenuKontekstowe(e.Node);
		}
	}
}
