
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
			this.dataGridViewWynik = new Spis();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.comboBoxTabela = new System.Windows.Forms.ComboBox();
			this.textBoxStatus = new System.Windows.Forms.TextBox();
			this.buttonPokaz = new ButtonDPI();
			this.numericUpDownIDOd = new NumericUpDownDPI();
			this.numericUpDownIDDo = new NumericUpDownDPI();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBoxWynik.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewWynik)).BeginInit();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownIDOd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownIDDo)).BeginInit();
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
			this.groupBoxWynik.Size = new System.Drawing.Size(777, 213);
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
			this.dataGridViewWynik.Size = new System.Drawing.Size(771, 191);
			this.dataGridViewWynik.TabIndex = 0;
			this.dataGridViewWynik.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewWynik_CellEndEdit);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.numericUpDownIDDo);
			this.groupBox1.Controls.Add(this.numericUpDownIDOd);
			this.groupBox1.Controls.Add(this.comboBoxTabela);
			this.groupBox1.Controls.Add(this.textBoxStatus);
			this.groupBox1.Controls.Add(this.buttonPokaz);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(777, 57);
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
			this.comboBoxTabela.Size = new System.Drawing.Size(201, 23);
			this.comboBoxTabela.TabIndex = 3;
			// 
			// textBoxStatus
			// 
			this.textBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxStatus.Location = new System.Drawing.Point(546, 24);
			this.textBoxStatus.Name = "textBoxStatus";
			this.textBoxStatus.ReadOnly = true;
			this.textBoxStatus.Size = new System.Drawing.Size(225, 23);
			this.textBoxStatus.TabIndex = 2;
			// 
			// buttonPokaz
			// 
			this.buttonPokaz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonPokaz.Location = new System.Drawing.Point(445, 23);
			this.buttonPokaz.Name = "buttonPokaz";
			this.buttonPokaz.Size = new System.Drawing.Size(95, 23);
			this.buttonPokaz.TabIndex = 1;
			this.buttonPokaz.Text = "Wczytaj";
			this.buttonPokaz.UseVisualStyleBackColor = true;
			this.buttonPokaz.Click += new System.EventHandler(this.buttonUruchom_Click);
			// 
			// numericUpDownIDOd
			// 
			this.numericUpDownIDOd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownIDOd.Location = new System.Drawing.Point(245, 22);
			this.numericUpDownIDOd.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDownIDOd.Name = "numericUpDownIDOd";
			this.numericUpDownIDOd.Size = new System.Drawing.Size(81, 23);
			this.numericUpDownIDOd.TabIndex = 4;
			this.numericUpDownIDOd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// numericUpDownIDDo
			// 
			this.numericUpDownIDDo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numericUpDownIDDo.Location = new System.Drawing.Point(354, 23);
			this.numericUpDownIDDo.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.numericUpDownIDDo.Name = "numericUpDownIDDo";
			this.numericUpDownIDDo.Size = new System.Drawing.Size(85, 23);
			this.numericUpDownIDDo.TabIndex = 4;
			this.numericUpDownIDDo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.numericUpDownIDDo.Value = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(213, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(26, 15);
			this.label1.TabIndex = 5;
			this.label1.Text = "ID=";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(332, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 15);
			this.label2.TabIndex = 5;
			this.label2.Text = "...";
			// 
			// EdytorTabeli
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBoxWynik);
			this.Controls.Add(this.groupBox1);
			this.Name = "EdytorTabeli";
			this.Size = new System.Drawing.Size(783, 282);
			this.groupBoxWynik.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewWynik)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownIDOd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownIDDo)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBoxWynik;
		private System.Windows.Forms.GroupBox groupBox1;
		private ButtonDPI buttonPokaz;
		private System.Windows.Forms.TextBox textBoxStatus;
		private Spis dataGridViewWynik;
		private System.Windows.Forms.ComboBox comboBoxTabela;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private NumericUpDownDPI numericUpDownIDDo;
		private NumericUpDownDPI numericUpDownIDOd;
	}
}
