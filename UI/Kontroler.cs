using System.Linq.Expressions;
using System.Reflection;

namespace ProFak.UI;

class Kontroler<TModel>
{
	private readonly HashSet<Control> aktualizowaneKontrolki;
	private TModel model = default!;
	private readonly List<Action> powiazania;
	private bool modelZmieniony;

	public TModel Model { get => model; set { model = value; AktualizujKontrolki(); } }
	public bool CzyModelZmieniony => modelZmieniony;

	public Kontroler()
	{
		aktualizowaneKontrolki = [];
		powiazania = [];
	}

	public void Powiazanie(DateTimePicker dateTimePicker, Expression<Func<TModel, DateTime>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(dateTimePicker, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(DateTimePicker dateTimePicker, Expression<Func<TModel, DateTime?>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(dateTimePicker, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(NumericUpDown numericUpDown, Expression<Func<TModel, decimal>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(numericUpDown, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(NumericUpDown numericUpDown, Expression<Func<TModel, int>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(numericUpDown, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(TextBox textBox, Expression<Func<TModel, string>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(textBox, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(CheckBox checkBox, Expression<Func<TModel, bool>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(checkBox, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(ComboBox comboBox, Expression<Func<TModel, string>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(comboBox, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(ComboBox comboBox, Expression<Func<TModel, int>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(comboBox, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie<TWartosc>(ComboBox comboBox, Expression<Func<TModel, TWartosc>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(comboBox, wlasciwosc, Powiazanie, wartoscZmieniona);

	private void Powiazanie<TKontrolka, TWartosc>(TKontrolka kontrolka, Expression<Func<TModel, TWartosc>> wlasciwosc, Action<TKontrolka, Func<TModel, TWartosc>, Action<TModel, TWartosc>?, Action?> powiazanie, Action? wartoscZmieniona = null)
	{
		var exp = (MemberExpression)wlasciwosc.Body;
		var pi = (PropertyInfo)exp.Member;
		var getterMI = pi.GetGetMethod() ?? throw new ArgumentException($"Nieprawidłowa właściwość {wlasciwosc} dowiązana do {kontrolka}.");
		var getter = getterMI.CreateDelegate<Func<TModel, TWartosc>>();
		var setter = pi.GetSetMethod()?.CreateDelegate<Action<TModel, TWartosc>>();
		powiazanie(kontrolka, getter, setter, wartoscZmieniona);
	}

	private void DodajPowiazanie<TKontrolka, TWartosc>(TKontrolka kontrolka, Func<TModel, TWartosc> pobierzWartosc, Action<TKontrolka, TWartosc> ustawWartosc)
		where TKontrolka : Control
	{
		void Powiazanie() => AktualizujKontrolke(kontrolka, pobierzWartosc, ustawWartosc);
		if (model != null) Powiazanie();
		powiazania.Add(Powiazanie);
	}

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
		DodajPowiazanie(comboBox, pobierzWartosc, (comboBox, wartosc) => { if (comboBox.SelectedIndex != -1) comboBox.SelectedIndex = -1; comboBox.Text = wartosc; });
	}

	public void Powiazanie(ComboBox comboBox, Func<TModel, int> pobierzWartosc, Action<TModel, int>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		comboBox.TextChanged += delegate { AktualizujModel(comboBox, ustawWartosc, comboBox => comboBox.SelectedIndex); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(comboBox, pobierzWartosc, (comboBox, wartosc) => { comboBox.SelectedIndex = wartosc; });
	}

	public void Powiazanie<TWartosc>(ComboBox comboBox, Func<TModel, TWartosc> pobierzWartosc, Action<TModel, TWartosc>? ustawWartosc, Action? wartoscZmieniona = null)
	{
		comboBox.SelectedIndexChanged += delegate { AktualizujModel(comboBox, (model, wartosc) => ustawWartosc?.Invoke(model, wartosc!), comboBox => comboBox.SelectedIndex == -1 ? default : (TWartosc)comboBox.SelectedValue!); wartoscZmieniona?.Invoke(); };
		DodajPowiazanie(comboBox, pobierzWartosc, (comboBox, wartosc) => { if (comboBox.SelectedIndex != -1) comboBox.SelectedIndex = -1; if (wartosc != null) comboBox.SelectedValue = wartosc; });
	}

	private void AktualizujModel<TWartosc, TKontrolka>(TKontrolka kontrolka, Action<TModel, TWartosc>? ustawWartosc, Func<TKontrolka, TWartosc> pobierzWartosc)
		where TKontrolka : Control
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
		where TKontrolka : Control
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

	public void AktualizujKontrolki()
	{
		foreach (var powiazanie in powiazania) powiazanie();
	}

	public void Slownik<TEnum>(ComboBox comboBox, bool dopuscPuste = false) where TEnum : struct, Enum
	{
		comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBox.DisplayMember = "Opis";
		comboBox.ValueMember = "Wartosc";
		var pozycje = new List<PozycjaListy<TEnum?>>();
		if (dopuscPuste) pozycje.Add(new PozycjaListy<TEnum?> { Opis = "" });
		foreach (TEnum wartosc in Enum.GetValues(typeof(TEnum)))
		{
			pozycje.Add(new PozycjaListy<TEnum?> { Wartosc = wartosc, Opis = DB.Rekord.Format(wartosc) });
		}
		comboBox.DataSource = pozycje.ToArray();
	}

	public void Slownik(ComboBox comboBox, string opisTrue, string opisFalse)
	{
		comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBox.DisplayMember = "Opis";
		comboBox.ValueMember = "Wartosc";
		comboBox.DataSource = new[] { new PozycjaListy<bool> { Wartosc = false, Opis = opisFalse }, new PozycjaListy<bool> { Wartosc = true, Opis = opisTrue } };
	}

	public void Slownik<T>(ComboBox comboBox, params T[] wartosci)
	{
		comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBox.DisplayMember = "Opis";
		comboBox.ValueMember = "Wartosc";
		comboBox.DataSource = wartosci.Select(wartosc => new PozycjaListy<T> { Wartosc = wartosc, Opis = wartosc?.ToString() ?? "" }).ToArray();
	}
}
