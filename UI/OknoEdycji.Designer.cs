
namespace ProFak.UI
{
	partial class OknoEdycji
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
			this.buttonZapisz = new System.Windows.Forms.Button();
			this.buttonAnuluj = new System.Windows.Forms.Button();
			this.tableLayoutPanelZawartosc = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanelPrzyciski = new System.Windows.Forms.FlowLayoutPanel();
			this.panelZawartosc = new System.Windows.Forms.Panel();
			this.tableLayoutPanelZawartosc.SuspendLayout();
			this.flowLayoutPanelPrzyciski.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonZapisz
			// 
			this.buttonZapisz.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonZapisz.Location = new System.Drawing.Point(3, 3);
			this.buttonZapisz.Name = "buttonZapisz";
			this.buttonZapisz.Size = new System.Drawing.Size(75, 23);
			this.buttonZapisz.TabIndex = 1000;
			this.buttonZapisz.Text = "Zapisz";
			this.buttonZapisz.UseVisualStyleBackColor = true;
			// 
			// buttonAnuluj
			// 
			this.buttonAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonAnuluj.Location = new System.Drawing.Point(84, 3);
			this.buttonAnuluj.Name = "buttonAnuluj";
			this.buttonAnuluj.Size = new System.Drawing.Size(75, 23);
			this.buttonAnuluj.TabIndex = 1001;
			this.buttonAnuluj.Text = "Anuluj";
			this.buttonAnuluj.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanelZawartosc
			// 
			this.tableLayoutPanelZawartosc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanelZawartosc.ColumnCount = 1;
			this.tableLayoutPanelZawartosc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelZawartosc.Controls.Add(this.flowLayoutPanelPrzyciski, 0, 1);
			this.tableLayoutPanelZawartosc.Controls.Add(this.panelZawartosc, 0, 0);
			this.tableLayoutPanelZawartosc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanelZawartosc.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanelZawartosc.Name = "tableLayoutPanelZawartosc";
			this.tableLayoutPanelZawartosc.RowCount = 2;
			this.tableLayoutPanelZawartosc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanelZawartosc.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanelZawartosc.Size = new System.Drawing.Size(231, 163);
			this.tableLayoutPanelZawartosc.TabIndex = 3;
			// 
			// flowLayoutPanelPrzyciski
			// 
			this.flowLayoutPanelPrzyciski.AutoSize = true;
			this.flowLayoutPanelPrzyciski.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanelPrzyciski.Controls.Add(this.buttonZapisz);
			this.flowLayoutPanelPrzyciski.Controls.Add(this.buttonAnuluj);
			this.flowLayoutPanelPrzyciski.Location = new System.Drawing.Point(3, 131);
			this.flowLayoutPanelPrzyciski.Name = "flowLayoutPanelPrzyciski";
			this.flowLayoutPanelPrzyciski.Size = new System.Drawing.Size(162, 29);
			this.flowLayoutPanelPrzyciski.TabIndex = 999;
			// 
			// panelZawartosc
			// 
			this.panelZawartosc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelZawartosc.Location = new System.Drawing.Point(3, 3);
			this.panelZawartosc.MinimumSize = new System.Drawing.Size(200, 100);
			this.panelZawartosc.Name = "panelZawartosc";
			this.panelZawartosc.Size = new System.Drawing.Size(225, 122);
			this.panelZawartosc.TabIndex = 1;
			// 
			// OknoEdycji
			// 
			this.AcceptButton = this.buttonZapisz;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.CancelButton = this.buttonAnuluj;
			this.ClientSize = new System.Drawing.Size(227, 163);
			this.Controls.Add(this.tableLayoutPanelZawartosc);
			this.Name = "OknoEdycji";
			this.Text = "OknoEdycji";
			this.tableLayoutPanelZawartosc.ResumeLayout(false);
			this.tableLayoutPanelZawartosc.PerformLayout();
			this.flowLayoutPanelPrzyciski.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button buttonZapisz;
		private System.Windows.Forms.Button buttonAnuluj;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelZawartosc;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPrzyciski;
		private System.Windows.Forms.Panel panelZawartosc;
	}
}