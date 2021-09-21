
namespace ProFak.UI
{
	partial class StawkaVatEdytor
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
			this.components = new System.ComponentModel.Container();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.numericUpDown1, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.checkBox1, 1, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(182, 83);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// bindingSource
			// 
			this.bindingSource.DataSource = typeof(ProFak.DB.StawkaVat);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(19, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(34, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Skrót";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 15);
			this.label2.TabIndex = 1;
			this.label2.Text = "Wartość";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Skrot", true));
			this.textBox1.Location = new System.Drawing.Point(59, 3);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(120, 23);
			this.textBox1.TabIndex = 3;
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDown1.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "Wartosc", true));
			this.numericUpDown1.Location = new System.Drawing.Point(59, 32);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(120, 23);
			this.numericUpDown1.TabIndex = 4;
			this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// checkBox1
			// 
			this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBox1.AutoSize = true;
			this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "CzyDomyslna", true));
			this.checkBox1.Location = new System.Drawing.Point(59, 61);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(120, 19);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "Domyślna";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// StawkaVatEdytor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "StawkaVatEdytor";
			this.Size = new System.Drawing.Size(182, 83);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.BindingSource bindingSource;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.CheckBox checkBox1;
	}
}
