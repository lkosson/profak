using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class ButtonDPI : Button
	{
		public ButtonDPI()
		{
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			Height = 23 * DeviceDpi / 96;
			if (Text == "..." || Text == "➕") Width = Height;
		}
	}
}
