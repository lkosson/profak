using ProFak.DB;

namespace ProFak.UI;

class DodajDodatkowyPodmiotKontrahentAkcja : DodajRekordAkcja<DodatkowyPodmiot, DodatkowyPodmiotEdytor>
{
	public override string Nazwa => "➕ Dodaj kontrahenta [SHIFT-INS]";
	public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.Shift && klawisz == Keys.Insert;

	public DodajDodatkowyPodmiotKontrahentAkcja(Action<DodatkowyPodmiot> przygotujRekord)
		: base(przygotujRekord)
	{
	}

	protected override DodatkowyPodmiot? UtworzRekord(Kontekst kontekst, IEnumerable<DodatkowyPodmiot> zaznaczoneRekordy)
	{
		using var spis = Spisy.Kontrahenci();
		var kontrahent = Spisy.Wybierz(kontekst, spis, "Wybierz pozycję", default);
		if (kontrahent == null) return null;
		var dodatkowyPodmiot = base.UtworzRekord(kontekst, zaznaczoneRekordy);
		if (dodatkowyPodmiot == null) return null;
		dodatkowyPodmiot.Adres = kontrahent.AdresRejestrowy;
		dodatkowyPodmiot.NIP = kontrahent.NIP;
		dodatkowyPodmiot.Nazwa = kontrahent.PelnaNazwaLubNazwa;
		return dodatkowyPodmiot;
	}
}
