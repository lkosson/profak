using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class WplataEdytor : EdytorDwieKolumny<Wplata>
	{
		private readonly NumericUpDown numericUpDownKwota;

		public WplataEdytor()
		{
			DodajDatePicker(wplata => wplata.Data, "Data wpływu");
			numericUpDownKwota = DodajNumericUpDown(wplata => wplata.Kwota, "Kwota");
			DodajTextBox(wplata => wplata.Uwagi, "Uwagi");
			UstawRozmiar();
		}

		protected override void PrzygotujRekord(Wplata rekord)
		{
			base.PrzygotujRekord(rekord);
			var faktura = Kontekst.Znajdz<Faktura>();
			if (rekord.Kwota == 0 && faktura != null) rekord.Kwota = faktura.PozostaloDoZaplaty;
		}

		protected override void RekordGotowy()
		{
			base.RekordGotowy();
			var faktura = Kontekst.Znajdz<Faktura>();
			if (faktura == null)
			{
				numericUpDownKwota.Enabled = false;
				numericUpDownKwota.Text = "";
			}
		}
	}
}
