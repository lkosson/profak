﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProFak.DB
{
	class Numerator : Rekord<Numerator>
	{
		public PrzeznaczenieNumeratora Przeznaczenie { get; set; } = PrzeznaczenieNumeratora.Faktura;
		public string Format { get; set; } = "[Numer]";

		public string PrzeznaczenieFmt => Przeznaczenie.ToString();

		public List<StanNumeratora> Stany { get; set; }

		public static string NadajNumer(Baza baza, PrzeznaczenieNumeratora przeznaczenie, Func<string, IFormattable> podstawienie, bool zwiekszLicznik = true)
		{
			var numerator = baza.Numeratory.FirstOrDefault(numerator => numerator.Przeznaczenie == przeznaczenie);
			if (numerator == null) throw new ApplicationException($"Brak definicji numeratora \"{przeznaczenie}\".");
			var regexFragment = new Regex(@"\[(?<nazwa>\w+)(:(?<format>[^\]]))?\]");
			var szablon = regexFragment.Replace(numerator.Format, fragment =>
			{
				var nazwa = fragment.Groups["nazwa"]?.Value;
				var format = fragment.Groups["format"]?.Value;
				if (String.Equals(nazwa, "numer", StringComparison.CurrentCultureIgnoreCase)) return "{0" + (String.IsNullOrEmpty(format) ? "" : ":" + format) + "}";
				var wartosc = podstawienie(nazwa);
				if (wartosc == null) throw new ApplicationException($"Nieznane wyrażenie numeratora \"{nazwa}\".");
				var tekst = String.IsNullOrWhiteSpace(format) ? wartosc.ToString() : wartosc.ToString(format, CultureInfo.CurrentCulture);
				return tekst;
			});
			var parametry = String.Format(szablon, "");

			var stanNumeratora = baza.StanyNumeratorow.FirstOrDefault(stan => stan.NumeratorId == numerator.Id && stan.Parametry == parametry);
			if (stanNumeratora == null)
			{
				stanNumeratora = new StanNumeratora { NumeratorRef = numerator, Parametry = parametry, OstatniaWartosc = 0 };
				baza.StanyNumeratorow.Add(stanNumeratora);
			}

			var licznik = stanNumeratora.OstatniaWartosc + 1;
			var numer = String.Format(szablon, licznik);
			if (zwiekszLicznik)
			{
				stanNumeratora.OstatniaWartosc = licznik;
				baza.Zapisz();
			}

			return numer;
		}
	}

	enum PrzeznaczenieNumeratora
	{
		Faktura,
		Proforma
	}
}
