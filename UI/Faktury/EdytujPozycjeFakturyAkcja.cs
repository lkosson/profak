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
	class EdytujPozycjeFakturyAkcja : EdytujRekordAkcja<PozycjaFaktury, PozycjaFakturyEdytor>
	{
		public override bool CzyDostepnaDlaRekordow(IEnumerable<PozycjaFaktury> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1 && zaznaczoneRekordy.Single().Ilosc >= 0;
	}
}
