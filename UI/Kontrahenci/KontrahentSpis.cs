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

	protected override TColor KolorWiersza(Kontrahent rekord)
	{
		if (rekord.CzyArchiwalny) return Kontrolki.Color(128, 128, 128);
		if (rekord.CzyImportKSeF) return Kontrolki.Color(135, 206, 250);
		return base.KolorWiersza(rekord);
	}
}
