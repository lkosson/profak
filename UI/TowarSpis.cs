using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class TowarSpis : Spis<Towar>
	{
		public TowarSpis()
		{
			DodajKolumne(nameof(Towar.Nazwa), "Nazwa", rozciagnij: true);
			DodajKolumne(nameof(Towar.Rodzaj), "Rodzaj");
			DodajKolumneKwota(nameof(Towar.CenaNetto), "Cena netto");
			DodajKolumneKwota(nameof(Towar.CenaBrutto), "Cena brutto");
			DodajKolumneId();
		}

		public override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.Towary.ToList();
		}
	}
}
