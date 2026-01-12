using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class Spisy
	{
		public static TRekord Wybierz<TRekord>(Kontekst kontekst, SpisZAkcjami<TRekord> spis, string tytul, Ref<TRekord> biezacaWartosc)
			where TRekord : Rekord<TRekord>
		{
			var wybor = new WybierzRekordAkcja<TRekord>();
			using var nowyKontekst = new Kontekst(kontekst);
			using var transakcja = nowyKontekst.Transakcja();
			using var dialog = new Dialog(tytul, spis, nowyKontekst);
			spis.Akcje.Insert(0, wybor);
			spis.Spis.Kontekst = nowyKontekst;
			spis.Spis.RekordPoczatkowy = biezacaWartosc;
			dialog.CzyPrzyciskiWidoczne = false;
			dialog.Size = new System.Drawing.Size(800, 450);
			if (dialog.ShowDialog() != DialogResult.OK) return default;
			transakcja.Zatwierdz();
			return wybor.WybranyRekord;
		}

		public static SpisZAkcjami<DeklaracjaVat, DeklaracjaVatSpis> DeklaracjeVat(string[] parametry = null)
		{
			return Utworz(new DeklaracjaVatSpis(parametry),
				new DodajRekordAkcja<DeklaracjaVat, DeklaracjaVatEdytor>(),
				new EdytujRekordAkcja<DeklaracjaVat, DeklaracjaVatEdytor>(),
				new UsunRekordAkcja<DeklaracjaVat>(),
				new GenerujJPK_V7MAkcja(),
				new GenerujJPK_FAAkcja(),
				new PrzeladujAkcja<DeklaracjaVat>()
			);
		}

		public static SpisZAkcjami<Faktura, FakturaSprzedazySpis> FakturySprzedazy(string[] parametry = null)
		{
			return Utworz(new FakturaSprzedazySpis(parametry),
				new FakturaSprzedazyAkcja(),
				new FakturaVatMarzaAkcja(),
				new FakturaPodobnaSprzedazAkcja(),
				new KorektaSprzedazyAkcja(),
				new EdytujRekordAkcja<Faktura, FakturaEdytor>(),
				new UsunFaktureAkcja(),
				new DodajWplateAkcja(),
				new WydrukFakturyAkcja(),
				new WydrukDuplikatuFakturyAkcja(),
				new WyslijMailAkcja(),
				new WyslijDoKSeFAkcja(),
				new ZapiszPlikiAkcja(),
				new WczytajJPK_FAAkcja(),
				new WczytajKSeFAkcja(),
				new PrzeladujAkcja<Faktura>()
			);
		}

		public static SpisZAkcjami<Faktura, FakturaProformaSpis> FakturyProforma(string[] parametry = null)
		{
			return Utworz(new FakturaProformaSpis(parametry),
				new FakturaProformaAkcja(),
				new FakturaPodobnaSprzedazAkcja(),
				new EdytujRekordAkcja<Faktura, FakturaEdytor>(),
				new UsunFaktureAkcja(),
				new DodajWplateAkcja(),
				new WydrukFakturyAkcja(),
				new WyslijMailAkcja(),
				new ZapiszPlikiAkcja(),
				new PrzeladujAkcja<Faktura>()
			);
		}

		public static SpisZAkcjami<Faktura, FakturaZakupuSpis> FakturyZakupu(string[] parametry = null)
		{
			return Utworz(new FakturaZakupuSpis(parametry),
				new FakturaZakupuAkcja(),
				new FakturaPodobnaZakupAkcja(),
				new KorektaZakupuAkcja(),
				new DowodWewnetrznyAkcja(),
				new WczytajKSeFAkcja(),
				new EdytujRekordAkcja<Faktura, FakturaZakupuEdytor>(),
				new UsunFaktureAkcja(),
				new DodajWplateAkcja(),
				new WydrukFakturyAkcja(),
				new ZapiszPlikiAkcja(),
				new WczytajJPK_FAAkcja(),
				new PrzeladujAkcja<Faktura>()
			);
		}

		public static SpisZAkcjami<Faktura, FakturaUsunietaSpis> FakturyUsuniete()
		{
			return Utworz(new FakturaUsunietaSpis(),
				new EdytujRekordAkcja<Faktura, FakturaEdytor>(),
				new UsunRekordAkcja<Faktura>(),
				new PrzeladujAkcja<Faktura>()
			);
		}

		public static SpisZAkcjami<Faktura, KSeFSpis> KSeFSprzedaz(params string[] parametry)
		{
			return Utworz(new KSeFSpis(true, parametry),
				new ZapiszJakoXMLAkcja(),
				new PrzeladujAkcja<Faktura>()
			);
		}

		public static SpisZAkcjami<Faktura, KSeFSpis> KSeFZakup(params string[] parametry)
		{
			return Utworz(new KSeFSpis(false, parametry),
				new DodajJakoZakupAkcja(),
				new ZapiszJakoXMLAkcja(),
				new PrzeladujAkcja<Faktura>()
			);
		}

		public static SpisZAkcjami<JednostkaMiary, JednostkaMiarySpis> JednostkiMiar()
		{
			return Utworz(new JednostkaMiarySpis(),
				new DodajRekordAkcja<JednostkaMiary, JednostkaMiaryEdytor>(),
				new EdytujRekordAkcja<JednostkaMiary, JednostkaMiaryEdytor>(),
				new UsunRekordAkcja<JednostkaMiary>(),
				new PrzeladujAkcja<JednostkaMiary>()
			);
		}

		public static SpisZAkcjami<Kontrahent, KontrahentSpis> Kontrahenci()
		{
			return Utworz(new KontrahentSpis(),
				new DodajRekordAkcja<Kontrahent, KontrahentEdytor>(),
				new MojaFirmaAkcja(),
				new EdytujRekordAkcja<Kontrahent, KontrahentEdytor>(),
				new UsunKontrahentaAkcja(),
				new PrzeladujAkcja<Kontrahent>()
			);
		}

		public static SpisZAkcjami<Numerator, NumeratorSpis> Numeratory()
		{
			return Utworz(new NumeratorSpis(),
				new DodajRekordAkcja<Numerator, NumeratorEdytor>(),
				new EdytujRekordAkcja<Numerator, NumeratorEdytor>(),
				new UsunRekordAkcja<Numerator>(),
				new PrzeladujAkcja<Numerator>()
			);
		}

		public static SpisZAkcjami<Plik, PlikSpis> Pliki()
		{
			var spis = new PlikSpis();
			return Utworz(spis,
				new DodajPlikAkcja(spis),
				new PokazPlikAction(),
				new ZapiszPlikAction(),
				new UsunRekordAkcja<Plik>(),
				new PrzeladujAkcja<Plik>()
			);
		}

		public static SpisZAkcjami<PozycjaFaktury, PozycjaFakturySpis> PozycjeFaktur()
		{
			var spis = new PozycjaFakturySpis();
			return Utworz(spis,
				new DodajPozycjeFakturyAkcja(pozycja => pozycja.FakturaRef = spis.FakturaRef),
				new EdytujPozycjeFakturyAkcja(),
				new UsunPozycjeFakturyAkcja(),
				new PrzeladujAkcja<PozycjaFaktury>()
			);
		}

		public static SpisZAkcjami<SkladkaZus, SkladkaZusSpis> SkladkiZus(string[] parametry = null)
		{
			return Utworz(new SkladkaZusSpis(parametry),
				new DodajRekordAkcja<SkladkaZus, SkladkaZusEdytor>(),
				new EdytujRekordAkcja<SkladkaZus, SkladkaZusEdytor>(),
				new UsunRekordAkcja<SkladkaZus>(),
				new PrzeladujAkcja<SkladkaZus>()
			);
		}

		public static SpisZAkcjami<SposobPlatnosci, SposobPlatnosciSpis> SposobyPlatnosci()
		{
			return Utworz(new SposobPlatnosciSpis(),
				new DodajRekordAkcja<SposobPlatnosci, SposobPlatnosciEdytor>(),
				new EdytujRekordAkcja<SposobPlatnosci, SposobPlatnosciEdytor>(),
				new UsunRekordAkcja<SposobPlatnosci>(),
				new PrzeladujAkcja<SposobPlatnosci>()
			);
		}

		public static SpisZAkcjami<StanNumeratora, StanNumeratoraSpis> StanyNumeratorow()
		{
			var spis = new StanNumeratoraSpis();
			return Utworz(spis,
				new DodajRekordAkcja<StanNumeratora, StanNumeratoraEdytor>(stanNumeratora => stanNumeratora.NumeratorRef = spis.NumeratorRef),
				new EdytujRekordAkcja<StanNumeratora, StanNumeratoraEdytor>(),
				new UsunRekordAkcja<StanNumeratora>(),
				new PrzeladujAkcja<StanNumeratora>()
			);
		}

		public static SpisZAkcjami<StawkaVat, StawkaVatSpis> StawkiVat()
		{
			return Utworz(new StawkaVatSpis(),
				new DodajRekordAkcja<StawkaVat, StawkaVatEdytor>(),
				new EdytujRekordAkcja<StawkaVat, StawkaVatEdytor>(),
				new UsunRekordAkcja<StawkaVat>(),
				new PrzeladujAkcja<StawkaVat>()
			);
		}

		public static SpisZAkcjami<Towar, TowarSpis> Towary()
		{
			return Utworz(new TowarSpis(),
				new DodajRekordAkcja<Towar, TowarEdytor>(),
				new EdytujRekordAkcja<Towar, TowarEdytor>(),
				new UsunRekordAkcja<Towar>(),
				new PrzeladujAkcja<Towar>()
			);
		}

		public static SpisZAkcjami<UrzadSkarbowy, UrzadSkarbowySpis> UrzedySkarbowe()
		{
			return Utworz(new UrzadSkarbowySpis(),
				new DodajRekordAkcja<UrzadSkarbowy, UrzadSkarbowyEdytor>(),
				new EdytujRekordAkcja<UrzadSkarbowy, UrzadSkarbowyEdytor>(),
				new UsunRekordAkcja<UrzadSkarbowy>(),
				new PrzeladujAkcja<UrzadSkarbowy>()
			);
		}

		public static SpisZAkcjami<Waluta, WalutaSpis> Waluty()
		{
			return Utworz(new WalutaSpis(),
				new DodajRekordAkcja<Waluta, WalutaEdytor>(),
				new EdytujRekordAkcja<Waluta, WalutaEdytor>(),
				new UsunRekordAkcja<Waluta>(),
				new PrzeladujAkcja<Waluta>()
			);
		}

		public static SpisZAkcjami<Wplata, WplataSpis> Wplaty()
		{
			var spis = new WplataSpis();
			return Utworz(spis,
				new DodajRekordAkcja<Wplata, WplataEdytor>(wplata => wplata.FakturaRef = spis.FakturaRef),
				new EdytujRekordAkcja<Wplata, WplataEdytor>(),
				new UsunRekordAkcja<Wplata>(),
				new PrzeladujAkcja<Wplata>()
			);
		}

		public static SpisZAkcjami<ZaliczkaPit, ZaliczkaPitSpis> ZaliczkiPit(string[] parametry = null)
		{
			return Utworz(new ZaliczkaPitSpis(parametry),
				new DodajRekordAkcja<ZaliczkaPit, ZaliczkaPitEdytor>(),
				new EdytujRekordAkcja<ZaliczkaPit, ZaliczkaPitEdytor>(),
				new UsunRekordAkcja<ZaliczkaPit>(),
				new WydrukZaliczekAkcja(),
				new GenerujJPK_EWPAkcja(),
				new GenerujJPK_PKPIRAkcja(),
				new PrzeladujAkcja<ZaliczkaPit>()
			);
		}

		private static SpisZAkcjami<TRekord, TSpis> Utworz<TRekord, TSpis>(TSpis spis, params AkcjaNaSpisie<TRekord>[] akcje)
			where TRekord : Rekord<TRekord>
			where TSpis : Spis<TRekord>
			=> new SpisZAkcjami<TRekord, TSpis>(spis, akcje);
	}
}
