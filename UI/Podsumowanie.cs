using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace ProFak.UI;

class Podsumowanie : LinkLabel
{
	public Podsumowanie()
	{
		AutoSize = true;
		LinkBehavior = LinkBehavior.HoverUnderline;
		Links.Clear();
	}

	[AllowNull]
	public override string Text
	{
		get => base.Text;
		set
		{
			var odnosniki = new List<(int poczatek, string wartosc)>();
			if (value != null)
			{
				var sb = new StringBuilder(value.Length);
				int? poczatek = null;
				for (var i = 0; i < value.Length; i++)
				{
					var ch = value[i];
					if (ch == '<')
					{
						poczatek = i + 1;
					}
					else if (ch == '>' && poczatek is not null)
					{
						var odnosnik = value[poczatek.Value..i];
						odnosniki.Add((sb.Length, odnosnik));
						sb.Append(odnosnik);
						poczatek = null;
					}
					else if (poczatek is null)
					{
						sb.Append(ch);
					}
				}
				base.Text = sb.ToString();
			}
			else
			{
				base.Text = "";
			}
			Links.Clear();
			foreach (var odnosnik in odnosniki)
				Links.Add(odnosnik.poczatek, odnosnik.wartosc.Length, odnosnik.wartosc);
		}
	}

	protected override void OnLinkClicked(LinkLabelLinkClickedEventArgs e)
	{
		if (e.Link?.LinkData is not string tekst) return;
		if (Regex.IsMatch(tekst, @"[0-9,\s]+")) tekst = tekst.Replace("\u00A0", "");
		Clipboard.SetText(tekst);
	}
}
