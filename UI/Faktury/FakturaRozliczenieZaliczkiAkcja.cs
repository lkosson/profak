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
		foreach (var _zaliczka in zaznaczoneRekordy.OrderBy(faktura => faktura.DataWystawienia).ThenBy(faktura => faktura.Id))
		{
			var zaliczka = _zaliczka;
			if (zaliczka.FakturaPierwotnaRef.IsNotNull) zaliczka = kontekst.Baza.Znajdz(zaliczka.FakturaPierwotnaRef);

			do
			{
				zaliczka.FakturaRozliczeniowaId = podobna.Id;
				kontekst.Baza.Zapisz(zaliczka);
				if (zaliczka.RazemBrutto != 0)
				{
					var wplata = new Wplata { Data = zaliczka.DataWystawienia, FakturaRef = podobna, Kwota = zaliczka.RazemBrutto, Uwagi = $"Zaliczka {zaliczka.Numer} z dnia {zaliczka.DataWystawienia.ToString(UI.Wyglad.FormatDaty)}", CzyZaliczka = true };
					kontekst.Baza.Zapisz(wplata);
				}
				if (zaliczka.FakturaKorygujacaRef.IsNull) break;
				zaliczka = kontekst.Baza.Znajdz(zaliczka.FakturaKorygujacaRef);
			}
			while (true);
		}
		return podobna;
	}

	protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
	{
		rekord.ZakonczWystawianie(kontekst.Baza);
		base.ZapiszRekord(kontekst, rekord);
	}
}
