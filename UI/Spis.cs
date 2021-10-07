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

		public static OknoSpisu JednostkiMiar(Kontekst kontekst)
		{
			return OknoSpisu.Utworz<DB.JednostkaMiary, JednostkaMiarySpis>(kontekst,
				"Jednostki miar",
				new DodajRekordAkcja<DB.JednostkaMiary, JednostkaMiaryEdytor>("Nowa jednostka miary"),
				new EdytujRekordAkcja<DB.JednostkaMiary, JednostkaMiaryEdytor>("Edycja jednostki miary"),
				new UsunRekordAkcja<DB.JednostkaMiary>()
			);
		}

		public static OknoSpisu Kontrahenci(Kontekst kontekst)
		{
			return OknoSpisu.Utworz<DB.Kontrahent, KontrahentSpis>(kontekst,
				"Kontrahenci",
				new DodajRekordAkcja<DB.Kontrahent, KontrahentEdytor>("Nowy kontrahent"),
				new EdytujRekordAkcja<DB.Kontrahent, KontrahentEdytor>("Edycja kontrahenta"),
				new UsunRekordAkcja<DB.Kontrahent>()
			);
		}

		public static OknoSpisu SposobyPlatnosci(Kontekst kontekst)
		{
			return OknoSpisu.Utworz<DB.SposobPlatnosci, SposobPlatnosciSpis>(kontekst,
				"Sposoby płatności",
				new DodajRekordAkcja<DB.SposobPlatnosci, SposobPlatnosciEdytor>("Nowy sposób płatności"),
				new EdytujRekordAkcja<DB.SposobPlatnosci, SposobPlatnosciEdytor>("Edycja sposobu płatności"),
				new UsunRekordAkcja<DB.SposobPlatnosci>()
			);
		}

		public static OknoSpisu StawkiVat(Kontekst kontekst)
		{
			return OknoSpisu.Utworz<DB.StawkaVat, StawkaVatSpis>(kontekst,
				"Stawki VAT",
				new DodajRekordAkcja<DB.StawkaVat, StawkaVatEdytor>("Nowa stawka VAT"),
				new EdytujRekordAkcja<DB.StawkaVat, StawkaVatEdytor>("Edycja stawki VAT"),
				new UsunRekordAkcja<DB.StawkaVat>()
			);
		}

		public static OknoSpisu Waluty(Kontekst kontekst)
		{
			return OknoSpisu.Utworz<DB.Waluta, WalutaSpis>(kontekst,
				"Waluty",
				new DodajRekordAkcja<DB.Waluta, WalutaEdytor>("Nowa waluta"),
				new EdytujRekordAkcja<DB.Waluta, WalutaEdytor>("Edycja waluty"),
				new UsunRekordAkcja<DB.Waluta>()
			);
		}
	}
}
