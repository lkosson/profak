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
	abstract class KorektaAkcja<TEdytor> : DodajRekordAkcja<Faktura, TEdytor>
		where TEdytor : Edytor<Faktura>, new()
	{
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;

		protected override Faktura UtworzRekord(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
		{
			var zaznaczona = zaznaczoneRekordy.Single();
			var korekta = zaznaczona.PrzygotujKorekte(kontekst.Baza);
			return korekta;
		}
	}
}
