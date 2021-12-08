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
	class WydrukFakturyAkcja : AkcjaNaSpisie<Faktura>
	{
		public override string Nazwa => "🖶 Drukuj [CTRL-P]";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any();
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => klawisz == Keys.P && modyfikatory == Keys.Control;

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
		{
			var wydruk = new Wydruki.Faktura(kontekst.Baza, zaznaczoneRekordy.Select(faktura => faktura.Ref));
			using var okno = new OknoWydruku(wydruk);
			okno.ShowDialog();
		}
	}
}
