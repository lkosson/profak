using Microsoft.EntityFrameworkCore;
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
			if (!DB.Baza.Przygotuj())
			{
				using var pierwszyStart = new UI.PierwszyStartBaza();
				if (pierwszyStart.ShowDialog() != DialogResult.OK) return;
			}

			using var kontekst = new UI.Kontekst();
			UI.Spis.SposobyPlatnosci(kontekst).ShowDialog();
		}
	}
}
