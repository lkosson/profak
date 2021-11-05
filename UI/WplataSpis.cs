using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class WplataSpis : Spis<Wplata>
	{
		public WplataSpis()
		{
			DodajKolumne(nameof(Wplata.Data), "Data");
			DodajKolumne(nameof(Wplata.Kwota), "Kwota", rozciagnij: true, wyrownajDoPrawej: true, format: "#,##0.00");
			DodajKolumne(nameof(Wplata.Id), "Id", wyrownajDoPrawej: true);
		}

		public override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.Wplaty.ToList();
		}
	}
}
