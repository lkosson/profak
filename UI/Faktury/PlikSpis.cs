using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class PlikSpis : Spis<Plik>
	{
		public Ref<Faktura> FakturaRef { get; set; }

		public PlikSpis()
		{
			DodajKolumne(nameof(Plik.Nazwa), "Nazwa", rozciagnij: true);
			DodajKolumne(nameof(Plik.Rozmiar), "Rozmiar", wyrownajDoPrawej: true);
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			IQueryable<Plik> q = Kontekst.Baza.Pliki;
			if (FakturaRef.IsNotNull) q = q.Where(plik => plik.FakturaId == FakturaRef.Id);
			Rekordy = q.ToList();
		}
	}
}
