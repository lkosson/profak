#if WINFORMS
namespace ProFak.UI;

partial class Wyszukiwarka<TRekord> : TextBox
{
	public Wyszukiwarka(Spis<TRekord> spis)
	{
		this.spis = spis;
		var opis = "";
		if (Wyglad.IkonyAkcji) opis += "🔍 ";
		opis += "Wyszukaj";
		if (Wyglad.SkrotyKlawiaturoweAkcji) opis += " [F3]";
		PlaceholderText = opis;
		TextAlign = HorizontalAlignment.Center;
	}

	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		UstawFiltr(Text);
		TextAlign = String.IsNullOrEmpty(Text) ? HorizontalAlignment.Center : HorizontalAlignment.Left;
	}

	protected override void OnKeyDown(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Escape)
		{
			Text = "";
			spis.Focus();
			e.Handled = true;
		}
		else if (e.KeyCode == Keys.Enter)
		{
			if (spis.Rekordy.Any()) spis.Focus();
			e.Handled = true;
		}
	}
}
#endif