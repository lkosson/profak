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
			DodajTextBox(nameof(JednostkaMiary.Skrot), "Skrót");
			DodajTextBox(nameof(JednostkaMiary.Nazwa), "Nazwa");
			DodajCheckBox(nameof(JednostkaMiary.CzyDomyslna), "Domyślna");
			DodajNumericUpDown(nameof(JednostkaMiary.LiczbaMiescPoPrzecinku), "Liczba miejsc po przecinku");
			MinimumSize = new Size(250, 100);
		}
	}
}
