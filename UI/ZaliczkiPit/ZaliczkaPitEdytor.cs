using ProFak.DB;

namespace ProFak.UI;

partial class ZaliczkaPitEdytor : ZaliczkaPitEdytorBase
{
	private readonly SpisZAkcjami<Faktura, FakturaSprzedazySpis> fakturySprzedazy;
	private readonly SpisZAkcjami<Faktura, FakturaZakupuSpis> fakturyZakupu;

	public ZaliczkaPitEdytor()
	{
		InitializeComponent();

		kontroler.Powiazanie(dateTimePickerMiesiac, deklaracja => deklaracja.Miesiac);
		kontroler.Powiazanie(numericUpDownPrzychody, deklaracja => deklaracja.Przychody);
		kontroler.Powiazanie(numericUpDownKoszty, deklaracja => deklaracja.Koszty);
		kontroler.Powiazanie(numericUpDownSkladkiZus, deklaracja => deklaracja.SkladkiZus);
		kontroler.Powiazanie(numericUpDownPodatek, deklaracja => deklaracja.Podatek);
		kontroler.Powiazanie(numericUpDownPrzeniesiony, deklaracja => deklaracja.Przeniesiony);
		kontroler.Powiazanie(numericUpDownDoPrzeniesienia, deklaracja => deklaracja.DoPrzeniesienia);
		kontroler.Powiazanie(numericUpDownDoWplaty, deklaracja => deklaracja.DoWplaty);

		var dodajSprzedazDoZaliczki = new DynamicznaAkcja<Faktura>("➕ Dodaj do zaliczki [INS]", kontekst =>
		{
			using var spis = new SpisZAkcjami<Faktura, FakturaSprzedazySpis>(new FakturaSprzedazySpis { CzyBezZaliczkiPit = true });
			var faktura = Spisy.Wybierz(kontekst, spis, "Wybierz fakturę", default);
			if (faktura == null) return;
			faktura.ZaliczkaPitRef = Rekord;
			kontekst.Baza.Zapisz(faktura);
			Przelicz();
		}, Keys.Insert, Keys.None);

		var dodajZakupDoZaliczki = new DynamicznaAkcja<Faktura>("➕ Dodaj do zaliczki [INS]", kontekst =>
		{
			using var spis = new SpisZAkcjami<Faktura, FakturaZakupuSpis>(new FakturaZakupuSpis { CzyBezZaliczkiPit = true });
			var faktura = Spisy.Wybierz(kontekst, spis, "Wybierz fakturę", default);
			if (faktura == null) return;
			faktura.ZaliczkaPitRef = Rekord;
			kontekst.Baza.Zapisz(faktura);
			Przelicz();
		}, Keys.Insert, Keys.None);


		var usunZZaliczki = new DynamicznaAkcja<Faktura>("❌ Usuń z zaliczki [DEL]", (kontekst, rekordy) =>
		{
			foreach (var rekord in rekordy)
			{
				rekord.ZaliczkaPitRef = default;
			}
			kontekst.Baza.Zapisz(rekordy);
			Przelicz();
		}, Keys.Delete, Keys.None);

		fakturySprzedazy = new SpisZAkcjami<Faktura, FakturaSprzedazySpis>(new FakturaSprzedazySpis(), new AkcjaNaSpisie<Faktura>[] { dodajSprzedazDoZaliczki, new EdytujRekordAkcja<Faktura, FakturaEdytor>(), usunZZaliczki, new WydrukFakturyAkcja(), new PrzeladujAkcja<Faktura>()});
		fakturyZakupu = new SpisZAkcjami<Faktura, FakturaZakupuSpis>(new FakturaZakupuSpis(), new AkcjaNaSpisie<Faktura>[] { dodajZakupDoZaliczki, new EdytujRekordAkcja<Faktura, FakturaEdytor>(), usunZZaliczki, new PrzeladujAkcja<Faktura>()});

		tabPageFakturySprzedazy.Controls.Add(fakturySprzedazy);
		tabPageFakturyZakupu.Controls.Add(fakturyZakupu);
	}

	protected override void RekordGotowy()
	{
		base.RekordGotowy();

		fakturySprzedazy.Spis.ZaliczkaPitRef = Rekord;
		fakturySprzedazy.Spis.Kontekst = Kontekst;
		fakturyZakupu.Spis.ZaliczkaPitRef = Rekord;
		fakturyZakupu.Spis.Kontekst = Kontekst;
	}

	private void buttonPrzelicz_Click(object? sender, EventArgs e)
	{
		try
		{
			WybierzFaktury();
			Przelicz();
		}
		catch (Exception exc)
		{
			OknoBledu.Pokaz(exc);
		}
	}

	private void WybierzFaktury()
	{
		Rekord.WybierzFaktury(Kontekst.Baza);
		fakturySprzedazy.Spis.PrzeladujBezpiecznie();
		fakturyZakupu.Spis.PrzeladujBezpiecznie();
	}

	private void Przelicz()
	{
		Rekord.Przelicz(Kontekst.Baza);
		kontroler.AktualizujKontrolki();
	}
}

class ZaliczkaPitEdytorBase : Edytor<ZaliczkaPit>
{
}
