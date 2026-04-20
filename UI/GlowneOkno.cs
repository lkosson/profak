using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace ProFak.UI;

partial class GlowneOkno
{
	private Menu menu;
	private Panel panelZawartosc;

	public GlowneOkno()
	{
		menu = new Menu(ZbudujMenu);

		panelZawartosc = new Panel();
		panelZawartosc.Margin = new Padding(0);

		var uklad = new Siatka([Wyglad.SzerokoscMenu, -1], [-1]);
		uklad.DodajWiersz([menu, panelZawartosc]);

		KeyPreview = true;
		WindowState = FormWindowState.Maximized;
		Text = "ProFak";
		Icon = Ikona;
		uklad.Dock = DockStyle.Fill;
		Controls.Add(uklad);
	}

	public static Icon? Ikona
	{
		get
		{
			var asm = Assembly.GetExecutingAssembly();
			using var dane = asm.GetManifestResourceStream("ProFak.ikona.ico");
			if (dane == null) return null;
			return new Icon(dane);
		}
	}

	private TTreeNode[] ZbudujMenu()
	{
		var fakturySprzedazyWszystkie = menu.UtworzWezel("Wszystkie", delegate { Wyswietl(Spisy.FakturySprzedazy(new())); });
		var fakturySprzedazyDoZaplaty = menu.UtworzWezel("Do zapłaty", delegate { Wyswietl(Spisy.FakturySprzedazy(new() { CzyDoZaplaty = true })); });
		var fakturySprzedazyZaplacone = menu.UtworzWezel("Zapłacone", delegate { Wyswietl(Spisy.FakturySprzedazy(new() { CzyZaplacone = true })); });
		var fakturySprzedazyKSeFDzis = menu.UtworzWezel("Dzisiejsze", delegate { Wyswietl(Spisy.KSeFSprzedaz(new() { CzySprzedaz = true, OdDaty = DateTime.Now.Date })); });
		var fakturySprzedazyKSeFMiesiac = menu.UtworzWezel("Z tego miesiąca", delegate { Wyswietl(Spisy.KSeFSprzedaz(new() { CzySprzedaz = true, OdDaty = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day) })); });
		var fakturySprzedazyKSeFPoprzedni = menu.UtworzWezel("Z tego i poprzedniego miesiąca", delegate { Wyswietl(Spisy.KSeFSprzedaz(new() { CzySprzedaz = true, OdDaty = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day).AddMonths(-1) })); });
		var fakturySprzedazyKSeFRok = menu.UtworzWezel("Z tego roku", delegate { Wyswietl(Spisy.KSeFSprzedaz(new() { CzySprzedaz = true, OdDaty = new DateTime(DateTime.Now.Year, 1, 1) })); });
		var fakturySprzedazyKSeFWszystkie = menu.UtworzWezel("Wszystkie", delegate { Wyswietl(Spisy.KSeFSprzedaz(new() { CzySprzedaz = true, OdDaty = KSeFSpis.DataStartowa })); });
		var fakturySprzedazyKSeF = menu.UtworzWezel("KSeF", [fakturySprzedazyKSeFDzis, fakturySprzedazyKSeFMiesiac, fakturySprzedazyKSeFPoprzedni, fakturySprzedazyKSeFRok, fakturySprzedazyKSeFWszystkie]);
		var fakturySprzedazyWedlugDaty = menu.UtworzWezel("Według daty", () => WypelnijDatyFaktur(faktura => faktura.Rodzaj == RodzajFaktury.Sprzedaż || faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży, (odDaty, doDaty) => Wyswietl(Spisy.FakturySprzedazy(new() { OdDaty = odDaty, DoDaty = doDaty }))));
		var fakturySprzedazyWedlugNabywcy = menu.UtworzWezel("Według nabywcy", () => WypelnijKontrahentowFaktur(faktura => faktura.Rodzaj == RodzajFaktury.Sprzedaż || faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży, faktura => faktura.Nabywca!, kontrahent => Wyswietl(Spisy.FakturySprzedazy(new() { KontrahentRef = kontrahent }))));
		var fakturySprzedazyWedlugTowaru = menu.UtworzWezel("Według towaru", () => WypelnijTowaryFaktur(pozycja => pozycja.Faktura!.Rodzaj == RodzajFaktury.Sprzedaż || pozycja.Faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży, towar => Wyswietl(Spisy.FakturySprzedazy(new() { TowarRef = towar }))));
		var fakturySprzedazy = menu.UtworzWezel("Faktury sprzedaży", [fakturySprzedazyWszystkie, fakturySprzedazyDoZaplaty, fakturySprzedazyZaplacone, fakturySprzedazyKSeF, fakturySprzedazyWedlugDaty, fakturySprzedazyWedlugNabywcy, fakturySprzedazyWedlugTowaru]);

		var rachunkiSprzedzazyWszystkie = menu.UtworzWezel("Wszystkie", delegate { Wyswietl(Spisy.RachunkiSprzedazy(new())); });
		var rachunkiSprzedazyWedlugDaty = menu.UtworzWezel("Według daty", () => WypelnijDatyFaktur(faktura => faktura.Rodzaj == RodzajFaktury.Rachunek || faktura.Rodzaj == RodzajFaktury.KorektaRachunku, (odDaty, doDaty) => Wyswietl(Spisy.RachunkiSprzedazy(new() { OdDaty = odDaty, DoDaty = doDaty }))));
		var rachunkiSprzedazyWedlugKontrahenta = menu.UtworzWezel("Według kontrahenta", () => WypelnijKontrahentowFaktur(faktura => faktura.Rodzaj == RodzajFaktury.Rachunek || faktura.Rodzaj == RodzajFaktury.KorektaRachunku, faktura => faktura.Nabywca!, kontrahent => Wyswietl(Spisy.RachunkiSprzedazy(new() { KontrahentRef = kontrahent }))));
		var rachunkiSprzedazyyDoZaplaty = menu.UtworzWezel("Do zapłaty", delegate { Wyswietl(Spisy.RachunkiSprzedazy(new() { CzyDoZaplaty = true })); });
		var rachunkiSprzedazyZaplacone = menu.UtworzWezel("Zapłacone", delegate { Wyswietl(Spisy.RachunkiSprzedazy(new() { CzyZaplacone = true })); });
		var rachunkiSprzedazy = menu.UtworzWezel("Rachunki", [rachunkiSprzedzazyWszystkie, rachunkiSprzedazyyDoZaplaty, rachunkiSprzedazyZaplacone, rachunkiSprzedazyWedlugDaty, rachunkiSprzedazyWedlugKontrahenta]);

		var fakturyProformaWszystkie = menu.UtworzWezel("Wszystkie", delegate { Wyswietl(Spisy.FakturyProforma(new())); });
		var fakturyProformaDoZaplaty = menu.UtworzWezel("Do zapłaty", delegate { Wyswietl(Spisy.FakturyProforma(new() { CzyDoZaplaty = true })); });
		var fakturyProformaZaplacone = menu.UtworzWezel("Zapłacone", delegate { Wyswietl(Spisy.FakturyProforma(new() { CzyZaplacone = true })); });
		var fakturyProformaWedlugDaty = menu.UtworzWezel("Według daty", () => WypelnijDatyFaktur(faktura => faktura.Rodzaj == RodzajFaktury.Proforma, (odDaty, doDaty) => Wyswietl(Spisy.FakturyProforma(new() { OdDaty = odDaty, DoDaty = doDaty }))));
		var fakturyProformaWedlugNabywcy = menu.UtworzWezel("Według nabywcy", () => WypelnijKontrahentowFaktur(faktura => faktura.Rodzaj == RodzajFaktury.Proforma, faktura => faktura.Nabywca!, kontrahent => Wyswietl(Spisy.FakturyProforma(new() { KontrahentRef = kontrahent }))));
		var fakturyProformaWedlugTowaru = menu.UtworzWezel("Według towaru", () => WypelnijTowaryFaktur(pozycja => pozycja.Faktura!.Rodzaj == RodzajFaktury.Proforma, towar => Wyswietl(Spisy.FakturyProforma(new() { TowarRef = towar }))));
		var fakturyProforma = menu.UtworzWezel("Faktury proforma", [fakturyProformaWszystkie, fakturyProformaDoZaplaty, fakturyProformaZaplacone, fakturyProformaWedlugDaty, fakturyProformaWedlugNabywcy, fakturyProformaWedlugTowaru]);

		var fakturyZakupuWszystkie = menu.UtworzWezel("Wszystkie", delegate { Wyswietl(Spisy.FakturyZakupu(new())); });
		var fakturyZakupuDoZaplaty = menu.UtworzWezel("Do zapłaty", delegate { Wyswietl(Spisy.FakturyZakupu(new() { CzyDoZaplaty = true })); });
		var fakturyZakupuZaplacone = menu.UtworzWezel("Zapłacone", delegate { Wyswietl(Spisy.FakturyZakupu(new() { CzyZaplacone = true })); });
		var fakturyZakupuKSeFPrzyrostowo = menu.UtworzWezel("Przyrostowo", delegate { Wyswietl(Spisy.KSeFZakup(new() { CzySprzedaz = false, OdDaty = default })); });
		var fakturyZakupuKSeFDzis = menu.UtworzWezel("Dzisiejsze", delegate { Wyswietl(Spisy.KSeFZakup(new() { CzySprzedaz = false, OdDaty = DateTime.Now.Date })); });
		var fakturyZakupuKSeFWczoraj = menu.UtworzWezel("Od wczoraj", delegate { Wyswietl(Spisy.KSeFZakup(new() { CzySprzedaz = false, OdDaty = DateTime.Now.Date.AddDays(-1) })); });
		var fakturyZakupuKSeFMiesiac = menu.UtworzWezel("Z tego miesiąca", delegate { Wyswietl(Spisy.KSeFZakup(new() { CzySprzedaz = false, OdDaty = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day) })); });
		var fakturyZakupuKSeFPoprzedni = menu.UtworzWezel("Z tego i poprzedniego miesiąca", delegate { Wyswietl(Spisy.KSeFZakup(new() { CzySprzedaz = false, OdDaty = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day).AddMonths(-1) })); });
		var fakturyZakupuKSeFRok = menu.UtworzWezel("Z tego roku", delegate { Wyswietl(Spisy.KSeFZakup(new() { CzySprzedaz = false, OdDaty = new DateTime(DateTime.Now.Year, 1, 1) })); });
		var fakturyZakupuKSeFWszystkie = menu.UtworzWezel("Wszystkie", delegate { Wyswietl(Spisy.KSeFZakup(new() { CzySprzedaz = false, OdDaty = KSeFSpis.DataStartowa })); });
		var fakturyZakupuKSeF = menu.UtworzWezel("KSeF", [fakturyZakupuKSeFPrzyrostowo, fakturyZakupuKSeFDzis, fakturyZakupuKSeFWczoraj, fakturyZakupuKSeFMiesiac, fakturyZakupuKSeFPoprzedni, fakturyZakupuKSeFRok, fakturyZakupuKSeFWszystkie]);
		var fakturyZakupuWedlugDaty = menu.UtworzWezel("Według daty", () => WypelnijDatyFaktur(faktura => faktura.Rodzaj == RodzajFaktury.Zakup || faktura.Rodzaj == RodzajFaktury.KorektaZakupu || faktura.Rodzaj == RodzajFaktury.DowódWewnętrzny, (odDaty, doDaty) => Wyswietl(Spisy.FakturyZakupu(new() { OdDaty = odDaty, DoDaty = doDaty }))));
		var fakturyZakupuWedlugSprzedawcy = menu.UtworzWezel("Według sprzedawcy", () => WypelnijKontrahentowFaktur(faktura => faktura.Rodzaj == RodzajFaktury.Zakup || faktura.Rodzaj == RodzajFaktury.KorektaZakupu || faktura.Rodzaj == RodzajFaktury.DowódWewnętrzny, faktura => faktura.Sprzedawca!, kontrahent => Wyswietl(Spisy.FakturyZakupu(new() { KontrahentRef = kontrahent }))));
		var fakturyZakupuWedlugTowaru = menu.UtworzWezel("Według towaru", () => WypelnijTowaryFaktur(pozycja => pozycja.Faktura!.Rodzaj == RodzajFaktury.Zakup || pozycja.Faktura.Rodzaj == RodzajFaktury.KorektaZakupu || pozycja.Faktura.Rodzaj == RodzajFaktury.DowódWewnętrzny, towar => Wyswietl(Spisy.FakturyZakupu(new() { TowarRef = towar }))));
		var fakturyZakupu = menu.UtworzWezel("Faktury zakupu", [fakturyZakupuWszystkie, fakturyZakupuDoZaplaty, fakturyZakupuZaplacone, fakturyZakupuKSeF, fakturyZakupuWedlugDaty, fakturyZakupuWedlugSprzedawcy, fakturyZakupuWedlugTowaru]);

		var faktury = menu.UtworzWezel("Faktury", [fakturySprzedazy, fakturyProforma, fakturyZakupu]);
		var deklaracjeVat = menu.UtworzWezel("Deklaracje Vat", WypelnijDeklaracjeVat);
		var skladkiZus = menu.UtworzWezel("Składki Zus", WypelnijSkladkiZus);
		var zaliczkiPit = menu.UtworzWezel("Zaliczki Pit", WypelnijZaliczkiPit);
		var podatki = menu.UtworzWezel("Podatki", [deklaracjeVat, skladkiZus, zaliczkiPit]);
		var kontrahenci = menu.UtworzWezel("Kontrahenci", delegate { Wyswietl(Spisy.Kontrahenci()); });
		var towary = menu.UtworzWezel("Towary", delegate { Wyswietl(Spisy.Towary()); });
		var jednostkiMiar = menu.UtworzWezel("Jednostki miar", delegate { Wyswietl(Spisy.JednostkiMiar()); });
		var sposobyPlatnosci = menu.UtworzWezel("Sposoby płatności", delegate { Wyswietl(Spisy.SposobyPlatnosci()); });
		var stawkiVat = menu.UtworzWezel("Stawki VAT", delegate { Wyswietl(Spisy.StawkiVat()); });
		var urzedySkarbowe = menu.UtworzWezel("Urzędy skarbowe", delegate { Wyswietl(Spisy.UrzedySkarbowe()); });
		var waluty = menu.UtworzWezel("Waluty", delegate { Wyswietl(Spisy.Waluty()); });
		var slowniki = menu.UtworzWezel("Słowniki", [jednostkiMiar, sposobyPlatnosci, stawkiVat, urzedySkarbowe, waluty]);
		var numeracja = menu.UtworzWezel("Numeracja", delegate { Wyswietl(Spisy.Numeratory()); });
		var konfiguracja = menu.UtworzWezel("Konfiguracja", KonfiguracjaEdytor.Wyswietl);
