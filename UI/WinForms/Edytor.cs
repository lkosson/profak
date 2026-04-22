#if WINFORMS
using System.ComponentModel;

namespace ProFak.UI;

abstract class Edytor : UserControl
{
	private readonly Container container;
	protected readonly ErrorProvider errorProvider;
	protected readonly ToolTip toolTip;
	public virtual bool CzyModelZmieniony => false;

	public Edytor()
	{
		container = new Container();
		errorProvider = new ErrorProvider(container);
		toolTip = new ToolTip(container);
	}

	protected override void OnParentChanged(EventArgs e)
	{
		base.OnParentChanged(e);
		if (ParentForm != null) ParentForm.FormClosing += ParentForm_FormClosing;
	}

	private void ParentForm_FormClosing(object? sender, FormClosingEventArgs e)
	{
		if (ParentForm != null
			&& ParentForm.DialogResult == DialogResult.Cancel
			&& e.CloseReason != CloseReason.WindowsShutDown
			&& e.CloseReason != CloseReason.TaskManagerClosing
			/* nie wystarczy sprawdzenie CloseReason.UserClosing - nie działa dla ESC */
			&& CzyModelZmieniony
			&& Wyglad.PotwierdzanieZamknieciaEdytora)
		{
			e.Cancel = !OknoKomunikatu.PytanieTakNie("Czy na pewno chcesz porzucić wprowadzone zmiany?", domyslnie: false);
		}
	}

	protected void UstawZawartosc(TControl zawartosc, Size wymiary = default)
	{
		SuspendLayout();
		if (wymiary == default) wymiary = zawartosc.Size;
		zawartosc.Dock = DockStyle.Fill;
		Controls.Add(zawartosc);
		MinimumSize = Size = wymiary;
		ResumeLayout(true);
	}

	public void Wymagane(TTextBox textBox)
	{
		textBox.Validating += TextBox_Wymagane_Validating;
	}

	public void Wymagane(TComboBox comboBox)
	{
		comboBox.Validating += ComboBox_Wymagane_Validating;
	}

	public void Walidacja(TTextBox textBox, Func<string, string?> walidator, bool miekki)
	{
		textBox.Validating += (control, e) =>
		{
			if (ModifierKeys == Keys.Shift) return;
			var blad = walidator(textBox.Text);

			if (blad == null)
			{
				errorProvider.SetError(textBox, "");
			}
			else
			{
				errorProvider.SetIconAlignment(textBox, ErrorIconAlignment.MiddleLeft);
				errorProvider.SetError(textBox, blad);
				if (!miekki) e.Cancel = true;
			}
		};
	}

	public void Walidacja<T>(TComboBox comboBox, Func<T, string?> walidator, bool miekki)
	{
		comboBox.Validating += (control, e) =>
		{
			if (ModifierKeys == Keys.Shift) return;
			var wartosc = comboBox.SelectedValue;
			var blad = wartosc is T t ? walidator(t) : "Wybrana wartość jest nieprawidłowa.";

			if (blad == null)
			{
				errorProvider.SetError(comboBox, "");
			}
			else
			{
				errorProvider.SetIconAlignment(comboBox, ErrorIconAlignment.MiddleLeft);
				errorProvider.SetError(comboBox, blad);
				if (!miekki) e.Cancel = true;
			}
		};
	}

	protected void Dymek(TControl kontrolka, string tresc)
	{
		toolTip.SetToolTip(kontrolka, tresc);
	}

	private void TextBox_Wymagane_Validating(object? sender, CancelEventArgs e)
	{
		if (ModifierKeys == Keys.Shift) return;
		if (sender is not TextBox textBox) return;
		if (String.IsNullOrEmpty(textBox.Text))
		{
			errorProvider.SetIconAlignment(textBox, ErrorIconAlignment.MiddleLeft);
			errorProvider.SetError(textBox, "Należy uzupełnić pole.");
			e.Cancel = true;
		}
		else
		{
			errorProvider.SetError(textBox, "");
		}
	}

	private void ComboBox_Wymagane_Validating(object? sender, CancelEventArgs e)
	{
		if (ModifierKeys == Keys.Shift) return;
		if (sender is not ComboBox comboBox) return;
		if ((comboBox.DropDownStyle == ComboBoxStyle.DropDownList && comboBox.SelectedIndex == -1)
			|| (comboBox.DropDownStyle != ComboBoxStyle.DropDownList) && String.IsNullOrEmpty(comboBox.Text))
		{
			errorProvider.SetIconAlignment(comboBox, ErrorIconAlignment.MiddleLeft);
			errorProvider.SetError(comboBox, "Należy uzupełnić pole.");
			e.Cancel = true;
		}
		else
		{
			errorProvider.SetError(comboBox, "");
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing) container.Dispose();
		base.Dispose(disposing);
	}
}
#endif