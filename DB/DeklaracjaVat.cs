using Microsoft.EntityFrameworkCore;

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

		public List<Faktura> Faktury { get; set; } = default!;

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

		public void WybierzFaktury(Baza baza)
		{
			var nieaktualneFaktury = baza.Faktury.Where(faktura => faktura.DeklaracjaVatId == Id).ToDictionary(faktura => faktura.Ref);
			var zmienioneFaktury = new List<Faktura>();

			var faktury = baza.Faktury
				.Where(faktura => faktura.DataSprzedazy < Miesiac.Date.AddMonths(1) && (faktura.DeklaracjaVatId == null || faktura.DeklaracjaVatId == Id))
				.ToList();

			foreach (var faktura in faktury)
			{
				if (!nieaktualneFaktury.Remove(faktura))
				{
					faktura.DeklaracjaVatRef = this;
					zmienioneFaktury.Add(faktura);
				}
			}

			foreach (var faktura in nieaktualneFaktury.Values)
			{
				faktura.DeklaracjaVatRef = default;
				zmienioneFaktury.Add(faktura);
			}

			baza.Zapisz(zmienioneFaktury);
		}

		public void Przelicz(Baza baza)
		{
			NettoZW = 0;
			Netto0 = 0;
			Netto5 = 0;
			Netto8 = 0;
			Netto23 = 0;
			NettoWDT = 0;
			NettoWNT = 0;

			Nalezny5 = 0;
			Nalezny8 = 0;
			Nalezny23 = 0;
			NaleznyWNT = 0;

			NettoSrodkiTrwale = 0;
			NettoPozostale = 0;

			NaliczonyPrzeniesiony = 0;
			NaliczonySrodkiTrwale = 0;
			NaliczonyPozostale = 0;

			var poprzedniaDeklaracja = baza.DeklaracjeVat
				.Where(deklaracja => deklaracja.Miesiac < Miesiac)
				.OrderByDescending(deklaracja => deklaracja.Miesiac)
				.FirstOrDefault();

			if (poprzedniaDeklaracja != null) NaliczonyPrzeniesiony = poprzedniaDeklaracja.DoPrzeniesienia;

			var faktury = baza.Faktury
				.Where(faktura => faktura.DeklaracjaVatId == Id)
				.Include(faktura => faktura.Pozycje).ThenInclude(pozycja => pozycja.StawkaVat)
				.ToList();

			foreach (var faktura in faktury)
			{
				if (faktura.CzySprzedaz)
				{
					foreach (var pozycja in faktura.Pozycje)
					{
						if (pozycja.StawkaVat == null) continue;
						if (faktura.CzyWDT) { NettoWDT += pozycja.WartoscNetto * faktura.KursWaluty; }
						else if (pozycja.StawkaVat.Skrot.ToLower().Contains("zw")) { NettoZW += pozycja.WartoscNetto * faktura.KursWaluty; }
						else if (pozycja.StawkaVat.Wartosc == 0) { Netto0 += pozycja.WartoscNetto * faktura.KursWaluty; }
						else if (pozycja.StawkaVat.Wartosc <= 5) { Netto5 += pozycja.WartoscNetto * faktura.KursWaluty; Nalezny5 += pozycja.WartoscVat * faktura.KursWaluty; }
						else if (pozycja.StawkaVat.Wartosc <= 8) { Netto8 += pozycja.WartoscNetto * faktura.KursWaluty; Nalezny8 += pozycja.WartoscVat * faktura.KursWaluty; }
						else { Netto23 += pozycja.WartoscNetto * faktura.KursWaluty; Nalezny23 += pozycja.WartoscVat * faktura.KursWaluty; }
					}
				}
				else if (faktura.CzyZakup)
				{
					if (faktura.CzyWNT) { NettoWNT += faktura.RazemNetto * faktura.KursWaluty; NaleznyWNT += faktura.VatNaliczony * faktura.KursWaluty; }
					/* bez else */
					if (faktura.CzyZakupSrodkowTrwalych) { NettoSrodkiTrwale += faktura.RazemNetto * faktura.KursWaluty; NaliczonySrodkiTrwale += faktura.VatNaliczony * faktura.KursWaluty; }
					else { NettoPozostale += faktura.RazemNetto * faktura.KursWaluty; NaliczonyPozostale += faktura.VatNaliczony * faktura.KursWaluty; }
				}
			}

			NettoZW = NettoZW.Zaokragl();
			Netto0 = Netto0.Zaokragl();
			Netto5 = Netto5.Zaokragl();
			Netto8 = Netto8.Zaokragl();
			Netto23 = Netto23.Zaokragl();
			NettoWDT = NettoWDT.Zaokragl();
			NettoWNT = NettoWNT.Zaokragl();

			Nalezny5 = Nalezny5.Zaokragl();
			Nalezny8 = Nalezny8.Zaokragl();
			Nalezny23 = Nalezny23.Zaokragl();
			NaleznyWNT = NaleznyWNT.Zaokragl();

			NettoSrodkiTrwale = NettoSrodkiTrwale.Zaokragl();
			NettoPozostale = NettoPozostale.Zaokragl();

			NaliczonyPrzeniesiony = NaliczonyPrzeniesiony.Zaokragl();
			NaliczonySrodkiTrwale = NaliczonySrodkiTrwale.Zaokragl();
			NaliczonyPozostale = NaliczonyPozostale.Zaokragl();
		}
	}
}
