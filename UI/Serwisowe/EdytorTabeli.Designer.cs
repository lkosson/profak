
namespace ProFak.UI
{
	partial class EdytorTabeli
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
			this.groupBoxWynik = new System.Windows.Forms.GroupBox();
			this.dataGridViewWynik = new System.Windows.Forms.DataGridView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboBoxTabela = new System.Windows.Forms.ComboBox();
			this.textBoxStatus = new System.Windows.Forms.TextBox();
			this.buttonPokaz = new System.Windows.Forms.Button();
			this.groupBoxWynik.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewWynik)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxWynik
			// 
			this.groupBoxWynik.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxWynik.Controls.Add(this.dataGridViewWynik);
			this.groupBoxWynik.Location = new System.Drawing.Point(3, 66);
			this.groupBoxWynik.Name = "groupBoxWynik";
			this.groupBoxWynik.Size = new System.Drawing.Size(627, 213);
			this.groupBoxWynik.TabIndex = 3;
			this.groupBoxWynik.TabStop = false;
			this.groupBoxWynik.Text = "Wynik";
			// 
			// dataGridViewWynik
			// 
			this.dataGridViewWynik.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewWynik.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewWynik.Location = new System.Drawing.Point(3, 19);
			this.dataGridViewWynik.Name = "dataGridViewWynik";
			this.dataGridViewWynik.RowTemplate.Height = 25;
			this.dataGridViewWynik.Size = new System.Drawing.Size(621, 191);
			this.dataGridViewWynik.TabIndex = 0;
			this.dataGridViewWynik.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewWynik_CellEndEdit);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.comboBoxTabela);
			this.groupBox1.Controls.Add(this.textBoxStatus);
			this.groupBox1.Controls.Add(this.buttonPokaz);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(627, 57);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Zakres danych";
			// 
			// comboBoxTabela
			// 
			this.comboBoxTabela.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBoxTabela.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxTabela.FormattingEnabled = true;
			this.comboBoxTabela.Location = new System.Drawing.Point(6, 22);
			this.comboBoxTabela.Name = "comboBoxTabela";
			this.comboBoxTabela.Size = new System.Drawing.Size(231, 23);
			this.comboBoxTabela.TabIndex = 3;
			// 
			// textBoxStatus
			// 
			this.textBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxStatus.Location = new System.Drawing.Point(344, 22);
			this.textBoxStatus.Name = "textBoxStatus";
			this.textBoxStatus.ReadOnly = true;
			this.textBoxStatus.Size = new System.Drawing.Size(277, 23);
			this.textBoxStatus.TabIndex = 2;
			// 
			// buttonPokaz
			// 
			this.buttonPokaz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonPokaz.Location = new System.Drawing.Point(243, 22);
			this.buttonPokaz.Name = "buttonPokaz";
			this.buttonPokaz.Size = new System.Drawing.Size(95, 23);
			this.buttonPokaz.TabIndex = 1;
			this.buttonPokaz.Text = "Wczytaj";
			this.buttonPokaz.UseVisualStyleBackColor = true;
			this.buttonPokaz.Click += new System.EventHandler(this.buttonUruchom_Click);
			// 
			// EdytorTabeli
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBoxWynik);
			this.Controls.Add(this.groupBox1);
			this.Name = "EdytorTabeli";
			this.Size = new System.Drawing.Size(633, 282);
			this.groupBoxWynik.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewWynik)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBoxWynik;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button buttonPokaz;
		private System.Windows.Forms.TextBox textBoxStatus;
		private System.Windows.Forms.DataGridView dataGridViewWynik;
		private System.Windows.Forms.ComboBox comboBoxTabela;
	}
}
