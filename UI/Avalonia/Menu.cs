#if AVALONIA
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using System.Collections.ObjectModel;

namespace ProFak.UI;

partial class Menu : Avalonia.Controls.TreeView
{
	protected override Type StyleKeyOverride => typeof(Avalonia.Controls.TreeView);
	public ObservableCollection<TTreeNode> Nodes { get; } = [];
	public TTreeNode? SelectedNode { get => (TTreeNode?)SelectedItem; set { SelectedItem = value; } }

	private void Zbuduj(bool pokazUkryte = false)
	{
		var pozycje = konstruktorMenu();
		Nodes.Clear();
		foreach (var pozycja in pozycje)
		{
			Nodes.Add(pozycja);
		}
		Rozwin(pokazUkryte);
	}

	protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
	{
		Padding = new Thickness(0);
		ItemTemplate = new FuncTreeDataTemplate<TTreeNode>((node, scope) => new TText { Text = node.Text }, treeNode => treeNode.Nodes);
		ItemsSource = Nodes;
		base.OnAttachedToLogicalTree(e);
		Zbuduj();
		AddHandler(TreeViewItem.ExpandedEvent, Expand);
		AddHandler(TreeViewItem.CollapsedEvent, Collapse);
		SelectionChanged += Menu_SelectionChanged;
		menuGotowe = true;
	}

	private void Expand(object? sender, RoutedEventArgs e)
	{
		var item = (TreeViewItem)e.Source!;
		if (item == null) return;
		var wezel = (TTreeNode)item.DataContext!;
		if (trwaAktualizacjaMenu) return;
		if (!rozwiniecia.TryGetValue(wezel, out var rozwiniecie)) return;
		trwaAktualizacjaMenu = true;
		try
		{
			var noweWezly = rozwiniecie();
			wezel.Nodes.Clear();
			foreach (var nowyWezel in noweWezly)
				wezel.Nodes.Add(nowyWezel);
		}
		finally
		{
			trwaAktualizacjaMenu = false;
		}

		ZapiszStanPozycji(wezel, zwinieta: false);
	}

	private void Collapse(object? sender, RoutedEventArgs e)
	{
		var item = (TreeViewItem)e.Source!;
		if (item == null) return;
		var wezel = (TTreeNode)item.DataContext!;
		ZapiszStanPozycji(wezel, zwinieta: true);
	}

	private TreeViewItem? ZnajdzKontrolkeDlaWezla(TTreeNode wezel)
	{
		TreeViewItem? Znajdz(Controls? kontrolki)
		{
			foreach (TreeViewItem kontrolka in kontrolki ?? [])
			{
				var wezelKontrolki = kontrolka.DataContext as TTreeNode;
				if (wezelKontrolki == wezel) return kontrolka;
				var wynik = Znajdz(kontrolka?.Presenter?.Panel?.Children);
				if (wynik != null) return wynik;
			}
			return null;
		}

		return Znajdz(Presenter?.Panel?.Children);
	}

	private void Menu_SelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		if (trwaAktualizacjaMenu) return;
		var wybrany = SelectedNode;
		if (wybrany == null) return;
		if (wybrany.Nodes.Count > 0)
		{
			var kontrolka = ZnajdzKontrolkeDlaWezla(wybrany);
			if (kontrolka != null && !kontrolka.IsExpanded) kontrolka.IsExpanded = true;
			SelectedNode = wybrany;
			return;
		}
		if (SelectedNode == wybrany) Wyswietl(wybrany);
		else SelectedNode = wybrany;

		ZapiszStanPozycji(SelectedNode /* dla spisu według kontrahentów/towarów/dat mogło się zmienić */);
	}

	protected override void OnKeyDown(Avalonia.Input.KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (SelectedNode == null) return;
		if (e.Key == Avalonia.Input.Key.Enter)
		{
			e.Handled = true;
			Wyswietl(SelectedNode);
		}
		else if (e.Key == Avalonia.Input.Key.OemQuestion)
		{
			e.Handled = true;
			// TODO Avalonia
			//SelectedNode.Collapse(false);
		}
	}
	// TODO Avalonia
	/*
	protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
	{
		base.OnNodeMouseClick(e);
		if (e.Button == MouseButtons.Right && e.Node != null) PokazMenuKontekstowe(e.Node);
	}
	*/

	private void Rozwin(bool pokazUkryte)
	{
		// TODO Avalonia
	}

	private void Wyswietl(TTreeNode wezel)
	{
		if (akcje.TryGetValue(wezel, out var akcja)) akcja();
	}
}
#endif