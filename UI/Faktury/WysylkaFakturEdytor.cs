using ProFak.DB;
using System.Data;
using System.Net;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ProFak.UI;

class WysylkaFakturEdytor : Edytor
{
	public IEnumerable<Faktura> Faktury { get; set; } = [];
	public Kontekst Kontekst { get; set; } = default!;

	private readonly TTextBox textBoxTresc;
	private readonly TTextBox textBoxTemat;
	private readonly TTextBox textBoxAdresat;
	private readonly TComboBox comboBoxFaktura;
	private readonly TButton buttonPoprzednia;
	private readonly TButton buttonNastepna;
	private readonly TButton buttonWyslij;
	private readonly TCheckBox checkBoxUstawDate;
	private readonly TCheckBox checkBoxPrzeliczTermin;

	private string szablonAdresat = "";
	private string szablonTemat = "";
	private string szablonTresc = "";
	private string szablonNadawca = "";

	public WysylkaFakturEdytor()
	{
		textBoxTresc = Kontrolki.TextArea(linie: 15, zmienionaWartosc: ZmienionaTresc);
		textBoxTemat = Kontrolki.TextBox(zmienionaWartosc: ZmienionyTemat);
		textBoxAdresat = Kontrolki.TextBox(zmienionaWartosc: ZmienionyAdresat);
		comboBoxFaktura = Kontrolki.DropDownList(zmienionaWartosc: ZmienionaFaktura);
		buttonPoprzednia = Kontrolki.Button("« Poprzednia", Poprzednia);
		buttonNastepna = Kontrolki.Button("» Następna", Nastepna);
		buttonWyslij = Kontrolki.Button("✉ Wyślij", Wyslij);
		checkBoxUstawDate = Kontrolki.CheckBox("Ustaw datę wystawienia na bieżący dzień", zmienionaWartosc: ZmienionaData);
		checkBoxPrzeliczTermin = Kontrolki.CheckBox("i przelicz termin płatności");

		checkBoxPrzeliczTermin.Enabled = false;
		var uklad = new Siatka([0, -1, 0, 0, 0], [0, 0, 0, 0, -1]);
		uklad.DodajWiersz("Faktura", [comboBoxFaktura, buttonPoprzednia, buttonNastepna, buttonWyslij]);
		uklad.DodajWiersz("Adresat", [(textBoxAdresat, 4)]);
		uklad.DodajWiersz("Temat", [(textBoxTemat, 4)]);
		uklad.DodajWiersz([(new Poziomo([checkBoxUstawDate, checkBoxPrzeliczTermin]), 5)]);
		uklad.DodajWiersz([(textBoxTresc, 5)]);

		UstawZawartosc(uklad);
	}

	protected override void EdytorGotowy()
	{
		base.EdytorGotowy();
		var konfiguracja = Kontekst.Baza.Konfiguracja.First();
		if (konfiguracja.CzyDomyslna) OknoKomunikatu.Ostrzezenie("Przed wysłaniem wiadomości należy uzupełnić parametry połączenia z serwerem pocztowym dostępne w menu \"Serwisowe\" » \"Konfiguracja\".");
		szablonAdresat = "[NABYWCA-NAZWA] <[NABYWCA-EMAIL]>";
		szablonTemat = konfiguracja.EMailTemat;
		szablonTresc = konfiguracja.EMailTresc;
		szablonNadawca = konfiguracja.EMailNadawca;
		var pozycje = new List<Faktura>();
		pozycje.Add(new Faktura { Numer = "(wszystkie)", Id = 0 });
		pozycje.AddRange(Faktury.OrderBy(e => e.DataWystawienia).ThenBy(e => e.Id));
#if WINFORMS
		comboBoxFaktura.DisplayMember = "Numer";
#endif
#if AVALONIA
		comboBoxFaktura.DisplayMemberBinding = Avalonia.Data.CompiledBinding.Create<Faktura, string>(pozycja => pozycja.Numer);
#endif
		comboBoxFaktura.DataSource = pozycje;
	}

	private void ZmienionaFaktura()
	{
		if (comboBoxFaktura.SelectedIndex == 0)
		{
			textBoxAdresat.Text = szablonAdresat;
			textBoxTemat.Text = szablonTemat;
			textBoxTresc.Text = szablonTresc;
		}
		else if (comboBoxFaktura.SelectedItem is Faktura faktura)
		{
			textBoxAdresat.Text = faktura.PodstawPolaWysylki(szablonAdresat);
			textBoxTemat.Text = faktura.PodstawPolaWysylki(szablonTemat);
			textBoxTresc.Text = faktura.PodstawPolaWysylki(szablonTresc);
		}
	}

