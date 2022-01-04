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
	class WydrukPKPiRAkcja : AkcjaNaSpisie<ZaliczkaPit>
	{
		public override string Nazwa => "🖶 Drukuj PKPiR [CTRL-P]";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<ZaliczkaPit> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => klawisz == Keys.P && modyfikatory == Keys.Control;

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<ZaliczkaPit> zaznaczoneRekordy)
		{
			var wydruk = new Wydruki.PKPiR(kontekst.Baza, zaznaczoneRekordy.Single());
			using var okno = new OknoWydruku(wydruk);
			okno.ShowDialog();
		}
	}
}
