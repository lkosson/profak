
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
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Do zapłaty");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Według daty", new System.Windows.Forms.TreeNode[] {
            treeNode3});
			System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Według nabywcy", new System.Windows.Forms.TreeNode[] {
            treeNode5});
			System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Według towaru", new System.Windows.Forms.TreeNode[] {
            treeNode7});
			System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Faktury sprzedaży", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode4,
            treeNode6,
            treeNode8});
			System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Wszystkie");
			System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Do zapłaty");
			System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Według daty", new System.Windows.Forms.TreeNode[] {
            treeNode12});
			System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Według sprzedawcy", new System.Windows.Forms.TreeNode[] {
            treeNode14});
			System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Według towaru", new System.Windows.Forms.TreeNode[] {
            treeNode16});
			System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Faktury zakupu", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11,
            treeNode13,
            treeNode15,
            treeNode17});
			System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Faktury", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode18});
			System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Kontrahenci");
			System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Towary");
			System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Jednostki miar");
			System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Sposoby płatności");
			System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Stawki VAT");
			System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Waluty");
			System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Słowniki", new System.Windows.Forms.TreeNode[] {
            treeNode22,
            treeNode23,
            treeNode24,
            treeNode25});
			System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Numeracja");
			System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Baza danych");
			System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Polecenie SQL");
			System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Bezpośrednia edycja");
			System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("O programie");
			System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Serwisowe", new System.Windows.Forms.TreeNode[] {
            treeNode27,
            treeNode28,
            treeNode29,
            treeNode30,
            treeNode31});
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
			treeNode1.Name = "Wszystkie";
			treeNode1.Text = "Wszystkie";
			treeNode2.Name = "DoZaplaty";
			treeNode2.Text = "Do zapłaty";
			treeNode3.Name = "";
			treeNode3.Text = "(ładowanie)";
			treeNode4.Name = "WedlugDaty";
			treeNode4.Text = "Według daty";
			treeNode5.Name = "";
			treeNode5.Text = "(ładowanie)";
			treeNode6.Name = "WedlugKontrahenta";
			treeNode6.Text = "Według nabywcy";
			treeNode7.Name = "";
			treeNode7.Text = "(ładowanie)";
			treeNode8.Name = "WedlugTowaru";
			treeNode8.Text = "Według towaru";
			treeNode9.Name = "FakturySprzedazy";
			treeNode9.Text = "Faktury sprzedaży";
			treeNode10.Name = "Wszystkie";
			treeNode10.Text = "Wszystkie";
			treeNode11.Name = "DoZaplaty";
			treeNode11.Text = "Do zapłaty";
			treeNode12.Name = "";
			treeNode12.Text = "(ładowanie)";
			treeNode13.Name = "WedlugDaty";
			treeNode13.Text = "Według daty";
			treeNode14.Name = "";
			treeNode14.Text = "(ładowanie)";
			treeNode15.Name = "WedlugKontrahenta";
			treeNode15.Text = "Według sprzedawcy";
			treeNode16.Name = "";
			treeNode16.Text = "(ładowanie)";
			treeNode17.Name = "WedlugTowaru";
			treeNode17.Text = "Według towaru";
			treeNode18.Name = "FakturyZakupu";
			treeNode18.Text = "Faktury zakupu";
			treeNode19.Name = "Faktury";
			treeNode19.Text = "Faktury";
			treeNode20.Name = "Kontrahenci";
			treeNode20.Text = "Kontrahenci";
			treeNode21.Name = "Towary";
			treeNode21.Text = "Towary";
			treeNode22.Name = "JednostkiMiar";
			treeNode22.Text = "Jednostki miar";
			treeNode23.Name = "SposobyPlatnosci";
			treeNode23.Text = "Sposoby płatności";
			treeNode24.Name = "StawkiVat";
			treeNode24.Text = "Stawki VAT";
			treeNode25.Name = "Waluty";
			treeNode25.Text = "Waluty";
			treeNode26.Name = "Slowniki";
			treeNode26.Text = "Słowniki";
			treeNode27.Name = "Numeratory";
			treeNode27.Text = "Numeracja";
			treeNode28.Name = "Baza";
			treeNode28.Text = "Baza danych";
			treeNode29.Name = "SQL";
			treeNode29.Text = "Polecenie SQL";
			treeNode30.Name = "Tabele";
			treeNode30.Text = "Bezpośrednia edycja";
			treeNode31.Name = "OProgramie";
			treeNode31.Text = "O programie";
			treeNode32.Name = "Node1";
			treeNode32.Text = "Serwisowe";
			this.treeViewMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode19,
            treeNode20,
            treeNode21,
            treeNode26,
            treeNode32});
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