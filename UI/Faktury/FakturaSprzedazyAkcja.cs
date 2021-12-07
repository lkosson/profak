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
		public override string Nazwa => "➕ Wystaw fakturę [INS]";

		public FakturaSprzedazyAkcja()
			: base(faktura => faktura.Rodzaj = RodzajFaktury.Sprzedaż)
		{
		}

		protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
		{
			rekord.Numer = Numerator.NadajNumer(kontekst.Baza, PrzeznaczenieNumeratora.Faktura, rekord.Podstawienie);
			base.ZapiszRekord(kontekst, rekord);
		}
	}
}