#if !SQLSERVER
		var bazaDanych = menu.UtworzWezel("Baza danych", delegate { Wyswietl(new BazyDanych()); });
#endif
		var usunieteFaktury = menu.UtworzWezel("Usunięte faktury", delegate { Wyswietl(Spisy.FakturyUsuniete()); });
		var polecenieSQL = menu.UtworzWezel("Polecenie SQL", delegate { Wyswietl(new EkranSQL()); });
		var bezposredniaEdycja = menu.UtworzWezel("Bezpośrednia edycja", delegate { Wyswietl(new EdytorTabeli()); });
		var oProgramie = menu.UtworzWezel("O programie", delegate { Wyswietl(new OProgramie()); });
#if SQLSERVER
		var serwisowe = menu.UtworzWezel("Serwisowe", [numeracja, konfiguracja, usunieteFaktury, polecenieSQL, bezposredniaEdycja, oProgramie]);
#else
		var serwisowe = menu.UtworzWezel("Serwisowe", [numeracja, konfiguracja, bazaDanych, usunieteFaktury, polecenieSQL, bezposredniaEdycja, oProgramie]);
#endif

		return [faktury, rachunkiSprzedazy, podatki, kontrahenci, towary, slowniki, serwisowe];
	}


	private TTreeNode[] WypelnijDatyFaktur(Expression<Func<Faktura, bool>> warunek, Action<DateTime, DateTime> akcja)
	{
		using var kontekst = new Kontekst();
		var daty = kontekst.Baza.Faktury
			.Where(warunek)
			.Select(faktura => faktura.DataSprzedazy)
			.Distinct()
			.ToList();

		var wezly = new List<TTreeNode>();

		var lata = daty.Append(DateTime.Now.Date).OrderBy(data => data).GroupBy(data => data.Year).Select(rok => (rok.Key, miesiace: rok.Select(data => data.Month).Distinct().ToList())).ToList();
		foreach (var (rok, miesiace) in lata)
		{
			void UruchomRok()
			{
				var odDaty = new DateTime(rok, 1, 1);
				var doDaty = odDaty.AddYears(1);
				akcja(odDaty, doDaty);
			}

			var wezlyMiesiace = new List<TTreeNode>();

			wezlyMiesiace.Add(menu.UtworzWezel("(cały rok)", UruchomRok));

			foreach (var miesiac in miesiace)
			{
				void UruchomMiesiac()
				{
					var odDaty = new DateTime(rok, miesiac, 1);
					var doDaty = odDaty.AddMonths(1);
					akcja(odDaty, doDaty);
				}
				wezlyMiesiace.Add(menu.UtworzWezel(new DateTime(rok, miesiac, 1).ToString("MMMM"), UruchomMiesiac));
			}

			wezly.Add(menu.UtworzWezel(rok.ToString(), wezlyMiesiace.ToArray()));
		}

		return wezly.ToArray();
	}

	private TTreeNode[] WypelnijKontrahentowFaktur(Expression<Func<Faktura, bool>> warunek, Expression<Func<Faktura, Kontrahent>> pole, Action<Kontrahent> akcja)
	{
		using var kontekst = new Kontekst();
		var kontrahenci = kontekst.Baza.Faktury
			.Where(warunek)
			.Include(pole)
			.Select(pole)
			.Distinct()
			.Where(kontrahent => !kontrahent.CzyArchiwalny)
			.OrderBy(kontrahent => kontrahent.Nazwa)
			.ToList();

		var wezly = new List<TTreeNode>();

		foreach (var kontrahent in kontrahenci)
		{
			wezly.Add(menu.UtworzWezel(kontrahent.Nazwa, delegate { akcja(kontrahent); }));
		}

		return wezly.ToArray();
	}

	private TTreeNode[] WypelnijTowaryFaktur(Expression<Func<PozycjaFaktury, bool>> warunek, Action<Towar> akcja)
	{
		using var kontekst = new Kontekst();
		var towary = kontekst.Baza.PozycjeFaktur
			.Where(warunek)
			.Include(pozycja => pozycja.Towar)
			.Select(pozycja => pozycja.Towar!)
			.Distinct()
			.Where(towar => !towar.CzyArchiwalny)
			.OrderBy(towar => towar.Nazwa)
			.ToList();

		var wezly = new List<TTreeNode>();

		foreach (var towar in towary)
		{
			wezly.Add(menu.UtworzWezel(towar.Nazwa, delegate { akcja(towar); }));
		}

		return wezly.ToArray();
	}

	private TTreeNode[] WypelnijDeklaracjeVat()
	{
		using var kontekst = new Kontekst();
		var daty = kontekst.Baza.DeklaracjeVat
			.Select(faktura => faktura.Miesiac)
			.Distinct()
			.ToList();
		return WypelnijWedlugLat(daty, rok => Wyswietl(Spisy.DeklaracjeVat(rok)));
	}

	private TTreeNode[] WypelnijSkladkiZus()
	{
		using var kontekst = new Kontekst();
		var daty = kontekst.Baza.SkladkiZus
			.Select(faktura => faktura.Miesiac)
			.Distinct()
			.ToList();
		return WypelnijWedlugLat(daty, rok => Wyswietl(Spisy.SkladkiZus(rok)));
	}

	private TTreeNode[] WypelnijZaliczkiPit()
	{
		using var kontekst = new Kontekst();
		var daty = kontekst.Baza.ZaliczkiPit
			.Select(faktura => faktura.Miesiac)
			.Distinct()
			.ToList();
		return WypelnijWedlugLat(daty, rok => Wyswietl(Spisy.ZaliczkiPit(rok)));
	}

	private TTreeNode[] WypelnijWedlugLat(IEnumerable<DateTime> daty, Action<int?> akcja)
	{
		var wezly = new List<TTreeNode>();

		var lata = daty.Append(DateTime.Now.Date).Select(data => data.Year).Distinct().OrderBy(rok => rok).ToList();
		wezly.Add(menu.UtworzWezel("(wszystkie)", delegate { akcja(null); }));
		foreach (var rok in lata)
		{
			wezly.Add(menu.UtworzWezel(rok.ToString(), delegate { akcja(rok); }));
		}

		return wezly.ToArray();
	}
}
