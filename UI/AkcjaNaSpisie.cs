using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	abstract class AkcjaNaSpisie<TRekord>
		where TRekord : Rekord<TRekord>
	{
		public virtual bool CzyDomyslna => false;
		public abstract string Nazwa { get; }
		public abstract bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy);
		public abstract void Uruchom(Kontekst kontekst, IEnumerable<TRekord> zaznaczoneRekordy);

		public AdapterAkcji UtworzAdapter(ISpis<TRekord> spis) => new AdapterAkcji<TRekord>(this, spis);
	}
}
