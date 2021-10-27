using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class UsunRekordAkcja<TRekord> : AkcjaNaSpisie<TRekord>
		where TRekord : Rekord<TRekord>, new()
	{
		public override string Nazwa => "Usuń zaznaczone pozycje";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => zaznaczoneRekordy.Count() >= 1;

		public override void Uruchom(Kontekst kontekst, IEnumerable<TRekord> zaznaczoneRekordy)
		{
			if (MessageBox.Show("Czy na pewno chcesz usunąć zaznaczoną pozycję?", "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
			kontekst.Baza.Set<TRekord>().RemoveRange(zaznaczoneRekordy);
			kontekst.Zapisz();
		}
	}
}
