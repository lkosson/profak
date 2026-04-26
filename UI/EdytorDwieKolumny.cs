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
	}

	public void UstawRozmiar()
	{
		UstawZawartosc(dwieKolumny);
	}

	public void DodajWiersz(TControl kontrolka, string? etykieta)
	{
		dwieKolumny.DodajWiersz(kontrolka, etykieta);
	}

	public TTextBox DodajTextBox(Expression<Func<TRekord, string>> wlasciwosc, string etykieta, bool wymagane = false)
	{
		var textbox = Kontrolki.TextBox();
		dwieKolumny.DodajWiersz(textbox, etykieta);
		kontroler.Powiazanie(textbox, wlasciwosc);
		if (wymagane) Wymagane(textbox);
		return textbox;
	}

	public TTextBox DodajTextArea(Expression<Func<TRekord, string>> wlasciwosc, string etykieta, bool wymagane = false, int linie = 1)
	{
		var textArea = Kontrolki.TextArea(linie);
		dwieKolumny.DodajWiersz(textArea, etykieta);
		kontroler.Powiazanie(textArea, wlasciwosc);
		if (wymagane) Wymagane(textArea);
		return textArea;
	}

	public TCheckBox DodajCheckBox(Expression<Func<TRekord, bool>> wlasciwosc, string etykieta)
	{
		var checkBox = Kontrolki.CheckBox(etykieta);
		dwieKolumny.DodajWiersz(checkBox, null);
		kontroler.Powiazanie(checkBox, wlasciwosc);
		return checkBox;
	}

	public TNumericUpDown DodajNumericUpDown(Expression<Func<TRekord, decimal>> wlasciwosc, string etykieta, int poprzecinku = 2)
	{
		var numericUpDown = Kontrolki.NumericUpDown(poprzecinku);
		dwieKolumny.DodajWiersz(numericUpDown, etykieta);
		kontroler.Powiazanie(numericUpDown, wlasciwosc);
		return numericUpDown;
	}

	public TNumericUpDown DodajNumericUpDown(Expression<Func<TRekord, int>> wlasciwosc, string etykieta)
	{
		var numericUpDown = Kontrolki.NumericUpDown(poPrzecinku: 0);
		dwieKolumny.DodajWiersz(numericUpDown, etykieta);
		kontroler.Powiazanie(numericUpDown, wlasciwosc);
		return numericUpDown;
	}

	public TDatePicker DodajDatePicker(Expression<Func<TRekord, DateTime>> wlasciwosc, string etykieta)
	{
		var dateTimePicker = Kontrolki.DatePicker();
		dwieKolumny.DodajWiersz(dateTimePicker, etykieta);
		kontroler.Powiazanie(dateTimePicker, wlasciwosc);
		return dateTimePicker;
	}

	public TComboBox DodajDropDownList<TEnum>(Expression<Func<TRekord, TEnum>> wlasciwosc, string etykieta, bool wymagane = false)
		where TEnum : struct, Enum
	{
		var comboBox = Kontrolki.DropDownList();
		dwieKolumny.DodajWiersz(comboBox, etykieta);
		kontroler.Slownik<TEnum>(comboBox);
		kontroler.Powiazanie(comboBox, wlasciwosc);
		if (wymagane) Wymagane(comboBox);
		return comboBox;
	}
}
