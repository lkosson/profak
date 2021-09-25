
namespace ProFak.UI
{
	partial class OknoSpisu
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
			this.panelSpis = new System.Windows.Forms.Panel();
			this.panelAkcji = new ProFak.UI.PanelAkcji();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelSpis
			// 
			this.panelSpis.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelSpis.Location = new System.Drawing.Point(3, 3);
			this.panelSpis.Name = "panelSpis";
			this.panelSpis.Size = new System.Drawing.Size(587, 424);
			this.panelSpis.TabIndex = 0;
			// 
			// panelAkcji
			// 
			this.panelAkcji.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelAkcji.Location = new System.Drawing.Point(596, 3);
			this.panelAkcji.Name = "panelAkcji";
			this.panelAkcji.Size = new System.Drawing.Size(223, 424);
			this.panelAkcji.TabIndex = 2;
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel.Controls.Add(this.panelAkcji, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.panelSpis, 0, 0);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(822, 430);
			this.tableLayoutPanel.TabIndex = 1;
			// 
			// OknoSpisu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(822, 430);
			this.Controls.Add(this.tableLayoutPanel);
			this.Name = "OknoSpisu";
			this.Text = "OknoSpisu";
			this.tableLayoutPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private PanelAkcji panelAkcji;
		private System.Windows.Forms.Panel panelSpis;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
	}
}