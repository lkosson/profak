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
	class FakturaPodobnaSprzedazAkcja : FakturaPodobnaAkcja
	{
		public override string Nazwa => "➕ Wystaw podobną [SHIFT-INS]";

		protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
		{
			rekord.Numer = Numerator.NadajNumer(kontekst.Baza, rekord.Numerator, rekord.Podstawienie);
			base.ZapiszRekord(kontekst, rekord);
		}
	}
}
