
namespace ProFak.UI
{
	partial class PozycjaFakturyEdytor
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
			this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
			this.checkBoxWedlugBrutto = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
			this.checkBoxRecznie = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxTowar = new System.Windows.Forms.ComboBox();
			this.buttonTowar = new System.Windows.Forms.Button();
			this.numericUpDownIlosc = new System.Windows.Forms.NumericUpDown();
			this.labelJednostka = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownIlosc)).BeginInit();
			this.SuspendLayout();
			// 
			// bindingSource
			// 
			this.bindingSource.DataSource = typeof(ProFak.DB.PozycjaFaktury);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 201);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.tableLayoutPanel3);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(236, 158);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Cena jednostkowa";
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.label4, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.label5, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.numericUpDown2, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.numericUpDown3, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.numericUpDown4, 1, 2);
			this.tableLayoutPanel3.Controls.Add(this.checkBoxWedlugBrutto, 1, 3);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 19);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 5;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(230, 136);
			this.tableLayoutPanel3.TabIndex = 0;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(37, 15);
			this.label3.TabIndex = 0;
			this.label3.Text = "Netto";
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(20, 36);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(23, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "Vat";
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 65);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(40, 15);
			this.label5.TabIndex = 0;
			this.label5.Text = "Brutto";
			// 
			// numericUpDown2
			// 
			this.numericUpDown2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDown2.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "CenaNetto", true));
			this.numericUpDown2.DecimalPlaces = 2;
			this.numericUpDown2.Location = new System.Drawing.Point(49, 3);
			this.numericUpDown2.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(178, 23);
			this.numericUpDown2.TabIndex = 3;
			this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDown3
			// 
			this.numericUpDown3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDown3.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "CenaVat", true));
			this.numericUpDown3.DecimalPlaces = 2;
			this.numericUpDown3.Location = new System.Drawing.Point(49, 32);
			this.numericUpDown3.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDown3.Name = "numericUpDown3";
			this.numericUpDown3.Size = new System.Drawing.Size(178, 23);
			this.numericUpDown3.TabIndex = 3;
			this.numericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDown4
			// 
			this.numericUpDown4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDown4.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "CenaBrutto", true));
			this.numericUpDown4.DecimalPlaces = 2;
			this.numericUpDown4.Location = new System.Drawing.Point(49, 61);
			this.numericUpDown4.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDown4.Name = "numericUpDown4";
			this.numericUpDown4.Size = new System.Drawing.Size(178, 23);
			this.numericUpDown4.TabIndex = 3;
			this.numericUpDown4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// checkBoxWedlugBrutto
			// 
			this.checkBoxWedlugBrutto.AutoSize = true;
			this.checkBoxWedlugBrutto.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "CzyWedlugCenBrutto", true));
			this.checkBoxWedlugBrutto.Location = new System.Drawing.Point(49, 90);
			this.checkBoxWedlugBrutto.Name = "checkBoxWedlugBrutto";
			this.checkBoxWedlugBrutto.Size = new System.Drawing.Size(131, 19);
			this.checkBoxWedlugBrutto.TabIndex = 4;
			this.checkBoxWedlugBrutto.Text = "Według ceny brutto";
			this.checkBoxWedlugBrutto.UseVisualStyleBackColor = true;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.tableLayoutPanel4);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(245, 40);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(236, 158);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Łączna wartość";
			// 
			// tableLayoutPanel4
			// 
			this.tableLayoutPanel4.ColumnCount = 2;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Controls.Add(this.label6, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.label7, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.label8, 0, 2);
			this.tableLayoutPanel4.Controls.Add(this.numericUpDown5, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.numericUpDown6, 1, 1);
			this.tableLayoutPanel4.Controls.Add(this.numericUpDown7, 1, 2);
			this.tableLayoutPanel4.Controls.Add(this.checkBoxRecznie, 1, 3);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 19);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.RowCount = 5;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(230, 136);
			this.tableLayoutPanel4.TabIndex = 0;
			// 
			// label6
			// 
			this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 7);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(37, 15);
			this.label6.TabIndex = 0;
			this.label6.Text = "Netto";
			// 
			// label7
			// 
			this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(20, 36);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(23, 15);
			this.label7.TabIndex = 0;
			this.label7.Text = "Vat";
			// 
			// label8
			// 
			this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(3, 65);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(40, 15);
			this.label8.TabIndex = 0;
			this.label8.Text = "Brutto";
			// 
			// numericUpDown5
			// 
			this.numericUpDown5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDown5.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "WartoscNetto", true));
			this.numericUpDown5.DecimalPlaces = 2;
			this.numericUpDown5.Location = new System.Drawing.Point(49, 3);
			this.numericUpDown5.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDown5.Name = "numericUpDown5";
			this.numericUpDown5.Size = new System.Drawing.Size(178, 23);
			this.numericUpDown5.TabIndex = 3;
			this.numericUpDown5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDown6
			// 
			this.numericUpDown6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDown6.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "WartoscVat", true));
			this.numericUpDown6.DecimalPlaces = 2;
			this.numericUpDown6.Location = new System.Drawing.Point(49, 32);
			this.numericUpDown6.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDown6.Name = "numericUpDown6";
			this.numericUpDown6.Size = new System.Drawing.Size(178, 23);
			this.numericUpDown6.TabIndex = 3;
			this.numericUpDown6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDown7
			// 
			this.numericUpDown7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDown7.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "WartoscBrutto", true));
			this.numericUpDown7.DecimalPlaces = 2;
			this.numericUpDown7.Location = new System.Drawing.Point(49, 61);
			this.numericUpDown7.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDown7.Name = "numericUpDown7";
			this.numericUpDown7.Size = new System.Drawing.Size(178, 23);
			this.numericUpDown7.TabIndex = 3;
			this.numericUpDown7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// checkBoxRecznie
			// 
			this.checkBoxRecznie.AutoSize = true;
			this.checkBoxRecznie.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bindingSource, "CzyWartosciReczne", true));
			this.checkBoxRecznie.Location = new System.Drawing.Point(49, 90);
			this.checkBoxRecznie.Name = "checkBoxRecznie";
			this.checkBoxRecznie.Size = new System.Drawing.Size(133, 19);
			this.checkBoxRecznie.TabIndex = 4;
			this.checkBoxRecznie.Text = "Ustaw kwoty ręcznie";
			this.checkBoxRecznie.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.ColumnCount = 6;
			this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 2);
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.comboBoxTowar, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.buttonTowar, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.numericUpDownIlosc, 4, 0);
			this.tableLayoutPanel2.Controls.Add(this.labelJednostka, 5, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(478, 31);
			this.tableLayoutPanel2.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Towar/opis";
			// 
			// comboBoxTowar
			// 
			this.comboBoxTowar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxTowar.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSource, "Opis", true));
			this.comboBoxTowar.FormattingEnabled = true;
			this.comboBoxTowar.Location = new System.Drawing.Point(75, 4);
			this.comboBoxTowar.Name = "comboBoxTowar";
			this.comboBoxTowar.Size = new System.Drawing.Size(233, 23);
			this.comboBoxTowar.TabIndex = 1;
			// 
			// buttonTowar
			// 
			this.buttonTowar.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.buttonTowar.AutoSize = true;
			this.buttonTowar.Location = new System.Drawing.Point(314, 3);
			this.buttonTowar.Name = "buttonTowar";
			this.buttonTowar.Size = new System.Drawing.Size(26, 25);
			this.buttonTowar.TabIndex = 2;
			this.buttonTowar.Text = "...";
			this.buttonTowar.UseVisualStyleBackColor = true;
			// 
			// numericUpDownIlosc
			// 
			this.numericUpDownIlosc.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.numericUpDownIlosc.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bindingSource, "Ilosc", true));
			this.numericUpDownIlosc.DecimalPlaces = 3;
			this.numericUpDownIlosc.Location = new System.Drawing.Point(366, 4);
			this.numericUpDownIlosc.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDownIlosc.Name = "numericUpDownIlosc";
			this.numericUpDownIlosc.Size = new System.Drawing.Size(73, 23);
			this.numericUpDownIlosc.TabIndex = 3;
			this.numericUpDownIlosc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// labelJednostka
			// 
			this.labelJednostka.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelJednostka.AutoSize = true;
			this.labelJednostka.Location = new System.Drawing.Point(445, 8);
			this.labelJednostka.Name = "labelJednostka";
			this.labelJednostka.Size = new System.Drawing.Size(30, 15);
			this.labelJednostka.TabIndex = 4;
			this.labelJednostka.Text = "[JM]";
			// 
			// PozycjaFakturyEdytor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "PozycjaFakturyEdytor";
			this.Size = new System.Drawing.Size(484, 201);
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownIlosc)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.BindingSource bindingSource;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.NumericUpDown numericUpDown3;
		private System.Windows.Forms.NumericUpDown numericUpDown4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown numericUpDown5;
		private System.Windows.Forms.NumericUpDown numericUpDown6;
		private System.Windows.Forms.NumericUpDown numericUpDown7;
		private System.Windows.Forms.ComboBox comboBoxTowar;
		private System.Windows.Forms.Button buttonTowar;
		private System.Windows.Forms.NumericUpDown numericUpDownIlosc;
		private System.Windows.Forms.Label labelJednostka;
		private System.Windows.Forms.CheckBox checkBoxWedlugBrutto;
		private System.Windows.Forms.CheckBox checkBoxRecznie;
	}
}
