using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class Spis : DataGridView
	{
		public Spis()
		{
			DoubleBuffered = true;
			AutoGenerateColumns = false;
			AllowUserToAddRows = false;
			AllowUserToDeleteRows = false;
			AllowUserToResizeRows = false;
			AllowUserToOrderColumns = true;
			RowHeadersVisible = false;
			ShowCellToolTips = true;
			ReadOnly = true;
			SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		}
	}
}
