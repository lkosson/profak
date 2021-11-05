using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class Spis : DataGridView
	{
		public Spis()
		{
			DoubleBuffered = true;
			AutoGenerateColumns = false;
			AllowUserToAddRows = false;
			AllowUserToDeleteRows = false;
			AllowUserToResizeRows = false;
			AllowUserToOrderColumns = true;
			RowHeadersVisible = false;
			ShowCellToolTips = true;
			ReadOnly = true;
			SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			TabIndex = 50;
		}

		public static TRekord Wybierz<TRekord>(Kontekst kontekst, Func<Kontekst, SpisZAkcjami<TRekord>> generatorSpisu, string tytul, Ref<TRekord> biezacaWartosc)
			where TRekord : Rekord<TRekord>
		{
			var wybor = new WybierzRekordAkcja<TRekord>();
			using var nowyKontekst = new Kontekst(kontekst);
			using var transakcja = nowyKontekst.Transakcja();
			using var spis = generatorSpisu(nowyKontekst);
			using var dialog = new Dialog(tytul, spis, nowyKontekst);
			spis.Akcje.Add(wybor);
			spis.Spis.RekordPoczatkowy = biezacaWartosc;
			dialog.CzyPrzyciskiWidoczne = false;
			dialog.Size = new System.Drawing.Size(800, 450);
			if (dialog.ShowDialog() != DialogResult.OK) return default;
			nowyKontekst.Baza.Zapisz();
			transakcja.Zatwierdz();
			return wybor.WybranyRekord;
		}

		public static SpisZAkcjami<Faktura, FakturaSprzedazySpis> FakturySprzedazy(Kontekst kontekst)
		{
			return SpisZAkcjami.Utworz(new FakturaSprzedazySpis { Kontekst = kontekst },
				new DodajRekordAkcja<Faktura, FakturaEdytor>("Nowa faktura sprzedaży"),
				new EdytujRekordAkcja<Faktura, FakturaEdytor>("Edycja faktury"),
				new UsunRekordAkcja<Faktura>()
			);
		}

		public static SpisZAkcjami<Faktura, FakturaZakupuSpis> FakturyZakupu(Kontekst kontekst)
		{
			return SpisZAkcjami.Utworz(new FakturaZakupuSpis { Kontekst = kontekst },
				new DodajRekordAkcja<Faktura, FakturaEdytor>("Nowa faktura zakupu"),
				new EdytujRekordAkcja<Faktura, FakturaEdytor>("Edycja faktury"),
				new UsunRekordAkcja<Faktura>()
			);
		}

		public static SpisZAkcjami<JednostkaMiary, JednostkaMiarySpis> JednostkiMiar(Kontekst kontekst)
		{
			return SpisZAkcjami.Utworz(new JednostkaMiarySpis { Kontekst = kontekst },
				new DodajRekordAkcja<JednostkaMiary, JednostkaMiaryEdytor>("Nowa jednostka miary"),
				new EdytujRekordAkcja<JednostkaMiary, JednostkaMiaryEdytor>("Edycja jednostki miary"),
				new UsunRekordAkcja<JednostkaMiary>()
			);
		}

		public static SpisZAkcjami<Kontrahent, KontrahentSpis> Kontrahenci(Kontekst kontekst)
		{
			return SpisZAkcjami.Utworz(new KontrahentSpis { Kontekst = kontekst },
				new DodajRekordAkcja<Kontrahent, KontrahentEdytor>("Nowy kontrahent"),
				new EdytujRekordAkcja<Kontrahent, KontrahentEdytor>("Edycja kontrahenta"),
				new UsunRekordAkcja<Kontrahent>()
			);
		}

		public static SpisZAkcjami<PozycjaFaktury, PozycjaFakturySpis> PozycjeFaktur(Kontekst kontekst)
		{
			return SpisZAkcjami.Utworz(new PozycjaFakturySpis { Kontekst = kontekst },
				new DodajRekordAkcja<PozycjaFaktury, PozycjaFakturyEdytor>("Nowa pozycja"),
				new EdytujRekordAkcja<PozycjaFaktury, PozycjaFakturyEdytor>("Edycja pozycji"),
				new UsunRekordAkcja<PozycjaFaktury>()
			);
		}

		public static SpisZAkcjami<SposobPlatnosci, SposobPlatnosciSpis> SposobyPlatnosci(Kontekst kontekst)
		{
			return SpisZAkcjami.Utworz(new SposobPlatnosciSpis { Kontekst = kontekst },
				new DodajRekordAkcja<SposobPlatnosci, SposobPlatnosciEdytor>("Nowy sposób płatności"),
				new EdytujRekordAkcja<SposobPlatnosci, SposobPlatnosciEdytor>("Edycja sposobu płatności"),
				new UsunRekordAkcja<SposobPlatnosci>()
			);
		}

		public static SpisZAkcjami<StawkaVat, StawkaVatSpis> StawkiVat(Kontekst kontekst)
		{
			return SpisZAkcjami.Utworz(new StawkaVatSpis { Kontekst = kontekst },
				new DodajRekordAkcja<StawkaVat, StawkaVatEdytor>("Nowa stawka VAT"),
				new EdytujRekordAkcja<StawkaVat, StawkaVatEdytor>("Edycja stawki VAT"),
				new UsunRekordAkcja<StawkaVat>()
			);
		}

		public static SpisZAkcjami<Towar, TowarSpis> Towary(Kontekst kontekst)
		{
			return SpisZAkcjami.Utworz(new TowarSpis { Kontekst = kontekst },
				new DodajRekordAkcja<Towar, TowarEdytor>("Nowy towar"),
				new EdytujRekordAkcja<Towar, TowarEdytor>("Edycja towaru"),
				new UsunRekordAkcja<Towar>()
			);
		}

		public static SpisZAkcjami<Waluta, WalutaSpis> Waluty(Kontekst kontekst)
		{
			return SpisZAkcjami.Utworz(new WalutaSpis { Kontekst = kontekst },
				new DodajRekordAkcja<Waluta, WalutaEdytor>("Nowa waluta"),
				new EdytujRekordAkcja<Waluta, WalutaEdytor>("Edycja waluty"),
				new UsunRekordAkcja<Waluta>()
			);
		}

		public static SpisZAkcjami<Wplata, WplataSpis> Wplaty(Kontekst kontekst)
		{
			var spis = new WplataSpis { Kontekst = kontekst };
			return SpisZAkcjami.Utworz(spis,
				new DodajRekordAkcja<Wplata, WplataEdytor>("Nowa wpłata", wplata => wplata.FakturaRef = spis.FakturaRef),
				new EdytujRekordAkcja<Wplata, WplataEdytor>("Edycja wpłaty"),
				new UsunRekordAkcja<Wplata>()
			);
		}
	}
}
