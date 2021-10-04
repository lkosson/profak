using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class WalutaEdytor : Edytor<Waluta>
	{
		public override string Tytul => "Waluta";

		public WalutaEdytor()
		{
			DodajTextBox(nameof(Waluta.Skrot), "Skrót");
			DodajTextBox(nameof(Waluta.Nazwa), "Nazwa");
			DodajCheckBox(nameof(Waluta.CzyDomyslna), "Domyślna");
		}
	}
}
