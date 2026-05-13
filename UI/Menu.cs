using ProFak.DB;

namespace ProFak.UI;

partial class Menu
{
	private bool menuGotowe;
	private readonly Func<TTreeNode[]> konstruktorMenu;

	public Menu(Func<TTreeNode[]> konstruktorMenu)
	{
		this.konstruktorMenu = konstruktorMenu;
	}

	private void ZapiszStanPozycji(TTreeNode? treeNode, bool ukryta = false, bool zwinieta = false, bool aktywna = false)
	{
		if (!menuGotowe) return;
		if (treeNode == null) return;
		using var kontekst = new Kontekst();
		if (kontekst.Baza.CzyZablokowana()) return;
		using var transakcja = kontekst.Transakcja();
		var stan = kontekst.Baza.StanyMenu.FirstOrDefault(e => e.Pozycja == treeNode.FullPath);
		if (stan == null) stan = new StanMenu { Pozycja = treeNode.FullPath };
		stan.CzyZwinieta = zwinieta;
		stan.CzyAktywna = aktywna;
		stan.CzyUkryta = ukryta || treeNode.ForeColor == KolorUkryty;
		if (stan.CzyAktywna)
		{
			var stareAktywne = kontekst.Baza.StanyMenu.Where(e => e.CzyAktywna).ToList();
			foreach (var staraAktywna in stareAktywne)
			{
				staraAktywna.CzyAktywna = false;
				kontekst.Baza.Zapisz(staraAktywna);
			}
		}
		kontekst.Baza.Zapisz(stan);
		transakcja.Zatwierdz();
	}

	private void PokazMenuKontekstowe(TTreeNode wezel)
	{
		void Pokaz()
		{
			ZapiszStanPozycji(wezel, ukryta: false);
			wezel.ForeColor = KolorAktywny;
		}

		void Ukryj()
		{
			ZapiszStanPozycji(wezel, ukryta: true);
			if (wezel.Parent == null) Nodes.Remove(wezel);
			else wezel.Parent.Nodes.Remove(wezel);
		}

		void PokazUkryte()
		{
			Zbuduj(pokazUkryte: true);
		}

		var menuPokaz = Kontrolki.MenuItem("Pokaż", Pokaz);
		var menuUkryj = Kontrolki.MenuItem("Ukryj", Ukryj);
		var menuPokazUkryte = Kontrolki.MenuItem("Pokaż ukryte", PokazUkryte);
		var ukryty = wezel.ForeColor == KolorUkryty;
		Kontrolki.Menu([ukryty ? menuPokaz : menuUkryj, menuPokazUkryte], wyswietlDla: this);

	}

	private void Rozwin(bool pokazUkryte = false)
	{
		using var kontekst = new Kontekst();
		var stany = kontekst.Baza.StanyMenu.ToList();
		CollapseAll();
		var stanyWedlugNazwy = stany.ToDictionary(stan => stan.Pozycja);
		TTreeNode? doWyswietlenia = null;
		Rozwin(Nodes.Cast<TTreeNode>(), stanyWedlugNazwy, pokazUkryte, ref doWyswietlenia);

		if (doWyswietlenia != null) SelectedNode = doWyswietlenia;
	}

	private void Rozwin(IEnumerable<TTreeNode> wezly, Dictionary<string, StanMenu> stany, bool pokazUkryte, ref TTreeNode? wybrany)
	{
		var doUsuniecia = new List<TTreeNode>();
		foreach (var wezel in wezly)
		{
			if (stany.TryGetValue(wezel.FullPath, out var stan))
			{
				if (!stan.CzyZwinieta) wezel.Expand();
				if (stan.CzyAktywna) wybrany = wezel;
				if (stan.CzyUkryta) doUsuniecia.Add(wezel);
			}

			Rozwin(wezel.Nodes.Cast<TTreeNode>(), stany, pokazUkryte, ref wybrany);
		}

		foreach (var wezel in doUsuniecia)
		{
			if (pokazUkryte) wezel.ForeColor = SystemColors.GrayText;
			else wezel.Remove();
		}
	}
}
