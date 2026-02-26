
namespace ProFak.UI;

partial class EdytorTabeli
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
		groupBoxWynik = new System.Windows.Forms.GroupBox();
		dataGridViewWynik = new Spis();
		groupBox1 = new System.Windows.Forms.GroupBox();
		tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		textBoxStatus = new System.Windows.Forms.TextBox();
		numericUpDownIDDo = new NumericUpDownDPI();
		buttonPokaz = new ButtonDPI();
		label2 = new System.Windows.Forms.Label();
		comboBoxTabela = new System.Windows.Forms.ComboBox();
		label1 = new System.Windows.Forms.Label();
		numericUpDownIDOd = new NumericUpDownDPI();
		groupBoxWynik.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)dataGridViewWynik).BeginInit();
		groupBox1.SuspendLayout();
		tableLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDownIDDo).BeginInit();
		((System.ComponentModel.ISupportInitialize)numericUpDownIDOd).BeginInit();
		SuspendLayout();
		// 
		// groupBoxWynik
		// 
		groupBoxWynik.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		groupBoxWynik.Controls.Add(dataGridViewWynik);
		groupBoxWynik.Location = new System.Drawing.Point(3, 66);
		groupBoxWynik.Name = "groupBoxWynik";
		groupBoxWynik.Size = new System.Drawing.Size(777, 213);
		groupBoxWynik.TabIndex = 3;
		groupBoxWynik.TabStop = false;
		groupBoxWynik.Text = "Wynik";
		// 
		// dataGridViewWynik
		// 
		dataGridViewWynik.AllowUserToAddRows = false;
		dataGridViewWynik.AllowUserToDeleteRows = false;
		dataGridViewWynik.AllowUserToOrderColumns = true;
		dataGridViewWynik.AllowUserToResizeRows = false;
		dataGridViewWynik.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dataGridViewWynik.Dock = System.Windows.Forms.DockStyle.Fill;
		dataGridViewWynik.EnableHeadersVisualStyles = false;
		dataGridViewWynik.Location = new System.Drawing.Point(3, 19);
		dataGridViewWynik.Name = "dataGridViewWynik";
		dataGridViewWynik.RowHeadersVisible = false;
		dataGridViewWynik.Size = new System.Drawing.Size(771, 191);
		dataGridViewWynik.TabIndex = 0;
		dataGridViewWynik.CellEndEdit += dataGridViewWynik_CellEndEdit;
		dataGridViewWynik.KeyUp += dataGridViewWynik_KeyUp;
		// 
		// groupBox1
		// 
		groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		groupBox1.Controls.Add(tableLayoutPanel1);
		groupBox1.Location = new System.Drawing.Point(3, 3);
		groupBox1.Name = "groupBox1";
		groupBox1.Size = new System.Drawing.Size(777, 57);
		groupBox1.TabIndex = 0;
		groupBox1.TabStop = false;
		groupBox1.Text = "Zakres danych";
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.ColumnCount = 7;
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
		tableLayoutPanel1.Controls.Add(textBoxStatus, 6, 0);
		tableLayoutPanel1.Controls.Add(numericUpDownIDDo, 4, 0);
		tableLayoutPanel1.Controls.Add(buttonPokaz, 5, 0);
		tableLayoutPanel1.Controls.Add(label2, 3, 0);
		tableLayoutPanel1.Controls.Add(comboBoxTabela, 0, 0);
		tableLayoutPanel1.Controls.Add(label1, 1, 0);
		tableLayoutPanel1.Controls.Add(numericUpDownIDOd, 2, 0);
		tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 1;
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
		tableLayoutPanel1.Size = new System.Drawing.Size(771, 35);
		tableLayoutPanel1.TabIndex = 4;
		// 
		// textBoxStatus
		// 
		textBoxStatus.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		textBoxStatus.Location = new System.Drawing.Point(572, 6);
		textBoxStatus.Name = "textBoxStatus";
		textBoxStatus.ReadOnly = true;
		textBoxStatus.Size = new System.Drawing.Size(196, 23);
		textBoxStatus.TabIndex = 2;
		// 
		// numericUpDownIDDo
		// 
		numericUpDownIDDo.Anchor = System.Windows.Forms.AnchorStyles.Right;
		numericUpDownIDDo.Location = new System.Drawing.Point(365, 6);
		numericUpDownIDDo.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
		numericUpDownIDDo.Name = "numericUpDownIDDo";
		numericUpDownIDDo.Size = new System.Drawing.Size(100, 23);
		numericUpDownIDDo.TabIndex = 4;
		numericUpDownIDDo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		numericUpDownIDDo.Value = new decimal(new int[] { 999999999, 0, 0, 0 });
		// 
		// buttonPokaz
		// 
		buttonPokaz.Anchor = System.Windows.Forms.AnchorStyles.Right;
		buttonPokaz.Location = new System.Drawing.Point(471, 6);
		buttonPokaz.Name = "buttonPokaz";
		buttonPokaz.Size = new System.Drawing.Size(95, 23);
		buttonPokaz.TabIndex = 1;
		buttonPokaz.Text = "Wczytaj";
		buttonPokaz.UseVisualStyleBackColor = true;
		buttonPokaz.Click += buttonUruchom_Click;
		// 
		// label2
		// 
		label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(343, 10);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(16, 15);
		label2.TabIndex = 5;
		label2.Text = "...";
		// 
		// comboBoxTabela
		// 
		comboBoxTabela.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		comboBoxTabela.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		comboBoxTabela.FormattingEnabled = true;
		comboBoxTabela.Location = new System.Drawing.Point(3, 6);
		comboBoxTabela.Name = "comboBoxTabela";
		comboBoxTabela.Size = new System.Drawing.Size(196, 23);
		comboBoxTabela.TabIndex = 3;
		// 
		// label1
		// 
		label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(205, 10);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(26, 15);
		label1.TabIndex = 5;
		label1.Text = "ID=";
		// 
		// numericUpDownIDOd
		// 
		numericUpDownIDOd.Anchor = System.Windows.Forms.AnchorStyles.Right;
		numericUpDownIDOd.Location = new System.Drawing.Point(237, 6);
		numericUpDownIDOd.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
		numericUpDownIDOd.Name = "numericUpDownIDOd";
		numericUpDownIDOd.Size = new System.Drawing.Size(100, 23);
		numericUpDownIDOd.TabIndex = 4;
		numericUpDownIDOd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		// 
		// EdytorTabeli
		// 
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		Controls.Add(groupBoxWynik);
		Controls.Add(groupBox1);
		Name = "EdytorTabeli";
		Size = new System.Drawing.Size(783, 282);
		groupBoxWynik.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)dataGridViewWynik).EndInit();
		groupBox1.ResumeLayout(false);
		tableLayoutPanel1.ResumeLayout(false);
		tableLayoutPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDownIDDo).EndInit();
		((System.ComponentModel.ISupportInitialize)numericUpDownIDOd).EndInit();
		ResumeLayout(false);
	}

	#endregion
	private System.Windows.Forms.GroupBox groupBoxWynik;
	private System.Windows.Forms.GroupBox groupBox1;
	private ButtonDPI buttonPokaz;
	private System.Windows.Forms.TextBox textBoxStatus;
	private Spis dataGridViewWynik;
	private System.Windows.Forms.ComboBox comboBoxTabela;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label label1;
	private NumericUpDownDPI numericUpDownIDDo;
	private NumericUpDownDPI numericUpDownIDOd;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
}
