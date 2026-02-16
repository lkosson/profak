using ProFak.DB;
using System.Data;

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
			Rekordy = Kontekst.Baza.StawkiVat.AsEnumerable().OrderBy(stawka => stawka.Skrot);
		}

		protected override void UstawStylWiersza(StawkaVat rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.CzyDomyslna) styl.Font = new Font(styl.Font!, FontStyle.Bold);
		}
	}
}
