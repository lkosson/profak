
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
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			label1 = new System.Windows.Forms.Label();
			labelWersja = new System.Windows.Forms.Label();
			labelSciezka = new System.Windows.Forms.Label();
			labelData = new System.Windows.Forms.Label();
			linkLabelStrona = new System.Windows.Forms.LinkLabel();
			label2 = new System.Windows.Forms.Label();
			btnSprawdzAktualizacje = new System.Windows.Forms.Button();
			tableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(label1, 0, 0);
			tableLayoutPanel1.Controls.Add(labelWersja, 0, 1);
			tableLayoutPanel1.Controls.Add(labelSciezka, 0, 2);
			tableLayoutPanel1.Controls.Add(labelData, 0, 3);
			tableLayoutPanel1.Controls.Add(linkLabelStrona, 0, 4);
			tableLayoutPanel1.Controls.Add(label2, 0, 6);
			tableLayoutPanel1.Controls.Add(btnSprawdzAktualizacje, 0, 5);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 8;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
			tableLayoutPanel1.Size = new System.Drawing.Size(494, 319);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Segoe UI", 16F);
			label1.Location = new System.Drawing.Point(208, 10);
			label1.Margin = new System.Windows.Forms.Padding(10);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(78, 30);
			label1.TabIndex = 0;
			label1.Text = "ProFak";
			// 
			// labelWersja
			// 
			labelWersja.Anchor = System.Windows.Forms.AnchorStyles.Top;
			labelWersja.AutoSize = true;
			labelWersja.Location = new System.Drawing.Point(222, 60);
			labelWersja.Margin = new System.Windows.Forms.Padding(10);
			labelWersja.Name = "labelWersja";
			labelWersja.Size = new System.Drawing.Size(50, 15);
			labelWersja.TabIndex = 0;
			labelWersja.Text = "[Wersja]";
			// 
			// labelSciezka
			// 
			labelSciezka.Anchor = System.Windows.Forms.AnchorStyles.Top;
			labelSciezka.AutoSize = true;
			labelSciezka.Location = new System.Drawing.Point(220, 95);
			labelSciezka.Margin = new System.Windows.Forms.Padding(10);
			labelSciezka.Name = "labelSciezka";
			labelSciezka.Size = new System.Drawing.Size(53, 15);
			labelSciezka.TabIndex = 0;
			labelSciezka.Text = "[Ścieżka]";
			// 
			// labelData
			// 
			labelData.Anchor = System.Windows.Forms.AnchorStyles.Top;
			labelData.AutoSize = true;
			labelData.Location = new System.Drawing.Point(227, 130);
			labelData.Margin = new System.Windows.Forms.Padding(10);
			labelData.Name = "labelData";
			labelData.Size = new System.Drawing.Size(39, 15);
			labelData.TabIndex = 0;
			labelData.Text = "[Data]";
			// 
			// linkLabelStrona
			// 
			linkLabelStrona.Anchor = System.Windows.Forms.AnchorStyles.Top;
			linkLabelStrona.AutoSize = true;
			linkLabelStrona.Location = new System.Drawing.Point(150, 165);
			linkLabelStrona.Margin = new System.Windows.Forms.Padding(10);
			linkLabelStrona.Name = "linkLabelStrona";
			linkLabelStrona.Size = new System.Drawing.Size(193, 15);
			linkLabelStrona.TabIndex = 1;
			linkLabelStrona.TabStop = true;
			linkLabelStrona.Text = "https://github.com/lkosson/profak";
			linkLabelStrona.LinkClicked += linkLabelStrona_LinkClicked;
			// 
			// label2
			// 
			label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(18, 236);
			label2.Margin = new System.Windows.Forms.Padding(10);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(457, 60);
			label2.TabIndex = 0;
			label2.Text = resources.GetString("label2.Text");
			// 
			// btnSprawdzAktualizacje
			// 
			btnSprawdzAktualizacje.Anchor = System.Windows.Forms.AnchorStyles.Top;
			btnSprawdzAktualizacje.Location = new System.Drawing.Point(180, 193);
			btnSprawdzAktualizacje.Name = "btnSprawdzAktualizacje";
			btnSprawdzAktualizacje.Size = new System.Drawing.Size(134, 30);
			btnSprawdzAktualizacje.TabIndex = 2;
			btnSprawdzAktualizacje.Text = "Sprawdź aktualiazcje";
			btnSprawdzAktualizacje.UseVisualStyleBackColor = true;
			btnSprawdzAktualizacje.Click += btnSprawdzAktualizacje_Click;
			// 
			// OProgramie
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(tableLayoutPanel1);
			Name = "OProgramie";
			Size = new System.Drawing.Size(494, 319);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelWersja;
		private System.Windows.Forms.Label labelSciezka;
		private System.Windows.Forms.Label labelData;
		private System.Windows.Forms.LinkLabel linkLabelStrona;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSprawdzAktualizacje;
    }
}
