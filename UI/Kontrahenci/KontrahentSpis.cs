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
	class KontrahentSpis : Spis<Kontrahent>
	{
		public KontrahentSpis()
		{
			DodajKolumne(nameof(Kontrahent.Nazwa), "Nazwa", rozciagnij: true);
			DodajKolumne(nameof(Kontrahent.NIP), "NIP");
			DodajKolumne(nameof(Kontrahent.AdresRejestrowyFmt), "Adres", szerokosc: 300);
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.Kontrahenci.AsEnumerable().Where(kontrahent => !kontrahent.CzyPodmiot).OrderBy(kontrahent => kontrahent.Nazwa);
		}

		protected override void UstawStylWiersza(Kontrahent rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.CzyPodmiot) styl.Font = new Font(styl.Font, FontStyle.Bold);
			else if (rekord.CzyArchiwalny) styl.ForeColor = Color.Gray;
		}
	}
}
