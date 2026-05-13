#if AVALONIA
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Styling;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ProFak.UI;

partial class Menu : Avalonia.Controls.TreeView
{
	protected override Type StyleKeyOverride => typeof(Avalonia.Controls.TreeView);
	public ObservableCollection<TTreeNode> Nodes { get; } = [];
	public TTreeNode? SelectedNode { get => (TTreeNode?)SelectedItem; set { SelectedItem = value; } }
	private Color KolorAktywny => Color.Black;
	private Color KolorUkryty => Color.Gray;

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

	public TTreeNode UtworzWezel(string tekst)
	{
		var wezel = new TTreeNode();
		wezel.Text = tekst;
		wezel.Zwiniety += Wezel_Zwiniety;
		wezel.Rozwiniety += Wezel_Rozwiniety;
		return wezel;
	}

	public TTreeNode UtworzWezel(string tekst, Action akcja)
	{
		var wezel = UtworzWezel(tekst);
		wezel.Akcja = akcja;
		return wezel;
	}

	public TTreeNode UtworzWezel(string tekst, TTreeNode[] podrzedne)
	{
		var wezel = UtworzWezel(tekst);
		foreach (var podrzedny in podrzedne)
		{
			podrzedny.Parent = wezel;
			wezel.Nodes.Add(podrzedny);
		}
		return wezel;
	}

	public TTreeNode UtworzWezel(string tekst, Func<TTreeNode[]> rozwiniecie)
	{
		var wezelLadowanie = UtworzWezel("(ładowanie)");
		var wezel = UtworzWezel(tekst, [wezelLadowanie]);
		wezel.Generator = rozwiniecie;
		return wezel;
	}

	protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
	{
		var style = new Style(x => x.OfType<TreeViewItem>());
		var binding = CompiledBinding.Create<TTreeNode, bool>(e => e.CzyRozwiniety);
		style.Setters.Add(new Setter { Property = TreeViewItem.IsExpandedProperty, Value = binding });
		Styles.Add(style);
		Padding = new Thickness(0);
		ItemTemplate = new FuncTreeDataTemplate<TTreeNode>((node, scope) => new TText { Text = node.Text }, treeNode => treeNode.Nodes);
		ItemsSource = Nodes;
		base.OnAttachedToLogicalTree(e);
		Zbuduj();
		SelectionChanged += Menu_SelectionChanged;
		menuGotowe = true;
	}

	private void Wezel_Rozwiniety(TTreeNode wezel)
	{ 
		ZapiszStanPozycji(wezel, zwinieta: false);
	}

	private void Wezel_Zwiniety(TTreeNode wezel)
	{
		ZapiszStanPozycji(wezel, zwinieta: true);
	}

	private void Menu_SelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		var wybrany = SelectedNode;
		if (wybrany == null) return;
		if (!ostatnioWybrany?.FullPath.StartsWith(wybrany.FullPath) ?? false)
		{
			while (wybrany.Nodes.Count > 0)
			{
				wybrany.CzyRozwiniety = true;
				wybrany = wybrany.Nodes[0];
			}
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
			SelectedNode.CzyRozwiniety = false;
		}
	}

	protected override void OnRightTapped(TappedEventArgs e)
	{
		base.OnRightTapped(e);
		if (SelectedNode != null) PokazMenuKontekstowe(SelectedNode);
	}

	private void Wyswietl(TTreeNode wezel)
	{
		ostatnioWybrany = wezel;
		wezel?.Akcja?.Invoke();
	}

	private void CollapseAll()
	{
		void Zwin(TTreeNode wezel)
		{
			if (wezel.CzyRozwiniety) wezel.CzyRozwiniety = false;
			foreach (var podrzedny in wezel.Nodes)
				Zwin(podrzedny);
		}

		foreach (var wezel in Nodes)
		{
			Zwin(wezel);
		}
	}
}

class WezelMenu : INotifyPropertyChanging, INotifyPropertyChanged
{
	// Nazwy dla zgodności z WinForms TreeNode
	public string Text { get; set; } = "";
	public string FullPath => Parent == null ? Text : Parent.FullPath + "\\" + Text;
	public TTreeNode? Parent { get; set; }
	public void Expand() => CzyRozwiniety = true;
	public void Remove() => Parent?.Nodes.Remove(this);

	// TODO Avalonia
	public System.Drawing.Color ForeColor { get; set; }
	public ObservableCollection<TTreeNode> Nodes { get; set; } = [];

	public Action? Akcja { get; set; }
	public Func<TTreeNode[]>? Generator { get; set; }

	public bool CzyRozwiniety
	{
		get
		{
			return field;
		}

		set
		{
			PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(nameof(CzyRozwiniety)));
			var poprzednio = field;
			field = value;
			if (Generator != null)
			{
				var noweWezly = Generator();
				Nodes.Clear();
				foreach (var nowyWezel in noweWezly)
					Nodes.Add(nowyWezel);
			}
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CzyRozwiniety)));
			if (poprzednio && !value) Zwiniety?.Invoke(this);
			if (!poprzednio && value) Rozwiniety?.Invoke(this);
		}
	}

	public event PropertyChangingEventHandler? PropertyChanging;
	public event PropertyChangedEventHandler? PropertyChanged;
	public event Action<TTreeNode>? Zwiniety;
	public event Action<TTreeNode>? Rozwiniety;

	public override string ToString() => FullPath;
}
#endif