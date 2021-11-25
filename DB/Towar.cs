using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Towar : Rekord<Towar>
	{
		public string Nazwa { get; set; } = "";
		public RodzajTowaru Rodzaj { get; set; } = RodzajTowaru.Towar;
		public decimal CenaNetto { get; set; }
		public decimal CenaBrutto { get; set; }
		public bool CzyWedlugCenBrutto { get; set; }
		public bool CzyArchiwalny { get; set; }

		public int? StawkaVatId { get; set; }
		public int? JednostkaMiaryId { get; set; }

		public Ref<StawkaVat> StawkaVatRef { get => StawkaVatId; set => StawkaVatId = value; }
		public Ref<JednostkaMiary> JednostkaMiaryRef { get => JednostkaMiaryId; set => JednostkaMiaryId = value; }

		public StawkaVat StawkaVat { get; set; }
		public JednostkaMiary JednostkaMiary { get; set; }
	}

	enum RodzajTowaru
	{
		Towar,
		Usługa
	}
}
