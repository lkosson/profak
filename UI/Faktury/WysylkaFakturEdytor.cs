using Microsoft.Reporting.WinForms;
using ProFak.DB;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace ProFak.UI.Faktury;

partial class WysylkaFakturEdytor : UserControl
{
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public IEnumerable<Faktura> Faktury { get; set; } = [];

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Kontekst Kontekst { get; set; } = default!;

	private string szablonAdresat = "";
	private string szablonTemat = "";
	private string szablonTresc = "";
	private string szablonNadawca = "";

	public WysylkaFakturEdytor()
	{
		InitializeComponent();
		Wyglad.UsunIkonePrzycisku(buttonNastepna);
		Wyglad.UsunIkonePrzycisku(buttonPoprzednia);
		Wyglad.UsunIkonePrzycisku(buttonWyslij);
	}

	protected override void OnLoad(EventArgs e)
	{
		var konfiguracja = Kontekst.Baza.Konfiguracja.First();
		if (konfiguracja.CzyDomyslna) MessageBox.Show("Przed wysłaniem wiadomości należy uzupełnić parametry połączenia z serwerem pocztowym dostępne w menu \"Serwisowe\" » \"Konfiguracja\".", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		szablonAdresat = "[NABYWCA-NAZWA] <[NABYWCA-EMAIL]>";
		szablonTemat = konfiguracja.EMailTemat;
		szablonTresc = konfiguracja.EMailTresc;
		szablonNadawca = konfiguracja.EMailNadawca;
		var pozycje = new List<Faktura>();
		pozycje.Add(new Faktura { Numer = "(wszystkie)", Id = 0 });
		pozycje.AddRange(Faktury.OrderBy(e => e.DataWystawienia).ThenBy(e => e.Id));
		comboBoxFaktura.DataSource = pozycje;
		base.OnLoad(e);
	}

	private void comboBoxFaktura_SelectedIndexChanged(object? sender, EventArgs e)
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

	private void buttonPoprzednia_Click(object? sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex == 0) return;
		comboBoxFaktura.SelectedIndex--;
	}

