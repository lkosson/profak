using ProFak.DB;

namespace ProFak.UI;

class ZapiszPlikiAkcja : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "🖫 Zapisz pliki";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any(e => e.Pliki?.Count > 0);

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		using var nowyKontekst = new Kontekst(kontekst);
		using var transakcja = nowyKontekst.Transakcja();
		var katalog = OknoWyboruPliku.Katalog("Wybierz katalog, do którego mają zostać zapisane pliki.");
		if (katalog == null) return;
		var liczbaPlikow = 0;
		foreach (var faktura in zaznaczoneRekordy)
		{
			foreach (var plik in faktura.Pliki)
			{
				var zawartosc = nowyKontekst.Baza.Znajdz(plik.ZawartoscRef);
				File.WriteAllBytes(Path.Combine(katalog, plik.Nazwa), zawartosc.Dane);
				liczbaPlikow++;
			}
		}
		OknoKomunikatu.Informacja($"Liczba zapisanych plików: {liczbaPlikow}.");
	}
}
