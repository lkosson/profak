using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class SposobPlatnosciSpis : Spis<SposobPlatnosci>
	{
		public SposobPlatnosciSpis()
		{
			DodajKolumne(nameof(SposobPlatnosci.Nazwa), "Nazwa", rozciagnij: true);
			DodajKolumne(nameof(SposobPlatnosci.LiczbaDni), "Liczba dni", wyrownajDoPrawej: true);
			DodajKolumne(nameof(SposobPlatnosci.CzyDomyslnyFmt), "Domyślny");
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.SposobyPlatnosci.ToList();
		}

		protected override void UstawStylWiersza(SposobPlatnosci rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.CzyDomyslny) styl.Font = new Font(styl.Font, FontStyle.Bold);
		}
	}
}
