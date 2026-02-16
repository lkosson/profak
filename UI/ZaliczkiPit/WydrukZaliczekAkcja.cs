using ProFak.DB;

namespace ProFak.UI;

class WydrukZaliczekAkcja : AkcjaNaSpisie<ZaliczkaPit>
{
	public override string Nazwa => "🖶 Drukuj [CTRL-P]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<ZaliczkaPit> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;
	public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => klawisz == Keys.P && modyfikatory == Keys.Control;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<ZaliczkaPit> zaznaczoneRekordy)
	{
		var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
		Wydruki.Wydruk wydruk;
		if (podmiot.FormaOpodatkowania == FormaOpodatkowania.Ryczałt) wydruk = new Wydruki.EwidencjaPrzychodow(kontekst.Baza, zaznaczoneRekordy.Single());
		else wydruk = new Wydruki.PKPiR(kontekst.Baza, zaznaczoneRekordy.Single());
		using var okno = new OknoWydruku(wydruk);
		okno.ShowDialog();
	}
}
