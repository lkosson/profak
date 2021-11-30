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
		public Ref<Faktura> FakturaRef { get; set; }

		public WplataSpis()
		{
			DodajKolumne(nameof(Wplata.Data), "Data");
			DodajKolumneKwota(nameof(Wplata.Kwota), "Kwota");
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			IQueryable<Wplata> q = Kontekst.Baza.Wplaty;
			if (FakturaRef.IsNotNull) q = q.Where(wplata => wplata.FakturaId == FakturaRef.Id);
			Rekordy = q.ToList();
		}
	}
}
