using ProFak.DB;
using System.ComponentModel;

namespace ProFak.UI;

class DodatkowyPodmiotSpis : Spis<DodatkowyPodmiot>
{
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Ref<Faktura> FakturaRef { get; set; }

	public DodatkowyPodmiotSpis()
	{
		DodajKolumne(nameof(DodatkowyPodmiot.RodzajFmt), "Rodzaj");
		DodajKolumne(nameof(DodatkowyPodmiot.NumerFmt), "Numer");
		DodajKolumne(nameof(DodatkowyPodmiot.Nazwa), "Nazwa", rozciagnij: true);
		DodajKolumneId();
	}

	protected override void Przeladuj()
	{
		IQueryable<DodatkowyPodmiot> q = Kontekst.Baza.DodatkowePodmioty;
		if (FakturaRef.IsNotNull) q = q.Where(wplata => wplata.FakturaId == FakturaRef.Id);
		q = q.OrderBy(wplata => wplata.Id);
		Rekordy = q.ToList();
	}
}
