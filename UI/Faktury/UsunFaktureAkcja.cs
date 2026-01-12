using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class UsunFaktureAkcja : UsunRekordAkcja<Faktura>
	{
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => base.CzyKlawiszSkrotu(klawisz, modyfikatory) || (klawisz == Keys.Delete && modyfikatory == Keys.Shift);
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => base.CzyDostepnaDlaRekordow(zaznaczoneRekordy) && !zaznaczoneRekordy.Any(faktura => faktura.FakturaKorygujacaRef.IsNotNull);

		protected override void Usun(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
		{
			var numeryFaktur = kontekst.Baza.Faktury.Where(e => e.Rodzaj != RodzajFaktury.Zakup && e.Rodzaj != RodzajFaktury.KorektaZakupu).Select(e => e.Numer).ToHashSet();
			var usuwaneFaktury = zaznaczoneRekordy.Select(e => e.Numer).ToHashSet();

			foreach (var faktura in zaznaczoneRekordy)
			{
				var przeznaczenie = faktura.Numerator;
				if (przeznaczenie == null) continue;
				var numerator = kontekst.Baza.Numeratory.FirstOrDefault(numerator => numerator.Przeznaczenie == przeznaczenie.Value);
				if (numerator == null) continue;
				var szablon = Numerator.PrzygotujWzorzec(numerator.Format, faktura.Podstawienie);
				var parametry = String.Format(szablon, "");
				var stanNumeratora = kontekst.Baza.StanyNumeratorow.FirstOrDefault(stan => stan.NumeratorId == numerator.Id && stan.Parametry == parametry);
				if (stanNumeratora == null) continue;
				var cofniety = false;
				while (stanNumeratora.OstatniaWartosc > 0)
				{
					var testowanyNumer = String.Format(szablon, stanNumeratora.OstatniaWartosc);
					if (numeryFaktur.Contains(testowanyNumer) && !usuwaneFaktury.Contains(testowanyNumer)) break;
					stanNumeratora.OstatniaWartosc--;
					cofniety = true;
				}
				if (cofniety) kontekst.Baza.Zapisz(stanNumeratora);
			}

			foreach (var faktura in zaznaczoneRekordy)
			{
				if (faktura.FakturaKorygowanaRef.IsNotNull)
				{
					var fakturaKorygowana = kontekst.Baza.Znajdz(faktura.FakturaKorygowanaRef);
					fakturaKorygowana.FakturaKorygujacaRef = default;
					kontekst.Baza.Zapisz(fakturaKorygowana);
				}

				if (GlowneOkno.ModifierKeys == Keys.Shift)
				{
					kontekst.Baza.Usun(faktura);
				}
				else
				{
					faktura.DeklaracjaVatRef = default;
					faktura.ZaliczkaPitRef = default;
					faktura.Rodzaj = RodzajFaktury.Usunięta;
					faktura.DataUsuniecia = DateTime.Now;
					faktura.Numer += " (USUNIĘTA)";
					kontekst.Baza.Zapisz(faktura);
				}
			}
		}
	}
}
