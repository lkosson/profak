#if AVALONIA
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Input.Platform;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace ProFak.UI;

partial class Podsumowanie : TText
{
	protected override Type StyleKeyOverride => typeof(TText);

	public Podsumowanie()
	{
	}

	public string Text
	{
		set
		{
			Inlines?.Clear();
			if (value is null) return;
			var sb = new StringBuilder(value.Length);
			var poczatek = 0;
			for (var i = 0; i <= value.Length; i++)
			{
				var ch = i == value.Length ? '\0' : value[i];
				if (ch == '<' || ch == '\0')
				{
					if (poczatek != i - 1)
					{
						Inlines?.Add(value[poczatek..i]);
					}
					poczatek = i + 1;
				}
				else if (ch == '>')
				{
					var odnosnik = value[poczatek..i];
					var link = new HyperlinkButton();
					link.Padding = new Avalonia.Thickness(0);
					link.Content = odnosnik;
					link.Click += delegate { TopLevel.GetTopLevel(this)?.Clipboard?.SetTextAsync(odnosnik); };
					var container = new InlineUIContainer(link) { BaselineAlignment = Avalonia.Media.BaselineAlignment.Bottom };
					Inlines?.Add(container);
					poczatek = i + 1;
				}
			}
		}
	}
}
#endif
