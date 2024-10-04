
namespace ProFak.UI
{
	partial class Dialog
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
			buttonZapisz = new ButtonDPI();
			buttonAnuluj = new ButtonDPI();
			tableLayoutPanelZawartosc = new System.Windows.Forms.TableLayoutPanel();
			flowLayoutPanelPrzyciski = new System.Windows.Forms.FlowLayoutPanel();
			panelZawartosc = new System.Windows.Forms.Panel();
			tableLayoutPanelZawartosc.SuspendLayout();
			flowLayoutPanelPrzyciski.SuspendLayout();
			SuspendLayout();
			// 
			// buttonZapisz
			// 
			buttonZapisz.AutoSize = true;
			buttonZapisz.DialogResult = System.Windows.Forms.DialogResult.OK;
			buttonZapisz.Location = new System.Drawing.Point(3, 3);
			buttonZapisz.Name = "buttonZapisz";
			buttonZapisz.Size = new System.Drawing.Size(79, 25);
			buttonZapisz.TabIndex = 1000;
			buttonZapisz.Text = "Zapisz [F10]";
			buttonZapisz.UseVisualStyleBackColor = true;
			// 
			// buttonAnuluj
			// 
			buttonAnuluj.AutoSize = true;
			buttonAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonAnuluj.Location = new System.Drawing.Point(88, 3);
			buttonAnuluj.Name = "buttonAnuluj";
			buttonAnuluj.Size = new System.Drawing.Size(83, 25);
			buttonAnuluj.TabIndex = 1001;
			buttonAnuluj.Text = "Anuluj [ESC]";
			buttonAnuluj.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanelZawartosc
			// 
			tableLayoutPanelZawartosc.AutoSize = true;
			tableLayoutPanelZawartosc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			tableLayoutPanelZawartosc.ColumnCount = 1;
			tableLayoutPanelZawartosc.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanelZawartosc.Controls.Add(flowLayoutPanelPrzyciski, 0, 1);
			tableLayoutPanelZawartosc.Controls.Add(panelZawartosc, 0, 0);
			tableLayoutPanelZawartosc.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanelZawartosc.Name = "tableLayoutPanelZawartosc";
			tableLayoutPanelZawartosc.RowCount = 2;
			tableLayoutPanelZawartosc.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanelZawartosc.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tableLayoutPanelZawartosc.Size = new System.Drawing.Size(196, 63);
			tableLayoutPanelZawartosc.TabIndex = 3;
			// 
			// flowLayoutPanelPrzyciski
			// 
			flowLayoutPanelPrzyciski.AutoSize = true;
			flowLayoutPanelPrzyciski.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			flowLayoutPanelPrzyciski.Controls.Add(buttonZapisz);
			flowLayoutPanelPrzyciski.Controls.Add(buttonAnuluj);
			flowLayoutPanelPrzyciski.Location = new System.Drawing.Point(3, 29);
			flowLayoutPanelPrzyciski.Name = "flowLayoutPanelPrzyciski";
			flowLayoutPanelPrzyciski.Size = new System.Drawing.Size(174, 31);
			flowLayoutPanelPrzyciski.TabIndex = 999;
			// 
			// panelZawartosc
			// 
			panelZawartosc.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			panelZawartosc.Location = new System.Drawing.Point(3, 3);
			panelZawartosc.MinimumSize = new System.Drawing.Size(190, 20);
			panelZawartosc.Name = "panelZawartosc";
			panelZawartosc.Size = new System.Drawing.Size(190, 20);
			panelZawartosc.TabIndex = 1;
			// 
			// Dialog
			// 
			AcceptButton = buttonZapisz;
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
			CancelButton = buttonAnuluj;
			ClientSize = new System.Drawing.Size(198, 65);
			Controls.Add(tableLayoutPanelZawartosc);
			KeyPreview = true;
			Name = "Dialog";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "OknoEdycji";
			tableLayoutPanelZawartosc.ResumeLayout(false);
			tableLayoutPanelZawartosc.PerformLayout();
			flowLayoutPanelPrzyciski.ResumeLayout(false);
			flowLayoutPanelPrzyciski.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private ButtonDPI buttonZapisz;
		private ButtonDPI buttonAnuluj;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanelZawartosc;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPrzyciski;
		private System.Windows.Forms.Panel panelZawartosc;
	}
}