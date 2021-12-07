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
	class PrzeladujAkcja<TRekord> : AkcjaNaSpisie<TRekord>
		where TRekord : Rekord<TRekord>, new()
	{
		public override string Nazwa => "⟳ Przeładuj spis";
		
		public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => true;
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.None && klawisz == Keys.F5;

		public override void Uruchom(Kontekst kontekst, IEnumerable<TRekord> zaznaczoneRekordy)
		{
		}
	}
}
