
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
			textBoxSQL = new System.Windows.Forms.TextBox();
			groupBoxWynik = new System.Windows.Forms.GroupBox();
			dataGridViewWynik = new System.Windows.Forms.DataGridView();
			groupBox1 = new System.Windows.Forms.GroupBox();
			textBoxStatus = new System.Windows.Forms.TextBox();
			buttonUruchom = new ButtonDPI();
			groupBoxWynik.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridViewWynik).BeginInit();
			groupBox1.SuspendLayout();
			SuspendLayout();
			// 
			// textBoxSQL
			// 
			textBoxSQL.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBoxSQL.Location = new System.Drawing.Point(6, 22);
			textBoxSQL.Multiline = true;
			textBoxSQL.Name = "textBoxSQL";
			textBoxSQL.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxSQL.Size = new System.Drawing.Size(379, 143);
			textBoxSQL.TabIndex = 0;
			textBoxSQL.KeyDown += textBoxSQL_KeyDown;
			// 
			// groupBoxWynik
			// 
			groupBoxWynik.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			groupBoxWynik.Controls.Add(dataGridViewWynik);
			groupBoxWynik.Location = new System.Drawing.Point(3, 186);
			groupBoxWynik.Name = "groupBoxWynik";
			groupBoxWynik.Size = new System.Drawing.Size(560, 93);
			groupBoxWynik.TabIndex = 3;
			groupBoxWynik.TabStop = false;
			groupBoxWynik.Text = "Wynik";
			// 
			// dataGridViewWynik
			// 
			dataGridViewWynik.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewWynik.Dock = System.Windows.Forms.DockStyle.Fill;
			dataGridViewWynik.Location = new System.Drawing.Point(3, 19);
			dataGridViewWynik.Name = "dataGridViewWynik";
			dataGridViewWynik.Size = new System.Drawing.Size(554, 71);
			dataGridViewWynik.TabIndex = 0;
			// 
			// groupBox1
			// 
			groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			groupBox1.Controls.Add(textBoxStatus);
			groupBox1.Controls.Add(buttonUruchom);
			groupBox1.Controls.Add(textBoxSQL);
			groupBox1.Location = new System.Drawing.Point(3, 3);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(560, 177);
			groupBox1.TabIndex = 0;
			groupBox1.TabStop = false;
			groupBox1.Text = "Treść polecenia";
			// 
			// textBoxStatus
			// 
			textBoxStatus.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			textBoxStatus.Location = new System.Drawing.Point(391, 50);
			textBoxStatus.Name = "textBoxStatus";
			textBoxStatus.ReadOnly = true;
			textBoxStatus.Size = new System.Drawing.Size(163, 23);
			textBoxStatus.TabIndex = 2;
			// 
			// buttonUruchom
			// 
			buttonUruchom.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			buttonUruchom.Location = new System.Drawing.Point(391, 21);
			buttonUruchom.Name = "buttonUruchom";
			buttonUruchom.Size = new System.Drawing.Size(163, 23);
			buttonUruchom.TabIndex = 1;
			buttonUruchom.Text = "Uruchom [F5]";
			buttonUruchom.UseVisualStyleBackColor = true;
			buttonUruchom.Click += buttonUruchom_Click;
			// 
			// EkranSQL
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(groupBoxWynik);
			Controls.Add(groupBox1);
			Name = "EkranSQL";
			Size = new System.Drawing.Size(566, 282);
			groupBoxWynik.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridViewWynik).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
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
