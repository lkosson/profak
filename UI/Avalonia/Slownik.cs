#if AVALONIA
using Avalonia.Data;
using Avalonia.Input;
using ProFak.DB;

namespace ProFak.UI;

class Slownik<T>
	where T : Rekord<T>
{
	private readonly Kontekst kontekst;
	private readonly TComboBox? comboBox;
	private readonly TSuggestBox? suggestBox;
	private readonly TButton? button;
	private readonly Func<IEnumerable<T>> pobierzWartosci;
	private readonly Func<T, string> wyswietlanaWartosc;
	private readonly Action<T?> ustawWartosc;
	private readonly Func<SpisZAkcjami<T>> generatorSpisu;
	private bool gotowy;
	private bool ustawionaNowaWartosc;
	private bool dopuscPustaWartosc;

	public Slownik(Kontekst kontekst, TComboBox comboBox, TButton? button, Func<IEnumerable<T>> pobierzWartosci, Func<T, string> wyswietlanaWartosc, Action<T?> ustawWartosc, Func<SpisZAkcjami<T>> generatorSpisu, bool dopuscPustaWartosc = false)
	{
		this.kontekst = kontekst;
		this.comboBox = comboBox;
		this.button = button;
		this.pobierzWartosci = pobierzWartosci;
		this.wyswietlanaWartosc = wyswietlanaWartosc;
		this.ustawWartosc = ustawWartosc;
		this.generatorSpisu = generatorSpisu;
		this.dopuscPustaWartosc = dopuscPustaWartosc;
	}

	public Slownik(Kontekst kontekst, TSuggestBox suggestBox, TButton? button, Func<IEnumerable<T>> pobierzWartosci, Func<T, string> wyswietlanaWartosc, Action<T?> ustawWartosc, Func<SpisZAkcjami<T>> generatorSpisu, bool dopuscPustaWartosc = false)
	{
		this.kontekst = kontekst;
		this.suggestBox = suggestBox;
		this.button = button;
		this.pobierzWartosci = pobierzWartosci;
		this.wyswietlanaWartosc = wyswietlanaWartosc;
		this.ustawWartosc = ustawWartosc;
		this.generatorSpisu = generatorSpisu;
		this.dopuscPustaWartosc = dopuscPustaWartosc;
	}

	public void Zainstaluj()
	{
		WypelnijListe();
		if (comboBox != null)
		{
			comboBox.SelectedIndex = -1;
			comboBox.SelectionChanged += SelectionChanged;
			comboBox.AttachedToVisualTree += AttachedToVisualTree;
			comboBox.KeyDown += KeyDown;
		}
		if (suggestBox != null)
		{
			suggestBox.SelectionChanged += SelectionChanged;
			suggestBox.AttachedToVisualTree += AttachedToVisualTree;
			suggestBox.KeyDown += KeyDown;
		}
		if (button != null) button.Click += button_Click;
		gotowy = true;
	}

	public void Przeladuj()
	{
		var wybranaWartosc = comboBox?.SelectedItem ?? suggestBox?.SelectedItem;
		gotowy = false;
		WypelnijListe();
		gotowy = true;
		comboBox?.SelectedItem = wybranaWartosc;
		suggestBox?.SelectedItem = wybranaWartosc;
	}

	private void KeyDown(object? sender, Avalonia.Input.KeyEventArgs e)
	{
		if (e.Key == Key.F2) PokazSpis();
		if (dopuscPustaWartosc && (e.Key == Key.Delete || e.Key == Key.Back))
		{
			comboBox?.SelectedIndex = -1;
			suggestBox?.SelectedItem = null;
		}
	}

	private void AttachedToVisualTree(object? sender, Avalonia.VisualTreeAttachmentEventArgs e)
	{
		gotowy = true;
	}

	private void PokazSpis()
	{
		var dotychczasowaPozycja = (PozycjaListyRekordu<T>?)(comboBox?.SelectedItem ?? suggestBox?.SelectedItem);
		using var spis = generatorSpisu();
		var wartosc = Spisy.Wybierz(kontekst, spis, "Wybierz pozycję", dotychczasowaPozycja?.Wartosc);
		if (wartosc == null) return;
		gotowy = false;
		WypelnijListe();
		comboBox?.SelectedIndex = -1;
		suggestBox?.SelectedItem = null;
		gotowy = true;
		ustawionaNowaWartosc = false;
		var pozycje = comboBox?.ItemsSource?.Cast<PozycjaListyRekordu<T>>() ?? suggestBox?.ItemsSource?.Cast<PozycjaListyRekordu<T>>() ?? [];
		var nowaPozycja = pozycje.FirstOrDefault(p => p.Wartosc == wartosc);
		if (nowaPozycja != null)
		{
			comboBox.SelectedItem = nowaPozycja;
			suggestBox.SelectedItem = nowaPozycja;
		}
		if (!ustawionaNowaWartosc) ustawWartosc(wartosc);
	}

	private void button_Click(object? sender, EventArgs e)
	{
		PokazSpis();
	}

	private void SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
	{
		if (!gotowy) return;
		var pozycja = comboBox?.SelectedItem ?? suggestBox?.SelectedItem;
		var wartosc = ((PozycjaListyRekordu<T>?)pozycja)?.Wartosc;
		ustawWartosc(wartosc);
		//if (comboBox.Focused && comboBox.DropDownStyle == ComboBoxStyle.DropDown) comboBox.Focus();
		ustawionaNowaWartosc = true;
	}

	private void WypelnijListe()
	{
		var dostepneWartosci = pobierzWartosci();

		var pozycje = new List<PozycjaListyRekordu<T>>(dostepneWartosci.Count() + 1);
		if (dopuscPustaWartosc) pozycje.Add(new PozycjaListyRekordu<T> { Wartosc = null, Opis = "" });
		foreach (var wartosc in dostepneWartosci)
		{
			var opis = wyswietlanaWartosc(wartosc);
			if (String.IsNullOrEmpty(opis)) continue;
			var pozycja = new PozycjaListyRekordu<T> { Wartosc = wartosc, Opis = opis };
			pozycje.Add(pozycja);
		}

		if (comboBox != null)
		{
			comboBox.SelectedValueBinding = CompiledBinding.Create<PozycjaListyRekordu<T>, Ref<T>>(pozycja => pozycja.Ref);
			comboBox.DisplayMemberBinding = CompiledBinding.Create<PozycjaListyRekordu<T>, string>(pozycja => pozycja.Opis);
			comboBox.ItemsSource = pozycje;
		}
		if (suggestBox != null)
		{
			suggestBox.ValueMemberBinding = CompiledBinding.Create<PozycjaListyRekordu<T>, string>(pozycja => pozycja.Opis);
			suggestBox.ItemsSource = pozycje;
		}
	}
}
#endif
