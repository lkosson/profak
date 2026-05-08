#if AVALONIA
using Avalonia.Input;

namespace ProFak.UI;

partial class Wyszukiwarka<TRekord> : TTextBox
{
	protected override Type StyleKeyOverride => typeof(TTextBox);

	public Wyszukiwarka(Spis<TRekord> spis)
	{
		this.spis = spis;
		PlaceholderText = Opis;
		TextAlignment = Avalonia.Media.TextAlignment.Center;
		Margin = new TPadding(3, 3);
		TextChanged += Wyszukiwarka_TextChanged;
	}

	protected override void OnGotFocus(FocusChangedEventArgs e)
	{
		TextAlignment = Avalonia.Media.TextAlignment.Left;
		PlaceholderText = "";
		base.OnGotFocus(e);
	}

	protected override void OnLostFocus(FocusChangedEventArgs e)
	{
		TextAlignment = Avalonia.Media.TextAlignment.Center;
		PlaceholderText = Opis;
		base.OnLostFocus(e);
	}

	private void Wyszukiwarka_TextChanged(object? sender, Avalonia.Controls.TextChangedEventArgs e)
	{
		UstawFiltr(Text ?? "");
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