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
	class FakturaZakupuAkcja : DodajRekordAkcja<Faktura, FakturaZakupuEdytor>
	{
		public override string Nazwa => "➕ Wprowadź fakturę [INS]";

		public FakturaZakupuAkcja()
			: base(faktura => faktura.Rodzaj = RodzajFaktury.Zakup)
		{
		}
	}
}
