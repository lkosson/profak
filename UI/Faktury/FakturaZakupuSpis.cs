using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class FakturaZakupuSpis : Spis<Faktura>
	{
		private readonly DateTime? odDaty;
		private readonly DateTime? doDaty;
		private readonly bool doZaplaty;
		private readonly bool zaplacone;
		protected readonly DataGridViewTextBoxColumn kolumnaNazwaSprzedawcy;
		protected readonly DataGridViewTextBoxColumn kolumnaNIPSprzedawcy;
		public Ref<Kontrahent> SprzedawcaRef { get; set; }
		public Ref<Towar> TowarRef { get; set; }
		public Ref<DeklaracjaVat> DeklaracjaVatRef { get; set; }
		public Ref<ZaliczkaPit> ZaliczkaPitRef { get; set; }
		public bool CzyBezDeklaracjiVat { get; set; }
		public bool CzyBezZaliczkiPit { get; set; }

		public override string Podsumowanie
		{
			get
			{
				var podsumowanie = base.Podsumowanie;
				if (WybraneRekordy.Count() > 1)
				{
					podsumowanie += $"\nRazem netto: {WybraneRekordy.Sum(faktura => faktura.RazemNetto).ToString(Format.Kwota)}";
					podsumowanie += $"\nRazem VAT: {WybraneRekordy.Sum(faktura => faktura.RazemVat).ToString(Format.Kwota)}";
					podsumowanie += $"\nRazem brutto: {WybraneRekordy.Sum(faktura => faktura.RazemBrutto).ToString(Format.Kwota)}";
				}
				return podsumowanie;
			}
		}

		public FakturaZakupuSpis()
		{
			DodajKolumne(nameof(Faktura.Numer), "Numer");
			DodajKolumne(nameof(Faktura.RodzajFmt), "Rodzaj");
			DodajKolumneData(nameof(Faktura.DataWystawienia), "Data wystawienia");
			DodajKolumneData(nameof(Faktura.DataSprzedazy), "Data sprzedaży");
			DodajKolumneData(nameof(Faktura.DataWprowadzenia), "Data wprowadzenia");
			kolumnaNazwaSprzedawcy = DodajKolumne(nameof(Faktura.NazwaSprzedawcy), "Sprzedawca", szerokosc: 250);
			kolumnaNIPSprzedawcy = DodajKolumne(nameof(Faktura.NIPSprzedawcy), "NIP sprzedawcy", szerokosc: 100);
			DodajKolumneKwota(nameof(Faktura.RazemNetto), "Netto");
			DodajKolumneKwota(nameof(Faktura.RazemVat), "VAT");
			DodajKolumneKwota(nameof(Faktura.RazemBrutto), "Brutto");
			DodajKolumne(nameof(Faktura.WalutaFmt), "Waluta", szerokosc: 70);
			DodajKolumneBool(nameof(Faktura.CzyZaplacona), "Zapł.", szerokosc: 50, tooltip: faktura => faktura.SumaWplat.ToString(Format.Kwota));
			DodajKolumneBool(nameof(Faktura.CzyKSeF), "KSeF", szerokosc: 50, tooltip: faktura => faktura.NumerKSeF);
			DodajKolumneBool(nameof(Faktura.CzyPliki), "Pliki", szerokosc: 50, tooltip: faktura => String.Join("\n", faktura.Pliki.Select(e => e.Nazwa)));
			DodajKolumne(nameof(Faktura.PozycjeFmt), "Pozycje", szerokosc: 150);
			DodajKolumne(nameof(Faktura.UwagiPubliczne), "Uwagi (publiczne)", szerokosc: 150);
			DodajKolumne(nameof(Faktura.UwagiWewnetrzne), "Uwagi (wewnętrzne)", szerokosc: 150);
			DodajKolumne(nameof(Faktura.OpisZdarzenia), "Opis KPiR", szerokosc: 150);
			DodajKolumneKwota(nameof(Faktura.SumaWplat), "Zapłacono");
			DodajKolumneKwota(nameof(Faktura.PozostaloDoZaplaty), "Do zapłaty");
			DodajKolumneKwota(nameof(Faktura.RazemRabat), "Rabat");
			DodajKolumneData(nameof(Faktura.TerminPlatnosci), "Termin płatności");
			DodajKolumne(nameof(Faktura.DniPoTerminie), "Dni po terminie");
			DodajKolumne(nameof(Faktura.NumerPowiazanej), "Powiązana");
			DodajKolumne(nameof(Faktura.NumerKSeF), "Numer KSeF", szerokosc: 230);
			DodajKolumneBool(nameof(Faktura.CzyTP), "TP", szerokosc: 50);
			DodajKolumneBool(nameof(Faktura.CzyWNT), "WNT", szerokosc: 50);
			DodajKolumneId();
		}

		public FakturaZakupuSpis(string[] parametry)
			: this()
		{
			if (parametry == null) return;
			int? rok = null;
			int? miesiac = null;
			foreach (var parametr in parametry)
			{
				if (parametr.StartsWith("R:")) rok = Int32.Parse(parametr[2..]);
				else if (parametr.StartsWith("M:")) miesiac = Int32.Parse(parametr[2..]);
				else if (parametr.StartsWith("K:")) SprzedawcaRef = Int32.Parse(parametr[2..]);
				else if (parametr.StartsWith("T:")) TowarRef = Int32.Parse(parametr[2..]);
				else if (parametr == "DoZaplaty") doZaplaty = true;
				else if (parametr == "Zaplacone") zaplacone = true;
			}
			if (!rok.HasValue) return;
			if (miesiac.HasValue)
			{
				odDaty = new DateTime(rok.Value, miesiac.Value, 1);
				doDaty = odDaty.Value.AddMonths(1);
			}
			else
			{
				odDaty = new DateTime(rok.Value, 1, 1);
				doDaty = odDaty.Value.AddYears(1);
			}
		}

		protected override void Przeladuj()
		{
			var q = Kontekst.Baza.Faktury
				.Include(faktura => faktura.Waluta)
				.Include(faktura => faktura.Wplaty)
				.Include(faktura => faktura.FakturaKorygowana)
				.Include(faktura => faktura.FakturaKorygujaca)
				.Include(faktura => faktura.Pozycje)
				.Include(faktura => faktura.Pliki)
				.Where(faktura => faktura.Rodzaj == RodzajFaktury.Zakup || faktura.Rodzaj == RodzajFaktury.KorektaZakupu || faktura.Rodzaj == RodzajFaktury.DowódWewnętrzny);
			if (SprzedawcaRef.IsNotNull) q = q.Where(faktura => faktura.SprzedawcaId == SprzedawcaRef.Id);
			if (odDaty.HasValue) q = q.Where(faktura => faktura.DataSprzedazy >= odDaty.Value);
			if (doDaty.HasValue) q = q.Where(faktura => faktura.DataSprzedazy < doDaty.Value);
			if (TowarRef.IsNotNull) q = q.Where(faktura => faktura.Pozycje.Any(pozycja => pozycja.TowarId == TowarRef.Id));
			if (DeklaracjaVatRef.IsNotNull) q = q.Where(faktura => faktura.DeklaracjaVatId == DeklaracjaVatRef.Id);
			if (ZaliczkaPitRef.IsNotNull) q = q.Where(faktura => faktura.ZaliczkaPitId == ZaliczkaPitRef.Id);
			if (CzyBezDeklaracjiVat) q = q.Where(faktura => faktura.DeklaracjaVatId == null);
			if (CzyBezZaliczkiPit) q = q.Where(faktura => faktura.ZaliczkaPitId == null);
			q = q.OrderBy(faktura => faktura.DataWystawienia).ThenBy(faktura => faktura.Id);
			Rekordy = q.ToList();
			if (doZaplaty) Rekordy = Rekordy.Where(faktura => !faktura.CzyZaplacona).ToList();
			if (zaplacone) Rekordy = Rekordy.Where(faktura => faktura.CzyZaplacona).ToList();
		}

		protected override void UstawStylWiersza(Faktura rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.DniPoTerminie > 0) styl.ForeColor = Color.FromArgb(160, 20, 20);
			else if (!rekord.CzyZaplacona) styl.ForeColor = Color.FromArgb(240, 80, 40);
			else if (rekord.FakturaKorygujacaRef.IsNotNull) styl.ForeColor = Color.FromArgb(120, 120, 120);
			else if (rekord.Rodzaj == RodzajFaktury.KorektaZakupu) styl.ForeColor = Color.FromArgb(50, 60, 220);
			else if (rekord.Rodzaj == RodzajFaktury.DowódWewnętrzny) styl.ForeColor = Color.FromArgb(220, 60, 220);
		}
	}

	class FakturaZakupuBezSprzedawcySpis : FakturaZakupuSpis
	{
		public FakturaZakupuBezSprzedawcySpis()
		{
			Columns.Remove(kolumnaNazwaSprzedawcy);
			Columns.Remove(kolumnaNIPSprzedawcy);
		}
	}
}
