using ProFak.DB;

namespace ProFak.UI;

class UsunFaktureAkcja : UsunRekordAkcja<Faktura>
{
	private bool usunNaStale;

	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory)
	{
		usunNaStale = modyfikatory == TKeyModifiers.Shift;
		return base.CzyKlawiszSkrotu(klawisz, modyfikatory) || (klawisz == TKeys.Delete && modyfikatory == TKeyModifiers.Shift);
	}

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
			var parametry = numerator.GenerujGrupe(faktura.Podstawienie);
			var stanNumeratora = kontekst.Baza.StanyNumeratorow.FirstOrDefault(stan => stan.NumeratorId == numerator.Id && stan.Parametry == parametry);
			if (stanNumeratora == null) continue;
			var cofniety = false;
			if (String.IsNullOrEmpty(numerator.Grupa) || numerator.Grupa == numerator.Format)
			{
				while (stanNumeratora.OstatniaWartosc > 0)
				{
					var testowanyNumer = numerator.GenerujNumer(faktura.Podstawienie, stanNumeratora.OstatniaWartosc);
					if (numeryFaktur.Contains(testowanyNumer) && !usuwaneFaktury.Contains(testowanyNumer)) break;
					stanNumeratora.OstatniaWartosc--;
					cofniety = true;
				}
			}
			else
			{
				var testowanyNumer = numerator.GenerujNumer(faktura.Podstawienie, stanNumeratora.OstatniaWartosc);
				if (usuwaneFaktury.Contains(testowanyNumer))
				{
					stanNumeratora.OstatniaWartosc--;
					cofniety = true;
				}
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

			if (usunNaStale)
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
