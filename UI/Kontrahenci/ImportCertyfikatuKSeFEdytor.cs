using KSeF.Client.Extensions;
using ProFak.DB;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace ProFak.UI;

class ImportCertyfikatuKSeFEdytor : Edytor
{
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public SrodowiskoKSeF SrodowiskoKSeF { get; set; }

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string Certyfikat { get; set; } = "";

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string NIP { get; set; } = "";

	private readonly LinkLabel linkLabelAplikacjaPodatnika;
	private readonly TextBox textBoxCertyfikat = Kontrolki.TextBox();
	private readonly TextBox textBoxKlucz = Kontrolki.TextBox();
	private readonly TextBox textBoxHaslo = Kontrolki.TextBox();
	private readonly Button buttonCertyfikat = Kontrolki.Button("...");
	private readonly Button buttonKlucz = Kontrolki.Button("...");
	private readonly Button buttonZapisz = Kontrolki.Button("Zapisz");

	public ImportCertyfikatuKSeFEdytor()
	{
		textBoxCertyfikat = Kontrolki.TextBox();
		textBoxKlucz = Kontrolki.TextBox();
		textBoxHaslo = Kontrolki.TextBox();
		buttonCertyfikat = Kontrolki.Button("...", WybierzCertyfikat);
		buttonKlucz = Kontrolki.Button("...", WybierzKlucz);
		buttonZapisz = Kontrolki.Button("Zapisz", Zapisz);
		linkLabelAplikacjaPodatnika = Kontrolki.Link("Aplikacja podatnika", AplikacjaPodatnika);

		var uklad = new Siatka([0, -1, 0], []);
		uklad.DodajWiersz([(Kontrolki.Text("Aby nadać ProFakowi dostęp do KSeF przy użyciu certyfikatu, zaloguj się do Aplikacji Podatnika używając poniższego odnośnika, a następnie wygeneruj nowy certyfikat przeznaczony do uwierzytelniania."), 3)]);
		uklad.DodajWiersz([(linkLabelAplikacjaPodatnika, 3)]);
		uklad.DodajWiersz([(Kontrolki.Text("Wskaż w poniższych polach pliki z wygenerowanym w Aplikacji Podatnika certyfikatem i kluczem prywatnym oraz wprowadź hasło do klucza prywatnego."), 3)]);
		uklad.DodajWiersz("Plik certyfikatu", [textBoxCertyfikat, buttonCertyfikat]);
		uklad.DodajWiersz("Plik klucza prywatnego", [textBoxKlucz, buttonKlucz]);
		uklad.DodajWiersz("Hasło klucza prywatnego", [(textBoxHaslo, 2)]);
		uklad.DodajWiersz([(Kontrolki.Text("Po kliknięciu \"Zapisz\" ProFak wczyta podane pliki, a następnie spróbuje uwierzytelnić się przy użyciu certyfikatu w API KSeF."), 3)]);
		uklad.DodajWiersz([(new Poziomo([buttonZapisz]), 3)]);
		uklad.Width = 500;
		uklad.Height = 210;

		UstawZawartosc(uklad);
	}

	protected override void OnLoad(EventArgs e)
	{
		if (SrodowiskoKSeF == SrodowiskoKSeF.Test) linkLabelAplikacjaPodatnika.Text = "https://ap-test.ksef.mf.gov.pl/web/";
		else if (SrodowiskoKSeF == SrodowiskoKSeF.Demo) linkLabelAplikacjaPodatnika.Text = "https://ap-demo.ksef.mf.gov.pl/web/";
		else if (SrodowiskoKSeF == SrodowiskoKSeF.Prod) linkLabelAplikacjaPodatnika.Text = "https://ap.ksef.mf.gov.pl/web/";
		base.OnLoad(e);
	}

	private void AplikacjaPodatnika()
	{
		Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = linkLabelAplikacjaPodatnika.Text });
	}

	private void WybierzCertyfikat()
	{
		using var dialog = new OpenFileDialog();
		dialog.Filter = "Certyfikaty (*.crt)|*.crt|Wszystkie pliki (*.*)|*.*";
		dialog.Title = "Wybierz plik z certyfikatem KSeF";
		dialog.RestoreDirectory = true;
		if (dialog.ShowDialog() != DialogResult.OK) return;
		var zawartosc = File.ReadAllText(dialog.FileName);
		if (zawartosc.Contains("-----BEGIN CERTIFICATE-----")) textBoxCertyfikat.Text = dialog.FileName;
		else MessageBox.Show("Wskazany plik nie zawiera poprawnego certyfikatu.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
	}

	private void WybierzKlucz()
	{
		using var dialog = new OpenFileDialog();
		dialog.Filter = "Klucze prywatne (*.key)|*.key|Wszystkie pliki (*.*)|*.*";
		dialog.Title = "Wybierz plik z kluczem prywatnym KSeF";
		dialog.RestoreDirectory = true;
		if (dialog.ShowDialog() != DialogResult.OK) return;
		textBoxKlucz.Text = dialog.FileName;
		var zawartosc = File.ReadAllText(dialog.FileName);
		if (zawartosc.Contains("-----BEGIN ENCRYPTED PRIVATE KEY-----") || zawartosc.Contains("-----BEGIN PRIVATE KEY-----")) textBoxKlucz.Text = dialog.FileName;
		else MessageBox.Show("Wskazany plik nie zawiera poprawnego klucza prywatnego.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
	}

	private void Zapisz()
	{
		if (String.IsNullOrEmpty(textBoxCertyfikat.Text))
		{
			MessageBox.Show("Wskaż plik z certyfikatem dostępowym KSeF.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return;
		}

		if (String.IsNullOrEmpty(textBoxKlucz.Text))
		{
			MessageBox.Show("Wskaż plik z kluczem prywatnym.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			return;
		}

		var plikCertyfikatu = textBoxCertyfikat.Text;
		var plikKlucza = textBoxKlucz.Text;
		if (!plikCertyfikatu.StartsWith("-----")) plikCertyfikatu = File.ReadAllText(plikCertyfikatu);
		if (!plikKlucza.StartsWith("-----")) plikKlucza = File.ReadAllText(plikKlucza);

		var certyfikat = X509CertificateLoaderExtensions.LoadCertificate(Encoding.UTF8.GetBytes(plikCertyfikatu));
		var polaczonyCertyfikat = X509CertificateLoaderExtensions.MergeWithPemKey(certyfikat, plikKlucza, textBoxHaslo.Text);
		var blobCertyfikatu = polaczonyCertyfikat.Export(System.Security.Cryptography.X509Certificates.X509ContentType.Pkcs12);

		OknoPostepu.Uruchom(async cancellationToken =>
		{
			IO.KSEF2.API.ZapomnijAktywnaSesje();
			using var api = new IO.KSEF2.API(SrodowiskoKSeF);
			await api.UwierzytelnijAsync(NIP, polaczonyCertyfikat, cancellationToken);
		});

		Certyfikat = Convert.ToBase64String(blobCertyfikatu);
		MessageBox.Show("Certyfikat zaimportowany poprawnie. Możesz skasować pliki certyfikatu i klucza prywatnego.", "ProFak", MessageBoxButtons.OK, MessageBoxIcon.Information);
		ParentForm?.Close();
	}
}
