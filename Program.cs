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
			/*
			var rekord = new DB.StawkaVat { Skrot = "23", Wartosc = 23, CzyDomyslna = true };
			new UI.OknoEdycji("Stawka VAT", new UI.StawkaVatEdytor(rekord)).ShowDialog();
			*/

			using var kontekst = new UI.Kontekst();
			UI.OknoSpisu.Utworz<DB.StawkaVat, UI.StawkaVatSpis>(kontekst, new UI.DodajRekordAkcja<DB.StawkaVat, UI.StawkaVatEdytor>()).ShowDialog();

			//new UI.OknoSpisu("Stawki VAT", new UI.StawkaVatSpis { Baza = baza }).ShowDialog();
			//UI.OknoSpisu.Utworz(new UI.StawkaVatSpis { Baza = baza }, "Stawki VAT", );
		}
	}
}
