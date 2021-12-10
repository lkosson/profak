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
	class FakturaSprzedazySpis : Spis<Faktura>
	{
		private readonly DateTime? odDaty;
		private readonly DateTime? doDaty;

		public FakturaSprzedazySpis()
		{
			DodajKolumne(nameof(Faktura.Numer), "Numer");
			DodajKolumne(nameof(Faktura.DataWystawienia), "Data wystawienia", format: "yyyy-MM-dd", szerokosc: 120);
			DodajKolumne(nameof(Faktura.DataSprzedazy), "Data sprzedaży", format: "yyyy-MM-dd", szerokosc: 120);
			DodajKolumne(nameof(Faktura.NazwaNabywcy), "Nabywca", rozciagnij: true);
			DodajKolumne(nameof(Faktura.NIPNabywcy), "NIP nabywcy", szerokosc: 120);
			DodajKolumneKwota(nameof(Faktura.RazemNetto), "Netto");
			DodajKolumneKwota(nameof(Faktura.RazemVat), "VAT");
			DodajKolumneKwota(nameof(Faktura.RazemBrutto), "Brutto");
			DodajKolumne(nameof(Faktura.WalutaFmt), "Waluta", szerokosc: 70);
			DodajKolumneBool(nameof(Faktura.CzyZaplacona), "Zapł.", szerokosc: 50);
			DodajKolumneId();
		}

		public FakturaSprzedazySpis(string[] parametry)
			: this()
		{
			int? rok = null;
			int? miesiac = null;
			foreach (var parametr in parametry)
			{
				if (parametr.Length == 4) rok = Int32.Parse(parametr);
				if (parametr.Length >= 1 && parametr.Length <= 2) miesiac = Int32.Parse(parametr);
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
				.Where(faktura => faktura.Rodzaj == RodzajFaktury.Sprzedaż || faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży || faktura.Rodzaj == RodzajFaktury.Proforma);
			if (odDaty.HasValue) q = q.Where(faktura => faktura.DataSprzedazy >= odDaty.Value);
			if (doDaty.HasValue) q = q.Where(faktura => faktura.DataSprzedazy < doDaty.Value);
			Rekordy = q.ToList();
		}

		protected override void UstawStylWiersza(Faktura rekord, string kolumna, DataGridViewCellStyle styl)
		{
			base.UstawStylWiersza(rekord, kolumna, styl);
			if (rekord.Rodzaj == RodzajFaktury.Proforma) styl.ForeColor = Color.DarkGreen;
			else if (!rekord.CzyZaplacona) styl.ForeColor = Color.DarkRed;
			else if (rekord.FakturaKorygujacaRef.IsNotNull) styl.ForeColor = Color.Gray;
			else if (rekord.Rodzaj == RodzajFaktury.KorektaSprzedaży) styl.ForeColor = Color.DarkBlue;
		}
	}
}
