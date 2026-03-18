using System.Globalization;
using System.Text.RegularExpressions;

namespace ProFak.DB;

public class Numerator : Rekord<Numerator>
{
	public PrzeznaczenieNumeratora Przeznaczenie { get; set; } = PrzeznaczenieNumeratora.Faktura;
	public string Format { get; set; } = "[Numer]";
	public string? Grupa { get; set; }

	public string PrzeznaczenieFmt => Format(Przeznaczenie);

	public List<StanNumeratora> Stany { get; set; } = default!;

	public override bool CzyPasuje(string fraza)
		=> base.CzyPasuje(fraza)
		|| CzyPasuje(Przeznaczenie, fraza)
		|| CzyPasuje(Format, fraza)
		|| CzyPasuje(Grupa, fraza);

	public static string NadajNumer(Baza baza, PrzeznaczenieNumeratora przeznaczenie, Func<string, IFormattable?> podstawienie, bool zwiekszLicznik = true)
	{
		baza.Zablokuj<Numerator>();
		var numerator = baza.Numeratory.FirstOrDefault(numerator => numerator.Przeznaczenie == przeznaczenie);
		if (numerator == null) throw new ApplicationException($"Brak definicji numeratora \"{przeznaczenie}\" - dodaj pozycję w spisie \"Serwisowe\" - \"Numeracja\".");
		return numerator.NadajNumer(baza, podstawienie, zwiekszLicznik);
	}

	public string GenerujGrupe(Func<string, IFormattable?> podstawienie) => Podstaw(String.IsNullOrEmpty(Grupa) ? Format : Grupa, podstawienie, default);
	public string GenerujNumer(Func<string, IFormattable?> podstawienie, int licznik) => Podstaw(Format, podstawienie, licznik);

	public string NadajNumer(Baza baza, Func<string, IFormattable?> podstawienie, bool zwiekszLicznik = true)
	{
		var parametry = GenerujGrupe(podstawienie);

		var stanNumeratora = baza.StanyNumeratorow.FirstOrDefault(stan => stan.NumeratorId == Id && stan.Parametry == parametry);
		if (stanNumeratora == null) stanNumeratora = new StanNumeratora { NumeratorRef = this, Parametry = parametry, OstatniaWartosc = 0 };

		var licznik = stanNumeratora.OstatniaWartosc + 1;
		var numer = GenerujNumer(podstawienie, licznik);
		if (zwiekszLicznik)
		{
			stanNumeratora.OstatniaWartosc = licznik;
			baza.Zapisz(stanNumeratora);
		}

		return numer;
	}

	private static string Podstaw(string format, Func<string, IFormattable?> podstawienie, int? numer)
	{
		return Regex.Replace(format, @"\[(?<nazwa>\w+)(:(?<format>[^\]]+))?\]", fragment =>
		{
			var nazwa = fragment.Groups["nazwa"].Value;
			var format = fragment.Groups["format"]?.Value;
			if (String.Equals(nazwa, "numer", StringComparison.CurrentCultureIgnoreCase)) return numer is null ? "" : numer.Value.ToString(format);
			var wartosc = podstawienie(nazwa);
			if (wartosc == null) throw new ApplicationException($"Nieznane wyrażenie numeratora \"{nazwa}\".");
			var tekst = String.IsNullOrWhiteSpace(format) ? wartosc.ToString() ?? "" : wartosc.ToString(format, CultureInfo.CurrentCulture);
			return tekst;
		});
	}
}

public enum PrzeznaczenieNumeratora
{
	Faktura,
	Proforma,
	KorektaSprzedaży,
	DowódWewnętrzny,
	VatMarża,
	KorektaVatMarży,
	Rachunek,
	KorektaRachunku
}
