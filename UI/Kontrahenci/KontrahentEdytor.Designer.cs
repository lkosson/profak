﻿
namespace ProFak.UI
{
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
			tabControl = new System.Windows.Forms.TabControl();
			tabPage1 = new System.Windows.Forms.TabPage();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			label2 = new System.Windows.Forms.Label();
			textBoxPelnaNazwa = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			textBoxNIP = new System.Windows.Forms.TextBox();
			textBoxAdresRejestrowy = new System.Windows.Forms.TextBox();
			textBoxAdresKorespondencyjny = new System.Windows.Forms.TextBox();
			textBoxTelefon = new System.Windows.Forms.TextBox();
			textBoxEMail = new System.Windows.Forms.TextBox();
			textBoxRachunekBankowy = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			comboBoxStan = new System.Windows.Forms.ComboBox();
			label9 = new System.Windows.Forms.Label();
			checkBoxTP = new System.Windows.Forms.CheckBox();
			buttonSprawdzMF = new ButtonDPI();
			buttonPobierzGUS = new ButtonDPI();
			tabPage2 = new System.Windows.Forms.TabPage();
			textBoxUwagi = new System.Windows.Forms.TextBox();
			tabPageFakturySprzedazy = new System.Windows.Forms.TabPage();
			tabPageFakturyZakupu = new System.Windows.Forms.TabPage();
			tabPagePodatki = new System.Windows.Forms.TabPage();
			tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			label10 = new System.Windows.Forms.Label();
			label11 = new System.Windows.Forms.Label();
			label12 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			comboBoxKodUrzedu = new System.Windows.Forms.ComboBox();
			textBoxOsobaFizycznaImie = new System.Windows.Forms.TextBox();
			textBoxOsobaFizycznaNazwisko = new System.Windows.Forms.TextBox();
			dateTimePickerOsobaFizycznaDataUrodzenia = new System.Windows.Forms.DateTimePickerFix();
			buttonUrzadSkarbowy = new ButtonDPI();
			label14 = new System.Windows.Forms.Label();
			comboBoxFormaOpodatkowania = new System.Windows.Forms.ComboBox();
			label15 = new System.Windows.Forms.Label();
			textBoxTokenKSeF = new System.Windows.Forms.TextBox();
			comboBoxSrodowiskoKSeF = new System.Windows.Forms.ComboBox();
			textBoxNazwa = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			backgroundWorkerSprawdzMF = new System.ComponentModel.BackgroundWorker();
			backgroundWorkerPobierzGUS = new System.ComponentModel.BackgroundWorker();
			buttonKSeFAuth = new ButtonDPI();
			((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
			tabControl.SuspendLayout();
			tabPage1.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			tabPage2.SuspendLayout();
			tabPagePodatki.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
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
			tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			tabControl.Location = new System.Drawing.Point(3, 32);
			tabControl.Name = "tabControl";
			tabControl.SelectedIndex = 0;
			tabControl.Size = new System.Drawing.Size(794, 390);
			tabControl.TabIndex = 2;
			// 
			// tabPage1
			// 
			tabPage1.Controls.Add(tableLayoutPanel1);
			tabPage1.Location = new System.Drawing.Point(4, 24);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new System.Windows.Forms.Padding(3);
			tabPage1.Size = new System.Drawing.Size(786, 362);
			tabPage1.TabIndex = 0;
			tabPage1.Text = "Dane podstawowe";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 3;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
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
			tableLayoutPanel1.Controls.Add(checkBoxTP, 1, 9);
			tableLayoutPanel1.Controls.Add(buttonSprawdzMF, 2, 6);
			tableLayoutPanel1.Controls.Add(buttonPobierzGUS, 2, 1);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 11;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new System.Drawing.Size(780, 356);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// label2
			// 
			label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(68, 7);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(72, 15);
			label2.TabIndex = 2;
			label2.Text = "Pełna nazwa";
			// 
			// textBoxPelnaNazwa
			// 
			textBoxPelnaNazwa.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel1.SetColumnSpan(textBoxPelnaNazwa, 2);
			textBoxPelnaNazwa.Location = new System.Drawing.Point(146, 3);
			textBoxPelnaNazwa.Name = "textBoxPelnaNazwa";
			textBoxPelnaNazwa.Size = new System.Drawing.Size(631, 23);
			textBoxPelnaNazwa.TabIndex = 1;
			// 
			// label3
			// 
			label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(114, 37);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(26, 15);
			label3.TabIndex = 2;
			label3.Text = "NIP";
			// 
			// textBoxNIP
			// 
			textBoxNIP.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxNIP.Location = new System.Drawing.Point(146, 33);
			textBoxNIP.Name = "textBoxNIP";
			textBoxNIP.Size = new System.Drawing.Size(465, 23);
			textBoxNIP.TabIndex = 2;
			// 
			// textBoxAdresRejestrowy
			// 
			textBoxAdresRejestrowy.AcceptsReturn = true;
			textBoxAdresRejestrowy.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel1.SetColumnSpan(textBoxAdresRejestrowy, 2);
			textBoxAdresRejestrowy.Location = new System.Drawing.Point(146, 63);
			textBoxAdresRejestrowy.Multiline = true;
			textBoxAdresRejestrowy.Name = "textBoxAdresRejestrowy";
			textBoxAdresRejestrowy.Size = new System.Drawing.Size(631, 65);
			textBoxAdresRejestrowy.TabIndex = 3;
			textBoxAdresRejestrowy.TextChanged += textBoxAdresRejestrowy_TextChanged;
			// 
			// textBoxAdresKorespondencyjny
			// 
			textBoxAdresKorespondencyjny.AcceptsReturn = true;
			textBoxAdresKorespondencyjny.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel1.SetColumnSpan(textBoxAdresKorespondencyjny, 2);
			textBoxAdresKorespondencyjny.Location = new System.Drawing.Point(146, 134);
			textBoxAdresKorespondencyjny.Multiline = true;
			textBoxAdresKorespondencyjny.Name = "textBoxAdresKorespondencyjny";
			textBoxAdresKorespondencyjny.Size = new System.Drawing.Size(631, 65);
			textBoxAdresKorespondencyjny.TabIndex = 4;
			// 
			// textBoxTelefon
			// 
			textBoxTelefon.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel1.SetColumnSpan(textBoxTelefon, 2);
			textBoxTelefon.Location = new System.Drawing.Point(146, 205);
			textBoxTelefon.Name = "textBoxTelefon";
			textBoxTelefon.Size = new System.Drawing.Size(631, 23);
			textBoxTelefon.TabIndex = 5;
			// 
			// textBoxEMail
			// 
			textBoxEMail.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel1.SetColumnSpan(textBoxEMail, 2);
			textBoxEMail.Location = new System.Drawing.Point(146, 234);
			textBoxEMail.Name = "textBoxEMail";
			textBoxEMail.Size = new System.Drawing.Size(631, 23);
			textBoxEMail.TabIndex = 6;
			// 
			// textBoxRachunekBankowy
			// 
			textBoxRachunekBankowy.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxRachunekBankowy.Location = new System.Drawing.Point(146, 264);
			textBoxRachunekBankowy.Name = "textBoxRachunekBankowy";
			textBoxRachunekBankowy.Size = new System.Drawing.Size(465, 23);
			textBoxRachunekBankowy.TabIndex = 7;
			// 
			// label4
			// 
			label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(46, 88);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(94, 15);
			label4.TabIndex = 2;
			label4.Text = "Adres rejestrowy";
			// 
			// label5
			// 
			label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(3, 159);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(137, 15);
			label5.TabIndex = 2;
			label5.Text = "Adres korespondencyjny";
			// 
			// label6
			// 
			label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(95, 209);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(45, 15);
			label6.TabIndex = 2;
			label6.Text = "Telefon";
			// 
			// label7
			// 
			label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(99, 238);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(41, 15);
			label7.TabIndex = 2;
			label7.Text = "E-Mail";
			// 
			// label8
			// 
			label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(30, 268);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(110, 15);
			label8.TabIndex = 2;
			label8.Text = "Rachunek bankowy";
			// 
			// comboBoxStan
			// 
			comboBoxStan.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel1.SetColumnSpan(comboBoxStan, 2);
			comboBoxStan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxStan.FormattingEnabled = true;
			comboBoxStan.Location = new System.Drawing.Point(146, 294);
			comboBoxStan.Name = "comboBoxStan";
			comboBoxStan.Size = new System.Drawing.Size(631, 23);
			comboBoxStan.TabIndex = 8;
			// 
			// label9
			// 
			label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(110, 298);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(30, 15);
			label9.TabIndex = 2;
			label9.Text = "Stan";
			// 
			// checkBoxTP
			// 
			checkBoxTP.AutoSize = true;
			checkBoxTP.Location = new System.Drawing.Point(146, 323);
			checkBoxTP.Name = "checkBoxTP";
			checkBoxTP.Size = new System.Drawing.Size(131, 19);
			checkBoxTP.TabIndex = 9;
			checkBoxTP.Text = "Podmiot powiązany";
			checkBoxTP.UseVisualStyleBackColor = true;
			// 
			// buttonSprawdzMF
			// 
			buttonSprawdzMF.AutoSize = true;
			buttonSprawdzMF.Location = new System.Drawing.Point(617, 263);
			buttonSprawdzMF.Name = "buttonSprawdzMF";
			buttonSprawdzMF.Size = new System.Drawing.Size(160, 25);
			buttonSprawdzMF.TabIndex = 10;
			buttonSprawdzMF.Text = "Sprawdź na białej liście VAT";
			buttonSprawdzMF.UseVisualStyleBackColor = true;
			buttonSprawdzMF.Click += buttonSprawdzMF_Click;
			// 
			// buttonPobierzGUS
			// 
			buttonPobierzGUS.AutoSize = true;
			buttonPobierzGUS.Location = new System.Drawing.Point(617, 32);
			buttonPobierzGUS.Name = "buttonPobierzGUS";
			buttonPobierzGUS.Size = new System.Drawing.Size(118, 25);
			buttonPobierzGUS.TabIndex = 10;
			buttonPobierzGUS.Text = "Pobierz dane z GUS";
			buttonPobierzGUS.UseVisualStyleBackColor = true;
			buttonPobierzGUS.Click += buttonPobierzGUS_Click;
			// 
			// tabPage2
			// 
			tabPage2.Controls.Add(textBoxUwagi);
			tabPage2.Location = new System.Drawing.Point(4, 24);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new System.Windows.Forms.Padding(3);
			tabPage2.Size = new System.Drawing.Size(786, 362);
			tabPage2.TabIndex = 1;
			tabPage2.Text = "Uwagi";
			tabPage2.UseVisualStyleBackColor = true;
			// 
			// textBoxUwagi
			// 
			textBoxUwagi.Dock = System.Windows.Forms.DockStyle.Fill;
			textBoxUwagi.Location = new System.Drawing.Point(3, 3);
			textBoxUwagi.Multiline = true;
			textBoxUwagi.Name = "textBoxUwagi";
			textBoxUwagi.Size = new System.Drawing.Size(780, 356);
			textBoxUwagi.TabIndex = 0;
			// 
			// tabPageFakturySprzedazy
			// 
			tabPageFakturySprzedazy.Location = new System.Drawing.Point(4, 24);
			tabPageFakturySprzedazy.Name = "tabPageFakturySprzedazy";
			tabPageFakturySprzedazy.Padding = new System.Windows.Forms.Padding(3);
			tabPageFakturySprzedazy.Size = new System.Drawing.Size(786, 362);
			tabPageFakturySprzedazy.TabIndex = 2;
			tabPageFakturySprzedazy.Text = "Sprzedaż do";
			tabPageFakturySprzedazy.UseVisualStyleBackColor = true;
			// 
			// tabPageFakturyZakupu
			// 
			tabPageFakturyZakupu.Location = new System.Drawing.Point(4, 24);
			tabPageFakturyZakupu.Name = "tabPageFakturyZakupu";
			tabPageFakturyZakupu.Padding = new System.Windows.Forms.Padding(3);
			tabPageFakturyZakupu.Size = new System.Drawing.Size(786, 362);
			tabPageFakturyZakupu.TabIndex = 3;
			tabPageFakturyZakupu.Text = "Zakup od";
			tabPageFakturyZakupu.UseVisualStyleBackColor = true;
			// 
			// tabPagePodatki
			// 
			tabPagePodatki.Controls.Add(tableLayoutPanel3);
			tabPagePodatki.Location = new System.Drawing.Point(4, 24);
			tabPagePodatki.Name = "tabPagePodatki";
			tabPagePodatki.Padding = new System.Windows.Forms.Padding(3);
			tabPagePodatki.Size = new System.Drawing.Size(786, 362);
			tabPagePodatki.TabIndex = 4;
			tabPagePodatki.Text = "Dane urzędowe";
			tabPagePodatki.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel3
			// 
			tableLayoutPanel3.ColumnCount = 5;
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
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
			tableLayoutPanel3.Controls.Add(buttonKSeFAuth, 3, 5);
			tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 7;
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel3.Size = new System.Drawing.Size(780, 356);
			tableLayoutPanel3.TabIndex = 0;
			// 
			// label10
			// 
			label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(3, 8);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(134, 15);
			label10.TabIndex = 0;
			label10.Text = "Kod urzędu skarbowego";
			// 
			// label11
			// 
			label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(59, 38);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(78, 15);
			label11.TabIndex = 0;
			label11.Text = "Pierwsze imię";
			// 
			// label12
			// 
			label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(80, 67);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(57, 15);
			label12.TabIndex = 0;
			label12.Text = "Nazwisko";
			// 
			// label13
			// 
			label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(51, 96);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(86, 15);
			label13.TabIndex = 0;
			label13.Text = "Data urodzenia";
			// 
			// comboBoxKodUrzedu
			// 
			comboBoxKodUrzedu.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel3.SetColumnSpan(comboBoxKodUrzedu, 3);
			comboBoxKodUrzedu.FormattingEnabled = true;
			comboBoxKodUrzedu.Location = new System.Drawing.Point(143, 4);
			comboBoxKodUrzedu.Name = "comboBoxKodUrzedu";
			comboBoxKodUrzedu.Size = new System.Drawing.Size(602, 23);
			comboBoxKodUrzedu.TabIndex = 0;
			// 
			// textBoxOsobaFizycznaImie
			// 
			textBoxOsobaFizycznaImie.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel3.SetColumnSpan(textBoxOsobaFizycznaImie, 4);
			textBoxOsobaFizycznaImie.Location = new System.Drawing.Point(143, 34);
			textBoxOsobaFizycznaImie.Name = "textBoxOsobaFizycznaImie";
			textBoxOsobaFizycznaImie.Size = new System.Drawing.Size(634, 23);
			textBoxOsobaFizycznaImie.TabIndex = 2;
			// 
			// textBoxOsobaFizycznaNazwisko
			// 
			textBoxOsobaFizycznaNazwisko.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel3.SetColumnSpan(textBoxOsobaFizycznaNazwisko, 4);
			textBoxOsobaFizycznaNazwisko.Location = new System.Drawing.Point(143, 63);
			textBoxOsobaFizycznaNazwisko.Name = "textBoxOsobaFizycznaNazwisko";
			textBoxOsobaFizycznaNazwisko.Size = new System.Drawing.Size(634, 23);
			textBoxOsobaFizycznaNazwisko.TabIndex = 3;
			// 
			// dateTimePickerOsobaFizycznaDataUrodzenia
			// 
			tableLayoutPanel3.SetColumnSpan(dateTimePickerOsobaFizycznaDataUrodzenia, 2);
			dateTimePickerOsobaFizycznaDataUrodzenia.Location = new System.Drawing.Point(143, 92);
			dateTimePickerOsobaFizycznaDataUrodzenia.Name = "dateTimePickerOsobaFizycznaDataUrodzenia";
			dateTimePickerOsobaFizycznaDataUrodzenia.ShowCheckBox = true;
			dateTimePickerOsobaFizycznaDataUrodzenia.Size = new System.Drawing.Size(200, 23);
			dateTimePickerOsobaFizycznaDataUrodzenia.TabIndex = 4;
			// 
			// buttonUrzadSkarbowy
			// 
			buttonUrzadSkarbowy.Anchor = System.Windows.Forms.AnchorStyles.Left;
			buttonUrzadSkarbowy.AutoSize = true;
			buttonUrzadSkarbowy.Location = new System.Drawing.Point(751, 3);
			buttonUrzadSkarbowy.Name = "buttonUrzadSkarbowy";
			buttonUrzadSkarbowy.Size = new System.Drawing.Size(26, 25);
			buttonUrzadSkarbowy.TabIndex = 1;
			buttonUrzadSkarbowy.Text = "...";
			buttonUrzadSkarbowy.UseVisualStyleBackColor = true;
			// 
			// label14
			// 
			label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(11, 125);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(126, 15);
			label14.TabIndex = 0;
			label14.Text = "Forma opodatkowania";
			// 
			// comboBoxFormaOpodatkowania
			// 
			comboBoxFormaOpodatkowania.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel3.SetColumnSpan(comboBoxFormaOpodatkowania, 4);
			comboBoxFormaOpodatkowania.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxFormaOpodatkowania.FormattingEnabled = true;
			comboBoxFormaOpodatkowania.Location = new System.Drawing.Point(143, 121);
			comboBoxFormaOpodatkowania.Name = "comboBoxFormaOpodatkowania";
			comboBoxFormaOpodatkowania.Size = new System.Drawing.Size(634, 23);
			comboBoxFormaOpodatkowania.TabIndex = 5;
			// 
			// label15
			// 
			label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(71, 155);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(66, 15);
			label15.TabIndex = 0;
			label15.Text = "Token KSeF";
			// 
			// textBoxTokenKSeF
			// 
			textBoxTokenKSeF.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxTokenKSeF.Location = new System.Drawing.Point(220, 151);
			textBoxTokenKSeF.Name = "textBoxTokenKSeF";
			textBoxTokenKSeF.Size = new System.Drawing.Size(456, 23);
			textBoxTokenKSeF.TabIndex = 7;
			// 
			// comboBoxSrodowiskoKSeF
			// 
			comboBoxSrodowiskoKSeF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxSrodowiskoKSeF.FormattingEnabled = true;
			comboBoxSrodowiskoKSeF.Location = new System.Drawing.Point(143, 150);
			comboBoxSrodowiskoKSeF.Name = "comboBoxSrodowiskoKSeF";
			comboBoxSrodowiskoKSeF.Size = new System.Drawing.Size(71, 23);
			comboBoxSrodowiskoKSeF.TabIndex = 6;
			// 
			// textBoxNazwa
			// 
			textBoxNazwa.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxNazwa.Location = new System.Drawing.Point(51, 3);
			textBoxNazwa.Name = "textBoxNazwa";
			textBoxNazwa.Size = new System.Drawing.Size(746, 23);
			textBoxNazwa.TabIndex = 1;
			textBoxNazwa.TextChanged += textBoxNazwa_TextChanged;
			// 
			// label1
			// 
			label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 7);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(42, 15);
			label1.TabIndex = 2;
			label1.Text = "Nazwa";
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 2;
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel2.Controls.Add(label1, 0, 0);
			tableLayoutPanel2.Controls.Add(tabControl, 0, 1);
			tableLayoutPanel2.Controls.Add(textBoxNazwa, 1, 0);
			tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 2;
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel2.Size = new System.Drawing.Size(800, 425);
			tableLayoutPanel2.TabIndex = 3;
			// 
			// backgroundWorkerSprawdzMF
			// 
			backgroundWorkerSprawdzMF.DoWork += backgroundWorkerSprawdzMF_DoWork;
			backgroundWorkerSprawdzMF.RunWorkerCompleted += backgroundWorkerSprawdzMF_RunWorkerCompleted;
			// 
			// backgroundWorkerPobierzGUS
			// 
			backgroundWorkerPobierzGUS.DoWork += backgroundWorkerPobierzGUS_DoWork;
			backgroundWorkerPobierzGUS.RunWorkerCompleted += backgroundWorkerPobierzGUS_RunWorkerCompleted;
			// 
			// buttonKSeFAuth
			// 
			buttonKSeFAuth.Anchor = System.Windows.Forms.AnchorStyles.Left;
			buttonKSeFAuth.AutoSize = true;
			tableLayoutPanel3.SetColumnSpan(buttonKSeFAuth, 2);
			buttonKSeFAuth.Location = new System.Drawing.Point(682, 150);
			buttonKSeFAuth.Name = "buttonKSeFAuth";
			buttonKSeFAuth.Size = new System.Drawing.Size(95, 25);
			buttonKSeFAuth.TabIndex = 8;
			buttonKSeFAuth.Text = "Uzyskaj dostęp";
			buttonKSeFAuth.UseVisualStyleBackColor = true;
			buttonKSeFAuth.Click += buttonKSeFAuth_Click;
			// 
			// KontrahentEdytor
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(tableLayoutPanel2);
			MinimumSize = new System.Drawing.Size(800, 425);
			Name = "KontrahentEdytor";
			Size = new System.Drawing.Size(800, 425);
			((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
			tabControl.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			tabPage2.ResumeLayout(false);
			tabPage2.PerformLayout();
			tabPagePodatki.ResumeLayout(false);
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel3.PerformLayout();
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
		private System.Windows.Forms.TextBox textBoxUwagi;
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
		private System.ComponentModel.BackgroundWorker backgroundWorkerSprawdzMF;
		private ButtonDPI buttonPobierzGUS;
		private System.ComponentModel.BackgroundWorker backgroundWorkerPobierzGUS;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox textBoxTokenKSeF;
		private System.Windows.Forms.ComboBox comboBoxSrodowiskoKSeF;
		private ButtonDPI buttonKSeFAuth;
		private System.Windows.Forms.DateTimePickerFix dateTimePickerOsobaFizycznaDataUrodzenia;
	}
}
