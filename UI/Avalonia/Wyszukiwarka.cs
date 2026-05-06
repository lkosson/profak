#if AVALONIA
using Avalonia.Input;

namespace ProFak.UI;

partial class Wyszukiwarka<TRekord> : TTextBox
{
	protected override Type StyleKeyOverride => typeof(TTextBox);

	public Wyszukiwarka(Spis<TRekord> spis)
	{
		this.spis = spis;
		var opis = "";
		if (Wyglad.IkonyAkcji) opis += "🔍 ";
		opis += "Wyszukaj";
		if (Wyglad.SkrotyKlawiaturoweAkcji) opis += " [F3]";
		PlaceholderText = opis;
		TextAlignment = Avalonia.Media.TextAlignment.Center;
		Margin = new TPadding(3, 3);
		TextChanged += Wyszukiwarka_TextChanged;
	}

	private void Wyszukiwarka_TextChanged(object? sender, Avalonia.Controls.TextChangedEventArgs e)
	{
		UstawFiltr(Text ?? "");
		TextAlignment = String.IsNullOrEmpty(Text) ? Avalonia.Media.TextAlignment.Center : Avalonia.Media.TextAlignment.Left;
	}

	protected override void OnKeyDown(Avalonia.Input.KeyEventArgs e)
	{
		if (e.Key == Key.Escape)
		{
			Text = "";
			spis.Focus();
			e.Handled = true;
		}
		else if (e.Key == Key.Enter)
		{
			if (spis.Rekordy.Any()) spis.Focus();
			e.Handled = true;
		}
		base.OnKeyDown(e);
	}
}
#endif