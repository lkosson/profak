
namespace ProFak.UI
{
	partial class OProgramie
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OProgramie));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.labelWersja = new System.Windows.Forms.Label();
			this.labelSciezka = new System.Windows.Forms.Label();
			this.labelData = new System.Windows.Forms.Label();
			this.linkLabelStrona = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.labelWersja, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.labelSciezka, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.labelData, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.linkLabelStrona, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 5);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 7;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(494, 281);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(207, 10);
			this.label1.Margin = new System.Windows.Forms.Padding(10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 30);
			this.label1.TabIndex = 0;
			this.label1.Text = "ProFak";
			// 
			// labelWersja
			// 
			this.labelWersja.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.labelWersja.AutoSize = true;
			this.labelWersja.Location = new System.Drawing.Point(222, 60);
			this.labelWersja.Margin = new System.Windows.Forms.Padding(10);
			this.labelWersja.Name = "labelWersja";
			this.labelWersja.Size = new System.Drawing.Size(50, 15);
			this.labelWersja.TabIndex = 0;
			this.labelWersja.Text = "[Wersja]";
			// 
			// labelSciezka
			// 
			this.labelSciezka.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.labelSciezka.AutoSize = true;
			this.labelSciezka.Location = new System.Drawing.Point(220, 95);
			this.labelSciezka.Margin = new System.Windows.Forms.Padding(10);
			this.labelSciezka.Name = "labelSciezka";
			this.labelSciezka.Size = new System.Drawing.Size(53, 15);
			this.labelSciezka.TabIndex = 0;
			this.labelSciezka.Text = "[Ścieżka]";
			// 
			// labelData
			// 
			this.labelData.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.labelData.AutoSize = true;
			this.labelData.Location = new System.Drawing.Point(227, 130);
			this.labelData.Margin = new System.Windows.Forms.Padding(10);
			this.labelData.Name = "labelData";
			this.labelData.Size = new System.Drawing.Size(39, 15);
			this.labelData.TabIndex = 0;
			this.labelData.Text = "[Data]";
			// 
			// linkLabelStrona
			// 
			this.linkLabelStrona.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.linkLabelStrona.AutoSize = true;
			this.linkLabelStrona.Location = new System.Drawing.Point(150, 165);
			this.linkLabelStrona.Margin = new System.Windows.Forms.Padding(10);
			this.linkLabelStrona.Name = "linkLabelStrona";
			this.linkLabelStrona.Size = new System.Drawing.Size(193, 15);
			this.linkLabelStrona.TabIndex = 1;
			this.linkLabelStrona.TabStop = true;
			this.linkLabelStrona.Text = "https://github.com/lkosson/profak";
			this.linkLabelStrona.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelStrona_LinkClicked);
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(18, 200);
			this.label2.Margin = new System.Windows.Forms.Padding(10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(458, 60);
			this.label2.TabIndex = 0;
			this.label2.Text = resources.GetString("label2.Text");
			// 
			// OProgramie
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "OProgramie";
			this.Size = new System.Drawing.Size(494, 281);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelWersja;
		private System.Windows.Forms.Label labelSciezka;
		private System.Windows.Forms.Label labelData;
		private System.Windows.Forms.LinkLabel linkLabelStrona;
		private System.Windows.Forms.Label label2;
	}
}
