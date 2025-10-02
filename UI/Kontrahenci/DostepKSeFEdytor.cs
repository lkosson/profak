﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ProFak.DB;

namespace ProFak.UI.Kontrahenci;
partial class DostepKSeFEdytor : UserControl
{
	public SrodowiskoKSeF SrodowiskoKSeF { get; set; }
	public string Token { get; set; }
	public string NIP { get => textBoxNIP.Text; set => textBoxNIP.Text = value; }

	public DostepKSeFEdytor()
	{
		InitializeComponent();
	}

	private void buttonPobierzXML_Click(object sender, EventArgs e)
	{
		var nip = NIP;
		string xml = null;
		OknoPostepu.Uruchom(async delegate
		{
			using var api = new IO.KSEF2.API(SrodowiskoKSeF);
			xml = await api.AuthenticateSignatureBeginAsync(nip);
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

	private void buttonWskazXML_Click(object sender, EventArgs e)
	{
		using var dialog = new OpenFileDialog();
		dialog.Filter = "Wniosek o dostęp (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
		dialog.Title = "Wybierz plik z podpisanym wnioskiem o dostęp";
		dialog.RestoreDirectory = true;
		if (dialog.ShowDialog() != DialogResult.OK) return;

		var signedXml = File.ReadAllText(dialog.FileName);

		string token = null;
		OknoPostepu.Uruchom(async delegate
		{
			using var api = new IO.KSEF2.API(SrodowiskoKSeF);
			await api.AuthenticateSignatureEndAsync(signedXml);
			token = await api.GenerateToken();
		});
		if (token == null) return;

		Token = token;
		MessageBox.Show("Dostęp do KSeF nadany pomyślnie. Można skasować utworzone pliki.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
		ParentForm.Close();
	}

	private void linkEPUAP_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "https://moj.gov.pl/nforms/signer/upload?xFormsAppName=SIGNER" });
	}
}
