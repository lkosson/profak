using ProFak.DB;
using System.ComponentModel;

namespace ProFak.UI;

class WplataSpis : Spis<Wplata>
{
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Ref<Faktura> FakturaRef { get; set; }

	public WplataSpis()
	{
		DodajKolumne(nameof(Wplata.Data), "Data");
		DodajKolumneKwota(nameof(Wplata.Kwota), "Kwota");
		DodajKolumne(nameof(Wplata.Uwagi), "Uwagi");
		DodajKolumneId();
	}

	protected override void Przeladuj()
	{
		IQueryable<Wplata> q = Kontekst.Baza.Wplaty;
		if (FakturaRef.IsNotNull) q = q.Where(wplata => wplata.FakturaId == FakturaRef.Id);
		q = q.OrderBy(wplata => wplata.Data).ThenBy(wplata => wplata.Id);
		Rekordy = q.ToList();
	}
}
