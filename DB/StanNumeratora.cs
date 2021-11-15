using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class StanNumeratora : Rekord<StanNumeratora>
	{
		public int NumeratorId { get; set; }
		public string Parametry { get; set; } = "";
		public int OstatniaWartosc { get; set; }

		public Ref<Numerator> NumeratorRef { get => NumeratorId; set => NumeratorId = value; }

		public Numerator Numerator { get; set; }
	}
}
