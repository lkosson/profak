#if AVALONIA
using Avalonia.Data;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ProFak.UI;

partial class Kontroler<TModel>
{
	public void Powiazanie(TSuggestBox suggestBox, Expression<Func<TModel, string>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(suggestBox, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(TSuggestBox suggestBox, Expression<Func<TModel, int>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(suggestBox, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie<TWartosc>(TSuggestBox suggestBox, Expression<Func<TModel, TWartosc>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(suggestBox, wlasciwosc, Powiazanie, wartoscZmieniona);

	public void Powiazanie(TDatePicker dateTimePicker, Func<TModel, DateTime> pobierzWartosc, Action<TModel, DateTime>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		DodajPowiazanie(dateTimePicker, TDatePicker.SelectedDateProperty, pobierzWartosc, ustawWartosc, wartoscZmieniona);
	}

	public void Powiazanie(TDatePicker dateTimePicker, Func<TModel, DateTime?> pobierzWartosc, Action<TModel, DateTime?>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		DodajPowiazanie(dateTimePicker, TDatePicker.SelectedDateProperty, pobierzWartosc, ustawWartosc, wartoscZmieniona);
	}

	public void Powiazanie(TNumericUpDown numericUpDown, Func<TModel, int> pobierzWartosc, Action<TModel, int>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		Powiazanie(numericUpDown, model => pobierzWartosc(model), (TModel model, decimal wartosc) => ustawWartosc?.Invoke(model, (int)wartosc), wartoscZmieniona);
	}

	public void Powiazanie(TNumericUpDown numericUpDown, Func<TModel, decimal> pobierzWartosc, Action<TModel, decimal>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		DodajPowiazanie(numericUpDown, TNumericUpDown.ValueProperty, pobierzWartosc, ustawWartosc, wartoscZmieniona);
	}

	public void Powiazanie(TTextBox textBox, Func<TModel, string> pobierzWartosc, Action<TModel, string>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		DodajPowiazanie(textBox, TTextBox.TextProperty, pobierzWartosc, ustawWartosc, wartoscZmieniona);
	}

	public void Powiazanie(TCheckBox checkBox, Func<TModel, bool> pobierzWartosc, Action<TModel, bool>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		DodajPowiazanie(checkBox, TCheckBox.IsCheckedProperty, pobierzWartosc, ustawWartosc, wartoscZmieniona);
	}

	public void Powiazanie(TSuggestBox suggestBox, Func<TModel, string> pobierzWartosc, Action<TModel, string>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		DodajPowiazanie(suggestBox, TSuggestBox.TextProperty, pobierzWartosc, ustawWartosc, wartoscZmieniona);
	}

	public void Powiazanie(TComboBox comboBox, Func<TModel, int> pobierzWartosc, Action<TModel, int>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		DodajPowiazanie(comboBox, TComboBox.SelectedIndexProperty, pobierzWartosc, ustawWartosc, wartoscZmieniona);
	}

	public void Powiazanie<TWartosc>(TComboBox comboBox, Func<TModel, TWartosc> pobierzWartosc, Action<TModel, TWartosc>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		DodajPowiazanie(comboBox, TComboBox.SelectedValueProperty, pobierzWartosc, ustawWartosc, wartoscZmieniona);
	}

	public void Powiazanie<TWartosc>(TSuggestBox suggestBox, Func<TModel, TWartosc> pobierzWartosc, Action<TModel, TWartosc>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		DodajPowiazanie(suggestBox, TSuggestBox.SelectedItemProperty, pobierzWartosc, ustawWartosc, wartoscZmieniona);
	}

	private void DodajPowiazanie<T>(TControl kontrolka, Avalonia.AvaloniaProperty wlasciwosc, Func<TModel, T> pobierzWartosc, Action<TModel, T>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		var wartosc = new PowiazanaWartosc<T>();
		wartosc.PropertyChanged += delegate { ustawWartosc?.Invoke(model, wartosc.Wartosc); wartoscZmieniona?.Invoke(); modelZmieniony |= !wartosc.CzyWlasnaZmiana; };
		kontrolka.DataContext = wartosc;
		kontrolka.Bind(wlasciwosc, PowiazanaWartosc<T>.Binding);
		powiazania.Add(delegate { wartosc.CzyWlasnaZmiana = true; wartosc.Wartosc = pobierzWartosc(model); wartosc.CzyWlasnaZmiana = false; });
	}

	public void Slownik<T>(TComboBox comboBox, PozycjaListy<T>[] wartosci)
	{
		comboBox.SelectedValueBinding = CompiledBinding.Create<PozycjaListy<T>, T>(pozycja => pozycja.Wartosc!);
		comboBox.DisplayMemberBinding = CompiledBinding.Create<PozycjaListy<T>, string>(pozycja => pozycja.Opis);
		comboBox.ItemsSource = wartosci;
	}
}

class PowiazanaWartosc<T> : INotifyPropertyChanged
{
	public static CompiledBinding Binding = CompiledBinding.Create<PowiazanaWartosc<T>, T?>(e => e.Wartosc);

	public bool CzyWlasnaZmiana { get; set; }
	public bool CzyTrwaZmiana { get; set; }

	public T? Wartosc
	{
		get
		{
			return field;
		}

		set
		{
			field = value;
			if (CzyTrwaZmiana) return;
			CzyTrwaZmiana = true;
			try
			{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Wartosc)));
			}
			finally
			{
				CzyTrwaZmiana = false;
			}
		}
	}

	public event PropertyChangedEventHandler? PropertyChanged;
}

#endif
