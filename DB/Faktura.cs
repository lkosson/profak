using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Faktura : Rekord<Faktura>
	{
		public string Numer { get; set; } = "";
		public DateTime DataWystawienia { get; set; } = DateTime.Now.Date;
		public DateTime DataSprzedazy { get; set; } = DateTime.Now.Date;
		public DateTime DataWprowadzenia { get; set; } = DateTime.Now.Date;
		public DateTime TerminPlatnosci { get; set; } = DateTime.Now.Date;
		public string NIPSprzedawcy { get; set; } = "";
		public string NazwaSprzedawcy { get; set; } = "";
		public string DaneSprzedawcy { get; set; } = "";
		public string NIPNabywcy { get; set; } = "";

		public string NazwaNabywcy { get; set; } = "";
		public string DaneNabywcy { get; set; } = "";
		public string RachunekBankowy { get; set; } = "";
		public string UwagiPubliczne { get; set; } = "";
		public string UwagiWewnetrzne { get; set; } = "";
		public decimal RazemNetto { get; set; }
		public decimal RazemVat { get; set; }
		public decimal RazemBrutto { get; set; }
		public decimal KursWaluty { get; set; }
		public string OpisSposobuPlatnosci { get; set; } = "";
		public RodzajFaktury Rodzaj { get; set; } = RodzajFaktury.Sprzedaż;
		public bool CzyWartosciReczne { get; set; }

		public decimal ProcentVatNaliczonego { get; set; }
		public decimal ProcentKosztow { get; set; }
		public bool CzyTP { get; set; }

		public int? SprzedawcaId { get; set; }
		public int? NabywcaId { get; set; }
		public int? FakturaKorygowanaId { get; set; }
		public int? FakturaKorygujacaId { get; set; }
		public int? WalutaId { get; set; }
		public int? SposobPlatnosciId { get; set; }

		public Ref<Kontrahent> SprzedawcaRef { get => SprzedawcaId; set => SprzedawcaId = value; }
		public Ref<Kontrahent> NabywcaRef { get => NabywcaId; set => NabywcaId = value; }
		public Ref<Faktura> FakturaKorygowanaRef { get => FakturaKorygowanaId; set => FakturaKorygowanaId = value; }
		public Ref<Faktura> FakturaKorygujacaRef { get => FakturaKorygujacaId; set => FakturaKorygujacaId = value; }
		public Ref<Waluta> WalutaRef { get => WalutaId; set => WalutaId = value; }
		public Ref<SposobPlatnosci> SposobPlatnosciRef { get => SposobPlatnosciId; set => SposobPlatnosciId = value; }

		public Kontrahent Sprzedawca { get; set; }
		public Kontrahent Nabywca { get; set; }
		public Faktura FakturaKorygowana { get; set; }
		public Faktura FakturaKorygujaca { get; set; }
		public Waluta Waluta { get; set; }
		public SposobPlatnosci SposobPlatnosci { get; set; }

		public List<PozycjaFaktury> Pozycje { get; set; }
		public List<Wplata> Wplaty { get; set; }
		public List<Plik> Pliki { get; set; }

		public decimal SumaWplat => Wplaty?.Sum(wplata => wplata.Kwota) ?? 0;
		public decimal PozostaloDoZaplaty => Math.Max(RazemBrutto - SumaWplat, 0);
		public bool CzyZaplacona => PozostaloDoZaplaty == 0;

		public decimal VatNaliczony => Zaokragl(RazemVat * ProcentVatNaliczonego / 100m);
		public decimal VatJakoKoszty => Zaokragl((RazemVat - VatNaliczony) * ProcentKosztow / 100m);
		public decimal NettoJakoKoszty => Zaokragl(RazemNetto * ProcentKosztow / 100m);
		public decimal Koszty => VatJakoKoszty + NettoJakoKoszty;

		public string WalutaFmt => Waluta?.Skrot;
		public PrzeznaczenieNumeratora Numerator => Rodzaj switch
		{
			RodzajFaktury.Sprzedaż => PrzeznaczenieNumeratora.Faktura,
			RodzajFaktury.Proforma => PrzeznaczenieNumeratora.Proforma,
			RodzajFaktury.KorektaSprzedaży => PrzeznaczenieNumeratora.Korekta,
			_ => (PrzeznaczenieNumeratora)(-1)
		};

		public bool CzySprzedaz => Rodzaj == RodzajFaktury.Sprzedaż || Rodzaj == RodzajFaktury.KorektaSprzedaży || Rodzaj == RodzajFaktury.Proforma;
		public bool CzyZakup => !CzySprzedaz;

		public void PrzeliczRazem(Baza baza)
		{
			var pozycje = baza.PozycjeFaktur.Where(pozycja => pozycja.FakturaId == Id).ToList();
			RazemNetto = pozycje.Sum(pozycja => pozycja.WartoscNetto);
			RazemVat = pozycje.Sum(pozycja => pozycja.WartoscVat);
			RazemBrutto = pozycje.Sum(pozycja => pozycja.WartoscBrutto);
		}

		internal IFormattable Podstawienie(string pole)
		{
			if (String.Equals(pole, "dzien", StringComparison.CurrentCultureIgnoreCase)) return DataWystawienia.Day;
			if (String.Equals(pole, "dzień", StringComparison.CurrentCultureIgnoreCase)) return DataWystawienia.Day;
			if (String.Equals(pole, "miesiac", StringComparison.CurrentCultureIgnoreCase)) return DataWystawienia.Month;
			if (String.Equals(pole, "miesiąc", StringComparison.CurrentCultureIgnoreCase)) return DataWystawienia.Month;
			if (String.Equals(pole, "rok", StringComparison.CurrentCultureIgnoreCase)) return DataWystawienia.Year;
			if (String.Equals(pole, "data", StringComparison.CurrentCultureIgnoreCase)) return DataWystawienia;
			return null;
		}

		public override bool CzyPasuje(string fraza)
			=> base.CzyPasuje(fraza)
			|| CzyPasuje(Numer, fraza)
			|| CzyPasuje(DataWystawienia, fraza)
			|| CzyPasuje(DataSprzedazy, fraza)
			|| CzyPasuje(DataWprowadzenia, fraza)
			|| CzyPasuje(TerminPlatnosci, fraza)
			|| CzyPasuje(NIPSprzedawcy, fraza)
			|| CzyPasuje(NazwaSprzedawcy, fraza)
			|| CzyPasuje(NIPNabywcy, fraza)
			|| CzyPasuje(NazwaNabywcy, fraza)
			|| CzyPasuje(DaneNabywcy, fraza)
			|| CzyPasuje(UwagiPubliczne, fraza)
			|| CzyPasuje(UwagiWewnetrzne, fraza)
			|| CzyPasuje(RazemNetto, fraza)
			|| CzyPasuje(RazemVat, fraza)
			|| CzyPasuje(RazemBrutto, fraza)
			|| CzyPasuje(KursWaluty, fraza)
			|| CzyPasuje(OpisSposobuPlatnosci, fraza)
			|| CzyPasuje(Rodzaj, fraza)
			|| CzyPasuje(CzyWartosciReczne ? "Ręczne" : "", fraza);
	}

	enum RodzajFaktury
	{
		Sprzedaż,
		Zakup,
		KorektaSprzedaży,
		KorektaZakupu,
		Proforma
	}
}
