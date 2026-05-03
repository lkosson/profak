#if AVALONIA
using Avalonia.Controls;

namespace ProFak.UI;

abstract class Edytor : Avalonia.Controls.Panel, IDisposable
{
	public virtual bool CzyModelZmieniony => false;
	protected int DeviceDpi => 96;

	public Edytor()
	{
	}

	void IDisposable.Dispose()
	{
	}

	protected void Zamknij()
	{
		var topLevel = TopLevel.GetTopLevel(this);
		if (topLevel is Dialog dialog) dialog.Zamknij();
	}

	protected override void OnAttachedToVisualTree(Avalonia.VisualTreeAttachmentEventArgs e)
	{
		base.OnAttachedToVisualTree(e);
		EdytorGotowy();
	}

	protected virtual void EdytorGotowy()
	{
		var topLevel = TopLevel.GetTopLevel(this);
		if (topLevel is DialogEdycji dialog) dialog.Closing += Dialog_Closing;
	}

	private void Dialog_Closing(object? sender, WindowClosingEventArgs e)
	{
		if (sender is DialogEdycji dialog
			&& !dialog.Wynik
			&& e.CloseReason != WindowCloseReason.OSShutdown
			&& e.CloseReason != WindowCloseReason.ApplicationShutdown
			&& CzyModelZmieniony
			&& Wyglad.PotwierdzanieZamknieciaEdytora)
		{
			e.Cancel = !OknoKomunikatu.PytanieTakNie("Czy na pewno chcesz porzucić wprowadzone zmiany?", domyslnie: false);
		}
	}

	protected void UstawZawartosc(TControl zawartosc, Size wymiary = default)
	{
		Children.Add(zawartosc);
		if (wymiary != default)
		{
			MinWidth = Width = wymiary.Width;
			MinHeight = Height = wymiary.Height;
		}
	}

	protected void KontrolkaStartowa(TControl kontrolka)
	{
		//TODO Avalonia
		//ActiveControl = kontrolka;
	}

	public void Wymagane(TTextBox textBox)
	{
		//TODO Avalonia
		//textBox.Validating += TextBox_Wymagane_Validating;
	}

	public void Wymagane(TComboBox comboBox)
	{
		//TODO Avalonia
		//comboBox.Validating += ComboBox_Wymagane_Validating;
	}

	public void Wymagane(TSuggestBox suggestBox)
	{
		//TODO Avalonia
	}

	public void Walidacja(TTextBox textBox, Func<string, string?> walidator, bool miekki)
	{
		//TODO Avalonia
		/*
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
		*/
	}

	public void Walidacja<T>(TComboBox comboBox, Func<T, string?> walidator, bool miekki)
	{
		//TODO Avalonia
		/*
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
		*/
	}

	protected void Dymek(TControl kontrolka, string tresc)
	{
		//TODO Avalonia
		//toolTip.SetToolTip(kontrolka, tresc);
	}
	/*
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
	*/
}
#endif