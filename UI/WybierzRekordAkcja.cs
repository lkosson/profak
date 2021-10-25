using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class WybierzRekordAkcja<TRekord> : AkcjaNaSpisie<TRekord>
		where TRekord : Rekord<TRekord>
	{
		public override bool CzyDomyslna => true;
		public override string Nazwa => "Wybierz zaznaczoną pozycję";
		public TRekord WybranyRekord { get; private set; }

		public WybierzRekordAkcja()
		{
		}

		public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;

		public override void Uruchom(Kontekst kontekst, IEnumerable<TRekord> zaznaczoneRekordy)
		{
			WybranyRekord = zaznaczoneRekordy.Single();
			kontekst.Dialog.DialogResult = DialogResult.OK;
			kontekst.Dialog.Close();
		}
	}
}
