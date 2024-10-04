
namespace ProFak.UI
{
	partial class EkranSQL
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
			this.textBoxSQL = new System.Windows.Forms.TextBox();
			this.groupBoxWynik = new System.Windows.Forms.GroupBox();
			this.dataGridViewWynik = new System.Windows.Forms.DataGridView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxStatus = new System.Windows.Forms.TextBox();
			this.buttonUruchom = new ButtonDPI();
			this.groupBoxWynik.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewWynik)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxSQL
			// 
			this.textBoxSQL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxSQL.Location = new System.Drawing.Point(6, 22);
			this.textBoxSQL.Multiline = true;
			this.textBoxSQL.Name = "textBoxSQL";
			this.textBoxSQL.Size = new System.Drawing.Size(379, 51);
			this.textBoxSQL.TabIndex = 0;
			this.textBoxSQL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSQL_KeyDown);
			// 
			// groupBoxWynik
			// 
			this.groupBoxWynik.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxWynik.Controls.Add(this.dataGridViewWynik);
			this.groupBoxWynik.Location = new System.Drawing.Point(3, 94);
			this.groupBoxWynik.Name = "groupBoxWynik";
			this.groupBoxWynik.Size = new System.Drawing.Size(560, 185);
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
			this.dataGridViewWynik.Size = new System.Drawing.Size(554, 163);
			this.dataGridViewWynik.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.textBoxStatus);
			this.groupBox1.Controls.Add(this.buttonUruchom);
			this.groupBox1.Controls.Add(this.textBoxSQL);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(560, 85);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Treść polecenia";
			// 
			// textBoxStatus
			// 
			this.textBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxStatus.Location = new System.Drawing.Point(391, 50);
			this.textBoxStatus.Name = "textBoxStatus";
			this.textBoxStatus.ReadOnly = true;
			this.textBoxStatus.Size = new System.Drawing.Size(163, 23);
			this.textBoxStatus.TabIndex = 2;
			// 
			// buttonUruchom
			// 
			this.buttonUruchom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonUruchom.Location = new System.Drawing.Point(391, 21);
			this.buttonUruchom.Name = "buttonUruchom";
			this.buttonUruchom.Size = new System.Drawing.Size(163, 23);
			this.buttonUruchom.TabIndex = 1;
			this.buttonUruchom.Text = "Uruchom";
			this.buttonUruchom.UseVisualStyleBackColor = true;
			this.buttonUruchom.Click += new System.EventHandler(this.buttonUruchom_Click);
			// 
			// EkranSQL
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBoxWynik);
			this.Controls.Add(this.groupBox1);
			this.Name = "EkranSQL";
			this.Size = new System.Drawing.Size(566, 282);
			this.groupBoxWynik.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewWynik)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxSQL;
		private System.Windows.Forms.GroupBox groupBoxWynik;
		private System.Windows.Forms.GroupBox groupBox1;
		private ButtonDPI buttonUruchom;
		private System.Windows.Forms.TextBox textBoxStatus;
		private System.Windows.Forms.DataGridView dataGridViewWynik;
	}
}
