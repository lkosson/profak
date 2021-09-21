using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var rekord = new DB.StawkaVat { Skrot = "23", Wartosc = 23, CzyDomyslna = true };
			new UI.OknoEdycji("Stawka VAT", new UI.StawkaVatEdytor(rekord)).ShowDialog();
		}
	}
}
