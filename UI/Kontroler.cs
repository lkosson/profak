using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class Kontroler<TModel>
	{
		private TModel model;
		private readonly HashSet<Control> aktualizowaneKontrolki;
		private List<Action> powiazania;

		public TModel Model { get => model; set { model = value; AktualizujKontrolki(); } }

		public Kontroler()
		{
			aktualizowaneKontrolki = new HashSet<Control>();
			powiazania = new List<Action>();
		}

		public void Powiazanie(NumericUpDown numericUpDown, Expression<Func<TModel, decimal>> wlasciwosc, Action wartoscZmieniona = null) => Powiazanie(numericUpDown, wlasciwosc, Powiazanie, wartoscZmieniona);
		public void Powiazanie(TextBox textBox, Expression<Func<TModel, string>> wlasciwosc, Action wartoscZmieniona = null) => Powiazanie(textBox, wlasciwosc, Powiazanie, wartoscZmieniona);
		public void Powiazanie(CheckBox checkBox, Expression<Func<TModel, bool>> wlasciwosc, Action wartoscZmieniona = null) => Powiazanie(checkBox, wlasciwosc, Powiazanie, wartoscZmieniona);
		public void Powiazanie(ComboBox comboBox, Expression<Func<TModel, string>> wlasciwosc, Action wartoscZmieniona = null) => Powiazanie(comboBox, wlasciwosc, Powiazanie, wartoscZmieniona);

		private void Powiazanie<TKontrolka, TWartosc>(TKontrolka kontrolka, Expression<Func<TModel, TWartosc>> wlasciwosc, Action<TKontrolka, Func<TModel, TWartosc>, Action<TModel, TWartosc>, Action> powiazanie, Action wartoscZmieniona = null)
		{
			var exp = (MemberExpression)wlasciwosc.Body;
			var pi = (PropertyInfo)exp.Member;
			var getter = (Func<TModel, TWartosc>)pi.GetGetMethod().CreateDelegate(typeof(Func<TModel, TWartosc>));
			var setter = (Action<TModel, TWartosc>)pi.GetSetMethod().CreateDelegate(typeof(Action<TModel, TWartosc>));
			powiazanie(kontrolka, getter, setter, wartoscZmieniona);
		}

		public void Powiazanie(NumericUpDown numericUpDown, Func<TModel, decimal> pobierzWartosc, Action<TModel, decimal> ustawWartosc, Action wartoscZmieniona = null)
		{
			numericUpDown.ValueChanged += delegate { AktualizujModel(numericUpDown, ustawWartosc, nud => nud.Value); if (wartoscZmieniona != null) wartoscZmieniona(); };
			Action powiazanie = delegate { AktualizujKontrolke(numericUpDown, pobierzWartosc, (nud, wartosc) => nud.Value = wartosc); };
			if (model != null) powiazanie();
			powiazania.Add(powiazanie);
		}

		public void Powiazanie(TextBox textBox, Func<TModel, string> pobierzWartosc, Action<TModel, string> ustawWartosc, Action wartoscZmieniona = null)
		{
			textBox.TextChanged += delegate { AktualizujModel(textBox, ustawWartosc, txt => txt.Text); if (wartoscZmieniona != null) wartoscZmieniona(); };
			Action powiazanie = delegate { AktualizujKontrolke(textBox, pobierzWartosc, (txt, wartosc) => txt.Text = wartosc); };
			if (model != null) powiazanie();
			powiazania.Add(powiazanie);
		}

		public void Powiazanie(CheckBox checkBox, Func<TModel, bool> pobierzWartosc, Action<TModel, bool> ustawWartosc, Action wartoscZmieniona = null)
		{
			checkBox.CheckedChanged += delegate { AktualizujModel(checkBox, ustawWartosc, chk => chk.Checked); if (wartoscZmieniona != null) wartoscZmieniona(); };
			Action powiazanie = delegate { AktualizujKontrolke(checkBox, pobierzWartosc, (chk, wartosc) => chk.Checked = wartosc); };
			if (model != null) powiazanie();
			powiazania.Add(powiazanie);
		}

		public void Powiazanie(ComboBox comboBox, Func<TModel, string> pobierzWartosc, Action<TModel, string> ustawWartosc, Action wartoscZmieniona = null)
		{
			comboBox.TextChanged += delegate { AktualizujModel(comboBox, ustawWartosc, comboBox => comboBox.Text); if (wartoscZmieniona != null) wartoscZmieniona(); };
			Action powiazanie = delegate { AktualizujKontrolke(comboBox, pobierzWartosc, (comboBox, wartosc) => { if (comboBox.SelectedIndex != -1) comboBox.SelectedIndex = -1; comboBox.Text = wartosc; }); };
			if (model != null) powiazanie();
			powiazania.Add(powiazanie);
		}

		private void AktualizujModel<TWartosc, TKontrolka>(TKontrolka kontrolka, Action<TModel, TWartosc> ustawWartosc, Func<TKontrolka, TWartosc> pobierzWartosc)
			where TKontrolka : Control
		{
			if (model is null) return;
			if (aktualizowaneKontrolki.Contains(kontrolka)) return;
			aktualizowaneKontrolki.Add(kontrolka);
			try
			{
				var wartosc = pobierzWartosc(kontrolka);
				ustawWartosc(model, wartosc);
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
	}
}
