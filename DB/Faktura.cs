using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Faktura : Rekord<Faktura>
	{
		public string Numer { get; set; }
		public DateTime DataWystawienia { get; set; }
		public DateTime DataSprzedazy { get; set; }
		public DateTime DataWprowadzenia { get; set; }
		public DateTime TerminPlatnosci { get; set; }
		public Ref<Kontrahent> SprzedawcId { get; set; }
		public Ref<Kontrahent> NabywcaId { get; set; }
		public string SposobPlatnosci { get; set; }
		public string NIPSprzedawcy { get; set; }
		public string NazwaSprzedawcy { get; set; }
		public string DaneSprzedawcy { get; set; }
		public string NIPNabywcy { get; set; }
		public string NazwaNabywcy { get; set; }
		public string DaneNabywcy { get; set; }
		public string RachunekBankowy { get; set; }
		public string Uwagi { get; set; }
		public Ref<Faktura> FakturaKorygowana { get; set; }
		public Ref<Faktura> FakturaKorygujaca { get; set; }
		public decimal RazemNetto { get; set; }
		public decimal RazemVat { get; set; }
		public decimal RazemBrutto { get; set; }
		public Ref<Waluta> WalutaId { get; set; }
		public decimal KursWaluty { get; set; }
		public Ref<SposobPlatnosci> SposobPlatnosciId { get; set; }
		public string OpisSposobuPlatnosci { get; set; }
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
