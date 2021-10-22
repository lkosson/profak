using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class SwobodnySlownik<T>
		where T : Rekord<T>
	{
		private readonly ComboBox comboBox;
		private readonly Button button;
		private readonly Func<IEnumerable<T>> pobierzWartosci;
		private readonly Func<T, string> wyswietlanaWartosc;
		private readonly Action<T> ustawWartosc;
		private bool gotowy;

		public SwobodnySlownik(ComboBox comboBox, Button button, Func<IEnumerable<T>> pobierzWartosci, Func<T, string> wyswietlanaWartosc, Action<T> ustawWartosc)
		{
			this.comboBox = comboBox;
			this.button = button;
			this.pobierzWartosci = pobierzWartosci;
			this.wyswietlanaWartosc = wyswietlanaWartosc;
			this.ustawWartosc = ustawWartosc;
		}

		public void Zainstaluj()
		{
			WypelnijListe();
			comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
			comboBox.TextChanged += comboBox_TextChanged;
			gotowy = true;
		}

		private void comboBox_TextChanged(object sender, EventArgs e)
		{
		}

		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!gotowy) return;
			var pozycja = (PozycjaListy<T>)comboBox.SelectedItem;
			ustawWartosc(pozycja?.Wartosc);
		}

		private void WypelnijListe()
		{
			var dostepneWartosci = pobierzWartosci();
			comboBox.BeginUpdate();
			comboBox.Items.Clear();
			foreach (var wartosc in dostepneWartosci)
			{
				var pozycja = new PozycjaListy<T> { Wartosc = wartosc, Opis = wyswietlanaWartosc(wartosc) };
				comboBox.Items.Add(pozycja);
			}
			comboBox.ValueMember = nameof(PozycjaListy<T>.Wartosc);
			comboBox.DisplayMember = nameof(PozycjaListy<T>.Opis);
			comboBox.EndUpdate();
		}
	}
}
