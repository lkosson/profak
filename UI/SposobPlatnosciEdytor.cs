using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class SposobPlatnosciEdytor : Edytor<SposobPlatnosci>
	{
		public SposobPlatnosciEdytor()
		{
			DodajTextBox(nameof(SposobPlatnosci.Nazwa), "Nazwa");
			DodajNumericUpDown(nameof(SposobPlatnosci.LiczbaDni), "Liczba dni");
			DodajCheckBox(nameof(SposobPlatnosci.CzyDomyslny), "Domyślny");
			MinimumSize = new Size(250, 80);
		}
	}
}
