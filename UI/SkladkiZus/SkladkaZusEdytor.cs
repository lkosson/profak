using ProFak.DB;

namespace ProFak.UI;

class SkladkaZusEdytor : Edytor<SkladkaZus>
{
	public SkladkaZusEdytor()
	{
		var dateTimePickerMiesiac = Kontrolki.DatePicker(tylkoMiesiac: true);
		var numericUpDownPodstawaSpoleczne = Kontrolki.NumericUpDown();
		var numericUpDownPodstawaZdrowotne = Kontrolki.NumericUpDown();
		var numericUpDownSkladkaEmerytalna = Kontrolki.NumericUpDown();
		var numericUpDownSkladkaRentowa = Kontrolki.NumericUpDown();
		var numericUpDownSkladkaWypadkowa = Kontrolki.NumericUpDown();
		var numericUpDownSkladkaSpoleczna = Kontrolki.NumericUpDown();
		var numericUpDownSkladkaZdrowotna = Kontrolki.NumericUpDown();
		var numericUpDownRozliczenieRoczneSkladkiZdrowotnej = Kontrolki.NumericUpDown();
		var numericUpDownFunduszPracy = Kontrolki.NumericUpDown();
		var numericUpDownSumaSkladek = Kontrolki.NumericUpDown();
		var numericUpDownOdliczenieOdDochodu = Kontrolki.NumericUpDown();

		kontroler.Powiazanie(dateTimePickerMiesiac, skladka => skladka.Miesiac);
		kontroler.Powiazanie(numericUpDownPodstawaSpoleczne, skladka => skladka.PodstawaSpoleczne);
		kontroler.Powiazanie(numericUpDownPodstawaZdrowotne, skladka => skladka.PodstawaZdrowotne);
		kontroler.Powiazanie(numericUpDownSkladkaEmerytalna, skladka => skladka.SkladkaEmerytalna);
		kontroler.Powiazanie(numericUpDownSkladkaRentowa, skladka => skladka.SkladkaRentowa);
		kontroler.Powiazanie(numericUpDownSkladkaWypadkowa, skladka => skladka.SkladkaWypadkowa);
		kontroler.Powiazanie(numericUpDownSkladkaSpoleczna, skladka => skladka.SkladkaSpoleczna);
		kontroler.Powiazanie(numericUpDownSkladkaZdrowotna, skladka => skladka.SkladkaZdrowotna);
		kontroler.Powiazanie(numericUpDownRozliczenieRoczneSkladkiZdrowotnej, skladka => skladka.RozliczenieRoczneSkladkiZdrowotnej);
		kontroler.Powiazanie(numericUpDownFunduszPracy, skladka => skladka.SkladkaFunduszPracy);
		kontroler.Powiazanie(numericUpDownSumaSkladek, skladka => skladka.SumaSkladek);
		kontroler.Powiazanie(numericUpDownOdliczenieOdDochodu, skladka => skladka.OdliczenieOdDochodu);

		var obliczenia = new DwieKolumny();
		obliczenia.DodajWiersz(numericUpDownPodstawaSpoleczne, "Składka społeczna - podstawa");
		obliczenia.DodajWiersz(numericUpDownPodstawaZdrowotne, "Składka zdrowotna - podstawa");
		obliczenia.DodajWiersz(numericUpDownSkladkaEmerytalna, "Składka emerytalna");
		obliczenia.DodajWiersz(numericUpDownSkladkaRentowa, "Składka rentowa");
		obliczenia.DodajWiersz(numericUpDownSkladkaWypadkowa, "Składka wypadkowa");
		obliczenia.DodajWiersz(numericUpDownSkladkaSpoleczna, "Składka społeczna - razem");
		obliczenia.DodajWiersz(numericUpDownSkladkaZdrowotna, "Składka zdrowotna");
		obliczenia.DodajWiersz(numericUpDownRozliczenieRoczneSkladkiZdrowotnej, "Składka zdrowotna - rozliczenie roczne");
		obliczenia.DodajWiersz(numericUpDownFunduszPracy, "Fundusz pracy");
		obliczenia.DodajWiersz(numericUpDownSumaSkladek, "Odliczenie od dochodu");
		obliczenia.DodajWiersz(numericUpDownOdliczenieOdDochodu, "Składki razem");

		var uklad = new Pionowo([
			new Poziomo([Kontrolki.Label("Miesiąc"), dateTimePickerMiesiac, Kontrolki.Button("Przelicz", Przelicz)]),
			obliczenia
			]);

		UstawZawartosc(uklad);
	}

	private void Przelicz()
	{
		Rekord.Przelicz(Kontekst.Baza);
		kontroler.AktualizujKontrolki();
	}
}
