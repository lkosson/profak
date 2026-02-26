
namespace ProFak.UI;

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
		flowLayoutPanelZawartosc = new System.Windows.Forms.FlowLayoutPanel();
		label1 = new System.Windows.Forms.Label();
		label3 = new System.Windows.Forms.Label();
		radioButtonNowaPrywatnaBaza = new System.Windows.Forms.RadioButton();
		radioButtonNowaPublicznaBaza = new System.Windows.Forms.RadioButton();
		radioButtonNowaLokalnaBaza = new System.Windows.Forms.RadioButton();
		radioButtonZewnetrznaBaza = new System.Windows.Forms.RadioButton();
		radioButtonBazaDemo = new System.Windows.Forms.RadioButton();
		radioButtonOdtworzKopie = new System.Windows.Forms.RadioButton();
		flowLayoutPanelStopka = new System.Windows.Forms.FlowLayoutPanel();
		buttonDalej = new System.Windows.Forms.Button();
		progressBar = new System.Windows.Forms.ProgressBar();
		labelStatus = new System.Windows.Forms.Label();
		backgroundWorker = new System.ComponentModel.BackgroundWorker();
		openFileDialogBaza = new System.Windows.Forms.OpenFileDialog();
		openFileDialogBackup = new System.Windows.Forms.OpenFileDialog();
		flowLayoutPanelZawartosc.SuspendLayout();
		flowLayoutPanelStopka.SuspendLayout();
		SuspendLayout();
		// 
		// flowLayoutPanelZawartosc
		// 
		flowLayoutPanelZawartosc.AutoSize = true;
		flowLayoutPanelZawartosc.Controls.Add(label1);
		flowLayoutPanelZawartosc.Controls.Add(label3);
		flowLayoutPanelZawartosc.Controls.Add(radioButtonNowaPrywatnaBaza);
		flowLayoutPanelZawartosc.Controls.Add(radioButtonNowaPublicznaBaza);
		flowLayoutPanelZawartosc.Controls.Add(radioButtonNowaLokalnaBaza);
		flowLayoutPanelZawartosc.Controls.Add(radioButtonZewnetrznaBaza);
		flowLayoutPanelZawartosc.Controls.Add(radioButtonBazaDemo);
		flowLayoutPanelZawartosc.Controls.Add(radioButtonOdtworzKopie);
		flowLayoutPanelZawartosc.Controls.Add(flowLayoutPanelStopka);
		flowLayoutPanelZawartosc.Dock = System.Windows.Forms.DockStyle.Fill;
		flowLayoutPanelZawartosc.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
		flowLayoutPanelZawartosc.Location = new System.Drawing.Point(0, 0);
		flowLayoutPanelZawartosc.Name = "flowLayoutPanelZawartosc";
		flowLayoutPanelZawartosc.Padding = new System.Windows.Forms.Padding(8);
		flowLayoutPanelZawartosc.Size = new System.Drawing.Size(585, 275);
		flowLayoutPanelZawartosc.TabIndex = 0;
		flowLayoutPanelZawartosc.WrapContents = false;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(11, 11);
		label1.Margin = new System.Windows.Forms.Padding(3);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(551, 30);
		label1.TabIndex = 0;
		label1.Text = "Wygląda na to, że program jeszcze nie był uruchamiany na tym komputerze. Przed rozpoczęciem pracy konieczne jest przygotowanie bazy danych.";
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(11, 47);
		label3.Margin = new System.Windows.Forms.Padding(3);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(551, 30);
		label3.TabIndex = 0;
		label3.Text = "Zaznacz jeden z poniższych punktów i kliknij \"Dalej\". Jeśli nie wiesz co wybrać i chcesz po prostu zacząć korzystać z programu, zostaw domyślny wybór bez zmian i kliknij \"Dalej\".";
		// 
		// radioButtonNowaPrywatnaBaza
		// 
		radioButtonNowaPrywatnaBaza.AutoSize = true;
		radioButtonNowaPrywatnaBaza.Checked = true;
		radioButtonNowaPrywatnaBaza.Location = new System.Drawing.Point(11, 83);
		radioButtonNowaPrywatnaBaza.Name = "radioButtonNowaPrywatnaBaza";
		radioButtonNowaPrywatnaBaza.Size = new System.Drawing.Size(490, 19);
		radioButtonNowaPrywatnaBaza.TabIndex = 1;
		radioButtonNowaPrywatnaBaza.TabStop = true;
		radioButtonNowaPrywatnaBaza.Text = "Utwórz nową, pustą bazę danych, dostępną tylko dla bieżącego użytkownika komputera.";
		radioButtonNowaPrywatnaBaza.UseVisualStyleBackColor = true;
		// 
		// radioButtonNowaPublicznaBaza
		// 
		radioButtonNowaPublicznaBaza.AutoSize = true;
		radioButtonNowaPublicznaBaza.Location = new System.Drawing.Point(11, 108);
		radioButtonNowaPublicznaBaza.Name = "radioButtonNowaPublicznaBaza";
		radioButtonNowaPublicznaBaza.Size = new System.Drawing.Size(501, 19);
		radioButtonNowaPublicznaBaza.TabIndex = 2;
		radioButtonNowaPublicznaBaza.Text = "Utwórz nową, pustą bazę danych, dostępną dla wszystkich użytkowników tego komputera.";
		radioButtonNowaPublicznaBaza.UseVisualStyleBackColor = true;
		// 
		// radioButtonNowaLokalnaBaza
		// 
		radioButtonNowaLokalnaBaza.AutoSize = true;
		radioButtonNowaLokalnaBaza.Location = new System.Drawing.Point(11, 133);
		radioButtonNowaLokalnaBaza.Name = "radioButtonNowaLokalnaBaza";
		radioButtonNowaLokalnaBaza.Size = new System.Drawing.Size(472, 19);
		radioButtonNowaLokalnaBaza.TabIndex = 3;
		radioButtonNowaLokalnaBaza.Text = "Utwórz nową, pustą bazę danych w katalogu w którym został uruchomiony program.";
		radioButtonNowaLokalnaBaza.UseVisualStyleBackColor = true;
		// 
		// radioButtonZewnetrznaBaza
		// 
		radioButtonZewnetrznaBaza.AutoSize = true;
		radioButtonZewnetrznaBaza.Location = new System.Drawing.Point(11, 158);
		radioButtonZewnetrznaBaza.Name = "radioButtonZewnetrznaBaza";
		radioButtonZewnetrznaBaza.Size = new System.Drawing.Size(461, 19);
		radioButtonZewnetrznaBaza.TabIndex = 3;
		radioButtonZewnetrznaBaza.Text = "Uruchom program korzystając z zewnętrznej bazy danych we wskazanym katalogu.";
		radioButtonZewnetrznaBaza.UseVisualStyleBackColor = true;
		// 
		// radioButtonBazaDemo
		// 
		radioButtonBazaDemo.AutoSize = true;
		radioButtonBazaDemo.Location = new System.Drawing.Point(11, 183);
		radioButtonBazaDemo.Name = "radioButtonBazaDemo";
		radioButtonBazaDemo.Size = new System.Drawing.Size(563, 19);
		radioButtonBazaDemo.TabIndex = 3;
		radioButtonBazaDemo.Text = "Uruchom program w trybie demonstracyjnym z przykładowymi danymi, w tymczasowej bazie danych.";
		radioButtonBazaDemo.UseVisualStyleBackColor = true;
		// 
		// radioButtonOdtworzKopie
		// 
		radioButtonOdtworzKopie.AutoSize = true;
		radioButtonOdtworzKopie.Location = new System.Drawing.Point(11, 208);
		radioButtonOdtworzKopie.Name = "radioButtonOdtworzKopie";
		radioButtonOdtworzKopie.Size = new System.Drawing.Size(346, 19);
		radioButtonOdtworzKopie.TabIndex = 3;
		radioButtonOdtworzKopie.Text = "Odtwórz bazę danych ze wskazanego pliku z kopią zapasową.";
		radioButtonOdtworzKopie.UseVisualStyleBackColor = true;
		// 
		// flowLayoutPanelStopka
		// 
		flowLayoutPanelStopka.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		flowLayoutPanelStopka.Controls.Add(buttonDalej);
		flowLayoutPanelStopka.Controls.Add(progressBar);
		flowLayoutPanelStopka.Controls.Add(labelStatus);
		flowLayoutPanelStopka.Location = new System.Drawing.Point(11, 233);
		flowLayoutPanelStopka.Name = "flowLayoutPanelStopka";
		flowLayoutPanelStopka.Size = new System.Drawing.Size(563, 29);
		flowLayoutPanelStopka.TabIndex = 5;
		flowLayoutPanelStopka.WrapContents = false;
		// 
		// buttonDalej
		// 
		buttonDalej.Location = new System.Drawing.Point(3, 3);
		buttonDalej.Name = "buttonDalej";
		buttonDalej.Size = new System.Drawing.Size(75, 23);
		buttonDalej.TabIndex = 4;
		buttonDalej.Text = "Dalej";
		buttonDalej.UseVisualStyleBackColor = true;
		buttonDalej.Click += buttonDalej_Click;
		// 
		// progressBar
		// 
		progressBar.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		progressBar.Location = new System.Drawing.Point(84, 3);
		progressBar.Name = "progressBar";
		progressBar.Size = new System.Drawing.Size(105, 23);
		progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
		progressBar.TabIndex = 5;
		progressBar.Visible = false;
		// 
		// labelStatus
		// 
		labelStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
		labelStatus.AutoSize = true;
		labelStatus.Location = new System.Drawing.Point(195, 7);
		labelStatus.Name = "labelStatus";
		labelStatus.Size = new System.Drawing.Size(38, 15);
		labelStatus.TabIndex = 6;
		labelStatus.Text = "label2";
		labelStatus.Visible = false;
		// 
		// backgroundWorker
		// 
		backgroundWorker.WorkerReportsProgress = true;
		backgroundWorker.DoWork += backgroundWorker_DoWork;
		backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
		backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
		// 
		// openFileDialogBaza
		// 
		openFileDialogBaza.FileName = "profak.sqlite3";
		openFileDialogBaza.Filter = "Baza danych ProFak (profak.sqlite3)|profak.sqlite3|Wszystkie pliki (*.*)|*.*";
		openFileDialogBaza.RestoreDirectory = true;
		// 
		// openFileDialogBackup
		// 
		openFileDialogBackup.Filter = "Kopia zapasowa programu ProFak (*.probak)|*.probak|Wszystkie pliki (*.*)|*.*";
		openFileDialogBackup.RestoreDirectory = true;
		// 
		// PierwszyStartBaza
		// 
		AcceptButton = buttonDalej;
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		ClientSize = new System.Drawing.Size(585, 275);
		Controls.Add(flowLayoutPanelZawartosc);
		Name = "PierwszyStartBaza";
		Text = "ProFak - Pierwsze uruchomienie";
		flowLayoutPanelZawartosc.ResumeLayout(false);
		flowLayoutPanelZawartosc.PerformLayout();
		flowLayoutPanelStopka.ResumeLayout(false);
		flowLayoutPanelStopka.PerformLayout();
		ResumeLayout(false);
		PerformLayout();

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