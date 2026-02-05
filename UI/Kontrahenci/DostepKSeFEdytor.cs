using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ProFak.DB;

namespace ProFak.UI.Kontrahenci;
partial class DostepKSeFEdytor : UserControl
{
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public SrodowiskoKSeF SrodowiskoKSeF { get; set; }

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string Token { get; set; }

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string NIP { get => textBoxNIP.Text; set => textBoxNIP.Text = value; }

	public DostepKSeFEdytor()
	{
		InitializeComponent();
	}

	private void buttonPobierzXML_Click(object sender, EventArgs e)
	{
		try
		{
			var nip = NIP;
			string xml = null;
			OknoPostepu.Uruchom(async () =>
			{
				using var api = new IO.KSEF2.API(SrodowiskoKSeF);
				xml = await api.PobierzZadanieDostepuDoPodpisuAsync(nip);
			});
			if (xml == null) return;

			using var dialog = new SaveFileDialog();
			dialog.Filter = "Wniosek o dostęp (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
			dialog.Title = "Wybierz miejsce do zapisu wniosku o dostęp";
			dialog.RestoreDirectory = true;
			dialog.FileName = "wniosek-do-podpisu.xml";
			if (dialog.ShowDialog() != DialogResult.OK) return;
			File.WriteAllText(dialog.FileName, xml);
			MessageBox.Show("Wniosek został zapisany pomyślnie. Podpisz go elektronicznie i załaduj do programu zgodnie z dalszymi instrukcjami.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		catch (Exception exc)
		{
			OknoBledu.Pokaz(exc);
		}
	}

	private void buttonWskazXML_Click(object sender, EventArgs e)
	{
		try
		{
			using var dialog = new OpenFileDialog();
			dialog.Filter = "Wniosek o dostęp (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
			dialog.Title = "Wybierz plik z podpisanym wnioskiem o dostęp";
			dialog.RestoreDirectory = true;
			if (dialog.ShowDialog() != DialogResult.OK) return;

			var signedXml = File.ReadAllText(dialog.FileName);

			string token = null;
			OknoPostepu.Uruchom(async cancellationToken =>
			{
				using var api = new IO.KSEF2.API(SrodowiskoKSeF);
				await api.PrzeslijZadanieDostepuAsync(signedXml, cancellationToken);
				token = await api.UtworzTokenAsync(cancellationToken);
			});
			if (token == null) return;

			Token = token;
			IO.KSEF2.API.ZapomnijAktywnaSesje();
			MessageBox.Show("Dostęp do KSeF nadany pomyślnie. Można skasować utworzone pliki.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
			ParentForm.Close();
		}
		catch (Exception exc)
		{
			OknoBledu.Pokaz(exc);
		}
	}

	private void linkEPUAP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "https://moj.gov.pl/nforms/signer/upload?xFormsAppName=SIGNER" });
	}
}
