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
	class FakturaSprzedazyAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
	{
		public FakturaSprzedazyAkcja()
			: base("Nowa faktura sprzedaży", faktura => faktura.Rodzaj = RodzajFaktury.Sprzedaż /*, pelnyEkran: true */)
		{
		}

		protected override void PrzedZapisem(Kontekst kontekst, Faktura rekord)
		{
			base.PrzedZapisem(kontekst, rekord);
			rekord.Numer = Numerator.NadajNumer(kontekst.Baza, PrzeznaczenieNumeratora.Faktura, rekord.Podstawienie, false);
		}
	}
}
