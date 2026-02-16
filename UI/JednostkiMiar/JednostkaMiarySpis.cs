using ProFak.DB;

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

		protected override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.JednostkiMiar.AsEnumerable().OrderBy(jednostka => jednostka.Nazwa);
		}

		protected override void UstawStylWiersza(JednostkaMiary rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.CzyDomyslna) styl.Font = new Font(styl.Font!, FontStyle.Bold);
		}
	}
}
