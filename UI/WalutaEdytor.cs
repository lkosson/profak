using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class WalutaEdytor : Edytor<Waluta>
	{
		public WalutaEdytor()
		{
			DodajTextBox(waluta => waluta.Skrot, "Skrót", wymagane: true);
			DodajTextBox(waluta => waluta.Nazwa, "Nazwa", wymagane: true);
			DodajCheckBox(waluta => waluta.CzyDomyslna, "Domyślna");
			MinimumSize = new Size(250, 80);
		}
	}
}
