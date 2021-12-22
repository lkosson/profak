
namespace ProFak.UI
{
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.buttonUtworzKopie = new System.Windows.Forms.Button();
			this.buttonPrzywrocKopie = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBoxRozmiar = new System.Windows.Forms.TextBox();
			this.comboBoxPlik = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.textBoxDataModyfikacji = new System.Windows.Forms.TextBox();
			this.saveFileDialogBackup = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialogBackup = new System.Windows.Forms.OpenFileDialog();
			this.buttonPrzenies = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 3);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.textBoxRozmiar, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.textBoxDataModyfikacji, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.comboBoxPlik, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.buttonPrzenies, 2, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(594, 157);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.buttonUtworzKopie);
			this.flowLayoutPanel1.Controls.Add(this.buttonPrzywrocKopie);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(128, 87);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(162, 29);
			this.flowLayoutPanel1.TabIndex = 40;
			// 
			// buttonUtworzKopie
			// 
			this.buttonUtworzKopie.Location = new System.Drawing.Point(3, 3);
			this.buttonUtworzKopie.Name = "buttonUtworzKopie";
			this.buttonUtworzKopie.Size = new System.Drawing.Size(75, 23);
			this.buttonUtworzKopie.TabIndex = 3;
			this.buttonUtworzKopie.Text = "Utwórz";
			this.buttonUtworzKopie.UseVisualStyleBackColor = true;
			this.buttonUtworzKopie.Click += new System.EventHandler(this.buttonUtworzKopie_Click);
			// 
			// buttonPrzywrocKopie
			// 
			this.buttonPrzywrocKopie.Location = new System.Drawing.Point(84, 3);
			this.buttonPrzywrocKopie.Name = "buttonPrzywrocKopie";
			this.buttonPrzywrocKopie.Size = new System.Drawing.Size(75, 23);
			this.buttonPrzywrocKopie.TabIndex = 3;
			this.buttonPrzywrocKopie.Text = "Przywróć";
			this.buttonPrzywrocKopie.UseVisualStyleBackColor = true;
			this.buttonPrzywrocKopie.Click += new System.EventHandler(this.buttonPrzywrocKopie_Click);
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(30, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "Plik bazy danych";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(48, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 15);
			this.label2.TabIndex = 0;
			this.label2.Text = "Rozmiar bazy";
			// 
			// textBoxRozmiar
			// 
			this.textBoxRozmiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.textBoxRozmiar, 2);
			this.textBoxRozmiar.Location = new System.Drawing.Point(131, 32);
			this.textBoxRozmiar.Name = "textBoxRozmiar";
			this.textBoxRozmiar.ReadOnly = true;
			this.textBoxRozmiar.Size = new System.Drawing.Size(460, 23);
			this.textBoxRozmiar.TabIndex = 10;
			// 
			// comboBoxPlik
			// 
			this.comboBoxPlik.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxPlik.FormattingEnabled = true;
			this.comboBoxPlik.Location = new System.Drawing.Point(131, 3);
			this.comboBoxPlik.Name = "comboBoxPlik";
			this.comboBoxPlik.Size = new System.Drawing.Size(379, 23);
			this.comboBoxPlik.TabIndex = 30;
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 94);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(122, 15);
			this.label4.TabIndex = 0;
			this.label4.Text = "Kopia bezpieczeństwa";
			// 
			// label5
			// 
			this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 65);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(119, 15);
			this.label5.TabIndex = 0;
			this.label5.Text = "Ostatnia modyfikacja";
			// 
			// textBoxDataModyfikacji
			// 
			this.textBoxDataModyfikacji.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.textBoxDataModyfikacji, 2);
			this.textBoxDataModyfikacji.Location = new System.Drawing.Point(131, 61);
			this.textBoxDataModyfikacji.Name = "textBoxDataModyfikacji";
			this.textBoxDataModyfikacji.ReadOnly = true;
			this.textBoxDataModyfikacji.Size = new System.Drawing.Size(460, 23);
			this.textBoxDataModyfikacji.TabIndex = 20;
			// 
			// saveFileDialogBackup
			// 
			this.saveFileDialogBackup.Filter = "Kopia zapasowa programu ProFak (*.probak)|*.probak|Wszystkie pliki (*.*)|*.*";
			this.saveFileDialogBackup.RestoreDirectory = true;
			// 
			// openFileDialogBackup
			// 
			this.openFileDialogBackup.Filter = "Kopia zapasowa programu ProFak (*.probak)|*.probak|Wszystkie pliki (*.*)|*.*";
			this.openFileDialogBackup.RestoreDirectory = true;
			// 
			// buttonPrzenies
			// 
			this.buttonPrzenies.Location = new System.Drawing.Point(516, 3);
			this.buttonPrzenies.Name = "buttonPrzenies";
			this.buttonPrzenies.Size = new System.Drawing.Size(75, 23);
			this.buttonPrzenies.TabIndex = 41;
			this.buttonPrzenies.Text = "Przenieś bazę";
			this.buttonPrzenies.UseVisualStyleBackColor = true;
			this.buttonPrzenies.Click += new System.EventHandler(this.buttonPrzenies_Click);
			// 
			// BazyDanych
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tableLayoutPanel1);
			this.MaximumSize = new System.Drawing.Size(600, 175);
			this.Name = "BazyDanych";
			this.Size = new System.Drawing.Size(600, 175);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxRozmiar;
		private System.Windows.Forms.ComboBox comboBoxPlik;
		private System.Windows.Forms.Button buttonUtworzKopie;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button buttonPrzywrocKopie;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxDataModyfikacji;
		private System.Windows.Forms.SaveFileDialog saveFileDialogBackup;
		private System.Windows.Forms.OpenFileDialog openFileDialogBackup;
		private System.Windows.Forms.Button buttonPrzenies;
	}
}
