using System.Data;
using System.Runtime.CompilerServices;

namespace ProFak.UI;

class EkranSQL : Edytor, IKontrolkaZKontekstem
{
	public Kontekst Kontekst { get; set; } = default!;

	private readonly TextBox textBoxSQL;
	private readonly Button buttonUruchom;
	private readonly TextBox textBoxStatus;
	private readonly DataGridView dataGridViewWynik;

	public EkranSQL()
	{
		textBoxSQL = Kontrolki.TextArea(linie: 5);
		buttonUruchom = Kontrolki.Button("Uruchom [F5]", akcja: Uruchom);
		textBoxStatus = Kontrolki.TextBox();
		dataGridViewWynik = new DataGridView();

		textBoxStatus.ReadOnly = true;
		textBoxSQL.KeyDown += textBoxSQL_KeyDown;

		var polecenie = new Siatka([-1, 0], []);
		polecenie.DodajWiersz([textBoxSQL, new Pionowo([buttonUruchom, textBoxStatus])]);

		var uklad = new Siatka([-1], [0, -1]);
		uklad.DodajWiersz([new Grupa("Polecenie", polecenie)]);
		uklad.DodajWiersz([new Grupa("Wynik", dataGridViewWynik)]);

		UstawZawartosc(uklad);
	}

	private void Uruchom()
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
		if (e.KeyCode == Keys.F5) Uruchom();
	}
}
