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
	class DowodWewnetrznyAkcja : DodajRekordAkcja<Faktura, FakturaZakupuEdytor>
	{
		public override string Nazwa => "➕ Dowód wewnętrzny";

		public DowodWewnetrznyAkcja()
			: base(faktura => faktura.Rodzaj = RodzajFaktury.DowódWewnętrzny)
		{
		}

		protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
		{
			rekord.NadajNumer(kontekst.Baza);
			base.ZapiszRekord(kontekst, rekord);
		}
	}
}
