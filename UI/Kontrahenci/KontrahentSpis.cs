using ProFak.DB;

namespace ProFak.UI;

class KontrahentSpis : Spis<Kontrahent>
{
	public KontrahentSpis()
	{
		DodajKolumne(nameof(Kontrahent.Nazwa), "Nazwa", szerokosc: 200);
		DodajKolumne(nameof(Kontrahent.PelnaNazwa), "Pełna nazwa", rozciagnij: true);
		DodajKolumne(nameof(Kontrahent.NIP), "NIP");
		DodajKolumne(nameof(Kontrahent.AdresRejestrowyFmt), "Adres", szerokosc: 300);
		DodajKolumneBool(nameof(Kontrahent.CzyArchiwalny), "Arch.", szerokosc: 50);
		DodajKolumneBool(nameof(Kontrahent.CzyImportKSeF), "KSeF", szerokosc: 50);
		DodajKolumneId();
	}

	protected override void Przeladuj()
	{
		Rekordy = Kontekst.Baza.Kontrahenci.AsEnumerable().Where(kontrahent => !kontrahent.CzyPodmiot).OrderBy(kontrahent => kontrahent.Nazwa);
	}

	protected override void UstawStylWiersza(Kontrahent rekord, string kolumna, DataGridViewCellStyle styl)
	{
		base.UstawStylWiersza(rekord, kolumna, styl);
		if (rekord.CzyPodmiot) styl.Font = new Font(styl.Font!, FontStyle.Bold);
		else if (rekord.CzyArchiwalny) styl.ForeColor = Color.Gray;
		else if (rekord.CzyImportKSeF) styl.ForeColor = Color.LightSkyBlue;
	}
}
