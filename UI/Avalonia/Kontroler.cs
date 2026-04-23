#if AVALONIA
using Avalonia.Data;

namespace ProFak.UI;

partial class Kontroler<TModel>
{
	public void Powiazanie(TDatePicker dateTimePicker, Func<TModel, DateTime> pobierzWartosc, Action<TModel, DateTime>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		dateTimePicker.SelectedDateChanged += delegate { AktualizujModel(dateTimePicker, ustawWartosc, dtp => dtp.SelectedDate?.LocalDateTime ?? default); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(dateTimePicker, pobierzWartosc, (dtp, wartosc) => dtp.SelectedDate = wartosc);
	}

	public void Powiazanie(TDatePicker dateTimePicker, Func<TModel, DateTime?> pobierzWartosc, Action<TModel, DateTime?>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		dateTimePicker.SelectedDateChanged += delegate { AktualizujModel(dateTimePicker, ustawWartosc, dtp => dtp.SelectedDate.HasValue ? (DateTime?)dtp.SelectedDate?.LocalDateTime : null); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(dateTimePicker, pobierzWartosc, (dtp, wartosc) => { dtp.SelectedDate = wartosc; });
	}

	public void Powiazanie(TNumericUpDown numericUpDown, Func<TModel, int> pobierzWartosc, Action<TModel, int>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		Powiazanie(numericUpDown, model => pobierzWartosc(model), (TModel model, decimal wartosc) => ustawWartosc?.Invoke(model, (int)wartosc), wartoscZmieniona);
	}

	public void Powiazanie(TNumericUpDown numericUpDown, Func<TModel, decimal> pobierzWartosc, Action<TModel, decimal>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		numericUpDown.ValueChanged += delegate { if (ustawWartosc != null) AktualizujModel(numericUpDown, ustawWartosc, nud => nud.Value ?? 0); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(numericUpDown, pobierzWartosc, (nud, wartosc) => nud.Value = wartosc);
	}

	public void Powiazanie(TTextBox textBox, Func<TModel, string> pobierzWartosc, Action<TModel, string>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		textBox.TextChanged += delegate { AktualizujModel(textBox, ustawWartosc, txt => txt.Text ?? ""); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(textBox, pobierzWartosc, (txt, wartosc) => txt.Text = wartosc);
	}

	public void Powiazanie(TCheckBox checkBox, Func<TModel, bool> pobierzWartosc, Action<TModel, bool>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		checkBox.IsCheckedChanged += delegate { AktualizujModel(checkBox, ustawWartosc, chk => chk.IsChecked ?? false); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(checkBox, pobierzWartosc, (chk, wartosc) => chk.IsChecked = wartosc);
	}

	public void Powiazanie(TSuggestBox suggestBox, Func<TModel, string> pobierzWartosc, Action<TModel, string>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		suggestBox.TextChanged += delegate { AktualizujModel(suggestBox, ustawWartosc, suggestBox => suggestBox.Text ?? ""); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(suggestBox, pobierzWartosc, (suggestBox, wartosc) => { suggestBox.Text = wartosc; });
	}

	public void Powiazanie(TComboBox comboBox, Func<TModel, int> pobierzWartosc, Action<TModel, int>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		comboBox.SelectionChanged += delegate { AktualizujModel(comboBox, ustawWartosc, comboBox => comboBox.SelectedIndex); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(comboBox, pobierzWartosc, (comboBox, wartosc) => { comboBox.SelectedIndex = wartosc; });
	}

	public void Powiazanie<TWartosc>(TComboBox comboBox, Func<TModel, TWartosc> pobierzWartosc, Action<TModel, TWartosc>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		comboBox.SelectionChanged += delegate { AktualizujModel(comboBox, (model, wartosc) => ustawWartosc?.Invoke(model, wartosc!), comboBox => comboBox.SelectedIndex == -1 ? default : (TWartosc)comboBox.SelectedValue!); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(comboBox, pobierzWartosc, (comboBox, wartosc) => { if (comboBox.SelectedIndex != -1) comboBox.SelectedIndex = -1; if (wartosc != null) comboBox.SelectedValue = wartosc; });
	}

	public void Powiazanie<TWartosc>(TSuggestBox suggestBox, Func<TModel, TWartosc> pobierzWartosc, Action<TModel, TWartosc>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		suggestBox.SelectionChanged += delegate { AktualizujModel(suggestBox, (model, wartosc) => ustawWartosc?.Invoke(model, wartosc!), suggestBox => suggestBox.SelectedItem is TWartosc wartosc ? wartosc : default); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(suggestBox, pobierzWartosc, (suggestBox, wartosc) => { suggestBox.SelectedItem = wartosc; });
	}

	private void Slownik<T>(TComboBox comboBox, PozycjaListy<T>[] wartosci)
	{
		comboBox.SelectedValueBinding = CompiledBinding.Create<PozycjaListy<T>, T>(pozycja => pozycja.Wartosc!);
		comboBox.DisplayMemberBinding = CompiledBinding.Create<PozycjaListy<T>, string>(pozycja => pozycja.Opis);
		comboBox.ItemsSource = wartosci;
	}
}
#endif
