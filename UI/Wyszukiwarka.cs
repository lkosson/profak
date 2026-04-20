using ProFak.DB;
using System.Text.RegularExpressions;

namespace ProFak.UI;

class Wyszukiwarka<TRekord> : TextBox
	where TRekord : Rekord<TRekord>
{
	private Spis<TRekord> spis;

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

	private void UstawFiltr(string wyrazenieFiltra)
	{
		if (String.IsNullOrWhiteSpace(wyrazenieFiltra))
		{
			spis.UstawFiltr(rekord => true);
		}
		else
		{
			var fragmenty = Regex.Matches(wyrazenieFiltra, @"(?:[^\s""]+|""[^""]*"")+");
			List<Func<TRekord, bool>> dopasowania = new List<Func<TRekord, bool>>();
			foreach (Match fragment in fragmenty)
			{
				if (!fragment.Success) continue;
				var fraza = fragment.Value;
				Func<TRekord, bool> dopasowanieFragmentu = rekord => rekord.CzyPasuje(fraza);
				dopasowania.Add(dopasowanieFragmentu);
			}
			spis.UstawFiltr((Func<TRekord, bool>)Delegate.Combine(dopasowania.ToArray())!);
		}
	}
}
