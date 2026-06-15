using ProFak.DB;

namespace ProFak.UI;

class RachunekKontrahentaEdytor : Edytor<RachunekKontrahenta>
{
	private readonly TTextBox textBoxNumerRachunku;
	private readonly TTextBox textBoxNazwaBanku;
	private readonly TComboBox comboBoxWaluta;
	private readonly TButton buttonWaluta;

	public RachunekKontrahentaEdytor()
	{
		textBoxNumerRachunku = Kontrolki.TextBox();
		textBoxNazwaBanku = Kontrolki.TextBox();
		comboBoxWaluta = Kontrolki.DropDownList();
		buttonWaluta = Kontrolki.ButtonSlownik();

		kontroler.Powiazanie(textBoxNumerRachunku, rachunekKontrahenta => rachunekKontrahenta.NumerRachunku);
		kontroler.Powiazanie(textBoxNazwaBanku, rachunekKontrahenta => rachunekKontrahenta.NazwaBanku);
		kontroler.Powiazanie(comboBoxWaluta, rachunekKontrahenta => rachunekKontrahenta.WalutaRef);

		Wymagane(textBoxNumerRachunku);

		var siatka = new Siatka([0, -1, 0], []);
		siatka.DodajWiersz("Numer rachunku", [(textBoxNumerRachunku, 2)]);
		siatka.DodajWiersz("Nazwa banku", [(textBoxNazwaBanku, 2)]);
		siatka.DodajWiersz("Waluta", [comboBoxWaluta, buttonWaluta]);

		UstawZawartosc(siatka);
	}

	protected override void KontekstGotowy()
	{
		base.KontekstGotowy();

		new Slownik<Waluta>(
			Kontekst, comboBoxWaluta, buttonWaluta,
			Kontekst.Baza.Waluty.ToList,
			waluta => waluta.Nazwa,
			waluta => { },
			Spisy.Waluty,
			dopuscPustaWartosc: true)
			.Zainstaluj();
	}
}
