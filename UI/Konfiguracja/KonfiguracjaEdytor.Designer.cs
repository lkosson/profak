
namespace ProFak.UI;

partial class KonfiguracjaEdytor
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
		tabPageEMail = new TabPage();
		tableLayoutPanel1 = new TableLayoutPanel();
		label2 = new Label();
		textBoxSMTPSerwer = new TextBox();
		label3 = new Label();
		label4 = new Label();
		label5 = new Label();
		label7 = new Label();
		label8 = new Label();
		textBoxEMailTresc = new TextBox();
		textBoxSMTPLogin = new TextBox();
		numericUpDownSMTPort = new NumericUpDownDPI();
		textBoxEMailNadawca = new TextBox();
		textBoxSMTPHaslo = new TextBox();
		textBoxEMailTemat = new TextBox();
		flowLayoutPanel1 = new FlowLayoutPanel();
		label9 = new Label();
		linkLabelTrescPomoc = new LinkLabel();
		tabPageWyglad = new TabPage();
		tableLayoutPanel3 = new TableLayoutPanel();
		checkBoxSkrotyKlawiaturoweAkcji = new CheckBox();
		checkBoxSkrotyKlawiaturoweZakladek = new CheckBox();
		checkBoxSkrotyKlawiaturowePrzyciskow = new CheckBox();
		checkBoxIkonyAkcji = new CheckBox();
		checkBoxDomyslnyPodgladStrony = new CheckBox();
		checkBoxPotwierdzanieZamknieciaEdytora = new CheckBox();
		checkBoxPotwierdzanieZamknieciaProgramu = new CheckBox();
		checkBoxWstepneLadowanieReportingServices = new CheckBox();
		numericUpDownSzerokoscMenu = new NumericUpDownDPI();
		textBoxNazwaCzcionki = new TextBox();
		label1 = new Label();
		label6 = new Label();
		buttonWybierzCzcionke = new ButtonDPI();
		checkBoxPrzywrocUstawieniaSpisow = new CheckBox();
		checkBoxPrzywrocUstawieniaMenu = new CheckBox();
		label11 = new Label();
		numericUpDownWysokoscWierszy = new NumericUpDownDPI();
		label10 = new Label();
		label12 = new Label();
		label13 = new Label();
		label14 = new Label();
		comboBoxFormatDaty = new ComboBoxFix();
		comboBoxFormatCzasu = new ComboBoxFix();
		comboBoxFormatKwoty = new ComboBoxFix();
		textBoxPrzykladDaty = new TextBox();
		textBoxPrzykladCzasu = new TextBox();
		textBoxPrzykladKwoty = new TextBox();
		linkLabelFormatDaty = new LinkLabel();
		linkLabelFormatCzasu = new LinkLabel();
		linkLabelFormatKwoty = new LinkLabel();
		textBoxRozmiarCzcionki = new TextBox();
		tableLayoutPanel2 = new TableLayoutPanel();
		((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
		tabControl.SuspendLayout();
		tabPageEMail.SuspendLayout();
		tableLayoutPanel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDownSMTPort).BeginInit();
		flowLayoutPanel1.SuspendLayout();
		tabPageWyglad.SuspendLayout();
		tableLayoutPanel3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDownSzerokoscMenu).BeginInit();
		((System.ComponentModel.ISupportInitialize)numericUpDownWysokoscWierszy).BeginInit();
		tableLayoutPanel2.SuspendLayout();
		SuspendLayout();
		// 
		// tabControl
		// 
		tableLayoutPanel2.SetColumnSpan(tabControl, 2);
		tabControl.Controls.Add(tabPageEMail);
		tabControl.Controls.Add(tabPageWyglad);
		tabControl.Dock = DockStyle.Fill;
		tabControl.Location = new Point(3, 3);
		tabControl.Name = "tabControl";
		tabControl.SelectedIndex = 0;
		tabControl.Size = new Size(794, 497);
		tabControl.TabIndex = 2;
		// 
		// tabPageEMail
		// 
		tabPageEMail.Controls.Add(tableLayoutPanel1);
		tabPageEMail.Location = new Point(4, 24);
		tabPageEMail.Name = "tabPageEMail";
		tabPageEMail.Padding = new Padding(3);
		tabPageEMail.Size = new Size(786, 469);
		tabPageEMail.TabIndex = 0;
		tabPageEMail.Text = "Konfiguracja e-mail";
		tabPageEMail.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.ColumnCount = 2;
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.Controls.Add(label2, 0, 0);
		tableLayoutPanel1.Controls.Add(textBoxSMTPSerwer, 1, 0);
		tableLayoutPanel1.Controls.Add(label3, 0, 1);
		tableLayoutPanel1.Controls.Add(label4, 0, 2);
		tableLayoutPanel1.Controls.Add(label5, 0, 3);
		tableLayoutPanel1.Controls.Add(label7, 0, 4);
		tableLayoutPanel1.Controls.Add(label8, 0, 5);
		tableLayoutPanel1.Controls.Add(textBoxEMailTresc, 1, 6);
		tableLayoutPanel1.Controls.Add(textBoxSMTPLogin, 1, 2);
		tableLayoutPanel1.Controls.Add(numericUpDownSMTPort, 1, 1);
		tableLayoutPanel1.Controls.Add(textBoxEMailNadawca, 1, 4);
		tableLayoutPanel1.Controls.Add(textBoxSMTPHaslo, 1, 3);
		tableLayoutPanel1.Controls.Add(textBoxEMailTemat, 1, 5);
		tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 6);
		tableLayoutPanel1.Dock = DockStyle.Fill;
		tableLayoutPanel1.Location = new Point(3, 3);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 10;
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
		tableLayoutPanel1.Size = new Size(780, 463);
		tableLayoutPanel1.TabIndex = 0;
		// 
		// label2
		// 
		label2.Anchor = AnchorStyles.Right;
		label2.AutoSize = true;
		label2.Location = new Point(34, 7);
		label2.Name = "label2";
		label2.Size = new Size(75, 15);
		label2.TabIndex = 2;
		label2.Text = "Serwer SMTP";
		// 
		// textBoxSMTPSerwer
		// 
		textBoxSMTPSerwer.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxSMTPSerwer.Location = new Point(115, 3);
		textBoxSMTPSerwer.Name = "textBoxSMTPSerwer";
		textBoxSMTPSerwer.Size = new Size(662, 23);
		textBoxSMTPSerwer.TabIndex = 0;
		// 
		// label3
		// 
		label3.Anchor = AnchorStyles.Right;
		label3.AutoSize = true;
		label3.Location = new Point(80, 36);
		label3.Name = "label3";
		label3.Size = new Size(29, 15);
		label3.TabIndex = 2;
		label3.Text = "Port";
		// 
		// label4
		// 
		label4.Anchor = AnchorStyles.Right;
		label4.AutoSize = true;
		label4.Location = new Point(72, 65);
		label4.Name = "label4";
		label4.Size = new Size(37, 15);
		label4.TabIndex = 2;
		label4.Text = "Login";
		// 
		// label5
		// 
		label5.Anchor = AnchorStyles.Right;
		label5.AutoSize = true;
		label5.Location = new Point(72, 94);
		label5.Name = "label5";
		label5.Size = new Size(37, 15);
		label5.TabIndex = 2;
		label5.Text = "Hasło";
		// 
		// label7
		// 
		label7.Anchor = AnchorStyles.Right;
		label7.AutoSize = true;
		label7.Location = new Point(22, 123);
		label7.Name = "label7";
		label7.Size = new Size(87, 15);
		label7.TabIndex = 2;
		label7.Text = "Adres nadawcy";
		// 
		// label8
		// 
		label8.Anchor = AnchorStyles.Right;
		label8.AutoSize = true;
		label8.Location = new Point(3, 152);
		label8.Name = "label8";
		label8.Size = new Size(106, 15);
		label8.TabIndex = 2;
		label8.Text = "Temat wiadomości";
		// 
		// textBoxEMailTresc
		// 
		textBoxEMailTresc.AcceptsReturn = true;
		textBoxEMailTresc.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxEMailTresc.Location = new Point(115, 177);
		textBoxEMailTresc.Multiline = true;
		textBoxEMailTresc.Name = "textBoxEMailTresc";
		textBoxEMailTresc.ScrollBars = ScrollBars.Both;
		textBoxEMailTresc.Size = new Size(662, 173);
		textBoxEMailTresc.TabIndex = 6;
		// 
		// textBoxSMTPLogin
		// 
		textBoxSMTPLogin.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxSMTPLogin.Location = new Point(115, 61);
		textBoxSMTPLogin.Name = "textBoxSMTPLogin";
		textBoxSMTPLogin.Size = new Size(662, 23);
		textBoxSMTPLogin.TabIndex = 2;
		// 
		// numericUpDownSMTPort
		// 
		numericUpDownSMTPort.Location = new Point(115, 32);
		numericUpDownSMTPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
		numericUpDownSMTPort.Name = "numericUpDownSMTPort";
		numericUpDownSMTPort.Size = new Size(72, 23);
		numericUpDownSMTPort.TabIndex = 1;
		numericUpDownSMTPort.TextAlign = HorizontalAlignment.Right;
		// 
		// textBoxEMailNadawca
		// 
		textBoxEMailNadawca.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxEMailNadawca.Location = new Point(115, 119);
		textBoxEMailNadawca.Name = "textBoxEMailNadawca";
		textBoxEMailNadawca.Size = new Size(662, 23);
		textBoxEMailNadawca.TabIndex = 4;
		// 
		// textBoxSMTPHaslo
		// 
		textBoxSMTPHaslo.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxSMTPHaslo.Location = new Point(115, 90);
		textBoxSMTPHaslo.Name = "textBoxSMTPHaslo";
		textBoxSMTPHaslo.Size = new Size(662, 23);
		textBoxSMTPHaslo.TabIndex = 3;
		textBoxSMTPHaslo.UseSystemPasswordChar = true;
		// 
		// textBoxEMailTemat
		// 
		textBoxEMailTemat.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		textBoxEMailTemat.Location = new Point(115, 148);
		textBoxEMailTemat.Name = "textBoxEMailTemat";
		textBoxEMailTemat.Size = new Size(662, 23);
		textBoxEMailTemat.TabIndex = 5;
		// 
		// flowLayoutPanel1
		// 
		flowLayoutPanel1.Anchor = AnchorStyles.Right;
		flowLayoutPanel1.AutoSize = true;
		flowLayoutPanel1.Controls.Add(label9);
		flowLayoutPanel1.Controls.Add(linkLabelTrescPomoc);
		flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
		flowLayoutPanel1.Location = new Point(6, 248);
		flowLayoutPanel1.Margin = new Padding(0);
		flowLayoutPanel1.Name = "flowLayoutPanel1";
		flowLayoutPanel1.Size = new Size(106, 30);
		flowLayoutPanel1.TabIndex = 7;
		flowLayoutPanel1.WrapContents = false;
		// 
		// label9
		// 
		label9.Anchor = AnchorStyles.Right;
		label9.AutoSize = true;
		label9.Location = new Point(3, 0);
		label9.Name = "label9";
		label9.Size = new Size(100, 15);
		label9.TabIndex = 2;
		label9.Text = "Treść wiadomości";
		// 
		// linkLabelTrescPomoc
		// 
		linkLabelTrescPomoc.AutoSize = true;
		linkLabelTrescPomoc.Location = new Point(3, 15);
		linkLabelTrescPomoc.Name = "linkLabelTrescPomoc";
		linkLabelTrescPomoc.Size = new Size(20, 15);
		linkLabelTrescPomoc.TabIndex = 3;
		linkLabelTrescPomoc.TabStop = true;
		linkLabelTrescPomoc.Text = "[?]";
		linkLabelTrescPomoc.LinkClicked += linkLabelTrescPomoc_LinkClicked;
		// 
		// tabPageWyglad
		// 
		tabPageWyglad.Controls.Add(tableLayoutPanel3);
		tabPageWyglad.Location = new Point(4, 24);
		tabPageWyglad.Name = "tabPageWyglad";
		tabPageWyglad.Padding = new Padding(3);
		tabPageWyglad.Size = new Size(786, 469);
		tabPageWyglad.TabIndex = 1;
		tabPageWyglad.Text = "Wygląd";
		tabPageWyglad.UseVisualStyleBackColor = true;
		// 
		// tableLayoutPanel3
		// 
		tableLayoutPanel3.ColumnCount = 5;
		tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
		tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel3.Controls.Add(checkBoxSkrotyKlawiaturoweAkcji, 0, 0);
		tableLayoutPanel3.Controls.Add(checkBoxSkrotyKlawiaturoweZakladek, 0, 1);
		tableLayoutPanel3.Controls.Add(checkBoxSkrotyKlawiaturowePrzyciskow, 0, 2);
		tableLayoutPanel3.Controls.Add(checkBoxIkonyAkcji, 0, 3);
		tableLayoutPanel3.Controls.Add(checkBoxDomyslnyPodgladStrony, 0, 4);
		tableLayoutPanel3.Controls.Add(checkBoxPotwierdzanieZamknieciaEdytora, 0, 5);
		tableLayoutPanel3.Controls.Add(checkBoxPotwierdzanieZamknieciaProgramu, 0, 6);
		tableLayoutPanel3.Controls.Add(checkBoxWstepneLadowanieReportingServices, 0, 7);
		tableLayoutPanel3.Controls.Add(numericUpDownSzerokoscMenu, 1, 10);
		tableLayoutPanel3.Controls.Add(textBoxNazwaCzcionki, 1, 11);
		tableLayoutPanel3.Controls.Add(label1, 0, 10);
		tableLayoutPanel3.Controls.Add(label6, 0, 11);
		tableLayoutPanel3.Controls.Add(buttonWybierzCzcionke, 4, 11);
		tableLayoutPanel3.Controls.Add(checkBoxPrzywrocUstawieniaSpisow, 0, 8);
		tableLayoutPanel3.Controls.Add(checkBoxPrzywrocUstawieniaMenu, 0, 9);
		tableLayoutPanel3.Controls.Add(label11, 0, 12);
		tableLayoutPanel3.Controls.Add(numericUpDownWysokoscWierszy, 1, 12);
		tableLayoutPanel3.Controls.Add(label10, 4, 16);
		tableLayoutPanel3.Controls.Add(label12, 0, 13);
		tableLayoutPanel3.Controls.Add(label13, 0, 14);
		tableLayoutPanel3.Controls.Add(label14, 0, 15);
		tableLayoutPanel3.Controls.Add(comboBoxFormatDaty, 1, 13);
		tableLayoutPanel3.Controls.Add(comboBoxFormatCzasu, 1, 14);
		tableLayoutPanel3.Controls.Add(comboBoxFormatKwoty, 1, 15);
		tableLayoutPanel3.Controls.Add(textBoxPrzykladDaty, 3, 13);
		tableLayoutPanel3.Controls.Add(textBoxPrzykladCzasu, 3, 14);
		tableLayoutPanel3.Controls.Add(textBoxPrzykladKwoty, 3, 15);
		tableLayoutPanel3.Controls.Add(linkLabelFormatDaty, 2, 13);
		tableLayoutPanel3.Controls.Add(linkLabelFormatCzasu, 2, 14);
		tableLayoutPanel3.Controls.Add(linkLabelFormatKwoty, 2, 15);
		tableLayoutPanel3.Controls.Add(textBoxRozmiarCzcionki, 3, 11);
		tableLayoutPanel3.Dock = DockStyle.Fill;
		tableLayoutPanel3.Location = new Point(3, 3);
		tableLayoutPanel3.Name = "tableLayoutPanel3";
		tableLayoutPanel3.RowCount = 17;
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel3.RowStyles.Add(new RowStyle());
		tableLayoutPanel3.Size = new Size(780, 463);
		tableLayoutPanel3.TabIndex = 0;
		// 
		// checkBoxSkrotyKlawiaturoweAkcji
		// 
		checkBoxSkrotyKlawiaturoweAkcji.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		checkBoxSkrotyKlawiaturoweAkcji.AutoSize = true;
		tableLayoutPanel3.SetColumnSpan(checkBoxSkrotyKlawiaturoweAkcji, 4);
		checkBoxSkrotyKlawiaturoweAkcji.Location = new Point(3, 3);
		checkBoxSkrotyKlawiaturoweAkcji.Name = "checkBoxSkrotyKlawiaturoweAkcji";
		checkBoxSkrotyKlawiaturoweAkcji.Size = new Size(461, 19);
		checkBoxSkrotyKlawiaturoweAkcji.TabIndex = 0;
		checkBoxSkrotyKlawiaturoweAkcji.Text = "Pokaż skróty klawiaturowe akcji na spisie";
		checkBoxSkrotyKlawiaturoweAkcji.UseVisualStyleBackColor = true;
		// 
		// checkBoxSkrotyKlawiaturoweZakladek
		// 
		checkBoxSkrotyKlawiaturoweZakladek.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		checkBoxSkrotyKlawiaturoweZakladek.AutoSize = true;
		tableLayoutPanel3.SetColumnSpan(checkBoxSkrotyKlawiaturoweZakladek, 5);
		checkBoxSkrotyKlawiaturoweZakladek.Location = new Point(3, 28);
		checkBoxSkrotyKlawiaturoweZakladek.Name = "checkBoxSkrotyKlawiaturoweZakladek";
		checkBoxSkrotyKlawiaturoweZakladek.Size = new Size(774, 19);
		checkBoxSkrotyKlawiaturoweZakladek.TabIndex = 1;
		checkBoxSkrotyKlawiaturoweZakladek.Text = "Pokaż skróty klawiaturowe do przełączania zakładek";
		checkBoxSkrotyKlawiaturoweZakladek.UseVisualStyleBackColor = true;
		// 
		// checkBoxSkrotyKlawiaturowePrzyciskow
		// 
		checkBoxSkrotyKlawiaturowePrzyciskow.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		checkBoxSkrotyKlawiaturowePrzyciskow.AutoSize = true;
		tableLayoutPanel3.SetColumnSpan(checkBoxSkrotyKlawiaturowePrzyciskow, 5);
		checkBoxSkrotyKlawiaturowePrzyciskow.Location = new Point(3, 53);
		checkBoxSkrotyKlawiaturowePrzyciskow.Name = "checkBoxSkrotyKlawiaturowePrzyciskow";
		checkBoxSkrotyKlawiaturowePrzyciskow.Size = new Size(774, 19);
		checkBoxSkrotyKlawiaturowePrzyciskow.TabIndex = 2;
		checkBoxSkrotyKlawiaturowePrzyciskow.Text = "Pokaż skróty klawiaturowe przycisków";
		checkBoxSkrotyKlawiaturowePrzyciskow.UseVisualStyleBackColor = true;
		// 
		// checkBoxIkonyAkcji
		// 
		checkBoxIkonyAkcji.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		checkBoxIkonyAkcji.AutoSize = true;
		tableLayoutPanel3.SetColumnSpan(checkBoxIkonyAkcji, 5);
		checkBoxIkonyAkcji.Location = new Point(3, 78);
		checkBoxIkonyAkcji.Name = "checkBoxIkonyAkcji";
		checkBoxIkonyAkcji.Size = new Size(774, 19);
		checkBoxIkonyAkcji.TabIndex = 3;
		checkBoxIkonyAkcji.Text = "Pokaż piktogramy akcji na spisie";
		checkBoxIkonyAkcji.UseVisualStyleBackColor = true;
		// 
		// checkBoxDomyslnyPodgladStrony
		// 
		checkBoxDomyslnyPodgladStrony.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		checkBoxDomyslnyPodgladStrony.AutoSize = true;
		tableLayoutPanel3.SetColumnSpan(checkBoxDomyslnyPodgladStrony, 5);
		checkBoxDomyslnyPodgladStrony.Location = new Point(3, 103);
		checkBoxDomyslnyPodgladStrony.Name = "checkBoxDomyslnyPodgladStrony";
		checkBoxDomyslnyPodgladStrony.Size = new Size(774, 19);
		checkBoxDomyslnyPodgladStrony.TabIndex = 4;
		checkBoxDomyslnyPodgladStrony.Text = "Wyświetlaj domyślnie widok strony jako podgląd wydruku";
		checkBoxDomyslnyPodgladStrony.UseVisualStyleBackColor = true;
		// 
		// checkBoxPotwierdzanieZamknieciaEdytora
		// 
		checkBoxPotwierdzanieZamknieciaEdytora.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		checkBoxPotwierdzanieZamknieciaEdytora.AutoSize = true;
		tableLayoutPanel3.SetColumnSpan(checkBoxPotwierdzanieZamknieciaEdytora, 5);
		checkBoxPotwierdzanieZamknieciaEdytora.Location = new Point(3, 128);
		checkBoxPotwierdzanieZamknieciaEdytora.Name = "checkBoxPotwierdzanieZamknieciaEdytora";
		checkBoxPotwierdzanieZamknieciaEdytora.Size = new Size(774, 19);
		checkBoxPotwierdzanieZamknieciaEdytora.TabIndex = 5;
		checkBoxPotwierdzanieZamknieciaEdytora.Text = "Pytaj o potwierdzenie porzucenia zmian";
		checkBoxPotwierdzanieZamknieciaEdytora.UseVisualStyleBackColor = true;
		// 
		// checkBoxPotwierdzanieZamknieciaProgramu
		// 
		checkBoxPotwierdzanieZamknieciaProgramu.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		checkBoxPotwierdzanieZamknieciaProgramu.AutoSize = true;
		tableLayoutPanel3.SetColumnSpan(checkBoxPotwierdzanieZamknieciaProgramu, 5);
		checkBoxPotwierdzanieZamknieciaProgramu.Location = new Point(3, 153);
		checkBoxPotwierdzanieZamknieciaProgramu.Name = "checkBoxPotwierdzanieZamknieciaProgramu";
		checkBoxPotwierdzanieZamknieciaProgramu.Size = new Size(774, 19);
		checkBoxPotwierdzanieZamknieciaProgramu.TabIndex = 6;
		checkBoxPotwierdzanieZamknieciaProgramu.Text = "Pytaj o potwierdzenie zamknięcia programu";
		checkBoxPotwierdzanieZamknieciaProgramu.UseVisualStyleBackColor = true;
		// 
		// checkBoxWstepneLadowanieReportingServices
		// 
		checkBoxWstepneLadowanieReportingServices.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		checkBoxWstepneLadowanieReportingServices.AutoSize = true;
		tableLayoutPanel3.SetColumnSpan(checkBoxWstepneLadowanieReportingServices, 5);
		checkBoxWstepneLadowanieReportingServices.Location = new Point(3, 178);
		checkBoxWstepneLadowanieReportingServices.Name = "checkBoxWstepneLadowanieReportingServices";
		checkBoxWstepneLadowanieReportingServices.Size = new Size(774, 19);
		checkBoxWstepneLadowanieReportingServices.TabIndex = 7;
		checkBoxWstepneLadowanieReportingServices.Text = "Załaduj w tle moduł wydruków przy starcie programu";
		checkBoxWstepneLadowanieReportingServices.UseVisualStyleBackColor = true;
		// 
		// numericUpDownSzerokoscMenu
		// 
		tableLayoutPanel3.SetColumnSpan(numericUpDownSzerokoscMenu, 4);
		numericUpDownSzerokoscMenu.Location = new Point(158, 253);
		numericUpDownSzerokoscMenu.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
		numericUpDownSzerokoscMenu.Name = "numericUpDownSzerokoscMenu";
		numericUpDownSzerokoscMenu.Size = new Size(64, 23);
		numericUpDownSzerokoscMenu.TabIndex = 10;
		numericUpDownSzerokoscMenu.TextAlign = HorizontalAlignment.Right;
		// 
		// textBoxNazwaCzcionki
		// 
		textBoxNazwaCzcionki.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel3.SetColumnSpan(textBoxNazwaCzcionki, 2);
		textBoxNazwaCzcionki.Location = new Point(158, 282);
		textBoxNazwaCzcionki.Name = "textBoxNazwaCzcionki";
		textBoxNazwaCzcionki.Size = new Size(250, 23);
		textBoxNazwaCzcionki.TabIndex = 11;
		// 
		// label1
		// 
		label1.Anchor = AnchorStyles.Right;
		label1.AutoSize = true;
		label1.Location = new Point(3, 257);
		label1.Name = "label1";
		label1.Size = new Size(149, 15);
		label1.TabIndex = 3;
		label1.Text = "Szerokość menu głównego";
		// 
		// label6
		// 
		label6.Anchor = AnchorStyles.Right;
		label6.AutoSize = true;
		label6.Location = new Point(97, 286);
		label6.Name = "label6";
		label6.Size = new Size(55, 15);
		label6.TabIndex = 3;
		label6.Text = "Czcionka";
		// 
		// buttonWybierzCzcionke
		// 
		buttonWybierzCzcionke.Anchor = AnchorStyles.Left;
		buttonWybierzCzcionke.Location = new Point(470, 282);
		buttonWybierzCzcionke.Name = "buttonWybierzCzcionke";
		buttonWybierzCzcionke.Size = new Size(23, 23);
		buttonWybierzCzcionke.TabIndex = 13;
		buttonWybierzCzcionke.Text = "...";
		buttonWybierzCzcionke.UseVisualStyleBackColor = true;
		buttonWybierzCzcionke.Click += buttonWybierzCzcionke_Click;
		// 
		// checkBoxPrzywrocUstawieniaSpisow
		// 
		checkBoxPrzywrocUstawieniaSpisow.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		checkBoxPrzywrocUstawieniaSpisow.AutoSize = true;
		tableLayoutPanel3.SetColumnSpan(checkBoxPrzywrocUstawieniaSpisow, 5);
		checkBoxPrzywrocUstawieniaSpisow.Location = new Point(3, 203);
		checkBoxPrzywrocUstawieniaSpisow.Name = "checkBoxPrzywrocUstawieniaSpisow";
		checkBoxPrzywrocUstawieniaSpisow.Size = new Size(774, 19);
		checkBoxPrzywrocUstawieniaSpisow.TabIndex = 8;
		checkBoxPrzywrocUstawieniaSpisow.Text = "Przywróć domyślne ustawienia spisów";
		checkBoxPrzywrocUstawieniaSpisow.UseVisualStyleBackColor = true;
		// 
		// checkBoxPrzywrocUstawieniaMenu
		// 
		checkBoxPrzywrocUstawieniaMenu.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		checkBoxPrzywrocUstawieniaMenu.AutoSize = true;
		tableLayoutPanel3.SetColumnSpan(checkBoxPrzywrocUstawieniaMenu, 5);
		checkBoxPrzywrocUstawieniaMenu.Location = new Point(3, 228);
		checkBoxPrzywrocUstawieniaMenu.Name = "checkBoxPrzywrocUstawieniaMenu";
		checkBoxPrzywrocUstawieniaMenu.Size = new Size(774, 19);
		checkBoxPrzywrocUstawieniaMenu.TabIndex = 9;
		checkBoxPrzywrocUstawieniaMenu.Text = "Przywróć domyślne ustawienia menu";
		checkBoxPrzywrocUstawieniaMenu.UseVisualStyleBackColor = true;
		// 
		// label11
		// 
		label11.Anchor = AnchorStyles.Right;
		label11.AutoSize = true;
		label11.Location = new Point(21, 315);
		label11.Name = "label11";
		label11.Size = new Size(131, 15);
		label11.TabIndex = 3;
		label11.Text = "Wysokość wierszy spisu";
		// 
		// numericUpDownWysokoscWierszy
		// 
		numericUpDownWysokoscWierszy.Location = new Point(158, 311);
		numericUpDownWysokoscWierszy.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
		numericUpDownWysokoscWierszy.Name = "numericUpDownWysokoscWierszy";
		numericUpDownWysokoscWierszy.Size = new Size(64, 23);
		numericUpDownWysokoscWierszy.TabIndex = 14;
		numericUpDownWysokoscWierszy.TextAlign = HorizontalAlignment.Right;
		// 
		// label10
		// 
		label10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		label10.AutoSize = true;
		tableLayoutPanel3.SetColumnSpan(label10, 5);
		label10.Location = new Point(3, 448);
		label10.Name = "label10";
		label10.Size = new Size(774, 15);
		label10.TabIndex = 4;
		label10.Text = "Zmiana czcionki i szerokości menu będzie obowiązywać od kolejnego uruchomienia programu.";
		// 
		// label12
		// 
		label12.Anchor = AnchorStyles.Right;
		label12.AutoSize = true;
		label12.Location = new Point(81, 344);
		label12.Name = "label12";
		label12.Size = new Size(71, 15);
		label12.TabIndex = 3;
		label12.Text = "Format daty";
		// 
		// label13
		// 
		label13.Anchor = AnchorStyles.Right;
		label13.AutoSize = true;
		label13.Location = new Point(75, 373);
		label13.Name = "label13";
		label13.Size = new Size(77, 15);
		label13.TabIndex = 3;
		label13.Text = "Format czasu";
		// 
		// label14
		// 
		label14.Anchor = AnchorStyles.Right;
		label14.AutoSize = true;
		label14.Location = new Point(78, 402);
		label14.Name = "label14";
		label14.Size = new Size(74, 15);
		label14.TabIndex = 3;
		label14.Text = "Format kwot";
		// 
		// comboBoxFormatDaty
		// 
		comboBoxFormatDaty.FormattingEnabled = true;
		comboBoxFormatDaty.Items.AddRange(new object[] { "yyyy-MM-dd", "dd.MM.yyyy", "d MMMM yyyy" });
		comboBoxFormatDaty.Location = new Point(158, 340);
		comboBoxFormatDaty.Name = "comboBoxFormatDaty";
		comboBoxFormatDaty.Size = new Size(230, 23);
		comboBoxFormatDaty.TabIndex = 15;
		// 
		// comboBoxFormatCzasu
		// 
		comboBoxFormatCzasu.FormattingEnabled = true;
		comboBoxFormatCzasu.Items.AddRange(new object[] { "yyyy-MM-dd HH:mm:ss", "dd.MM.yyyy HH:mm:ss", "d MMMM yyyy H:mm:ss" });
		comboBoxFormatCzasu.Location = new Point(158, 369);
		comboBoxFormatCzasu.Name = "comboBoxFormatCzasu";
		comboBoxFormatCzasu.Size = new Size(230, 23);
		comboBoxFormatCzasu.TabIndex = 15;
		// 
		// comboBoxFormatKwoty
		// 
		comboBoxFormatKwoty.FormattingEnabled = true;
		comboBoxFormatKwoty.Items.AddRange(new object[] { "#,##0.00", "0.00", "0.##" });
		comboBoxFormatKwoty.Location = new Point(158, 398);
		comboBoxFormatKwoty.Name = "comboBoxFormatKwoty";
		comboBoxFormatKwoty.Size = new Size(230, 23);
		comboBoxFormatKwoty.TabIndex = 15;
		// 
		// textBoxPrzykladDaty
		// 
		tableLayoutPanel3.SetColumnSpan(textBoxPrzykladDaty, 2);
		textBoxPrzykladDaty.Location = new Point(414, 340);
		textBoxPrzykladDaty.Name = "textBoxPrzykladDaty";
		textBoxPrzykladDaty.ReadOnly = true;
		textBoxPrzykladDaty.Size = new Size(198, 23);
		textBoxPrzykladDaty.TabIndex = 16;
		// 
		// textBoxPrzykladCzasu
		// 
		tableLayoutPanel3.SetColumnSpan(textBoxPrzykladCzasu, 2);
		textBoxPrzykladCzasu.Location = new Point(414, 369);
		textBoxPrzykladCzasu.Name = "textBoxPrzykladCzasu";
		textBoxPrzykladCzasu.ReadOnly = true;
		textBoxPrzykladCzasu.Size = new Size(198, 23);
		textBoxPrzykladCzasu.TabIndex = 16;
		// 
		// textBoxPrzykladKwoty
		// 
		tableLayoutPanel3.SetColumnSpan(textBoxPrzykladKwoty, 2);
		textBoxPrzykladKwoty.Location = new Point(414, 398);
		textBoxPrzykladKwoty.Name = "textBoxPrzykladKwoty";
		textBoxPrzykladKwoty.ReadOnly = true;
		textBoxPrzykladKwoty.Size = new Size(198, 23);
		textBoxPrzykladKwoty.TabIndex = 16;
		// 
		// linkLabelFormatDaty
		// 
		linkLabelFormatDaty.Anchor = AnchorStyles.Left;
		linkLabelFormatDaty.AutoSize = true;
		linkLabelFormatDaty.Location = new Point(394, 344);
		linkLabelFormatDaty.Name = "linkLabelFormatDaty";
		linkLabelFormatDaty.Size = new Size(12, 15);
		linkLabelFormatDaty.TabIndex = 17;
		linkLabelFormatDaty.TabStop = true;
		linkLabelFormatDaty.Text = "?";
		linkLabelFormatDaty.LinkClicked += linkLabelFormatDaty_LinkClicked;
		// 
		// linkLabelFormatCzasu
		// 
		linkLabelFormatCzasu.Anchor = AnchorStyles.Left;
		linkLabelFormatCzasu.AutoSize = true;
		linkLabelFormatCzasu.Location = new Point(394, 373);
		linkLabelFormatCzasu.Name = "linkLabelFormatCzasu";
		linkLabelFormatCzasu.Size = new Size(12, 15);
		linkLabelFormatCzasu.TabIndex = 17;
		linkLabelFormatCzasu.TabStop = true;
		linkLabelFormatCzasu.Text = "?";
		linkLabelFormatCzasu.LinkClicked += linkLabelFormatCzasu_LinkClicked;
		// 
		// linkLabelFormatKwoty
		// 
		linkLabelFormatKwoty.Anchor = AnchorStyles.Left;
		linkLabelFormatKwoty.AutoSize = true;
		linkLabelFormatKwoty.Location = new Point(394, 402);
		linkLabelFormatKwoty.Name = "linkLabelFormatKwoty";
		linkLabelFormatKwoty.Size = new Size(12, 15);
		linkLabelFormatKwoty.TabIndex = 17;
		linkLabelFormatKwoty.TabStop = true;
		linkLabelFormatKwoty.Text = "?";
		linkLabelFormatKwoty.LinkClicked += linkLabelFormatKwoty_LinkClicked;
		// 
		// textBoxRozmiarCzcionki
		// 
		textBoxRozmiarCzcionki.Location = new Point(414, 282);
		textBoxRozmiarCzcionki.Name = "textBoxRozmiarCzcionki";
		textBoxRozmiarCzcionki.Size = new Size(50, 23);
		textBoxRozmiarCzcionki.TabIndex = 12;
		// 
		// tableLayoutPanel2
		// 
		tableLayoutPanel2.ColumnCount = 2;
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.Controls.Add(tabControl, 0, 0);
		tableLayoutPanel2.Dock = DockStyle.Fill;
		tableLayoutPanel2.Location = new Point(0, 0);
		tableLayoutPanel2.Name = "tableLayoutPanel2";
		tableLayoutPanel2.RowCount = 1;
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
		tableLayoutPanel2.Size = new Size(800, 503);
		tableLayoutPanel2.TabIndex = 3;
		// 
		// KonfiguracjaEdytor
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		Controls.Add(tableLayoutPanel2);
		MinimumSize = new Size(800, 425);
		Name = "KonfiguracjaEdytor";
		Size = new Size(800, 503);
		((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
		tabControl.ResumeLayout(false);
		tabPageEMail.ResumeLayout(false);
		tableLayoutPanel1.ResumeLayout(false);
		tableLayoutPanel1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDownSMTPort).EndInit();
		flowLayoutPanel1.ResumeLayout(false);
		flowLayoutPanel1.PerformLayout();
		tabPageWyglad.ResumeLayout(false);
		tableLayoutPanel3.ResumeLayout(false);
		tableLayoutPanel3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)numericUpDownSzerokoscMenu).EndInit();
		((System.ComponentModel.ISupportInitialize)numericUpDownWysokoscWierszy).EndInit();
		tableLayoutPanel2.ResumeLayout(false);
		ResumeLayout(false);
	}

	#endregion
	private System.Windows.Forms.TabControl tabControl;
	private System.Windows.Forms.TabPage tabPageEMail;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.TextBox textBoxSMTPSerwer;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.TextBox textBoxSMTPLogin;
	private System.Windows.Forms.TextBox textBoxEMailTresc;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.Label label5;
	private System.Windows.Forms.Label label8;
	private System.Windows.Forms.Label label9;
	private System.Windows.Forms.Label label7;
	private NumericUpDownDPI numericUpDownSMTPort;
	private System.Windows.Forms.TextBox textBoxEMailNadawca;
	private System.Windows.Forms.TextBox textBoxSMTPHaslo;
	private System.Windows.Forms.TextBox textBoxEMailTemat;
	private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	private System.Windows.Forms.LinkLabel linkLabelTrescPomoc;
	private TabPage tabPageWyglad;
	private TableLayoutPanel tableLayoutPanel3;
	private CheckBox checkBoxSkrotyKlawiaturoweAkcji;
	private CheckBox checkBoxSkrotyKlawiaturoweZakladek;
	private CheckBox checkBoxSkrotyKlawiaturowePrzyciskow;
	private CheckBox checkBoxIkonyAkcji;
	private CheckBox checkBoxDomyslnyPodgladStrony;
	private CheckBox checkBoxPotwierdzanieZamknieciaEdytora;
	private CheckBox checkBoxPotwierdzanieZamknieciaProgramu;
	private CheckBox checkBoxWstepneLadowanieReportingServices;
	private ButtonDPI buttonWybierzCzcionke;
	private NumericUpDownDPI numericUpDownSzerokoscMenu;
	private TextBox textBoxNazwaCzcionki;
	private Label label1;
	private Label label6;
	private TextBox textBoxRozmiarCzcionki;
	private Label label10;
	private CheckBox checkBoxPrzywrocUstawieniaSpisow;
	private CheckBox checkBoxPrzywrocUstawieniaMenu;
	private Label label11;
	private NumericUpDownDPI numericUpDownWysokoscWierszy;
	private Label label12;
	private Label label13;
	private Label label14;
	private ComboBox comboBoxFormatDaty;
	private ComboBox comboBoxFormatCzasu;
	private ComboBox comboBoxFormatKwoty;
	private TextBox textBoxPrzykladCzasu;
	private TextBox textBoxPrzykladKwoty;
	private TextBox textBoxPrzykladDaty;
	private LinkLabel linkLabelFormatDaty;
	private LinkLabel linkLabelFormatCzasu;
	private LinkLabel linkLabelFormatKwoty;
}
