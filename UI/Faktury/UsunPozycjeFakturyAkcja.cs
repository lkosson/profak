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
	class UsunPozycjeFakturyAkcja : UsunRekordAkcja<PozycjaFaktury>
	{
		public override bool CzyDostepnaDlaRekordow(IEnumerable<PozycjaFaktury> zaznaczoneRekordy) => base.CzyDostepnaDlaRekordow(zaznaczoneRekordy) && !zaznaczoneRekordy.Any(faktura => faktura.Ilosc < 0);
	}
}
