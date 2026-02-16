using ProFak.DB;

namespace ProFak.UI
{
	class TowarSpis : Spis<Towar>
	{
		public TowarSpis()
		{
			DodajKolumne(nameof(Towar.Nazwa), "Nazwa", rozciagnij: true);
			DodajKolumne(nameof(Towar.Rodzaj), "Rodzaj");
			DodajKolumneKwota(nameof(Towar.CenaNetto), "Cena netto");
			DodajKolumneKwota(nameof(Towar.CenaBrutto), "Cena brutto");
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.Towary.AsEnumerable().OrderBy(towar => towar.Nazwa);
		}

		protected override void UstawStylWiersza(Towar rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.CzyArchiwalny) styl.ForeColor = Color.LightGray;
			else if (rekord.Rodzaj == RodzajTowaru.Usługa) styl.ForeColor = Color.DarkBlue;
			if (rekord.CzyWedlugCenBrutto && kolumna == nameof(Towar.CenaBrutto)) styl.Font = new Font(styl.Font!, FontStyle.Bold);
			if (!rekord.CzyWedlugCenBrutto && kolumna == nameof(Towar.CenaNetto)) styl.Font = new Font(styl.Font!, FontStyle.Bold);
		}
	}
}
