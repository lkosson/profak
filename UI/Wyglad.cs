using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class Wyglad
	{
		private static Regex nazwaPlusSkrot = new Regex(@"(?<nazwa>[^[]+)(\[(?<skrot>.+)\])?");

		public static bool SkrotyKlawiaturoweAkcji { get; set; } = true;
		public static bool SkrotyKlawiaturoweZakladek { get; set; } = true;
		public static bool SkrotyKlawiaturowePrzyciskow { get; set; } = true;
		public static bool IkonyAkcji { get; set; } = true;
		public static bool DomyslnyPodgladStrony { get; set; } = true;
		public static bool PotwierdzanieZamknieciaEdytora { get; set; } = true;
		public static int SzerokoscMenu { get; set; } = 270;

		public static string NazwaAkcji(AdapterAkcji adapter)
		{
			var nazwa = SkrotyKlawiaturoweAkcji ? adapter.Nazwa : adapter.NazwaBezSkrotu;
			if (!IkonyAkcji) nazwa = NazwaBezIkony(nazwa);
			return nazwa;
		}

		public static string NazwaBezIkony(string nazwa)
		{
			var idx = 0;
			while (idx < nazwa.Length && (!Char.IsLetterOrDigit(nazwa[idx]) || Char.IsWhiteSpace(nazwa[idx]))) idx++;
			return nazwa.Substring(idx);
		}

		public static void UsunSkrotyZakladek(TabControl tabControl)
		{
			if (SkrotyKlawiaturoweZakladek) return;
			foreach (TabPage zakladka in tabControl.TabPages)
			{
				zakladka.Text = NazwaBezSkrotu(zakladka.Text).Trim();
			}
		}

		public static void UsunIkonePrzycisku(Button przycisk)
		{
			if (IkonyAkcji) return;
			przycisk.Text = NazwaBezIkony(przycisk.Text);
		}

		public static void UsunSkrotPrzycisku(Button przycisk)
		{
			if (SkrotyKlawiaturowePrzyciskow) return;
			przycisk.Text = NazwaBezSkrotu(przycisk.Text);
		}

		public static string NazwaBezSkrotu(string tekst)
		{
			var dopasowanie = nazwaPlusSkrot.Match(tekst);
			if (!dopasowanie.Success) return tekst;
			return dopasowanie.Groups["nazwa"].Value;
		}

		public static string Skrot(string tekst)
		{
			var dopasowanie = nazwaPlusSkrot.Match(tekst);
			if (!dopasowanie.Groups["skrot"].Success) return "";
			return dopasowanie.Groups["skrot"].Value;
		}

		public static void DostosujDoWine()
		{
			var ntdll = NativeLibrary.Load("ntdll.dll");
			if (NativeLibrary.TryGetExport(ntdll, "wine_get_version", out _))
			{
				SkrotyKlawiaturoweZakladek = false;
				IkonyAkcji = false;
				DomyslnyPodgladStrony = false;
			}
			NativeLibrary.Free(ntdll);
		}
	}
}
