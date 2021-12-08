using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class KorektaSprzedazyAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
	{
		public override string Nazwa => "➕ Wystaw korektę";

		public KorektaSprzedazyAkcja()
		{
		}

		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1 && zaznaczoneRekordy.Single().Rodzaj != RodzajFaktury.Proforma;

		protected override Faktura UtworzRekord(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
		{
			var bazowa = zaznaczoneRekordy.Single();

			while (bazowa.FakturaKorygujacaRef.IsNotNull)
			{
				bazowa = kontekst.Baza.Znajdz(bazowa.FakturaKorygujacaRef);
			}

			var korekta = base.UtworzRekord(kontekst, zaznaczoneRekordy);
			korekta.Rodzaj = RodzajFaktury.KorektaSprzedaży;
			korekta.DataSprzedazy = bazowa.DataSprzedazy;
			korekta.FakturaKorygowana = bazowa;
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

			var starePozycje = kontekst.Baza.PozycjeFaktur.Where(pozycja => pozycja.FakturaId == bazowa.Id).ToList();
			var nowePozycje = new List<PozycjaFaktury>();
			foreach (var staraPozycja in starePozycje)
			{
				if (staraPozycja.Ilosc < 0) continue;

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
				nowePozycje.Add(pozycjaPo);
			}

			kontekst.Baza.Zapisz(nowePozycje);
			kontekst.Baza.Zapisz(bazowa);

			return korekta;
		}

		protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
		{
			rekord.Numer = Numerator.NadajNumer(kontekst.Baza, PrzeznaczenieNumeratora.Korekta, rekord.Podstawienie);
			base.ZapiszRekord(kontekst, rekord);
		}
	}
}
