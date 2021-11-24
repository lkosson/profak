using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	interface IEdytor<TRekord>
	{
		TRekord Rekord { get; }
		Kontekst Kontekst { get; }
		void Przygotuj(Kontekst kontekst, TRekord rekord);
	}
}
