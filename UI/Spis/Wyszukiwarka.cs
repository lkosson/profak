using ProFak.DB;
using System.Text.RegularExpressions;

namespace ProFak.UI;

partial class Wyszukiwarka<TRekord>
	where TRekord : Rekord<TRekord>
{
	private Spis<TRekord> spis;

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
