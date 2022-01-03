
namespace ProFak.UI
{
	partial class ZaliczkaPitEdytor
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPageObliczenia = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.numericUpDownPrzychody = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownKoszty = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownPodatek = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownPrzeniesiony = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownDoPrzeniesienia = new System.Windows.Forms.NumericUpDown();
			this.numericUpDownDoWplaty = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.numericUpDownSkladkiZus = new System.Windows.Forms.NumericUpDown();
			this.tabPageFakturySprzedazy = new System.Windows.Forms.TabPage();
			this.tabPageFakturyZakupu = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.dateTimePickerMiesiac = new System.Windows.Forms.DateTimePicker();
			this.buttonPrzelicz = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.tabPageObliczenia.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrzychody)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownKoszty)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPodatek)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrzeniesiony)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDoPrzeniesienia)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDoWplaty)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSkladkiZus)).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tableLayoutPanel2.SetColumnSpan(this.tabControl, 4);
			this.tabControl.Controls.Add(this.tabPageObliczenia);
			this.tabControl.Controls.Add(this.tabPageFakturySprzedazy);
			this.tabControl.Controls.Add(this.tabPageFakturyZakupu);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(3, 34);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(794, 388);
			this.tabControl.TabIndex = 2;
			// 
			// tabPageObliczenia
			// 
			this.tabPageObliczenia.Controls.Add(this.tableLayoutPanel1);
			this.tabPageObliczenia.Location = new System.Drawing.Point(4, 24);
			this.tabPageObliczenia.Name = "tabPageObliczenia";
			this.tabPageObliczenia.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageObliczenia.Size = new System.Drawing.Size(786, 360);
			this.tabPageObliczenia.TabIndex = 0;
			this.tabPageObliczenia.Text = "Obliczenia";
			this.tabPageObliczenia.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.numericUpDownPrzychody, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.numericUpDownKoszty, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.numericUpDownPodatek, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.numericUpDownPrzeniesiony, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.numericUpDownDoPrzeniesienia, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.numericUpDownDoWplaty, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.label8, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.numericUpDownSkladkiZus, 1, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 8;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 354);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(34, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(62, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "Przychody";
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(55, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(41, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "Koszty";
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(46, 94);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 15);
			this.label4.TabIndex = 2;
			this.label4.Text = "Podatek";
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(23, 123);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(73, 15);
			this.label5.TabIndex = 2;
			this.label5.Text = "Przeniesiony";
			// 
			// label6
			// 
			this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(3, 152);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(93, 15);
			this.label6.TabIndex = 2;
			this.label6.Text = "Do przeniesienia";
			// 
			// label7
			// 
			this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(36, 181);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(60, 15);
			this.label7.TabIndex = 2;
			this.label7.Text = "Do wpłaty";
			// 
			// numericUpDownPrzychody
			// 
			this.numericUpDownPrzychody.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownPrzychody.DecimalPlaces = 2;
			this.numericUpDownPrzychody.Location = new System.Drawing.Point(102, 3);
			this.numericUpDownPrzychody.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDownPrzychody.Name = "numericUpDownPrzychody";
			this.numericUpDownPrzychody.Size = new System.Drawing.Size(675, 23);
			this.numericUpDownPrzychody.TabIndex = 3;
			this.numericUpDownPrzychody.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDownKoszty
			// 
			this.numericUpDownKoszty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownKoszty.DecimalPlaces = 2;
			this.numericUpDownKoszty.Location = new System.Drawing.Point(102, 32);
			this.numericUpDownKoszty.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDownKoszty.Name = "numericUpDownKoszty";
			this.numericUpDownKoszty.Size = new System.Drawing.Size(675, 23);
			this.numericUpDownKoszty.TabIndex = 4;
			this.numericUpDownKoszty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDownPodatek
			// 
			this.numericUpDownPodatek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownPodatek.DecimalPlaces = 2;
			this.numericUpDownPodatek.Location = new System.Drawing.Point(102, 90);
			this.numericUpDownPodatek.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDownPodatek.Name = "numericUpDownPodatek";
			this.numericUpDownPodatek.Size = new System.Drawing.Size(675, 23);
			this.numericUpDownPodatek.TabIndex = 6;
			this.numericUpDownPodatek.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDownPrzeniesiony
			// 
			this.numericUpDownPrzeniesiony.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownPrzeniesiony.DecimalPlaces = 2;
			this.numericUpDownPrzeniesiony.Location = new System.Drawing.Point(102, 119);
			this.numericUpDownPrzeniesiony.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDownPrzeniesiony.Name = "numericUpDownPrzeniesiony";
			this.numericUpDownPrzeniesiony.Size = new System.Drawing.Size(675, 23);
			this.numericUpDownPrzeniesiony.TabIndex = 7;
			this.numericUpDownPrzeniesiony.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDownDoPrzeniesienia
			// 
			this.numericUpDownDoPrzeniesienia.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownDoPrzeniesienia.DecimalPlaces = 2;
			this.numericUpDownDoPrzeniesienia.Location = new System.Drawing.Point(102, 148);
			this.numericUpDownDoPrzeniesienia.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDownDoPrzeniesienia.Name = "numericUpDownDoPrzeniesienia";
			this.numericUpDownDoPrzeniesienia.Size = new System.Drawing.Size(675, 23);
			this.numericUpDownDoPrzeniesienia.TabIndex = 8;
			this.numericUpDownDoPrzeniesienia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDownDoWplaty
			// 
			this.numericUpDownDoWplaty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownDoWplaty.DecimalPlaces = 2;
			this.numericUpDownDoWplaty.Location = new System.Drawing.Point(102, 177);
			this.numericUpDownDoWplaty.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDownDoWplaty.Name = "numericUpDownDoWplaty";
			this.numericUpDownDoWplaty.Size = new System.Drawing.Size(675, 23);
			this.numericUpDownDoWplaty.TabIndex = 9;
			this.numericUpDownDoWplaty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label8
			// 
			this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(30, 65);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(66, 15);
			this.label8.TabIndex = 2;
			this.label8.Text = "Składki Zus";
			// 
			// numericUpDownSkladkiZus
			// 
			this.numericUpDownSkladkiZus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownSkladkiZus.DecimalPlaces = 2;
			this.numericUpDownSkladkiZus.Location = new System.Drawing.Point(102, 61);
			this.numericUpDownSkladkiZus.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDownSkladkiZus.Name = "numericUpDownSkladkiZus";
			this.numericUpDownSkladkiZus.Size = new System.Drawing.Size(675, 23);
			this.numericUpDownSkladkiZus.TabIndex = 5;
			this.numericUpDownSkladkiZus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// tabPageFakturySprzedazy
			// 
			this.tabPageFakturySprzedazy.Location = new System.Drawing.Point(4, 24);
			this.tabPageFakturySprzedazy.Name = "tabPageFakturySprzedazy";
			this.tabPageFakturySprzedazy.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageFakturySprzedazy.Size = new System.Drawing.Size(786, 360);
			this.tabPageFakturySprzedazy.TabIndex = 2;
			this.tabPageFakturySprzedazy.Text = "Sprzedaż";
			this.tabPageFakturySprzedazy.UseVisualStyleBackColor = true;
			// 
			// tabPageFakturyZakupu
			// 
			this.tabPageFakturyZakupu.Location = new System.Drawing.Point(4, 24);
			this.tabPageFakturyZakupu.Name = "tabPageFakturyZakupu";
			this.tabPageFakturyZakupu.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageFakturyZakupu.Size = new System.Drawing.Size(786, 360);
			this.tabPageFakturyZakupu.TabIndex = 3;
			this.tabPageFakturyZakupu.Text = "Zakup";
			this.tabPageFakturyZakupu.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Miesiąc";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 4;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.tabControl, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.dateTimePickerMiesiac, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.buttonPrzelicz, 2, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(800, 425);
			this.tableLayoutPanel2.TabIndex = 3;
			// 
			// dateTimePickerMiesiac
			// 
			this.dateTimePickerMiesiac.CustomFormat = "MMMM yyyy";
			this.dateTimePickerMiesiac.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePickerMiesiac.Location = new System.Drawing.Point(56, 3);
			this.dateTimePickerMiesiac.Name = "dateTimePickerMiesiac";
			this.dateTimePickerMiesiac.Size = new System.Drawing.Size(200, 23);
			this.dateTimePickerMiesiac.TabIndex = 3;
			// 
			// buttonPrzelicz
			// 
			this.buttonPrzelicz.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.buttonPrzelicz.AutoSize = true;
			this.buttonPrzelicz.Location = new System.Drawing.Point(262, 3);
			this.buttonPrzelicz.Name = "buttonPrzelicz";
			this.buttonPrzelicz.Size = new System.Drawing.Size(75, 25);
			this.buttonPrzelicz.TabIndex = 4;
			this.buttonPrzelicz.Text = "Przelicz";
			this.buttonPrzelicz.UseVisualStyleBackColor = true;
			this.buttonPrzelicz.Click += new System.EventHandler(this.buttonPrzelicz_Click);
			// 
			// ZaliczkaPitEdytor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel2);
			this.MinimumSize = new System.Drawing.Size(800, 425);
			this.Name = "ZaliczkaPitEdytor";
			this.Size = new System.Drawing.Size(800, 425);
			this.tabControl.ResumeLayout(false);
			this.tabPageObliczenia.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrzychody)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownKoszty)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPodatek)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownPrzeniesiony)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDoPrzeniesienia)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDoWplaty)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownSkladkiZus)).EndInit();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageObliczenia;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TabPage tabPageFakturySprzedazy;
		private System.Windows.Forms.TabPage tabPageFakturyZakupu;
		private System.Windows.Forms.DateTimePicker dateTimePickerMiesiac;
		private System.Windows.Forms.Button buttonPrzelicz;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown numericUpDownPrzychody;
		private System.Windows.Forms.NumericUpDown numericUpDownKoszty;
		private System.Windows.Forms.NumericUpDown numericUpDownPodatek;
		private System.Windows.Forms.NumericUpDown numericUpDownPrzeniesiony;
		private System.Windows.Forms.NumericUpDown numericUpDownDoPrzeniesienia;
		private System.Windows.Forms.NumericUpDown numericUpDownDoWplaty;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown numericUpDownSkladkiZus;
	}
}
