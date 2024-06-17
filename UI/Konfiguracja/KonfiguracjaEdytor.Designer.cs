
namespace ProFak.UI
{
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
			tabControl = new System.Windows.Forms.TabControl();
			tabPageEMail = new System.Windows.Forms.TabPage();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			label2 = new System.Windows.Forms.Label();
			textBoxSMTPSerwer = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			textBoxEMailTresc = new System.Windows.Forms.TextBox();
			textBoxSMTPLogin = new System.Windows.Forms.TextBox();
			numericUpDownSMTPort = new System.Windows.Forms.NumericUpDown();
			textBoxEMailNadawca = new System.Windows.Forms.TextBox();
			textBoxSMTPHaslo = new System.Windows.Forms.TextBox();
			textBoxEMailTemat = new System.Windows.Forms.TextBox();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			label9 = new System.Windows.Forms.Label();
			linkLabelTrescPomoc = new System.Windows.Forms.LinkLabel();
			tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			backgroundWorkerSprawdzMF = new System.ComponentModel.BackgroundWorker();
			backgroundWorkerPobierzGUS = new System.ComponentModel.BackgroundWorker();
			tabControl.SuspendLayout();
			tabPageEMail.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownSMTPort).BeginInit();
			flowLayoutPanel1.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			SuspendLayout();
			// 
			// tabControl
			// 
			tableLayoutPanel2.SetColumnSpan(tabControl, 2);
			tabControl.Controls.Add(tabPageEMail);
			tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			tabControl.Location = new System.Drawing.Point(3, 3);
			tabControl.Name = "tabControl";
			tabControl.SelectedIndex = 0;
			tabControl.Size = new System.Drawing.Size(794, 419);
			tabControl.TabIndex = 2;
			// 
			// tabPageEMail
			// 
			tabPageEMail.Controls.Add(tableLayoutPanel1);
			tabPageEMail.Location = new System.Drawing.Point(4, 24);
			tabPageEMail.Name = "tabPageEMail";
			tabPageEMail.Padding = new System.Windows.Forms.Padding(3);
			tabPageEMail.Size = new System.Drawing.Size(786, 391);
			tabPageEMail.TabIndex = 0;
			tabPageEMail.Text = "Konfiguracja e-mail";
			tabPageEMail.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
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
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 10;
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
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel1.Size = new System.Drawing.Size(780, 385);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// label2
			// 
			label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(34, 7);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(75, 15);
			label2.TabIndex = 2;
			label2.Text = "Serwer SMTP";
			// 
			// textBoxSMTPSerwer
			// 
			textBoxSMTPSerwer.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxSMTPSerwer.Location = new System.Drawing.Point(115, 3);
			textBoxSMTPSerwer.Name = "textBoxSMTPSerwer";
			textBoxSMTPSerwer.Size = new System.Drawing.Size(662, 23);
			textBoxSMTPSerwer.TabIndex = 0;
			// 
			// label3
			// 
			label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(80, 36);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(29, 15);
			label3.TabIndex = 2;
			label3.Text = "Port";
			// 
			// label4
			// 
			label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(72, 65);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(37, 15);
			label4.TabIndex = 2;
			label4.Text = "Login";
			// 
			// label5
			// 
			label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(72, 94);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(37, 15);
			label5.TabIndex = 2;
			label5.Text = "Hasło";
			// 
			// label7
			// 
			label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(22, 123);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(87, 15);
			label7.TabIndex = 2;
			label7.Text = "Adres nadawcy";
			// 
			// label8
			// 
			label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(3, 152);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(106, 15);
			label8.TabIndex = 2;
			label8.Text = "Temat wiadomości";
			// 
			// textBoxEMailTresc
			// 
			textBoxEMailTresc.AcceptsReturn = true;
			textBoxEMailTresc.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxEMailTresc.Location = new System.Drawing.Point(115, 177);
			textBoxEMailTresc.Multiline = true;
			textBoxEMailTresc.Name = "textBoxEMailTresc";
			textBoxEMailTresc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			textBoxEMailTresc.Size = new System.Drawing.Size(662, 173);
			textBoxEMailTresc.TabIndex = 6;
			// 
			// textBoxSMTPLogin
			// 
			textBoxSMTPLogin.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxSMTPLogin.Location = new System.Drawing.Point(115, 61);
			textBoxSMTPLogin.Name = "textBoxSMTPLogin";
			textBoxSMTPLogin.Size = new System.Drawing.Size(662, 23);
			textBoxSMTPLogin.TabIndex = 2;
			// 
			// numericUpDownSMTPort
			// 
			numericUpDownSMTPort.Location = new System.Drawing.Point(115, 32);
			numericUpDownSMTPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
			numericUpDownSMTPort.Name = "numericUpDownSMTPort";
			numericUpDownSMTPort.Size = new System.Drawing.Size(72, 23);
			numericUpDownSMTPort.TabIndex = 1;
			numericUpDownSMTPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// textBoxEMailNadawca
			// 
			textBoxEMailNadawca.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxEMailNadawca.Location = new System.Drawing.Point(115, 119);
			textBoxEMailNadawca.Name = "textBoxEMailNadawca";
			textBoxEMailNadawca.Size = new System.Drawing.Size(662, 23);
			textBoxEMailNadawca.TabIndex = 4;
			// 
			// textBoxSMTPHaslo
			// 
			textBoxSMTPHaslo.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxSMTPHaslo.Location = new System.Drawing.Point(115, 90);
			textBoxSMTPHaslo.Name = "textBoxSMTPHaslo";
			textBoxSMTPHaslo.Size = new System.Drawing.Size(662, 23);
			textBoxSMTPHaslo.TabIndex = 3;
			textBoxSMTPHaslo.UseSystemPasswordChar = true;
			// 
			// textBoxEMailTemat
			// 
			textBoxEMailTemat.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxEMailTemat.Location = new System.Drawing.Point(115, 148);
			textBoxEMailTemat.Name = "textBoxEMailTemat";
			textBoxEMailTemat.Size = new System.Drawing.Size(662, 23);
			textBoxEMailTemat.TabIndex = 5;
			// 
			// flowLayoutPanel1
			// 
			flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			flowLayoutPanel1.AutoSize = true;
			flowLayoutPanel1.Controls.Add(label9);
			flowLayoutPanel1.Controls.Add(linkLabelTrescPomoc);
			flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			flowLayoutPanel1.Location = new System.Drawing.Point(6, 248);
			flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(106, 30);
			flowLayoutPanel1.TabIndex = 7;
			flowLayoutPanel1.WrapContents = false;
			// 
			// label9
			// 
			label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(3, 0);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(100, 15);
			label9.TabIndex = 2;
			label9.Text = "Treść wiadomości";
			// 
			// linkLabelTrescPomoc
			// 
			linkLabelTrescPomoc.AutoSize = true;
			linkLabelTrescPomoc.Location = new System.Drawing.Point(3, 15);
			linkLabelTrescPomoc.Name = "linkLabelTrescPomoc";
			linkLabelTrescPomoc.Size = new System.Drawing.Size(20, 15);
			linkLabelTrescPomoc.TabIndex = 3;
			linkLabelTrescPomoc.TabStop = true;
			linkLabelTrescPomoc.Text = "[?]";
			linkLabelTrescPomoc.LinkClicked += linkLabelTrescPomoc_LinkClicked;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 2;
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel2.Controls.Add(tabControl, 0, 0);
			tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 1;
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel2.Size = new System.Drawing.Size(800, 425);
			tableLayoutPanel2.TabIndex = 3;
			// 
			// KonfiguracjaEdytor
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(tableLayoutPanel2);
			MinimumSize = new System.Drawing.Size(800, 425);
			Name = "KonfiguracjaEdytor";
			Size = new System.Drawing.Size(800, 425);
			tabControl.ResumeLayout(false);
			tabPageEMail.ResumeLayout(false);
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownSMTPort).EndInit();
			flowLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.PerformLayout();
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
		private System.Windows.Forms.TextBox textBoxAdresRejestrowy;
		private System.Windows.Forms.TextBox textBoxEMailTresc;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.ComponentModel.BackgroundWorker backgroundWorkerSprawdzMF;
		private System.ComponentModel.BackgroundWorker backgroundWorkerPobierzGUS;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown numericUpDownSMTPort;
		private System.Windows.Forms.TextBox textBoxEMailNadawca;
		private System.Windows.Forms.TextBox textBoxSMTPHaslo;
		private System.Windows.Forms.TextBox textBoxEMailTemat;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.LinkLabel linkLabelTrescPomoc;
	}
}
