using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class PozycjaFaktury : Rekord<PozycjaFaktury>
	{
		public Ref<Faktura> FakturaId { get; set; }
		public Ref<Towar> TowarId { get; set; }
		public string Opis { get; set; }
		public decimal KwotaNetto { get; set; }
		public decimal KwotaVat { get; set; }
		public decimal KwotaBrutto { get; set; }
		public decimal Ilosc { get; set; }
		public decimal WartoscNetto { get; set; }
		public decimal WartoscVat { get; set; }
		public decimal WartoscBrutto { get; set; }
		public bool CzyWedlugCenBrutto { get; set; }
	}
}
