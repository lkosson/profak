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
			DodajKolumne(nameof(Towar.CenaNetto), "Cena netto");
			DodajKolumne(nameof(Towar.CenaBrutto), "Cena brutto");
			DodajKolumne(nameof(Towar.Id), "Id", wyrownajDoPrawej: true);
		}

		public override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.Towary.ToList();
		}
	}
}
