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
	partial class EkranSQL : UserControl, IKontrolkaZKontekstem
	{
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Kontekst Kontekst { get; set; } = default!;

		public EkranSQL()
		{
			InitializeComponent();
		}

		private void buttonUruchom_Click(object? sender, EventArgs e)
		{
			try
			{
				var wynik = Kontekst.Baza.Zapytanie(FormattableStringFactory.Create(textBoxSQL.Text));
				var tabela = new DataTable();
				if (wynik.Any())
				{
					foreach (var pole in wynik.First())
					{
						tabela.Columns.Add(pole.Key);
					}

					foreach (var wiersz in wynik)
					{
						tabela.Rows.Add(wiersz.Values.ToArray());
					}
				}

				dataGridViewWynik.DataSource = tabela;
				textBoxStatus.Text = "Liczba wierszy: " + tabela.Rows.Count;
			}
			catch (Exception exc)
			{
				textBoxStatus.Text = exc.GetType() + ": " + exc.Message;
			}
		}

		private void textBoxSQL_KeyDown(object? sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5) buttonUruchom_Click(sender, e);
		}
	}
}
