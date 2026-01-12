using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class PozycjaListy<T>
	{
		public T Wartosc { get; set; }
		public string Opis { get; set; }

		public PozycjaListy()
		{
		}
	}

	class PozycjaListyRekordu<T>
		where T : Rekord<T>
	{
		public T Wartosc { get; set; }
		public Ref<T> Ref => Wartosc == null ? default : Wartosc.Ref;
		public string Opis { get; set; }

		public PozycjaListyRekordu()
		{
		}
	}
}
