using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class JednostkaMiaryEdytor : EdytorDwieKolumny<JednostkaMiary>
	{
		public JednostkaMiaryEdytor()
		{
			DodajTextBox(jednostkaMiary => jednostkaMiary.Skrot, "Skrót", wymagane: true);
			DodajTextBox(jednostkaMiary => jednostkaMiary.Nazwa, "Nazwa", wymagane: true);
			DodajCheckBox(jednostkaMiary => jednostkaMiary.CzyDomyslna, "Domyślna");
			DodajNumericUpDown(jednostkaMiary => jednostkaMiary.LiczbaMiescPoPrzecinku, "Liczba miejsc po przecinku");
			MinimumSize = new Size(250, 120);
		}
	}
}
