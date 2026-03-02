
namespace ProFak.UI;

partial class FakturaEdytor
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
		tableLayoutPanel = new TableLayoutPanel();
		tableLayoutPanelKontrahenci = new TableLayoutPanel();
		groupBox2 = new GroupBox();
		tableLayoutPanel5 = new TableLayoutPanel();
		label6 = new Label();
		label7 = new Label();
		label8 = new Label();
		textBoxDaneSprzedawcy = new TextBox();
		buttonSprzedawca = new ButtonDPI();
		comboBoxNIPSprzedawcy = new ComboBoxFix();
		comboBoxNazwaSprzedawcy = new ComboBoxFix();
		buttonNowySprzedawca = new ButtonDPI();
		groupBox3 = new GroupBox();
		tableLayoutPanel6 = new TableLayoutPanel();
		label9 = new Label();
		label10 = new Label();
		label11 = new Label();
		textBoxDaneNabywcy = new TextBox();
		buttonNabywca = new ButtonDPI();
		comboBoxNIPNabywcy = new ComboBoxFix();
		comboBoxNazwaNabywcy = new ComboBoxFix();
		buttonNowyNabywca = new ButtonDPI();
		tabControl1 = new TabControl();
		tabPagePozycje = new TabPage();
		tabPageWplaty = new TabPage();
		tabPagePliki = new TabPage();
		tabPageUwagi = new TabPage();
		tableLayoutPanel2 = new TableLayoutPanel();
		groupBox4 = new GroupBox();
		linkLabelUwagiPomoc = new LinkLabel();
		textBoxUwagiPubliczne = new TextBox();
		groupBox5 = new GroupBox();
		textBoxUwagiWewnetrzne = new TextBox();
		tabPageDodatkowePodmioty = new TabPage();
		tabPagePodatki = new TabPage();
		tableLayoutPanel7 = new TableLayoutPanel();
		label1 = new Label();
		label19 = new Label();
		comboBoxProcentKosztow = new ComboBox();
		comboBoxProcentVat = new ComboBox();
		checkBoxWDT = new CheckBox();
		checkBoxWNT = new CheckBox();
		checkBoxTP = new CheckBox();
		checkBoxZakupSrodkowTrwalych = new CheckBox();
		label20 = new Label();
		textBoxOpisZdarzenia = new TextBox();
		label23 = new Label();
		comboBoxProceduraMarzy = new ComboBox();
		checkBoxReczneKwoty = new CheckBox();
		tabPageKSeF = new TabPage();
		tableLayoutPanel8 = new TableLayoutPanel();
		buttonKSeFGeneruj = new ButtonDPI();
		textBoxKSeFXML = new TextBox();
		label21 = new Label();
		textBoxNumerKSeF = new TextBox();
		linkLabelKSeFUrl = new LinkLabel();
		label24 = new Label();
		dateTimePickerDataKSeF = new DateTimePickerFix();
		tableLayoutPanel1 = new TableLayoutPanel();
		textBoxNumer = new TextBox();
		labelRodzaj = new Label();
		label2 = new Label();
		numericUpDownKurs = new NumericUpDownDPI();
		buttonWaluta = new ButtonDPI();
		label15 = new Label();
		comboBoxWaluta = new ComboBoxFix();
		label22 = new Label();
		tableLayoutPanelDatyKwoty = new TableLayoutPanel();
		groupBox6 = new GroupBox();
		tableLayoutPanel9 = new TableLayoutPanel();
		label12 = new Label();
		label13 = new Label();
		label14 = new Label();
		textBoxRachunekBankowy = new TextBox();
		dateTimePickerTerminPlatnosci = new DateTimePickerFix();
		comboBoxSposobPlatnosci = new ComboBoxFix();
		buttonSposobPlatnosci = new ButtonDPI();
		textBoxNazwaBanku = new TextBox();
		groupBox1 = new GroupBox();
		tableLayoutPanel4 = new TableLayoutPanel();
		label3 = new Label();
		label4 = new Label();
		label5 = new Label();
		dateTimePickerDataWystawienia = new DateTimePickerFix();
		dateTimePickerDataSprzedazy = new DateTimePickerFix();
		dateTimePickerDataWprowadzenia = new DateTimePickerFix();
		groupBox7 = new GroupBox();
		tableLayoutPanel3 = new TableLayoutPanel();
		label16 = new Label();
		label17 = new Label();
		label18 = new Label();
		numericUpDownNetto = new NumericUpDownDPI();
		numericUpDownVat = new NumericUpDownDPI();
		numericUpDownBrutto = new NumericUpDownDPI();
		((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
		tableLayoutPanel.SuspendLayout();
		tableLayoutPanelKontrahenci.SuspendLayout();
		groupBox2.SuspendLayout();
		tableLayoutPanel5.SuspendLayout();
		groupBox3.SuspendLayout();
		tableLayoutPanel6.SuspendLayout();
		tabControl1.SuspendLayout();
		tabPageUwagi.SuspendLayout();
		tableLayoutPanel2.SuspendLayout();
		groupBox4.SuspendLayout();
		groupBox5.SuspendLayout();
		tabPagePodatki.SuspendLayout();
		tableLayoutPanel7.SuspendLayout();
		tabPageKSeF.SuspendLayout();
		tableLayoutPanel8.SuspendLayout();
		tableLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDownKurs).BeginInit();
		tableLayoutPanelDatyKwoty.SuspendLayout();
		groupBox6.SuspendLayout();
		tableLayoutPanel9.SuspendLayout();
		groupBox1.SuspendLayout();
		tableLayoutPanel4.SuspendLayout();
		groupBox7.SuspendLayout();
		tableLayoutPanel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDownNetto).BeginInit();
		((System.ComponentModel.ISupportInitialize)numericUpDownVat).BeginInit();
		((System.ComponentModel.ISupportInitialize)numericUpDownBrutto).BeginInit();
		SuspendLayout();
		// 
		// tableLayoutPanel
		// 
		tableLayoutPanel.ColumnCount = 1;
		tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel.Controls.Add(tableLayoutPanelKontrahenci, 0, 1);
		tableLayoutPanel.Controls.Add(tabControl1, 0, 3);
		tableLayoutPanel.Controls.Add(tableLayoutPanel1, 0, 0);
		tableLayoutPanel.Controls.Add(tableLayoutPanelDatyKwoty, 0, 2);
		tableLayoutPanel.Dock = DockStyle.Fill;
		tableLayoutPanel.Location = new Point(0, 0);
		tableLayoutPanel.Name = "tableLayoutPanel";
		tableLayoutPanel.RowCount = 4;
		tableLayoutPanel.RowStyles.Add(new RowStyle());
		tableLayoutPanel.RowStyles.Add(new RowStyle());
		tableLayoutPanel.RowStyles.Add(new RowStyle());
		tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel.Size = new Size(900, 550);
		tableLayoutPanel.TabIndex = 0;
		// 
		// tableLayoutPanelKontrahenci
		// 
		tableLayoutPanelKontrahenci.AutoSize = true;
		tableLayoutPanelKontrahenci.AutoSizeMode = AutoSizeMode.GrowAndShrink;
		tableLayoutPanelKontrahenci.ColumnCount = 6;
		tableLayoutPanelKontrahenci.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
		tableLayoutPanelKontrahenci.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
		tableLayoutPanelKontrahenci.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
		tableLayoutPanelKontrahenci.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
		tableLayoutPanelKontrahenci.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
		tableLayoutPanelKontrahenci.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66667F));
		tableLayoutPanelKontrahenci.Controls.Add(groupBox2, 0, 0);
		tableLayoutPanelKontrahenci.Controls.Add(groupBox3, 3, 0);
		tableLayoutPanelKontrahenci.Dock = DockStyle.Fill;
		tableLayoutPanelKontrahenci.Location = new Point(3, 40);
		tableLayoutPanelKontrahenci.Name = "tableLayoutPanelKontrahenci";
		tableLayoutPanelKontrahenci.RowCount = 1;
		tableLayoutPanelKontrahenci.RowStyles.Add(new RowStyle());
		tableLayoutPanelKontrahenci.RowStyles.Add(new RowStyle(SizeType.Absolute, 153F));
		tableLayoutPanelKontrahenci.Size = new Size(894, 153);
		tableLayoutPanelKontrahenci.TabIndex = 2;
		// 
		// groupBox2
		// 
		tableLayoutPanelKontrahenci.SetColumnSpan(groupBox2, 3);
		groupBox2.Controls.Add(tableLayoutPanel5);
		groupBox2.Dock = DockStyle.Fill;
		groupBox2.Location = new Point(3, 3);
		groupBox2.Name = "groupBox2";
		groupBox2.Size = new Size(441, 147);
		groupBox2.TabIndex = 10;
		groupBox2.TabStop = false;
		groupBox2.Text = "Sprzedawca";
		// 
		// tableLayoutPanel5
		// 
		tableLayoutPanel5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
		tableLayoutPanel5.ColumnCount = 4;
		tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel5.Controls.Add(label6, 0, 0);
		tableLayoutPanel5.Controls.Add(label7, 0, 1);
		tableLayoutPanel5.Controls.Add(label8, 0, 2);
		tableLayoutPanel5.Controls.Add(textBoxDaneSprzedawcy, 1, 2);
		tableLayoutPanel5.Controls.Add(buttonSprzedawca, 2, 0);
		tableLayoutPanel5.Controls.Add(comboBoxNIPSprzedawcy, 1, 0);
		tableLayoutPanel5.Controls.Add(comboBoxNazwaSprzedawcy, 1, 1);
		tableLayoutPanel5.Controls.Add(buttonNowySprzedawca, 3, 0);
		tableLayoutPanel5.Dock = DockStyle.Fill;
		tableLayoutPanel5.Location = new Point(3, 19);
		tableLayoutPanel5.Name = "tableLayoutPanel5";
		tableLayoutPanel5.RowCount = 3;
		tableLayoutPanel5.RowStyles.Add(new RowStyle());
		tableLayoutPanel5.RowStyles.Add(new RowStyle());
		tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel5.Size = new Size(435, 125);
		tableLayoutPanel5.TabIndex = 0;
		// 
		// label6
		// 
		label6.Anchor = AnchorStyles.Right;
		label6.AutoSize = true;
		label6.Location = new Point(19, 8);
		label6.Name = "label6";
		label6.Size = new Size(26, 15);
		label6.TabIndex = 0;
		label6.Text = "NIP";
		// 
		// label7
		// 
		label7.Anchor = AnchorStyles.Right;
		label7.AutoSize = true;
		label7.Location = new Point(3, 38);
		label7.Name = "label7";
		label7.Size = new Size(42, 15);
		label7.TabIndex = 0;
		label7.Text = "Nazwa";
		// 
		// label8
		// 
		label8.Anchor = AnchorStyles.Right;
		label8.AutoSize = true;
		label8.Location = new Point(8, 85);
		label8.Name = "label8";
		label8.Size = new Size(37, 15);
		label8.TabIndex = 0;
		label8.Text = "Adres";
		// 
		// textBoxDaneSprzedawcy
		// 
		textBoxDaneSprzedawcy.AcceptsReturn = true;
		tableLayoutPanel5.SetColumnSpan(textBoxDaneSprzedawcy, 3);
		textBoxDaneSprzedawcy.Dock = DockStyle.Fill;
		textBoxDaneSprzedawcy.Location = new Point(51, 63);
		textBoxDaneSprzedawcy.Multiline = true;
		textBoxDaneSprzedawcy.Name = "textBoxDaneSprzedawcy";
		textBoxDaneSprzedawcy.Size = new Size(381, 59);
		textBoxDaneSprzedawcy.TabIndex = 30;
		// 
		// buttonSprzedawca
		// 
		buttonSprzedawca.Anchor = AnchorStyles.Left;
		buttonSprzedawca.AutoSize = true;
		buttonSprzedawca.Location = new Point(375, 3);
		buttonSprzedawca.Name = "buttonSprzedawca";
		buttonSprzedawca.Size = new Size(26, 25);
		buttonSprzedawca.TabIndex = 11;
		buttonSprzedawca.Text = "...";
		toolTip.SetToolTip(buttonSprzedawca, "Wyświetl pełną listę");
		buttonSprzedawca.UseVisualStyleBackColor = true;
		// 
		// comboBoxNIPSprzedawcy
		// 
		comboBoxNIPSprzedawcy.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBoxNIPSprzedawcy.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		comboBoxNIPSprzedawcy.AutoCompleteSource = AutoCompleteSource.ListItems;
		comboBoxNIPSprzedawcy.FormattingEnabled = true;
		comboBoxNIPSprzedawcy.Location = new Point(51, 4);
		comboBoxNIPSprzedawcy.Name = "comboBoxNIPSprzedawcy";
		comboBoxNIPSprzedawcy.Size = new Size(318, 23);
		comboBoxNIPSprzedawcy.TabIndex = 10;
		// 
		// comboBoxNazwaSprzedawcy
		// 
		comboBoxNazwaSprzedawcy.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBoxNazwaSprzedawcy.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		comboBoxNazwaSprzedawcy.AutoCompleteSource = AutoCompleteSource.ListItems;
		tableLayoutPanel5.SetColumnSpan(comboBoxNazwaSprzedawcy, 3);
		comboBoxNazwaSprzedawcy.FormattingEnabled = true;
		comboBoxNazwaSprzedawcy.Location = new Point(51, 34);
		comboBoxNazwaSprzedawcy.Name = "comboBoxNazwaSprzedawcy";
		comboBoxNazwaSprzedawcy.Size = new Size(381, 23);
		comboBoxNazwaSprzedawcy.TabIndex = 20;
		// 
		// buttonNowySprzedawca
		// 
		buttonNowySprzedawca.Anchor = AnchorStyles.Left;
		buttonNowySprzedawca.AutoSize = true;
		buttonNowySprzedawca.Location = new Point(407, 3);
		buttonNowySprzedawca.Name = "buttonNowySprzedawca";
		buttonNowySprzedawca.Size = new Size(25, 25);
		buttonNowySprzedawca.TabIndex = 12;
		buttonNowySprzedawca.Text = "+";
		toolTip.SetToolTip(buttonNowySprzedawca, "Dodaj sprzedawcę do słownika kontrahentów");
		buttonNowySprzedawca.UseVisualStyleBackColor = true;
		buttonNowySprzedawca.Click += buttonNowySprzedawca_Click;
		// 
		// groupBox3
		// 
		tableLayoutPanelKontrahenci.SetColumnSpan(groupBox3, 3);
		groupBox3.Controls.Add(tableLayoutPanel6);
		groupBox3.Dock = DockStyle.Fill;
		groupBox3.Location = new Point(450, 3);
		groupBox3.Name = "groupBox3";
		groupBox3.Size = new Size(441, 147);
		groupBox3.TabIndex = 20;
		groupBox3.TabStop = false;
		groupBox3.Text = "Nabywca";
		// 
		// tableLayoutPanel6
		// 
		tableLayoutPanel6.AutoSize = true;
		tableLayoutPanel6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
		tableLayoutPanel6.ColumnCount = 4;
		tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel6.Controls.Add(label9, 0, 0);
		tableLayoutPanel6.Controls.Add(label10, 0, 1);
		tableLayoutPanel6.Controls.Add(label11, 0, 2);
		tableLayoutPanel6.Controls.Add(textBoxDaneNabywcy, 1, 2);
		tableLayoutPanel6.Controls.Add(buttonNabywca, 2, 0);
		tableLayoutPanel6.Controls.Add(comboBoxNIPNabywcy, 1, 0);
		tableLayoutPanel6.Controls.Add(comboBoxNazwaNabywcy, 1, 1);
		tableLayoutPanel6.Controls.Add(buttonNowyNabywca, 3, 0);
		tableLayoutPanel6.Dock = DockStyle.Fill;
		tableLayoutPanel6.Location = new Point(3, 19);
		tableLayoutPanel6.Name = "tableLayoutPanel6";
		tableLayoutPanel6.RowCount = 3;
		tableLayoutPanel6.RowStyles.Add(new RowStyle());
		tableLayoutPanel6.RowStyles.Add(new RowStyle());
		tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel6.Size = new Size(435, 125);
		tableLayoutPanel6.TabIndex = 0;
		// 
		// label9
		// 
		label9.Anchor = AnchorStyles.Right;
		label9.AutoSize = true;
		label9.Location = new Point(19, 8);
		label9.Name = "label9";
		label9.Size = new Size(26, 15);
		label9.TabIndex = 0;
		label9.Text = "NIP";
		// 
		// label10
		// 
		label10.Anchor = AnchorStyles.Right;
		label10.AutoSize = true;
		label10.Location = new Point(3, 38);
		label10.Name = "label10";
		label10.Size = new Size(42, 15);
		label10.TabIndex = 0;
		label10.Text = "Nazwa";
		// 
		// label11
		// 
		label11.Anchor = AnchorStyles.Right;
		label11.AutoSize = true;
		label11.Location = new Point(8, 85);
		label11.Name = "label11";
		label11.Size = new Size(37, 15);
		label11.TabIndex = 0;
		label11.Text = "Adres";
		// 
		// textBoxDaneNabywcy
		// 
		textBoxDaneNabywcy.AcceptsReturn = true;
		tableLayoutPanel6.SetColumnSpan(textBoxDaneNabywcy, 3);
		textBoxDaneNabywcy.Dock = DockStyle.Fill;
		textBoxDaneNabywcy.Location = new Point(51, 63);
		textBoxDaneNabywcy.Multiline = true;
		textBoxDaneNabywcy.Name = "textBoxDaneNabywcy";
		textBoxDaneNabywcy.Size = new Size(381, 59);
		textBoxDaneNabywcy.TabIndex = 30;
		// 
		// buttonNabywca
		// 
		buttonNabywca.Anchor = AnchorStyles.Left;
		buttonNabywca.AutoSize = true;
		buttonNabywca.Location = new Point(375, 3);
		buttonNabywca.Name = "buttonNabywca";
		buttonNabywca.Size = new Size(26, 25);
		buttonNabywca.TabIndex = 11;
		buttonNabywca.Text = "...";
		toolTip.SetToolTip(buttonNabywca, "Wyświetl pełną listę");
		buttonNabywca.UseVisualStyleBackColor = true;
		// 
		// comboBoxNIPNabywcy
		// 
		comboBoxNIPNabywcy.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBoxNIPNabywcy.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		comboBoxNIPNabywcy.AutoCompleteSource = AutoCompleteSource.ListItems;
		comboBoxNIPNabywcy.FormattingEnabled = true;
		comboBoxNIPNabywcy.Location = new Point(51, 4);
		comboBoxNIPNabywcy.Name = "comboBoxNIPNabywcy";
		comboBoxNIPNabywcy.Size = new Size(318, 23);
		comboBoxNIPNabywcy.TabIndex = 10;
		// 
		// comboBoxNazwaNabywcy
		// 
		comboBoxNazwaNabywcy.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBoxNazwaNabywcy.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		comboBoxNazwaNabywcy.AutoCompleteSource = AutoCompleteSource.ListItems;
		tableLayoutPanel6.SetColumnSpan(comboBoxNazwaNabywcy, 3);
		comboBoxNazwaNabywcy.FormattingEnabled = true;
		comboBoxNazwaNabywcy.Location = new Point(51, 34);
		comboBoxNazwaNabywcy.Name = "comboBoxNazwaNabywcy";
		comboBoxNazwaNabywcy.Size = new Size(381, 23);
		comboBoxNazwaNabywcy.TabIndex = 20;
		// 
		// buttonNowyNabywca
		// 
		buttonNowyNabywca.Anchor = AnchorStyles.Left;
		buttonNowyNabywca.AutoSize = true;
		buttonNowyNabywca.Location = new Point(407, 3);
		buttonNowyNabywca.Name = "buttonNowyNabywca";
		buttonNowyNabywca.Size = new Size(25, 25);
		buttonNowyNabywca.TabIndex = 12;
		buttonNowyNabywca.Text = "+";
		toolTip.SetToolTip(buttonNowyNabywca, "Dodaj nabywcę do słownika kontrahentów");
		buttonNowyNabywca.UseVisualStyleBackColor = true;
		buttonNowyNabywca.Click += buttonNowyNabywca_Click;
		// 
		// tabControl1
		// 
		tabControl1.Controls.Add(tabPagePozycje);
		tabControl1.Controls.Add(tabPageWplaty);
		tabControl1.Controls.Add(tabPagePliki);
		tabControl1.Controls.Add(tabPageUwagi);
		tabControl1.Controls.Add(tabPageDodatkowePodmioty);
		tabControl1.Controls.Add(tabPagePodatki);
		tabControl1.Controls.Add(tabPageKSeF);
		tabControl1.Dock = DockStyle.Fill;
		tabControl1.Location = new Point(3, 326);
		tabControl1.Name = "tabControl1";
		tabControl1.SelectedIndex = 0;
		tabControl1.Size = new Size(894, 221);
		tabControl1.TabIndex = 4;
		// 
		// tabPagePozycje
		// 
		tabPagePozycje.Location = new Point(4, 24);
		tabPagePozycje.Name = "tabPagePozycje";
		tabPagePozycje.Size = new Size(886, 193);
		tabPagePozycje.TabIndex = 0;
		tabPagePozycje.Text = "Pozycje   [ᴄᴛʀʟ-ғ₁]";
		tabPagePozycje.UseVisualStyleBackColor = true;
		// 
		// tabPageWplaty
		// 
		tabPageWplaty.Location = new Point(4, 24);
		tabPageWplaty.Name = "tabPageWplaty";
		tabPageWplaty.Size = new Size(886, 193);
		tabPageWplaty.TabIndex = 1;
		tabPageWplaty.Text = "Wpłaty   [ᴄᴛʀʟ-ғ₂]";
		tabPageWplaty.UseVisualStyleBackColor = true;
		// 
		// tabPagePliki
		// 
		tabPagePliki.Location = new Point(4, 24);
		tabPagePliki.Name = "tabPagePliki";
		tabPagePliki.Size = new Size(886, 193);
		tabPagePliki.TabIndex = 3;
		tabPagePliki.Text = "Pliki   [ᴄᴛʀʟ-ғ₃]";
		tabPagePliki.UseVisualStyleBackColor = true;
		// 
		// tabPageUwagi
		// 
		tabPageUwagi.Controls.Add(tableLayoutPanel2);
		tabPageUwagi.Location = new Point(4, 24);
		tabPageUwagi.Name = "tabPageUwagi";
		tabPageUwagi.Size = new Size(886, 193);
		tabPageUwagi.TabIndex = 2;
		tabPageUwagi.Text = "Uwagi   [ᴄᴛʀʟ-ғ₄]";
		tabPageUwagi.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel2
		// 
		tableLayoutPanel2.ColumnCount = 2;
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
		tableLayoutPanel2.Controls.Add(groupBox4, 0, 0);
		tableLayoutPanel2.Controls.Add(groupBox5, 1, 0);
		tableLayoutPanel2.Dock = DockStyle.Fill;
		tableLayoutPanel2.Location = new Point(0, 0);
		tableLayoutPanel2.Name = "tableLayoutPanel2";
		tableLayoutPanel2.RowCount = 1;
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		tableLayoutPanel2.Size = new Size(886, 193);
		tableLayoutPanel2.TabIndex = 2;
		// 
		// groupBox4
		// 
		groupBox4.Controls.Add(linkLabelUwagiPomoc);
		groupBox4.Controls.Add(textBoxUwagiPubliczne);
		groupBox4.Dock = DockStyle.Fill;
		groupBox4.Location = new Point(3, 3);
		groupBox4.Name = "groupBox4";
		groupBox4.Size = new Size(437, 187);
		groupBox4.TabIndex = 1;
		groupBox4.TabStop = false;
		groupBox4.Text = "Uwagi (drukowane na fakturze)";
		// 
		// linkLabelUwagiPomoc
		// 
		linkLabelUwagiPomoc.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		linkLabelUwagiPomoc.AutoSize = true;
		linkLabelUwagiPomoc.Location = new Point(419, 166);
		linkLabelUwagiPomoc.Name = "linkLabelUwagiPomoc";
		linkLabelUwagiPomoc.Size = new Size(12, 15);
		linkLabelUwagiPomoc.TabIndex = 1;
		linkLabelUwagiPomoc.TabStop = true;
		linkLabelUwagiPomoc.Text = "?";
		linkLabelUwagiPomoc.LinkClicked += linkLabelUwagiPomoc_LinkClicked;
		// 
		// textBoxUwagiPubliczne
		// 
		textBoxUwagiPubliczne.AcceptsReturn = true;
		textBoxUwagiPubliczne.Dock = DockStyle.Fill;
		textBoxUwagiPubliczne.Location = new Point(3, 19);
		textBoxUwagiPubliczne.Multiline = true;
		textBoxUwagiPubliczne.Name = "textBoxUwagiPubliczne";
		textBoxUwagiPubliczne.Size = new Size(431, 165);
		textBoxUwagiPubliczne.TabIndex = 0;
		// 
		// groupBox5
		// 
		groupBox5.Controls.Add(textBoxUwagiWewnetrzne);
		groupBox5.Dock = DockStyle.Fill;
		groupBox5.Location = new Point(446, 3);
		groupBox5.Name = "groupBox5";
		groupBox5.Size = new Size(437, 187);
		groupBox5.TabIndex = 1;
		groupBox5.TabStop = false;
		groupBox5.Text = "Uwagi (wewnętrzne)";
		// 
		// textBoxUwagiWewnetrzne
		// 
		textBoxUwagiWewnetrzne.AcceptsReturn = true;
		textBoxUwagiWewnetrzne.Dock = DockStyle.Fill;
		textBoxUwagiWewnetrzne.Location = new Point(3, 19);
		textBoxUwagiWewnetrzne.Multiline = true;
		textBoxUwagiWewnetrzne.Name = "textBoxUwagiWewnetrzne";
		textBoxUwagiWewnetrzne.Size = new Size(431, 165);
		textBoxUwagiWewnetrzne.TabIndex = 0;
		// 
		// tabPageDodatkowePodmioty
		// 
		tabPageDodatkowePodmioty.Location = new Point(4, 24);
		tabPageDodatkowePodmioty.Name = "tabPageDodatkowePodmioty";
		tabPageDodatkowePodmioty.Size = new Size(886, 193);
		tabPageDodatkowePodmioty.TabIndex = 6;
		tabPageDodatkowePodmioty.Text = "Dodatkowe podmioty   [ᴄᴛʀʟ-ғ₅]";
		tabPageDodatkowePodmioty.UseVisualStyleBackColor = true;
		// 
		// tabPagePodatki
		// 
		tabPagePodatki.Controls.Add(tableLayoutPanel7);
		tabPagePodatki.Location = new Point(4, 24);
		tabPagePodatki.Name = "tabPagePodatki";
		tabPagePodatki.Padding = new Padding(3);
		tabPagePodatki.Size = new Size(886, 193);
		tabPagePodatki.TabIndex = 4;
		tabPagePodatki.Text = "Podatki   [ᴄᴛʀʟ-ғ₆]";
		tabPagePodatki.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel7
		// 
		tableLayoutPanel7.ColumnCount = 4;
		tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
		tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
		tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
		tableLayoutPanel7.Controls.Add(label1, 0, 0);
		tableLayoutPanel7.Controls.Add(label19, 0, 1);
		tableLayoutPanel7.Controls.Add(comboBoxProcentKosztow, 1, 0);
		tableLayoutPanel7.Controls.Add(comboBoxProcentVat, 1, 1);
		tableLayoutPanel7.Controls.Add(checkBoxWDT, 3, 0);
		tableLayoutPanel7.Controls.Add(checkBoxWNT, 3, 1);
		tableLayoutPanel7.Controls.Add(checkBoxTP, 3, 2);
		tableLayoutPanel7.Controls.Add(checkBoxZakupSrodkowTrwalych, 3, 3);
		tableLayoutPanel7.Controls.Add(label20, 0, 2);
		tableLayoutPanel7.Controls.Add(textBoxOpisZdarzenia, 1, 2);
		tableLayoutPanel7.Controls.Add(label23, 0, 3);
		tableLayoutPanel7.Controls.Add(comboBoxProceduraMarzy, 1, 3);
		tableLayoutPanel7.Controls.Add(checkBoxReczneKwoty, 3, 4);
		tableLayoutPanel7.Location = new Point(6, 6);
		tableLayoutPanel7.Name = "tableLayoutPanel7";
		tableLayoutPanel7.RowCount = 6;
		tableLayoutPanel7.RowStyles.Add(new RowStyle());
		tableLayoutPanel7.RowStyles.Add(new RowStyle());
		tableLayoutPanel7.RowStyles.Add(new RowStyle());
		tableLayoutPanel7.RowStyles.Add(new RowStyle());
		tableLayoutPanel7.RowStyles.Add(new RowStyle());
		tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel7.Size = new Size(583, 152);
		tableLayoutPanel7.TabIndex = 0;
		// 
		// label1
		// 
		label1.Anchor = AnchorStyles.Right;
		label1.AutoSize = true;
		label1.Location = new Point(40, 7);
		label1.Name = "label1";
		label1.Size = new Size(100, 15);
		label1.TabIndex = 0;
		label1.Text = "Udział w kosztach";
		// 
		// label19
		// 
		label19.Anchor = AnchorStyles.Right;
		label19.AutoSize = true;
		label19.Location = new Point(3, 36);
		label19.Name = "label19";
		label19.Size = new Size(137, 15);
		label19.TabIndex = 0;
		label19.Text = "Udział w VAT naliczonym";
		// 
		// comboBoxProcentKosztow
		// 
		comboBoxProcentKosztow.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBoxProcentKosztow.FormattingEnabled = true;
		comboBoxProcentKosztow.Items.AddRange(new object[] { "100%", "75%", "20%", "0%" });
		comboBoxProcentKosztow.Location = new Point(146, 3);
		comboBoxProcentKosztow.Name = "comboBoxProcentKosztow";
		comboBoxProcentKosztow.Size = new Size(204, 23);
		comboBoxProcentKosztow.TabIndex = 0;
		// 
		// comboBoxProcentVat
		// 
		comboBoxProcentVat.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBoxProcentVat.FormattingEnabled = true;
		comboBoxProcentVat.Items.AddRange(new object[] { "100%", "50%", "0%" });
		comboBoxProcentVat.Location = new Point(146, 32);
		comboBoxProcentVat.Name = "comboBoxProcentVat";
		comboBoxProcentVat.Size = new Size(204, 23);
		comboBoxProcentVat.TabIndex = 1;
		// 
		// checkBoxWDT
		// 
		checkBoxWDT.Anchor = AnchorStyles.Left;
		checkBoxWDT.AutoSize = true;
		checkBoxWDT.Location = new Point(376, 5);
		checkBoxWDT.Name = "checkBoxWDT";
		checkBoxWDT.Size = new Size(50, 19);
		checkBoxWDT.TabIndex = 4;
		checkBoxWDT.Text = "WDT";
		checkBoxWDT.UseVisualStyleBackColor = true;
		// 
		// checkBoxWNT
		// 
		checkBoxWNT.Anchor = AnchorStyles.Left;
		checkBoxWNT.AutoSize = true;
		checkBoxWNT.Location = new Point(376, 34);
		checkBoxWNT.Name = "checkBoxWNT";
		checkBoxWNT.Size = new Size(52, 19);
		checkBoxWNT.TabIndex = 5;
		checkBoxWNT.Text = "WNT";
		checkBoxWNT.UseVisualStyleBackColor = true;
		// 
		// checkBoxTP
		// 
		checkBoxTP.Anchor = AnchorStyles.Left;
		checkBoxTP.AutoSize = true;
		checkBoxTP.Location = new Point(376, 63);
		checkBoxTP.Name = "checkBoxTP";
		checkBoxTP.Size = new Size(131, 19);
		checkBoxTP.TabIndex = 6;
		checkBoxTP.Text = "Podmiot powiązany";
		checkBoxTP.UseVisualStyleBackColor = true;
		// 
		// checkBoxZakupSrodkowTrwalych
		// 
		checkBoxZakupSrodkowTrwalych.Anchor = AnchorStyles.Left;
		checkBoxZakupSrodkowTrwalych.AutoSize = true;
		checkBoxZakupSrodkowTrwalych.Location = new Point(376, 92);
		checkBoxZakupSrodkowTrwalych.Name = "checkBoxZakupSrodkowTrwalych";
		checkBoxZakupSrodkowTrwalych.Size = new Size(155, 19);
		checkBoxZakupSrodkowTrwalych.TabIndex = 7;
		checkBoxZakupSrodkowTrwalych.Text = "Zakup środków trwałych";
		checkBoxZakupSrodkowTrwalych.UseVisualStyleBackColor = true;
		// 
		// label20
		// 
		label20.Anchor = AnchorStyles.Right;
		label20.AutoSize = true;
		label20.Location = new Point(7, 65);
		label20.Name = "label20";
		label20.Size = new Size(133, 15);
		label20.TabIndex = 14;
		label20.Text = "Opis zdarzenia na PKPiR";
		// 
		// textBoxOpisZdarzenia
		// 
		textBoxOpisZdarzenia.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxOpisZdarzenia.Location = new Point(146, 61);
		textBoxOpisZdarzenia.Name = "textBoxOpisZdarzenia";
		textBoxOpisZdarzenia.Size = new Size(204, 23);
		textBoxOpisZdarzenia.TabIndex = 2;
		// 
		// label23
		// 
		label23.Anchor = AnchorStyles.Right;
		label23.AutoSize = true;
		label23.Location = new Point(44, 94);
		label23.Name = "label23";
		label23.Size = new Size(96, 15);
		label23.TabIndex = 14;
		label23.Text = "Procedura marży";
		// 
		// comboBoxProceduraMarzy
		// 
		comboBoxProceduraMarzy.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBoxProceduraMarzy.FormattingEnabled = true;
		comboBoxProceduraMarzy.Items.AddRange(new object[] { "100%", "50%", "0%" });
		comboBoxProceduraMarzy.Location = new Point(146, 90);
		comboBoxProceduraMarzy.Name = "comboBoxProceduraMarzy";
		comboBoxProceduraMarzy.Size = new Size(204, 23);
		comboBoxProceduraMarzy.TabIndex = 3;
		// 
		// checkBoxReczneKwoty
		// 
		checkBoxReczneKwoty.Anchor = AnchorStyles.Left;
		checkBoxReczneKwoty.AutoSize = true;
		checkBoxReczneKwoty.Location = new Point(376, 119);
		checkBoxReczneKwoty.Name = "checkBoxReczneKwoty";
		checkBoxReczneKwoty.Size = new Size(201, 19);
		checkBoxReczneKwoty.TabIndex = 8;
		checkBoxReczneKwoty.Text = "Kwota \"razem\" ustawiona ręcznie";
		checkBoxReczneKwoty.UseVisualStyleBackColor = true;
		// 
		// tabPageKSeF
		// 
		tabPageKSeF.Controls.Add(tableLayoutPanel8);
		tabPageKSeF.Location = new Point(4, 24);
		tabPageKSeF.Name = "tabPageKSeF";
		tabPageKSeF.Size = new Size(886, 193);
		tabPageKSeF.TabIndex = 5;
		tabPageKSeF.Text = "KSeF   [ᴄᴛʀʟ-ғ₇]";
		tabPageKSeF.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel8
		// 
		tableLayoutPanel8.ColumnCount = 8;
		tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
		tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel8.Controls.Add(buttonKSeFGeneruj, 5, 0);
		tableLayoutPanel8.Controls.Add(textBoxKSeFXML, 0, 1);
		tableLayoutPanel8.Controls.Add(label21, 0, 0);
		tableLayoutPanel8.Controls.Add(textBoxNumerKSeF, 1, 0);
		tableLayoutPanel8.Controls.Add(linkLabelKSeFUrl, 6, 0);
		tableLayoutPanel8.Controls.Add(label24, 3, 0);
		tableLayoutPanel8.Controls.Add(dateTimePickerDataKSeF, 4, 0);
		tableLayoutPanel8.Dock = DockStyle.Fill;
		tableLayoutPanel8.Location = new Point(0, 0);
		tableLayoutPanel8.Name = "tableLayoutPanel8";
		tableLayoutPanel8.RowCount = 2;
		tableLayoutPanel8.RowStyles.Add(new RowStyle());
		tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel8.Size = new Size(886, 193);
		tableLayoutPanel8.TabIndex = 0;
		// 
		// buttonKSeFGeneruj
		// 
		buttonKSeFGeneruj.AutoSize = true;
		buttonKSeFGeneruj.Location = new Point(570, 3);
		buttonKSeFGeneruj.Name = "buttonKSeFGeneruj";
		buttonKSeFGeneruj.Size = new Size(103, 25);
		buttonKSeFGeneruj.TabIndex = 2;
		buttonKSeFGeneruj.Text = "Generuj XML";
		buttonKSeFGeneruj.UseVisualStyleBackColor = true;
		buttonKSeFGeneruj.Click += buttonKSeFGeneruj_Click;
		// 
		// textBoxKSeFXML
		// 
		tableLayoutPanel8.SetColumnSpan(textBoxKSeFXML, 8);
		textBoxKSeFXML.Dock = DockStyle.Fill;
		textBoxKSeFXML.Font = new Font("Consolas", 9F);
		textBoxKSeFXML.Location = new Point(3, 34);
		textBoxKSeFXML.Multiline = true;
		textBoxKSeFXML.Name = "textBoxKSeFXML";
		textBoxKSeFXML.ScrollBars = ScrollBars.Both;
		textBoxKSeFXML.Size = new Size(880, 156);
		textBoxKSeFXML.TabIndex = 3;
		// 
		// label21
		// 
		label21.Anchor = AnchorStyles.Right;
		label21.AutoSize = true;
		label21.Location = new Point(3, 8);
		label21.Name = "label21";
		label21.Size = new Size(72, 15);
		label21.TabIndex = 0;
		label21.Text = "Numer KSeF";
		// 
		// textBoxNumerKSeF
		// 
		textBoxNumerKSeF.Anchor = AnchorStyles.Left;
		textBoxNumerKSeF.Location = new Point(81, 4);
		textBoxNumerKSeF.Name = "textBoxNumerKSeF";
		textBoxNumerKSeF.Size = new Size(226, 23);
		textBoxNumerKSeF.TabIndex = 0;
		// 
		// linkLabelKSeFUrl
		// 
		linkLabelKSeFUrl.Anchor = AnchorStyles.Left;
		linkLabelKSeFUrl.AutoSize = true;
		linkLabelKSeFUrl.Location = new Point(679, 8);
		linkLabelKSeFUrl.Name = "linkLabelKSeFUrl";
		linkLabelKSeFUrl.Size = new Size(0, 15);
		linkLabelKSeFUrl.TabIndex = 6;
		linkLabelKSeFUrl.LinkClicked += linkLabelKSeFUrl_LinkClicked;
		// 
		// label24
		// 
		label24.Anchor = AnchorStyles.Right;
		label24.AutoSize = true;
		label24.Location = new Point(333, 8);
		label24.Name = "label24";
		label24.Size = new Size(59, 15);
		label24.TabIndex = 0;
		label24.Text = "Data KSeF";
		// 
		// dateTimePickerDataKSeF
		// 
		dateTimePickerDataKSeF.Anchor = AnchorStyles.Left;
		dateTimePickerDataKSeF.Enabled = false;
		dateTimePickerDataKSeF.Location = new Point(398, 4);
		dateTimePickerDataKSeF.Name = "dateTimePickerDataKSeF";
		dateTimePickerDataKSeF.Size = new Size(166, 23);
		dateTimePickerDataKSeF.TabIndex = 1;
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.AutoSize = true;
		tableLayoutPanel1.ColumnCount = 11;
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
		tableLayoutPanel1.Controls.Add(textBoxNumer, 3, 0);
		tableLayoutPanel1.Controls.Add(labelRodzaj, 0, 0);
		tableLayoutPanel1.Controls.Add(label2, 2, 0);
		tableLayoutPanel1.Controls.Add(numericUpDownKurs, 10, 0);
		tableLayoutPanel1.Controls.Add(buttonWaluta, 7, 0);
		tableLayoutPanel1.Controls.Add(label15, 5, 0);
		tableLayoutPanel1.Controls.Add(comboBoxWaluta, 6, 0);
		tableLayoutPanel1.Controls.Add(label22, 9, 0);
		tableLayoutPanel1.Location = new Point(3, 3);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 1;
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.Size = new Size(894, 31);
		tableLayoutPanel1.TabIndex = 1;
		// 
		// textBoxNumer
		// 
		textBoxNumer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxNumer.Location = new Point(129, 4);
		textBoxNumer.Name = "textBoxNumer";
		textBoxNumer.Size = new Size(404, 23);
		textBoxNumer.TabIndex = 0;
		// 
		// labelRodzaj
		// 
		labelRodzaj.Anchor = AnchorStyles.Left;
		labelRodzaj.AutoSize = true;
		labelRodzaj.Location = new Point(3, 8);
		labelRodzaj.Name = "labelRodzaj";
		labelRodzaj.Size = new Size(50, 15);
		labelRodzaj.TabIndex = 0;
		labelRodzaj.Text = "[Rodzaj]";
		// 
		// label2
		// 
		label2.Anchor = AnchorStyles.Right;
		label2.AutoSize = true;
		label2.Location = new Point(79, 8);
		label2.Name = "label2";
		label2.Size = new Size(44, 15);
		label2.TabIndex = 2;
		label2.Text = "Numer";
		// 
		// numericUpDownKurs
		// 
		numericUpDownKurs.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		numericUpDownKurs.DecimalPlaces = 4;
		numericUpDownKurs.Location = new Point(817, 4);
		numericUpDownKurs.Margin = new Padding(3, 4, 3, 4);
		numericUpDownKurs.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
		numericUpDownKurs.Name = "numericUpDownKurs";
		numericUpDownKurs.Size = new Size(74, 23);
		numericUpDownKurs.TabIndex = 3;
		numericUpDownKurs.TextAlign = HorizontalAlignment.Right;
		// 
		// buttonWaluta
		// 
		buttonWaluta.Anchor = AnchorStyles.Left;
		buttonWaluta.AutoSize = true;
		buttonWaluta.Location = new Point(729, 3);
		buttonWaluta.Name = "buttonWaluta";
		buttonWaluta.Size = new Size(26, 25);
		buttonWaluta.TabIndex = 2;
		buttonWaluta.Text = "...";
		toolTip.SetToolTip(buttonWaluta, "Wyświetl pełną listę");
		buttonWaluta.UseVisualStyleBackColor = true;
		// 
		// label15
		// 
		label15.Anchor = AnchorStyles.Right;
		label15.AutoSize = true;
		label15.Location = new Point(559, 8);
		label15.Name = "label15";
		label15.Size = new Size(44, 15);
		label15.TabIndex = 0;
		label15.Text = "Waluta";
		// 
		// comboBoxWaluta
		// 
		comboBoxWaluta.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBoxWaluta.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		comboBoxWaluta.AutoCompleteSource = AutoCompleteSource.ListItems;
		comboBoxWaluta.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBoxWaluta.FormattingEnabled = true;
		comboBoxWaluta.Location = new Point(609, 4);
		comboBoxWaluta.Name = "comboBoxWaluta";
		comboBoxWaluta.Size = new Size(114, 23);
		comboBoxWaluta.TabIndex = 1;
		// 
		// label22
		// 
		label22.Anchor = AnchorStyles.Right;
		label22.AutoSize = true;
		label22.Location = new Point(781, 8);
		label22.Name = "label22";
		label22.Size = new Size(30, 15);
		label22.TabIndex = 0;
		label22.Text = "Kurs";
		// 
		// tableLayoutPanelDatyKwoty
		// 
		tableLayoutPanelDatyKwoty.AutoSize = true;
		tableLayoutPanelDatyKwoty.AutoSizeMode = AutoSizeMode.GrowAndShrink;
		tableLayoutPanelDatyKwoty.ColumnCount = 3;
		tableLayoutPanelDatyKwoty.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
		tableLayoutPanelDatyKwoty.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
		tableLayoutPanelDatyKwoty.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
		tableLayoutPanelDatyKwoty.Controls.Add(groupBox6, 1, 0);
		tableLayoutPanelDatyKwoty.Controls.Add(groupBox1, 0, 0);
		tableLayoutPanelDatyKwoty.Controls.Add(groupBox7, 2, 0);
		tableLayoutPanelDatyKwoty.Dock = DockStyle.Fill;
		tableLayoutPanelDatyKwoty.Location = new Point(3, 199);
		tableLayoutPanelDatyKwoty.Name = "tableLayoutPanelDatyKwoty";
		tableLayoutPanelDatyKwoty.RowCount = 1;
		tableLayoutPanelDatyKwoty.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanelDatyKwoty.Size = new Size(894, 121);
		tableLayoutPanelDatyKwoty.TabIndex = 3;
		// 
		// groupBox6
		// 
		groupBox6.AutoSize = true;
		groupBox6.Controls.Add(tableLayoutPanel9);
		groupBox6.Dock = DockStyle.Fill;
		groupBox6.Location = new Point(271, 3);
		groupBox6.Name = "groupBox6";
		groupBox6.Size = new Size(441, 115);
		groupBox6.TabIndex = 40;
		groupBox6.TabStop = false;
		groupBox6.Text = "Płatność";
		// 
		// tableLayoutPanel9
		// 
		tableLayoutPanel9.AutoSize = true;
		tableLayoutPanel9.AutoSizeMode = AutoSizeMode.GrowAndShrink;
		tableLayoutPanel9.ColumnCount = 4;
		tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel9.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel9.Controls.Add(label12, 0, 0);
		tableLayoutPanel9.Controls.Add(label13, 0, 1);
		tableLayoutPanel9.Controls.Add(label14, 0, 2);
		tableLayoutPanel9.Controls.Add(textBoxRachunekBankowy, 1, 2);
		tableLayoutPanel9.Controls.Add(dateTimePickerTerminPlatnosci, 1, 1);
		tableLayoutPanel9.Controls.Add(comboBoxSposobPlatnosci, 1, 0);
		tableLayoutPanel9.Controls.Add(buttonSposobPlatnosci, 3, 0);
		tableLayoutPanel9.Controls.Add(textBoxNazwaBanku, 2, 2);
		tableLayoutPanel9.Dock = DockStyle.Fill;
		tableLayoutPanel9.Location = new Point(3, 19);
		tableLayoutPanel9.Name = "tableLayoutPanel9";
		tableLayoutPanel9.RowCount = 4;
		tableLayoutPanel9.RowStyles.Add(new RowStyle());
		tableLayoutPanel9.RowStyles.Add(new RowStyle());
		tableLayoutPanel9.RowStyles.Add(new RowStyle());
		tableLayoutPanel9.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel9.Size = new Size(435, 93);
		tableLayoutPanel9.TabIndex = 0;
		// 
		// label12
		// 
		label12.Anchor = AnchorStyles.Right;
		label12.AutoSize = true;
		label12.Location = new Point(3, 8);
		label12.Name = "label12";
		label12.Size = new Size(97, 15);
		label12.TabIndex = 0;
		label12.Text = "Sposób płatności";
		// 
		// label13
		// 
		label13.Anchor = AnchorStyles.Right;
		label13.AutoSize = true;
		label13.Location = new Point(6, 38);
		label13.Name = "label13";
		label13.Size = new Size(94, 15);
		label13.TabIndex = 0;
		label13.Text = "Termin płatności";
		// 
		// label14
		// 
		label14.Anchor = AnchorStyles.Right;
		label14.AutoSize = true;
		label14.Location = new Point(3, 67);
		label14.Name = "label14";
		label14.Size = new Size(97, 15);
		label14.TabIndex = 0;
		label14.Text = "Numer rachunku";
		// 
		// textBoxRachunekBankowy
		// 
		textBoxRachunekBankowy.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxRachunekBankowy.Location = new Point(106, 63);
		textBoxRachunekBankowy.Name = "textBoxRachunekBankowy";
		textBoxRachunekBankowy.Size = new Size(201, 23);
		textBoxRachunekBankowy.TabIndex = 3;
		// 
		// dateTimePickerTerminPlatnosci
		// 
		dateTimePickerTerminPlatnosci.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel9.SetColumnSpan(dateTimePickerTerminPlatnosci, 3);
		dateTimePickerTerminPlatnosci.Location = new Point(106, 34);
		dateTimePickerTerminPlatnosci.Name = "dateTimePickerTerminPlatnosci";
		dateTimePickerTerminPlatnosci.Size = new Size(326, 23);
		dateTimePickerTerminPlatnosci.TabIndex = 2;
		// 
		// comboBoxSposobPlatnosci
		// 
		comboBoxSposobPlatnosci.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBoxSposobPlatnosci.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
		comboBoxSposobPlatnosci.AutoCompleteSource = AutoCompleteSource.ListItems;
		tableLayoutPanel9.SetColumnSpan(comboBoxSposobPlatnosci, 2);
		comboBoxSposobPlatnosci.FormattingEnabled = true;
		comboBoxSposobPlatnosci.Location = new Point(106, 4);
		comboBoxSposobPlatnosci.Name = "comboBoxSposobPlatnosci";
		comboBoxSposobPlatnosci.Size = new Size(294, 23);
		comboBoxSposobPlatnosci.TabIndex = 0;
		// 
		// buttonSposobPlatnosci
		// 
		buttonSposobPlatnosci.Anchor = AnchorStyles.Left;
		buttonSposobPlatnosci.AutoSize = true;
		buttonSposobPlatnosci.Location = new Point(406, 3);
		buttonSposobPlatnosci.Name = "buttonSposobPlatnosci";
		buttonSposobPlatnosci.Size = new Size(26, 25);
		buttonSposobPlatnosci.TabIndex = 1;
		buttonSposobPlatnosci.Text = "...";
		toolTip.SetToolTip(buttonSposobPlatnosci, "Wyświetl pełną listę");
		buttonSposobPlatnosci.UseVisualStyleBackColor = true;
		// 
		// textBoxNazwaBanku
		// 
		textBoxNazwaBanku.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel9.SetColumnSpan(textBoxNazwaBanku, 2);
		textBoxNazwaBanku.Location = new Point(313, 63);
		textBoxNazwaBanku.Name = "textBoxNazwaBanku";
		textBoxNazwaBanku.PlaceholderText = "Nazwa banku";
		textBoxNazwaBanku.Size = new Size(119, 23);
		textBoxNazwaBanku.TabIndex = 4;
		// 
		// groupBox1
		// 
		groupBox1.AutoSize = true;
		groupBox1.Controls.Add(tableLayoutPanel4);
		groupBox1.Dock = DockStyle.Fill;
		groupBox1.Location = new Point(3, 3);
		groupBox1.Name = "groupBox1";
		groupBox1.Size = new Size(262, 115);
		groupBox1.TabIndex = 30;
		groupBox1.TabStop = false;
		groupBox1.Text = "Daty";
		// 
		// tableLayoutPanel4
		// 
		tableLayoutPanel4.AutoSize = true;
		tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
		tableLayoutPanel4.ColumnCount = 2;
		tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel4.Controls.Add(label3, 0, 0);
		tableLayoutPanel4.Controls.Add(label4, 0, 1);
		tableLayoutPanel4.Controls.Add(label5, 0, 2);
		tableLayoutPanel4.Controls.Add(dateTimePickerDataWystawienia, 1, 0);
		tableLayoutPanel4.Controls.Add(dateTimePickerDataSprzedazy, 1, 1);
		tableLayoutPanel4.Controls.Add(dateTimePickerDataWprowadzenia, 1, 2);
		tableLayoutPanel4.Dock = DockStyle.Fill;
		tableLayoutPanel4.Location = new Point(3, 19);
		tableLayoutPanel4.Name = "tableLayoutPanel4";
		tableLayoutPanel4.RowCount = 4;
		tableLayoutPanel4.RowStyles.Add(new RowStyle());
		tableLayoutPanel4.RowStyles.Add(new RowStyle());
		tableLayoutPanel4.RowStyles.Add(new RowStyle());
		tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel4.Size = new Size(256, 93);
		tableLayoutPanel4.TabIndex = 0;
		// 
		// label3
		// 
		label3.Anchor = AnchorStyles.Right;
		label3.AutoSize = true;
		label3.Location = new Point(15, 7);
		label3.Name = "label3";
		label3.Size = new Size(98, 15);
		label3.TabIndex = 0;
		label3.Text = "Data wystawienia";
		// 
		// label4
		// 
		label4.Anchor = AnchorStyles.Right;
		label4.AutoSize = true;
		label4.Location = new Point(28, 36);
		label4.Name = "label4";
		label4.Size = new Size(85, 15);
		label4.TabIndex = 1;
		label4.Text = "Data sprzedaży";
		// 
		// label5
		// 
		label5.Anchor = AnchorStyles.Right;
		label5.AutoSize = true;
		label5.Location = new Point(3, 65);
		label5.Name = "label5";
		label5.Size = new Size(110, 15);
		label5.TabIndex = 1;
		label5.Text = "Data wprowadzenia";
		// 
		// dateTimePickerDataWystawienia
		// 
		dateTimePickerDataWystawienia.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		dateTimePickerDataWystawienia.Location = new Point(119, 3);
		dateTimePickerDataWystawienia.Name = "dateTimePickerDataWystawienia";
		dateTimePickerDataWystawienia.Size = new Size(134, 23);
		dateTimePickerDataWystawienia.TabIndex = 30;
		// 
		// dateTimePickerDataSprzedazy
		// 
		dateTimePickerDataSprzedazy.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		dateTimePickerDataSprzedazy.Location = new Point(119, 32);
		dateTimePickerDataSprzedazy.Name = "dateTimePickerDataSprzedazy";
		dateTimePickerDataSprzedazy.Size = new Size(134, 23);
		dateTimePickerDataSprzedazy.TabIndex = 31;
		// 
		// dateTimePickerDataWprowadzenia
		// 
		dateTimePickerDataWprowadzenia.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		dateTimePickerDataWprowadzenia.Location = new Point(119, 61);
		dateTimePickerDataWprowadzenia.Name = "dateTimePickerDataWprowadzenia";
		dateTimePickerDataWprowadzenia.Size = new Size(134, 23);
		dateTimePickerDataWprowadzenia.TabIndex = 32;
		// 
		// groupBox7
		// 
		groupBox7.AutoSize = true;
		groupBox7.Controls.Add(tableLayoutPanel3);
		groupBox7.Dock = DockStyle.Fill;
		groupBox7.Location = new Point(718, 3);
		groupBox7.Name = "groupBox7";
		groupBox7.Size = new Size(173, 115);
		groupBox7.TabIndex = 41;
		groupBox7.TabStop = false;
		groupBox7.Text = "Razem";
		// 
		// tableLayoutPanel3
		// 
		tableLayoutPanel3.AutoSize = true;
		tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
		tableLayoutPanel3.ColumnCount = 2;
		tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel3.Controls.Add(label16, 0, 0);
		tableLayoutPanel3.Controls.Add(label17, 0, 1);
		tableLayoutPanel3.Controls.Add(label18, 0, 2);
		tableLayoutPanel3.Controls.Add(numericUpDownNetto, 1, 0);
		tableLayoutPanel3.Controls.Add(numericUpDownVat, 1, 1);
		tableLayoutPanel3.Controls.Add(numericUpDownBrutto, 1, 2);
		tableLayoutPanel3.Dock = DockStyle.Fill;
		tableLayoutPanel3.Location = new Point(3, 19);
		tableLayoutPanel3.Name = "tableLayoutPanel3";
		tableLayoutPanel3.RowCount = 3;
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.Size = new Size(167, 93);
		tableLayoutPanel3.TabIndex = 0;
		// 
		// label16
		// 
		label16.Anchor = AnchorStyles.Right;
		label16.AutoSize = true;
		label16.Location = new Point(6, 8);
		label16.Name = "label16";
		label16.Size = new Size(37, 15);
		label16.TabIndex = 0;
		label16.Text = "Netto";
		// 
		// label17
		// 
		label17.Anchor = AnchorStyles.Right;
		label17.AutoSize = true;
		label17.Location = new Point(17, 39);
		label17.Name = "label17";
		label17.Size = new Size(26, 15);
		label17.TabIndex = 0;
		label17.Text = "VAT";
		// 
		// label18
		// 
		label18.Anchor = AnchorStyles.Right;
		label18.AutoSize = true;
		label18.Location = new Point(3, 70);
		label18.Name = "label18";
		label18.Size = new Size(40, 15);
		label18.TabIndex = 0;
		label18.Text = "Brutto";
		// 
		// numericUpDownNetto
		// 
		numericUpDownNetto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		numericUpDownNetto.DecimalPlaces = 2;
		numericUpDownNetto.Location = new Point(49, 4);
		numericUpDownNetto.Margin = new Padding(3, 4, 3, 4);
		numericUpDownNetto.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
		numericUpDownNetto.Minimum = new decimal(new int[] { 999999999, 0, 0, Int32.MinValue });
		numericUpDownNetto.Name = "numericUpDownNetto";
		numericUpDownNetto.Size = new Size(115, 23);
		numericUpDownNetto.TabIndex = 50;
		numericUpDownNetto.TextAlign = HorizontalAlignment.Right;
		// 
		// numericUpDownVat
		// 
		numericUpDownVat.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		numericUpDownVat.DecimalPlaces = 2;
		numericUpDownVat.Location = new Point(49, 35);
		numericUpDownVat.Margin = new Padding(3, 4, 3, 4);
		numericUpDownVat.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
		numericUpDownVat.Minimum = new decimal(new int[] { 999999999, 0, 0, Int32.MinValue });
		numericUpDownVat.Name = "numericUpDownVat";
		numericUpDownVat.Size = new Size(115, 23);
		numericUpDownVat.TabIndex = 51;
		numericUpDownVat.TextAlign = HorizontalAlignment.Right;
		// 
		// numericUpDownBrutto
		// 
		numericUpDownBrutto.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		numericUpDownBrutto.DecimalPlaces = 2;
		numericUpDownBrutto.Location = new Point(49, 66);
		numericUpDownBrutto.Margin = new Padding(3, 4, 3, 4);
		numericUpDownBrutto.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
		numericUpDownBrutto.Minimum = new decimal(new int[] { 999999999, 0, 0, Int32.MinValue });
		numericUpDownBrutto.Name = "numericUpDownBrutto";
		numericUpDownBrutto.Size = new Size(115, 23);
		numericUpDownBrutto.TabIndex = 52;
		numericUpDownBrutto.TextAlign = HorizontalAlignment.Right;
		// 
		// FakturaEdytor
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		Controls.Add(tableLayoutPanel);
		MinimumSize = new Size(900, 550);
		Name = "FakturaEdytor";
		Size = new Size(900, 550);
		((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
		tableLayoutPanel.ResumeLayout(false);
		tableLayoutPanel.PerformLayout();
		tableLayoutPanelKontrahenci.ResumeLayout(false);
		groupBox2.ResumeLayout(false);
		tableLayoutPanel5.ResumeLayout(false);
		tableLayoutPanel5.PerformLayout();
		groupBox3.ResumeLayout(false);
		groupBox3.PerformLayout();
		tableLayoutPanel6.ResumeLayout(false);
		tableLayoutPanel6.PerformLayout();
		tabControl1.ResumeLayout(false);
		tabPageUwagi.ResumeLayout(false);
		tableLayoutPanel2.ResumeLayout(false);
		groupBox4.ResumeLayout(false);
		groupBox4.PerformLayout();
		groupBox5.ResumeLayout(false);
		groupBox5.PerformLayout();
		tabPagePodatki.ResumeLayout(false);
		tableLayoutPanel7.ResumeLayout(false);
		tableLayoutPanel7.PerformLayout();
		tabPageKSeF.ResumeLayout(false);
		tableLayoutPanel8.ResumeLayout(false);
		tableLayoutPanel8.PerformLayout();
		tableLayoutPanel1.ResumeLayout(false);
		tableLayoutPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDownKurs).EndInit();
		tableLayoutPanelDatyKwoty.ResumeLayout(false);
		tableLayoutPanelDatyKwoty.PerformLayout();
		groupBox6.ResumeLayout(false);
		groupBox6.PerformLayout();
		tableLayoutPanel9.ResumeLayout(false);
		tableLayoutPanel9.PerformLayout();
		groupBox1.ResumeLayout(false);
		groupBox1.PerformLayout();
		tableLayoutPanel4.ResumeLayout(false);
		tableLayoutPanel4.PerformLayout();
		groupBox7.ResumeLayout(false);
		groupBox7.PerformLayout();
		tableLayoutPanel3.ResumeLayout(false);
		tableLayoutPanel3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDownNetto).EndInit();
		((System.ComponentModel.ISupportInitialize)numericUpDownVat).EndInit();
		((System.ComponentModel.ISupportInitialize)numericUpDownBrutto).EndInit();
		ResumeLayout(false);
	}

	#endregion
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
	private System.Windows.Forms.Label labelRodzaj;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.TextBox textBoxNumer;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanelKontrahenci;
	private System.Windows.Forms.GroupBox groupBox1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
	private System.Windows.Forms.GroupBox groupBox2;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
	private System.Windows.Forms.GroupBox groupBox3;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
	private System.Windows.Forms.TabControl tabControl1;
	private System.Windows.Forms.TabPage tabPagePozycje;
	private System.Windows.Forms.TabPage tabPageWplaty;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.Label label5;
	private System.Windows.Forms.Label label6;
	private System.Windows.Forms.Label label7;
	private System.Windows.Forms.Label label8;
	private System.Windows.Forms.Label label9;
	private System.Windows.Forms.Label label10;
	private System.Windows.Forms.Label label11;
	private System.Windows.Forms.GroupBox groupBox6;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
	private System.Windows.Forms.Label label12;
	private System.Windows.Forms.Label label13;
	private System.Windows.Forms.Label label14;
	private System.Windows.Forms.TextBox textBoxDaneSprzedawcy;
	private System.Windows.Forms.TextBox textBoxDaneNabywcy;
	private System.Windows.Forms.TextBox textBoxRachunekBankowy;
	private System.Windows.Forms.ComboBoxFix comboBoxSposobPlatnosci;
	private ButtonDPI buttonSprzedawca;
	private ButtonDPI buttonNabywca;
	private ButtonDPI buttonSposobPlatnosci;
	private System.Windows.Forms.ComboBoxFix comboBoxNIPSprzedawcy;
	private System.Windows.Forms.ComboBoxFix comboBoxNIPNabywcy;
	private System.Windows.Forms.ComboBoxFix comboBoxNazwaSprzedawcy;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.Label label15;
	private System.Windows.Forms.ComboBoxFix comboBoxWaluta;
	private System.Windows.Forms.ComboBoxFix comboBoxNazwaNabywcy;
	private ButtonDPI buttonWaluta;
	private System.Windows.Forms.TabPage tabPageUwagi;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
	private System.Windows.Forms.GroupBox groupBox5;
	private System.Windows.Forms.TextBox textBoxUwagiWewnetrzne;
	private System.Windows.Forms.GroupBox groupBox4;
	private System.Windows.Forms.TextBox textBoxUwagiPubliczne;
	private System.Windows.Forms.GroupBox groupBox7;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
	private System.Windows.Forms.Label label16;
	private System.Windows.Forms.Label label17;
	private System.Windows.Forms.Label label18;
	private NumericUpDownDPI numericUpDownNetto;
	private NumericUpDownDPI numericUpDownVat;
	private NumericUpDownDPI numericUpDownBrutto;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDatyKwoty;
	private System.Windows.Forms.TabPage tabPagePliki;
	private System.Windows.Forms.TabPage tabPagePodatki;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label label19;
	private System.Windows.Forms.CheckBox checkBoxTP;
	private System.Windows.Forms.ComboBox comboBoxProcentKosztow;
	private System.Windows.Forms.ComboBox comboBoxProcentVat;
	private ButtonDPI buttonNowySprzedawca;
	private ButtonDPI buttonNowyNabywca;
	private System.Windows.Forms.CheckBox checkBoxZakupSrodkowTrwalych;
	private System.Windows.Forms.CheckBox checkBoxWDT;
	private System.Windows.Forms.CheckBox checkBoxWNT;
	private System.Windows.Forms.Label label20;
	private System.Windows.Forms.TextBox textBoxOpisZdarzenia;
	private System.Windows.Forms.TabPage tabPageKSeF;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
	private System.Windows.Forms.Label label21;
	private System.Windows.Forms.TextBox textBoxNumerKSeF;
	private System.Windows.Forms.TextBox textBoxKSeFXML;
	private ButtonDPI buttonKSeFGeneruj;
	private System.Windows.Forms.Label label22;
	private System.Windows.Forms.DateTimePickerFix dateTimePickerDataWystawienia;
	private System.Windows.Forms.DateTimePickerFix dateTimePickerDataSprzedazy;
	private System.Windows.Forms.DateTimePickerFix dateTimePickerDataWprowadzenia;
	private System.Windows.Forms.DateTimePickerFix dateTimePickerTerminPlatnosci;
	private NumericUpDownDPI numericUpDownKurs;
	private System.Windows.Forms.Label label23;
	private System.Windows.Forms.ComboBox comboBoxProceduraMarzy;
	private System.Windows.Forms.CheckBox checkBoxReczneKwoty;
	private System.Windows.Forms.TabPage tabPageDodatkowePodmioty;
	private System.Windows.Forms.LinkLabel linkLabelUwagiPomoc;
	private System.Windows.Forms.LinkLabel linkLabelKSeFUrl;
	private System.Windows.Forms.TextBox textBoxNazwaBanku;
	private Label label24;
	private System.Windows.Forms.DateTimePickerFix dateTimePickerDataKSeF;
}
