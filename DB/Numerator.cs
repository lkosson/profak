using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Numerator : Rekord<Numerator>
	{
		public PrzeznaczenieNumeratora Przeznaczenie { get; set; } = PrzeznaczenieNumeratora.Faktura;
		public string Format { get; set; } = "[Numer]";

		public string PrzeznaczenieFmt => Przeznaczenie.ToString();

		public List<StanNumeratora> Stany { get; set; }
	}

	enum PrzeznaczenieNumeratora
	{
		Faktura
	}
}
