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
	class KorektaSprzedazyAkcja : KorektaAkcja<FakturaEdytor>
	{
		public override string Nazwa => "➕ Wystaw korektę";
		protected override RodzajFaktury Rodzaj => RodzajFaktury.KorektaSprzedaży;
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1 && zaznaczoneRekordy.Single().Rodzaj != RodzajFaktury.Proforma;

		protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
		{
			rekord.Numer = Numerator.NadajNumer(kontekst.Baza, PrzeznaczenieNumeratora.Korekta, rekord.Podstawienie);
			base.ZapiszRekord(kontekst, rekord);
		}
	}
}
