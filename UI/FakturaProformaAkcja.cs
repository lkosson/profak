﻿using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class FakturaProformaAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
	{
		public FakturaProformaAkcja()
			: base("Nowa faktura pro forma", faktura => faktura.Rodzaj = RodzajFaktury.Proforma /*, pelnyEkran: true */)
		{
		}

		protected override void PrzedZapisem(Kontekst kontekst, Faktura rekord)
		{
			base.PrzedZapisem(kontekst, rekord);
			rekord.Numer = Numerator.NadajNumer(kontekst.Baza, PrzeznaczenieNumeratora.Proforma, rekord.Podstawienie, false);
		}
	}
}
