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

		private void buttonPrzelicz_Click(object? sender, EventArgs e)
		{
			try
			{
				WybierzFaktury();
				Przelicz();
			}
			catch (Exception exc)
			{
				OknoBledu.Pokaz(exc);
			}
		}

		private void WybierzFaktury()
		{
			Rekord.WybierzFaktury(Kontekst.Baza);
			fakturySprzedazy.Spis.PrzeladujBezpiecznie();
			fakturyZakupu.Spis.PrzeladujBezpiecznie();
		}

		private void Przelicz()
		{
			Rekord.Przelicz(Kontekst.Baza);
			kontroler.AktualizujKontrolki();
		}
	}

	class DeklaracjaVatEdytorBase : Edytor<DeklaracjaVat>
	{
	}
}
