using ProFak.DB;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ProFak.UI;

class Wyglad
{
	private static Regex nazwaPlusSkrot = new Regex(@"(?<nazwa>[^[]+)(\[(?<skrot>.+)\])?");

	public static bool SkrotyKlawiaturoweAkcji { get; set; }
	public static bool SkrotyKlawiaturoweZakladek { get; set; }
	public static bool SkrotyKlawiaturowePrzyciskow { get; set; }
	public static bool IkonyAkcji { get; set; }
	public static bool DomyslnyPodgladStrony { get; set; }
	public static bool PotwierdzanieZamknieciaEdytora { get; set; }
	public static bool PotwierdzanieZamknieciaProgramu { get; set; }
	public static bool WstepneLadowanieReportingServices { get; set; }
	public static int SzerokoscMenu { get; set; }
	public static int? RozmiarCzcionki { get; set; }
	public static string? NazwaCzcionki { get; set; }

	public static string NazwaAkcji(AdapterAkcji adapter)
	{
		var nazwa = SkrotyKlawiaturoweAkcji ? adapter.Nazwa : adapter.NazwaBezSkrotu;
		if (!IkonyAkcji) nazwa = NazwaBezIkony(nazwa);
		return nazwa;
	}

	public static string NazwaBezIkony(string nazwa)
	{
		var idx = 0;
		while (idx < nazwa.Length && (!Char.IsLetterOrDigit(nazwa[idx]) || Char.IsWhiteSpace(nazwa[idx]))) idx++;
		return nazwa.Substring(idx);
	}

	public static void UsunSkrotyZakladek(TabControl tabControl)
	{
		if (SkrotyKlawiaturoweZakladek) return;
		foreach (TabPage zakladka in tabControl.TabPages)
		{
			zakladka.Text = NazwaBezSkrotu(zakladka.Text).Trim();
		}
	}

	public static void UsunIkonePrzycisku(Button przycisk)
	{
		if (IkonyAkcji) return;
		przycisk.Text = NazwaBezIkony(przycisk.Text);
	}

	public static void UsunSkrotPrzycisku(Button przycisk)
	{
		if (SkrotyKlawiaturowePrzyciskow) return;
		przycisk.Text = NazwaBezSkrotu(przycisk.Text);
	}

	public static string NazwaBezSkrotu(string tekst)
	{
		var dopasowanie = nazwaPlusSkrot.Match(tekst);
		if (!dopasowanie.Success) return tekst;
		return dopasowanie.Groups["nazwa"].Value;
	}

	public static string Skrot(string tekst)
	{
		var dopasowanie = nazwaPlusSkrot.Match(tekst);
		if (!dopasowanie.Groups["skrot"].Success) return "";
		return dopasowanie.Groups["skrot"].Value;
	}

	private static void UstawCzcionke()
	{
		if (!RozmiarCzcionki.HasValue) return;
		try
		{
			if (!String.IsNullOrEmpty(NazwaCzcionki)) Application.SetDefaultFont(new Font(NazwaCzcionki, RozmiarCzcionki.Value));
			else Application.SetDefaultFont(new Font(SystemFonts.MessageBoxFont!.FontFamily, RozmiarCzcionki.Value, SystemFonts.MessageBoxFont.Style));
		}
		catch (InvalidOperationException)
		{
			// Application.SetDefaultFont nie działa jeśli było już wyświetlone jakiekolwiek okno
		}
	}

	public static int PrzeskalujRozmiar(int rozmiarPx)
	{
		if (!RozmiarCzcionki.HasValue) return rozmiarPx;
		return (int)(rozmiarPx * RozmiarCzcionki.Value / SystemFonts.MessageBoxFont!.Size);
	}

	public static void ZaladujDomyslny()
	{
		UstawNaPodstawieKonfiguracji(Konfiguracja.Domyslna);
	}

	public static void WczytajZBazy()
	{
		using var kontekst = new Kontekst();
		var konfiguracja = kontekst.Baza.Konfiguracja.First();
		UstawNaPodstawieKonfiguracji(konfiguracja);
	}

	public static void UstawNaPodstawieKonfiguracji(Konfiguracja konfiguracja)
	{
		if (konfiguracja.Wersja < 1) return;
		SkrotyKlawiaturoweAkcji = konfiguracja.SkrotyKlawiaturoweAkcji;
		SkrotyKlawiaturoweZakladek = konfiguracja.SkrotyKlawiaturoweZakladek;
		SkrotyKlawiaturowePrzyciskow = konfiguracja.SkrotyKlawiaturowePrzyciskow;
		IkonyAkcji = konfiguracja.IkonyAkcji;
		DomyslnyPodgladStrony = konfiguracja.DomyslnyPodgladStrony;
		PotwierdzanieZamknieciaEdytora = konfiguracja.PotwierdzanieZamknieciaEdytora;
		PotwierdzanieZamknieciaProgramu = konfiguracja.PotwierdzanieZamknieciaProgramu;
		WstepneLadowanieReportingServices = konfiguracja.WstepneLadowanieReportingServices;
		SzerokoscMenu = konfiguracja.SzerokoscMenu;
		RozmiarCzcionki = konfiguracja.RozmiarCzcionki == 0 ? null : konfiguracja.RozmiarCzcionki;
		NazwaCzcionki = konfiguracja.NazwaCzcionki;
		UstawCzcionke();
	}
}
