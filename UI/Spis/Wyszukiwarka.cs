using ProFak.DB;
using System.Text.RegularExpressions;

namespace ProFak.UI;

partial class Wyszukiwarka<TRekord>
	where TRekord : Rekord<TRekord>
{
	private Spis<TRekord> spis;
	private string Opis => (Wyglad.IkonyAkcji ? "🔍 " : "") + "Wyszukaj" + (Wyglad.SkrotyKlawiaturoweAkcji ? " [F3]" : "");

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
				if (fraza.StartsWith('!')) dopasowania.Add(rekord => !rekord.CzyPasuje(fraza[1..]));
				else dopasowania.Add(rekord => rekord.CzyPasuje(fraza));
			}
			spis.UstawFiltr(rekord => dopasowania.All(f => f(rekord)));
		}
	}
}
