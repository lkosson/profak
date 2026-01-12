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
		private bool ustawionaNowaWartosc;
		private bool dopuscPustaWartosc;

		public Slownik(Kontekst kontekst, ComboBox comboBox, Button button, Func<IEnumerable<T>> pobierzWartosci, Func<T, string> wyswietlanaWartosc, Action<T> ustawWartosc, Func<SpisZAkcjami<T>> generatorSpisu, bool dopuscPustaWartosc = false)
		{
			this.kontekst = kontekst;
			this.comboBox = comboBox;
			this.button = button;
			this.pobierzWartosci = pobierzWartosci;
			this.wyswietlanaWartosc = wyswietlanaWartosc;
			this.ustawWartosc = ustawWartosc;
			this.generatorSpisu = generatorSpisu;
			this.dopuscPustaWartosc = dopuscPustaWartosc;
		}

		public void Zainstaluj()
		{
			WypelnijListe();
			if (comboBox.DropDownStyle != ComboBoxStyle.DropDownList)
			{
				comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
				comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
			}
			comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
			comboBox.HandleCreated += ComboBox_HandleCreated;
			comboBox.KeyDown += ComboBox_KeyDown;
			if (button != null) button.Click += button_Click;
			gotowy = comboBox.IsHandleCreated;
		}

		public void Przeladuj()
		{
			var wybranaWartosc = comboBox.SelectedItem;
			gotowy = false;
			WypelnijListe();
			gotowy = true;
			comboBox.SelectedItem = wybranaWartosc;
		}

		private void ComboBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F2) PokazSpis();
			if (dopuscPustaWartosc && (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)) comboBox.SelectedIndex = -1;
		}

		private void ComboBox_HandleCreated(object sender, EventArgs e)
		{
			gotowy = true;
		}

		private void PokazSpis()
		{
			var dotychczasowaPozycja = (PozycjaListyRekordu<T>)comboBox.SelectedItem;
			using var spis = generatorSpisu();
			var wartosc = Spisy.Wybierz(kontekst, spis, "Wybierz pozycję", dotychczasowaPozycja?.Wartosc);
			if (wartosc == null) return;
			gotowy = false;
			WypelnijListe();
			comboBox.SelectedIndex = -1;
			gotowy = true;
			ustawionaNowaWartosc = false;
			var nowaPozycja = comboBox.Items.Cast<PozycjaListyRekordu<T>>().FirstOrDefault(p => p.Wartosc == wartosc);
			if (nowaPozycja != null) comboBox.SelectedItem = nowaPozycja;
			if (!ustawionaNowaWartosc) ustawWartosc(wartosc);
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
			if (comboBox.Focused && comboBox.DropDownStyle == ComboBoxStyle.DropDown) comboBox.Focus();
			ustawionaNowaWartosc = true;
		}

		private void WypelnijListe()
		{
			var dostepneWartosci = pobierzWartosci();
			comboBox.BeginUpdate();
			var pozycje = new List<PozycjaListyRekordu<T>>();
			if (dopuscPustaWartosc) pozycje.Add(new PozycjaListyRekordu<T> { Wartosc = null, Opis = "" });
			foreach (var wartosc in dostepneWartosci)
			{
				var opis = wyswietlanaWartosc(wartosc);
				if (String.IsNullOrEmpty(opis)) continue;
				var pozycja = new PozycjaListyRekordu<T> { Wartosc = wartosc, Opis = opis };
				pozycje.Add(pozycja);
			}
			comboBox.DataSource = pozycje;
			comboBox.ValueMember = nameof(PozycjaListyRekordu<T>.Ref);
			comboBox.DisplayMember = nameof(PozycjaListyRekordu<T>.Opis);
			comboBox.EndUpdate();
		}
	}
}
