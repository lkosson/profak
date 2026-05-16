#if AVALONIA
using Avalonia.Data;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ProFak.UI;

partial class Kontroler<TModel>
{
	private readonly List<PowiazanaWartosc> powiazaneWartosci = [];
	public bool CzyModelPoprawny => powiazaneWartosci.All(e => !e.CzyBlad);

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

		void AktualizujKontrolke()
		{
			if (wartosc.CzyWlasnaZmiana) return;
			wartosc.CzyWlasnaZmiana = true;
			wartosc.Wartosc = pobierzWartosc(model);
			wartosc.CzyWlasnaZmiana = false;
		}

		void AktualizujModel()
		{
			ustawWartosc?.Invoke(model, wartosc.Wartosc!);
			wartoscZmieniona?.Invoke();
			modelZmieniony |= !wartosc.CzyWlasnaZmiana;
		}

		wartosc.PropertyChanged += delegate { AktualizujModel(); };
		kontrolka.DataContext = wartosc;
		kontrolka.Bind(wlasciwosc, PowiazanaWartosc<T>.Binding);
		powiazania.Add(AktualizujKontrolke);
		powiazaneWartosci.Add(wartosc);
	}

	public void Slownik<T>(TComboBox comboBox, PozycjaListy<T>[] wartosci)
	{
		comboBox.SelectedValueBinding = CompiledBinding.Create<PozycjaListy<T>, T>(pozycja => pozycja.Wartosc!);
		comboBox.DisplayMemberBinding = CompiledBinding.Create<PozycjaListy<T>, string>(pozycja => pozycja.Opis);
		comboBox.ItemsSource = wartosci;
	}
}

class PowiazanaWartosc : INotifyPropertyChanged
{
	private List<(Func<object?, string?> walidator, bool miekki)> walidatory = [];

	public bool CzyWlasnaZmiana { get; set; }
	public bool CzyTrwaZmiana { get; set; }
	public bool CzyBlad => SurowaWartosc is BindingNotification { ErrorType: not BindingErrorType.None, HasValue: false };
	public object? SurowaWartosc
	{
		get
		{
			return field;
		}

		set
		{
			object? nowaWartosc = value;
			foreach (var (walidator, miekki) in walidatory)
			{
				var wynik = walidator(nowaWartosc);
				if (wynik == null) continue;
				nowaWartosc = new BindingNotification(new DataValidationException(wynik), BindingErrorType.DataValidationError, miekki ? nowaWartosc : Avalonia.AvaloniaProperty.UnsetValue);
				break;
			}
			field = nowaWartosc;
			if (CzyTrwaZmiana) return;
			CzyTrwaZmiana = true;
			try
			{
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SurowaWartosc)));
			}
			finally
			{
				CzyTrwaZmiana = false;
			}
		}
	}

	public static CompiledBinding Binding = CompiledBinding.Create<PowiazanaWartosc, object?>(e => e.SurowaWartosc);
	public event PropertyChangedEventHandler? PropertyChanged;
	public void DodajWalidator(Func<object?, string?> walidator, bool miekki) => walidatory.Add((walidator, miekki));
}

class PowiazanaWartosc<T> : PowiazanaWartosc
{
	public T? Wartosc
	{
		get => SurowaWartosc is T wartosc ? wartosc 
			: SurowaWartosc is BindingNotification { ErrorType: BindingErrorType.DataValidationError } notification && notification.Value is T wartoscBledna ? wartoscBledna 
			: default;
		set => SurowaWartosc = value;
	}

	public void DodajWalidator(Func<T?, string?> walidator, bool miekki) => base.DodajWalidator(surowaWartosc => surowaWartosc is T wartosc || surowaWartosc == null ? walidator((T?)surowaWartosc) : null, miekki);
}

#endif
