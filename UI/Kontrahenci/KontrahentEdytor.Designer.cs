
namespace ProFak.UI;

partial class KontrahentEdytor
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
		tabPage1 = new TabPage();
		tableLayoutPanel1 = new TableLayoutPanel();
		label2 = new Label();
		textBoxPelnaNazwa = new TextBox();
		label3 = new Label();
		textBoxNIP = new TextBox();
		textBoxAdresRejestrowy = new TextBox();
		textBoxAdresKorespondencyjny = new TextBox();
		textBoxTelefon = new TextBox();
		textBoxEMail = new TextBox();
		textBoxRachunekBankowy = new TextBox();
		label4 = new Label();
		label5 = new Label();
		label6 = new Label();
		label7 = new Label();
		label8 = new Label();
		comboBoxStan = new ComboBox();
		label9 = new Label();
		checkBoxTP = new CheckBox();
		buttonSprawdzMF = new ButtonDPI();
		buttonPobierzGUS = new ButtonDPI();
		labelSposobPlatnosci = new Label();
		comboBoxSposobPlatnosci = new ComboBox();
		buttonSposobPlatnosci = new ButtonDPI();
		textBoxNazwaBanku = new TextBox();
		label16 = new Label();
		buttonWaluta = new ButtonDPI();
		comboBoxWaluta = new ComboBox();
		tabPage2 = new TabPage();
		tableLayoutPanel4 = new TableLayoutPanel();
		groupBox1 = new GroupBox();
		textBoxUwagiWewnetrzne = new TextBox();
		groupBox2 = new GroupBox();
		textBoxUwagiPubliczne = new TextBox();
		tabPageFakturySprzedazy = new TabPage();
		tabPageFakturyZakupu = new TabPage();
		tabPagePodatki = new TabPage();
		tableLayoutPanel3 = new TableLayoutPanel();
		label10 = new Label();
		label11 = new Label();
		label12 = new Label();
		label13 = new Label();
		comboBoxKodUrzedu = new ComboBox();
		textBoxOsobaFizycznaImie = new TextBox();
		textBoxOsobaFizycznaNazwisko = new TextBox();
		dateTimePickerOsobaFizycznaDataUrodzenia = new DateTimePickerFix();
		buttonUrzadSkarbowy = new ButtonDPI();
		label14 = new Label();
		comboBoxFormaOpodatkowania = new ComboBox();
		label15 = new Label();
		textBoxTokenKSeF = new TextBox();
		comboBoxSrodowiskoKSeF = new ComboBox();
		flowLayoutPanel1 = new FlowLayoutPanel();
		buttonKSeFAuth = new ButtonDPI();
		buttonCertyfikatKSeF = new ButtonDPI();
		textBoxNazwa = new TextBox();
		label1 = new Label();
		tableLayoutPanel2 = new TableLayoutPanel();
		((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
		tabControl.SuspendLayout();
		tabPage1.SuspendLayout();
		tableLayoutPanel1.SuspendLayout();
		tabPage2.SuspendLayout();
		tableLayoutPanel4.SuspendLayout();
		groupBox1.SuspendLayout();
		groupBox2.SuspendLayout();
		tabPagePodatki.SuspendLayout();
		tableLayoutPanel3.SuspendLayout();
		flowLayoutPanel1.SuspendLayout();
		tableLayoutPanel2.SuspendLayout();
		SuspendLayout();
		// 
		// tabControl
		// 
		tableLayoutPanel2.SetColumnSpan(tabControl, 2);
		tabControl.Controls.Add(tabPage1);
		tabControl.Controls.Add(tabPage2);
		tabControl.Controls.Add(tabPageFakturySprzedazy);
		tabControl.Controls.Add(tabPageFakturyZakupu);
		tabControl.Controls.Add(tabPagePodatki);
		tabControl.Dock = DockStyle.Fill;
		tabControl.Location = new Point(3, 32);
		tabControl.Name = "tabControl";
		tabControl.SelectedIndex = 0;
		tabControl.Size = new Size(794, 458);
		tabControl.TabIndex = 2;
		// 
		// tabPage1
		// 
		tabPage1.Controls.Add(tableLayoutPanel1);
		tabPage1.Location = new Point(4, 24);
		tabPage1.Name = "tabPage1";
		tabPage1.Padding = new Padding(3);
		tabPage1.Size = new Size(786, 430);
		tabPage1.TabIndex = 0;
		tabPage1.Text = "Dane podstawowe";
		tabPage1.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.ColumnCount = 4;
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.Controls.Add(label2, 0, 0);
		tableLayoutPanel1.Controls.Add(textBoxPelnaNazwa, 1, 0);
		tableLayoutPanel1.Controls.Add(label3, 0, 1);
		tableLayoutPanel1.Controls.Add(textBoxNIP, 1, 1);
		tableLayoutPanel1.Controls.Add(textBoxAdresRejestrowy, 1, 2);
		tableLayoutPanel1.Controls.Add(textBoxAdresKorespondencyjny, 1, 3);
		tableLayoutPanel1.Controls.Add(textBoxTelefon, 1, 4);
		tableLayoutPanel1.Controls.Add(textBoxEMail, 1, 5);
		tableLayoutPanel1.Controls.Add(textBoxRachunekBankowy, 1, 6);
		tableLayoutPanel1.Controls.Add(label4, 0, 2);
		tableLayoutPanel1.Controls.Add(label5, 0, 3);
		tableLayoutPanel1.Controls.Add(label6, 0, 4);
		tableLayoutPanel1.Controls.Add(label7, 0, 5);
		tableLayoutPanel1.Controls.Add(label8, 0, 6);
		tableLayoutPanel1.Controls.Add(comboBoxStan, 1, 7);
		tableLayoutPanel1.Controls.Add(label9, 0, 7);
		tableLayoutPanel1.Controls.Add(checkBoxTP, 1, 10);
		tableLayoutPanel1.Controls.Add(buttonSprawdzMF, 3, 6);
		tableLayoutPanel1.Controls.Add(buttonPobierzGUS, 3, 1);
		tableLayoutPanel1.Controls.Add(labelSposobPlatnosci, 0, 9);
		tableLayoutPanel1.Controls.Add(comboBoxSposobPlatnosci, 1, 9);
		tableLayoutPanel1.Controls.Add(buttonSposobPlatnosci, 3, 9);
		tableLayoutPanel1.Controls.Add(textBoxNazwaBanku, 2, 6);
		tableLayoutPanel1.Controls.Add(label16, 0, 11);
		tableLayoutPanel1.Controls.Add(buttonWaluta, 2, 11);
		tableLayoutPanel1.Controls.Add(comboBoxWaluta, 1, 11);
		tableLayoutPanel1.Dock = DockStyle.Fill;
		tableLayoutPanel1.Location = new Point(3, 3);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 13;
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 16F));
		tableLayoutPanel1.Size = new Size(780, 424);
		tableLayoutPanel1.TabIndex = 0;
		// 
		// label2
		// 
		label2.Anchor = AnchorStyles.Right;
		label2.AutoSize = true;
		label2.Location = new Point(68, 7);
		label2.Name = "label2";
		label2.Size = new Size(72, 15);
		label2.TabIndex = 2;
		label2.Text = "Pełna nazwa";
		// 
		// textBoxPelnaNazwa
		// 
		textBoxPelnaNazwa.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(textBoxPelnaNazwa, 3);
		textBoxPelnaNazwa.Location = new Point(146, 3);
		textBoxPelnaNazwa.Name = "textBoxPelnaNazwa";
		textBoxPelnaNazwa.Size = new Size(631, 23);
		textBoxPelnaNazwa.TabIndex = 0;
		// 
		// label3
		// 
		label3.Anchor = AnchorStyles.Right;
		label3.AutoSize = true;
		label3.Location = new Point(114, 37);
		label3.Name = "label3";
		label3.Size = new Size(26, 15);
		label3.TabIndex = 2;
		label3.Text = "NIP";
		// 
		// textBoxNIP
		// 
		textBoxNIP.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(textBoxNIP, 2);
		textBoxNIP.Location = new Point(146, 33);
		textBoxNIP.Name = "textBoxNIP";
		textBoxNIP.Size = new Size(465, 23);
		textBoxNIP.TabIndex = 1;
		// 
		// textBoxAdresRejestrowy
		// 
		textBoxAdresRejestrowy.AcceptsReturn = true;
		textBoxAdresRejestrowy.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(textBoxAdresRejestrowy, 3);
		textBoxAdresRejestrowy.Location = new Point(146, 63);
		textBoxAdresRejestrowy.Multiline = true;
		textBoxAdresRejestrowy.Name = "textBoxAdresRejestrowy";
		textBoxAdresRejestrowy.Size = new Size(631, 65);
		textBoxAdresRejestrowy.TabIndex = 3;
		textBoxAdresRejestrowy.TextChanged += textBoxAdresRejestrowy_TextChanged;
		// 
		// textBoxAdresKorespondencyjny
		// 
		textBoxAdresKorespondencyjny.AcceptsReturn = true;
		textBoxAdresKorespondencyjny.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(textBoxAdresKorespondencyjny, 3);
		textBoxAdresKorespondencyjny.Location = new Point(146, 134);
		textBoxAdresKorespondencyjny.Multiline = true;
		textBoxAdresKorespondencyjny.Name = "textBoxAdresKorespondencyjny";
		textBoxAdresKorespondencyjny.Size = new Size(631, 65);
		textBoxAdresKorespondencyjny.TabIndex = 4;
		// 
		// textBoxTelefon
		// 
		textBoxTelefon.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(textBoxTelefon, 3);
		textBoxTelefon.Location = new Point(146, 205);
		textBoxTelefon.Name = "textBoxTelefon";
		textBoxTelefon.Size = new Size(631, 23);
		textBoxTelefon.TabIndex = 5;
		// 
		// textBoxEMail
		// 
		textBoxEMail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(textBoxEMail, 3);
		textBoxEMail.Location = new Point(146, 234);
		textBoxEMail.Name = "textBoxEMail";
		textBoxEMail.Size = new Size(631, 23);
		textBoxEMail.TabIndex = 6;
		// 
		// textBoxRachunekBankowy
		// 
		textBoxRachunekBankowy.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxRachunekBankowy.Location = new Point(146, 264);
		textBoxRachunekBankowy.Name = "textBoxRachunekBankowy";
		textBoxRachunekBankowy.Size = new Size(263, 23);
		textBoxRachunekBankowy.TabIndex = 7;
		// 
		// label4
		// 
		label4.Anchor = AnchorStyles.Right;
		label4.AutoSize = true;
		label4.Location = new Point(46, 88);
		label4.Name = "label4";
		label4.Size = new Size(94, 15);
		label4.TabIndex = 2;
		label4.Text = "Adres rejestrowy";
		// 
		// label5
		// 
		label5.Anchor = AnchorStyles.Right;
		label5.AutoSize = true;
		label5.Location = new Point(3, 159);
		label5.Name = "label5";
		label5.Size = new Size(137, 15);
		label5.TabIndex = 2;
		label5.Text = "Adres korespondencyjny";
		// 
		// label6
		// 
		label6.Anchor = AnchorStyles.Right;
		label6.AutoSize = true;
		label6.Location = new Point(94, 209);
		label6.Name = "label6";
		label6.Size = new Size(46, 15);
		label6.TabIndex = 2;
		label6.Text = "Telefon";
		// 
		// label7
		// 
		label7.Anchor = AnchorStyles.Right;
		label7.AutoSize = true;
		label7.Location = new Point(99, 238);
		label7.Name = "label7";
		label7.Size = new Size(41, 15);
		label7.TabIndex = 2;
		label7.Text = "E-Mail";
		// 
		// label8
		// 
		label8.Anchor = AnchorStyles.Right;
		label8.AutoSize = true;
		label8.Location = new Point(30, 268);
		label8.Name = "label8";
		label8.Size = new Size(110, 15);
		label8.TabIndex = 2;
		label8.Text = "Rachunek bankowy";
		// 
		// comboBoxStan
		// 
		comboBoxStan.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(comboBoxStan, 3);
		comboBoxStan.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBoxStan.FormattingEnabled = true;
		comboBoxStan.Location = new Point(146, 294);
		comboBoxStan.Name = "comboBoxStan";
		comboBoxStan.Size = new Size(631, 23);
		comboBoxStan.TabIndex = 10;
		// 
		// label9
		// 
		label9.Anchor = AnchorStyles.Right;
		label9.AutoSize = true;
		label9.Location = new Point(110, 298);
		label9.Name = "label9";
		label9.Size = new Size(30, 15);
		label9.TabIndex = 2;
		label9.Text = "Stan";
		// 
		// checkBoxTP
		// 
		checkBoxTP.AutoSize = true;
		checkBoxTP.Location = new Point(146, 354);
		checkBoxTP.Name = "checkBoxTP";
		checkBoxTP.Size = new Size(131, 19);
		checkBoxTP.TabIndex = 13;
		checkBoxTP.Text = "Podmiot powiązany";
		checkBoxTP.UseVisualStyleBackColor = true;
		// 
		// buttonSprawdzMF
		// 
		buttonSprawdzMF.AutoSize = true;
		buttonSprawdzMF.Location = new Point(617, 263);
		buttonSprawdzMF.Name = "buttonSprawdzMF";
		buttonSprawdzMF.Size = new Size(160, 25);
		buttonSprawdzMF.TabIndex = 9;
		buttonSprawdzMF.Text = "Sprawdź na białej liście VAT";
		buttonSprawdzMF.UseVisualStyleBackColor = true;
		buttonSprawdzMF.Click += buttonSprawdzMF_Click;
		// 
		// buttonPobierzGUS
		// 
		buttonPobierzGUS.AutoSize = true;
		buttonPobierzGUS.Location = new Point(617, 32);
		buttonPobierzGUS.Name = "buttonPobierzGUS";
		buttonPobierzGUS.Size = new Size(118, 25);
		buttonPobierzGUS.TabIndex = 2;
		buttonPobierzGUS.Text = "Pobierz dane z GUS";
		buttonPobierzGUS.UseVisualStyleBackColor = true;
		buttonPobierzGUS.Click += buttonPobierzGUS_Click;
		// 
		// labelSposobPlatnosci
		// 
		labelSposobPlatnosci.Anchor = AnchorStyles.Right;
		labelSposobPlatnosci.AutoSize = true;
		labelSposobPlatnosci.Location = new Point(43, 328);
		labelSposobPlatnosci.Name = "labelSposobPlatnosci";
		labelSposobPlatnosci.Size = new Size(97, 15);
		labelSposobPlatnosci.TabIndex = 2;
		labelSposobPlatnosci.Text = "Sposób płatności";
		// 
		// comboBoxSposobPlatnosci
		// 
		comboBoxSposobPlatnosci.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(comboBoxSposobPlatnosci, 2);
		comboBoxSposobPlatnosci.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBoxSposobPlatnosci.FormattingEnabled = true;
		comboBoxSposobPlatnosci.Location = new Point(146, 324);
		comboBoxSposobPlatnosci.Name = "comboBoxSposobPlatnosci";
		comboBoxSposobPlatnosci.Size = new Size(465, 23);
		comboBoxSposobPlatnosci.TabIndex = 11;
		// 
		// buttonSposobPlatnosci
		// 
		buttonSposobPlatnosci.AutoSize = true;
		buttonSposobPlatnosci.Location = new Point(617, 323);
		buttonSposobPlatnosci.Name = "buttonSposobPlatnosci";
		buttonSposobPlatnosci.Size = new Size(26, 25);
		buttonSposobPlatnosci.TabIndex = 12;
		buttonSposobPlatnosci.Text = "...";
		buttonSposobPlatnosci.UseVisualStyleBackColor = true;
		buttonSposobPlatnosci.Click += buttonSprawdzMF_Click;
		// 
		// textBoxNazwaBanku
		// 
		textBoxNazwaBanku.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxNazwaBanku.Location = new Point(415, 264);
		textBoxNazwaBanku.Name = "textBoxNazwaBanku";
		textBoxNazwaBanku.PlaceholderText = "Nazwa banku";
		textBoxNazwaBanku.Size = new Size(196, 23);
		textBoxNazwaBanku.TabIndex = 8;
		// 
		// label16
		// 
		label16.Anchor = AnchorStyles.Right;
		label16.AutoSize = true;
		label16.Location = new Point(42, 384);
		label16.Name = "label16";
		label16.Size = new Size(98, 15);
		label16.TabIndex = 14;
		label16.Text = "Domyślna waluta";
		// 
		// buttonWaluta
		// 
		buttonWaluta.AutoSize = true;
		buttonWaluta.Location = new Point(415, 379);
		buttonWaluta.Name = "buttonWaluta";
		buttonWaluta.Size = new Size(26, 25);
		buttonWaluta.TabIndex = 16;
		buttonWaluta.Text = "...";
		buttonWaluta.UseVisualStyleBackColor = true;
		// 
		// comboBoxWaluta
		// 
		comboBoxWaluta.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBoxWaluta.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBoxWaluta.FormattingEnabled = true;
		comboBoxWaluta.Location = new Point(146, 380);
		comboBoxWaluta.Name = "comboBoxWaluta";
		comboBoxWaluta.Size = new Size(263, 23);
		comboBoxWaluta.TabIndex = 15;
		// 
		// tabPage2
		// 
		tabPage2.Controls.Add(tableLayoutPanel4);
		tabPage2.Location = new Point(4, 24);
		tabPage2.Name = "tabPage2";
		tabPage2.Padding = new Padding(3);
		tabPage2.Size = new Size(786, 430);
		tabPage2.TabIndex = 1;
		tabPage2.Text = "Uwagi";
		tabPage2.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel4
		// 
		tableLayoutPanel4.ColumnCount = 1;
		tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
		tableLayoutPanel4.Controls.Add(groupBox1, 0, 0);
		tableLayoutPanel4.Controls.Add(groupBox2, 0, 1);
		tableLayoutPanel4.Dock = DockStyle.Fill;
		tableLayoutPanel4.Location = new Point(3, 3);
		tableLayoutPanel4.Name = "tableLayoutPanel4";
		tableLayoutPanel4.RowCount = 2;
		tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
		tableLayoutPanel4.Size = new Size(780, 424);
		tableLayoutPanel4.TabIndex = 2;
		// 
		// groupBox1
		// 
		groupBox1.Controls.Add(textBoxUwagiWewnetrzne);
		groupBox1.Dock = DockStyle.Fill;
		groupBox1.Location = new Point(3, 3);
		groupBox1.Name = "groupBox1";
		groupBox1.Size = new Size(774, 206);
		groupBox1.TabIndex = 1;
		groupBox1.TabStop = false;
		groupBox1.Text = "Uwagi (wewnętrzne)";
		// 
		// textBoxUwagiWewnetrzne
		// 
		textBoxUwagiWewnetrzne.AcceptsReturn = true;
		textBoxUwagiWewnetrzne.Dock = DockStyle.Fill;
		textBoxUwagiWewnetrzne.Location = new Point(3, 19);
		textBoxUwagiWewnetrzne.Multiline = true;
		textBoxUwagiWewnetrzne.Name = "textBoxUwagiWewnetrzne";
		textBoxUwagiWewnetrzne.Size = new Size(768, 184);
		textBoxUwagiWewnetrzne.TabIndex = 0;
		// 
		// groupBox2
		// 
		groupBox2.Controls.Add(textBoxUwagiPubliczne);
		groupBox2.Dock = DockStyle.Fill;
		groupBox2.Location = new Point(3, 215);
		groupBox2.Name = "groupBox2";
		groupBox2.Size = new Size(774, 206);
		groupBox2.TabIndex = 1;
		groupBox2.TabStop = false;
		groupBox2.Text = "Uwagi (drukowane na fakturze)";
		// 
		// textBoxUwagiPubliczne
		// 
		textBoxUwagiPubliczne.AcceptsReturn = true;
		textBoxUwagiPubliczne.Dock = DockStyle.Fill;
		textBoxUwagiPubliczne.Location = new Point(3, 19);
		textBoxUwagiPubliczne.Multiline = true;
		textBoxUwagiPubliczne.Name = "textBoxUwagiPubliczne";
		textBoxUwagiPubliczne.Size = new Size(768, 184);
		textBoxUwagiPubliczne.TabIndex = 0;
		// 
		// tabPageFakturySprzedazy
		// 
		tabPageFakturySprzedazy.Location = new Point(4, 24);
		tabPageFakturySprzedazy.Name = "tabPageFakturySprzedazy";
		tabPageFakturySprzedazy.Padding = new Padding(3);
		tabPageFakturySprzedazy.Size = new Size(786, 430);
		tabPageFakturySprzedazy.TabIndex = 2;
		tabPageFakturySprzedazy.Text = "Sprzedaż do";
		tabPageFakturySprzedazy.UseVisualStyleBackColor = true;
		// 
		// tabPageFakturyZakupu
		// 
		tabPageFakturyZakupu.Location = new Point(4, 24);
		tabPageFakturyZakupu.Name = "tabPageFakturyZakupu";
		tabPageFakturyZakupu.Padding = new Padding(3);
		tabPageFakturyZakupu.Size = new Size(786, 430);
		tabPageFakturyZakupu.TabIndex = 3;
		tabPageFakturyZakupu.Text = "Zakup od";
		tabPageFakturyZakupu.UseVisualStyleBackColor = true;
		// 
		// tabPagePodatki
		// 
		tabPagePodatki.Controls.Add(tableLayoutPanel3);
		tabPagePodatki.Location = new Point(4, 24);
		tabPagePodatki.Name = "tabPagePodatki";
		tabPagePodatki.Padding = new Padding(3);
		tabPagePodatki.Size = new Size(786, 430);
		tabPagePodatki.TabIndex = 4;
		tabPagePodatki.Text = "Dane urzędowe";
		tabPagePodatki.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel3
		// 
		tableLayoutPanel3.ColumnCount = 5;
		tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel3.Controls.Add(label10, 0, 0);
		tableLayoutPanel3.Controls.Add(label11, 0, 1);
		tableLayoutPanel3.Controls.Add(label12, 0, 2);
		tableLayoutPanel3.Controls.Add(label13, 0, 3);
		tableLayoutPanel3.Controls.Add(comboBoxKodUrzedu, 1, 0);
		tableLayoutPanel3.Controls.Add(textBoxOsobaFizycznaImie, 1, 1);
		tableLayoutPanel3.Controls.Add(textBoxOsobaFizycznaNazwisko, 1, 2);
		tableLayoutPanel3.Controls.Add(dateTimePickerOsobaFizycznaDataUrodzenia, 1, 3);
		tableLayoutPanel3.Controls.Add(buttonUrzadSkarbowy, 4, 0);
		tableLayoutPanel3.Controls.Add(label14, 0, 4);
		tableLayoutPanel3.Controls.Add(comboBoxFormaOpodatkowania, 1, 4);
		tableLayoutPanel3.Controls.Add(label15, 0, 5);
		tableLayoutPanel3.Controls.Add(textBoxTokenKSeF, 2, 5);
		tableLayoutPanel3.Controls.Add(comboBoxSrodowiskoKSeF, 1, 5);
		tableLayoutPanel3.Controls.Add(flowLayoutPanel1, 3, 5);
		tableLayoutPanel3.Dock = DockStyle.Fill;
		tableLayoutPanel3.Location = new Point(3, 3);
		tableLayoutPanel3.Name = "tableLayoutPanel3";
		tableLayoutPanel3.RowCount = 7;
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
		tableLayoutPanel3.Size = new Size(780, 424);
		tableLayoutPanel3.TabIndex = 0;
		// 
		// label10
		// 
		label10.Anchor = AnchorStyles.Right;
		label10.AutoSize = true;
		label10.Location = new Point(3, 8);
		label10.Name = "label10";
		label10.Size = new Size(134, 15);
		label10.TabIndex = 0;
		label10.Text = "Kod urzędu skarbowego";
		// 
		// label11
		// 
		label11.Anchor = AnchorStyles.Right;
		label11.AutoSize = true;
		label11.Location = new Point(59, 38);
		label11.Name = "label11";
		label11.Size = new Size(78, 15);
		label11.TabIndex = 0;
		label11.Text = "Pierwsze imię";
		// 
		// label12
		// 
		label12.Anchor = AnchorStyles.Right;
		label12.AutoSize = true;
		label12.Location = new Point(80, 67);
		label12.Name = "label12";
		label12.Size = new Size(57, 15);
		label12.TabIndex = 0;
		label12.Text = "Nazwisko";
		// 
		// label13
		// 
		label13.Anchor = AnchorStyles.Right;
		label13.AutoSize = true;
		label13.Location = new Point(51, 96);
		label13.Name = "label13";
		label13.Size = new Size(86, 15);
		label13.TabIndex = 0;
		label13.Text = "Data urodzenia";
		// 
		// comboBoxKodUrzedu
		// 
		comboBoxKodUrzedu.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel3.SetColumnSpan(comboBoxKodUrzedu, 3);
		comboBoxKodUrzedu.FormattingEnabled = true;
		comboBoxKodUrzedu.Location = new Point(143, 4);
		comboBoxKodUrzedu.Name = "comboBoxKodUrzedu";
		comboBoxKodUrzedu.Size = new Size(602, 23);
		comboBoxKodUrzedu.TabIndex = 0;
		// 
		// textBoxOsobaFizycznaImie
		// 
		textBoxOsobaFizycznaImie.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel3.SetColumnSpan(textBoxOsobaFizycznaImie, 4);
		textBoxOsobaFizycznaImie.Location = new Point(143, 34);
		textBoxOsobaFizycznaImie.Name = "textBoxOsobaFizycznaImie";
		textBoxOsobaFizycznaImie.Size = new Size(634, 23);
		textBoxOsobaFizycznaImie.TabIndex = 2;
		// 
		// textBoxOsobaFizycznaNazwisko
		// 
		textBoxOsobaFizycznaNazwisko.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel3.SetColumnSpan(textBoxOsobaFizycznaNazwisko, 4);
		textBoxOsobaFizycznaNazwisko.Location = new Point(143, 63);
		textBoxOsobaFizycznaNazwisko.Name = "textBoxOsobaFizycznaNazwisko";
		textBoxOsobaFizycznaNazwisko.Size = new Size(634, 23);
		textBoxOsobaFizycznaNazwisko.TabIndex = 3;
		// 
		// dateTimePickerOsobaFizycznaDataUrodzenia
		// 
		tableLayoutPanel3.SetColumnSpan(dateTimePickerOsobaFizycznaDataUrodzenia, 2);
		dateTimePickerOsobaFizycznaDataUrodzenia.Location = new Point(143, 92);
		dateTimePickerOsobaFizycznaDataUrodzenia.Name = "dateTimePickerOsobaFizycznaDataUrodzenia";
		dateTimePickerOsobaFizycznaDataUrodzenia.ShowCheckBox = true;
		dateTimePickerOsobaFizycznaDataUrodzenia.Size = new Size(200, 23);
		dateTimePickerOsobaFizycznaDataUrodzenia.TabIndex = 4;
		// 
		// buttonUrzadSkarbowy
		// 
		buttonUrzadSkarbowy.Anchor = AnchorStyles.Left;
		buttonUrzadSkarbowy.AutoSize = true;
		buttonUrzadSkarbowy.Location = new Point(751, 3);
		buttonUrzadSkarbowy.Name = "buttonUrzadSkarbowy";
		buttonUrzadSkarbowy.Size = new Size(26, 25);
		buttonUrzadSkarbowy.TabIndex = 1;
		buttonUrzadSkarbowy.Text = "...";
		buttonUrzadSkarbowy.UseVisualStyleBackColor = true;
		// 
		// label14
		// 
		label14.Anchor = AnchorStyles.Right;
		label14.AutoSize = true;
		label14.Location = new Point(11, 125);
		label14.Name = "label14";
		label14.Size = new Size(126, 15);
		label14.TabIndex = 0;
		label14.Text = "Forma opodatkowania";
		// 
		// comboBoxFormaOpodatkowania
		// 
		comboBoxFormaOpodatkowania.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel3.SetColumnSpan(comboBoxFormaOpodatkowania, 4);
		comboBoxFormaOpodatkowania.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBoxFormaOpodatkowania.FormattingEnabled = true;
		comboBoxFormaOpodatkowania.Location = new Point(143, 121);
		comboBoxFormaOpodatkowania.Name = "comboBoxFormaOpodatkowania";
		comboBoxFormaOpodatkowania.Size = new Size(634, 23);
		comboBoxFormaOpodatkowania.TabIndex = 5;
		// 
		// label15
		// 
		label15.Anchor = AnchorStyles.Right;
		label15.AutoSize = true;
		label15.Location = new Point(70, 155);
		label15.Name = "label15";
		label15.Size = new Size(67, 15);
		label15.TabIndex = 0;
		label15.Text = "Token KSeF";
		// 
		// textBoxTokenKSeF
		// 
		textBoxTokenKSeF.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxTokenKSeF.Location = new Point(220, 151);
		textBoxTokenKSeF.Name = "textBoxTokenKSeF";
		textBoxTokenKSeF.Size = new Size(335, 23);
		textBoxTokenKSeF.TabIndex = 7;
		// 
		// comboBoxSrodowiskoKSeF
		// 
		comboBoxSrodowiskoKSeF.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBoxSrodowiskoKSeF.FormattingEnabled = true;
		comboBoxSrodowiskoKSeF.Location = new Point(143, 150);
		comboBoxSrodowiskoKSeF.Name = "comboBoxSrodowiskoKSeF";
		comboBoxSrodowiskoKSeF.Size = new Size(71, 23);
		comboBoxSrodowiskoKSeF.TabIndex = 6;
		// 
		// flowLayoutPanel1
		// 
		flowLayoutPanel1.AutoSize = true;
		tableLayoutPanel3.SetColumnSpan(flowLayoutPanel1, 2);
		flowLayoutPanel1.Controls.Add(buttonKSeFAuth);
		flowLayoutPanel1.Controls.Add(buttonCertyfikatKSeF);
		flowLayoutPanel1.Location = new Point(558, 147);
		flowLayoutPanel1.Margin = new Padding(0);
		flowLayoutPanel1.Name = "flowLayoutPanel1";
		flowLayoutPanel1.Size = new Size(222, 31);
		flowLayoutPanel1.TabIndex = 9;
		flowLayoutPanel1.WrapContents = false;
		// 
		// buttonKSeFAuth
		// 
		buttonKSeFAuth.Anchor = AnchorStyles.Left;
		buttonKSeFAuth.AutoSize = true;
		buttonKSeFAuth.Location = new Point(3, 3);
		buttonKSeFAuth.Name = "buttonKSeFAuth";
		buttonKSeFAuth.Size = new Size(95, 25);
		buttonKSeFAuth.TabIndex = 8;
		buttonKSeFAuth.Text = "Uzyskaj dostęp";
		buttonKSeFAuth.UseVisualStyleBackColor = true;
		buttonKSeFAuth.Click += buttonKSeFAuth_Click;
		// 
		// buttonCertyfikatKSeF
		// 
		buttonCertyfikatKSeF.Anchor = AnchorStyles.Left;
		buttonCertyfikatKSeF.AutoSize = true;
		buttonCertyfikatKSeF.Location = new Point(104, 3);
		buttonCertyfikatKSeF.Name = "buttonCertyfikatKSeF";
		buttonCertyfikatKSeF.Size = new Size(115, 25);
		buttonCertyfikatKSeF.TabIndex = 8;
		buttonCertyfikatKSeF.Text = "Importuj certyfikat";
		buttonCertyfikatKSeF.UseVisualStyleBackColor = true;
		buttonCertyfikatKSeF.Click += buttonCertyfikatKSeF_Click;
		// 
		// textBoxNazwa
		// 
		textBoxNazwa.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxNazwa.Location = new Point(51, 3);
		textBoxNazwa.Name = "textBoxNazwa";
		textBoxNazwa.Size = new Size(746, 23);
		textBoxNazwa.TabIndex = 1;
		textBoxNazwa.TextChanged += textBoxNazwa_TextChanged;
		// 
		// label1
		// 
		label1.Anchor = AnchorStyles.Right;
		label1.AutoSize = true;
		label1.Location = new Point(3, 7);
		label1.Name = "label1";
		label1.Size = new Size(42, 15);
		label1.TabIndex = 2;
		label1.Text = "Nazwa";
		// 
		// tableLayoutPanel2
		// 
		tableLayoutPanel2.ColumnCount = 2;
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.Controls.Add(label1, 0, 0);
		tableLayoutPanel2.Controls.Add(tabControl, 0, 1);
		tableLayoutPanel2.Controls.Add(textBoxNazwa, 1, 0);
		tableLayoutPanel2.Dock = DockStyle.Fill;
		tableLayoutPanel2.Location = new Point(0, 0);
		tableLayoutPanel2.Name = "tableLayoutPanel2";
		tableLayoutPanel2.RowCount = 2;
		tableLayoutPanel2.RowStyles.Add(new RowStyle());
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
		tableLayoutPanel2.Size = new Size(800, 493);
		tableLayoutPanel2.TabIndex = 3;
		// 
		// KontrahentEdytor
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		Controls.Add(tableLayoutPanel2);
		MinimumSize = new Size(800, 425);
		Name = "KontrahentEdytor";
		Size = new Size(800, 493);
		((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
		tabControl.ResumeLayout(false);
		tabPage1.ResumeLayout(false);
		tableLayoutPanel1.ResumeLayout(false);
		tableLayoutPanel1.PerformLayout();
		tabPage2.ResumeLayout(false);
		tableLayoutPanel4.ResumeLayout(false);
		groupBox1.ResumeLayout(false);
		groupBox1.PerformLayout();
		groupBox2.ResumeLayout(false);
		groupBox2.PerformLayout();
		tabPagePodatki.ResumeLayout(false);
		tableLayoutPanel3.ResumeLayout(false);
		tableLayoutPanel3.PerformLayout();
		flowLayoutPanel1.ResumeLayout(false);
		flowLayoutPanel1.PerformLayout();
		tableLayoutPanel2.ResumeLayout(false);
		tableLayoutPanel2.PerformLayout();
		ResumeLayout(false);
	}

	#endregion
	private System.Windows.Forms.TabControl tabControl;
	private System.Windows.Forms.TabPage tabPage1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.TabPage tabPage2;
	private System.Windows.Forms.TextBox textBoxNazwa;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.TextBox textBoxPelnaNazwa;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.TextBox textBoxNIP;
	private System.Windows.Forms.TextBox textBoxAdresRejestrowy;
	private System.Windows.Forms.TextBox textBoxAdresKorespondencyjny;
	private System.Windows.Forms.TextBox textBoxTelefon;
	private System.Windows.Forms.TextBox textBoxEMail;
	private System.Windows.Forms.TextBox textBoxRachunekBankowy;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.Label label5;
	private System.Windows.Forms.Label label6;
	private System.Windows.Forms.Label label7;
	private System.Windows.Forms.Label label8;
	private System.Windows.Forms.TextBox textBoxUwagiWewnetrzne;
	private System.Windows.Forms.ComboBox comboBoxStan;
	private System.Windows.Forms.Label label9;
	private System.Windows.Forms.CheckBox checkBoxTP;
	private System.Windows.Forms.TabPage tabPageFakturySprzedazy;
	private System.Windows.Forms.TabPage tabPageFakturyZakupu;
	private System.Windows.Forms.TabPage tabPagePodatki;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
	private System.Windows.Forms.Label label10;
	private System.Windows.Forms.Label label11;
	private System.Windows.Forms.Label label12;
	private System.Windows.Forms.Label label13;
	private System.Windows.Forms.ComboBox comboBoxKodUrzedu;
	private System.Windows.Forms.TextBox textBoxOsobaFizycznaImie;
	private System.Windows.Forms.TextBox textBoxOsobaFizycznaNazwisko;
	private ButtonDPI buttonUrzadSkarbowy;
	private System.Windows.Forms.Label label14;
	private System.Windows.Forms.ComboBox comboBoxFormaOpodatkowania;
	private ButtonDPI buttonSprawdzMF;
	private ButtonDPI buttonPobierzGUS;
	private System.Windows.Forms.Label label15;
	private System.Windows.Forms.TextBox textBoxTokenKSeF;
	private System.Windows.Forms.ComboBox comboBoxSrodowiskoKSeF;
	private ButtonDPI buttonKSeFAuth;
	private System.Windows.Forms.DateTimePickerFix dateTimePickerOsobaFizycznaDataUrodzenia;
	private System.Windows.Forms.Label labelSposobPlatnosci;
	private System.Windows.Forms.ComboBox comboBoxSposobPlatnosci;
	private ButtonDPI buttonSposobPlatnosci;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxUwagiPubliczne;
	private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	private ButtonDPI buttonCertyfikatKSeF;
	private System.Windows.Forms.TextBox textBoxNazwaBanku;
	private Label label16;
	private ButtonDPI buttonWaluta;
	private ComboBox comboBoxWaluta;
}
