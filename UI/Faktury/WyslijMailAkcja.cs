using ProFak.DB;
using ProFak.UI.Faktury;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class WyslijMailAkcja : AkcjaNaSpisie<Faktura>
	{
		public override string Nazwa => "✉ Wyślij e-mailem [CTRL-E]";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any();
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => klawisz == Keys.E && modyfikatory == Keys.Control;

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
		{
			using var nowyKontekst = new Kontekst(kontekst);
			using var edytor = new WysylkaFakturEdytor();
			using var okno = new Dialog("Wysyłka faktur", edytor, nowyKontekst);
			okno.CzyPrzyciskiWidoczne = false;
			edytor.Kontekst = nowyKontekst;
			edytor.Faktury = zaznaczoneRekordy;
			okno.ShowDialog();
		}
	}
}
