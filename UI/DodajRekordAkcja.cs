using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class DodajRekordAkcja<TRekord, TEdytor> : AkcjaNaSpisie<TRekord>
		where TRekord : Rekord<TRekord>, new()
		where TEdytor : Control, IEdytor<TRekord>, new()
	{
		private readonly string tytul;

		public override string Nazwa => "Dodaj nową pozycję";
		
		public DodajRekordAkcja(string tytul)
		{
			this.tytul = tytul;
		}

		public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => true;

		public override void Uruchom(Kontekst kontekst, IEnumerable<TRekord> zaznaczoneRekordy)
		{
			var rekord = new TRekord();
			using var edytor = new TEdytor();
			edytor.Kontekst = kontekst;
			edytor.Rekord = rekord;
			using var okno = new Dialog(tytul, edytor);
			if (okno.ShowDialog() != DialogResult.OK) return;
			kontekst.Baza.Set<TRekord>().Add(rekord);
			kontekst.Baza.SaveChanges();
		}
	}
}
