namespace ProFak.UI.Kontrahenci;

partial class DostepKSeFEdytor
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
		var resources = new System.ComponentModel.ComponentResourceManager(typeof(DostepKSeFEdytor));
		tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		label1 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		buttonPobierzXML = new ButtonDPI();
		textBoxNIP = new System.Windows.Forms.TextBox();
		label5 = new System.Windows.Forms.Label();
		buttonWskazXML = new ButtonDPI();
		label3 = new System.Windows.Forms.Label();
		linkEPUAP = new System.Windows.Forms.LinkLabel();
		label6 = new System.Windows.Forms.Label();
		buttonZamknij = new ButtonDPI();
		label4 = new System.Windows.Forms.Label();
		tableLayoutPanel1.SuspendLayout();
		SuspendLayout();
		// 
		// tableLayoutPanel1
		// 
		tableLayoutPanel1.ColumnCount = 1;
		tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
		tableLayoutPanel1.Controls.Add(label1, 0, 0);
		tableLayoutPanel1.Controls.Add(buttonWskazXML, 0, 9);
		tableLayoutPanel1.Controls.Add(label3, 0, 8);
		tableLayoutPanel1.Controls.Add(buttonZamknij, 0, 11);
		tableLayoutPanel1.Controls.Add(label4, 0, 1);
		tableLayoutPanel1.Controls.Add(textBoxNIP, 0, 3);
		tableLayoutPanel1.Controls.Add(label5, 0, 2);
		tableLayoutPanel1.Controls.Add(label2, 0, 4);
		tableLayoutPanel1.Controls.Add(buttonPobierzXML, 0, 5);
		tableLayoutPanel1.Controls.Add(label6, 0, 6);
		tableLayoutPanel1.Controls.Add(linkEPUAP, 0, 7);
		tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		tableLayoutPanel1.Name = "tableLayoutPanel1";
		tableLayoutPanel1.RowCount = 12;
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
		tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
		tableLayoutPanel1.Size = new System.Drawing.Size(634, 370);
		tableLayoutPanel1.TabIndex = 0;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Location = new System.Drawing.Point(3, 0);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(628, 45);
		label1.TabIndex = 0;
		label1.Text = resources.GetString("label1.Text");
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Location = new System.Drawing.Point(3, 134);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(595, 45);
		label2.TabIndex = 0;
		label2.Text = resources.GetString("label2.Text");
		// 
		// buttonPobierzXML
		// 
		buttonPobierzXML.AutoSize = true;
		buttonPobierzXML.Location = new System.Drawing.Point(3, 182);
		buttonPobierzXML.Name = "buttonPobierzXML";
		buttonPobierzXML.Size = new System.Drawing.Size(146, 25);
		buttonPobierzXML.TabIndex = 3;
		buttonPobierzXML.Text = "Pobierz XML do podpisu";
		buttonPobierzXML.UseVisualStyleBackColor = true;
		buttonPobierzXML.Click += buttonPobierzXML_Click;
		// 
		// textBoxNIP
		// 
		textBoxNIP.Location = new System.Drawing.Point(3, 108);
		textBoxNIP.Name = "textBoxNIP";
		textBoxNIP.ReadOnly = true;
		textBoxNIP.Size = new System.Drawing.Size(190, 23);
		textBoxNIP.TabIndex = 1;
		// 
		// label5
		// 
		label5.AutoSize = true;
		label5.Location = new System.Drawing.Point(3, 75);
		label5.Name = "label5";
		label5.Size = new System.Drawing.Size(607, 30);
		label5.TabIndex = 0;
		label5.Text = "Jeśli jeszcze nie korzystałeś z KSeF lub nie masz tokena, w pierwszej kolejności zweryfikuj, czy podany niżej NIP jest prawidłowy:";
		// 
		// buttonWskazXML
		// 
		buttonWskazXML.AutoSize = true;
		buttonWskazXML.Location = new System.Drawing.Point(3, 288);
		buttonWskazXML.Name = "buttonWskazXML";
		buttonWskazXML.Size = new System.Drawing.Size(143, 25);
		buttonWskazXML.TabIndex = 5;
		buttonWskazXML.Text = "Wczytaj podpisany XML";
		buttonWskazXML.UseVisualStyleBackColor = true;
		buttonWskazXML.Click += buttonWskazXML_Click;
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Location = new System.Drawing.Point(3, 255);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(627, 30);
		label3.TabIndex = 4;
		label3.Text = resources.GetString("label3.Text");
		// 
		// linkEPUAP
		// 
		linkEPUAP.AutoSize = true;
		linkEPUAP.Location = new System.Drawing.Point(3, 240);
		linkEPUAP.Name = "linkEPUAP";
		linkEPUAP.Size = new System.Drawing.Size(211, 15);
		linkEPUAP.TabIndex = 2;
		linkEPUAP.TabStop = true;
		linkEPUAP.Text = "Podpisz dokument Profilem Zaufanym";
		linkEPUAP.LinkClicked += linkEPUAP_LinkClicked;
		// 
		// label6
		// 
		label6.AutoSize = true;
		label6.Location = new System.Drawing.Point(3, 210);
		label6.Name = "label6";
		label6.Size = new System.Drawing.Size(581, 30);
		label6.TabIndex = 4;
		label6.Text = "Jak już masz zapisany plik, kliknij na poniższy odnośnik, aby przejść do https://moj.gov.pl/ w celu podpisania dokumentu. Możesz też podpisać dokument podpisem certyfikowanym.";
		// 
		// buttonZamknij
		// 
		buttonZamknij.AutoSize = true;
		buttonZamknij.DialogResult = System.Windows.Forms.DialogResult.OK;
		buttonZamknij.Location = new System.Drawing.Point(3, 342);
		buttonZamknij.Name = "buttonZamknij";
		buttonZamknij.Size = new System.Drawing.Size(75, 25);
		buttonZamknij.TabIndex = 5;
		buttonZamknij.Text = "Anuluj";
		buttonZamknij.UseVisualStyleBackColor = true;
		// 
		// label4
		// 
		label4.AutoSize = true;
		label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
		label4.Location = new System.Drawing.Point(3, 45);
		label4.Name = "label4";
		label4.Size = new System.Drawing.Size(615, 30);
		label4.TabIndex = 0;
		label4.Text = "Jeśli masz wątpliwości co do bezpieczeństwa swoich danych, tego jak ProFak będzie je przetwarzał i jak działa KSeF, zamknij to okno.";
		// 
		// DostepKSeFEdytor
		// 
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		Controls.Add(tableLayoutPanel1);
		Name = "DostepKSeFEdytor";
		Size = new System.Drawing.Size(634, 370);
		tableLayoutPanel1.ResumeLayout(false);
		tableLayoutPanel1.PerformLayout();
		ResumeLayout(false);
	}

	#endregion

	private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.LinkLabel linkEPUAP;
	private ButtonDPI buttonPobierzXML;
	private System.Windows.Forms.Label label3;
	private ButtonDPI buttonWskazXML;
	private System.Windows.Forms.TextBox textBoxNIP;
	private System.Windows.Forms.Label label5;
	private System.Windows.Forms.Label label6;
	private ButtonDPI buttonZamknij;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label label4;
}
