#if WINFORMS
using System.Runtime.InteropServices;

namespace ProFak.UI;

partial class Menu : TreeView
{
	private TreeNode? ostatnioWybrany;
	private Color KolorAktywny => SystemColors.ControlText;
	private Color KolorUkryty => SystemColors.GrayText;

	private void Zbuduj(bool pokazUkryte = false)
	{
		Margin = new Padding(0);
		var pozycje = konstruktorMenu();
		Nodes.Clear();
		Nodes.AddRange(pozycje);
		Rozwin(pokazUkryte);
	}

	protected override void OnHandleCreated(EventArgs e)
	{
		SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);
		base.OnHandleCreated(e);
		Zbuduj();
		menuGotowe = true;
	}

	protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
	{
		base.OnBeforeExpand(e);
		if (e.Node == null) return;
		if (trwaAktualizacjaMenu) return;
		if (!rozwiniecia.TryGetValue(e.Node, out var rozwiniecie)) return;
		trwaAktualizacjaMenu = true;
		try
		{
			var noweWezly = rozwiniecie();
			e.Node.Nodes.Clear();
			e.Node.Nodes.AddRange(noweWezly);
		}
		finally
		{
			trwaAktualizacjaMenu = false;
		}
	}

	protected override void OnAfterExpand(TreeViewEventArgs e)
	{
		base.OnAfterExpand(e);
		ZapiszStanPozycji(e.Node, zwinieta: false);
	}

	protected override void OnAfterCollapse(TreeViewEventArgs e)
	{
		base.OnAfterCollapse(e);
		ZapiszStanPozycji(e.Node, zwinieta: true);
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

		ZapiszStanPozycji(SelectedNode /* dla spisu według kontrahentów/towarów/dat mogło się zmienić */, zwinieta: false, aktywna: true);
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
		if (ostatnioWybrany != null)
		{
			ostatnioWybrany.BackColor = Color.Empty;
			ostatnioWybrany.ForeColor = Color.Empty;
		}
		ostatnioWybrany = wezel;
		wezel.BackColor = SystemColors.Highlight;
		wezel.ForeColor = SystemColors.HighlightText;

		if (akcje.TryGetValue(wezel, out var akcja)) akcja();
	}

	private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
	private const int TVS_EX_DOUBLEBUFFER = 0x0004;
	[DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
}
#endif