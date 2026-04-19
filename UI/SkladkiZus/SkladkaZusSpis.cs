using ProFak.DB;

namespace ProFak.UI;

#pragma warning disable WFO1000 // Missing code serialization configuration for property content
class SkladkaZusSpis : Spis<SkladkaZus>
{
	public int? Rok { get; set; }

	public override string Podsumowanie
	{
		get
		{
			var podsumowanie = base.Podsumowanie;
			if (WybraneRekordy.Count() > 1)
			{
				podsumowanie += $"\nRazem: <{WybraneRekordy.Sum(skladka => skladka.SumaSkladek).ToString(Wyglad.FormatKwoty)}>";
				podsumowanie += $"\nRazem społeczne: <{WybraneRekordy.Sum(skladka => skladka.SkladkaSpoleczna).ToString(Wyglad.FormatKwoty)}>";
				podsumowanie += $"\nRazem zdrowotne: <{WybraneRekordy.Sum(skladka => skladka.SkladkaZdrowotna).ToString(Wyglad.FormatKwoty)}>";
			}
			return podsumowanie;
		}
	}

	public SkladkaZusSpis()
	{
		DodajKolumne(nameof(SkladkaZus.MiesiacFmt), "Miesiąc");
		DodajKolumneKwota(nameof(SkladkaZus.SkladkaSpoleczna), "Składka społeczna", szerokosc: 150);
		DodajKolumneKwota(nameof(SkladkaZus.SkladkaZdrowotna), "Składka zdrowotna", szerokosc: 150);
		DodajKolumneKwota(nameof(SkladkaZus.SumaSkladek), "Razem", szerokosc: 150);
		DodajKolumneId();
	}

	protected override void Przeladuj()
	{
		var q = Kontekst.Baza.SkladkiZus;
		if (Rok.HasValue) q = q.Where(skladka => skladka.Miesiac >= new DateTime(Rok.Value, 1, 1) && skladka.Miesiac < new DateTime(Rok.Value + 1, 1, 1));
		Rekordy = q.OrderBy(skladka => skladka.Miesiac).ToList();
	}
}