	private void Poprzednia()
	{
		if (comboBoxFaktura.SelectedIndex == 0) return;
		comboBoxFaktura.SelectedIndex--;
	}

	private void Nastepna()
	{
		if (comboBoxFaktura.SelectedIndex == comboBoxFaktura.Items.Count - 1) return;
		comboBoxFaktura.SelectedIndex++;
	}

	private void Wyslij()
	{
		var idx = comboBoxFaktura.SelectedIndex;
		if (idx == 0) WyslijWszystkie();
		else WyslijBiezaca();
	}

	private void WyslijBiezaca()
	{
		if (comboBoxFaktura.SelectedItem is not Faktura fakturaDoWysylki) return;
		using var nowyKontekst = new Kontekst(Kontekst);
		using var transakcja = nowyKontekst.Transakcja();
		var fakturaDoZapisu = nowyKontekst.Baza.Znajdz(fakturaDoWysylki.Ref);
		if (checkBoxUstawDate.Checked) fakturaDoZapisu.DataWystawienia = DateTime.Now;
		if (checkBoxPrzeliczTermin.Checked)
		{
			var sposobPlatnosci = nowyKontekst.Baza.ZnajdzLubNull(fakturaDoZapisu.SposobPlatnosciRef);
			if (sposobPlatnosci != null) fakturaDoZapisu.TerminPlatnosci = fakturaDoZapisu.DataWystawienia.AddDays(sposobPlatnosci.LiczbaDni);
		}
		fakturaDoZapisu.DataWyslania = DateTime.Now;
		nowyKontekst.Baza.Zapisz(fakturaDoZapisu);

		var idx = comboBoxFaktura.SelectedIndex;
		var temat = textBoxTemat.Text ?? "";
		var tresc = textBoxTresc.Text ?? "";
		var adresat = textBoxAdresat.Text ?? "";
		var nadawca = fakturaDoWysylki.PodstawPolaWysylki(szablonNadawca);
		try
		{
			InternetAddress.Parse(adresat);
		}
		catch (Exception)
		{
			throw new ApplicationException($"Adres odbiorcy \"{adresat}\" jest nieprawidłowy.");
		}
		try
		{
			InternetAddress.Parse(nadawca);
		}
		catch (Exception)
		{
			throw new ApplicationException($"Adres nadawcy \"{nadawca}\" jest nieprawidłowy.");
		}
		OknoPostepu.Uruchom(async cancellationToken =>
		{
			var pdf = PrzygotujPDF(fakturaDoWysylki);
			await Wyslij(temat, tresc, adresat, nadawca, pdf, fakturaDoWysylki.Numer, cancellationToken);
		});
		transakcja.Zatwierdz();
		var faktury = (List<Faktura>)comboBoxFaktura.DataSource!;
		faktury.Remove(fakturaDoWysylki);
		comboBoxFaktura.BeginUpdate();
		comboBoxFaktura.DataSource = Array.Empty<Faktura>();
		comboBoxFaktura.DataSource = faktury;
		comboBoxFaktura.SelectedIndex = Math.Min(idx, faktury.Count - 1);
		comboBoxFaktura.EndUpdate();
		if (faktury.Count == 1) Zamknij();
	}

