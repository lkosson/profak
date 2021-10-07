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
	class EdytujRekordAkcja<TRekord, TEdytor> : AkcjaNaSpisie<TRekord>
		where TRekord : Rekord<TRekord>, new()
		where TEdytor : Control, IEdytor<TRekord>, new()
	{
		private readonly string tytul;

		public override string Nazwa => "Wyświetl zaznaczoną pozycję";

		public EdytujRekordAkcja(string tytul)
		{
			this.tytul = tytul;
		}

		public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;

		public override void Uruchom(Kontekst kontekst, IEnumerable<TRekord> zaznaczoneRekordy)
		{
			var rekord = zaznaczoneRekordy.Single();
			var kopiaRekordu = rekord.Kopia();
			using var edytor = new TEdytor();
			edytor.Kontekst = kontekst;
			edytor.Rekord = rekord;
			using var okno = new OknoEdycji(tytul, edytor);
			if (okno.ShowDialog() != DialogResult.OK)
			{
				kontekst.Baza.Entry(rekord).State = EntityState.Detached;
				kontekst.Baza.Entry(kopiaRekordu).State = EntityState.Modified;
			}
			kontekst.Baza.SaveChanges();
		}
	}
}
