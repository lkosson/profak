﻿using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFak.UI
{
	class StanNumeratoraEdytor : EdytorDwieKolumny<StanNumeratora>
	{
		public StanNumeratoraEdytor()
		{
			DodajTextBox(stanNumeratora => stanNumeratora.Parametry, "Parametry");
			DodajNumericUpDown(stanNumeratora => stanNumeratora.OstatniaWartosc, "Ostatnia wartość");
			UstawRozmiar();
		}
	}
}
