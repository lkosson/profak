namespace ProFak.UI.Faktury;

partial class WysylkaFakturEdytor
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
		tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		textBoxTresc = new System.Windows.Forms.TextBox();
		textBoxTemat = new System.Windows.Forms.TextBox();
		textBoxAdresat = new System.Windows.Forms.TextBox();
		comboBoxFaktura = new System.Windows.Forms.ComboBox();
		label1 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		buttonPoprzednia = new ButtonDPI();
		buttonNastepna = new ButtonDPI();
		label3 = new System.Windows.Forms.Label();
		buttonWyslij = new ButtonDPI();
		flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
		checkBoxUstawDate = new System.Windows.Forms.CheckBox();
		checkBoxPrzeliczTermin = new System.Windows.Forms.CheckBox();
		tableLayoutPanel1.SuspendLayout();
		flowLayoutPanel1.SuspendLayout();
		SuspendLayout();
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.ColumnCount = 5;
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
		tableLayoutPanel1.Controls.Add(textBoxTresc, 0, 4);
		tableLayoutPanel1.Controls.Add(textBoxTemat, 1, 2);
		tableLayoutPanel1.Controls.Add(textBoxAdresat, 1, 1);
		tableLayoutPanel1.Controls.Add(comboBoxFaktura, 1, 0);
		tableLayoutPanel1.Controls.Add(label1, 0, 2);
		tableLayoutPanel1.Controls.Add(label2, 0, 1);
		tableLayoutPanel1.Controls.Add(buttonPoprzednia, 2, 0);
		tableLayoutPanel1.Controls.Add(buttonNastepna, 3, 0);
		tableLayoutPanel1.Controls.Add(label3, 0, 0);
		tableLayoutPanel1.Controls.Add(buttonWyslij, 4, 0);
		tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 3);
		tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 5;
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
		tableLayoutPanel1.Size = new System.Drawing.Size(560, 440);
		tableLayoutPanel1.TabIndex = 0;
		// 
		// textBoxTresc
		// 
		textBoxTresc.AcceptsReturn = true;
		tableLayoutPanel1.SetColumnSpan(textBoxTresc, 5);
		textBoxTresc.Dock = System.Windows.Forms.DockStyle.Fill;
		textBoxTresc.Font = new System.Drawing.Font("Consolas", 9F);
		textBoxTresc.Location = new System.Drawing.Point(3, 123);
		textBoxTresc.Multiline = true;
		textBoxTresc.Name = "textBoxTresc";
		textBoxTresc.Size = new System.Drawing.Size(554, 314);
		textBoxTresc.TabIndex = 4;
		textBoxTresc.TextChanged += textBoxTresc_TextChanged;
		// 
		// textBoxTemat
		// 
		textBoxTemat.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(textBoxTemat, 4);
		textBoxTemat.Location = new System.Drawing.Point(56, 63);
		textBoxTemat.Name = "textBoxTemat";
		textBoxTemat.Size = new System.Drawing.Size(501, 23);
		textBoxTemat.TabIndex = 2;
		textBoxTemat.TextChanged += textBoxTemat_TextChanged;
		// 
		// textBoxAdresat
		// 
		textBoxAdresat.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(textBoxAdresat, 4);
		textBoxAdresat.Location = new System.Drawing.Point(56, 34);
		textBoxAdresat.Name = "textBoxAdresat";
		textBoxAdresat.Size = new System.Drawing.Size(501, 23);
		textBoxAdresat.TabIndex = 1;
		textBoxAdresat.TextChanged += textBoxAdresat_TextChanged;
		// 
		// comboBoxFaktura
		// 
		comboBoxFaktura.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		comboBoxFaktura.DisplayMember = "Numer";
		comboBoxFaktura.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		comboBoxFaktura.FormattingEnabled = true;
		comboBoxFaktura.Location = new System.Drawing.Point(56, 4);
		comboBoxFaktura.Name = "comboBoxFaktura";
		comboBoxFaktura.Size = new System.Drawing.Size(259, 23);
		comboBoxFaktura.TabIndex = 0;
		comboBoxFaktura.SelectedIndexChanged += comboBoxFaktura_SelectedIndexChanged;
		// 
		// label1
		// 
		label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(11, 67);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(39, 15);
		label1.TabIndex = 1;
		label1.Text = "Temat";
		// 
		// label2
		// 
		label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(3, 38);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(47, 15);
		label2.TabIndex = 1;
		label2.Text = "Adresat";
		// 
		// buttonPoprzednia
		// 
		buttonPoprzednia.Anchor = System.Windows.Forms.AnchorStyles.Left;
		buttonPoprzednia.AutoSize = true;
		buttonPoprzednia.Location = new System.Drawing.Point(321, 3);
		buttonPoprzednia.Name = "buttonPoprzednia";
		buttonPoprzednia.Size = new System.Drawing.Size(85, 25);
		buttonPoprzednia.TabIndex = 6;
		buttonPoprzednia.Text = "« Poprzednia";
		buttonPoprzednia.UseVisualStyleBackColor = true;
		buttonPoprzednia.Click += buttonPoprzednia_Click;
		// 
		// buttonNastepna
		// 
		buttonNastepna.Anchor = System.Windows.Forms.AnchorStyles.Left;
		buttonNastepna.AutoSize = true;
		buttonNastepna.Location = new System.Drawing.Point(412, 3);
		buttonNastepna.Name = "buttonNastepna";
		buttonNastepna.Size = new System.Drawing.Size(76, 25);
		buttonNastepna.TabIndex = 7;
		buttonNastepna.Text = "» Następna";
		buttonNastepna.UseVisualStyleBackColor = true;
		buttonNastepna.Click += buttonNastepna_Click;
		// 
		// label3
		// 
		label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(4, 8);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(46, 15);
		label3.TabIndex = 1;
		label3.Text = "Faktura";
		// 
		// buttonWyslij
		// 
		buttonWyslij.Anchor = System.Windows.Forms.AnchorStyles.Left;
		buttonWyslij.AutoSize = true;
		buttonWyslij.Location = new System.Drawing.Point(494, 3);
		buttonWyslij.Name = "buttonWyslij";
		buttonWyslij.Size = new System.Drawing.Size(63, 25);
		buttonWyslij.TabIndex = 5;
		buttonWyslij.Text = "✉ Wyślij";
		buttonWyslij.UseVisualStyleBackColor = true;
		buttonWyslij.Click += buttonWyslij_Click;
		// 
		// flowLayoutPanel1
		// 
		flowLayoutPanel1.AutoSize = true;
		tableLayoutPanel1.SetColumnSpan(flowLayoutPanel1, 5);
		flowLayoutPanel1.Controls.Add(checkBoxUstawDate);
		flowLayoutPanel1.Controls.Add(checkBoxPrzeliczTermin);
		flowLayoutPanel1.Location = new System.Drawing.Point(3, 92);
		flowLayoutPanel1.Name = "flowLayoutPanel1";
		flowLayoutPanel1.Size = new System.Drawing.Size(412, 25);
		flowLayoutPanel1.TabIndex = 3;
		// 
		// checkBoxUstawDate
		// 
		checkBoxUstawDate.AutoSize = true;
		checkBoxUstawDate.Location = new System.Drawing.Point(3, 3);
		checkBoxUstawDate.Name = "checkBoxUstawDate";
		checkBoxUstawDate.Size = new System.Drawing.Size(240, 19);
		checkBoxUstawDate.TabIndex = 0;
		checkBoxUstawDate.Text = "Ustaw datę wystawienia na bieżący dzień";
		checkBoxUstawDate.UseVisualStyleBackColor = true;
		checkBoxUstawDate.CheckedChanged += checkBoxUstawDate_CheckedChanged;
		// 
		// checkBoxPrzeliczTermin
		// 
		checkBoxPrzeliczTermin.AutoSize = true;
		checkBoxPrzeliczTermin.Enabled = false;
		checkBoxPrzeliczTermin.Location = new System.Drawing.Point(249, 3);
		checkBoxPrzeliczTermin.Name = "checkBoxPrzeliczTermin";
		checkBoxPrzeliczTermin.Size = new System.Drawing.Size(160, 19);
		checkBoxPrzeliczTermin.TabIndex = 1;
		checkBoxPrzeliczTermin.Text = "i przelicz termin płatności";
		checkBoxPrzeliczTermin.UseVisualStyleBackColor = true;
		// 
		// WysylkaFakturEdytor
		// 
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		Controls.Add(tableLayoutPanel1);
		MinimumSize = new System.Drawing.Size(560, 440);
		Name = "WysylkaFakturEdytor";
		Size = new System.Drawing.Size(560, 440);
		tableLayoutPanel1.ResumeLayout(false);
		tableLayoutPanel1.PerformLayout();
		flowLayoutPanel1.ResumeLayout(false);
		flowLayoutPanel1.PerformLayout();
		ResumeLayout(false);
	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.TextBox textBoxTresc;
	private System.Windows.Forms.TextBox textBoxTemat;
	private System.Windows.Forms.TextBox textBoxAdresat;
	private System.Windows.Forms.ComboBox comboBoxFaktura;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label label2;
	private ButtonDPI buttonPoprzednia;
	private ButtonDPI buttonNastepna;
	private System.Windows.Forms.Label label3;
	private ButtonDPI buttonWyslij;
	private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	private System.Windows.Forms.CheckBox checkBoxUstawDate;
	private System.Windows.Forms.CheckBox checkBoxPrzeliczTermin;
}
