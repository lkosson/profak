#if WINFORMS
using System.Linq.Expressions;
using System.Reflection;

namespace ProFak.UI;

partial class Kontroler<TModel>
{
	public void Powiazanie(DateTimePicker dateTimePicker, Func<TModel, DateTime> pobierzWartosc, Action<TModel, DateTime>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		dateTimePicker.ValueChanged += delegate { AktualizujModel(dateTimePicker, ustawWartosc, dtp => dtp.Value); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(dateTimePicker, pobierzWartosc, (dtp, wartosc) => dtp.Value = wartosc);
	}

	public void Powiazanie(DateTimePicker dateTimePicker, Func<TModel, DateTime?> pobierzWartosc, Action<TModel, DateTime?>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		dateTimePicker.ValueChanged += delegate { AktualizujModel(dateTimePicker, ustawWartosc, dtp => dtp.Checked ? (DateTime?)dtp.Value : null); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(dateTimePicker, pobierzWartosc, (dtp, wartosc) => { dtp.Checked = wartosc.HasValue; if (wartosc.HasValue) dtp.Value = wartosc.Value; });
	}

	public void Powiazanie(NumericUpDown numericUpDown, Func<TModel, int> pobierzWartosc, Action<TModel, int>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		Powiazanie(numericUpDown, model => pobierzWartosc(model), (TModel model, decimal wartosc) => ustawWartosc?.Invoke(model, (int)wartosc), wartoscZmieniona);
	}

	public void Powiazanie(NumericUpDown numericUpDown, Func<TModel, decimal> pobierzWartosc, Action<TModel, decimal>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		numericUpDown.Enter += delegate { numericUpDown.Select(0, numericUpDown.Text.Length); };
		numericUpDown.ValueChanged += delegate { if (ustawWartosc != null) AktualizujModel(numericUpDown, ustawWartosc, nud => nud.Value); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(numericUpDown, pobierzWartosc, (nud, wartosc) => nud.Value = wartosc);
	}

	public void Powiazanie(TextBox textBox, Func<TModel, string> pobierzWartosc, Action<TModel, string>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		textBox.TextChanged += delegate { AktualizujModel(textBox, ustawWartosc, txt => txt.Text); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(textBox, pobierzWartosc, (txt, wartosc) => txt.Text = wartosc);
	}

	public void Powiazanie(CheckBox checkBox, Func<TModel, bool> pobierzWartosc, Action<TModel, bool>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		checkBox.CheckedChanged += delegate { AktualizujModel(checkBox, ustawWartosc, chk => chk.Checked); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(checkBox, pobierzWartosc, (chk, wartosc) => chk.Checked = wartosc);
	}

	public void Powiazanie(ComboBox comboBox, Func<TModel, string> pobierzWartosc, Action<TModel, string>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		comboBox.TextChanged += delegate { AktualizujModel(comboBox, ustawWartosc, comboBox => comboBox.Text); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(comboBox, pobierzWartosc, (comboBox, wartosc) => { comboBox.Text = wartosc; });
	}

	public void Powiazanie(ComboBox comboBox, Func<TModel, int> pobierzWartosc, Action<TModel, int>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		comboBox.SelectedIndexChanged += delegate { AktualizujModel(comboBox, ustawWartosc, comboBox => comboBox.SelectedIndex); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(comboBox, pobierzWartosc, (comboBox, wartosc) => { comboBox.SelectedIndex = wartosc; });
	}

	public void Powiazanie<TWartosc>(ComboBox comboBox, Func<TModel, TWartosc> pobierzWartosc, Action<TModel, TWartosc>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		comboBox.SelectedIndexChanged += delegate { AktualizujModel(comboBox, (model, wartosc) => ustawWartosc?.Invoke(model, wartosc!), comboBox => comboBox.SelectedIndex == -1 ? default : (TWartosc)comboBox.SelectedValue!); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(comboBox, pobierzWartosc, (comboBox, wartosc) => { if (comboBox.SelectedIndex != -1) comboBox.SelectedIndex = -1; if (wartosc != null) comboBox.SelectedValue = wartosc; });
	}

	private void Slownik<T>(ComboBox comboBox, PozycjaListy<T>[] wartosci)
	{
		comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBox.DisplayMember = "Opis";
		comboBox.ValueMember = "Wartosc";
		comboBox.DataSource = wartosci;
	}
}

#endif
