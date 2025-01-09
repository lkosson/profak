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
	class FakturaPodobnaAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
	{
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.Shift && klawisz == Keys.Insert;

		protected override Faktura UtworzRekord(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
		{
			var zaznaczona = zaznaczoneRekordy.Single();
			var korekta = zaznaczona.PrzygotujPodobna(kontekst.Baza);
			return korekta;
		}

		protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
		{
			if (rekord.Numerator.HasValue) rekord.Numer = Numerator.NadajNumer(kontekst.Baza, rekord.Numerator.Value, rekord.Podstawienie);
			base.ZapiszRekord(kontekst, rekord);
		}
	}
}
