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
			_ => PrzeznaczenieNumeratora.Faktura
		};

		public bool CzySprzedaz => Rodzaj == RodzajFaktury.Sprzedaż || Rodzaj == RodzajFaktury.KorektaSprzedaży || Rodzaj == RodzajFaktury.Proforma;
		public bool CzyZakup => !CzySprzedaz;

		public void PrzeliczRazem(Baza baza)
		{
			var pozycje = baza.PozycjeFaktur.Where(pozycja => pozycja.FakturaId == Id).ToList();
			PrzeliczRazem(pozycje);
		}

		public void PrzeliczRazem(IEnumerable<PozycjaFaktury> pozycje)
		{
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

		public Faktura PrzygotujKorekte(Baza baza)
		{
			var bazowa = this;

			while (bazowa.FakturaKorygujacaRef.IsNotNull)
			{
				bazowa = baza.Znajdz(bazowa.FakturaKorygujacaRef);
			}

			var korekta = new Faktura();
			baza.Zapisz(korekta);
			if (bazowa.Rodzaj == RodzajFaktury.Sprzedaż || bazowa.Rodzaj == RodzajFaktury.KorektaSprzedaży) korekta.Rodzaj = RodzajFaktury.KorektaSprzedaży;
			else if (bazowa.Rodzaj == RodzajFaktury.Zakup || bazowa.Rodzaj == RodzajFaktury.KorektaZakupu) korekta.Rodzaj = RodzajFaktury.KorektaZakupu;
			else throw new ApplicationException($"Nie można korygować faktury {bazowa.Rodzaj}.");
			korekta.DataSprzedazy = bazowa.DataSprzedazy;
			korekta.FakturaKorygowanaRef = bazowa;
			korekta.NIPSprzedawcy = bazowa.NIPSprzedawcy;
			korekta.NazwaSprzedawcy = bazowa.NazwaSprzedawcy;
			korekta.DaneSprzedawcy = bazowa.DaneSprzedawcy;
			korekta.NIPNabywcy = bazowa.NIPNabywcy;
			korekta.NazwaNabywcy = bazowa.NazwaNabywcy;
			korekta.DaneNabywcy = bazowa.DaneNabywcy;
			korekta.RachunekBankowy = bazowa.RachunekBankowy;
			korekta.UwagiPubliczne = bazowa.UwagiPubliczne;
			korekta.KursWaluty = bazowa.KursWaluty;
			korekta.OpisSposobuPlatnosci = bazowa.OpisSposobuPlatnosci;
			korekta.SprzedawcaRef = bazowa.SprzedawcaRef;
			korekta.NabywcaRef = bazowa.NabywcaRef;
			korekta.WalutaRef = bazowa.WalutaRef;
			korekta.SposobPlatnosciRef = bazowa.SposobPlatnosciRef;

			bazowa.FakturaKorygujacaRef = korekta;

			var starePozycje = baza.PozycjeFaktur.Where(pozycja => pozycja.FakturaId == bazowa.Id).ToList();
			var nowePozycje = new List<PozycjaFaktury>();
			foreach (var staraPozycja in starePozycje)
			{
				if (staraPozycja.CzyPrzedKorekta) continue;

				var pozycjaPrzed = new PozycjaFaktury();
				pozycjaPrzed.FakturaId = korekta.Id;
				pozycjaPrzed.TowarId = staraPozycja.TowarId;
				pozycjaPrzed.Opis = staraPozycja.Opis;
				pozycjaPrzed.CenaNetto = staraPozycja.CenaNetto;
				pozycjaPrzed.CenaVat = staraPozycja.CenaVat;
				pozycjaPrzed.CenaBrutto = staraPozycja.CenaBrutto;
				pozycjaPrzed.Ilosc = -staraPozycja.Ilosc;
				pozycjaPrzed.WartoscNetto = -staraPozycja.WartoscNetto;
				pozycjaPrzed.WartoscVat = -staraPozycja.WartoscVat;
				pozycjaPrzed.WartoscBrutto = -staraPozycja.WartoscBrutto;
				pozycjaPrzed.CzyWedlugCenBrutto = staraPozycja.CzyWedlugCenBrutto;
				pozycjaPrzed.CzyWartosciReczne = staraPozycja.CzyWartosciReczne;
				pozycjaPrzed.StawkaVatRef = staraPozycja.StawkaVatRef;
				pozycjaPrzed.CzyPrzedKorekta = true;
				pozycjaPrzed.LP = staraPozycja.LP;
				nowePozycje.Add(pozycjaPrzed);

				var pozycjaPo = new PozycjaFaktury();
				pozycjaPo.FakturaId = korekta.Id;
				pozycjaPo.TowarId = staraPozycja.TowarId;
				pozycjaPo.Opis = staraPozycja.Opis;
				pozycjaPo.CenaNetto = staraPozycja.CenaNetto;
				pozycjaPo.CenaVat = staraPozycja.CenaVat;
				pozycjaPo.CenaBrutto = staraPozycja.CenaBrutto;
				pozycjaPo.Ilosc = staraPozycja.Ilosc;
				pozycjaPo.WartoscNetto = staraPozycja.WartoscNetto;
				pozycjaPo.WartoscVat = staraPozycja.WartoscVat;
				pozycjaPo.WartoscBrutto = staraPozycja.WartoscBrutto;
				pozycjaPo.CzyWedlugCenBrutto = staraPozycja.CzyWedlugCenBrutto;
				pozycjaPo.CzyWartosciReczne = staraPozycja.CzyWartosciReczne;
				pozycjaPo.StawkaVatRef = staraPozycja.StawkaVatRef;
				pozycjaPo.CzyPrzedKorekta = false;
				pozycjaPo.LP = staraPozycja.LP;
				nowePozycje.Add(pozycjaPo);
			}

			baza.Zapisz(nowePozycje);
			baza.Zapisz(bazowa);

			return korekta;
		}

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
