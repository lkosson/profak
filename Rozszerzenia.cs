namespace ProFak
{
	static class Rozszerzenia
	{
		public static string JakoJednaLinia(this string wejscie) => String.Join(", ", (wejscie ?? "").Split('\r', '\n').Where(linia => !String.IsNullOrWhiteSpace(linia)));

		public static (string linia1, string linia2) JakoDwieLinie(this string wejscie)
		{
			if (String.IsNullOrWhiteSpace(wejscie)) return ("", "");
			var sep = wejscie.IndexOfAny(new[] { '\r', '\n' });
			if (sep < 0) return (wejscie, "");
			return (wejscie[..sep], wejscie[sep..].Replace("\r", " ").Replace("\n", " ").Replace("  ", " ").Trim());
		}

		public static decimal Zaokragl(this decimal wartosc, int miejsca = 2) => Decimal.Round(wartosc, miejsca, MidpointRounding.AwayFromZero);
	}
}
