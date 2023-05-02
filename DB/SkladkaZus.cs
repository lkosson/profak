using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class SkladkaZus : Rekord<SkladkaZus>
	{
		public DateTime Miesiac { get; set; } = DateTime.Now.Date.AddDays(1 - DateTime.Now.Day);

		public decimal PodstawaSpoleczne { get; set; }
		public decimal PodstawaZdrowotne { get; set; }
		public decimal SkladkaEmerytalna { get; set; }
		public decimal SkladkaRentowa { get; set; }
		public decimal SkladkaWypadkowa { get; set; }
		public decimal SkladkaSpoleczna { get; set; }
		public decimal SkladkaZdrowotna { get; set; }
		public decimal RozliczenieRoczneSkladkiZdrowotnej { get; set; }
		public decimal SkladkaFunduszPracy { get; set; }
		public decimal SumaSkladek { get; set; }
		public decimal OdliczenieOdDochodu { get; set; }

		public string MiesiacFmt => Miesiac.ToString("MM/yyyy");
	}
}
