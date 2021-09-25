using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	interface ISpis<TRekord>
		where TRekord : Rekord<TRekord>
	{
		Kontekst Kontekst { get; set; }
		IEnumerable<TRekord> WybraneRekordy { get; set; }
		void Przeladuj();
		string Tytul { get; }
	}
}
