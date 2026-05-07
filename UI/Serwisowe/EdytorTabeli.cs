using ProFak.DB;
using System.Data;

namespace ProFak.UI;

partial class EdytorTabeli : Edytor, IKontrolkaZKontekstem
{
	public Kontekst Kontekst { get; set; } = default!;

	private readonly TButton buttonWczytaj;
	private readonly TTextBox textBoxStatus;
	private readonly Spis spis;
	private readonly TComboBox comboBoxTabela;
	private readonly TNumericUpDown numericUpDownIDDo;
	private readonly TNumericUpDown numericUpDownIDOd;

	public EdytorTabeli()
	{
		comboBoxTabela = Kontrolki.DropDownList();
		numericUpDownIDOd = Kontrolki.NumericUpDown(poPrzecinku: 0);
		numericUpDownIDDo = Kontrolki.NumericUpDown(poPrzecinku: 0);
		buttonWczytaj = Kontrolki.Button("Wczytaj", Wczytaj);
		textBoxStatus = Kontrolki.TextBox();
		spis = new SpisEdytowalny(Edycja, Usuniecie);

		numericUpDownIDOd.Value = 0;
		numericUpDownIDDo.Value = 999999999;

		var zakres = new Siatka([0, 0, 0, 0, 0, 0, -1], []);
		zakres.DodajWiersz([comboBoxTabela, Kontrolki.Label("ID="), numericUpDownIDOd, Kontrolki.Label("..."), numericUpDownIDDo, buttonWczytaj, textBoxStatus]);

		var uklad = new Siatka([-1], [0, -1]);
		uklad.DodajWiersz([new Grupa("Zakres danych", zakres)]);
		uklad.DodajWiersz([new Grupa("Wynik", spis)]);

		var tabele = new[]
		{
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Deklaracje Vat", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.DeklaracjeVat) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Dodatkowe podmioty", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.DodatkowePodmioty) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Faktury", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.Faktury) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Jednostki miar", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.JednostkiMiar) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Kolumny spisów", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.KolumnySpisow) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Konfiguracja", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.Konfiguracja) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Kontrahenci", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.Kontrahenci) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Numeratory", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.Numeratory) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Pliki", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.Pliki) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Pozycje faktur", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.PozycjeFaktur) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Składki Zus", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.SkladkiZus) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Sposoby płatności", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.SposobyPlatnosci) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Stany menu", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.StanyMenu) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Stany numeratorów", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.StanyNumeratorow) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Stawki Vat", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.StawkiVat) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Towary", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.Towary) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Urzędy skarbowe", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.UrzedySkarbowe) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Waluty", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.Waluty) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Wpłaty", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.Wplaty) },
			new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Zawartości", Wartosc = GeneratorDanych(kontekst => kontekst.Baza.Zawartosci) }
		};

		var kontroler = new Kontroler<object>();
		kontroler.Slownik(comboBoxTabela, tabele);

		UstawZawartosc(uklad);
	}

	private Func<IEnumerable<object>> GeneratorDanych<T>(Func<Kontekst, IQueryable<T>> generatorTabeli)
		where T : DB.Rekord<T>
	{
		return delegate
		{
			var idOd = (int?)numericUpDownIDOd.Value;
			var idDo = (int?)numericUpDownIDDo.Value;
			return generatorTabeli(Kontekst).Where(rekord => rekord.Id >= idOd && rekord.Id <= idDo).Cast<object>().ToList();
		};
	}

	private void Wczytaj()
	{
		try
		{
			if (comboBoxTabela.SelectedValue is not Func<IEnumerable<object>> generator) return;
			var dane = generator();
			spis.DataSource = dane;
			textBoxStatus.Text = "Liczba pozycji: " + dane.Count();
		}
		catch (Exception exc)
		{
			textBoxStatus.Text = exc.GetType() + ": " + exc.Message;
		}
	}

	private void Edycja(object wiersz)
	{
		try
		{
			if (wiersz is not Rekord rekord) return;
			textBoxStatus.Text = "Zapisywanie ...";
			using var nowyKontekst = new Kontekst(Kontekst);
			using var tx = nowyKontekst.Transakcja();
			Kontekst.Baza.Zapisz(rekord);
			tx.Zatwierdz();
			textBoxStatus.Text = "Zapisano zmianę.";
		}
		catch (Exception exc)
		{
			textBoxStatus.Text = exc.GetType() + ": " + exc.Message;
		}
	}

	private void Usuniecie(object[] wiersze)
	{
		try
		{
			textBoxStatus.Text = "Kasowanie ...";
			using var nowyKontekst = new Kontekst(Kontekst);
			using var tx = nowyKontekst.Transakcja();
			foreach (var rekord in wiersze.OfType<Rekord>())
			{
				Kontekst.Baza.Usun(rekord);
			}
			tx.Zatwierdz();
			Wczytaj();
			if (wiersze.Length == 1) textBoxStatus.Text = "Usunięto rekord.";
			else textBoxStatus.Text = "Usunięto rekordy : " + wiersze.Length;
		}
		catch (Exception exc)
		{
			textBoxStatus.Text = exc.GetType() + ": " + exc.Message;
		}
	}
}
