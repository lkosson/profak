
namespace ProFak.UI;

partial class BazyDanych
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
		flowLayoutPanel1 = new FlowLayoutPanel();
		buttonUtworzKopie = new ButtonDPI();
		buttonPrzywrocKopie = new ButtonDPI();
		label1 = new Label();
		label2 = new Label();
		textBoxRozmiar = new TextBox();
		label4 = new Label();
		label5 = new Label();
		textBoxDataModyfikacji = new TextBox();
		comboBoxPlik = new ComboBox();
		buttonPrzenies = new ButtonDPI();
		label3 = new Label();
		flowLayoutPanel2 = new FlowLayoutPanel();
		buttonZapiszJSON = new ButtonDPI();
		buttonWczytajJSON = new ButtonDPI();
		saveFileDialogBackup = new SaveFileDialog();
		openFileDialogBackup = new OpenFileDialog();
		saveFileDialogJSON = new SaveFileDialog();
		openFileDialogJSON = new OpenFileDialog();
		tableLayoutPanel1.SuspendLayout();
		flowLayoutPanel1.SuspendLayout();
		flowLayoutPanel2.SuspendLayout();
		SuspendLayout();
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.ColumnCount = 3;
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
		tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 3);
		tableLayoutPanel1.Controls.Add(label1, 0, 0);
		tableLayoutPanel1.Controls.Add(label2, 0, 1);
		tableLayoutPanel1.Controls.Add(textBoxRozmiar, 1, 1);
		tableLayoutPanel1.Controls.Add(label4, 0, 3);
		tableLayoutPanel1.Controls.Add(label5, 0, 2);
		tableLayoutPanel1.Controls.Add(textBoxDataModyfikacji, 1, 2);
		tableLayoutPanel1.Controls.Add(comboBoxPlik, 1, 0);
		tableLayoutPanel1.Controls.Add(buttonPrzenies, 2, 0);
		tableLayoutPanel1.Controls.Add(label3, 0, 4);
		tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 1, 4);
		tableLayoutPanel1.Location = new Point(3, 3);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 6;
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle());
		tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
		tableLayoutPanel1.Size = new Size(594, 157);
		tableLayoutPanel1.TabIndex = 0;
		// 
		// flowLayoutPanel1
		// 
		flowLayoutPanel1.AutoSize = true;
		flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
		flowLayoutPanel1.Controls.Add(buttonUtworzKopie);
		flowLayoutPanel1.Controls.Add(buttonPrzywrocKopie);
		flowLayoutPanel1.Location = new Point(128, 87);
		flowLayoutPanel1.Margin = new Padding(0);
		flowLayoutPanel1.Name = "flowLayoutPanel1";
		flowLayoutPanel1.Size = new Size(162, 29);
		flowLayoutPanel1.TabIndex = 40;
		// 
		// buttonUtworzKopie
		// 
		buttonUtworzKopie.Location = new Point(3, 3);
		buttonUtworzKopie.Name = "buttonUtworzKopie";
		buttonUtworzKopie.Size = new Size(75, 23);
		buttonUtworzKopie.TabIndex = 3;
		buttonUtworzKopie.Text = "Utwórz";
		buttonUtworzKopie.UseVisualStyleBackColor = true;
		buttonUtworzKopie.Click += buttonUtworzKopie_Click;
		// 
		// buttonPrzywrocKopie
		// 
		buttonPrzywrocKopie.Location = new Point(84, 3);
		buttonPrzywrocKopie.Name = "buttonPrzywrocKopie";
		buttonPrzywrocKopie.Size = new Size(75, 23);
		buttonPrzywrocKopie.TabIndex = 3;
		buttonPrzywrocKopie.Text = "Przywróć";
		buttonPrzywrocKopie.UseVisualStyleBackColor = true;
		buttonPrzywrocKopie.Click += buttonPrzywrocKopie_Click;
		// 
		// label1
		// 
		label1.Anchor = AnchorStyles.Right;
		label1.AutoSize = true;
		label1.Location = new Point(30, 7);
		label1.Name = "label1";
		label1.Size = new Size(95, 15);
		label1.TabIndex = 0;
		label1.Text = "Plik bazy danych";
		// 
		// label2
		// 
		label2.Anchor = AnchorStyles.Right;
		label2.AutoSize = true;
		label2.Location = new Point(48, 36);
		label2.Name = "label2";
		label2.Size = new Size(77, 15);
		label2.TabIndex = 0;
		label2.Text = "Rozmiar bazy";
		// 
		// textBoxRozmiar
		// 
		textBoxRozmiar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(textBoxRozmiar, 2);
		textBoxRozmiar.Location = new Point(131, 32);
		textBoxRozmiar.Name = "textBoxRozmiar";
		textBoxRozmiar.ReadOnly = true;
		textBoxRozmiar.Size = new Size(460, 23);
		textBoxRozmiar.TabIndex = 10;
		// 
		// label4
		// 
		label4.Anchor = AnchorStyles.Right;
		label4.AutoSize = true;
		label4.Location = new Point(3, 94);
		label4.Name = "label4";
		label4.Size = new Size(122, 15);
		label4.TabIndex = 0;
		label4.Text = "Kopia bezpieczeństwa";
		// 
		// label5
		// 
		label5.Anchor = AnchorStyles.Right;
		label5.AutoSize = true;
		label5.Location = new Point(6, 65);
		label5.Name = "label5";
		label5.Size = new Size(119, 15);
		label5.TabIndex = 0;
		label5.Text = "Ostatnia modyfikacja";
		// 
		// textBoxDataModyfikacji
		// 
		textBoxDataModyfikacji.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		tableLayoutPanel1.SetColumnSpan(textBoxDataModyfikacji, 2);
		textBoxDataModyfikacji.Location = new Point(131, 61);
		textBoxDataModyfikacji.Name = "textBoxDataModyfikacji";
		textBoxDataModyfikacji.ReadOnly = true;
		textBoxDataModyfikacji.Size = new Size(460, 23);
		textBoxDataModyfikacji.TabIndex = 20;
		// 
		// comboBoxPlik
		// 
		comboBoxPlik.Anchor = AnchorStyles.Left | AnchorStyles.Right;
		comboBoxPlik.FormattingEnabled = true;
		comboBoxPlik.Location = new Point(131, 3);
		comboBoxPlik.Name = "comboBoxPlik";
		comboBoxPlik.Size = new Size(379, 23);
		comboBoxPlik.TabIndex = 30;
		// 
		// buttonPrzenies
		// 
		buttonPrzenies.Location = new Point(516, 3);
		buttonPrzenies.Name = "buttonPrzenies";
		buttonPrzenies.Size = new Size(75, 23);
		buttonPrzenies.TabIndex = 41;
		buttonPrzenies.Text = "Przenieś bazę";
		buttonPrzenies.UseVisualStyleBackColor = true;
		buttonPrzenies.Click += buttonPrzenies_Click;
		// 
		// label3
		// 
		label3.Anchor = AnchorStyles.Right;
		label3.AutoSize = true;
		label3.Location = new Point(37, 124);
		label3.Name = "label3";
		label3.Size = new Size(88, 15);
		label3.TabIndex = 0;
		label3.Text = "Eksport danych";
		// 
		// flowLayoutPanel2
		// 
		flowLayoutPanel2.AutoSize = true;
		flowLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
		flowLayoutPanel2.Controls.Add(buttonZapiszJSON);
		flowLayoutPanel2.Controls.Add(buttonWczytajJSON);
		flowLayoutPanel2.Location = new Point(128, 116);
		flowLayoutPanel2.Margin = new Padding(0);
		flowLayoutPanel2.Name = "flowLayoutPanel2";
		flowLayoutPanel2.Size = new Size(194, 31);
		flowLayoutPanel2.TabIndex = 40;
		// 
		// buttonZapiszJSON
		// 
		buttonZapiszJSON.AutoSize = true;
		buttonZapiszJSON.Location = new Point(3, 3);
		buttonZapiszJSON.Name = "buttonZapiszJSON";
		buttonZapiszJSON.Size = new Size(93, 25);
		buttonZapiszJSON.TabIndex = 3;
		buttonZapiszJSON.Text = "Zapisz JSON";
		buttonZapiszJSON.UseVisualStyleBackColor = true;
		buttonZapiszJSON.Click += buttonZapiszJSON_Click;
		// 
		// buttonWczytajJSON
		// 
		buttonWczytajJSON.AutoSize = true;
		buttonWczytajJSON.Location = new Point(102, 3);
		buttonWczytajJSON.Name = "buttonWczytajJSON";
		buttonWczytajJSON.Size = new Size(89, 25);
		buttonWczytajJSON.TabIndex = 3;
		buttonWczytajJSON.Text = "Wczytaj JSON";
		buttonWczytajJSON.UseVisualStyleBackColor = true;
		buttonWczytajJSON.Click += buttonWczytajJSON_Click;
		// 
		// saveFileDialogBackup
		// 
		saveFileDialogBackup.Filter = "Kopia zapasowa programu ProFak (*.probak)|*.probak|Wszystkie pliki (*.*)|*.*";
		saveFileDialogBackup.RestoreDirectory = true;
		// 
		// openFileDialogBackup
		// 
		openFileDialogBackup.Filter = "Kopia zapasowa programu ProFak (*.probak)|*.probak|Wszystkie pliki (*.*)|*.*";
		openFileDialogBackup.RestoreDirectory = true;
		// 
		// saveFileDialogJSON
		// 
		saveFileDialogJSON.Filter = "Dane programu ProFak (*.json)|*.json|Wszystkie pliki (*.*)|*.*";
		saveFileDialogJSON.RestoreDirectory = true;
		// 
		// openFileDialogJSON
		// 
		openFileDialogJSON.Filter = "Dane programu ProFak (*.json)|*.json|Wszystkie pliki (*.*)|*.*";
		openFileDialogJSON.RestoreDirectory = true;
		// 
		// BazyDanych
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		Controls.Add(tableLayoutPanel1);
		MaximumSize = new Size(600, 175);
		Name = "BazyDanych";
		Size = new Size(600, 175);
		tableLayoutPanel1.ResumeLayout(false);
		tableLayoutPanel1.PerformLayout();
		flowLayoutPanel1.ResumeLayout(false);
		flowLayoutPanel2.ResumeLayout(false);
		flowLayoutPanel2.PerformLayout();
		ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.TextBox textBoxRozmiar;
	private System.Windows.Forms.ComboBox comboBoxPlik;
	private ButtonDPI buttonUtworzKopie;
	private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
	private ButtonDPI buttonPrzywrocKopie;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.Label label5;
	private System.Windows.Forms.TextBox textBoxDataModyfikacji;
	private System.Windows.Forms.SaveFileDialog saveFileDialogBackup;
	private System.Windows.Forms.OpenFileDialog openFileDialogBackup;
	private ButtonDPI buttonPrzenies;
	private Label label3;
	private FlowLayoutPanel flowLayoutPanel2;
	private ButtonDPI buttonZapiszJSON;
	private ButtonDPI buttonWczytajJSON;
	private SaveFileDialog saveFileDialogJSON;
	private OpenFileDialog openFileDialogJSON;
}
