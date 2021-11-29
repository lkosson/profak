using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Wplata : Rekord<Wplata>
	{
		public int FakturaId { get; set; }
		public DateTime Data { get; set; } = DateTime.Now.Date;
		public decimal Kwota { get; set; }

		public Ref<Faktura> FakturaRef { get => FakturaId; set => FakturaId = value; }

		public Faktura Faktura { get; set; }

		public override bool CzyPasuje(string fraza)
			=> base.CzyPasuje(fraza)
			|| CzyPasuje(Data, fraza)
			|| CzyPasuje(Kwota, fraza);
	}
}
