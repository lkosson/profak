using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak
{
	public static class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			if (args.Length > 0)
			{
				if (args[0] == "xsd") Wydruki.GeneratorXSD.Utworz();
				return;
			}

			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			if (!DB.Baza.Przygotuj())
			{
				using var pierwszyStart = new UI.PierwszyStartBaza();
				if (pierwszyStart.ShowDialog() != DialogResult.OK) return;
			}

			Application.Run(new UI.GlowneOkno());
		}
	}
}
