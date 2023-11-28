
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
			var treeNode1 = new System.Windows.Forms.TreeNode("Wszystkie");
			var treeNode2 = new System.Windows.Forms.TreeNode("Do zapłaty");
			var treeNode3 = new System.Windows.Forms.TreeNode("KSeF");
			var treeNode4 = new System.Windows.Forms.TreeNode("(ładowanie)");
			var treeNode5 = new System.Windows.Forms.TreeNode("Według daty", new System.Windows.Forms.TreeNode[] { treeNode4 });
			var treeNode6 = new System.Windows.Forms.TreeNode("(ładowanie)");
			var treeNode7 = new System.Windows.Forms.TreeNode("Według nabywcy", new System.Windows.Forms.TreeNode[] { treeNode6 });
			var treeNode8 = new System.Windows.Forms.TreeNode("(ładowanie)");
			var treeNode9 = new System.Windows.Forms.TreeNode("Według towaru", new System.Windows.Forms.TreeNode[] { treeNode8 });
			var treeNode10 = new System.Windows.Forms.TreeNode("Faktury sprzedaży", new System.Windows.Forms.TreeNode[] { treeNode1, treeNode2, treeNode3, treeNode5, treeNode7, treeNode9 });
			var treeNode11 = new System.Windows.Forms.TreeNode("Wszystkie");
			var treeNode12 = new System.Windows.Forms.TreeNode("Do zapłaty");
			var treeNode13 = new System.Windows.Forms.TreeNode("KSeF");
			var treeNode14 = new System.Windows.Forms.TreeNode("(ładowanie)");
			var treeNode15 = new System.Windows.Forms.TreeNode("Według daty", new System.Windows.Forms.TreeNode[] { treeNode14 });
			var treeNode16 = new System.Windows.Forms.TreeNode("(ładowanie)");
			var treeNode17 = new System.Windows.Forms.TreeNode("Według sprzedawcy", new System.Windows.Forms.TreeNode[] { treeNode16 });
			var treeNode18 = new System.Windows.Forms.TreeNode("(ładowanie)");
			var treeNode19 = new System.Windows.Forms.TreeNode("Według towaru", new System.Windows.Forms.TreeNode[] { treeNode18 });
			var treeNode20 = new System.Windows.Forms.TreeNode("Faktury zakupu", new System.Windows.Forms.TreeNode[] { treeNode11, treeNode12, treeNode13, treeNode15, treeNode17, treeNode19 });
			var treeNode21 = new System.Windows.Forms.TreeNode("Faktury", new System.Windows.Forms.TreeNode[] { treeNode10, treeNode20 });
			var treeNode22 = new System.Windows.Forms.TreeNode("(ładowanie)");
			var treeNode23 = new System.Windows.Forms.TreeNode("Deklaracje Vat", new System.Windows.Forms.TreeNode[] { treeNode22 });
			var treeNode24 = new System.Windows.Forms.TreeNode("(ładowanie)");
			var treeNode25 = new System.Windows.Forms.TreeNode("Składki Zus", new System.Windows.Forms.TreeNode[] { treeNode24 });
			var treeNode26 = new System.Windows.Forms.TreeNode("(ładowanie)");
			var treeNode27 = new System.Windows.Forms.TreeNode("Zaliczki Pit", new System.Windows.Forms.TreeNode[] { treeNode26 });
			var treeNode28 = new System.Windows.Forms.TreeNode("Podatki", new System.Windows.Forms.TreeNode[] { treeNode23, treeNode25, treeNode27 });
			var treeNode29 = new System.Windows.Forms.TreeNode("Kontrahenci");
			var treeNode30 = new System.Windows.Forms.TreeNode("Towary");
			var treeNode31 = new System.Windows.Forms.TreeNode("Jednostki miar");
			var treeNode32 = new System.Windows.Forms.TreeNode("Sposoby płatności");
			var treeNode33 = new System.Windows.Forms.TreeNode("Stawki VAT");
			var treeNode34 = new System.Windows.Forms.TreeNode("Urzędy skarbowe");
			var treeNode35 = new System.Windows.Forms.TreeNode("Waluty");
			var treeNode36 = new System.Windows.Forms.TreeNode("Słowniki", new System.Windows.Forms.TreeNode[] { treeNode31, treeNode32, treeNode33, treeNode34, treeNode35 });
			var treeNode37 = new System.Windows.Forms.TreeNode("Numeracja");
			var treeNode38 = new System.Windows.Forms.TreeNode("Baza danych");
			var treeNode39 = new System.Windows.Forms.TreeNode("Polecenie SQL");
			var treeNode40 = new System.Windows.Forms.TreeNode("Bezpośrednia edycja");
			var treeNode41 = new System.Windows.Forms.TreeNode("O programie");
			var treeNode42 = new System.Windows.Forms.TreeNode("Serwisowe", new System.Windows.Forms.TreeNode[] { treeNode37, treeNode38, treeNode39, treeNode40, treeNode41 });
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(GlowneOkno));
			panel1 = new System.Windows.Forms.Panel();
			menu = new Menu();
			panelZawartosc = new System.Windows.Forms.Panel();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.Controls.Add(menu);
			panel1.Dock = System.Windows.Forms.DockStyle.Left;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(247, 593);
			panel1.TabIndex = 2;
			// 
			// menu
			// 
			menu.Dock = System.Windows.Forms.DockStyle.Fill;
			menu.Location = new System.Drawing.Point(0, 0);
			menu.Name = "menu";
			treeNode1.Name = "Wszystkie";
			treeNode1.Text = "Wszystkie";
			treeNode2.Name = "DoZaplaty";
			treeNode2.Text = "Do zapłaty";
			treeNode3.Name = "KSeFSprzedaz";
			treeNode3.Text = "KSeF";
			treeNode4.Name = "";
			treeNode4.Text = "(ładowanie)";
			treeNode5.Name = "WedlugDaty";
			treeNode5.Text = "Według daty";
			treeNode6.Name = "";
			treeNode6.Text = "(ładowanie)";
			treeNode7.Name = "WedlugKontrahenta";
			treeNode7.Text = "Według nabywcy";
			treeNode8.Name = "";
			treeNode8.Text = "(ładowanie)";
			treeNode9.Name = "WedlugTowaru";
			treeNode9.Text = "Według towaru";
			treeNode10.Name = "FakturySprzedazy";
			treeNode10.Text = "Faktury sprzedaży";
			treeNode11.Name = "Wszystkie";
			treeNode11.Text = "Wszystkie";
			treeNode12.Name = "DoZaplaty";
			treeNode12.Text = "Do zapłaty";
			treeNode13.Name = "KSeFZakup";
			treeNode13.Text = "KSeF";
			treeNode14.Name = "";
			treeNode14.Text = "(ładowanie)";
			treeNode15.Name = "WedlugDaty";
			treeNode15.Text = "Według daty";
			treeNode16.Name = "";
			treeNode16.Text = "(ładowanie)";
			treeNode17.Name = "WedlugKontrahenta";
			treeNode17.Text = "Według sprzedawcy";
			treeNode18.Name = "";
			treeNode18.Text = "(ładowanie)";
			treeNode19.Name = "WedlugTowaru";
			treeNode19.Text = "Według towaru";
			treeNode20.Name = "FakturyZakupu";
			treeNode20.Text = "Faktury zakupu";
			treeNode21.Name = "Faktury";
			treeNode21.Text = "Faktury";
			treeNode22.Name = "";
			treeNode22.Text = "(ładowanie)";
			treeNode23.Name = "DeklaracjeVat";
			treeNode23.Text = "Deklaracje Vat";
			treeNode24.Name = "";
			treeNode24.Text = "(ładowanie)";
			treeNode25.Name = "SkladkiZus";
			treeNode25.Text = "Składki Zus";
			treeNode26.Name = "";
			treeNode26.Text = "(ładowanie)";
			treeNode27.Name = "ZaliczkiPit";
			treeNode27.Text = "Zaliczki Pit";
			treeNode28.Name = "Podatki";
			treeNode28.Text = "Podatki";
			treeNode29.Name = "Kontrahenci";
			treeNode29.Text = "Kontrahenci";
			treeNode30.Name = "Towary";
			treeNode30.Text = "Towary";
			treeNode31.Name = "JednostkiMiar";
			treeNode31.Text = "Jednostki miar";
			treeNode32.Name = "SposobyPlatnosci";
			treeNode32.Text = "Sposoby płatności";
			treeNode33.Name = "StawkiVat";
			treeNode33.Text = "Stawki VAT";
			treeNode34.Name = "UrzedySkarbowe";
			treeNode34.Text = "Urzędy skarbowe";
			treeNode35.Name = "Waluty";
			treeNode35.Text = "Waluty";
			treeNode36.Name = "Slowniki";
			treeNode36.Text = "Słowniki";
			treeNode37.Name = "Numeratory";
			treeNode37.Text = "Numeracja";
			treeNode38.Name = "Baza";
			treeNode38.Text = "Baza danych";
			treeNode39.Name = "SQL";
			treeNode39.Text = "Polecenie SQL";
			treeNode40.Name = "Tabele";
			treeNode40.Text = "Bezpośrednia edycja";
			treeNode41.Name = "OProgramie";
			treeNode41.Text = "O programie";
			treeNode42.Name = "Node1";
			treeNode42.Text = "Serwisowe";
			menu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { treeNode21, treeNode28, treeNode29, treeNode30, treeNode36, treeNode42 });
			menu.Size = new System.Drawing.Size(247, 593);
			menu.TabIndex = 1;
			menu.BeforeExpand += menu_BeforeExpand;
			menu.AfterSelect += menu_AfterSelect;
			menu.KeyPress += menu_KeyPress;
			// 
			// panelZawartosc
			// 
			panelZawartosc.Dock = System.Windows.Forms.DockStyle.Fill;
			panelZawartosc.Location = new System.Drawing.Point(247, 0);
			panelZawartosc.Name = "panelZawartosc";
			panelZawartosc.Size = new System.Drawing.Size(851, 593);
			panelZawartosc.TabIndex = 3;
			// 
			// GlowneOkno
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(1098, 593);
			Controls.Add(panelZawartosc);
			Controls.Add(panel1);
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			KeyPreview = true;
			Name = "GlowneOkno";
			Text = "ProFak";
			WindowState = System.Windows.Forms.FormWindowState.Maximized;
			panel1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private Menu menu;
		private System.Windows.Forms.Panel panelZawartosc;
	}
}