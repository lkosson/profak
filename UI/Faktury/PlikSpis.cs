using ProFak.DB;
using System.ComponentModel;

namespace ProFak.UI
{
	class PlikSpis : Spis<Plik>
	{
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
			q = q.OrderBy(plik => plik.Id);
			Rekordy = q.ToList();
		}
	}
}
