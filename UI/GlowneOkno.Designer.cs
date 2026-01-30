
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