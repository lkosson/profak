
namespace ProFak.UI
{
	partial class OknoSpisu
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
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.panelAkcji1 = new ProFak.UI.PanelAkcji();
			this.buttonDodaj = new System.Windows.Forms.Button();
			this.buttonUsun = new System.Windows.Forms.Button();
			this.buttonEdytuj = new System.Windows.Forms.Button();
			this.panelSpis = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.panelAkcji1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Cursor = System.Windows.Forms.Cursors.VSplit;
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer.Location = new System.Drawing.Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.panelSpis);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.Controls.Add(this.panelAkcji1);
			this.splitContainer.Size = new System.Drawing.Size(800, 450);
			this.splitContainer.SplitterDistance = 573;
			this.splitContainer.TabIndex = 0;
			// 
			// panelAkcji1
			// 
			this.panelAkcji1.Controls.Add(this.buttonDodaj);
			this.panelAkcji1.Controls.Add(this.buttonUsun);
			this.panelAkcji1.Controls.Add(this.buttonEdytuj);
			this.panelAkcji1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelAkcji1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.panelAkcji1.Location = new System.Drawing.Point(0, 0);
			this.panelAkcji1.Name = "panelAkcji1";
			this.panelAkcji1.Size = new System.Drawing.Size(223, 450);
			this.panelAkcji1.TabIndex = 2;
			this.panelAkcji1.WrapContents = false;
			// 
			// buttonDodaj
			// 
			this.buttonDodaj.AutoSize = true;
			this.buttonDodaj.Location = new System.Drawing.Point(3, 3);
			this.buttonDodaj.Name = "buttonDodaj";
			this.buttonDodaj.Size = new System.Drawing.Size(217, 25);
			this.buttonDodaj.TabIndex = 0;
			this.buttonDodaj.Text = "Dodaj nową pozycję";
			this.buttonDodaj.UseVisualStyleBackColor = true;
			// 
			// buttonUsun
			// 
			this.buttonUsun.AutoSize = true;
			this.buttonUsun.Location = new System.Drawing.Point(3, 34);
			this.buttonUsun.Name = "buttonUsun";
			this.buttonUsun.Size = new System.Drawing.Size(217, 25);
			this.buttonUsun.TabIndex = 0;
			this.buttonUsun.Text = "Usuń zaznaczone pozycje";
			this.buttonUsun.UseVisualStyleBackColor = true;
			// 
			// buttonEdytuj
			// 
			this.buttonEdytuj.AutoSize = true;
			this.buttonEdytuj.Location = new System.Drawing.Point(3, 65);
			this.buttonEdytuj.Name = "buttonEdytuj";
			this.buttonEdytuj.Size = new System.Drawing.Size(217, 25);
			this.buttonEdytuj.TabIndex = 0;
			this.buttonEdytuj.Text = "Edytuj zaznaczoną pozycję";
			this.buttonEdytuj.UseVisualStyleBackColor = true;
			// 
			// panelSpis
			// 
			this.panelSpis.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelSpis.Location = new System.Drawing.Point(0, 0);
			this.panelSpis.Name = "panelSpis";
			this.panelSpis.Size = new System.Drawing.Size(573, 450);
			this.panelSpis.TabIndex = 0;
			// 
			// OknoSpisu
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.splitContainer);
			this.Name = "OknoSpisu";
			this.Text = "OknoSpisu";
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.panelAkcji1.ResumeLayout(false);
			this.panelAkcji1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Button buttonDodaj;
		private System.Windows.Forms.Button buttonEdytuj;
		private System.Windows.Forms.Button buttonUsun;
		private PanelAkcji panelAkcji1;
		private System.Windows.Forms.Panel panelSpis;
	}
}