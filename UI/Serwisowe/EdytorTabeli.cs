using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	partial class EdytorTabeli : UserControl, IKontrolkaZKontekstem
	{
		public Kontekst Kontekst { get; set; }

		public EdytorTabeli()
		{
			InitializeComponent();
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			comboBoxTabela.DisplayMember = nameof(PozycjaListy<IQueryable>.Opis);
			comboBoxTabela.ValueMember = nameof(PozycjaListy<IQueryable>.Wartosc);
			comboBoxTabela.DataSource = new[]
			{
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Faktury", Wartosc = Kontekst.Baza.Faktury.Cast<object>().ToList },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Jednostki miar", Wartosc = Kontekst.Baza.JednostkiMiar.Cast<object>().ToList },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Kontrahenci", Wartosc = Kontekst.Baza.Kontrahenci.Cast<object>().ToList },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Numeratory", Wartosc = Kontekst.Baza.Numeratory.Cast<object>().ToList },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Pliki", Wartosc = Kontekst.Baza.Pliki.Cast<object>().ToList },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Pozycje faktur", Wartosc = Kontekst.Baza.PozycjeFaktur.Cast<object>().ToList },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Sposoby płatności", Wartosc = Kontekst.Baza.SposobyPlatnosci.Cast<object>().ToList },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Stany numeratorów", Wartosc = Kontekst.Baza.StanyNumeratorow.Cast<object>().ToList },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Towary", Wartosc = Kontekst.Baza.Towary.Cast<object>().ToList },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Waluty", Wartosc = Kontekst.Baza.Waluty.Cast<object>().ToList },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Wpłaty", Wartosc = Kontekst.Baza.Wplaty.Cast<object>().ToList },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Zawartości", Wartosc = Kontekst.Baza.Zawartosci.Cast<object>().ToList }
			};
		}

		private void buttonUruchom_Click(object sender, EventArgs e)
		{
			try
			{
				var generator = (Func<IEnumerable<object>>)comboBoxTabela.SelectedValue;
				var dane = generator();
				dataGridViewWynik.DataSource = dane;
				textBoxStatus.Text = "Liczba pozycji: " + dane.Count();
			}
			catch (Exception exc)
			{
				textBoxStatus.Text = exc.GetType() + ": " + exc.Message;
			}
		}

		private void dataGridViewWynik_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			try
			{
				textBoxStatus.Text = "Zapisywanie ...";
				using var nowyKontekst = new Kontekst(Kontekst);
				using var tx = nowyKontekst.Transakcja();
				var rekord = dataGridViewWynik.Rows[e.RowIndex].DataBoundItem;
				Kontekst.Baza.Zapisz(rekord);
				tx.Zatwierdz();
				textBoxStatus.Text = "Zapisano zmianę.";
			}
			catch (Exception exc)
			{
				textBoxStatus.Text = exc.GetType() + ": " + exc.Message;
			}
		}
	}
}
