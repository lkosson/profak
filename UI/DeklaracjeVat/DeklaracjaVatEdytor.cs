using ProFak.DB;

namespace ProFak.UI;

partial class DeklaracjaVatEdytor : Edytor<DeklaracjaVat>
{
	private readonly SpisZAkcjami<Faktura, FakturaSprzedazySpis> fakturySprzedazy;
	private readonly SpisZAkcjami<Faktura, FakturaZakupuSpis> fakturyZakupu;

	public DeklaracjaVatEdytor()
	{
		var dateTimePickerMiesiac = Kontrolki.DatePicker(tylkoMiesiac: true);

		var numericUpDownNettoZW = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNetto0 = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNetto5 = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNetto8 = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNetto23 = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNettoWDT = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNettoWNT = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNalezny5 = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNalezny8 = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNalezny23 = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNaleznyWNT = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNettoSrodkiTrwale = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNettoPozostale = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNaliczonyPrzeniesiony = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNaliczonySrodkiTrwale = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNaliczonyPozostale = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNettoRazem = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNaleznyRazem = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownNaliczonyRazem = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownDoWplaty = Kontrolki.NumericUpDown(poPrzecinku: 0);
		var numericUpDownDoPrzeniesienia = Kontrolki.NumericUpDown(poPrzecinku: 0);

		kontroler.Powiazanie(dateTimePickerMiesiac, deklaracja => deklaracja.Miesiac);
		kontroler.Powiazanie(numericUpDownNettoZW, deklaracja => deklaracja.NettoZW);
		kontroler.Powiazanie(numericUpDownNetto0, deklaracja => deklaracja.Netto0);
		kontroler.Powiazanie(numericUpDownNetto5, deklaracja => deklaracja.Netto5);
		kontroler.Powiazanie(numericUpDownNetto8, deklaracja => deklaracja.Netto8);
		kontroler.Powiazanie(numericUpDownNetto23, deklaracja => deklaracja.Netto23);
		kontroler.Powiazanie(numericUpDownNettoWDT, deklaracja => deklaracja.NettoWDT);
		kontroler.Powiazanie(numericUpDownNettoWNT, deklaracja => deklaracja.NettoWNT);
		kontroler.Powiazanie(numericUpDownNalezny5, deklaracja => deklaracja.Nalezny5);
		kontroler.Powiazanie(numericUpDownNalezny8, deklaracja => deklaracja.Nalezny8);
		kontroler.Powiazanie(numericUpDownNalezny23, deklaracja => deklaracja.Nalezny23);
		kontroler.Powiazanie(numericUpDownNaleznyWNT, deklaracja => deklaracja.NaleznyWNT);
		kontroler.Powiazanie(numericUpDownNettoSrodkiTrwale, deklaracja => deklaracja.NettoSrodkiTrwale);
		kontroler.Powiazanie(numericUpDownNettoPozostale, deklaracja => deklaracja.NettoPozostale);
		kontroler.Powiazanie(numericUpDownNaliczonyPrzeniesiony, deklaracja => deklaracja.NaliczonyPrzeniesiony);
		kontroler.Powiazanie(numericUpDownNaliczonySrodkiTrwale, deklaracja => deklaracja.NaliczonySrodkiTrwale);
		kontroler.Powiazanie(numericUpDownNaliczonyPozostale, deklaracja => deklaracja.NaliczonyPozostale);
		kontroler.Powiazanie(numericUpDownNettoRazem, deklaracja => deklaracja.NettoRazem);
		kontroler.Powiazanie(numericUpDownNaleznyRazem, deklaracja => deklaracja.NaleznyRazem);
		kontroler.Powiazanie(numericUpDownNaliczonyRazem, deklaracja => deklaracja.NaliczonyRazem);
		kontroler.Powiazanie(numericUpDownDoWplaty, deklaracja => deklaracja.DoWplaty);
		kontroler.Powiazanie(numericUpDownDoPrzeniesienia, deklaracja => deklaracja.DoPrzeniesienia);

		var siatkaObliczenia = new Siatka([0, -1, -1, 20, 0, -1, -1], []);
		siatkaObliczenia.DodajWiersz([null, "Podstawa", "Podatek należny", null, null, "Wartość netto", "Podatek naliczony"]);
		siatkaObliczenia.DodajWiersz([Kontrolki.Label("Zwolnione"), numericUpDownNettoZW, null, null, Kontrolki.Label("Z przeniesienia"), null, numericUpDownNaliczonyPrzeniesiony]);
		siatkaObliczenia.DodajWiersz([Kontrolki.Label("0%"), numericUpDownNetto0, null, null, Kontrolki.Label("Środki trwałe"), numericUpDownNettoSrodkiTrwale, numericUpDownNaliczonySrodkiTrwale]);
		siatkaObliczenia.DodajWiersz([Kontrolki.Label("5%"), numericUpDownNetto5, numericUpDownNalezny5, null, Kontrolki.Label("Pozostałe"), numericUpDownNettoPozostale, numericUpDownNaliczonyPozostale]);
		siatkaObliczenia.DodajWiersz([Kontrolki.Label("8%"), numericUpDownNetto8, numericUpDownNalezny8, null, Kontrolki.Label("Razem"), null, numericUpDownNaliczonyRazem]);
		siatkaObliczenia.DodajWiersz([Kontrolki.Label("23%"), numericUpDownNetto23, numericUpDownNalezny23]);
		siatkaObliczenia.DodajWiersz([Kontrolki.Label("WDT"), numericUpDownNettoWDT]);
		siatkaObliczenia.DodajWiersz([Kontrolki.Label("WNT"), numericUpDownNettoWNT, numericUpDownNaleznyWNT, null, Kontrolki.Label("Do wpłaty"), null, numericUpDownDoWplaty]);
		siatkaObliczenia.DodajWiersz([Kontrolki.Label("Razem"), numericUpDownNettoRazem, numericUpDownNaleznyRazem, null, Kontrolki.Label("Do przeniesienia"), null, numericUpDownDoPrzeniesienia]);
		siatkaObliczenia.Dock = DockStyle.Fill;

		var dodajSprzedazDoDeklaracji = new DynamicznaAkcja<Faktura>("➕ Dodaj do deklaracji [INS]", kontekst =>
		{
			using var spis = new SpisZAkcjami<Faktura, FakturaSprzedazySpis>(new FakturaSprzedazySpis { CzyBezDeklaracjiVat = true });
			var faktura = Spisy.Wybierz(kontekst, spis, "Wybierz fakturę", default);
			if (faktura == null) return;
			faktura.DeklaracjaVatRef = Rekord;
			kontekst.Baza.Zapisz(faktura);
			Przelicz();
		}, Keys.Insert, Keys.None);

		var dodajZakupDoDeklaracji = new DynamicznaAkcja<Faktura>("➕ Dodaj do deklaracji [INS]", kontekst =>
		{
			using var spis = new SpisZAkcjami<Faktura, FakturaZakupuSpis>(new FakturaZakupuSpis { CzyBezDeklaracjiVat = true });
			var faktura = Spisy.Wybierz(kontekst, spis, "Wybierz fakturę", default);
			if (faktura == null) return;
			faktura.DeklaracjaVatRef = Rekord;
			kontekst.Baza.Zapisz(faktura);
			Przelicz();
		}, Keys.Insert, Keys.None);


		var usunZDeklaracji = new DynamicznaAkcja<Faktura>("❌ Usuń z deklaracji [DEL]", (kontekst, rekordy) =>
		{
			foreach (var rekord in rekordy)
			{
				rekord.DeklaracjaVatRef = default;
			}
			kontekst.Baza.Zapisz(rekordy);
			Przelicz();
		}, Keys.Delete, Keys.None);

		fakturySprzedazy = new SpisZAkcjami<Faktura, FakturaSprzedazySpis>(new FakturaSprzedazySpis(), new AkcjaNaSpisie<Faktura>[] { dodajSprzedazDoDeklaracji, new EdytujRekordAkcja<Faktura, FakturaEdytor>(), usunZDeklaracji, new WydrukFakturyAkcja(), new PrzeladujAkcja<Faktura>() });
		fakturyZakupu = new SpisZAkcjami<Faktura, FakturaZakupuSpis>(new FakturaZakupuSpis(), new AkcjaNaSpisie<Faktura>[] { dodajZakupDoDeklaracji, new EdytujRekordAkcja<Faktura, FakturaEdytor>(), usunZDeklaracji, new PrzeladujAkcja<Faktura>() });

		var zakladki = new Zakladki();
		zakladki.Dock = DockStyle.Fill;
		zakladki.Dodaj("Obliczenia", siatkaObliczenia);
		zakladki.Dodaj("Sprzedaż", fakturySprzedazy);
		zakladki.Dodaj("Zakup", fakturyZakupu);

		var siatkaUklad = new Siatka([0, 0, 0, -1], [0, -1]);
		siatkaUklad.DodajWiersz([Kontrolki.Label("Miesiąc"), dateTimePickerMiesiac, Kontrolki.Button("Przelicz", delegate { WybierzFaktury(); Przelicz(); })]);
		siatkaUklad.DodajWiersz([(zakladki, 4)]);
		siatkaUklad.Dock = DockStyle.Fill;

		Controls.Add(siatkaUklad);
		MinimumSize = Size = new Size(800, 425);
	}

	protected override void RekordGotowy()
	{
		base.RekordGotowy();

		fakturySprzedazy.Spis.DeklaracjaVatRef = Rekord;
		fakturySprzedazy.Spis.Kontekst = Kontekst;
		fakturyZakupu.Spis.DeklaracjaVatRef = Rekord;
		fakturyZakupu.Spis.Kontekst = Kontekst;
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
