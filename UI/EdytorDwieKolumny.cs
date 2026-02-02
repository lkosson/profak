using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	abstract class EdytorDwieKolumny<TRekord> : Edytor<TRekord>
		where TRekord : Rekord<TRekord>
	{
		private readonly DwieKolumny dwieKolumny;

		public EdytorDwieKolumny()
		{
			dwieKolumny = new DwieKolumny();
			Controls.Add(dwieKolumny);
		}

		public void UstawRozmiar()
		{
			Size = dwieKolumny.Size;
			dwieKolumny.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
		}

		public void DodajWiersz(Control kontrolka, string etykieta)
		{
			dwieKolumny.DodajWiersz(kontrolka, etykieta);
		}

		public void DodajTextBox(Expression<Func<TRekord, string>> wlasciwosc, string etykieta, bool wymagane = false)
		{
			var textbox = dwieKolumny.DodajTextBox(etykieta);
			kontroler.Powiazanie(textbox, wlasciwosc);
			if (wymagane) Wymagane(textbox);
		}

		public void DodajCheckBox(Expression<Func<TRekord, bool>> wlasciwosc, string etykieta)
		{
			var checkBox = dwieKolumny.DodajCheckBox(etykieta);
			kontroler.Powiazanie(checkBox, wlasciwosc);
		}

		public void DodajNumericUpDown(Expression<Func<TRekord, decimal>> wlasciwosc, string etykieta, int poprzecinku = 2)
		{
			var numericUpDown = dwieKolumny.DodajNumericUpDown(etykieta, poprzecinku);
			kontroler.Powiazanie(numericUpDown, wlasciwosc);
		}

		public void DodajNumericUpDown(Expression<Func<TRekord, int>> wlasciwosc, string etykieta)
		{
			var numericUpDown = dwieKolumny.DodajNumericUpDown(etykieta, 0);
			kontroler.Powiazanie(numericUpDown, wlasciwosc);
		}

		public void DodajDatePicker(Expression<Func<TRekord, DateTime>> wlasciwosc, string etykieta)
		{
			var dateTimePicker = dwieKolumny.DodajDatePicker(etykieta);
			kontroler.Powiazanie(dateTimePicker, wlasciwosc);
		}

		public void DodajComboBox<TEnum>(Expression<Func<TRekord, TEnum>> wlasciwosc, string etykieta, bool wymagane = false)
			where TEnum : struct, Enum
		{
			var comboBox = dwieKolumny.DodajComboBox(etykieta);
			kontroler.Slownik<TEnum>(comboBox);
			kontroler.Powiazanie(comboBox, wlasciwosc);
			if (wymagane) Wymagane(comboBox);
		}
	}
}
