#if WINFORMS

namespace ProFak.UI;

partial class SpisZAkcjami<TRekord>
{
	protected override void OnGotFocus(EventArgs e)
	{
		base.OnGotFocus(e);
		Spis.Focus();
	}

	protected override void OnCreateControl()
	{
		panelAkcji.CzyGlownySpis = Spis.Kontekst.Dialog == null || Spis.Kontekst.Dialog is not DialogEdycji;
		panelAkcji.UstawUklad([wyszukiwarka], adapteryAkcji, [podsumowanie]);
		base.OnCreateControl();
	}

	private void Zamknij()
	{
		Dispose();
	}
}
#endif