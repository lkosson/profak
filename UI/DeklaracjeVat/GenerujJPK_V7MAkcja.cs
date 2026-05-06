using ProFak.DB;

namespace ProFak.UI;

class GenerujJPK_V7MAkcja : AkcjaNaSpisie<DeklaracjaVat>
{
	public override string Nazwa => "Generuj JPK_V7M";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<DeklaracjaVat> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<DeklaracjaVat> zaznaczoneRekordy)
	{
		var deklaracja = zaznaczoneRekordy.Single();
		var plik = OknoWyboruPliku.Zapisz("Wybierz miejsce do zapisu JPK", "Deklaracja JPK_V7M", "*.xml", $"jpk-v7m-{deklaracja.Miesiac:yyyy-MM}.xml");
		if (plik == null) return;
		using var nowyKontekst = new Kontekst(kontekst);
		nowyKontekst.Dodaj(deklaracja);
		IO.JPK_V7M.Generator.Utworz(plik, nowyKontekst.Baza, deklaracja);
		OknoKomunikatu.Informacja("Plik został zapisany pomyślnie.");
	}
}
