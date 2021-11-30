using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class StawkaVatEdytor : EdytorDwieKolumny<StawkaVat>
	{
		public StawkaVatEdytor()
		{
			DodajTextBox(stawkaVat => stawkaVat.Skrot, "Skrót", wymagane: true);
			DodajNumericUpDown(stawkaVat => stawkaVat.Wartosc, "Wartość");
			DodajCheckBox(stawkaVat => stawkaVat.CzyDomyslna, "Domyślna");
			MinimumSize = new Size(250, 80);
		}
	}
}
