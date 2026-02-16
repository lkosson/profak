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
	class WalutaSpis : Spis<Waluta>
	{
		public WalutaSpis()
		{
			DodajKolumne(nameof(Waluta.Skrot), "Skrót");
			DodajKolumne(nameof(Waluta.Nazwa), "Nazwa", rozciagnij: true);
			DodajKolumne(nameof(Waluta.CzyDomyslnaFmt), "Domyślna");
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.Waluty.AsEnumerable().OrderBy(waluta => waluta.Skrot);
		}

		protected override void UstawStylWiersza(Waluta rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.CzyDomyslna) styl.Font = new Font(styl.Font!, FontStyle.Bold);
		}
	}
}
