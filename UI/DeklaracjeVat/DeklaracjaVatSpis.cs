using ProFak.DB;

namespace ProFak.UI;

#pragma warning disable WFO1000 // Missing code serialization configuration for property content
class DeklaracjaVatSpis : Spis<DeklaracjaVat>
{
	public int? Rok { get; set; }

	public override string Podsumowanie
	{
		get
		{
			var podsumowanie = base.Podsumowanie;
			if (WybraneRekordy.Count() > 1)
			{
				podsumowanie += $"\nRazem do wpłaty: <{WybraneRekordy.Sum(deklaracjaVat => deklaracjaVat.DoWplaty).ToString(Wyglad.FormatKwoty)}>";
				podsumowanie += $"\nRazem należny: <{WybraneRekordy.Sum(deklaracjaVat => deklaracjaVat.NaleznyRazem).ToString(Wyglad.FormatKwoty)}>";
				podsumowanie += $"\nRazem naliczony: <{WybraneRekordy.Sum(deklaracjaVat => deklaracjaVat.NaliczonyRazem).ToString(Wyglad.FormatKwoty)}>";
			}
			return podsumowanie;
		}
	}

	public DeklaracjaVatSpis()
	{
		DodajKolumne(nameof(DeklaracjaVat.MiesiacFmt), "Miesiąc");
		DodajKolumneKwota(nameof(DeklaracjaVat.NettoRazem), "Podstawa", format: Wyglad.FormatKwoty);
		DodajKolumneKwota(nameof(DeklaracjaVat.NaleznyRazem), "Należny", format: Wyglad.FormatKwoty);
		DodajKolumneKwota(nameof(DeklaracjaVat.NaliczonyRazem), "Naliczony", format: Wyglad.FormatKwoty);
		DodajKolumneKwota(nameof(DeklaracjaVat.DoWplaty), "Do wpłaty", format: Wyglad.FormatKwoty);
		DodajKolumneId();
	}

	protected override void Przeladuj()
	{
		var q = Kontekst.Baza.DeklaracjeVat;
		if (Rok.HasValue) q = q.Where(deklaracja => deklaracja.Miesiac >= new DateTime(Rok.Value, 1, 1) && deklaracja.Miesiac < new DateTime(Rok.Value + 1, 1, 1));
		Rekordy = q.OrderBy(deklaracja => deklaracja.Miesiac).ToList();
	}
}
