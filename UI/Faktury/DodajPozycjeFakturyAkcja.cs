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
	class DodajPozycjeFakturyAkcja : DodajRekordAkcja<PozycjaFaktury, PozycjaFakturyEdytor>
	{
		public DodajPozycjeFakturyAkcja(Action<PozycjaFaktury> przygotujRekord)
			: base(przygotujRekord)
		{
		}

		protected override PozycjaFaktury UtworzRekord(Kontekst kontekst, IEnumerable<PozycjaFaktury> zaznaczoneRekordy)
		{
			var rekord = base.UtworzRekord(kontekst, zaznaczoneRekordy);

			var ostatniaIstniejacaPozycja = kontekst.Baza.PozycjeFaktur
				.Where(pozycja => pozycja.FakturaId == rekord.FakturaId && pozycja.Id != rekord.Id && !pozycja.CzyPrzedKorekta)
				.OrderByDescending(pozycja => pozycja.LP)
				.FirstOrDefault();
			if (ostatniaIstniejacaPozycja != null) rekord.LP = ostatniaIstniejacaPozycja.LP + 1;

			return rekord;
		}
	}
}
