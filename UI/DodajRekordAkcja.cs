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
	class DodajRekordAkcja<TRekord, TEdytor> : AkcjaNaSpisie<TRekord>
		where TRekord : Rekord<TRekord>, new()
		where TEdytor : Control, IEdytor<TRekord>, new()
	{
		private readonly string tytul;
		private readonly Action<TRekord> przygotujRekord;

		public override string Nazwa => tytul;
		
		public DodajRekordAkcja(string tytul, Action<TRekord> przygotujRekord = null)
		{
			this.tytul = tytul;
			this.przygotujRekord = przygotujRekord;
		}

		public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => true;

		protected virtual TRekord UtworzRekord(Kontekst kontekst, IEnumerable<TRekord> zaznaczoneRekordy)
		{
			var rekord = new TRekord();
			if (przygotujRekord != null) przygotujRekord(rekord);
			kontekst.Baza.Set<TRekord>().Add(rekord);
			kontekst.Baza.Zapisz();
			return rekord;
		}

		public override void Uruchom(Kontekst kontekst, IEnumerable<TRekord> zaznaczoneRekordy)
		{
			using var nowyKontekst = new Kontekst(kontekst);
			using var transakcja = nowyKontekst.Transakcja();
			var rekord = UtworzRekord(nowyKontekst, zaznaczoneRekordy);
			while (true)
			{
				try
				{
					using var edytor = new TEdytor();
					using var okno = new Dialog(tytul, edytor, nowyKontekst);
					edytor.Przygotuj(nowyKontekst, rekord);
					if (okno.ShowDialog() == DialogResult.OK)
					{
						nowyKontekst.Baza.Zapisz();
						transakcja.Zatwierdz();
					}
					else
					{
						nowyKontekst.Baza.Entry(rekord).State = EntityState.Detached;
					}
					break;
				}
				catch (Exception exc)
				{
					MessageBox.Show($"Wystąpił nieobsłużony błąd. Uruchom ponownie program i spróbuj ponownie wykonać operację.\n\n{exc}", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
