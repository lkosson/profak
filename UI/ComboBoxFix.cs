using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
	class ComboBoxFix : ComboBox
	{
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (!Focused) Select(0, 0);
		}
	}
}
