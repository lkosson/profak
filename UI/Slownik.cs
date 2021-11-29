using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class Slownik<T>
		where T : Rekord<T>
	{
		private readonly Kontekst kontekst;
		private readonly ComboBox comboBox;
		private readonly Button button;
		private readonly Func<IEnumerable<T>> pobierzWartosci;
		private readonly Func<T, string> wyswietlanaWartosc;
		private readonly Action<T> ustawWartosc;
		private readonly Func<SpisZAkcjami<T>> generatorSpisu;
		private bool gotowy;

		public Slownik(Kontekst kontekst, ComboBox comboBox, Button button, Func<IEnumerable<T>> pobierzWartosci, Func<T, string> wyswietlanaWartosc, Action<T> ustawWartosc, Func<SpisZAkcjami<T>> generatorSpisu)
		{
			this.kontekst = kontekst;
			this.comboBox = comboBox;
			this.button = button;
			this.pobierzWartosci = pobierzWartosci;
			this.wyswietlanaWartosc = wyswietlanaWartosc;
			this.ustawWartosc = ustawWartosc;
			this.generatorSpisu = generatorSpisu;
		}

		public void Zainstaluj()
		{
			WypelnijListe();
			comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
			comboBox.HandleCreated += ComboBox_HandleCreated;
			comboBox.KeyDown += ComboBox_KeyDown;
			if (button != null) button.Click += button_Click;
			gotowy = comboBox.IsHandleCreated;
		}

		private void ComboBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F2) PokazSpis();
		}

		private void ComboBox_HandleCreated(object sender, EventArgs e)
		{
			gotowy = true;
		}

		private void PokazSpis()
		{
			var dotychczasowaPozycja = (PozycjaListyRekordu<T>)comboBox.SelectedItem;
			var wartosc = Spis.Wybierz(kontekst, generatorSpisu, "Wybierz pozycję", dotychczasowaPozycja?.Wartosc);
			if (wartosc == null) return;
			gotowy = false;
			WypelnijListe();
			gotowy = true;
			var nowaPozycja = comboBox.Items.Cast<PozycjaListyRekordu<T>>().FirstOrDefault(p => p.Wartosc == wartosc);
			if (nowaPozycja != null) comboBox.SelectedItem = nowaPozycja;
		}

		private void button_Click(object sender, EventArgs e)
		{
			PokazSpis();
		}

		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!gotowy) return;
			var pozycja = (PozycjaListyRekordu<T>)comboBox.SelectedItem;
			ustawWartosc(pozycja?.Wartosc);
		}

		private void WypelnijListe()
		{
			var dostepneWartosci = pobierzWartosci();
			comboBox.BeginUpdate();
			//comboBox.Items.Clear();
			var pozycje = new List<PozycjaListyRekordu<T>>();
			foreach (var wartosc in dostepneWartosci)
			{
				var pozycja = new PozycjaListyRekordu<T> { Wartosc = wartosc, Opis = wyswietlanaWartosc(wartosc) };
				//comboBox.Items.Add(pozycja);
				pozycje.Add(pozycja);
			}
			comboBox.DataSource = pozycje;
			comboBox.ValueMember = nameof(PozycjaListyRekordu<T>.Ref);
			comboBox.DisplayMember = nameof(PozycjaListyRekordu<T>.Opis);
			comboBox.EndUpdate();
		}
	}
}
