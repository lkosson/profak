
namespace ProFak.UI
{
	partial class NumeratorEdytor
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.comboBoxPrzeznaczenie = new System.Windows.Forms.ComboBox();
			this.panelStan = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBoxFormat = new System.Windows.Forms.ComboBox();
			this.textBoxPrzyklad = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxPrzeznaczenie, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.panelStan, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxFormat, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.textBoxPrzyklad, 1, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 250);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(34, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Przeznaczenie";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(69, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 15);
			this.label2.TabIndex = 0;
			this.label2.Text = "Format";
			// 
			// comboBoxPrzeznaczenie
			// 
			this.comboBoxPrzeznaczenie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxPrzeznaczenie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxPrzeznaczenie.FormattingEnabled = true;
			this.comboBoxPrzeznaczenie.Location = new System.Drawing.Point(120, 3);
			this.comboBoxPrzeznaczenie.Name = "comboBoxPrzeznaczenie";
			this.comboBoxPrzeznaczenie.Size = new System.Drawing.Size(677, 23);
			this.comboBoxPrzeznaczenie.TabIndex = 1;
			// 
			// panelStan
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.panelStan, 2);
			this.panelStan.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelStan.Location = new System.Drawing.Point(3, 90);
			this.panelStan.Name = "panelStan";
			this.panelStan.Size = new System.Drawing.Size(794, 157);
			this.panelStan.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 65);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(111, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "Przykładowy numer";
			// 
			// comboBoxFormat
			// 
			this.comboBoxFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxFormat.FormattingEnabled = true;
			this.comboBoxFormat.Items.AddRange(new object[] {
            "F/[Numer]",
            "F/[Numer:000000]",
            "F/[Numer:0000]/[Rok]",
            "F/[Numer:0000]/[Miesiac:00]/[Rok]",
            "F/[Numer]/[Data:yy/MM]",
            "F/[Numer:0000]/[Data:yyMMdd]"});
			this.comboBoxFormat.Location = new System.Drawing.Point(120, 32);
			this.comboBoxFormat.Name = "comboBoxFormat";
			this.comboBoxFormat.Size = new System.Drawing.Size(677, 23);
			this.comboBoxFormat.TabIndex = 4;
			this.comboBoxFormat.TextChanged += new System.EventHandler(this.comboBoxFormat_TextChanged);
			// 
			// textBoxPrzyklad
			// 
			this.textBoxPrzyklad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxPrzyklad.Location = new System.Drawing.Point(120, 61);
			this.textBoxPrzyklad.Name = "textBoxPrzyklad";
			this.textBoxPrzyklad.ReadOnly = true;
			this.textBoxPrzyklad.Size = new System.Drawing.Size(677, 23);
			this.textBoxPrzyklad.TabIndex = 5;
			// 
			// NumeratorEdytor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.MinimumSize = new System.Drawing.Size(800, 250);
			this.Name = "NumeratorEdytor";
			this.Size = new System.Drawing.Size(800, 250);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox comboBoxPrzeznaczenie;
		private System.Windows.Forms.Panel panelStan;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBoxFormat;
		private System.Windows.Forms.TextBox textBoxPrzyklad;
	}
}
