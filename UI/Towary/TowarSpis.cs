using ProFak.DB;

namespace ProFak.UI;

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

	protected override TColor KolorWiersza(Towar rekord)
	{
		if (rekord.CzyArchiwalny) return Kontrolki.Color(210, 210, 210);
		if (rekord.Rodzaj == RodzajTowaru.Usługa) return Kontrolki.Color(0, 0, 139);
		return base.KolorWiersza(rekord);
	}
}
