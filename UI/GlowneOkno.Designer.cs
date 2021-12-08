
namespace ProFak.UI
{
	partial class GlowneOkno
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Faktury sprzedaży");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Faktury zakupu");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Faktury", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Kontrahenci");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Towary");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Jednostki miar");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Sposoby płatności");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Stawki VAT");
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Waluty");
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Słowniki", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9});
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Numeracja");
			System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Polecenie SQL");
			System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Bezpośrednia edycja");
			System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Serwisowe", new System.Windows.Forms.TreeNode[] {
            treeNode11,
            treeNode12,
            treeNode13});
			this.panel1 = new System.Windows.Forms.Panel();
			this.treeViewMenu = new System.Windows.Forms.TreeView();
			this.panelZawartosc = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.treeViewMenu);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(247, 593);
			this.panel1.TabIndex = 2;
			// 
			// treeViewMenu
			// 
			this.treeViewMenu.Cursor = System.Windows.Forms.Cursors.Default;
			this.treeViewMenu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeViewMenu.Location = new System.Drawing.Point(0, 0);
			this.treeViewMenu.Name = "treeViewMenu";
			treeNode1.Name = "FakturySprzedazy";
			treeNode1.Text = "Faktury sprzedaży";
			treeNode2.Name = "FakturyZakupu";
			treeNode2.Text = "Faktury zakupu";
			treeNode3.Name = "";
			treeNode3.Text = "Faktury";
			treeNode4.Name = "Kontrahenci";
			treeNode4.Text = "Kontrahenci";
			treeNode5.Name = "Towary";
			treeNode5.Text = "Towary";
			treeNode6.Name = "JednostkiMiar";
			treeNode6.Text = "Jednostki miar";
			treeNode7.Name = "SposobyPlatnosci";
			treeNode7.Text = "Sposoby płatności";
			treeNode8.Name = "StawkiVat";
			treeNode8.Text = "Stawki VAT";
			treeNode9.Name = "Waluty";
			treeNode9.Text = "Waluty";
			treeNode10.Name = "Node0";
			treeNode10.Text = "Słowniki";
			treeNode11.Name = "Numeratory";
			treeNode11.Text = "Numeracja";
			treeNode12.Name = "SQL";
			treeNode12.Text = "Polecenie SQL";
			treeNode13.Name = "Tabele";
			treeNode13.Text = "Bezpośrednia edycja";
			treeNode14.Name = "Node1";
			treeNode14.Text = "Serwisowe";
			this.treeViewMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode10,
            treeNode14});
			this.treeViewMenu.Size = new System.Drawing.Size(247, 593);
			this.treeViewMenu.TabIndex = 1;
			this.treeViewMenu.DoubleClick += new System.EventHandler(this.treeViewMenu_DoubleClick);
			this.treeViewMenu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.treeViewMenu_KeyPress);
			// 
			// panelZawartosc
			// 
			this.panelZawartosc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelZawartosc.Location = new System.Drawing.Point(247, 0);
			this.panelZawartosc.Name = "panelZawartosc";
			this.panelZawartosc.Size = new System.Drawing.Size(851, 593);
			this.panelZawartosc.TabIndex = 3;
			// 
			// GlowneOkno
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1098, 593);
			this.Controls.Add(this.panelZawartosc);
			this.Controls.Add(this.panel1);
			this.KeyPreview = true;
			this.Name = "GlowneOkno";
			this.Text = "ProFak";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TreeView treeViewMenu;
		private System.Windows.Forms.Panel panelZawartosc;
	}
}