using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class WplataEdytor : Edytor<Wplata>
	{
		public WplataEdytor()
		{
			DodajDatePicker(wplata => wplata.Data, "Data wpływu");
			DodajNumericUpDown(wplata => wplata.Kwota, "Kwota");
			MinimumSize = new Size(250, 100);
		}
	}
}
