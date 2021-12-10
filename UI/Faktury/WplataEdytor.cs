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
			MinimumSize = new Size(250, 100);
		}

		protected override void PrzygotujRekord(Wplata rekord)
		{
			base.PrzygotujRekord(rekord);
			if (rekord.Kwota == 0) rekord.Kwota = Kontekst.Baza.Faktury.Where(faktura => faktura.Id == rekord.FakturaId).Include(faktura => faktura.Wplaty).First().PozostaloDoZaplaty;
		}
	}
}
