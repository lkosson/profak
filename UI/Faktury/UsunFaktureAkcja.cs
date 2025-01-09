using Microsoft.EntityFrameworkCore;
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
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => base.CzyDostepnaDlaRekordow(zaznaczoneRekordy) && !zaznaczoneRekordy.Any(faktura => faktura.FakturaKorygujacaRef.IsNotNull);

		protected override void Usun(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
		{
			base.Usun(kontekst, zaznaczoneRekordy);

			var numeryFaktur = kontekst.Baza.Faktury.Select(e => e.Numer).ToHashSet();

			foreach (var faktura in zaznaczoneRekordy)
			{
				var numerator = kontekst.Baza.Numeratory.FirstOrDefault(numerator => numerator.Przeznaczenie == faktura.Numerator);
				if (numerator == null) continue;
				var szablon = Numerator.PrzygotujWzorzec(numerator.Format, faktura.Podstawienie);
				var parametry = String.Format(szablon, "");
				var stanNumeratora = kontekst.Baza.StanyNumeratorow.FirstOrDefault(stan => stan.NumeratorId == numerator.Id && stan.Parametry == parametry);
				if (stanNumeratora == null) continue;
				var cofniety = false;
				while (stanNumeratora.OstatniaWartosc > 0)
				{
					var testowanyNumer = String.Format(szablon, stanNumeratora.OstatniaWartosc);
					if (numeryFaktur.Contains(testowanyNumer)) break;
					stanNumeratora.OstatniaWartosc--;
					cofniety = true;
				}
				if (cofniety) kontekst.Baza.Zapisz(stanNumeratora);
			}
		}
	}
}
