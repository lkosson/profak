using ProFak.DB;
using System.Data;

namespace ProFak.UI;

class SpisZAkcjami<TRekord> : Siatka, IKontrolkaZKontekstem
	where TRekord : Rekord<TRekord>
{
	protected readonly PanelAkcji panelAkcji;
	protected readonly Wyszukiwarka<TRekord> wyszukiwarka;
	protected readonly Podsumowanie podsumowanie;
	protected AdapterAkcji? domyslnaAkcja;
	protected readonly List<AkcjaNaSpisie<TRekord>> akcje;
	protected readonly List<AdapterAkcji> adapteryAkcji;

	public Spis<TRekord> Spis { get; }
	public List<AkcjaNaSpisie<TRekord>> Akcje => akcje;
	public Kontekst Kontekst { get => Spis.Kontekst; set => Spis.Kontekst = value; }
	public int PreferowanaSzerokosc => Spis.PreferowanaSzerokosc + Spis.Margin.Right + panelAkcji.Width + panelAkcji.Margin.Right;

	public SpisZAkcjami(Spis<TRekord> spis)
		: base([-1, 0], [-1])
	{
		akcje = new List<AkcjaNaSpisie<TRekord>>();
		adapteryAkcji = [];
		panelAkcji = new PanelAkcji();
		wyszukiwarka = new Wyszukiwarka<TRekord>(spis);
		podsumowanie = new Podsumowanie();

		Spis = spis;

		spis.ZaznaczenieZmienione += spis_ZaznaczenieZmienione;
		spis.RekordyZmienione += spis_RekordyZmienione;
		spis.PokazMenuKontekstowe += PokazMenuKontekstowe;
		spis.ObsluzKlawisz += ObsluzKlawisz;
		Controls.Add(spis, 0, 0);
		MinimumSize = new Size(panelAkcji.MinimumSize.Width + spis.MinimumSize.Width + panelAkcji.Margin.Left + spis.Margin.Right, Math.Max(panelAkcji.MinimumSize.Height, spis.MinimumSize.Height) + Math.Max(panelAkcji.Margin.Top, spis.Margin.Top) + Math.Max(panelAkcji.Margin.Bottom, spis.Margin.Bottom));

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

	protected virtual bool ObsluzKlawisz(Keys klawisz, Keys modyfikatory)
	{
		if (klawisz == Keys.Escape) { Dispose(); return true; }
		else if (klawisz == Keys.F3 || (klawisz == Keys.F && modyfikatory == Keys.Control)) { wyszukiwarka.Focus(); return true; }
		else if (klawisz == Keys.Home && Spis.Rekordy.FirstOrDefault() is TRekord pierwszyRekord) { Spis.WybraneRekordy = [pierwszyRekord]; return true; }
		else if (klawisz == Keys.End && Spis.Rekordy.LastOrDefault() is TRekord ostatniRekord) { Spis.WybraneRekordy = [ostatniRekord]; return true; }
		else if (klawisz == Keys.Apps || (klawisz == Keys.F10 && modyfikatory == Keys.Shift)) { PokazMenuKontekstowe(); return true; }
		else return panelAkcji.ObsluzKlawisz(klawisz, modyfikatory);
	}

	private void PokazMenuKontekstowe()
	{
		var menu = ZbudujMenuKontekstowe();
		menu.Closed += delegate
		{
			BeginInvoke(delegate { menu.Dispose(); });
		};
		menu.Show(Cursor.Position);
	}

	protected virtual ContextMenuStrip ZbudujMenuKontekstowe()
	{
		var pozycje = new List<TMenuItem>();
		foreach (var adapter in adapteryAkcji.OrderBy(e => e.CzyDomyslna ? 0 : 1))
		{
			if (adapter.CzyGlobalna) continue;
			if (!adapter.CzyDostepna) continue;
			var pozycja = Kontrolki.MenuItem(adapter.NazwaBezSkrotu, adapter.Uruchom, skrot: adapter.Skrot);
			pozycje.Add(pozycja);
		}
		var menu = Kontrolki.Menu(pozycje.ToArray());
		return menu;
	}

	protected override void OnGotFocus(EventArgs e)
	{
		base.OnGotFocus(e);
		Spis.Focus();
	}

	protected override void OnCreateControl()
	{
		foreach (var akcja in akcje)
		{
			var adapter = akcja.UtworzAdapter(Spis);
			if (adapter.CzyDomyslna && domyslnaAkcja == null) domyslnaAkcja = adapter;
			adapteryAkcji.Add(adapter);
		}

		panelAkcji.CzyGlownySpis = Spis.Kontekst.Dialog == null || Spis.Kontekst.Dialog is not DialogEdycji;
		panelAkcji.SuspendLayout();
		panelAkcji.DodajKontrolke(wyszukiwarka);
		foreach (var adapter in adapteryAkcji)
		{
			panelAkcji.DodajAkcje(adapter);
		}
		panelAkcji.DodajKontrolke(podsumowanie);
		panelAkcji.ResumeLayout();
		base.OnCreateControl();
	}
}

class SpisZAkcjami<TRekord, TSpis> : SpisZAkcjami<TRekord>
	where TRekord : Rekord<TRekord>
	where TSpis : Spis<TRekord>
{
	public new TSpis Spis { get; }

	public SpisZAkcjami(TSpis spis, IEnumerable<AkcjaNaSpisie<TRekord>>? akcje = null)
		: base(spis)
	{
		Spis = spis;
		if (akcje != null) Akcje.AddRange(akcje);
	}
}
