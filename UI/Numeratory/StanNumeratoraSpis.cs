using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class StanNumeratoraSpis : Spis<StanNumeratora>
	{
		public Ref<Numerator> NumeratorRef { get; set; }

		public StanNumeratoraSpis()
		{
			DodajKolumne(nameof(StanNumeratora.Parametry), "Parametry", rozciagnij: true);
			DodajKolumne(nameof(StanNumeratora.OstatniaWartosc), "Ostatnia wartość");
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			IQueryable<StanNumeratora> q = Kontekst.Baza.StanyNumeratorow;
			if (NumeratorRef.IsNotNull) q = q.Where(stanNumeratora => stanNumeratora.NumeratorId == NumeratorRef.Id);
			Rekordy = q.ToList();
		}
	}
}
