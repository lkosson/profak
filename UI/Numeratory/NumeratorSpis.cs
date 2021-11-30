using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class NumeratorSpis : Spis<Numerator>
	{
		public NumeratorSpis()
		{
			DodajKolumne(nameof(Numerator.Przeznaczenie), "Przeznaczenie", rozciagnij: true);
			DodajKolumne(nameof(Numerator.Format), "Format");
			DodajKolumneId();
		}

		protected override void Przeladuj()
		{
			Rekordy = Kontekst.Baza.Numeratory.ToList();
		}
	}
}
