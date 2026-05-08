#if AVALONIA
using Avalonia;
using Avalonia.Input;
using Avalonia.LogicalTree;

namespace ProFak.UI;

partial class SpisZAkcjami<TRekord> : IDisposable
{
	void IDisposable.Dispose()
	{
	}

	protected override void OnGotFocus(FocusChangedEventArgs e)
	{
		base.OnGotFocus(e);
		//Spis.Focus();
	}

	protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
	{
		panelAkcji.CzyGlownySpis = Spis.Kontekst.Dialog == null || Spis.Kontekst.Dialog is not DialogEdycji;
		panelAkcji.UstawUklad([wyszukiwarka], adapteryAkcji, [podsumowanie]);
		base.OnAttachedToLogicalTree(e);
	}

	private void Zamknij()
	{
		if (Spis.Kontekst.Dialog is DialogEdycji) return;
		if (Spis.Kontekst.Dialog is not Dialog dialog) return;
		dialog.Zamknij();
	}
}
#endif
