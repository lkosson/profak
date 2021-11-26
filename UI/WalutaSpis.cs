using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class WalutaSpis : Spis<Waluta>
	{
		public WalutaSpis()
		{
			DodajKolumne(nameof(Waluta.Skrot), "Skrót");
			DodajKolumne(nameof(Waluta.Nazwa), "Nazwa", rozciagnij: true);
			DodajKolumne(nameof(Waluta.CzyDomyslnaFmt), "Domyślna");
			DodajKolumneId();
		}

		public override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.Waluty.ToList();
		}
	}
}
