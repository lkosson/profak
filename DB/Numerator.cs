using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Numerator : Rekord<Numerator>
	{
		public PrzeznaczenieNumeratora Przeznaczenie { get; set; }
		public string Format { get; set; } = "";

		public string PrzeznaczenieFmt => Przeznaczenie.ToString();

		public List<StanNumeratora> Stany { get; set; }

		public override void WypelnijDomyslnePola(Baza baza)
		{
			base.WypelnijDomyslnePola(baza);
			Przeznaczenie = PrzeznaczenieNumeratora.Faktura;
			Format = "[Numer]";
		}
	}

	enum PrzeznaczenieNumeratora
	{
		Faktura
	}
}
