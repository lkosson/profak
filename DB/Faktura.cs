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
		public DateTime DataWystawienia { get; set; }
		public DateTime DataSprzedazy { get; set; }
		public DateTime DataWprowadzenia { get; set; }
		public DateTime TerminPlatnosci { get; set; }
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
		public RodzajFaktury Rodzaj { get; set; }
		public bool CzyWartosciReczne { get; set; }

		public int? SprzedawcaId { get; set; }
		public int? NabywcaId { get; set; }
		public int? FakturaKorygowanaId { get; set; }
		public int? FakturaKorygujacaId { get; set; }
		public int WalutaId { get; set; }
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

		public override void WypelnijDomyslnePola(Baza baza)
		{
			base.WypelnijDomyslnePola(baza);
			Rodzaj = RodzajFaktury.Sprzedaż;
			DataSprzedazy = DateTime.Now.Date;
			DataWystawienia = DateTime.Now.Date;
			DataWprowadzenia = DateTime.Now.Date;
			TerminPlatnosci = DateTime.Now.Date;
			WalutaRef = baza.Waluty.OrderByDescending(waluta => waluta.CzyDomyslna).ThenBy(waluta => waluta.Id).FirstOrDefault();
			SposobPlatnosciRef = baza.SposobyPlatnosci.OrderByDescending(sposob => sposob.CzyDomyslny).ThenBy(sposob => sposob.Id).FirstOrDefault();
			if (WalutaRef.IsNull) throw new ApplicationException("Przed wprowadzeniem faktury należy zdefiniować przynajmniej jedną walutę.");
			if (SposobPlatnosciRef.IsNull) throw new ApplicationException("Przed wprowadzeniem faktury należy zdefiniować przynajmniej jeden sposób płatności.");
		}

		public void PrzeliczRazem(Baza baza)
		{
			var pozycje = baza.PozycjeFaktur.Where(pozycja => pozycja.FakturaId == Id).ToList();
			RazemNetto = pozycje.Sum(pozycja => pozycja.WartoscNetto);
			RazemVat = pozycje.Sum(pozycja => pozycja.WartoscVat);
			RazemBrutto = pozycje.Sum(pozycja => pozycja.WartoscBrutto);
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
