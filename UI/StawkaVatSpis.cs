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
	partial class StawkaVatSpis : Spis<StawkaVat>
	{
		public StawkaVatSpis()
		{
			DodajKolumne(nameof(StawkaVat.Skrot), "Skrót", rozciagnij: true);
			DodajKolumne(nameof(StawkaVat.Wartosc), "Wartość", wyrownajDoPrawej: true, format: "0");
			DodajKolumne(nameof(StawkaVat.CzyDomyslnaFmt), "Domyślna");
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.StawkiVat.ToList();
		}

		protected override void UstawStylWiersza(StawkaVat rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.CzyDomyslna) styl.Font = new Font(styl.Font, FontStyle.Bold);
		}
	}
}
