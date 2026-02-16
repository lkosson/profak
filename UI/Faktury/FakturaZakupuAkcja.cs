using ProFak.DB;

namespace ProFak.UI
{
	class FakturaZakupuAkcja : DodajRekordAkcja<Faktura, FakturaZakupuEdytor>
	{
		public override string Nazwa => "➕ Wprowadź fakturę [INS]";

		public FakturaZakupuAkcja()
			: base(faktura => faktura.Rodzaj = RodzajFaktury.Zakup)
		{
		}
	}
}
