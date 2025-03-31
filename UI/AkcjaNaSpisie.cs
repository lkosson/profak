using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	abstract class AkcjaNaSpisie<TRekord>
		where TRekord : Rekord<TRekord>
	{
		public abstract string Nazwa { get; }
		public abstract bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy);
		public abstract void Uruchom(Kontekst kontekst, ref IEnumerable<TRekord> zaznaczoneRekordy);
		public virtual bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => false;
		public virtual IReadOnlyCollection<AkcjaNaSpisie<TRekord>> Podrzedne => [];

		public AdapterAkcji UtworzAdapter(Spis<TRekord> spis) => new AdapterAkcji<TRekord>(this, spis);
	}
}
