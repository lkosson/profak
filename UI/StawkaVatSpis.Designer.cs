
namespace ProFak.UI
{
	partial class StawkaVatSpis
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.spis = new ProFak.UI.Spis();
			this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.skrotDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.wartoscDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.czyDomyslnaDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.spis)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// spis
			// 
			this.spis.AllowUserToAddRows = false;
			this.spis.AllowUserToDeleteRows = false;
			this.spis.AllowUserToOrderColumns = true;
			this.spis.AllowUserToResizeRows = false;
			this.spis.AutoGenerateColumns = false;
			this.spis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.spis.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.skrotDataGridViewTextBoxColumn,
            this.wartoscDataGridViewTextBoxColumn,
            this.czyDomyslnaDataGridViewCheckBoxColumn,
            this.idDataGridViewTextBoxColumn});
			this.spis.DataSource = this.bindingSource;
			this.spis.Dock = System.Windows.Forms.DockStyle.Fill;
			this.spis.Location = new System.Drawing.Point(0, 0);
			this.spis.Name = "spis";
			this.spis.RowHeadersVisible = false;
			this.spis.Size = new System.Drawing.Size(866, 388);
			this.spis.TabIndex = 0;
			// 
			// bindingSource
			// 
			this.bindingSource.DataSource = typeof(ProFak.DB.StawkaVat);
			// 
			// skrotDataGridViewTextBoxColumn
			// 
			this.skrotDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.skrotDataGridViewTextBoxColumn.DataPropertyName = "Skrot";
			this.skrotDataGridViewTextBoxColumn.HeaderText = "Skrót";
			this.skrotDataGridViewTextBoxColumn.Name = "skrotDataGridViewTextBoxColumn";
			// 
			// wartoscDataGridViewTextBoxColumn
			// 
			this.wartoscDataGridViewTextBoxColumn.DataPropertyName = "Wartosc";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			this.wartoscDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
			this.wartoscDataGridViewTextBoxColumn.HeaderText = "Wartość";
			this.wartoscDataGridViewTextBoxColumn.Name = "wartoscDataGridViewTextBoxColumn";
			// 
			// czyDomyslnaDataGridViewCheckBoxColumn
			// 
			this.czyDomyslnaDataGridViewCheckBoxColumn.DataPropertyName = "CzyDomyslna";
			this.czyDomyslnaDataGridViewCheckBoxColumn.HeaderText = "Czy domyślna";
			this.czyDomyslnaDataGridViewCheckBoxColumn.Name = "czyDomyslnaDataGridViewCheckBoxColumn";
			// 
			// idDataGridViewTextBoxColumn
			// 
			this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			this.idDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
			this.idDataGridViewTextBoxColumn.HeaderText = "Id";
			this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
			// 
			// StawkaVatSpis
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.spis);
			this.Name = "StawkaVatSpis";
			this.Size = new System.Drawing.Size(866, 388);
			((System.ComponentModel.ISupportInitialize)(this.spis)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Spis spis;
		private System.Windows.Forms.BindingSource bindingSource;
		private System.Windows.Forms.DataGridViewTextBoxColumn skrotDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn wartoscDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewCheckBoxColumn czyDomyslnaDataGridViewCheckBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
	}
}
