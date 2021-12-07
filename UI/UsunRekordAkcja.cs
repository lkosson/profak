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
		public override string Nazwa => "❌ Usuń zaznaczone pozycje";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => zaznaczoneRekordy.Count() >= 1;
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.None && (klawisz == Keys.Delete || klawisz == Keys.F8);

		public override void Uruchom(Kontekst kontekst, IEnumerable<TRekord> zaznaczoneRekordy)
		{
			using var nowyKontekst = new Kontekst(kontekst);
			if (MessageBox.Show("Czy na pewno chcesz usunąć zaznaczoną pozycję?", "ProFak", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
			using var transakcja = nowyKontekst.Transakcja();
			nowyKontekst.Baza.Usun(zaznaczoneRekordy);
			transakcja.Zatwierdz();
		}
	}
}
