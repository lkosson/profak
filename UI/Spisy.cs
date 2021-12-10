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
		public static TRekord Wybierz<TRekord>(Kontekst kontekst, Func<SpisZAkcjami<TRekord>> generatorSpisu, string tytul, Ref<TRekord> biezacaWartosc)
			where TRekord : Rekord<TRekord>
		{
			var wybor = new WybierzRekordAkcja<TRekord>();
			using var nowyKontekst = new Kontekst(kontekst);
			using var transakcja = nowyKontekst.Transakcja();
			using var spis = generatorSpisu();
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

		public static SpisZAkcjami<Faktura, FakturaSprzedazySpis> FakturySprzedazy()
		{
			return Utworz(new FakturaSprzedazySpis(),
				new FakturaSprzedazyAkcja(),
				new FakturaProformaAkcja(),
				new KorektaSprzedazyAkcja(),
				new EdytujRekordAkcja<Faktura, FakturaEdytor>(),
				new UsunFaktureAkcja(),
				new DodajWplateAkcja(),
				new WydrukFakturyAkcja(),
				new PrzeladujAkcja<Faktura>()
			);
		}

		public static SpisZAkcjami<Faktura, FakturaZakupuSpis> FakturyZakupu()
		{
			return Utworz(new FakturaZakupuSpis(),
				new FakturaZakupuAkcja(),
				new KorektaZakupuAkcja(),
				new EdytujRekordAkcja<Faktura, FakturaZakupuEdytor>(),
				new UsunFaktureAkcja(),
				new DodajWplateAkcja(),
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
				new UsunRekordAkcja<Kontrahent>(),
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

		private static SpisZAkcjami<TRekord, TSpis> Utworz<TRekord, TSpis>(TSpis spis, params AkcjaNaSpisie<TRekord>[] akcje)
			where TRekord : Rekord<TRekord>
			where TSpis : Spis<TRekord>
		{
			var okno = new SpisZAkcjami<TRekord, TSpis>(spis);
			okno.Akcje.AddRange(akcje);
			return okno;
		}
	}
}