	private void buttonNastepna_Click(object? sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex == comboBoxFaktura.Items.Count - 1) return;
		comboBoxFaktura.SelectedIndex++;
	}

	private void buttonWyslij_Click(object? sender, EventArgs e)
	{
		try
		{
			var idx = comboBoxFaktura.SelectedIndex;
			if (idx == 0) WyslijWszystkie();
			else WyslijBiezaca();
		}
		catch (Exception exc)
		{
			OknoBledu.Pokaz(exc);
		}
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
			var sposobPlatnosci = nowyKontekst.Baza.Znajdz(fakturaDoZapisu.SposobPlatnosciRef);
			if (sposobPlatnosci != null) fakturaDoZapisu.TerminPlatnosci = fakturaDoZapisu.DataWystawienia.AddDays(sposobPlatnosci.LiczbaDni);
		}
		fakturaDoZapisu.DataWyslania = DateTime.Now;
		nowyKontekst.Baza.Zapisz(fakturaDoZapisu);

		var idx = comboBoxFaktura.SelectedIndex;
		var temat = textBoxTemat.Text;
		var tresc = textBoxTresc.Text;
		var adresat = textBoxAdresat.Text;
		var nadawca = fakturaDoWysylki.PodstawPolaWysylki(szablonNadawca);
		if (!MailAddress.TryCreate(adresat, out var _)) throw new ApplicationException($"Adres odbiorcy \"{adresat}\" jest nieprawidłowy.");
		if (!MailAddress.TryCreate(nadawca, out var _)) throw new ApplicationException($"Adres nadawcy \"{nadawca}\" jest nieprawidłowy.");
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
		comboBoxFaktura.DisplayMember = "Numer";
		comboBoxFaktura.SelectedIndex = Math.Min(idx, faktury.Count - 1);
		comboBoxFaktura.EndUpdate();

		if (faktury.Count == 1 && ParentForm != null)
		{
			ParentForm.DialogResult = DialogResult.OK;
			ParentForm.Close();
		}
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
					var sposobPlatnosci = nowyKontekst.Baza.Znajdz(fakturaDoZapisu.SposobPlatnosciRef);
					if (sposobPlatnosci != null) fakturaDoZapisu.TerminPlatnosci = fakturaDoZapisu.DataWystawienia.AddDays(sposobPlatnosci.LiczbaDni);
				}
				fakturaDoZapisu.DataWyslania = DateTime.Now;
				nowyKontekst.Baza.Zapisz(fakturaDoZapisu);
				var adresat = fakturaDoWysylki.PodstawPolaWysylki(szablonAdresat);
				var temat = fakturaDoWysylki.PodstawPolaWysylki(szablonTemat);
				var tresc = fakturaDoWysylki.PodstawPolaWysylki(szablonTresc);
				var nadawca = fakturaDoWysylki.PodstawPolaWysylki(szablonNadawca);
				if (!MailAddress.TryCreate(adresat, out var _)) throw new ApplicationException($"Adres odbiorcy \"{adresat}\" dla faktury {fakturaDoWysylki.Numer} jest nieprawidłowy.");
				if (!MailAddress.TryCreate(nadawca, out var _)) throw new ApplicationException($"Adres nadawcy \"{nadawca}\" jest nieprawidłowy.");
				var pdf = PrzygotujPDF(fakturaDoWysylki);
				await Wyslij(temat, tresc, adresat, nadawca, pdf, fakturaDoWysylki.Numer, cancellationToken);
				transakcja.Zatwierdz();
			}
		});

		if (ParentForm != null)
		{
			ParentForm.DialogResult = DialogResult.OK;
			ParentForm.Close();
		}
	}

	private byte[] PrzygotujPDF(Ref<Faktura> fakturaRef)
	{
		using var localReport = new LocalReport();
		var wydruk = new Wydruki.Faktura(Kontekst.Baza, new[] { fakturaRef });
		wydruk.Przygotuj(localReport);
		var pdf = localReport.Render("PDF");
		return pdf;
	}

	private async Task Wyslij(string temat, string tresc, string adresat, string nadawca, byte[] pdf, string nazwa, CancellationToken cancellationToken)
	{
		var konfiguracja = Kontekst.Baza.Konfiguracja.First();
		var smtp = new SmtpClient(konfiguracja.SMTPSerwer, konfiguracja.SMTPPort);
		smtp.EnableSsl = konfiguracja.SMTPPort != 25;
		smtp.UseDefaultCredentials = false;
		smtp.Credentials = new NetworkCredential(konfiguracja.SMTPLogin, konfiguracja.SMTPHaslo);
		var wiadomosc = new MailMessage();
		wiadomosc.From = new MailAddress(nadawca);
		wiadomosc.Subject = temat;
		wiadomosc.Body = tresc;
		wiadomosc.IsBodyHtml = false;
		wiadomosc.To.Add(adresat);
		wiadomosc.Attachments.Add(new Attachment(new MemoryStream(pdf), nazwa.Replace('/', '-').Replace(':', '-'), "application/pdf"));
		await smtp.SendMailAsync(wiadomosc, cancellationToken);
	}

	private void textBoxAdresat_TextChanged(object? sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex != 0) return;
		szablonAdresat = textBoxAdresat.Text;
	}

	private void textBoxTemat_TextChanged(object? sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex != 0) return;
		szablonTemat = textBoxTemat.Text;
	}

	private void textBoxTresc_TextChanged(object? sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex != 0) return;
		szablonTresc = textBoxTresc.Text;
	}

	private void checkBoxUstawDate_CheckedChanged(object? sender, EventArgs e)
	{
		checkBoxPrzeliczTermin.Enabled = checkBoxUstawDate.Checked;
		if (!checkBoxUstawDate.Checked) checkBoxPrzeliczTermin.Checked = false;
	}
}
