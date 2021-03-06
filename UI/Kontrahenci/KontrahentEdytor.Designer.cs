
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
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxPelnaNazwa = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.textBoxNIP = new System.Windows.Forms.TextBox();
			this.textBoxAdresRejestrowy = new System.Windows.Forms.TextBox();
			this.textBoxAdresKorespondencyjny = new System.Windows.Forms.TextBox();
			this.textBoxTelefon = new System.Windows.Forms.TextBox();
			this.textBoxEMail = new System.Windows.Forms.TextBox();
			this.textBoxRachunekBankowy = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.comboBoxStan = new System.Windows.Forms.ComboBox();
			this.label9 = new System.Windows.Forms.Label();
			this.checkBoxTP = new System.Windows.Forms.CheckBox();
			this.buttonSprawdzMF = new System.Windows.Forms.Button();
			this.buttonPobierzGUS = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.textBoxUwagi = new System.Windows.Forms.TextBox();
			this.tabPageFakturySprzedazy = new System.Windows.Forms.TabPage();
			this.tabPageFakturyZakupu = new System.Windows.Forms.TabPage();
			this.tabPagePodatki = new System.Windows.Forms.TabPage();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.comboBoxKodUrzedu = new System.Windows.Forms.ComboBox();
			this.textBoxOsobaFizycznaImie = new System.Windows.Forms.TextBox();
			this.textBoxOsobaFizycznaNazwisko = new System.Windows.Forms.TextBox();
			this.dateTimePickerOsobaFizycznaDataUrodzenia = new System.Windows.Forms.DateTimePicker();
			this.buttonUrzadSkarbowy = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.comboBoxFormaOpodatkowania = new System.Windows.Forms.ComboBox();
			this.textBoxNazwa = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.backgroundWorkerSprawdzMF = new System.ComponentModel.BackgroundWorker();
			this.backgroundWorkerPobierzGUS = new System.ComponentModel.BackgroundWorker();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPagePodatki.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tableLayoutPanel2.SetColumnSpan(this.tabControl, 2);
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Controls.Add(this.tabPageFakturySprzedazy);
			this.tabControl.Controls.Add(this.tabPageFakturyZakupu);
			this.tabControl.Controls.Add(this.tabPagePodatki);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(3, 32);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(794, 390);
			this.tabControl.TabIndex = 2;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.tableLayoutPanel1);
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(786, 362);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Dane podstawowe";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.textBoxPelnaNazwa, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.textBoxNIP, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.textBoxAdresRejestrowy, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.textBoxAdresKorespondencyjny, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.textBoxTelefon, 1, 4);
			this.tableLayoutPanel1.Controls.Add(this.textBoxEMail, 1, 5);
			this.tableLayoutPanel1.Controls.Add(this.textBoxRachunekBankowy, 1, 6);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 5);
			this.tableLayoutPanel1.Controls.Add(this.label8, 0, 6);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxStan, 1, 7);
			this.tableLayoutPanel1.Controls.Add(this.label9, 0, 7);
			this.tableLayoutPanel1.Controls.Add(this.checkBoxTP, 1, 9);
			this.tableLayoutPanel1.Controls.Add(this.buttonSprawdzMF, 2, 6);
			this.tableLayoutPanel1.Controls.Add(this.buttonPobierzGUS, 2, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 11;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(780, 356);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(68, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 15);
			this.label2.TabIndex = 2;
			this.label2.Text = "Pełna nazwa";
			// 
			// textBoxPelnaNazwa
			// 
			this.textBoxPelnaNazwa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.textBoxPelnaNazwa, 2);
			this.textBoxPelnaNazwa.Location = new System.Drawing.Point(146, 3);
			this.textBoxPelnaNazwa.Name = "textBoxPelnaNazwa";
			this.textBoxPelnaNazwa.Size = new System.Drawing.Size(631, 23);
			this.textBoxPelnaNazwa.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(114, 37);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(26, 15);
			this.label3.TabIndex = 2;
			this.label3.Text = "NIP";
			// 
			// textBoxNIP
			// 
			this.textBoxNIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxNIP.Location = new System.Drawing.Point(146, 33);
			this.textBoxNIP.Name = "textBoxNIP";
			this.textBoxNIP.Size = new System.Drawing.Size(465, 23);
			this.textBoxNIP.TabIndex = 2;
			// 
			// textBoxAdresRejestrowy
			// 
			this.textBoxAdresRejestrowy.AcceptsReturn = true;
			this.textBoxAdresRejestrowy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.textBoxAdresRejestrowy, 2);
			this.textBoxAdresRejestrowy.Location = new System.Drawing.Point(146, 63);
			this.textBoxAdresRejestrowy.Multiline = true;
			this.textBoxAdresRejestrowy.Name = "textBoxAdresRejestrowy";
			this.textBoxAdresRejestrowy.Size = new System.Drawing.Size(631, 65);
			this.textBoxAdresRejestrowy.TabIndex = 3;
			this.textBoxAdresRejestrowy.TextChanged += new System.EventHandler(this.textBoxAdresRejestrowy_TextChanged);
			// 
			// textBoxAdresKorespondencyjny
			// 
			this.textBoxAdresKorespondencyjny.AcceptsReturn = true;
			this.textBoxAdresKorespondencyjny.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.textBoxAdresKorespondencyjny, 2);
			this.textBoxAdresKorespondencyjny.Location = new System.Drawing.Point(146, 134);
			this.textBoxAdresKorespondencyjny.Multiline = true;
			this.textBoxAdresKorespondencyjny.Name = "textBoxAdresKorespondencyjny";
			this.textBoxAdresKorespondencyjny.Size = new System.Drawing.Size(631, 65);
			this.textBoxAdresKorespondencyjny.TabIndex = 4;
			// 
			// textBoxTelefon
			// 
			this.textBoxTelefon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.textBoxTelefon, 2);
			this.textBoxTelefon.Location = new System.Drawing.Point(146, 205);
			this.textBoxTelefon.Name = "textBoxTelefon";
			this.textBoxTelefon.Size = new System.Drawing.Size(631, 23);
			this.textBoxTelefon.TabIndex = 5;
			// 
			// textBoxEMail
			// 
			this.textBoxEMail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.textBoxEMail, 2);
			this.textBoxEMail.Location = new System.Drawing.Point(146, 234);
			this.textBoxEMail.Name = "textBoxEMail";
			this.textBoxEMail.Size = new System.Drawing.Size(631, 23);
			this.textBoxEMail.TabIndex = 6;
			// 
			// textBoxRachunekBankowy
			// 
			this.textBoxRachunekBankowy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRachunekBankowy.Location = new System.Drawing.Point(146, 264);
			this.textBoxRachunekBankowy.Name = "textBoxRachunekBankowy";
			this.textBoxRachunekBankowy.Size = new System.Drawing.Size(465, 23);
			this.textBoxRachunekBankowy.TabIndex = 7;
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(46, 88);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(94, 15);
			this.label4.TabIndex = 2;
			this.label4.Text = "Adres rejestrowy";
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 159);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(137, 15);
			this.label5.TabIndex = 2;
			this.label5.Text = "Adres korespondencyjny";
			// 
			// label6
			// 
			this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(94, 209);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(46, 15);
			this.label6.TabIndex = 2;
			this.label6.Text = "Telefon";
			// 
			// label7
			// 
			this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(99, 238);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(41, 15);
			this.label7.TabIndex = 2;
			this.label7.Text = "E-Mail";
			// 
			// label8
			// 
			this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(30, 268);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(110, 15);
			this.label8.TabIndex = 2;
			this.label8.Text = "Rachunek bankowy";
			// 
			// comboBoxStan
			// 
			this.comboBoxStan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.comboBoxStan, 2);
			this.comboBoxStan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxStan.FormattingEnabled = true;
			this.comboBoxStan.Location = new System.Drawing.Point(146, 294);
			this.comboBoxStan.Name = "comboBoxStan";
			this.comboBoxStan.Size = new System.Drawing.Size(631, 23);
			this.comboBoxStan.TabIndex = 8;
			// 
			// label9
			// 
			this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(110, 298);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(30, 15);
			this.label9.TabIndex = 2;
			this.label9.Text = "Stan";
			// 
			// checkBoxTP
			// 
			this.checkBoxTP.AutoSize = true;
			this.checkBoxTP.Location = new System.Drawing.Point(146, 323);
			this.checkBoxTP.Name = "checkBoxTP";
			this.checkBoxTP.Size = new System.Drawing.Size(131, 19);
			this.checkBoxTP.TabIndex = 9;
			this.checkBoxTP.Text = "Podmiot powiązany";
			this.checkBoxTP.UseVisualStyleBackColor = true;
			// 
			// buttonSprawdzMF
			// 
			this.buttonSprawdzMF.AutoSize = true;
			this.buttonSprawdzMF.Location = new System.Drawing.Point(617, 263);
			this.buttonSprawdzMF.Name = "buttonSprawdzMF";
			this.buttonSprawdzMF.Size = new System.Drawing.Size(160, 25);
			this.buttonSprawdzMF.TabIndex = 10;
			this.buttonSprawdzMF.Text = "Sprawdź na białej liście VAT";
			this.buttonSprawdzMF.UseVisualStyleBackColor = true;
			this.buttonSprawdzMF.Click += new System.EventHandler(this.buttonSprawdzMF_Click);
			// 
			// buttonPobierzGUS
			// 
			this.buttonPobierzGUS.AutoSize = true;
			this.buttonPobierzGUS.Location = new System.Drawing.Point(617, 32);
			this.buttonPobierzGUS.Name = "buttonPobierzGUS";
			this.buttonPobierzGUS.Size = new System.Drawing.Size(118, 25);
			this.buttonPobierzGUS.TabIndex = 10;
			this.buttonPobierzGUS.Text = "Pobierz dane z GUS";
			this.buttonPobierzGUS.UseVisualStyleBackColor = true;
			this.buttonPobierzGUS.Click += new System.EventHandler(this.buttonPobierzGUS_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.textBoxUwagi);
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(786, 362);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Uwagi";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// textBoxUwagi
			// 
			this.textBoxUwagi.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxUwagi.Location = new System.Drawing.Point(3, 3);
			this.textBoxUwagi.Multiline = true;
			this.textBoxUwagi.Name = "textBoxUwagi";
			this.textBoxUwagi.Size = new System.Drawing.Size(780, 356);
			this.textBoxUwagi.TabIndex = 0;
			// 
			// tabPageFakturySprzedazy
			// 
			this.tabPageFakturySprzedazy.Location = new System.Drawing.Point(4, 24);
			this.tabPageFakturySprzedazy.Name = "tabPageFakturySprzedazy";
			this.tabPageFakturySprzedazy.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageFakturySprzedazy.Size = new System.Drawing.Size(786, 362);
			this.tabPageFakturySprzedazy.TabIndex = 2;
			this.tabPageFakturySprzedazy.Text = "Sprzedaż do";
			this.tabPageFakturySprzedazy.UseVisualStyleBackColor = true;
			// 
			// tabPageFakturyZakupu
			// 
			this.tabPageFakturyZakupu.Location = new System.Drawing.Point(4, 24);
			this.tabPageFakturyZakupu.Name = "tabPageFakturyZakupu";
			this.tabPageFakturyZakupu.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageFakturyZakupu.Size = new System.Drawing.Size(786, 362);
			this.tabPageFakturyZakupu.TabIndex = 3;
			this.tabPageFakturyZakupu.Text = "Zakup od";
			this.tabPageFakturyZakupu.UseVisualStyleBackColor = true;
			// 
			// tabPagePodatki
			// 
			this.tabPagePodatki.Controls.Add(this.tableLayoutPanel3);
			this.tabPagePodatki.Location = new System.Drawing.Point(4, 24);
			this.tabPagePodatki.Name = "tabPagePodatki";
			this.tabPagePodatki.Padding = new System.Windows.Forms.Padding(3);
			this.tabPagePodatki.Size = new System.Drawing.Size(786, 362);
			this.tabPagePodatki.TabIndex = 4;
			this.tabPagePodatki.Text = "Podatki";
			this.tabPagePodatki.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 3;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.Controls.Add(this.label10, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.label11, 0, 1);
			this.tableLayoutPanel3.Controls.Add(this.label12, 0, 2);
			this.tableLayoutPanel3.Controls.Add(this.label13, 0, 3);
			this.tableLayoutPanel3.Controls.Add(this.comboBoxKodUrzedu, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.textBoxOsobaFizycznaImie, 1, 1);
			this.tableLayoutPanel3.Controls.Add(this.textBoxOsobaFizycznaNazwisko, 1, 2);
			this.tableLayoutPanel3.Controls.Add(this.dateTimePickerOsobaFizycznaDataUrodzenia, 1, 3);
			this.tableLayoutPanel3.Controls.Add(this.buttonUrzadSkarbowy, 2, 0);
			this.tableLayoutPanel3.Controls.Add(this.label14, 0, 4);
			this.tableLayoutPanel3.Controls.Add(this.comboBoxFormaOpodatkowania, 1, 4);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 6;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(780, 356);
			this.tableLayoutPanel3.TabIndex = 0;
			// 
			// label10
			// 
			this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(3, 8);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(134, 15);
			this.label10.TabIndex = 0;
			this.label10.Text = "Kod urzędu skarbowego";
			// 
			// label11
			// 
			this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(59, 38);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(78, 15);
			this.label11.TabIndex = 0;
			this.label11.Text = "Pierwsze imię";
			// 
			// label12
			// 
			this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(80, 67);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(57, 15);
			this.label12.TabIndex = 0;
			this.label12.Text = "Nazwisko";
			// 
			// label13
			// 
			this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(51, 96);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(86, 15);
			this.label13.TabIndex = 0;
			this.label13.Text = "Data urodzenia";
			// 
			// comboBoxKodUrzedu
			// 
			this.comboBoxKodUrzedu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxKodUrzedu.FormattingEnabled = true;
			this.comboBoxKodUrzedu.Location = new System.Drawing.Point(143, 4);
			this.comboBoxKodUrzedu.Name = "comboBoxKodUrzedu";
			this.comboBoxKodUrzedu.Size = new System.Drawing.Size(602, 23);
			this.comboBoxKodUrzedu.TabIndex = 1;
			// 
			// textBoxOsobaFizycznaImie
			// 
			this.textBoxOsobaFizycznaImie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel3.SetColumnSpan(this.textBoxOsobaFizycznaImie, 2);
			this.textBoxOsobaFizycznaImie.Location = new System.Drawing.Point(143, 34);
			this.textBoxOsobaFizycznaImie.Name = "textBoxOsobaFizycznaImie";
			this.textBoxOsobaFizycznaImie.Size = new System.Drawing.Size(634, 23);
			this.textBoxOsobaFizycznaImie.TabIndex = 2;
			// 
			// textBoxOsobaFizycznaNazwisko
			// 
			this.textBoxOsobaFizycznaNazwisko.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel3.SetColumnSpan(this.textBoxOsobaFizycznaNazwisko, 2);
			this.textBoxOsobaFizycznaNazwisko.Location = new System.Drawing.Point(143, 63);
			this.textBoxOsobaFizycznaNazwisko.Name = "textBoxOsobaFizycznaNazwisko";
			this.textBoxOsobaFizycznaNazwisko.Size = new System.Drawing.Size(634, 23);
			this.textBoxOsobaFizycznaNazwisko.TabIndex = 2;
			// 
			// dateTimePickerOsobaFizycznaDataUrodzenia
			// 
			this.dateTimePickerOsobaFizycznaDataUrodzenia.Location = new System.Drawing.Point(143, 92);
			this.dateTimePickerOsobaFizycznaDataUrodzenia.Name = "dateTimePickerOsobaFizycznaDataUrodzenia";
			this.dateTimePickerOsobaFizycznaDataUrodzenia.ShowCheckBox = true;
			this.dateTimePickerOsobaFizycznaDataUrodzenia.Size = new System.Drawing.Size(200, 23);
			this.dateTimePickerOsobaFizycznaDataUrodzenia.TabIndex = 3;
			// 
			// buttonUrzadSkarbowy
			// 
			this.buttonUrzadSkarbowy.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.buttonUrzadSkarbowy.AutoSize = true;
			this.buttonUrzadSkarbowy.Location = new System.Drawing.Point(751, 3);
			this.buttonUrzadSkarbowy.Name = "buttonUrzadSkarbowy";
			this.buttonUrzadSkarbowy.Size = new System.Drawing.Size(26, 25);
			this.buttonUrzadSkarbowy.TabIndex = 12;
			this.buttonUrzadSkarbowy.Text = "...";
			this.buttonUrzadSkarbowy.UseVisualStyleBackColor = true;
			// 
			// label14
			// 
			this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(11, 125);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(126, 15);
			this.label14.TabIndex = 0;
			this.label14.Text = "Forma opodatkowania";
			// 
			// comboBoxFormaOpodatkowania
			// 
			this.comboBoxFormaOpodatkowania.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel3.SetColumnSpan(this.comboBoxFormaOpodatkowania, 2);
			this.comboBoxFormaOpodatkowania.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxFormaOpodatkowania.FormattingEnabled = true;
			this.comboBoxFormaOpodatkowania.Location = new System.Drawing.Point(143, 121);
			this.comboBoxFormaOpodatkowania.Name = "comboBoxFormaOpodatkowania";
			this.comboBoxFormaOpodatkowania.Size = new System.Drawing.Size(634, 23);
			this.comboBoxFormaOpodatkowania.TabIndex = 5;
			// 
			// textBoxNazwa
			// 
			this.textBoxNazwa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxNazwa.Location = new System.Drawing.Point(51, 3);
			this.textBoxNazwa.Name = "textBoxNazwa";
			this.textBoxNazwa.Size = new System.Drawing.Size(746, 23);
			this.textBoxNazwa.TabIndex = 1;
			this.textBoxNazwa.TextChanged += new System.EventHandler(this.textBoxNazwa_TextChanged);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 15);
			this.label1.TabIndex = 2;
			this.label1.Text = "Nazwa";
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.tabControl, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.textBoxNazwa, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 2;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(800, 425);
			this.tableLayoutPanel2.TabIndex = 3;
			// 
			// backgroundWorkerSprawdzMF
			// 
			this.backgroundWorkerSprawdzMF.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSprawdzMF_DoWork);
			this.backgroundWorkerSprawdzMF.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerSprawdzMF_RunWorkerCompleted);
			// 
			// backgroundWorkerPobierzGUS
			// 
			this.backgroundWorkerPobierzGUS.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerPobierzGUS_DoWork);
			this.backgroundWorkerPobierzGUS.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerPobierzGUS_RunWorkerCompleted);
			// 
			// KontrahentEdytor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel2);
			this.MinimumSize = new System.Drawing.Size(800, 425);
			this.Name = "KontrahentEdytor";
			this.Size = new System.Drawing.Size(800, 425);
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.tabPagePodatki.ResumeLayout(false);
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.ResumeLayout(false);

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
		private System.Windows.Forms.DateTimePicker dateTimePickerOsobaFizycznaDataUrodzenia;
		private System.Windows.Forms.Button buttonUrzadSkarbowy;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.ComboBox comboBoxFormaOpodatkowania;
		private System.Windows.Forms.Button buttonSprawdzMF;
		private System.ComponentModel.BackgroundWorker backgroundWorkerSprawdzMF;
		private System.Windows.Forms.Button buttonPobierzGUS;
		private System.ComponentModel.BackgroundWorker backgroundWorkerPobierzGUS;
	}
}
