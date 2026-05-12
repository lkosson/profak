using ProFak.DB;
using System.Data;

namespace ProFak.UI;

partial class SpisZAkcjami<TRekord> : Siatka, IKontrolkaZKontekstem
	where TRekord : Rekord<TRekord>
{
	protected readonly PanelAkcji panelAkcji;
	protected readonly Wyszukiwarka<TRekord> wyszukiwarka;
	protected readonly Podsumowanie podsumowanie;
	protected AdapterAkcji? domyslnaAkcja;
	protected readonly List<AdapterAkcji> adapteryAkcji;

	public Spis<TRekord> Spis { get; }
	public Kontekst Kontekst { get => Spis.Kontekst; set => Spis.Kontekst = value; }
	public int PreferowanaSzerokosc => (int)(Spis.PreferowanaSzerokosc + Spis.Margin.Right + panelAkcji.Width + panelAkcji.Margin.Right);

	public SpisZAkcjami(Spis<TRekord> spis, IEnumerable<AkcjaNaSpisie<TRekord>> akcje)
		: base([-1, 0], [-1])
	{
		adapteryAkcji = [];
		panelAkcji = new PanelAkcji();
		wyszukiwarka = new Wyszukiwarka<TRekord>(spis);
		podsumowanie = new Podsumowanie();
		Spis = spis;

		foreach (var akcja in akcje)
			DodajAkcje(akcja);

		spis.ZaznaczenieZmienione += spis_ZaznaczenieZmienione;
		spis.RekordyZmienione += spis_RekordyZmienione;
		spis.PokazMenuKontekstowe += PokazMenuKontekstowe;
		spis.ObsluzKlawisz += ObsluzKlawisz;
#if WINFORMS
		MinimumSize = new Size(panelAkcji.MinimumSize.Width + spis.MinimumSize.Width + panelAkcji.Margin.Left + spis.Margin.Right, Math.Max(panelAkcji.MinimumSize.Height, spis.MinimumSize.Height) + Math.Max(panelAkcji.Margin.Top, spis.Margin.Top) + Math.Max(panelAkcji.Margin.Bottom, spis.Margin.Bottom));
#endif
		DodajWiersz([spis, panelAkcji]);
	}

	private void spis_ZaznaczenieZmienione()
	{
		podsumowanie.Text = Spis.Podsumowanie;
		panelAkcji.Aktualizuj();
	}

	private void spis_RekordyZmienione()
	{
		podsumowanie.Text = Spis.Podsumowanie;
	}

	protected virtual bool ObsluzKlawisz(TKeys klawisz, TKeyModifiers modyfikatory)
	{
		if (klawisz == TKeys.Escape) { Zamknij(); return true; }
		else if (klawisz == TKeys.F3 || (klawisz == TKeys.F && modyfikatory == TKeyModifiers.Control)) { wyszukiwarka.Focus(); return true; }
		else if (klawisz == TKeys.Home && Spis.Rekordy.FirstOrDefault() is TRekord pierwszyRekord) { Spis.WybraneRekordy = [pierwszyRekord]; return true; }
		else if (klawisz == TKeys.End && Spis.Rekordy.LastOrDefault() is TRekord ostatniRekord) { Spis.WybraneRekordy = [ostatniRekord]; return true; }
		else if (klawisz == TKeys.Apps || (klawisz == TKeys.F10 && modyfikatory == TKeyModifiers.Shift)) { PokazMenuKontekstowe(); return true; }
		else return panelAkcji.ObsluzKlawisz(klawisz, modyfikatory);
	}

	private void PokazMenuKontekstowe()
	{
		var pozycje = new List<TMenuItem>();
		foreach (var adapter in adapteryAkcji.OrderBy(e => e.CzyDomyslna ? 0 : 1))
		{
			if (adapter.CzyGlobalna) continue;
			if (!adapter.CzyDostepna) continue;
			var pozycja = Kontrolki.MenuItem(adapter.NazwaBezSkrotu, adapter.Uruchom, skrot: adapter.Skrot);
			pozycje.Add(pozycja);
		}
		Kontrolki.Menu(pozycje.ToArray(), wyswietlDla: this);
	}

	public void DodajAkcje(AkcjaNaSpisie<TRekord> akcja, bool naPoczatku = false)
	{
		var adapter = akcja.UtworzAdapter(Spis);
		if (adapter.CzyDomyslna) domyslnaAkcja = adapter;
		if (naPoczatku) adapteryAkcji.Insert(0, adapter);
		else adapteryAkcji.Add(adapter);
	}
}

class SpisZAkcjami<TRekord, TSpis> : SpisZAkcjami<TRekord>
	where TRekord : Rekord<TRekord>
	where TSpis : Spis<TRekord>
{
	public new TSpis Spis { get; }

	public SpisZAkcjami(TSpis spis, IEnumerable<AkcjaNaSpisie<TRekord>>? akcje = null)
		: base(spis, akcje ?? [])
	{
		Spis = spis;
	}
}
