using ProFak.DB;

namespace ProFak.UI;

class ZaliczkaPitSpis : Spis<ZaliczkaPit>
{
	public int? Rok { get; set; }

	public override string Podsumowanie
	{
		get
		{
			var podsumowanie = base.Podsumowanie;
			if (WybraneRekordy.Count() > 1)
			{
				podsumowanie += $"\nRazem podatek: <{WybraneRekordy.Sum(zaliczka => zaliczka.Podatek).ToString(Wyglad.FormatKwoty)}>";
				podsumowanie += $"\nRazem przychody: <{WybraneRekordy.Sum(zaliczka => zaliczka.Przychody).ToString(Wyglad.FormatKwoty)}>";
				podsumowanie += $"\nRazem koszty: <{WybraneRekordy.Sum(zaliczka => zaliczka.Koszty).ToString(Wyglad.FormatKwoty)}>";
			}
			return podsumowanie;
		}
	}

	public ZaliczkaPitSpis()
	{
		DodajKolumne(nameof(ZaliczkaPit.MiesiacFmt), "Miesiąc");
		DodajKolumneKwota(nameof(ZaliczkaPit.Przychody), "Przychody");
		DodajKolumneKwota(nameof(ZaliczkaPit.Koszty), "Koszty");
		DodajKolumneKwota(nameof(ZaliczkaPit.DoWplaty), "Do wpłaty");
		DodajKolumneId();
	}

	protected override void Przeladuj()
	{
		var q = Kontekst.Baza.ZaliczkiPit;
		if (Rok.HasValue) q = q.Where(zaliczka => zaliczka.Miesiac >= new DateTime(Rok.Value, 1, 1) && zaliczka.Miesiac < new DateTime(Rok.Value + 1, 1, 1));
		Rekordy = q.OrderBy(zaliczka => zaliczka.Miesiac).ToList();
	}
}
