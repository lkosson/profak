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
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any() && !zaznaczoneRekordy.Any(e => !e.Numerator.HasValue);
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => klawisz == Keys.P && modyfikatory == Keys.Control;
		protected virtual bool CzyDuplikat => false;

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
		{
			var wydruk = new Wydruki.Faktura(kontekst.Baza, zaznaczoneRekordy.Select(faktura => faktura.Ref), CzyDuplikat);
			using var okno = new OknoWydruku(wydruk);
			okno.ShowDialog();
		}
	}
}
