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
		private readonly DataGridViewTextBoxColumn kolumnaNazwaSprzedawcy;
		private readonly DataGridViewTextBoxColumn kolumnaNIPSprzedawcy;
		public Ref<Kontrahent> SprzedawcaRef { get; set; }
		public Ref<Towar> TowarRef { get; set; }

		public override string Podsumowanie
		{
			get
			{
				var podsumowanie = base.Podsumowanie;
				if (WybraneRekordy.Count() > 1)
				{
					podsumowanie += $"\nRazem netto: {WybraneRekordy.Sum(faktura => faktura.RazemNetto).ToString("n2")}";
					podsumowanie += $"\nRazem brutto: {WybraneRekordy.Sum(faktura => faktura.RazemBrutto).ToString("n2")}";
				}
				return podsumowanie;
			}
		}

		public FakturaZakupuSpis()
		{
			DodajKolumne(nameof(Faktura.Numer), "Numer");
			DodajKolumne(nameof(Faktura.RodzajFmt), "Rodzaj");
			DodajKolumne(nameof(Faktura.NumerPowiazanej), "Powiązana");
			DodajKolumne(nameof(Faktura.DataWystawienia), "Data wystawienia", format: "yyyy-MM-dd", szerokosc: 120);
			DodajKolumne(nameof(Faktura.DataSprzedazy), "Data sprzedaży", format: "yyyy-MM-dd", szerokosc: 120);
			kolumnaNazwaSprzedawcy = DodajKolumne(nameof(Faktura.NazwaSprzedawcy), "Sprzedawca", rozciagnij: true);
			kolumnaNIPSprzedawcy = DodajKolumne(nameof(Faktura.NIPSprzedawcy), "NIP sprzedawcy", szerokosc: 120);
			DodajKolumneKwota(nameof(Faktura.RazemNetto), "Netto");
			DodajKolumneKwota(nameof(Faktura.RazemVat), "VAT");
			DodajKolumneKwota(nameof(Faktura.RazemBrutto), "Brutto");
			DodajKolumne(nameof(Faktura.WalutaFmt), "Waluta", szerokosc: 70);
			DodajKolumneBool(nameof(Faktura.CzyZaplacona), "Zapł.", szerokosc: 50);
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
			kolumnaNazwaSprzedawcy.Visible = SprzedawcaRef.IsNull;
			kolumnaNIPSprzedawcy.Visible = SprzedawcaRef.IsNull;
			
			var q = Kontekst.Baza.Faktury
				.Include(faktura => faktura.Waluta)
				.Include(faktura => faktura.Wplaty)
				.Include(faktura => faktura.FakturaKorygowana)
				.Include(faktura => faktura.FakturaKorygujaca)
				.Where(faktura => faktura.Rodzaj == RodzajFaktury.Zakup || faktura.Rodzaj == RodzajFaktury.KorektaZakupu);
			if (SprzedawcaRef.IsNotNull) q = q.Where(faktura => faktura.SprzedawcaId == SprzedawcaRef.Id);
			if (odDaty.HasValue) q = q.Where(faktura => faktura.DataSprzedazy >= odDaty.Value);
			if (doDaty.HasValue) q = q.Where(faktura => faktura.DataSprzedazy < doDaty.Value);
			if (TowarRef.IsNotNull) q = q.Where(faktura => faktura.Pozycje.Any(pozycja => pozycja.TowarId == TowarRef.Id));
			Rekordy = q.ToList();
			if (doZaplaty) Rekordy = Rekordy.Where(faktura => !faktura.CzyZaplacona).ToList();
		}

		protected override void UstawStylWiersza(Faktura rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (!rekord.CzyZaplacona) styl.ForeColor = Color.FromArgb(240, 80, 40);
			else if (rekord.FakturaKorygujacaRef.IsNotNull) styl.ForeColor = Color.FromArgb(120, 120, 120);
			else if (rekord.Rodzaj == RodzajFaktury.KorektaZakupu) styl.ForeColor = Color.FromArgb(50, 60, 220);
		}
	}
}
