namespace ProFak.UI;

partial class KonfiguracjaSpisu
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
		splitContainer1 = new System.Windows.Forms.SplitContainer();
		listBoxKolumny = new System.Windows.Forms.ListBox();
		tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		label1 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		label3 = new System.Windows.Forms.Label();
		textBoxKolumna = new System.Windows.Forms.TextBox();
		numericUpDownSzerokosc = new System.Windows.Forms.NumericUpDown();
		numericUpDownKolejnosc = new System.Windows.Forms.NumericUpDown();
		checkBoxUkryta = new System.Windows.Forms.CheckBox();
		checkBoxRozciagnij = new System.Windows.Forms.CheckBox();
		((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
		splitContainer1.Panel1.SuspendLayout();
		splitContainer1.Panel2.SuspendLayout();
		splitContainer1.SuspendLayout();
		tableLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDownSzerokosc).BeginInit();
		((System.ComponentModel.ISupportInitialize)numericUpDownKolejnosc).BeginInit();
		SuspendLayout();
		// 
		// splitContainer1
		// 
		splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		splitContainer1.Location = new System.Drawing.Point(0, 0);
		splitContainer1.Name = "splitContainer1";
		// 
		// splitContainer1.Panel1
		// 
		splitContainer1.Panel1.Controls.Add(listBoxKolumny);
		// 
		// splitContainer1.Panel2
		// 
		splitContainer1.Panel2.Controls.Add(tableLayoutPanel1);
		splitContainer1.Size = new System.Drawing.Size(562, 140);
		splitContainer1.SplitterDistance = 282;
		splitContainer1.TabIndex = 0;
		// 
		// listBoxKolumny
		// 
		listBoxKolumny.Dock = System.Windows.Forms.DockStyle.Fill;
		listBoxKolumny.FormattingEnabled = true;
		listBoxKolumny.ItemHeight = 15;
		listBoxKolumny.Location = new System.Drawing.Point(0, 0);
		listBoxKolumny.Name = "listBoxKolumny";
		listBoxKolumny.Size = new System.Drawing.Size(282, 140);
		listBoxKolumny.TabIndex = 0;
		listBoxKolumny.SelectedIndexChanged += listBoxKolumny_SelectedIndexChanged;
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.ColumnCount = 2;
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
		tableLayoutPanel1.Controls.Add(label1, 0, 0);
		tableLayoutPanel1.Controls.Add(label2, 0, 1);
		tableLayoutPanel1.Controls.Add(label3, 0, 2);
		tableLayoutPanel1.Controls.Add(textBoxKolumna, 1, 0);
		tableLayoutPanel1.Controls.Add(numericUpDownSzerokosc, 1, 1);
		tableLayoutPanel1.Controls.Add(numericUpDownKolejnosc, 1, 2);
		tableLayoutPanel1.Controls.Add(checkBoxUkryta, 1, 3);
		tableLayoutPanel1.Controls.Add(checkBoxRozciagnij, 1, 4);
		tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 6;
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
		tableLayoutPanel1.Size = new System.Drawing.Size(276, 140);
		tableLayoutPanel1.TabIndex = 0;
		// 
		// label1
		// 
		label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(7, 7);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(55, 15);
		label1.TabIndex = 0;
		label1.Text = "Kolumna";
		// 
		// label2
		// 
		label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(3, 36);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(59, 15);
		label2.TabIndex = 0;
		label2.Text = "Szerokość";
		// 
		// label3
		// 
		label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(4, 65);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(58, 15);
		label3.TabIndex = 0;
		label3.Text = "Kolejność";
		// 
		// textBoxKolumna
		// 
		textBoxKolumna.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		textBoxKolumna.Location = new System.Drawing.Point(68, 3);
		textBoxKolumna.Name = "textBoxKolumna";
		textBoxKolumna.ReadOnly = true;
		textBoxKolumna.Size = new System.Drawing.Size(205, 23);
		textBoxKolumna.TabIndex = 1;
		// 
		// numericUpDownSzerokosc
		// 
		numericUpDownSzerokosc.Location = new System.Drawing.Point(68, 32);
		numericUpDownSzerokosc.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
		numericUpDownSzerokosc.Minimum = new decimal(new int[] { 1, 0, 0, System.Int32.MinValue });
		numericUpDownSzerokosc.Name = "numericUpDownSzerokosc";
		numericUpDownSzerokosc.Size = new System.Drawing.Size(100, 23);
		numericUpDownSzerokosc.TabIndex = 2;
		numericUpDownSzerokosc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		numericUpDownSzerokosc.ValueChanged += numericUpDownSzerokosc_ValueChanged;
		// 
		// numericUpDownKolejnosc
		// 
		numericUpDownKolejnosc.Location = new System.Drawing.Point(68, 61);
		numericUpDownKolejnosc.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
		numericUpDownKolejnosc.Name = "numericUpDownKolejnosc";
		numericUpDownKolejnosc.Size = new System.Drawing.Size(100, 23);
		numericUpDownKolejnosc.TabIndex = 2;
		numericUpDownKolejnosc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		numericUpDownKolejnosc.ValueChanged += numericUpDownKolejnosc_ValueChanged;
		// 
		// checkBoxUkryta
		// 
		checkBoxUkryta.AutoSize = true;
		checkBoxUkryta.Location = new System.Drawing.Point(68, 90);
		checkBoxUkryta.Name = "checkBoxUkryta";
		checkBoxUkryta.Size = new System.Drawing.Size(60, 19);
		checkBoxUkryta.TabIndex = 3;
		checkBoxUkryta.Text = "Ukryta";
		checkBoxUkryta.UseVisualStyleBackColor = true;
		checkBoxUkryta.CheckedChanged += checkBoxUkryta_CheckedChanged;
		// 
		// checkBoxRozciagnij
		// 
		checkBoxRozciagnij.AutoSize = true;
		checkBoxRozciagnij.Location = new System.Drawing.Point(68, 115);
		checkBoxRozciagnij.Name = "checkBoxRozciagnij";
		checkBoxRozciagnij.Size = new System.Drawing.Size(189, 19);
		checkBoxRozciagnij.TabIndex = 3;
		checkBoxRozciagnij.Text = "Rozciągnij do pełnej szerokości";
		checkBoxRozciagnij.UseVisualStyleBackColor = true;
		checkBoxRozciagnij.CheckedChanged += checkBoxRozciagnij_CheckedChanged;
		// 
		// KonfiguracjaSpisu
		// 
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		Controls.Add(splitContainer1);
		Name = "KonfiguracjaSpisu";
		Size = new System.Drawing.Size(562, 140);
		splitContainer1.Panel1.ResumeLayout(false);
		splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
		splitContainer1.ResumeLayout(false);
		tableLayoutPanel1.ResumeLayout(false);
		tableLayoutPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDownSzerokosc).EndInit();
		((System.ComponentModel.ISupportInitialize)numericUpDownKolejnosc).EndInit();
		ResumeLayout(false);
	}

	#endregion

	private System.Windows.Forms.SplitContainer splitContainer1;
	private System.Windows.Forms.ListBox listBoxKolumny;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.TextBox textBoxKolumna;
	private System.Windows.Forms.NumericUpDown numericUpDownSzerokosc;
	private System.Windows.Forms.NumericUpDown numericUpDownKolejnosc;
	private System.Windows.Forms.CheckBox checkBoxUkryta;
	private System.Windows.Forms.CheckBox checkBoxRozciagnij;
}
