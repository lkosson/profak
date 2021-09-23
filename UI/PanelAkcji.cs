using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class PanelAkcji : FlowLayoutPanel
	{
		public PanelAkcji()
		{
			FlowDirection = FlowDirection.TopDown;
			WrapContents = false;
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			SuspendLayout();
			foreach (Control ctl in Controls)
			{
				ctl.Width = ClientSize.Width - Padding.Left - Padding.Right - ctl.Margin.Left - ctl.Margin.Right;
			}
			ResumeLayout();
		}
	}
}
