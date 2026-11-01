using Microsoft.EntityFrameworkCore;
using ProFak.DB;

namespace ProFak.UI;

class PozycjaFakturySpis : Spis<PozycjaFaktury>
{
	public Ref<Faktura> FakturaRef { get; set; }

	public override string Podsumowanie
	{
		get
		{
			var podsumowanie = base.Podsumowanie;
			if (WybraneRekordy.Count() > 1)
			{
				podsumowanie += $"\nRazem netto: <{WybraneRekordy.Sum(pozycja => pozycja.WartoscNettoAbs).ToString(Wyglad.FormatKwoty)}>";
				podsumowanie += $"\nRazem VAT: <{WybraneRekordy.Sum(pozycja => pozycja.WartoscVatAbs).ToString(Wyglad.FormatKwoty)}>";
				podsumowanie += $"\nRazem brutto: <{WybraneRekordy.Sum(pozycja => pozycja.WartoscBruttoAbs).ToString(Wyglad.FormatKwoty)}>";
			}
			return podsumowanie;
		}
	}

	public PozycjaFakturySpis()
	{
		DodajKolumne(nameof(PozycjaFaktury.LP), "LP", szerokosc: 30);
		DodajKolumne(nameof(PozycjaFaktury.Opis), "Opis", rozciagnij: true);
		DodajKolumne(nameof(PozycjaFaktury.IloscAbs), "Ilość", wyrownajDoPrawej: true, szerokosc: 50);
		DodajKolumne(nameof(PozycjaFaktury.JednostkaMiaryFmt), "JM", szerokosc: 50);
		DodajKolumneKwota(nameof(PozycjaFaktury.Cena), "Cena", szerokosc: 70);
		DodajKolumneKwota(nameof(PozycjaFaktury.WartoscNettoAbs), "Netto", szerokosc: 70);
		DodajKolumneKwota(nameof(PozycjaFaktury.WartoscVatAbs), "VAT", szerokosc: 70);
		DodajKolumneKwota(nameof(PozycjaFaktury.WartoscBruttoAbs), "Brutto", szerokosc: 70);
		DodajKolumneKwota(nameof(PozycjaFaktury.RabatFmt), "Rabat", szerokosc: 70);
		DodajKolumneId();
	}

	protected override void Przeladuj()
	{
		IQueryable<PozycjaFaktury> q = Kontekst.Baza.PozycjeFaktur;
		if (FakturaRef.IsNotNull) q = q.Where(pozycja => pozycja.FakturaId == FakturaRef.Id);
		q = q.Include(pozycja => pozycja.JednostkaMiary);
		q = q.OrderBy(pozycja => pozycja.LP).ThenByDescending(pozycja => pozycja.CzyPrzedKorekta).ThenBy(pozycja => pozycja.Id);
		Rekordy = q.ToList();
	}

	protected override TColor KolorWiersza(PozycjaFaktury rekord) => rekord.CzyPrzedKorekta ? Kontrolki.Color(210, 210, 210) : base.KolorWiersza(rekord);
}
