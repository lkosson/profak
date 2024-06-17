using Microsoft.Reporting.WinForms;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI.Faktury;

partial class WysylkaFakturEdytor : UserControl
{
	public IEnumerable<Faktura> Faktury { get; set; }
	public Kontekst Kontekst { get; set; }

	private string szablonAdresat;
	private string szablonTemat;
	private string szablonTresc;
	private string szablonNadawca;

	public WysylkaFakturEdytor()
	{
		InitializeComponent();
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

	private void comboBoxFaktura_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex == 0)
		{
			textBoxAdresat.Text = szablonAdresat;
			textBoxTemat.Text = szablonTemat;
			textBoxTresc.Text = szablonTresc;
		}
		else
		{
			var faktura = (Faktura)comboBoxFaktura.SelectedItem;
			textBoxAdresat.Text = faktura.PodstawPolaWysylki(szablonAdresat);
			textBoxTemat.Text = faktura.PodstawPolaWysylki(szablonTemat);
			textBoxTresc.Text = faktura.PodstawPolaWysylki(szablonTresc);
		}
	}

	private void buttonPoprzednia_Click(object sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex == 0) return;
		comboBoxFaktura.SelectedIndex--;
	}

	private void buttonNastepna_Click(object sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex == comboBoxFaktura.Items.Count - 1) return;
		comboBoxFaktura.SelectedIndex++;
	}

	private void buttonWyslij_Click(object sender, EventArgs e)
	{
		var idx = comboBoxFaktura.SelectedIndex;
		if (idx == 0) WyslijWszystkie();
		else WyslijBiezaca();
	}

	private void WyslijBiezaca()
	{
		var idx = comboBoxFaktura.SelectedIndex;
		var faktura = (Faktura)comboBoxFaktura.SelectedItem;
		var temat = textBoxTemat.Text;
		var tresc = textBoxTresc.Text;
		var adresat = textBoxAdresat.Text;
		var nadawca = faktura.PodstawPolaWysylki(szablonNadawca);
		OknoPostepu.Uruchom(async delegate
		{
			var pdf = PrzygotujPDF(faktura);
			await Wyslij(temat, tresc, adresat, nadawca, pdf, faktura.Numer);
		});
		var faktury = (List<Faktura>)comboBoxFaktura.DataSource;
		faktury.Remove(faktura);
		comboBoxFaktura.BeginUpdate();
		comboBoxFaktura.DataSource = Array.Empty<Faktura>();
		comboBoxFaktura.DataSource = faktury;
		comboBoxFaktura.DisplayMember = "Numer";
		comboBoxFaktura.SelectedIndex = Math.Min(idx, faktury.Count - 1);
		comboBoxFaktura.EndUpdate();

		if (faktury.Count == 1)
		{
			ParentForm.DialogResult = DialogResult.OK;
			ParentForm.Close();
		}
	}

	private void WyslijWszystkie()
	{
		OknoPostepu.Uruchom(async delegate
		{
			var faktury = (List<Faktura>)comboBoxFaktura.DataSource;
			foreach (var faktura in faktury)
			{
				if (faktura.Id == 0) continue;
				var pdf = PrzygotujPDF(faktura);
				var adresat = faktura.PodstawPolaWysylki(szablonAdresat);
				var temat = faktura.PodstawPolaWysylki(szablonTemat);
				var tresc = faktura.PodstawPolaWysylki(szablonTresc);
				var nadawca = faktura.PodstawPolaWysylki(szablonNadawca);
				await Wyslij(temat, tresc, adresat, nadawca, pdf, faktura.Numer);
			}
		});
		ParentForm.DialogResult = DialogResult.OK;
		ParentForm.Close();
	}

	private byte[] PrzygotujPDF(Ref<Faktura> fakturaRef)
	{
		using var localReport = new LocalReport();
		var wydruk = new Wydruki.Faktura(Kontekst.Baza, new[] { fakturaRef });
		wydruk.Przygotuj(localReport);
		var pdf = localReport.Render("PDF");
		return pdf;
	}

	private async Task Wyslij(string temat, string tresc, string adresat, string nadawca, byte[] pdf, string nazwa)
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
		await smtp.SendMailAsync(wiadomosc);
	}

	private void textBoxAdresat_TextChanged(object sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex != 0) return;
		szablonAdresat = textBoxAdresat.Text;
	}

	private void textBoxTemat_TextChanged(object sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex != 0) return;
		szablonTemat = textBoxTemat.Text;
	}

	private void textBoxTresc_TextChanged(object sender, EventArgs e)
	{
		if (comboBoxFaktura.SelectedIndex != 0) return;
		szablonTresc = textBoxTresc.Text;
	}
}
