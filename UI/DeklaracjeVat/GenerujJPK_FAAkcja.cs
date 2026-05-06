using ProFak.DB;

namespace ProFak.UI;

class GenerujJPK_FAAkcja : AkcjaNaSpisie<DeklaracjaVat>
{
	public override string Nazwa => "Generuj JPK_FA";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<DeklaracjaVat> zaznaczoneRekordy) => zaznaczoneRekordy.Any();

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<DeklaracjaVat> zaznaczoneRekordy)
	{
		var plik = OknoWyboruPliku.Zapisz("Wybierz miejsce do zapisu JPK", "Deklaracja JPK_FA", "*.xml", zaznaczoneRekordy.Count() == 1 ? $"jpk-fa-{zaznaczoneRekordy.Single().Miesiac:yyyy-MM}.xml" : $"jpk-fa-{zaznaczoneRekordy.Min(e => e.Miesiac):yyyy-MM}-{zaznaczoneRekordy.Max(e => e.Miesiac):yyyy-MM}.xml");
		if (plik == null) return;
		using var nowyKontekst = new Kontekst(kontekst);
		foreach (var deklaracja in zaznaczoneRekordy) nowyKontekst.Dodaj(deklaracja);
		IO.JPK_FA.Generator.Utworz(plik, nowyKontekst.Baza, zaznaczoneRekordy);
		OknoKomunikatu.Informacja("Plik został zapisany pomyślnie.");
	}
}
