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
		private readonly bool pelnyEkran;

		public override string Nazwa => "Wyświetl zaznaczoną pozycję";

		public EdytujRekordAkcja(string tytul, bool pelnyEkran = false)
		{
			this.tytul = tytul;
			this.pelnyEkran = pelnyEkran;
		}

		public override bool CzyDostepnaDlaRekordow(IEnumerable<TRekord> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;

		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.None && (klawisz == Keys.Enter || klawisz == Keys.F4);

		public override void Uruchom(Kontekst kontekst, IEnumerable<TRekord> zaznaczoneRekordy)
		{
			using var nowyKontekst = new Kontekst(kontekst);
			using var transakcja = nowyKontekst.Transakcja();
			var rekord = nowyKontekst.Baza.Znajdz(zaznaczoneRekordy.Single().Ref);
			while (true)
			{
				try
				{
					using var edytor = new TEdytor();
					using var okno = new Dialog(tytul, edytor, nowyKontekst);
					if (pelnyEkran) okno.WindowState = FormWindowState.Maximized;
					edytor.Przygotuj(nowyKontekst, rekord);
					if (okno.ShowDialog() == DialogResult.OK)
					{
						nowyKontekst.Baza.Zapisz(rekord);
						transakcja.Zatwierdz();
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
