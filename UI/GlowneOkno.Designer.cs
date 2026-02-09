
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlowneOkno));
			panelMenu = new System.Windows.Forms.Panel();
			menu = new Menu();
			panelZawartosc = new System.Windows.Forms.Panel();
			panelMenu.SuspendLayout();
			SuspendLayout();
			// 
			// panelMenu
			// 
			panelMenu.Controls.Add(menu);
			panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
			panelMenu.Location = new System.Drawing.Point(0, 0);
			panelMenu.Name = "panelMenu";
			panelMenu.Size = new System.Drawing.Size(247, 593);
			panelMenu.TabIndex = 2;
			// 
			// menu
			// 
			menu.Dock = System.Windows.Forms.DockStyle.Fill;
			menu.Location = new System.Drawing.Point(0, 0);
			menu.Name = "menu";
			menu.Size = new System.Drawing.Size(247, 593);
			menu.TabIndex = 1;
			menu.AfterCollapse += menu_AfterCollapse;
			menu.BeforeExpand += menu_BeforeExpand;
			menu.AfterExpand += menu_AfterExpand;
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
			Controls.Add(panelMenu);
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			KeyPreview = true;
			Name = "GlowneOkno";
			Text = "ProFak";
			WindowState = System.Windows.Forms.FormWindowState.Maximized;
			panelMenu.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel panelMenu;
		private Menu menu;
		private System.Windows.Forms.Panel panelZawartosc;
	}
}