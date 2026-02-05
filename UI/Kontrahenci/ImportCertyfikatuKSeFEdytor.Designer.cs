namespace ProFak.UI.Kontrahenci
{
	partial class ImportCertyfikatuKSeFEdytor
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
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			label1 = new System.Windows.Forms.Label();
			linkLabelAplikacjaPodatnika = new System.Windows.Forms.LinkLabel();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			textBoxCertyfikat = new System.Windows.Forms.TextBox();
			textBoxKlucz = new System.Windows.Forms.TextBox();
			textBoxHaslo = new System.Windows.Forms.TextBox();
			buttonCertyfikat = new System.Windows.Forms.Button();
			buttonKlucz = new System.Windows.Forms.Button();
			buttonZapisz = new System.Windows.Forms.Button();
			tableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 3;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel1.Controls.Add(label1, 0, 0);
			tableLayoutPanel1.Controls.Add(linkLabelAplikacjaPodatnika, 0, 1);
			tableLayoutPanel1.Controls.Add(label2, 0, 2);
			tableLayoutPanel1.Controls.Add(label3, 0, 3);
			tableLayoutPanel1.Controls.Add(label4, 0, 4);
			tableLayoutPanel1.Controls.Add(label5, 0, 5);
			tableLayoutPanel1.Controls.Add(textBoxCertyfikat, 1, 3);
			tableLayoutPanel1.Controls.Add(textBoxKlucz, 1, 4);
			tableLayoutPanel1.Controls.Add(textBoxHaslo, 1, 5);
			tableLayoutPanel1.Controls.Add(buttonCertyfikat, 2, 3);
			tableLayoutPanel1.Controls.Add(buttonKlucz, 2, 4);
			tableLayoutPanel1.Controls.Add(buttonZapisz, 0, 6);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 7;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.Size = new System.Drawing.Size(567, 212);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			tableLayoutPanel1.SetColumnSpan(label1, 3);
			label1.Location = new System.Drawing.Point(3, 0);
			label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(550, 30);
			label1.TabIndex = 0;
			label1.Text = "Aby nadać aplikacji dostęp do KSeF przy użyciu certyfikatu, zaloguj się do Aplikacji Podatnika używając poniższego odnośnika, a następnie wygeneruj nowy certyfikat przeznaczony do uwierzytelniania.";
			// 
			// linkLabelAplikacjaPodatnika
			// 
			linkLabelAplikacjaPodatnika.AutoSize = true;
			tableLayoutPanel1.SetColumnSpan(linkLabelAplikacjaPodatnika, 3);
			linkLabelAplikacjaPodatnika.Location = new System.Drawing.Point(3, 38);
			linkLabelAplikacjaPodatnika.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
			linkLabelAplikacjaPodatnika.Name = "linkLabelAplikacjaPodatnika";
			linkLabelAplikacjaPodatnika.Size = new System.Drawing.Size(111, 15);
			linkLabelAplikacjaPodatnika.TabIndex = 1;
			linkLabelAplikacjaPodatnika.TabStop = true;
			linkLabelAplikacjaPodatnika.Text = "Aplikacja Podatnika";
			linkLabelAplikacjaPodatnika.LinkClicked += linkLabelAplikacjaPodatnika_LinkClicked;
			// 
			// label2
			// 
			label2.AutoSize = true;
			tableLayoutPanel1.SetColumnSpan(label2, 3);
			label2.Location = new System.Drawing.Point(3, 61);
			label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 8);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(545, 15);
			label2.TabIndex = 2;
			label2.Text = "Załaduj wygenerowany certyfikat i klucz prywatny, a następnie wprowadź hasło do klucza prywatnego.";
			// 
			// label3
			// 
			label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(3, 92);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(85, 15);
			label3.TabIndex = 3;
			label3.Text = "Plik certyfikatu";
			// 
			// label4
			// 
			label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(3, 123);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(128, 15);
			label4.TabIndex = 3;
			label4.Text = "Plik klucza prywatnego";
			// 
			// label5
			// 
			label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(3, 153);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(139, 15);
			label5.TabIndex = 3;
			label5.Text = "Hasło klucza prywatnego";
			// 
			// textBoxCertyfikat
			// 
			textBoxCertyfikat.AcceptsReturn = true;
			textBoxCertyfikat.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxCertyfikat.Location = new System.Drawing.Point(148, 88);
			textBoxCertyfikat.Name = "textBoxCertyfikat";
			textBoxCertyfikat.Size = new System.Drawing.Size(384, 23);
			textBoxCertyfikat.TabIndex = 4;
			// 
			// textBoxKlucz
			// 
			textBoxKlucz.AcceptsReturn = true;
			textBoxKlucz.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxKlucz.Location = new System.Drawing.Point(148, 119);
			textBoxKlucz.Name = "textBoxKlucz";
			textBoxKlucz.Size = new System.Drawing.Size(384, 23);
			textBoxKlucz.TabIndex = 4;
			// 
			// textBoxHaslo
			// 
			textBoxHaslo.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel1.SetColumnSpan(textBoxHaslo, 2);
			textBoxHaslo.Location = new System.Drawing.Point(148, 149);
			textBoxHaslo.Name = "textBoxHaslo";
			textBoxHaslo.Size = new System.Drawing.Size(416, 23);
			textBoxHaslo.TabIndex = 4;
			// 
			// buttonCertyfikat
			// 
			buttonCertyfikat.Anchor = System.Windows.Forms.AnchorStyles.Left;
			buttonCertyfikat.AutoSize = true;
			buttonCertyfikat.Location = new System.Drawing.Point(538, 87);
			buttonCertyfikat.Name = "buttonCertyfikat";
			buttonCertyfikat.Size = new System.Drawing.Size(26, 25);
			buttonCertyfikat.TabIndex = 5;
			buttonCertyfikat.Text = "...";
			buttonCertyfikat.UseVisualStyleBackColor = true;
			buttonCertyfikat.Click += buttonCertyfikat_Click;
			// 
			// buttonKlucz
			// 
			buttonKlucz.Anchor = System.Windows.Forms.AnchorStyles.Left;
			buttonKlucz.AutoSize = true;
			buttonKlucz.Location = new System.Drawing.Point(538, 118);
			buttonKlucz.Name = "buttonKlucz";
			buttonKlucz.Size = new System.Drawing.Size(26, 25);
			buttonKlucz.TabIndex = 5;
			buttonKlucz.Text = "...";
			buttonKlucz.UseVisualStyleBackColor = true;
			buttonKlucz.Click += buttonKlucz_Click;
			// 
			// buttonZapisz
			// 
			buttonZapisz.AutoSize = true;
			buttonZapisz.Location = new System.Drawing.Point(3, 178);
			buttonZapisz.Name = "buttonZapisz";
			buttonZapisz.Size = new System.Drawing.Size(59, 25);
			buttonZapisz.TabIndex = 5;
			buttonZapisz.Text = "Zapisz";
			buttonZapisz.UseVisualStyleBackColor = true;
			buttonZapisz.Click += buttonZapisz_Click;
			// 
			// ImportCertyfikatuKSeFEdytor
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(tableLayoutPanel1);
			Name = "ImportCertyfikatuKSeFEdytor";
			Size = new System.Drawing.Size(567, 212);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel linkLabelAplikacjaPodatnika;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBoxCertyfikat;
		private System.Windows.Forms.TextBox textBoxKlucz;
		private System.Windows.Forms.TextBox textBoxHaslo;
		private System.Windows.Forms.Button buttonCertyfikat;
		private System.Windows.Forms.Button buttonKlucz;
		private System.Windows.Forms.Button buttonZapisz;
	}
}
