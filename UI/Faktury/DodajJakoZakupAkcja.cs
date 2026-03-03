using ProFak.DB;

namespace ProFak.UI;

class DodajJakoZakupAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
{
	public override string Nazwa => "➕ Dodaj jako zakup [INS]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;// && zaznaczoneRekordy.Single().Id == 0;
	public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.None && klawisz == Keys.Insert;
	public override bool PrzeladujPoZakonczeniu => false;

	protected override Faktura UtworzRekord(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
	{
		var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
		var naglowek = zaznaczoneRekordy.Single();
		if (String.IsNullOrEmpty(naglowek.XMLKSeF))
		{
			OknoPostepu.Uruchom(async cancellationToken =>
			{
				using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
				await api.UwierzytelnijAsync(podmiot.NIP, podmiot.TokenKSeF, cancellationToken);
				naglowek.XMLKSeF = await api.PobierzFaktureAsync(naglowek.NumerKSeF, cancellationToken);
				cancellationToken.ThrowIfCancellationRequested();
			});
		}
		var faktura = IO.FA_3.Generator.ZbudujDB(kontekst.Baza, naglowek.XMLKSeF);
		faktura.NumerKSeF = naglowek.NumerKSeF;
		faktura.DataKSeF = naglowek.DataKSeF;
		using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
		faktura.URLKSeF = api.ZbudujUrl(naglowek.XMLKSeF, faktura.NIPSprzedawcy, faktura.DataWystawienia);
		kontekst.Baza.Zapisz(faktura);
		IO.FA_3.Generator.PoprawPowiazaniaPoZapisie(kontekst.Baza, faktura);
		return faktura;
	}
}
