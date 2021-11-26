using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class JednostkaMiarySpis : Spis<JednostkaMiary>
	{
		public JednostkaMiarySpis()
		{
			DodajKolumne(nameof(JednostkaMiary.Skrot), "Skrót");
			DodajKolumne(nameof(JednostkaMiary.Nazwa), "Nazwa", rozciagnij: true);
			DodajKolumne(nameof(JednostkaMiary.CzyDomyslnaFmt), "Domyślna");
			DodajKolumne(nameof(JednostkaMiary.LiczbaMiescPoPrzecinku), "Liczba miejsc po przecinku", wyrownajDoPrawej: true);
			DodajKolumneId();
		}

		public override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.JednostkiMiar.ToList();
		}
	}
}
