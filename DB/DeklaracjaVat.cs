using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class DeklaracjaVat : Rekord<DeklaracjaVat>
	{
		public DateTime Miesiac { get; set; } = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day).AddMonths(-1);

		public decimal NettoZW { get; set; }
		public decimal Netto0 { get; set; }
		public decimal Netto5 { get; set; }
		public decimal Netto8 { get; set; }
		public decimal Netto23 { get; set; }
		public decimal NettoWDT { get; set; }
		public decimal NettoWNT { get; set; }

		public decimal Nalezny5 { get; set; }
		public decimal Nalezny8 { get; set; }
		public decimal Nalezny23 { get; set; }
		public decimal NaleznyWNT { get; set; }

		public decimal NettoSrodkiTrwale { get; set; }
		public decimal NettoPozostale { get; set; }

		public decimal NaliczonyPrzeniesiony { get; set; }
		public decimal NaliczonySrodkiTrwale { get; set; }
		public decimal NaliczonyPozostale { get; set; }

		public string MiesiacFmt => Miesiac.ToString("MM/yyyy");

		public decimal NettoRazem => NettoZW + Netto0 + Netto5 + Netto8 + Netto23 + NettoWDT + NettoWNT;
		public decimal NaleznyRazem => Nalezny5 + Nalezny8 + Nalezny23 + NaleznyWNT;
		public decimal NaliczonyRazem => NaliczonyPrzeniesiony + NaliczonySrodkiTrwale + NaliczonyPozostale;

		public decimal DoWplaty => Math.Max(NaleznyRazem - NaliczonyRazem, 0);
		public decimal DoPrzeniesienia => Math.Max(NaliczonyRazem - NaleznyRazem, 0);

		public List<Faktura> Faktury { get; set; }

		public override bool CzyPasuje(string fraza)
			=> base.CzyPasuje(fraza)
			|| CzyPasuje(Miesiac, fraza)
			|| CzyPasuje(NettoZW, fraza)
			|| CzyPasuje(Netto0, fraza)
			|| CzyPasuje(Netto5, fraza)
			|| CzyPasuje(Netto8, fraza)
			|| CzyPasuje(Netto23, fraza)
			|| CzyPasuje(NettoWDT, fraza)
			|| CzyPasuje(NettoWNT, fraza)
			|| CzyPasuje(Nalezny5, fraza)
			|| CzyPasuje(Nalezny8, fraza)
			|| CzyPasuje(Nalezny23, fraza)
			|| CzyPasuje(NaleznyWNT, fraza)
			|| CzyPasuje(NettoSrodkiTrwale, fraza)
			|| CzyPasuje(NettoPozostale, fraza)
			|| CzyPasuje(NaliczonyPrzeniesiony, fraza)
			|| CzyPasuje(NaliczonySrodkiTrwale, fraza)
			|| CzyPasuje(NaliczonyPozostale, fraza)
			|| CzyPasuje(NettoRazem, fraza)
			|| CzyPasuje(NaleznyRazem, fraza)
			|| CzyPasuje(NaliczonyRazem, fraza)
			|| CzyPasuje(DoWplaty, fraza)
			|| CzyPasuje(DoPrzeniesienia, fraza);
	}
}
