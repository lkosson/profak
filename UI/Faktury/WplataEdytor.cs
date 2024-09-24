using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class WplataEdytor : EdytorDwieKolumny<Wplata>
	{
		public WplataEdytor()
		{
			DodajDatePicker(wplata => wplata.Data, "Data wpływu");
			DodajNumericUpDown(wplata => wplata.Kwota, "Kwota");
			MinimumSize = new Size(250, 60);
		}

		protected override void PrzygotujRekord(Wplata rekord)
		{
			base.PrzygotujRekord(rekord);
			var faktura = Kontekst.Znajdz<Faktura>();
			if (rekord.Kwota == 0 && faktura != null) rekord.Kwota = faktura.PozostaloDoZaplaty;
		}
	}
}
