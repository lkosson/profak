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
	partial class StawkaVatEdytor : Edytor<StawkaVat>
	{
		public StawkaVatEdytor()
		{
			DodajTextBox(nameof(StawkaVat.Skrot), "Skrót");
			DodajNumericUpDown(nameof(StawkaVat.Wartosc), "Wartość");
			DodajCheckBox(nameof(StawkaVat.CzyDomyslna), "Domyślna");
			MinimumSize = new Size(250, 80);
		}
	}
}
