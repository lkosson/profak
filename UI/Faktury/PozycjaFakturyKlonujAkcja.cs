using ProFak.DB;

namespace ProFak.UI
{
	class PozycjaFakturyKlonujAkcja : DodajRekordAkcja<PozycjaFaktury, PozycjaFakturyEdytor>
	{
		public override string Nazwa => "➕ Klonuj pozycję [SHIFT-INS]";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<PozycjaFaktury> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1 && !zaznaczoneRekordy.Single().CzyPrzedKorekta;
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.Shift && klawisz == Keys.Insert;

		protected override PozycjaFaktury UtworzRekord(Kontekst kontekst, IEnumerable<PozycjaFaktury> zaznaczoneRekordy)
		{
			var zaznaczona = zaznaczoneRekordy.Single();
			var podobna = zaznaczona.PrzygotujPodobna();

			var ostatniaIstniejacaPozycja = kontekst.Baza.PozycjeFaktur
				.Where(pozycja => pozycja.FakturaId == zaznaczona.FakturaId && !pozycja.CzyPrzedKorekta)
				.OrderByDescending(pozycja => pozycja.LP)
				.FirstOrDefault();
			if (ostatniaIstniejacaPozycja != null) podobna.LP = ostatniaIstniejacaPozycja.LP + 1;

			return podobna;
		}
	}
}
