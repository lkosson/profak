
namespace ProFak.UI;

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
		tableLayoutPanel1 = new TableLayoutPanel();
		label1 = new Label();
		label2 = new Label();
		comboBoxPrzeznaczenie = new ComboBox();
		panelStan = new Panel();
		label3 = new Label();
		comboBoxFormat = new ComboBox();
		textBoxPrzyklad = new TextBox();
		label4 = new Label();
		textBoxGrupa = new TextBox();
		linkLabelGrupa = new LinkLabel();
		((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
		tableLayoutPanel1.SuspendLayout();
		SuspendLayout();
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.ColumnCount = 3;
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.Controls.Add(label1, 0, 0);
		tableLayoutPanel1.Controls.Add(label2, 0, 1);
		tableLayoutPanel1.Controls.Add(comboBoxPrzeznaczenie, 1, 0);
		tableLayoutPanel1.Controls.Add(panelStan, 0, 4);
		tableLayoutPanel1.Controls.Add(label3, 0, 3);
		tableLayoutPanel1.Controls.Add(comboBoxFormat, 1, 1);
		tableLayoutPanel1.Controls.Add(textBoxPrzyklad, 1, 3);
		tableLayoutPanel1.Controls.Add(label4, 0, 2);
		tableLayoutPanel1.Controls.Add(textBoxGrupa, 1, 2);
		tableLayoutPanel1.Controls.Add(linkLabelGrupa, 2, 2);
		tableLayoutPanel1.Dock = DockStyle.Fill;
		tableLayoutPanel1.Location = new Point(0, 0);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 5;
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.Size = new Size(800, 293);
		tableLayoutPanel1.TabIndex = 0;
		// 
		// label1
		// 
		label1.Anchor = AnchorStyles.Right;
		label1.AutoSize = true;
		label1.Location = new Point(34, 7);
		label1.Name = "label1";
		label1.Size = new Size(80, 15);
		label1.TabIndex = 0;
		label1.Text = "Przeznaczenie";
		// 
		// label2
		// 
		label2.Anchor = AnchorStyles.Right;
		label2.AutoSize = true;
		label2.Location = new Point(69, 36);
		label2.Name = "label2";
		label2.Size = new Size(45, 15);
		label2.TabIndex = 0;
		label2.Text = "Format";
		// 
		// comboBoxPrzeznaczenie
		// 
		comboBoxPrzeznaczenie.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(comboBoxPrzeznaczenie, 2);
		comboBoxPrzeznaczenie.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBoxPrzeznaczenie.FormattingEnabled = true;
		comboBoxPrzeznaczenie.Location = new Point(120, 3);
		comboBoxPrzeznaczenie.Name = "comboBoxPrzeznaczenie";
		comboBoxPrzeznaczenie.Size = new Size(677, 23);
		comboBoxPrzeznaczenie.TabIndex = 0;
		// 
		// panelStan
		// 
		tableLayoutPanel1.SetColumnSpan(panelStan, 3);
		panelStan.Dock = DockStyle.Fill;
		panelStan.Location = new Point(3, 119);
		panelStan.Name = "panelStan";
		panelStan.Size = new Size(794, 171);
		panelStan.TabIndex = 4;
		// 
		// label3
		// 
		label3.Anchor = AnchorStyles.Right;
		label3.AutoSize = true;
		label3.Location = new Point(3, 94);
		label3.Name = "label3";
		label3.Size = new Size(111, 15);
		label3.TabIndex = 0;
		label3.Text = "Przykładowy numer";
		// 
		// comboBoxFormat
		// 
		comboBoxFormat.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(comboBoxFormat, 2);
		comboBoxFormat.FormattingEnabled = true;
		comboBoxFormat.Items.AddRange(new object[] { "F/[Numer]", "F/[Numer:000000]", "F/[Numer:0000]/[Rok]", "F/[Numer:0000]/[Miesiac:00]/[Rok]", "F/[Numer]/[Data:yy/MM]", "F/[Numer:0000]/[Data:yyMMdd]" });
		comboBoxFormat.Location = new Point(120, 32);
		comboBoxFormat.Name = "comboBoxFormat";
		comboBoxFormat.Size = new Size(677, 23);
		comboBoxFormat.TabIndex = 1;
		// 
		// textBoxPrzyklad
		// 
		textBoxPrzyklad.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(textBoxPrzyklad, 2);
		textBoxPrzyklad.Location = new Point(120, 90);
		textBoxPrzyklad.Name = "textBoxPrzyklad";
		textBoxPrzyklad.ReadOnly = true;
		textBoxPrzyklad.Size = new Size(677, 23);
		textBoxPrzyklad.TabIndex = 3;
		// 
		// label4
		// 
		label4.Anchor = AnchorStyles.Right;
		label4.AutoSize = true;
		label4.Location = new Point(75, 65);
		label4.Name = "label4";
		label4.Size = new Size(39, 15);
		label4.TabIndex = 0;
		label4.Text = "Grupa";
		// 
		// textBoxGrupa
		// 
		textBoxGrupa.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxGrupa.Location = new Point(120, 61);
		textBoxGrupa.Name = "textBoxGrupa";
		textBoxGrupa.Size = new Size(659, 23);
		textBoxGrupa.TabIndex = 2;
		// 
		// linkLabelGrupa
		// 
		linkLabelGrupa.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		linkLabelGrupa.AutoSize = true;
		linkLabelGrupa.Location = new Point(785, 65);
		linkLabelGrupa.Name = "linkLabelGrupa";
		linkLabelGrupa.Size = new Size(12, 15);
		linkLabelGrupa.TabIndex = 5;
		linkLabelGrupa.TabStop = true;
		linkLabelGrupa.Text = "?";
		linkLabelGrupa.LinkClicked += linkLabelGrupa_LinkClicked;
		// 
		// NumeratorEdytor
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		Controls.Add(tableLayoutPanel1);
		MinimumSize = new Size(800, 250);
		Name = "NumeratorEdytor";
		Size = new Size(800, 293);
		((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
		tableLayoutPanel1.ResumeLayout(false);
		tableLayoutPanel1.PerformLayout();
		ResumeLayout(false);

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
	private Label label4;
	private TextBox textBoxGrupa;
	private LinkLabel linkLabelGrupa;
}
