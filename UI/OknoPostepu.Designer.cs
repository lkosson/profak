
namespace ProFak.UI
{
	partial class OknoPostepu
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
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			label1 = new System.Windows.Forms.Label();
			progressBar = new System.Windows.Forms.ProgressBar();
			buttonAnuluj = new System.Windows.Forms.Button();
			tableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(buttonAnuluj, 0, 2);
			tableLayoutPanel1.Controls.Add(label1, 0, 0);
			tableLayoutPanel1.Controls.Add(progressBar, 0, 1);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
			tableLayoutPanel1.RowCount = 3;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanel1.Size = new System.Drawing.Size(264, 81);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(6, 3);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(200, 15);
			label1.TabIndex = 0;
			label1.Text = "Trwa wykonywanie zleconej operacji.";
			// 
			// progressBar
			// 
			progressBar.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			progressBar.Location = new System.Drawing.Point(6, 22);
			progressBar.Name = "progressBar";
			progressBar.Size = new System.Drawing.Size(252, 23);
			progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			progressBar.TabIndex = 3;
			// 
			// buttonAnuluj
			// 
			buttonAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonAnuluj.Enabled = false;
			buttonAnuluj.Location = new System.Drawing.Point(6, 52);
			buttonAnuluj.Name = "buttonAnuluj";
			buttonAnuluj.Size = new System.Drawing.Size(75, 23);
			buttonAnuluj.TabIndex = 4;
			buttonAnuluj.Text = "Anuluj";
			buttonAnuluj.UseVisualStyleBackColor = true;
			// 
			// OknoPostepu
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(264, 81);
			Controls.Add(tableLayoutPanel1);
			Name = "OknoPostepu";
			Text = "ProFak - czekaj";
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonAnuluj;
		private System.Windows.Forms.ProgressBar progressBar;
	}
}