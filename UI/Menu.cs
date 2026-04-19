using ProFak.DB;

namespace ProFak.UI;

partial class Menu
{
	private bool menuGotowe;
	private bool trwaAktualizacjaMenu;
	private Dictionary<TTreeNode, Action> akcje = [];
	private Dictionary<TTreeNode, Func<TTreeNode[]>> rozwiniecia = [];
	private readonly Func<TTreeNode[]> konstruktorMenu;

	public Menu(Func<TTreeNode[]> konstruktorMenu)
	{
		this.konstruktorMenu = konstruktorMenu;
	}

	public TTreeNode UtworzWezel(string tekst)
	{
		var wezel = Kontrolki.TreeNode(tekst);
		return wezel;
	}

	public TTreeNode UtworzWezel(string tekst, Action akcja)
	{
		var wezel = Kontrolki.TreeNode(tekst);
		akcje[wezel] = akcja;
		return wezel;
	}

	public TTreeNode UtworzWezel(string tekst, TTreeNode[]? podrzedne = null)
	{
		return Kontrolki.TreeNode(tekst, podrzedne);
	}

	public TTreeNode UtworzWezel(string tekst, Func<TTreeNode[]> rozwiniecie)
	{
		var wezelLadowanie = Kontrolki.TreeNode("(ładowanie)");
		var wezel = UtworzWezel(tekst, [wezelLadowanie]);
		rozwiniecia[wezel] = rozwiniecie;
		return wezel;
	}

	private TTreeNode? Wezel(params string[] sciezka)
	{
		var wezel = Nodes[sciezka.First()];
		if (wezel == null) return null;
		foreach (var nazwa in sciezka.Skip(1))
		{
			if (nazwa == "*") wezel = wezel.Nodes.Cast<TreeNode>().LastOrDefault();
			else wezel = wezel.Nodes[nazwa];
			if (wezel == null) return null;
		}
		return wezel;
	}
	/*
	public void Rozwin(params string[] sciezka)
	{
		var wezel = Wezel(sciezka);
		wezel?.Expand();
	}

	public void Wybierz(params string[] sciezka)
	{
		var wezel = Wezel(sciezka);
		if (wezel != null) SelectedNode = wezel;
	}
	*/
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
			if (wezel.Parent == null) Nodes.Remove(wezel);
			else wezel.Parent.Nodes.Remove(wezel);
		};
		menuPokazUkryte.Click += delegate
		{
			Zbuduj();
			Rozwin(pokazUkryte: true);
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

	private void Rozwin(bool pokazUkryte = false)
	{
		using var kontekst = new Kontekst();
		var stany = kontekst.Baza.StanyMenu.ToList();
		if (stany.Count == 0) return;

		CollapseAll();
		var stanyWedlugNazwy = stany.ToDictionary(stan => stan.Pozycja);
		TreeNode? doWyswietlenia = null;
		RozwinMenu(Nodes.Cast<TreeNode>(), stanyWedlugNazwy, pokazUkryte, ref doWyswietlenia);

		if (doWyswietlenia == null) SelectedNode = doWyswietlenia;
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
}
