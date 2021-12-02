using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.Wydruki
{
	abstract class Wydruk
	{
		public abstract void Przygotuj(Microsoft.Reporting.WinForms.LocalReport report);
	}
}
