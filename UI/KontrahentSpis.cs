using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class KontrahentSpis : Spis<Kontrahent>
	{
		public KontrahentSpis()
		{
			DodajKolumne(nameof(Kontrahent.Nazwa), "Nazwa", rozciagnij: true);
			DodajKolumne(nameof(Kontrahent.NIP), "NIP");
			DodajKolumne(nameof(Kontrahent.AdresRejestrowy), "Adres");
			DodajKolumne(nameof(Kontrahent.Id), "Id", wyrownajDoPrawej: true);
		}

		public override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.Kontrahenci.ToList();
		}
	}
}
