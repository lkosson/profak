
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
			tabControl = new System.Windows.Forms.TabControl();
			tabPageObliczenia = new System.Windows.Forms.TabPage();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			numericUpDownPrzychody = new NumericUpDownDPI();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			numericUpDownKoszty = new NumericUpDownDPI();
			numericUpDownPodatek = new NumericUpDownDPI();
			numericUpDownPrzeniesiony = new NumericUpDownDPI();
			numericUpDownDoPrzeniesienia = new NumericUpDownDPI();
			numericUpDownDoWplaty = new NumericUpDownDPI();
			label8 = new System.Windows.Forms.Label();
			numericUpDownSkladkiZus = new NumericUpDownDPI();
			tabPageFakturySprzedazy = new System.Windows.Forms.TabPage();
			tabPageFakturyZakupu = new System.Windows.Forms.TabPage();
			label1 = new System.Windows.Forms.Label();
			tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			dateTimePickerMiesiac = new System.Windows.Forms.DateTimePicker();
			buttonPrzelicz = new ButtonDPI();
			tabControl.SuspendLayout();
			tabPageObliczenia.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownPrzychody).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownKoszty).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownPodatek).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownPrzeniesiony).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownDoPrzeniesienia).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownDoWplaty).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownSkladkiZus).BeginInit();
			tableLayoutPanel2.SuspendLayout();
			SuspendLayout();
			// 
			// tabControl
			// 
			tableLayoutPanel2.SetColumnSpan(tabControl, 4);
			tabControl.Controls.Add(tabPageObliczenia);
			tabControl.Controls.Add(tabPageFakturySprzedazy);
			tabControl.Controls.Add(tabPageFakturyZakupu);
			tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			tabControl.Location = new System.Drawing.Point(3, 34);
			tabControl.Name = "tabControl";
			tabControl.SelectedIndex = 0;
			tabControl.Size = new System.Drawing.Size(794, 388);
			tabControl.TabIndex = 2;
			// 
			// tabPageObliczenia
			// 
			tabPageObliczenia.Controls.Add(tableLayoutPanel1);
			tabPageObliczenia.Location = new System.Drawing.Point(4, 24);
			tabPageObliczenia.Name = "tabPageObliczenia";
			tabPageObliczenia.Padding = new System.Windows.Forms.Padding(3);
			tabPageObliczenia.Size = new System.Drawing.Size(786, 360);
			tabPageObliczenia.TabIndex = 0;
			tabPageObliczenia.Text = "Obliczenia";
			tabPageObliczenia.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(label2, 0, 0);
			tableLayoutPanel1.Controls.Add(label3, 0, 1);
			tableLayoutPanel1.Controls.Add(label4, 0, 3);
			tableLayoutPanel1.Controls.Add(label5, 0, 4);
			tableLayoutPanel1.Controls.Add(numericUpDownPrzychody, 1, 0);
			tableLayoutPanel1.Controls.Add(label6, 0, 5);
			tableLayoutPanel1.Controls.Add(label7, 0, 6);
			tableLayoutPanel1.Controls.Add(numericUpDownKoszty, 1, 1);
			tableLayoutPanel1.Controls.Add(numericUpDownPodatek, 1, 3);
			tableLayoutPanel1.Controls.Add(numericUpDownPrzeniesiony, 1, 4);
			tableLayoutPanel1.Controls.Add(numericUpDownDoPrzeniesienia, 1, 5);
			tableLayoutPanel1.Controls.Add(numericUpDownDoWplaty, 1, 6);
			tableLayoutPanel1.Controls.Add(label8, 0, 2);
			tableLayoutPanel1.Controls.Add(numericUpDownSkladkiZus, 1, 2);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 8;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new System.Drawing.Size(780, 354);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// label2
			// 
			label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(34, 7);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(62, 15);
			label2.TabIndex = 2;
			label2.Text = "Przychody";
			// 
			// label3
			// 
			label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(55, 36);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(41, 15);
			label3.TabIndex = 2;
			label3.Text = "Koszty";
			// 
			// label4
			// 
			label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(46, 94);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(50, 15);
			label4.TabIndex = 2;
			label4.Text = "Podatek";
			// 
			// label5
			// 
			label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(23, 123);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(73, 15);
			label5.TabIndex = 2;
			label5.Text = "Przeniesiony";
			// 
			// numericUpDownPrzychody
			// 
			numericUpDownPrzychody.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			numericUpDownPrzychody.DecimalPlaces = 2;
			numericUpDownPrzychody.Location = new System.Drawing.Point(102, 3);
			numericUpDownPrzychody.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
			numericUpDownPrzychody.Name = "numericUpDownPrzychody";
			numericUpDownPrzychody.Size = new System.Drawing.Size(675, 23);
			numericUpDownPrzychody.TabIndex = 3;
			numericUpDownPrzychody.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label6
			// 
			label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(3, 152);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(93, 15);
			label6.TabIndex = 2;
			label6.Text = "Do przeniesienia";
			// 
			// label7
			// 
			label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(36, 181);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(60, 15);
			label7.TabIndex = 2;
			label7.Text = "Do wpłaty";
			// 
			// numericUpDownKoszty
			// 
			numericUpDownKoszty.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			numericUpDownKoszty.DecimalPlaces = 2;
			numericUpDownKoszty.Location = new System.Drawing.Point(102, 32);
			numericUpDownKoszty.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
			numericUpDownKoszty.Name = "numericUpDownKoszty";
			numericUpDownKoszty.Size = new System.Drawing.Size(675, 23);
			numericUpDownKoszty.TabIndex = 4;
			numericUpDownKoszty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDownPodatek
			// 
			numericUpDownPodatek.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			numericUpDownPodatek.DecimalPlaces = 2;
			numericUpDownPodatek.Location = new System.Drawing.Point(102, 90);
			numericUpDownPodatek.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
			numericUpDownPodatek.Name = "numericUpDownPodatek";
			numericUpDownPodatek.Size = new System.Drawing.Size(675, 23);
			numericUpDownPodatek.TabIndex = 6;
			numericUpDownPodatek.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDownPrzeniesiony
			// 
			numericUpDownPrzeniesiony.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			numericUpDownPrzeniesiony.DecimalPlaces = 2;
			numericUpDownPrzeniesiony.Location = new System.Drawing.Point(102, 119);
			numericUpDownPrzeniesiony.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
			numericUpDownPrzeniesiony.Name = "numericUpDownPrzeniesiony";
			numericUpDownPrzeniesiony.Size = new System.Drawing.Size(675, 23);
			numericUpDownPrzeniesiony.TabIndex = 7;
			numericUpDownPrzeniesiony.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDownDoPrzeniesienia
			// 
			numericUpDownDoPrzeniesienia.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			numericUpDownDoPrzeniesienia.DecimalPlaces = 2;
			numericUpDownDoPrzeniesienia.Location = new System.Drawing.Point(102, 148);
			numericUpDownDoPrzeniesienia.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
			numericUpDownDoPrzeniesienia.Name = "numericUpDownDoPrzeniesienia";
			numericUpDownDoPrzeniesienia.Size = new System.Drawing.Size(675, 23);
			numericUpDownDoPrzeniesienia.TabIndex = 8;
			numericUpDownDoPrzeniesienia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDownDoWplaty
			// 
			numericUpDownDoWplaty.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			numericUpDownDoWplaty.DecimalPlaces = 2;
			numericUpDownDoWplaty.Location = new System.Drawing.Point(102, 177);
			numericUpDownDoWplaty.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
			numericUpDownDoWplaty.Name = "numericUpDownDoWplaty";
			numericUpDownDoWplaty.Size = new System.Drawing.Size(675, 23);
			numericUpDownDoWplaty.TabIndex = 9;
			numericUpDownDoWplaty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label8
			// 
			label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(30, 65);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(66, 15);
			label8.TabIndex = 2;
			label8.Text = "Składki Zus";
			// 
			// numericUpDownSkladkiZus
			// 
			numericUpDownSkladkiZus.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			numericUpDownSkladkiZus.DecimalPlaces = 2;
			numericUpDownSkladkiZus.Location = new System.Drawing.Point(102, 61);
			numericUpDownSkladkiZus.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
			numericUpDownSkladkiZus.Name = "numericUpDownSkladkiZus";
			numericUpDownSkladkiZus.Size = new System.Drawing.Size(675, 23);
			numericUpDownSkladkiZus.TabIndex = 5;
			numericUpDownSkladkiZus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// tabPageFakturySprzedazy
			// 
			tabPageFakturySprzedazy.Location = new System.Drawing.Point(4, 24);
			tabPageFakturySprzedazy.Name = "tabPageFakturySprzedazy";
			tabPageFakturySprzedazy.Padding = new System.Windows.Forms.Padding(3);
			tabPageFakturySprzedazy.Size = new System.Drawing.Size(786, 360);
			tabPageFakturySprzedazy.TabIndex = 2;
			tabPageFakturySprzedazy.Text = "Sprzedaż";
			tabPageFakturySprzedazy.UseVisualStyleBackColor = true;
			// 
			// tabPageFakturyZakupu
			// 
			tabPageFakturyZakupu.Location = new System.Drawing.Point(4, 24);
			tabPageFakturyZakupu.Name = "tabPageFakturyZakupu";
			tabPageFakturyZakupu.Padding = new System.Windows.Forms.Padding(3);
			tabPageFakturyZakupu.Size = new System.Drawing.Size(786, 360);
			tabPageFakturyZakupu.TabIndex = 3;
			tabPageFakturyZakupu.Text = "Zakup";
			tabPageFakturyZakupu.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 8);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(47, 15);
			label1.TabIndex = 2;
			label1.Text = "Miesiąc";
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 4;
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel2.Controls.Add(label1, 0, 0);
			tableLayoutPanel2.Controls.Add(tabControl, 0, 1);
			tableLayoutPanel2.Controls.Add(dateTimePickerMiesiac, 1, 0);
			tableLayoutPanel2.Controls.Add(buttonPrzelicz, 2, 0);
			tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 2;
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel2.Size = new System.Drawing.Size(800, 425);
			tableLayoutPanel2.TabIndex = 3;
			// 
			// dateTimePickerMiesiac
			// 
			dateTimePickerMiesiac.Anchor = System.Windows.Forms.AnchorStyles.Left;
			dateTimePickerMiesiac.CustomFormat = "MMMM yyyy";
			dateTimePickerMiesiac.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerMiesiac.Location = new System.Drawing.Point(56, 4);
			dateTimePickerMiesiac.Name = "dateTimePickerMiesiac";
			dateTimePickerMiesiac.Size = new System.Drawing.Size(200, 23);
			dateTimePickerMiesiac.TabIndex = 3;
			// 
			// buttonPrzelicz
			// 
			buttonPrzelicz.Anchor = System.Windows.Forms.AnchorStyles.Left;
			buttonPrzelicz.AutoSize = true;
			buttonPrzelicz.Location = new System.Drawing.Point(262, 3);
			buttonPrzelicz.Name = "buttonPrzelicz";
			buttonPrzelicz.Size = new System.Drawing.Size(75, 25);
			buttonPrzelicz.TabIndex = 4;
			buttonPrzelicz.Text = "Przelicz";
			buttonPrzelicz.UseVisualStyleBackColor = true;
			buttonPrzelicz.Click += buttonPrzelicz_Click;
			// 
			// ZaliczkaPitEdytor
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(tableLayoutPanel2);
			MinimumSize = new System.Drawing.Size(800, 425);
			Name = "ZaliczkaPitEdytor";
			Size = new System.Drawing.Size(800, 425);
			tabControl.ResumeLayout(false);
			tabPageObliczenia.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownPrzychody).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownKoszty).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownPodatek).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownPrzeniesiony).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownDoPrzeniesienia).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownDoWplaty).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownSkladkiZus).EndInit();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			ResumeLayout(false);
		}

		#endregion
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageObliczenia;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.TabPage tabPageFakturySprzedazy;
		private System.Windows.Forms.TabPage tabPageFakturyZakupu;
		private System.Windows.Forms.DateTimePicker dateTimePickerMiesiac;
		private ButtonDPI buttonPrzelicz;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private NumericUpDownDPI numericUpDownPrzychody;
		private NumericUpDownDPI numericUpDownKoszty;
		private NumericUpDownDPI numericUpDownPodatek;
		private NumericUpDownDPI numericUpDownPrzeniesiony;
		private NumericUpDownDPI numericUpDownDoPrzeniesienia;
		private NumericUpDownDPI numericUpDownDoWplaty;
		private System.Windows.Forms.Label label8;
		private NumericUpDownDPI numericUpDownSkladkiZus;
	}
}
