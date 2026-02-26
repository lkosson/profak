using ProFak.DB;
using System.Linq.Expressions;

namespace ProFak.UI;

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

	public TextBox DodajTextBox(Expression<Func<TRekord, string>> wlasciwosc, string etykieta, bool wymagane = false, int linie = 1)
	{
		var textbox = dwieKolumny.DodajTextBox(etykieta, linie);
		kontroler.Powiazanie(textbox, wlasciwosc);
		if (wymagane) Wymagane(textbox);
		return textbox;
	}

	public CheckBox DodajCheckBox(Expression<Func<TRekord, bool>> wlasciwosc, string etykieta)
	{
		var checkBox = dwieKolumny.DodajCheckBox(etykieta);
		kontroler.Powiazanie(checkBox, wlasciwosc);
		return checkBox;
	}

	public NumericUpDown DodajNumericUpDown(Expression<Func<TRekord, decimal>> wlasciwosc, string etykieta, int poprzecinku = 2)
	{
		var numericUpDown = dwieKolumny.DodajNumericUpDown(etykieta, poprzecinku);
		kontroler.Powiazanie(numericUpDown, wlasciwosc);
		return numericUpDown;
	}

	public NumericUpDown DodajNumericUpDown(Expression<Func<TRekord, int>> wlasciwosc, string etykieta)
	{
		var numericUpDown = dwieKolumny.DodajNumericUpDown(etykieta, 0);
		kontroler.Powiazanie(numericUpDown, wlasciwosc);
		return numericUpDown;
	}

	public DateTimePicker DodajDatePicker(Expression<Func<TRekord, DateTime>> wlasciwosc, string etykieta)
	{
		var dateTimePicker = dwieKolumny.DodajDatePicker(etykieta);
		kontroler.Powiazanie(dateTimePicker, wlasciwosc);
		return dateTimePicker;
	}

	public ComboBox DodajComboBox<TEnum>(Expression<Func<TRekord, TEnum>> wlasciwosc, string etykieta, bool wymagane = false)
		where TEnum : struct, Enum
	{
		var comboBox = dwieKolumny.DodajComboBox(etykieta);
		kontroler.Slownik<TEnum>(comboBox);
		kontroler.Powiazanie(comboBox, wlasciwosc);
		if (wymagane) Wymagane(comboBox);
		return comboBox;
	}
}
