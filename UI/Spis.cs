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
		}

		public static T Wybierz<T>(Kontekst kontekst, Func<Kontekst, SpisZAkcjami<T>> generatorSpisu, string tytul)
			where T : Rekord<T>
		{
			var wybor = new WybierzRekordAkcja<T>();
			using var nowyKontekst = new Kontekst(kontekst);
			using var spis = generatorSpisu(nowyKontekst);
			using var dialog = new Dialog(tytul, spis, nowyKontekst);
			spis.Akcje.Add(wybor);
			if (dialog.ShowDialog() != DialogResult.OK) return default;
			return wybor.WybranyRekord;
		}

		public static SpisZAkcjami<Faktura> FakturySprzedazy(Kontekst kontekst)
		{
			return SpisZAkcjami<Faktura>.Utworz(new FakturaSprzedazySpis { Kontekst = kontekst },
				//new DodajRekordAkcja<Faktura, FakturaEdytor>("Nowa faktura sprzedaży"),
				//new EdytujRekordAkcja<Faktura, FakturaEdytor>("Edycja faktury"),
				new UsunRekordAkcja<Faktura>()
			);
		}

		public static SpisZAkcjami<Faktura> FakturyZakupu(Kontekst kontekst)
		{
			return SpisZAkcjami<Faktura>.Utworz(new FakturaZakupuSpis { Kontekst = kontekst },
				//new DodajRekordAkcja<Faktura, FakturaEdytor>("Nowa faktura zakupu"),
				//new EdytujRekordAkcja<Faktura, FakturaEdytor>("Edycja faktury"),
				new UsunRekordAkcja<Faktura>()
			);
		}

		public static SpisZAkcjami<JednostkaMiary> JednostkiMiar(Kontekst kontekst)
		{
			return SpisZAkcjami<JednostkaMiary>.Utworz(new JednostkaMiarySpis { Kontekst = kontekst },
				new DodajRekordAkcja<JednostkaMiary, JednostkaMiaryEdytor>("Nowa jednostka miary"),
				new EdytujRekordAkcja<JednostkaMiary, JednostkaMiaryEdytor>("Edycja jednostki miary"),
				new UsunRekordAkcja<JednostkaMiary>()
			);
		}

		public static SpisZAkcjami<Kontrahent> Kontrahenci(Kontekst kontekst)
		{
			return SpisZAkcjami<Kontrahent>.Utworz(new KontrahentSpis { Kontekst = kontekst },
				new DodajRekordAkcja<Kontrahent, KontrahentEdytor>("Nowy kontrahent"),
				new EdytujRekordAkcja<Kontrahent, KontrahentEdytor>("Edycja kontrahenta"),
				new UsunRekordAkcja<Kontrahent>()
			);
		}

		public static SpisZAkcjami<SposobPlatnosci> SposobyPlatnosci(Kontekst kontekst)
		{
			return SpisZAkcjami<SposobPlatnosci>.Utworz(new SposobPlatnosciSpis { Kontekst = kontekst },
				new DodajRekordAkcja<SposobPlatnosci, SposobPlatnosciEdytor>("Nowy sposób płatności"),
				new EdytujRekordAkcja<SposobPlatnosci, SposobPlatnosciEdytor>("Edycja sposobu płatności"),
				new UsunRekordAkcja<SposobPlatnosci>()
			);
		}

		public static SpisZAkcjami<StawkaVat> StawkiVat(Kontekst kontekst)
		{
			return SpisZAkcjami<StawkaVat>.Utworz(new StawkaVatSpis { Kontekst = kontekst },
				new DodajRekordAkcja<StawkaVat, StawkaVatEdytor>("Nowa stawka VAT"),
				new EdytujRekordAkcja<StawkaVat, StawkaVatEdytor>("Edycja stawki VAT"),
				new UsunRekordAkcja<StawkaVat>()
			);
		}

		public static SpisZAkcjami<Towar> Towary(Kontekst kontekst)
		{
			return SpisZAkcjami<Towar>.Utworz(new TowarSpis { Kontekst = kontekst },
				new DodajRekordAkcja<Towar, TowarEdytor>("Nowy towar"),
				new EdytujRekordAkcja<Towar, TowarEdytor>("Edycja towaru"),
				new UsunRekordAkcja<Towar>()
			);
		}

		public static SpisZAkcjami<Waluta> Waluty(Kontekst kontekst)
		{
			return SpisZAkcjami<Waluta>.Utworz(new WalutaSpis { Kontekst = kontekst },
				new DodajRekordAkcja<Waluta, WalutaEdytor>("Nowa waluta"),
				new EdytujRekordAkcja<Waluta, WalutaEdytor>("Edycja waluty"),
				new UsunRekordAkcja<Waluta>()
			);
		}
	}
}
