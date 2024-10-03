using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class UrzadSkarbowyEdytor : EdytorDwieKolumny<UrzadSkarbowy>
	{
		public UrzadSkarbowyEdytor()
		{
			DodajTextBox(urzad => urzad.Kod, "Kod", wymagane: true);
			DodajTextBox(urzad => urzad.Nazwa, "Nazwa", wymagane: true);
			UstawRozmiar();
		}
	}
}
