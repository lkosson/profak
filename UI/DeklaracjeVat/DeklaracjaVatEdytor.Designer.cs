
namespace ProFak.UI;

partial class DeklaracjaVatEdytor
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
		tabControl = new TabControl();
		tabPageObliczenia = new TabPage();
		tabPageFakturySprzedazy = new TabPage();
		tabPageFakturyZakupu = new TabPage();
		label1 = new Label();
		tableLayoutPanel2 = new TableLayoutPanel();
		dateTimePickerMiesiac = new DateTimePickerFix();
		buttonPrzelicz = new ButtonDPI();
		((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
		tabControl.SuspendLayout();
		tableLayoutPanel2.SuspendLayout();
		SuspendLayout();
		// 
		// tabControl
		// 
		tableLayoutPanel2.SetColumnSpan(tabControl, 4);
		tabControl.Controls.Add(tabPageObliczenia);
		tabControl.Controls.Add(tabPageFakturySprzedazy);
		tabControl.Controls.Add(tabPageFakturyZakupu);
		tabControl.Dock = DockStyle.Fill;
		tabControl.Location = new Point(3, 34);
		tabControl.Name = "tabControl";
		tabControl.SelectedIndex = 0;
		tabControl.Size = new Size(794, 388);
		tabControl.TabIndex = 2;
		// 
		// tabPageObliczenia
		// 
		tabPageObliczenia.Location = new Point(4, 24);
		tabPageObliczenia.Name = "tabPageObliczenia";
		tabPageObliczenia.Padding = new Padding(3);
		tabPageObliczenia.Size = new Size(786, 360);
		tabPageObliczenia.TabIndex = 0;
		tabPageObliczenia.Text = "Obliczenia";
		tabPageObliczenia.UseVisualStyleBackColor = true;
		// 
		// tabPageFakturySprzedazy
		// 
		tabPageFakturySprzedazy.Location = new Point(4, 24);
		tabPageFakturySprzedazy.Name = "tabPageFakturySprzedazy";
		tabPageFakturySprzedazy.Padding = new Padding(3);
		tabPageFakturySprzedazy.Size = new Size(786, 360);
		tabPageFakturySprzedazy.TabIndex = 2;
		tabPageFakturySprzedazy.Text = "Sprzedaż";
		tabPageFakturySprzedazy.UseVisualStyleBackColor = true;
		// 
		// tabPageFakturyZakupu
		// 
		tabPageFakturyZakupu.Location = new Point(4, 24);
		tabPageFakturyZakupu.Name = "tabPageFakturyZakupu";
		tabPageFakturyZakupu.Padding = new Padding(3);
		tabPageFakturyZakupu.Size = new Size(786, 360);
		tabPageFakturyZakupu.TabIndex = 3;
		tabPageFakturyZakupu.Text = "Zakup";
		tabPageFakturyZakupu.UseVisualStyleBackColor = true;
		// 
		// label1
		// 
		label1.Anchor = AnchorStyles.Right;
		label1.AutoSize = true;
		label1.Location = new Point(3, 8);
		label1.Name = "label1";
		label1.Size = new Size(47, 15);
		label1.TabIndex = 2;
		label1.Text = "Miesiąc";
		// 
		// tableLayoutPanel2
		// 
		tableLayoutPanel2.ColumnCount = 4;
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.Controls.Add(label1, 0, 0);
		tableLayoutPanel2.Controls.Add(tabControl, 0, 1);
		tableLayoutPanel2.Controls.Add(dateTimePickerMiesiac, 1, 0);
		tableLayoutPanel2.Controls.Add(buttonPrzelicz, 2, 0);
		tableLayoutPanel2.Dock = DockStyle.Fill;
		tableLayoutPanel2.Location = new Point(0, 0);
		tableLayoutPanel2.Name = "tableLayoutPanel2";
		tableLayoutPanel2.RowCount = 2;
		tableLayoutPanel2.RowStyles.Add(new RowStyle());
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.Size = new Size(800, 425);
		tableLayoutPanel2.TabIndex = 3;
		// 
		// dateTimePickerMiesiac
		// 
		dateTimePickerMiesiac.Anchor = AnchorStyles.Left;
		dateTimePickerMiesiac.CustomFormat = "MM-yyyy";
		dateTimePickerMiesiac.Format = DateTimePickerFormat.Custom;
		dateTimePickerMiesiac.Location = new Point(56, 4);
		dateTimePickerMiesiac.Name = "dateTimePickerMiesiac";
		dateTimePickerMiesiac.Size = new Size(200, 23);
		dateTimePickerMiesiac.TabIndex = 3;
		// 
		// buttonPrzelicz
		// 
		buttonPrzelicz.Anchor = AnchorStyles.Left;
		buttonPrzelicz.AutoSize = true;
		buttonPrzelicz.Location = new Point(262, 3);
		buttonPrzelicz.Name = "buttonPrzelicz";
		buttonPrzelicz.Size = new Size(75, 25);
		buttonPrzelicz.TabIndex = 4;
		buttonPrzelicz.Text = "Przelicz";
		buttonPrzelicz.UseVisualStyleBackColor = true;
		buttonPrzelicz.Click += buttonPrzelicz_Click;
		// 
		// DeklaracjaVatEdytor
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		Controls.Add(tableLayoutPanel2);
		MinimumSize = new Size(800, 425);
		Name = "DeklaracjaVatEdytor";
		Size = new Size(800, 425);
		((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
		tabControl.ResumeLayout(false);
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
	private ButtonDPI buttonPrzelicz;
        private System.Windows.Forms.DateTimePickerFix dateTimePickerMiesiac;
    }
