using ProFak.DB;

namespace ProFak.UI
{
	abstract class KorektaAkcja<TEdytor> : DodajRekordAkcja<Faktura, TEdytor>
		where TEdytor : Edytor<Faktura>, new()
	{
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;

		protected override Faktura UtworzRekord(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
		{
			var zaznaczona = zaznaczoneRekordy.Single();
			var korekta = zaznaczona.PrzygotujKorekte(kontekst.Baza);
			return korekta;
		}
	}
}
