using ProFak.DB;

namespace ProFak.UI;

class FakturaRozliczenieZaliczkiAkcja : FakturaPodobnaAkcja
{
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() >= 1 && zaznaczoneRekordy.All(faktura => faktura.FakturaRozliczeniowaRef.IsNull && faktura.Rodzaj is RodzajFaktury.Zaliczka or RodzajFaktury.KorektaZaliczki);
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => modyfikatory == TKeyModifiers.Shift && klawisz == TKeys.Insert;
	public override string Nazwa => "➕ Wystaw rozliczenie [SHIFT-INS]";

	protected override Faktura? UtworzRekord(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
	{
		var zaznaczona = zaznaczoneRekordy.First();
		var podobna = zaznaczona.PrzygotujPodobna(kontekst.Baza);
		podobna.Rodzaj = RodzajFaktury.Rozliczenie;
		foreach (var zaliczka in zaznaczoneRekordy)
		{
			zaliczka.FakturaRozliczeniowaId = podobna.Id;
			kontekst.Baza.Zapisz(zaliczka);
			var wplata = new Wplata { Data = zaliczka.DataWystawienia, FakturaRef = podobna, Kwota = zaliczka.RazemBrutto, Uwagi = $"Zaliczka {zaliczka.Numer} z dnia {zaliczka.DataWystawienia.ToString(Wyglad.FormatDaty)}", CzyRozliczenie = true };
			kontekst.Baza.Zapisz(wplata);
		}
		return podobna;
	}

	protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
	{
		rekord.ZakonczWystawianie(kontekst.Baza);
		base.ZapiszRekord(kontekst, rekord);
	}
}
