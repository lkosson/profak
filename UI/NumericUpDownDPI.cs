using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class NumericUpDownDPI : NumericUpDown
	{
		public NumericUpDownDPI()
		{
		}

		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			AutoScaleMode = AutoScaleMode.None;
		}
	}
}
