
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
			System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Deklaracje Vat");
			System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Składki Zus");
			System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Zaliczki Pit");
			System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Podatki", new System.Windows.Forms.TreeNode[] {
            treeNode20,
            treeNode21,
            treeNode22});
			System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Kontrahenci");
			System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Towary");
			System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Jednostki miar");
			System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Sposoby płatności");
			System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Stawki VAT");
			System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Urzędy skarbowe");
			System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Waluty");
			System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Słowniki", new System.Windows.Forms.TreeNode[] {
            treeNode26,
            treeNode27,
            treeNode28,
            treeNode29,
            treeNode30});
			System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Numeracja");
			System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Baza danych");
			System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("Polecenie SQL");
			System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Bezpośrednia edycja");
			System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("O programie");
			System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("Serwisowe", new System.Windows.Forms.TreeNode[] {
            treeNode32,
            treeNode33,
            treeNode34,
            treeNode35,
            treeNode36});
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlowneOkno));
			this.panel1 = new System.Windows.Forms.Panel();
			this.menu = new ProFak.UI.Menu();
			this.panelZawartosc = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.menu);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(247, 593);
			this.panel1.TabIndex = 2;
			// 
			// menu
			// 
			this.menu.Cursor = System.Windows.Forms.Cursors.Default;
			this.menu.Dock = System.Windows.Forms.DockStyle.Fill;
			this.menu.Location = new System.Drawing.Point(0, 0);
			this.menu.Name = "menu";
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
			treeNode20.Name = "DeklaracjeVat";
			treeNode20.Text = "Deklaracje Vat";
			treeNode21.Name = "SkladkiZus";
			treeNode21.Text = "Składki Zus";
			treeNode22.Name = "ZaliczkiPit";
			treeNode22.Text = "Zaliczki Pit";
			treeNode23.Name = "Podatki";
			treeNode23.Text = "Podatki";
			treeNode24.Name = "Kontrahenci";
			treeNode24.Text = "Kontrahenci";
			treeNode25.Name = "Towary";
			treeNode25.Text = "Towary";
			treeNode26.Name = "JednostkiMiar";
			treeNode26.Text = "Jednostki miar";
			treeNode27.Name = "SposobyPlatnosci";
			treeNode27.Text = "Sposoby płatności";
			treeNode28.Name = "StawkiVat";
			treeNode28.Text = "Stawki VAT";
			treeNode29.Name = "UrzedySkarbowe";
			treeNode29.Text = "Urzędy skarbowe";
			treeNode30.Name = "Waluty";
			treeNode30.Text = "Waluty";
			treeNode31.Name = "Slowniki";
			treeNode31.Text = "Słowniki";
			treeNode32.Name = "Numeratory";
			treeNode32.Text = "Numeracja";
			treeNode33.Name = "Baza";
			treeNode33.Text = "Baza danych";
			treeNode34.Name = "SQL";
			treeNode34.Text = "Polecenie SQL";
			treeNode35.Name = "Tabele";
			treeNode35.Text = "Bezpośrednia edycja";
			treeNode36.Name = "OProgramie";
			treeNode36.Text = "O programie";
			treeNode37.Name = "Node1";
			treeNode37.Text = "Serwisowe";
			this.menu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode19,
            treeNode23,
            treeNode24,
            treeNode25,
            treeNode31,
            treeNode37});
			this.menu.Size = new System.Drawing.Size(247, 593);
			this.menu.TabIndex = 1;
			this.menu.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.menu_BeforeExpand);
			this.menu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.menu_AfterSelect);
			this.menu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.menu_KeyPress);
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
		private Menu menu;
		private System.Windows.Forms.Panel panelZawartosc;
	}
}