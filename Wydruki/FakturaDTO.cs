using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.Wydruki
{
	public class FakturaDTO
	{
		public string Tytul { get; set; }
		public string Podtytul { get; set; }
		public string Naglowek { get; set; }
		public string DaneSprzedawcy { get; set; }
		public string DaneNabywcy { get; set; }
		public string Stopka { get; set; }

		public string LP { get; set; }
		public string NaglowekPozycji { get; set; }
		public string OpisPozycji { get; set; }
		public decimal CenaNetto { get; set; }
		public string Ilosc { get; set; }
		public decimal WartoscNetto { get; set; }
		public string StawkaVAT { get; set; }
		public decimal WartoscVat { get; set; }
		public decimal WartoscBrutto { get; set; }
		public string Rabat { get; set; }
		public bool JestVAT { get; set; }
		public bool JestRabat { get; set; }

		public string NumerKSeF { get; set; }
		public string KodKSeF { get; set; }
	}
}
