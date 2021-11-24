using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class JednostkaMiaryEdytor : Edytor<JednostkaMiary>
	{
		public JednostkaMiaryEdytor()
		{
			DodajTextBox(jednostkaMiary => jednostkaMiary.Skrot, "Skrót");
			DodajTextBox(jednostkaMiary => jednostkaMiary.Nazwa, "Nazwa");
			DodajCheckBox(jednostkaMiary => jednostkaMiary.CzyDomyslna, "Domyślna");
			DodajNumericUpDown(jednostkaMiary => jednostkaMiary.LiczbaMiescPoPrzecinku, "Liczba miejsc po przecinku");
			MinimumSize = new Size(250, 100);
		}
	}
}
