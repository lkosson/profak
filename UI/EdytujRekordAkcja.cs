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
		where TEdytor : Edytor<TRekord>, new()
	{
		private readonly bool pelnyEkran;

		public override string Nazwa => "✎ Wyświetl [F2]";

		public EdytujRekordAkcja(bool pelnyEkran = false)
		{
			this.pelnyEkran = pelnyEkran;
		}

		public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;

		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.None && (klawisz == Keys.Enter || klawisz == Keys.F2);

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<TRekord> zaznaczoneRekordy)
		{
			using var nowyKontekst = new Kontekst(kontekst);
			using var transakcja = nowyKontekst.Transakcja();
			var rekordRef = zaznaczoneRekordy.Single().Ref;
			nowyKontekst.Baza.Zablokuj(rekordRef);
			var rekord = nowyKontekst.Baza.Znajdz(rekordRef);
			nowyKontekst.Dodaj(rekord);
			using var edytor = new TEdytor();
			using var okno = new Dialog("Edycja danych", edytor, nowyKontekst);
			if (pelnyEkran) okno.WindowState = FormWindowState.Maximized;
			edytor.Przygotuj(nowyKontekst, rekord);
			if (okno.ShowDialog() != DialogResult.OK) return;
			edytor.KoniecEdycji();
			nowyKontekst.Baza.Zapisz(rekord);
			transakcja.Zatwierdz();
		}
	}
}
