#if WINFORMS
using System.Linq.Expressions;
using System.Reflection;

namespace ProFak.UI;

partial class Kontroler<TModel>
{
	public void Powiazanie(TDatePicker dateTimePicker, Func<TModel, DateTime> pobierzWartosc, Action<TModel, DateTime>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		dateTimePicker.ValueChanged += delegate { AktualizujModel(dateTimePicker, ustawWartosc, dtp => dtp.Value); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(dateTimePicker, pobierzWartosc, (dtp, wartosc) => dtp.Value = wartosc);
	}

	public void Powiazanie(TDatePicker dateTimePicker, Func<TModel, DateTime?> pobierzWartosc, Action<TModel, DateTime?>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		dateTimePicker.ValueChanged += delegate { AktualizujModel(dateTimePicker, ustawWartosc, dtp => dtp.Checked ? (DateTime?)dtp.Value : null); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(dateTimePicker, pobierzWartosc, (dtp, wartosc) => { dtp.Checked = wartosc.HasValue; if (wartosc.HasValue) dtp.Value = wartosc.Value; });
	}

	public void Powiazanie(TNumericUpDown numericUpDown, Func<TModel, int> pobierzWartosc, Action<TModel, int>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		Powiazanie(numericUpDown, model => pobierzWartosc(model), (TModel model, decimal wartosc) => ustawWartosc?.Invoke(model, (int)wartosc), wartoscZmieniona);
	}

	public void Powiazanie(TNumericUpDown numericUpDown, Func<TModel, decimal> pobierzWartosc, Action<TModel, decimal>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		numericUpDown.Enter += delegate { numericUpDown.Select(0, numericUpDown.Text.Length); };
		numericUpDown.ValueChanged += delegate { if (ustawWartosc != null) AktualizujModel(numericUpDown, ustawWartosc, nud => nud.Value); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(numericUpDown, pobierzWartosc, (nud, wartosc) => nud.Value = wartosc);
	}

	public void Powiazanie(TTextBox textBox, Func<TModel, string> pobierzWartosc, Action<TModel, string>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		textBox.TextChanged += delegate { AktualizujModel(textBox, ustawWartosc, txt => txt.Text); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(textBox, pobierzWartosc, (txt, wartosc) => txt.Text = wartosc);
	}

	public void Powiazanie(TCheckBox checkBox, Func<TModel, bool> pobierzWartosc, Action<TModel, bool>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		checkBox.CheckedChanged += delegate { AktualizujModel(checkBox, ustawWartosc, chk => chk.Checked); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(checkBox, pobierzWartosc, (chk, wartosc) => chk.Checked = wartosc);
	}

	public void Powiazanie(TSuggestBox suggestBox, Func<TModel, string> pobierzWartosc, Action<TModel, string>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		suggestBox.TextChanged += delegate { AktualizujModel(suggestBox, ustawWartosc, suggestBox => suggestBox.Text); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(suggestBox, pobierzWartosc, (suggestBox, wartosc) => { suggestBox.Text = wartosc; });
	}

	public void Powiazanie(TComboBox comboBox, Func<TModel, int> pobierzWartosc, Action<TModel, int>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		comboBox.SelectedIndexChanged += delegate { AktualizujModel(comboBox, ustawWartosc, comboBox => comboBox.SelectedIndex); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(comboBox, pobierzWartosc, (comboBox, wartosc) => { comboBox.SelectedIndex = wartosc; });
	}

	public void Powiazanie<TWartosc>(TComboBox comboBox, Func<TModel, TWartosc> pobierzWartosc, Action<TModel, TWartosc>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		comboBox.SelectedIndexChanged += delegate { AktualizujModel(comboBox, (model, wartosc) => ustawWartosc?.Invoke(model, wartosc!), comboBox => comboBox.SelectedIndex == -1 ? default : (TWartosc)comboBox.SelectedValue!); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(comboBox, pobierzWartosc, (comboBox, wartosc) => { if (comboBox.SelectedIndex != -1) comboBox.SelectedIndex = -1; if (wartosc != null) comboBox.SelectedValue = wartosc; });
	}

	public void Slownik<T>(TComboBox comboBox, PozycjaListy<T>[] wartosci)
	{
		comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBox.DisplayMember = "Opis";
		comboBox.ValueMember = "Wartosc";
		comboBox.DataSource = wartosci;
	}

	private void DodajPowiazanie<TKontrolka, TWartosc>(TKontrolka kontrolka, Func<TModel, TWartosc> pobierzWartosc, Action<TKontrolka, TWartosc> ustawWartosc)
		where TKontrolka : TControl
	{
		void Powiazanie() => AktualizujKontrolke(kontrolka, pobierzWartosc, ustawWartosc);
		if (model != null) Powiazanie();
		powiazania.Add(Powiazanie);
	}

	private void AktualizujModel<TWartosc, TKontrolka>(TKontrolka kontrolka, Action<TModel, TWartosc>? ustawWartosc, Func<TKontrolka, TWartosc> pobierzWartosc)
		where TKontrolka : TControl
	{
		if (model is null) return;
		if (aktualizowaneKontrolki.Contains(kontrolka)) return;
		aktualizowaneKontrolki.Add(kontrolka);
		try
		{
			var wartosc = pobierzWartosc(kontrolka);
			ustawWartosc?.Invoke(model, wartosc);
			modelZmieniony = true;
		}
		finally
		{
			aktualizowaneKontrolki.Remove(kontrolka);
		}
	}

	private void AktualizujKontrolke<TWartosc, TKontrolka>(TKontrolka kontrolka, Func<TModel, TWartosc> pobierzWartosc, Action<TKontrolka, TWartosc> ustawWartosc)
		where TKontrolka : TControl
	{
		if (model is null) return;
		if (aktualizowaneKontrolki.Contains(kontrolka)) return;
		aktualizowaneKontrolki.Add(kontrolka);
		try
		{
			var wartosc = pobierzWartosc(model);
			ustawWartosc(kontrolka, wartosc);
		}
		finally
		{
			aktualizowaneKontrolki.Remove(kontrolka);
		}
	}
}

#endif