	private void WyslijWszystkie()
	{
		var ustawDate = checkBoxUstawDate.Checked;
		var przeliczTermin = checkBoxPrzeliczTermin.Checked;
		OknoPostepu.Uruchom(async cancellationToken =>
		{
			using var nowyKontekst = new Kontekst(Kontekst);
			var faktury = (List<Faktura>)comboBoxFaktura.DataSource!;
			foreach (var fakturaDoWysylki in faktury)
			{
				if (cancellationToken.IsCancellationRequested) return;
				if (fakturaDoWysylki.Id == 0) continue;
				using var transakcja = nowyKontekst.Transakcja();
				var fakturaDoZapisu = nowyKontekst.Baza.Znajdz(fakturaDoWysylki.Ref);
				if (ustawDate) fakturaDoZapisu.DataWystawienia = DateTime.Now;
				if (przeliczTermin)
				{
					var sposobPlatnosci = nowyKontekst.Baza.ZnajdzLubNull(fakturaDoZapisu.SposobPlatnosciRef);
					if (sposobPlatnosci != null) fakturaDoZapisu.TerminPlatnosci = fakturaDoZapisu.DataWystawienia.AddDays(sposobPlatnosci.LiczbaDni);
				}
				fakturaDoZapisu.DataWyslania = DateTime.Now;
				nowyKontekst.Baza.Zapisz(fakturaDoZapisu);
				var adresat = fakturaDoWysylki.PodstawPolaWysylki(szablonAdresat);
				var temat = fakturaDoWysylki.PodstawPolaWysylki(szablonTemat);
				var tresc = fakturaDoWysylki.PodstawPolaWysylki(szablonTresc);
				var nadawca = fakturaDoWysylki.PodstawPolaWysylki(szablonNadawca);
				try
				{
					InternetAddress.Parse(adresat);
				}
				catch (Exception)
				{
					throw new ApplicationException($"Adres odbiorcy \"{adresat}\" dla faktury {fakturaDoWysylki.Numer} jest nieprawidłowy.");
				}
				try
				{
					InternetAddress.Parse(nadawca);
				}
				catch (Exception)
				{
					throw new ApplicationException($"Adres nadawcy \"{nadawca}\" dla faktury {fakturaDoWysylki.Numer} jest nieprawidłowy.");
				}
				var pdf = PrzygotujPDF(fakturaDoWysylki);
				await Wyslij(temat, tresc, adresat, nadawca, pdf, fakturaDoWysylki.Numer, cancellationToken);
				transakcja.Zatwierdz();
			}
		});

		Zamknij();
	}

	private byte[] PrzygotujPDF(Ref<Faktura> fakturaRef)
	{
		var wydruk = new Wydruki.Faktura(Kontekst.Baza, new[] { fakturaRef });
		return wydruk.ZapiszJako();
	}

	private async Task Wyslij(string temat, string tresc, string adresat, string nadawca, byte[] pdf, string nazwa, CancellationToken cancellationToken)
	{
		var konfiguracja = Kontekst.Baza.Konfiguracja.First();

		var wiadomosc = new MimeMessage();
		wiadomosc.From.Add(InternetAddress.Parse(nadawca));
		wiadomosc.To.Add(InternetAddress.Parse(adresat));
		wiadomosc.Subject = temat;

		var builder = new BodyBuilder();
		builder.TextBody = tresc;
		builder.Attachments.Add(nazwa.Replace('/', '-').Replace(':', '-') + ".pdf", pdf, ContentType.Parse("application/pdf"));
		wiadomosc.Body = builder.ToMessageBody();

		using var smtp = new SmtpClient();
		var secureSocketOptions = konfiguracja.SMTPPort == 25 ? SecureSocketOptions.None : SecureSocketOptions.Auto;

		await smtp.ConnectAsync(konfiguracja.SMTPSerwer, konfiguracja.SMTPPort, secureSocketOptions, cancellationToken);

		if (!String.IsNullOrEmpty(konfiguracja.SMTPLogin) && !String.IsNullOrEmpty(konfiguracja.SMTPHaslo))
		{
			await smtp.AuthenticateAsync(konfiguracja.SMTPLogin, konfiguracja.SMTPHaslo, cancellationToken);
		}

		await smtp.SendAsync(wiadomosc, cancellationToken);
		await smtp.DisconnectAsync(true, cancellationToken);
	}

	private void ZmienionyAdresat()
	{
		if (comboBoxFaktura.SelectedIndex != 0) return;
		szablonAdresat = textBoxAdresat.Text ?? "";
	}

	private void ZmienionyTemat()
	{
		if (comboBoxFaktura.SelectedIndex != 0) return;
		szablonTemat = textBoxTemat.Text ?? "";
	}

	private void ZmienionaTresc()
	{
		if (comboBoxFaktura.SelectedIndex != 0) return;
		szablonTresc = textBoxTresc.Text ?? "";
	}

	private void ZmienionaData()
	{
		checkBoxPrzeliczTermin.Enabled = checkBoxUstawDate.Checked;
		if (!checkBoxUstawDate.Checked) checkBoxPrzeliczTermin.Checked = false;
	}
}
