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

		public static SpisZAkcjami<JednostkaMiary> JednostkiMiar(Kontekst kontekst)
		{
			return SpisZAkcjami<JednostkaMiary>.Utworz(new JednostkaMiarySpis { Kontekst = kontekst },
				new DodajRekordAkcja<DB.JednostkaMiary, JednostkaMiaryEdytor>("Nowa jednostka miary"),
				new EdytujRekordAkcja<DB.JednostkaMiary, JednostkaMiaryEdytor>("Edycja jednostki miary"),
				new UsunRekordAkcja<DB.JednostkaMiary>()
			);
		}

		public static SpisZAkcjami<Kontrahent> Kontrahenci(Kontekst kontekst)
		{
			return SpisZAkcjami<Kontrahent>.Utworz(new KontrahentSpis { Kontekst = kontekst },
				new DodajRekordAkcja<DB.Kontrahent, KontrahentEdytor>("Nowy kontrahent"),
				new EdytujRekordAkcja<DB.Kontrahent, KontrahentEdytor>("Edycja kontrahenta"),
				new UsunRekordAkcja<DB.Kontrahent>()
			);
		}

		public static SpisZAkcjami<SposobPlatnosci> SposobyPlatnosci(Kontekst kontekst)
		{
			return SpisZAkcjami<SposobPlatnosci>.Utworz(new SposobPlatnosciSpis { Kontekst = kontekst },
				new DodajRekordAkcja<DB.SposobPlatnosci, SposobPlatnosciEdytor>("Nowy sposób płatności"),
				new EdytujRekordAkcja<DB.SposobPlatnosci, SposobPlatnosciEdytor>("Edycja sposobu płatności"),
				new UsunRekordAkcja<DB.SposobPlatnosci>()
			);
		}

		public static SpisZAkcjami<StawkaVat> StawkiVat(Kontekst kontekst)
		{
			return SpisZAkcjami<StawkaVat>.Utworz(new StawkaVatSpis { Kontekst = kontekst },
				new DodajRekordAkcja<DB.StawkaVat, StawkaVatEdytor>("Nowa stawka VAT"),
				new EdytujRekordAkcja<DB.StawkaVat, StawkaVatEdytor>("Edycja stawki VAT"),
				new UsunRekordAkcja<DB.StawkaVat>()
			);
		}

		public static SpisZAkcjami<Towar> Towary(Kontekst kontekst)
		{
			return SpisZAkcjami<Towar>.Utworz(new TowarSpis { Kontekst = kontekst },
				new DodajRekordAkcja<DB.Towar, TowarEdytor>("Nowy towar"),
				new EdytujRekordAkcja<DB.Towar, TowarEdytor>("Edycja towaru"),
				new UsunRekordAkcja<DB.Towar>()
			);
		}

		public static SpisZAkcjami<Waluta> Waluty(Kontekst kontekst)
		{
			return SpisZAkcjami<Waluta>.Utworz(new WalutaSpis { Kontekst = kontekst },
				new DodajRekordAkcja<DB.Waluta, WalutaEdytor>("Nowa waluta"),
				new EdytujRekordAkcja<DB.Waluta, WalutaEdytor>("Edycja waluty"),
				new UsunRekordAkcja<DB.Waluta>()
			);
		}
	}
}
