
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Wszystkie");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Według daty", new System.Windows.Forms.TreeNode[] {
            treeNode2});
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Według nabywcy", new System.Windows.Forms.TreeNode[] {
            treeNode4});
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Według towaru", new System.Windows.Forms.TreeNode[] {
            treeNode6});
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Faktury sprzedaży", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode3,
            treeNode5,
            treeNode7});
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Wszystkie");
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Według daty", new System.Windows.Forms.TreeNode[] {
            treeNode10});
			System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Według sprzedawcy", new System.Windows.Forms.TreeNode[] {
            treeNode12});
			System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Według towaru", new System.Windows.Forms.TreeNode[] {
            treeNode14});
			System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Faktury zakupu", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode11,
            treeNode13,
            treeNode15});
			System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Faktury", new System.Windows.Forms.TreeNode[] {
            treeNode8,
            treeNode16});
			System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Kontrahenci");
			System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Towary");
			System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Jednostki miar");
			System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Sposoby płatności");
			System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Stawki VAT");
			System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Waluty");
			System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Słowniki", new System.Windows.Forms.TreeNode[] {
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23});
			System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Numeracja");
			System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Baza danych");
			System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Polecenie SQL");
			System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Bezpośrednia edycja");
			System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("O programie");
			System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Serwisowe", new System.Windows.Forms.TreeNode[] {
            treeNode25,
            treeNode26,
            treeNode27,
            treeNode28,
            treeNode29});
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlowneOkno));
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
			treeNode1.Name = "";
			treeNode1.Text = "Wszystkie";
			treeNode2.Name = "";
			treeNode2.Text = "(ładowanie)";
			treeNode3.Name = "WedlugDaty";
			treeNode3.Text = "Według daty";
			treeNode4.Name = "";
			treeNode4.Text = "(ładowanie)";
			treeNode5.Name = "WedlugKontrahenta";
			treeNode5.Text = "Według nabywcy";
			treeNode6.Name = "";
			treeNode6.Text = "(ładowanie)";
			treeNode7.Name = "WedlugTowaru";
			treeNode7.Text = "Według towaru";
			treeNode8.Name = "FakturySprzedazy";
			treeNode8.Text = "Faktury sprzedaży";
			treeNode9.Name = "";
			treeNode9.Text = "Wszystkie";
			treeNode10.Name = "";
			treeNode10.Text = "(ładowanie)";
			treeNode11.Name = "WedlugDaty";
			treeNode11.Text = "Według daty";
			treeNode12.Name = "";
			treeNode12.Text = "(ładowanie)";
			treeNode13.Name = "WedlugKontrahenta";
			treeNode13.Text = "Według sprzedawcy";
			treeNode14.Name = "";
			treeNode14.Text = "(ładowanie)";
			treeNode15.Name = "WedlugTowaru";
			treeNode15.Text = "Według towaru";
			treeNode16.Name = "FakturyZakupu";
			treeNode16.Text = "Faktury zakupu";
			treeNode17.Name = "Faktury";
			treeNode17.Text = "Faktury";
			treeNode18.Name = "Kontrahenci";
			treeNode18.Text = "Kontrahenci";
			treeNode19.Name = "Towary";
			treeNode19.Text = "Towary";
			treeNode20.Name = "JednostkiMiar";
			treeNode20.Text = "Jednostki miar";
			treeNode21.Name = "SposobyPlatnosci";
			treeNode21.Text = "Sposoby płatności";
			treeNode22.Name = "StawkiVat";
			treeNode22.Text = "Stawki VAT";
			treeNode23.Name = "Waluty";
			treeNode23.Text = "Waluty";
			treeNode24.Name = "Slowniki";
			treeNode24.Text = "Słowniki";
			treeNode25.Name = "Numeratory";
			treeNode25.Text = "Numeracja";
			treeNode26.Name = "Baza";
			treeNode26.Text = "Baza danych";
			treeNode27.Name = "SQL";
			treeNode27.Text = "Polecenie SQL";
			treeNode28.Name = "Tabele";
			treeNode28.Text = "Bezpośrednia edycja";
			treeNode29.Name = "OProgramie";
			treeNode29.Text = "O programie";
			treeNode30.Name = "Node1";
			treeNode30.Text = "Serwisowe";
			this.treeViewMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18,
            treeNode19,
            treeNode24,
            treeNode30});
			this.treeViewMenu.Size = new System.Drawing.Size(247, 593);
			this.treeViewMenu.TabIndex = 1;
			this.treeViewMenu.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewMenu_BeforeExpand);
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
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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