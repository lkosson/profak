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
		public override string Nazwa => "➕ Wystaw pro formę";
		public FakturaProformaAkcja()
			: base(faktura => faktura.Rodzaj = RodzajFaktury.Proforma)
		{
		}

		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => false;

		protected override void ZapiszRekord(Kontekst kontekst, Faktura rekord)
		{
			rekord.Numer = Numerator.NadajNumer(kontekst.Baza, rekord.Numerator.Value, rekord.Podstawienie);
			base.ZapiszRekord(kontekst, rekord);
		}
	}
}
