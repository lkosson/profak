
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
			System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Deklaracje Vat", new System.Windows.Forms.TreeNode[] {
            treeNode20});
			System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Składki Zus", new System.Windows.Forms.TreeNode[] {
            treeNode22});
			System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("(ładowanie)");
			System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Zaliczki Pit", new System.Windows.Forms.TreeNode[] {
            treeNode24});
			System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Podatki", new System.Windows.Forms.TreeNode[] {
            treeNode21,
            treeNode23,
            treeNode25});
			System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Kontrahenci");
			System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Towary");
			System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Jednostki miar");
			System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Sposoby płatności");
			System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Stawki VAT");
			System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Urzędy skarbowe");
			System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Waluty");
			System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("Słowniki", new System.Windows.Forms.TreeNode[] {
            treeNode29,
            treeNode30,
            treeNode31,
            treeNode32,
            treeNode33});
			System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Numeracja");
			System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("Baza danych");
			System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("Polecenie SQL");
			System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("Bezpośrednia edycja");
			System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("O programie");
			System.Windows.Forms.TreeNode treeNode40 = new System.Windows.Forms.TreeNode("Serwisowe", new System.Windows.Forms.TreeNode[] {
            treeNode35,
            treeNode36,
            treeNode37,
            treeNode38,
            treeNode39});
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
			treeNode20.Name = "";
			treeNode20.Text = "(ładowanie)";
			treeNode21.Name = "DeklaracjeVat";
			treeNode21.Text = "Deklaracje Vat";
			treeNode22.Name = "";
			treeNode22.Text = "(ładowanie)";
			treeNode23.Name = "SkladkiZus";
			treeNode23.Text = "Składki Zus";
			treeNode24.Name = "";
			treeNode24.Text = "(ładowanie)";
			treeNode25.Name = "ZaliczkiPit";
			treeNode25.Text = "Zaliczki Pit";
			treeNode26.Name = "Podatki";
			treeNode26.Text = "Podatki";
			treeNode27.Name = "Kontrahenci";
			treeNode27.Text = "Kontrahenci";
			treeNode28.Name = "Towary";
			treeNode28.Text = "Towary";
			treeNode29.Name = "JednostkiMiar";
			treeNode29.Text = "Jednostki miar";
			treeNode30.Name = "SposobyPlatnosci";
			treeNode30.Text = "Sposoby płatności";
			treeNode31.Name = "StawkiVat";
			treeNode31.Text = "Stawki VAT";
			treeNode32.Name = "UrzedySkarbowe";
			treeNode32.Text = "Urzędy skarbowe";
			treeNode33.Name = "Waluty";
			treeNode33.Text = "Waluty";
			treeNode34.Name = "Slowniki";
			treeNode34.Text = "Słowniki";
			treeNode35.Name = "Numeratory";
			treeNode35.Text = "Numeracja";
			treeNode36.Name = "Baza";
			treeNode36.Text = "Baza danych";
			treeNode37.Name = "SQL";
			treeNode37.Text = "Polecenie SQL";
			treeNode38.Name = "Tabele";
			treeNode38.Text = "Bezpośrednia edycja";
			treeNode39.Name = "OProgramie";
			treeNode39.Text = "O programie";
			treeNode40.Name = "Node1";
			treeNode40.Text = "Serwisowe";
			this.menu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode19,
            treeNode26,
            treeNode27,
            treeNode28,
            treeNode34,
            treeNode40});
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