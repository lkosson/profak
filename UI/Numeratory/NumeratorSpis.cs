using ProFak.DB;

namespace ProFak.UI;

class NumeratorSpis : Spis<Numerator>
{
	public NumeratorSpis()
	{
		DodajKolumne(nameof(Numerator.PrzeznaczenieFmt), "Przeznaczenie", rozciagnij: true);
		DodajKolumne(nameof(Numerator.Format), "Format");
		DodajKolumneId();
	}

	protected override void Przeladuj()
	{
		Rekordy = Kontekst.Baza.Numeratory.OrderBy(numerator => numerator.Przeznaczenie).ThenBy(numerator => numerator.Id).ToList();
	}
}
