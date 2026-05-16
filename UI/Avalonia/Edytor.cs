#if AVALONIA
using Avalonia.Controls;

namespace ProFak.UI;

abstract class Edytor : Avalonia.Controls.Panel, IDisposable
{
	protected override Type StyleKeyOverride => typeof(Avalonia.Controls.Panel);
	public virtual bool CzyModelZmieniony => false;
	public virtual bool CzyModelPoprawny => true;
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

	protected void UstawZawartosc(TControl zawartosc)
	{
		Children.Add(zawartosc);
	}

	protected void KontrolkaStartowa(TControl kontrolka)
	{
		kontrolka.AttachedToVisualTree += delegate 
		{
			var topLevel = TopLevel.GetTopLevel(this);
			topLevel?.Opened += delegate
			{
				kontrolka.Focus(Avalonia.Input.NavigationMethod.Tab);
			};
		};
	}

	public void Wymagane(TControl kontrolka)
	{
		if (kontrolka.DataContext is not PowiazanaWartosc wartosc) return;
		wartosc.DodajWalidator(wartosc => wartosc is null or "" ? "Należy uzupełnić pole." : null, false);
	}

	public void Walidacja(TTextBox textBox, Func<string?, string?> walidator, bool miekki)
	{
		if (textBox.DataContext is not PowiazanaWartosc<string> wartosc) return;
		wartosc.DodajWalidator(walidator, miekki);
	}

	public void Walidacja<T>(TComboBox comboBox, Func<T?, string?> walidator, bool miekki)
	{
		if (comboBox.DataContext is not PowiazanaWartosc<T> wartosc) return;
		wartosc.DodajWalidator(walidator, miekki);
	}

	protected void Dymek(TControl kontrolka, string tresc)
	{
		kontrolka.SetValue(Avalonia.Controls.ToolTip.TipProperty, tresc);
	}
}
#endif