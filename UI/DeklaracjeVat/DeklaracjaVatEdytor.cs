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

			tabPageFakturySprzedazy.Controls.Add(fakturySprzedazy = Spisy.FakturySprzedazyBezAkcji());
			tabPageFakturyZakupu.Controls.Add(fakturyZakupu = Spisy.FakturyZakupuBezAkcji());
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

		}
	}

	class DeklaracjaVatEdytorBase : Edytor<DeklaracjaVat>
	{
	}
}
