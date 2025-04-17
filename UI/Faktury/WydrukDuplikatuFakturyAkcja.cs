using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class WydrukDuplikatuFakturyAkcja : WydrukFakturyAkcja
	{
		public override string Nazwa => "🖶 Drukuj duplikat";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any() && !zaznaczoneRekordy.Any(e => !e.Numerator.HasValue);
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => false;
		protected override bool CzyDuplikat => true;
	}
}
