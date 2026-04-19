using ProFak.DB;

namespace ProFak.UI;

class ZaliczkaPitEdytor : Edytor<ZaliczkaPit>
{
	private readonly SpisZAkcjami<Faktura, FakturaSprzedazySpis> fakturySprzedazy;
	private readonly SpisZAkcjami<Faktura, FakturaZakupuSpis> fakturyZakupu;

	public ZaliczkaPitEdytor()
	{
		var dateTimePickerMiesiac = Kontrolki.DatePicker(tylkoMiesiac: true);
		var numericUpDownPrzychody = Kontrolki.NumericUpDown();
		var numericUpDownKoszty = Kontrolki.NumericUpDown();
		var numericUpDownSkladkiZus = Kontrolki.NumericUpDown();
		var numericUpDownPodatek = Kontrolki.NumericUpDown();
		var numericUpDownPrzeniesiony = Kontrolki.NumericUpDown();
		var numericUpDownDoPrzeniesienia = Kontrolki.NumericUpDown();
		var numericUpDownDoWplaty = Kontrolki.NumericUpDown();

		kontroler.Powiazanie(dateTimePickerMiesiac, deklaracja => deklaracja.Miesiac);
		kontroler.Powiazanie(numericUpDownPrzychody, deklaracja => deklaracja.Przychody);
		kontroler.Powiazanie(numericUpDownKoszty, deklaracja => deklaracja.Koszty);
		kontroler.Powiazanie(numericUpDownSkladkiZus, deklaracja => deklaracja.SkladkiZus);
		kontroler.Powiazanie(numericUpDownPodatek, deklaracja => deklaracja.Podatek);
		kontroler.Powiazanie(numericUpDownPrzeniesiony, deklaracja => deklaracja.Przeniesiony);
		kontroler.Powiazanie(numericUpDownDoPrzeniesienia, deklaracja => deklaracja.DoPrzeniesienia);
		kontroler.Powiazanie(numericUpDownDoWplaty, deklaracja => deklaracja.DoWplaty);

		var obliczenia = new DwieKolumny();
		obliczenia.DodajWiersz(numericUpDownPrzychody, "Przychody");
		obliczenia.DodajWiersz(numericUpDownKoszty, "Koszty");
		obliczenia.DodajWiersz(numericUpDownSkladkiZus, "Składki ZUS");
		obliczenia.DodajWiersz(numericUpDownPodatek, "Podatek");
		obliczenia.DodajWiersz(numericUpDownPrzeniesiony, "Przeniesiony");
		obliczenia.DodajWiersz(numericUpDownDoPrzeniesienia, "Do przeniesienia");
		obliczenia.DodajWiersz(numericUpDownDoWplaty, "Do wpłaty");

		var dodajSprzedazDoZaliczki = new DynamicznaAkcja<Faktura>("➕ Dodaj do zaliczki [INS]", kontekst =>
		{
			using var spis = new SpisZAkcjami<Faktura, FakturaSprzedazySpis>(new FakturaSprzedazySpis { Parametry = new() { CzyBezZaliczkiPit = true } });
			var faktura = Spisy.Wybierz(kontekst, spis, "Wybierz fakturę", default);
			if (faktura == null) return;
			faktura.ZaliczkaPitRef = Rekord;
			kontekst.Baza.Zapisz(faktura);
			Przelicz();
		}, Keys.Insert, Keys.None);

		var dodajZakupDoZaliczki = new DynamicznaAkcja<Faktura>("➕ Dodaj do zaliczki [INS]", kontekst =>
		{
			using var spis = new SpisZAkcjami<Faktura, FakturaZakupuSpis>(new FakturaZakupuSpis { Parametry = new() { CzyBezZaliczkiPit = true } });
			var faktura = Spisy.Wybierz(kontekst, spis, "Wybierz fakturę", default);
			if (faktura == null) return;
			faktura.ZaliczkaPitRef = Rekord;
			kontekst.Baza.Zapisz(faktura);
			Przelicz();
		}, Keys.Insert, Keys.None);

		var usunZZaliczki = new DynamicznaAkcja<Faktura>("❌ Usuń z zaliczki [DEL]", (kontekst, rekordy) =>
		{
			foreach (var rekord in rekordy)
			{
				rekord.ZaliczkaPitRef = default;
			}
			kontekst.Baza.Zapisz(rekordy);
			Przelicz();
		}, Keys.Delete, Keys.None);

		fakturySprzedazy = new SpisZAkcjami<Faktura, FakturaSprzedazySpis>(new FakturaSprzedazySpis(), new AkcjaNaSpisie<Faktura>[] { dodajSprzedazDoZaliczki, new EdytujRekordAkcja<Faktura, FakturaEdytor>(), usunZZaliczki, new WydrukFakturyAkcja(), new PrzeladujAkcja<Faktura>() });
		fakturyZakupu = new SpisZAkcjami<Faktura, FakturaZakupuSpis>(new FakturaZakupuSpis(), new AkcjaNaSpisie<Faktura>[] { dodajZakupDoZaliczki, new EdytujRekordAkcja<Faktura, FakturaEdytor>(), usunZZaliczki, new PrzeladujAkcja<Faktura>() });

		var naglowek = new Poziomo([
			Kontrolki.Label("Miesiąc"),
			dateTimePickerMiesiac,
			Kontrolki.Button("Przelicz", delegate { WybierzFaktury(); Przelicz(); })
			]);

		var zakladki = new Zakladki();
		zakladki.Dodaj("Obliczenia", obliczenia);
		zakladki.Dodaj("Sprzedaż", fakturySprzedazy);
		zakladki.Dodaj("Zakup", fakturyZakupu);

		var uklad = new Siatka([-1], [0, -1]);
		uklad.DodajWiersz([naglowek]);
		uklad.DodajWiersz([zakladki]);

		UstawZawartosc(uklad);
	}

	protected override void RekordGotowy()
	{
		base.RekordGotowy();

		fakturySprzedazy.Spis.Parametry.ZaliczkaPitRef = Rekord;
		fakturySprzedazy.Spis.Kontekst = Kontekst;
		fakturyZakupu.Spis.Parametry.ZaliczkaPitRef = Rekord;
		fakturyZakupu.Spis.Kontekst = Kontekst;
	}

	private void buttonPrzelicz_Click(object? sender, EventArgs e)
	{
		try
		{
			WybierzFaktury();
			Przelicz();
		}
		catch (Exception exc)
		{
			OknoBledu.Pokaz(exc);
		}
	}

	private void WybierzFaktury()
	{
		Rekord.WybierzFaktury(Kontekst.Baza);
		fakturySprzedazy.Spis.PrzeladujBezpiecznie();
		fakturyZakupu.Spis.PrzeladujBezpiecznie();
	}

	private void Przelicz()
	{
		Rekord.Przelicz(Kontekst.Baza);
		kontroler.AktualizujKontrolki();
	}
}
