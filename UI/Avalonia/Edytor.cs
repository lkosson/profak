#if AVALONIA
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;

namespace ProFak.UI;

abstract class Edytor : Panel, IDisposable
{
	protected override Type StyleKeyOverride => typeof(Avalonia.Controls.Panel);
	public virtual bool CzyModelZmieniony => false;
	public virtual bool CzyModelPoprawny => true;
	private TControl? kontrolkaStartowa;

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
		var topLevel = TopLevel.GetTopLevel(this);
		topLevel?.Opened += TopLevel_Opened;
	}

	private void TopLevel_Opened(object? sender, EventArgs e)
	{
		if (sender is not TopLevel topLevel) return;
		kontrolkaStartowa ??= (TControl?)this.GetVisualDescendants().FirstOrDefault(kontrolka => kontrolka is InputElement { Focusable: true });
		kontrolkaStartowa?.Focus(NavigationMethod.Tab);
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
		kontrolkaStartowa = kontrolka;
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