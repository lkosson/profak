
namespace ProFak.UI
{
	partial class PierwszyStartBaza
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.flowLayoutPanelZawartosc = new System.Windows.Forms.FlowLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.radioButtonNowaPrywatnaBaza = new System.Windows.Forms.RadioButton();
			this.radioButtonNowaPublicznaBaza = new System.Windows.Forms.RadioButton();
			this.radioButtonNowaLokalnaBaza = new System.Windows.Forms.RadioButton();
			this.radioButtonZewnetrznaBaza = new System.Windows.Forms.RadioButton();
			this.radioButtonBazaDemo = new System.Windows.Forms.RadioButton();
			this.radioButtonOdtworzKopie = new System.Windows.Forms.RadioButton();
			this.flowLayoutPanelStopka = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonDalej = new System.Windows.Forms.Button();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.labelStatus = new System.Windows.Forms.Label();
			this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.openFileDialogBaza = new System.Windows.Forms.OpenFileDialog();
			this.openFileDialogBackup = new System.Windows.Forms.OpenFileDialog();
			this.flowLayoutPanelZawartosc.SuspendLayout();
			this.flowLayoutPanelStopka.SuspendLayout();
			this.SuspendLayout();
			// 
			// flowLayoutPanelZawartosc
			// 
			this.flowLayoutPanelZawartosc.AutoSize = true;
			this.flowLayoutPanelZawartosc.Controls.Add(this.label1);
			this.flowLayoutPanelZawartosc.Controls.Add(this.label3);
			this.flowLayoutPanelZawartosc.Controls.Add(this.radioButtonNowaPrywatnaBaza);
			this.flowLayoutPanelZawartosc.Controls.Add(this.radioButtonNowaPublicznaBaza);
			this.flowLayoutPanelZawartosc.Controls.Add(this.radioButtonNowaLokalnaBaza);
			this.flowLayoutPanelZawartosc.Controls.Add(this.radioButtonZewnetrznaBaza);
			this.flowLayoutPanelZawartosc.Controls.Add(this.radioButtonBazaDemo);
			this.flowLayoutPanelZawartosc.Controls.Add(this.radioButtonOdtworzKopie);
			this.flowLayoutPanelZawartosc.Controls.Add(this.flowLayoutPanelStopka);
			this.flowLayoutPanelZawartosc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanelZawartosc.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanelZawartosc.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanelZawartosc.Name = "flowLayoutPanelZawartosc";
			this.flowLayoutPanelZawartosc.Padding = new System.Windows.Forms.Padding(8);
			this.flowLayoutPanelZawartosc.Size = new System.Drawing.Size(585, 275);
			this.flowLayoutPanelZawartosc.TabIndex = 0;
			this.flowLayoutPanelZawartosc.WrapContents = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 11);
			this.label1.Margin = new System.Windows.Forms.Padding(3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(551, 30);
			this.label1.TabIndex = 0;
			this.label1.Text = "Wygląda na to, że program jeszcze nie był uruchamiany na tym komputerze. Przed ro" +
    "zpoczęciem pracy konieczne jest przygotowanie bazy danych.";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(11, 47);
			this.label3.Margin = new System.Windows.Forms.Padding(3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(551, 30);
			this.label3.TabIndex = 0;
			this.label3.Text = "Zaznacz jeden z poniższych punktów i kliknij \"Dalej\". Jeśli nie wiesz co wybrać i" +
    " chcesz po prostu zacząć korzystać z programu, zostaw domyślny wybór bez zmian i" +
    " kliknij \"Dalej\".";
			// 
			// radioButtonNowaPrywatnaBaza
			// 
			this.radioButtonNowaPrywatnaBaza.AutoSize = true;
			this.radioButtonNowaPrywatnaBaza.Checked = true;
			this.radioButtonNowaPrywatnaBaza.Location = new System.Drawing.Point(11, 83);
			this.radioButtonNowaPrywatnaBaza.Name = "radioButtonNowaPrywatnaBaza";
			this.radioButtonNowaPrywatnaBaza.Size = new System.Drawing.Size(490, 19);
			this.radioButtonNowaPrywatnaBaza.TabIndex = 1;
			this.radioButtonNowaPrywatnaBaza.TabStop = true;
			this.radioButtonNowaPrywatnaBaza.Text = "Utwórz nową, pustą bazę danych, dostępną tylko dla bieżącego użytkownika komputer" +
    "a.";
			this.radioButtonNowaPrywatnaBaza.UseVisualStyleBackColor = true;
			// 
			// radioButtonNowaPublicznaBaza
			// 
			this.radioButtonNowaPublicznaBaza.AutoSize = true;
			this.radioButtonNowaPublicznaBaza.Location = new System.Drawing.Point(11, 108);
			this.radioButtonNowaPublicznaBaza.Name = "radioButtonNowaPublicznaBaza";
			this.radioButtonNowaPublicznaBaza.Size = new System.Drawing.Size(501, 19);
			this.radioButtonNowaPublicznaBaza.TabIndex = 2;
			this.radioButtonNowaPublicznaBaza.Text = "Utwórz nową, pustą bazę danych, dostępną dla wszystkich użytkowników tego kompute" +
    "ra.";
			this.radioButtonNowaPublicznaBaza.UseVisualStyleBackColor = true;
			// 
			// radioButtonNowaLokalnaBaza
			// 
			this.radioButtonNowaLokalnaBaza.AutoSize = true;
			this.radioButtonNowaLokalnaBaza.Location = new System.Drawing.Point(11, 133);
			this.radioButtonNowaLokalnaBaza.Name = "radioButtonNowaLokalnaBaza";
			this.radioButtonNowaLokalnaBaza.Size = new System.Drawing.Size(472, 19);
			this.radioButtonNowaLokalnaBaza.TabIndex = 3;
			this.radioButtonNowaLokalnaBaza.Text = "Utwórz nową, pustą bazę danych w katalogu w którym został uruchomiony program.";
			this.radioButtonNowaLokalnaBaza.UseVisualStyleBackColor = true;
			// 
			// radioButtonZewnetrznaBaza
			// 
			this.radioButtonZewnetrznaBaza.AutoSize = true;
			this.radioButtonZewnetrznaBaza.Location = new System.Drawing.Point(11, 158);
			this.radioButtonZewnetrznaBaza.Name = "radioButtonZewnetrznaBaza";
			this.radioButtonZewnetrznaBaza.Size = new System.Drawing.Size(461, 19);
			this.radioButtonZewnetrznaBaza.TabIndex = 3;
			this.radioButtonZewnetrznaBaza.Text = "Uruchom program korzystając z zewnętrznej bazy danych we wskazanym katalogu.";
			this.radioButtonZewnetrznaBaza.UseVisualStyleBackColor = true;
			// 
			// radioButtonBazaDemo
			// 
			this.radioButtonBazaDemo.AutoSize = true;
			this.radioButtonBazaDemo.Location = new System.Drawing.Point(11, 183);
			this.radioButtonBazaDemo.Name = "radioButtonBazaDemo";
			this.radioButtonBazaDemo.Size = new System.Drawing.Size(563, 19);
			this.radioButtonBazaDemo.TabIndex = 3;
			this.radioButtonBazaDemo.Text = "Uruchom program w trybie demonstracyjnym z przykładowymi danymi, w tymczasowej ba" +
    "zie danych.";
			this.radioButtonBazaDemo.UseVisualStyleBackColor = true;
			// 
			// radioButtonOdtworzKopie
			// 
			this.radioButtonOdtworzKopie.AutoSize = true;
			this.radioButtonOdtworzKopie.Location = new System.Drawing.Point(11, 208);
			this.radioButtonOdtworzKopie.Name = "radioButtonOdtworzKopie";
			this.radioButtonOdtworzKopie.Size = new System.Drawing.Size(346, 19);
			this.radioButtonOdtworzKopie.TabIndex = 3;
			this.radioButtonOdtworzKopie.Text = "Odtwórz bazę danych ze wskazanego pliku z kopią zapasową.";
			this.radioButtonOdtworzKopie.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanelStopka
			// 
			this.flowLayoutPanelStopka.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.flowLayoutPanelStopka.Controls.Add(this.buttonDalej);
			this.flowLayoutPanelStopka.Controls.Add(this.progressBar);
			this.flowLayoutPanelStopka.Controls.Add(this.labelStatus);
			this.flowLayoutPanelStopka.Location = new System.Drawing.Point(11, 233);
			this.flowLayoutPanelStopka.Name = "flowLayoutPanelStopka";
			this.flowLayoutPanelStopka.Size = new System.Drawing.Size(563, 29);
			this.flowLayoutPanelStopka.TabIndex = 5;
			this.flowLayoutPanelStopka.WrapContents = false;
			// 
			// buttonDalej
			// 
			this.buttonDalej.Location = new System.Drawing.Point(3, 3);
			this.buttonDalej.Name = "buttonDalej";
			this.buttonDalej.Size = new System.Drawing.Size(75, 23);
			this.buttonDalej.TabIndex = 4;
			this.buttonDalej.Text = "Dalej";
			this.buttonDalej.UseVisualStyleBackColor = true;
			this.buttonDalej.Click += new System.EventHandler(this.buttonDalej_Click);
			// 
			// progressBar
			// 
			this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar.Location = new System.Drawing.Point(84, 3);
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(105, 23);
			this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.progressBar.TabIndex = 5;
			this.progressBar.Visible = false;
			// 
			// labelStatus
			// 
			this.labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.labelStatus.AutoSize = true;
			this.labelStatus.Location = new System.Drawing.Point(195, 7);
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Size = new System.Drawing.Size(38, 15);
			this.labelStatus.TabIndex = 6;
			this.labelStatus.Text = "label2";
			this.labelStatus.Visible = false;
			// 
			// backgroundWorker
			// 
			this.backgroundWorker.WorkerReportsProgress = true;
			this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
			this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
			this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
			// 
			// openFileDialogBaza
			// 
			this.openFileDialogBaza.FileName = "profak.sqlite3";
			this.openFileDialogBaza.Filter = "profak.sqlite3|Baza danych ProFak (profak.sqlite3)|*.*|Wszystkie pliki (*.*)";
			this.openFileDialogBaza.RestoreDirectory = true;
			// 
			// openFileDialogBackup
			// 
			this.openFileDialogBackup.Filter = "*.probak|Kopia zapasowa programu ProFak (*.probak)|*.*|Wszystkie pliki (*.*)";
			this.openFileDialogBackup.RestoreDirectory = true;
			// 
			// PierwszyStartBaza
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(585, 275);
			this.Controls.Add(this.flowLayoutPanelZawartosc);
			this.Name = "PierwszyStartBaza";
			this.Text = "ProFak - Pierwsze uruchomienie";
			this.flowLayoutPanelZawartosc.ResumeLayout(false);
			this.flowLayoutPanelZawartosc.PerformLayout();
			this.flowLayoutPanelStopka.ResumeLayout(false);
			this.flowLayoutPanelStopka.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelZawartosc;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RadioButton radioButtonNowaPrywatnaBaza;
		private System.Windows.Forms.RadioButton radioButtonNowaPublicznaBaza;
		private System.Windows.Forms.RadioButton radioButtonNowaLokalnaBaza;
		private System.Windows.Forms.RadioButton radioButtonZewnetrznaBaza;
		private System.Windows.Forms.RadioButton radioButtonOdtworzKopie;
		private System.Windows.Forms.Button buttonDalej;
		private System.Windows.Forms.RadioButton radioButtonBazaDemo;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelStopka;
		private System.Windows.Forms.ProgressBar progressBar;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
		private System.Windows.Forms.Label labelStatus;
		private System.Windows.Forms.OpenFileDialog openFileDialogBaza;
		private System.Windows.Forms.OpenFileDialog openFileDialogBackup;
	}
}