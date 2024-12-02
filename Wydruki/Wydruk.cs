using ProFak.DB;
using System.IO;
using System.Reflection;

namespace ProFak.Wydruki
{
	abstract class Wydruk
	{
		public abstract void Przygotuj(Microsoft.Reporting.WinForms.LocalReport report);

		protected Stream WczytajSzablon(string nazwa)
		{
			var lokalnyPlik = Path.Combine(Baza.LokalnyKatalog, nazwa + ".rdlc");
			if (File.Exists(lokalnyPlik)) return File.OpenRead(lokalnyPlik);
			var asm = Assembly.GetCallingAssembly();
			return asm.GetManifestResourceStream("ProFak.Wydruki." + nazwa + ".rdlc");
		}
	}
}
