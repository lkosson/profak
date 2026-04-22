#if AVALONIA
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Interactivity;
using System.Collections.ObjectModel;

namespace ProFak.UI;

partial class Menu : Avalonia.Controls.TreeView
{
	public ObservableCollection<TTreeNode> Nodes { get; } = [];

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

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
	{
		ItemTemplate = new FuncTreeDataTemplate<TTreeNode>((node, scope) => new TText { Text = node.Text }, treeNode => treeNode.Nodes);
		ItemsSource = Nodes;
		base.OnAttachedToVisualTree(e);
		Zbuduj();
		AddHandler(TreeViewItem.ExpandedEvent, Expand);
		AddHandler(TreeViewItem.CollapsedEvent, Collapse);
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

		ZapiszStanPozycji(wezel);
	}

	private void Collapse(object? sender, RoutedEventArgs e)
	{
		var treeNode = (TreeViewItem)e.Source!;
		ZapiszStanPozycji(treeNode);
	}

	protected override void OnAfterSelect(TreeViewEventArgs e)
	{
		base.OnAfterSelect(e);
		if (trwaAktualizacjaMenu) return;
		if ((e.Action & TreeViewAction.ByKeyboard) == TreeViewAction.ByKeyboard) return;
		var wybrany = SelectedNode;
		if (wybrany == null) return;
		if (ostatnioWybrany == null || ostatnioWybrany.TreeView == null || !ostatnioWybrany.FullPath.StartsWith(wybrany.FullPath)) while (wybrany.Nodes.Count > 0) wybrany = wybrany.Nodes[0];
		if (SelectedNode == wybrany) Wyswietl(wybrany);
		else SelectedNode = wybrany;

		ZapiszStanPozycji(SelectedNode /* dla spisu według kontrahentów/towarów/dat mogło się zmienić */);
	}

	protected override void OnKeyPress(KeyPressEventArgs e)
	{
		base.OnKeyPress(e);
		if (SelectedNode == null) return;
		if (e.KeyChar == '\r')
		{
			e.Handled = true;
			Wyswietl(SelectedNode);
		}
		else if (e.KeyChar == '/')
		{
			e.Handled = true;
			SelectedNode.Collapse(false);
		}
	}
	protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
	{
		base.OnNodeMouseClick(e);
		if (e.Button == MouseButtons.Right && e.Node != null) PokazMenuKontekstowe(e.Node);
	}

	private void Wyswietl(TTreeNode wezel)
	{
		if (akcje.TryGetValue(wezel, out var akcja)) akcja();
	}
}
#endif