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
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Kontekst Kontekst { get; set; } = default!;

		public EdytorTabeli()
		{
			InitializeComponent();
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			dataGridViewWynik.AllowUserToDeleteRows = true;
			dataGridViewWynik.RowHeadersVisible = true;
			comboBoxTabela.DisplayMember = nameof(PozycjaListy<IQueryable>.Opis);
			comboBoxTabela.ValueMember = nameof(PozycjaListy<IQueryable>.Wartosc);
			comboBoxTabela.DataSource = new[]
			{
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Deklaracje Vat", Wartosc = GeneratorDanych(Kontekst.Baza.DeklaracjeVat) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Dodatkowe podmioty", Wartosc = GeneratorDanych(Kontekst.Baza.DodatkowePodmioty) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Faktury", Wartosc = GeneratorDanych(Kontekst.Baza.Faktury) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Jednostki miar", Wartosc = GeneratorDanych(Kontekst.Baza.JednostkiMiar) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Kolumny spisów", Wartosc = GeneratorDanych(Kontekst.Baza.KolumnySpisow) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Konfiguracja", Wartosc = GeneratorDanych(Kontekst.Baza.Konfiguracja) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Kontrahenci", Wartosc = GeneratorDanych(Kontekst.Baza.Kontrahenci) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Numeratory", Wartosc = GeneratorDanych(Kontekst.Baza.Numeratory) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Pliki", Wartosc = GeneratorDanych(Kontekst.Baza.Pliki) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Pozycje faktur", Wartosc = GeneratorDanych(Kontekst.Baza.PozycjeFaktur) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Składki Zus", Wartosc = GeneratorDanych(Kontekst.Baza.SkladkiZus) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Sposoby płatności", Wartosc = GeneratorDanych(Kontekst.Baza.SposobyPlatnosci) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Stany menu", Wartosc = GeneratorDanych(Kontekst.Baza.StanyMenu) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Stany numeratorów", Wartosc = GeneratorDanych(Kontekst.Baza.StanyNumeratorow) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Stawki Vat", Wartosc = GeneratorDanych(Kontekst.Baza.StawkiVat) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Towary", Wartosc = GeneratorDanych(Kontekst.Baza.Towary) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Urzędy skarbowe", Wartosc = GeneratorDanych(Kontekst.Baza.UrzedySkarbowe) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Waluty", Wartosc = GeneratorDanych(Kontekst.Baza.Waluty) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Wpłaty", Wartosc = GeneratorDanych(Kontekst.Baza.Wplaty) },
				new PozycjaListy<Func<IEnumerable<object>>> { Opis = "Zawartości", Wartosc = GeneratorDanych(Kontekst.Baza.Zawartosci) }
			};
		}

		private Func<IEnumerable<object>> GeneratorDanych<T>(IQueryable<T> tabela)
			where T : DB.Rekord<T>
		{
			return delegate
			{
				var idOd = (int)numericUpDownIDOd.Value;
				var idDo = (int)numericUpDownIDDo.Value;
				return tabela.Where(rekord => rekord.Id >= idOd && rekord.Id <= idDo).Cast<object>().ToList();
			};
		}

		private void buttonUruchom_Click(object? sender, EventArgs e)
		{
			try
			{
				if (comboBoxTabela.SelectedValue is not Func<IEnumerable<object>> generator) return;
				var dane = generator();
				dataGridViewWynik.DataSource = dane;
				textBoxStatus.Text = "Liczba pozycji: " + dane.Count();
			}
			catch (Exception exc)
			{
				textBoxStatus.Text = exc.GetType() + ": " + exc.Message;
			}
		}

		private void dataGridViewWynik_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
		{
			try
			{
				textBoxStatus.Text = "Zapisywanie ...";
				using var nowyKontekst = new Kontekst(Kontekst);
				using var tx = nowyKontekst.Transakcja();
				var rekord = dataGridViewWynik.Rows[e.RowIndex].DataBoundItem;
				if (rekord != null) Kontekst.Baza.Zapisz(rekord);
				tx.Zatwierdz();
				textBoxStatus.Text = "Zapisano zmianę.";
			}
			catch (Exception exc)
			{
				textBoxStatus.Text = exc.GetType() + ": " + exc.Message;
			}
		}

		private void dataGridViewWynik_KeyUp(object? sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && e.Modifiers == Keys.None)
			{
				try
				{
					if (dataGridViewWynik.SelectedRows.Count == 0) return;
					textBoxStatus.Text = "Kasowanie ...";
					using var nowyKontekst = new Kontekst(Kontekst);
					using var tx = nowyKontekst.Transakcja();
					foreach (DataGridViewRow wiersz in dataGridViewWynik.SelectedRows)
					{
						var rekord = wiersz.DataBoundItem;
						if (rekord != null) Kontekst.Baza.Usun(rekord);
					}
					tx.Zatwierdz();
					if (dataGridViewWynik.SelectedRows.Count == 1) textBoxStatus.Text = "Usunięto rekord.";
					else textBoxStatus.Text = "Usunięto rekordy : " + dataGridViewWynik.SelectedRows.Count;
				}
				catch (Exception exc)
				{
					textBoxStatus.Text = exc.GetType() + ": " + exc.Message;
				}
			}
		}
	}
}
