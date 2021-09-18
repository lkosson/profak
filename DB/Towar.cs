using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Towar : Rekord<Towar>
	{
		public string Nazwa { get; set; }
		public RodzajTowaru Rodzaj { get; set; }
		public Ref<StawkaVat> StawkaVatId { get; set; }
		public Ref<JednostkaMiary> JednostkaMiaryId { get; set; }
		public decimal CenaNetto { get; set; }
		public decimal CenaBrutto { get; set; }
		public bool CzyWedlugCenBrutto { get; set; }
	}

	enum RodzajTowaru
	{
		Towar,
		Usługa
	}
}
