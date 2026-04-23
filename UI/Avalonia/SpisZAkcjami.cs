#if AVALONIA
using Avalonia;
using Avalonia.Input;

namespace ProFak.UI;

partial class SpisZAkcjami<TRekord>
{
	protected override void OnGotFocus(FocusChangedEventArgs e)
	{
		base.OnGotFocus(e);
		Spis.Focus();
	}

	protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
	{
		panelAkcji.CzyGlownySpis = Spis.Kontekst.Dialog == null || Spis.Kontekst.Dialog is not DialogEdycji;
		panelAkcji.UstawUklad([wyszukiwarka], adapteryAkcji, [podsumowanie]);
		base.OnAttachedToVisualTree(e);
	}

	private void Zamknij()
	{
		((Avalonia.Controls.Panel?)Parent)?.Children.Remove(this);
	}
}
#endif
