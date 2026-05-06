using ProFak.DB;

namespace ProFak.UI;

class GenerujJPK_EWPAkcja : AkcjaNaSpisie<ZaliczkaPit>
{
	public override string Nazwa => "Generuj JPK_EWP";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<ZaliczkaPit> zaznaczoneRekordy) => zaznaczoneRekordy.Any();

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<ZaliczkaPit> zaznaczoneRekordy)
	{
		var plik = OknoWyboruPliku.Zapisz("Wybierz miejsce do zapisu JPK", "Deklaracja JPK_EWP", "*.xml", zaznaczoneRekordy.Count() == 1 ? $"jpk-ewp-{zaznaczoneRekordy.Single().Miesiac:yyyy-MM}.xml" : $"jpk-ewp-{zaznaczoneRekordy.Min(e => e.Miesiac):yyyy-MM}-{zaznaczoneRekordy.Max(e => e.Miesiac):yyyy-MM}.xml");
		if (plik == null) return;
		using var nowyKontekst = new Kontekst(kontekst);
		foreach (var deklaracja in zaznaczoneRekordy) nowyKontekst.Dodaj(deklaracja);
		IO.JPK_EWP.Generator.Utworz(plik, nowyKontekst.Baza, zaznaczoneRekordy);
		OknoKomunikatu.Informacja("Plik został zapisany pomyślnie.");
	}
}
