using ProFak.DB;

namespace ProFak.UI;

class ZapiszPlikAkcja : AkcjaNaSpisie<Plik>
{
	public override string Nazwa => "🖫 Zapisz plik [CTRL-S]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Plik> zaznaczoneRekordy) => zaznaczoneRekordy.Any();
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => modyfikatory == TKeyModifiers.Control && klawisz == TKeys.S;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Plik> zaznaczoneRekordy)
	{
		var plik = zaznaczoneRekordy.Single();
		using var nowyKontekst = new Kontekst(kontekst);
		nowyKontekst.Dodaj(plik);
		var nazwa = OknoWyboruPliku.Zapisz("Zapisywanie pliku", nazwa: plik.Nazwa);
		if (nazwa == null) return;
		using var transakcja = nowyKontekst.Transakcja();
		var zawartosc = nowyKontekst.Baza.Znajdz<Zawartosc>(plik.ZawartoscId);
		File.WriteAllBytes(nazwa, zawartosc.Dane);
	}
}
