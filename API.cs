using ProFak.DB;

namespace ProFak;

public class API(string baza) : IDisposable
{
	public Baza Baza { get; private set; } = new Baza(baza);

	public Transakcja Transakcja() => new Transakcja(Baza);

	public Kontrahent MojaFirma() => Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);

	public Faktura PrzygotujFakture(RodzajFaktury rodzaj, Kontrahent kontrahent, List<(Ref<Towar> towar, decimal ilosc)>? specyfikacja = null)
	{
		var faktura = new Faktura();
		faktura.Rodzaj = rodzaj;
		faktura.DataWprowadzenia = faktura.DataWystawienia = faktura.DataSprzedazy = DateTime.Now;
		if (faktura.CzySprzedaz)
		{
			faktura.UstawNabywce(kontrahent);
			faktura.UstawSprzedawce(MojaFirma());
		}
		else
		{
			faktura.UstawNabywce(MojaFirma());
			faktura.UstawSprzedawce(kontrahent);
		}

		if (kontrahent.SposobPlatnosciRef.IsNull)
		{
			var sposobPlatnosci = Baza.SposobyPlatnosci.FirstOrDefault(sposobPlatnosci => sposobPlatnosci.CzyDomyslny);
			if (sposobPlatnosci != null) faktura.UstawSposobPlatnosci(sposobPlatnosci);
		}
		else
		{
			var sposobPlatnosci = Baza.Znajdz(kontrahent.SposobPlatnosciRef);
			faktura.UstawSposobPlatnosci(sposobPlatnosci);
		}
		faktura.NadajNumer(Baza);

		Baza.Zapisz(faktura);

		if (specyfikacja != null)
		{
			var lp = 1;
			foreach (var (towarRef, ilosc) in specyfikacja)
			{
				var towar = Baza.Znajdz(towarRef);
				var pozycja = new PozycjaFaktury();
				pozycja.FakturaRef = faktura;
				pozycja.LP = lp;
				pozycja.UstawTowar(towar);
				pozycja.Ilosc = ilosc;
				pozycja.PrzeliczCeny(Baza);
				Baza.Zapisz(pozycja);
				lp++;
			}
		}

		faktura.PrzeliczRazem(Baza);
		Baza.Zapisz(faktura);

		return faktura;
	}

	public void Dispose() => Baza.Dispose();
}
