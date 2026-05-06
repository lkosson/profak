using ProFak.DB;

namespace ProFak.UI;

class GenerujJPK_PKPIRAkcja : AkcjaNaSpisie<ZaliczkaPit>
{
	public override string Nazwa => "Generuj JPK_PKPIR";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<ZaliczkaPit> zaznaczoneRekordy) => zaznaczoneRekordy.Any();

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<ZaliczkaPit> zaznaczoneRekordy)
	{
		var plik = OknoWyboruPliku.Zapisz("Wybierz miejsce do zapisu JPK", "Deklaracja JPK_PKPIR", "*.xml", zaznaczoneRekordy.Count() == 1 ? $"jpk-pkpir-{zaznaczoneRekordy.Single().Miesiac:yyyy-MM}.xml" : $"jpk-pkpir-{zaznaczoneRekordy.Min(e => e.Miesiac):yyyy-MM}-{zaznaczoneRekordy.Max(e => e.Miesiac):yyyy-MM}.xml");
		if (plik == null) return;
		using var nowyKontekst = new Kontekst(kontekst);
		foreach (var deklaracja in zaznaczoneRekordy) nowyKontekst.Dodaj(deklaracja);
		IO.JPK_PKPIR.Generator.Utworz(plik, nowyKontekst.Baza, zaznaczoneRekordy);
		OknoKomunikatu.Informacja("Plik został zapisany pomyślnie.");
	}
}
