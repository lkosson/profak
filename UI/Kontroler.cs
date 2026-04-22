using System.Linq.Expressions;
using System.Reflection;

namespace ProFak.UI;

partial class Kontroler<TModel>
{
	private readonly HashSet<TControl> aktualizowaneKontrolki;
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

	public void Powiazanie(TDatePicker dateTimePicker, Expression<Func<TModel, DateTime>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(dateTimePicker, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(TDatePicker dateTimePicker, Expression<Func<TModel, DateTime?>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(dateTimePicker, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(TNumericUpDown numericUpDown, Expression<Func<TModel, decimal>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(numericUpDown, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(TNumericUpDown numericUpDown, Expression<Func<TModel, int>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(numericUpDown, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(TTextBox textBox, Expression<Func<TModel, string>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(textBox, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(TCheckBox checkBox, Expression<Func<TModel, bool>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(checkBox, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(TComboBox comboBox, Expression<Func<TModel, string>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(comboBox, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie(TComboBox comboBox, Expression<Func<TModel, int>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(comboBox, wlasciwosc, Powiazanie, wartoscZmieniona);
	public void Powiazanie<TWartosc>(TComboBox comboBox, Expression<Func<TModel, TWartosc>> wlasciwosc, Action? wartoscZmieniona = null) => Powiazanie(comboBox, wlasciwosc, Powiazanie, wartoscZmieniona);

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

	public void AktualizujKontrolki()
	{
		foreach (var powiazanie in powiazania) powiazanie();
	}

	public void Slownik<TEnum>(TComboBox comboBox, bool dopuscPuste = false) where TEnum : struct, Enum
	{
		var pozycje = new List<PozycjaListy<TEnum?>>();
		if (dopuscPuste) pozycje.Add(new PozycjaListy<TEnum?> { Opis = "" });
		foreach (TEnum wartosc in Enum.GetValues(typeof(TEnum)))
		{
			pozycje.Add(new PozycjaListy<TEnum?> { Wartosc = wartosc, Opis = DB.Rekord.Format(wartosc) });
		}
		Slownik(comboBox, pozycje.ToArray());
	}

	public void Slownik(TComboBox comboBox, string opisTrue, string opisFalse)
	{
		Slownik(comboBox, [new PozycjaListy<bool> { Wartosc = false, Opis = opisFalse }, new PozycjaListy<bool> { Wartosc = true, Opis = opisTrue }]);
	}

	public void Slownik<T>(TComboBox comboBox, params T[] wartosci)
	{
		Slownik(comboBox, wartosci.Select(wartosc => new PozycjaListy<T> { Wartosc = wartosc, Opis = wartosc?.ToString() ?? "" }).ToArray());
	}
}
