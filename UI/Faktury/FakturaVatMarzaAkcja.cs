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
	class FakturaVatMarzaAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
	{
		public override string Nazwa => "➕ Wystaw vat marża";
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => false;

		public FakturaVatMarzaAkcja()
			: base(faktura => faktura.Rodzaj = RodzajFaktury.VatMarża)
		{
		}

		protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
		{
			rekord.NadajNumer(kontekst.Baza);
			base.ZapiszRekord(kontekst, rekord);
		}
	}
}
