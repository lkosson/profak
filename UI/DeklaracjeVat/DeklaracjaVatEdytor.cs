using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	partial class DeklaracjaVatEdytor : DeklaracjaVatEdytorBase
	{
		private readonly SpisZAkcjami<Faktura, FakturaSprzedazySpis> fakturySprzedazy;
		private readonly SpisZAkcjami<Faktura, FakturaZakupuSpis> fakturyZakupu;

		public DeklaracjaVatEdytor()
		{
			InitializeComponent();

			kontroler.Powiazanie(dateTimePickerMiesiac, deklaracja => deklaracja.Miesiac);
			kontroler.Powiazanie(numericUpDownNettoZW, deklaracja => deklaracja.NettoZW);
			kontroler.Powiazanie(numericUpDownNetto0, deklaracja => deklaracja.Netto0);
			kontroler.Powiazanie(numericUpDownNetto5, deklaracja => deklaracja.Netto5);
			kontroler.Powiazanie(numericUpDownNetto8, deklaracja => deklaracja.Netto8);
			kontroler.Powiazanie(numericUpDownNetto23, deklaracja => deklaracja.Netto23);
			kontroler.Powiazanie(numericUpDownNettoWDT, deklaracja => deklaracja.NettoWDT);
			kontroler.Powiazanie(numericUpDownNettoWNT, deklaracja => deklaracja.NettoWNT);
			kontroler.Powiazanie(numericUpDownNalezny5, deklaracja => deklaracja.Nalezny5);
			kontroler.Powiazanie(numericUpDownNalezny8, deklaracja => deklaracja.Nalezny8);
			kontroler.Powiazanie(numericUpDownNalezny23, deklaracja => deklaracja.Nalezny23);
			kontroler.Powiazanie(numericUpDownNaleznyWNT, deklaracja => deklaracja.NaleznyWNT);
			kontroler.Powiazanie(numericUpDownNettoSrodkiTrwale, deklaracja => deklaracja.NettoSrodkiTrwale);
			kontroler.Powiazanie(numericUpDownNettoPozostale, deklaracja => deklaracja.NettoPozostale);
			kontroler.Powiazanie(numericUpDownNaliczonyPrzeniesiony, deklaracja => deklaracja.NaliczonyPrzeniesiony);
			kontroler.Powiazanie(numericUpDownNaliczonySrodkiTrwale, deklaracja => deklaracja.NaliczonySrodkiTrwale);
			kontroler.Powiazanie(numericUpDownNaliczonyPozostale, deklaracja => deklaracja.NaliczonyPozostale);
			kontroler.Powiazanie(numericUpDownNettoRazem, deklaracja => deklaracja.NettoRazem);
			kontroler.Powiazanie(numericUpDownNaleznyRazem, deklaracja => deklaracja.NaleznyRazem);
			kontroler.Powiazanie(numericUpDownNaliczonyRazem, deklaracja => deklaracja.NaliczonyRazem);
			kontroler.Powiazanie(numericUpDownDoWplaty, deklaracja => deklaracja.DoWplaty);
			kontroler.Powiazanie(numericUpDownDoPrzeniesienia, deklaracja => deklaracja.DoPrzeniesienia);

			var dodajSprzedazDoDeklaracji = new DynamicznaAkcja<Faktura>("➕ Dodaj do deklaracji [INS]", kontekst =>
			{
				using var spis = new SpisZAkcjami<Faktura, FakturaSprzedazySpis>(new FakturaSprzedazySpis { CzyBezDeklaracjiVat = true });
				var faktura = Spisy.Wybierz(kontekst, spis, "Wybierz fakturę", default);
				if (faktura == null) return;
				faktura.DeklaracjaVatRef = Rekord;
				kontekst.Baza.Zapisz(faktura);
				Przelicz();
			}, Keys.Insert, Keys.None);

			var dodajZakupDoDeklaracji = new DynamicznaAkcja<Faktura>("➕ Dodaj do deklaracji [INS]", kontekst =>
			{
				using var spis = new SpisZAkcjami<Faktura, FakturaZakupuSpis>(new FakturaZakupuSpis { CzyBezDeklaracjiVat = true });
				var faktura = Spisy.Wybierz(kontekst, spis, "Wybierz fakturę", default);
				if (faktura == null) return;
				faktura.DeklaracjaVatRef = Rekord;
				kontekst.Baza.Zapisz(faktura);
				Przelicz();
			}, Keys.Insert, Keys.None);


			var usunZDeklaracji = new DynamicznaAkcja<Faktura>("❌ Usuń z deklaracji [DEL]", (kontekst, rekordy) =>
			{
				foreach (var rekord in rekordy)
				{
					rekord.DeklaracjaVatRef = default;
				}
				kontekst.Baza.Zapisz(rekordy);
				Przelicz();
			}, Keys.Delete, Keys.None);

			fakturySprzedazy = new SpisZAkcjami<Faktura, FakturaSprzedazySpis>(new FakturaSprzedazySpis(), new AkcjaNaSpisie<Faktura>[] { dodajSprzedazDoDeklaracji, new EdytujRekordAkcja<Faktura, FakturaEdytor>(), usunZDeklaracji, new WydrukFakturyAkcja(), new PrzeladujAkcja<Faktura>()});
			fakturyZakupu = new SpisZAkcjami<Faktura, FakturaZakupuSpis>(new FakturaZakupuSpis(), new AkcjaNaSpisie<Faktura>[] { dodajZakupDoDeklaracji, new EdytujRekordAkcja<Faktura, FakturaEdytor>(), usunZDeklaracji, new PrzeladujAkcja<Faktura>()});

			tabPageFakturySprzedazy.Controls.Add(fakturySprzedazy);
			tabPageFakturyZakupu.Controls.Add(fakturyZakupu);
		}

		protected override void RekordGotowy()
		{
			base.RekordGotowy();

			fakturySprzedazy.Spis.DeklaracjaVatRef = Rekord;
			fakturySprzedazy.Spis.Kontekst = Kontekst;
			fakturyZakupu.Spis.DeklaracjaVatRef = Rekord;
			fakturyZakupu.Spis.Kontekst = Kontekst;
		}

		private void buttonPrzelicz_Click(object sender, EventArgs e)
		{
			WybierzFaktury();
			Przelicz();
		}

		private void WybierzFaktury()
		{
			var nieaktualneFaktury = Kontekst.Baza.Faktury.Where(faktura => faktura.DeklaracjaVatId == Rekord.Id).ToDictionary(faktura => faktura.Ref);
			var zmienioneFaktury = new List<Faktura>();

			var faktury = Kontekst.Baza.Faktury
				.Where(faktura => faktura.DataSprzedazy < Rekord.Miesiac.Date.AddMonths(1) && (faktura.DeklaracjaVatId == null || faktura.DeklaracjaVatId == Rekord.Id))
				.ToList();

			foreach (var faktura in faktury)
			{
				if (!nieaktualneFaktury.Remove(faktura))
				{
					faktura.DeklaracjaVatRef = Rekord;
					zmienioneFaktury.Add(faktura);
				}
			}

			foreach (var faktura in nieaktualneFaktury.Values)
			{
				faktura.DeklaracjaVatRef = default;
				zmienioneFaktury.Add(faktura);
			}

			Kontekst.Baza.Zapisz(zmienioneFaktury);

			fakturySprzedazy.Spis.PrzeladujBezpiecznie();
			fakturyZakupu.Spis.PrzeladujBezpiecznie();
		}

		private void Przelicz()
		{
			Rekord.NettoZW = 0;
			Rekord.Netto0 = 0;
			Rekord.Netto5 = 0;
			Rekord.Netto8 = 0;
			Rekord.Netto23 = 0;
			Rekord.NettoWDT = 0;
			Rekord.NettoWNT = 0;

			Rekord.Nalezny5 = 0;
			Rekord.Nalezny8 = 0;
			Rekord.Nalezny23 = 0;
			Rekord.NaleznyWNT = 0;

			Rekord.NettoSrodkiTrwale = 0;
			Rekord.NettoPozostale = 0;

			Rekord.NaliczonyPrzeniesiony = 0;
			Rekord.NaliczonySrodkiTrwale = 0;
			Rekord.NaliczonyPozostale = 0;

			var poprzedniaDeklaracja = Kontekst.Baza.DeklaracjeVat
				.Where(deklaracja => deklaracja.Miesiac < Rekord.Miesiac)
				.OrderByDescending(deklaracja => deklaracja.Miesiac)
				.FirstOrDefault();

			if (poprzedniaDeklaracja != null) Rekord.NaliczonyPrzeniesiony = poprzedniaDeklaracja.DoPrzeniesienia;

			var faktury = Kontekst.Baza.Faktury
				.Where(faktura => faktura.DeklaracjaVatId == Rekord.Id)
				.Include(faktura => faktura.Pozycje).ThenInclude(pozycja => pozycja.StawkaVat)
				.ToList();

			foreach (var faktura in faktury)
			{
				if (faktura.CzySprzedaz)
				{
					foreach (var pozycja in faktura.Pozycje)
					{
						if (pozycja.StawkaVat == null) continue;
						if (faktura.CzyWDT) { Rekord.NettoWDT += pozycja.WartoscNetto; }
						else if (pozycja.StawkaVat.Skrot.ToLower().Contains("zw")) { Rekord.NettoZW += pozycja.WartoscNetto; }
						else if (pozycja.StawkaVat.Wartosc == 0) { Rekord.Netto0 += pozycja.WartoscNetto; }
						else if (pozycja.StawkaVat.Wartosc <= 5) { Rekord.Netto5 += pozycja.WartoscNetto; Rekord.Nalezny5 += pozycja.WartoscVat; }
						else if (pozycja.StawkaVat.Wartosc <= 8) { Rekord.Netto8 += pozycja.WartoscNetto; Rekord.Nalezny8 += pozycja.WartoscVat; }
						else { Rekord.Netto23 += pozycja.WartoscNetto; Rekord.Nalezny23 += pozycja.WartoscVat; }
					}
				}
				else if (faktura.CzyZakup)
				{
					if (faktura.CzyWNT) { Rekord.NettoWNT += faktura.RazemNetto; Rekord.NaleznyWNT += faktura.VatNaliczony; }
					/* bez else */
					if (faktura.CzyZakupSrodkowTrwalych) { Rekord.NettoSrodkiTrwale += faktura.RazemNetto; Rekord.NaliczonySrodkiTrwale += faktura.VatNaliczony; }
					else { Rekord.NettoPozostale += faktura.RazemNetto; Rekord.NaliczonyPozostale += faktura.VatNaliczony; }
				}
			}

			Rekord.NettoZW = Decimal.Round(Rekord.NettoZW, MidpointRounding.AwayFromZero);
			Rekord.Netto0 = Decimal.Round(Rekord.Netto0, MidpointRounding.AwayFromZero);
			Rekord.Netto5 = Decimal.Round(Rekord.Netto5, MidpointRounding.AwayFromZero);
			Rekord.Netto8 = Decimal.Round(Rekord.Netto8, MidpointRounding.AwayFromZero);
			Rekord.Netto23 = Decimal.Round(Rekord.Netto23, MidpointRounding.AwayFromZero);
			Rekord.NettoWDT = Decimal.Round(Rekord.NettoWDT, MidpointRounding.AwayFromZero);
			Rekord.NettoWNT = Decimal.Round(Rekord.NettoWNT, MidpointRounding.AwayFromZero);

			Rekord.Nalezny5 = Decimal.Round(Rekord.Nalezny5, MidpointRounding.AwayFromZero);
			Rekord.Nalezny8 = Decimal.Round(Rekord.Nalezny8, MidpointRounding.AwayFromZero);
			Rekord.Nalezny23 = Decimal.Round(Rekord.Nalezny23, MidpointRounding.AwayFromZero);
			Rekord.NaleznyWNT = Decimal.Round(Rekord.NaleznyWNT, MidpointRounding.AwayFromZero);

			Rekord.NettoSrodkiTrwale = Decimal.Round(Rekord.NettoSrodkiTrwale, MidpointRounding.AwayFromZero);
			Rekord.NettoPozostale = Decimal.Round(Rekord.NettoPozostale, MidpointRounding.AwayFromZero);

			Rekord.NaliczonyPrzeniesiony = Decimal.Round(Rekord.NaliczonyPrzeniesiony, MidpointRounding.AwayFromZero);
			Rekord.NaliczonySrodkiTrwale = Decimal.Round(Rekord.NaliczonySrodkiTrwale, MidpointRounding.AwayFromZero);
			Rekord.NaliczonyPozostale = Decimal.Round(Rekord.NaliczonyPozostale, MidpointRounding.AwayFromZero);

			kontroler.AktualizujKontrolki();
		}
	}

	class DeklaracjaVatEdytorBase : Edytor<DeklaracjaVat>
	{
	}
}
