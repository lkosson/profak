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
			DodajTextBox(sposobPlatnosci => sposobPlatnosci.Nazwa, "Nazwa");
			DodajNumericUpDown(sposobPlatnosci => sposobPlatnosci.LiczbaDni, "Liczba dni");
			DodajCheckBox(sposobPlatnosci => sposobPlatnosci.CzyDomyslny, "Domyślny");
			MinimumSize = new Size(250, 80);
		}
	}
}
