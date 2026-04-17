using ProFak.DB;
using System.ComponentModel;
using System.Diagnostics;

namespace ProFak.UI;

partial class DostepKSeFEdytor : Edytor
{
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public SrodowiskoKSeF SrodowiskoKSeF { get; set; }

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string? Token { get; set; }

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string? NIP { get => textBoxNIP.Text; set => textBoxNIP.Text = value; }

	private readonly TextBox textBoxNIP;

	public DostepKSeFEdytor()
	{
		textBoxNIP = Kontrolki.TextBox();
		var buttonPobierzXML = Kontrolki.Button("Pobierz XML do podpisu", PobierzXML);
		var buttonWskazXML = Kontrolki.Button("Wczytaj podpisany XML", WskazXML);

		var uklad = new Pionowo([
			Kontrolki.Text("Aby ProFak mógł korzystać w Twoim imieniu z KSeF, musisz nadać mu odpowiednie uprawnienia. Jeśli masz już token dostępowy, zamknij to okno i wprowadź go na poprzednim ekranie, w polu tekstowym obok przycisku, który właśnie kliknąłeś."),
			Kontrolki.Text("Jeśli masz wątpliwości co do bezpieczeństwa swoich danych, tego jak ProFak będzie je przetwarzał i jak działa KSeF, zamknij to okno.", pogrubiony: true),
			Kontrolki.Text("Jeśli jeszcze nie korzystałeś z KSeF lub nie masz tokena, w pierwszej kolejności zweryfikuj, czy podany niżej NIP jest prawidłowy:"),
			new Poziomo([textBoxNIP]),
			Kontrolki.Text("Jeśli NIP się zgadza, kliknij poniższy przycisk, aby pobrać elektroniczny dokument umożliwiający ProFakowi jednorazowy dostęp w Twoim imieniu do KSeF. Zapisz ten plik w wygodnym miejscu, np. na pulpicie. Będzie on potrzebny tylko na czas nadawania dostępu. Potem można go skaskować."),
			new Poziomo([buttonPobierzXML]),
			Kontrolki.Text("Jak już masz zapisany plik, kliknij na poniższy odnośnik, aby przejść do https://moj.gov.pl/ w celu podpisania dokumentu. Możesz też podpisać dokument podpisem certyfikowanym."),
			Kontrolki.Link("Podpisz dokument Profilem Zaufanym", EPUAP),
			Kontrolki.Text("Gdy już masz podpisany plik, kliknij na poniższy przycisk, aby go wskazać. ProFak użyje go do zalogowania się do KSeF w Twoim imieniu i wygenerowania tokena dostępowego umożliwiającego wystawianie i odbieranie faktur."),
			new Poziomo([buttonWskazXML])
			]);
		uklad.Size = uklad.GetPreferredSize(new Size(800 * DeviceDpi / 96, 0));

		UstawZawartosc(uklad);
	}

	private void PobierzXML()
	{
		var nip = NIP;
		if (String.IsNullOrEmpty(nip)) throw new ApplicationException("Należy podać NIP firmy.");
		string? xml = null;
		OknoPostepu.Uruchom(async cancellationToken =>
		{
			using var api = new IO.KSEF2.API(SrodowiskoKSeF);
			xml = await api.PobierzZadanieDostepuDoPodpisuAsync(nip, cancellationToken);
		});
		if (xml == null) return;

		using var dialog = new SaveFileDialog();
		dialog.Filter = "Wniosek o dostęp (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
		dialog.Title = "Wybierz miejsce do zapisu wniosku o dostęp";
		dialog.RestoreDirectory = true;
		dialog.FileName = "wniosek-do-podpisu.xml";
		if (dialog.ShowDialog() != DialogResult.OK) return;
		File.WriteAllText(dialog.FileName, xml);
		OknoKomunikatu.Informacja("Wniosek został zapisany pomyślnie. Podpisz go elektronicznie i załaduj do programu zgodnie z dalszymi instrukcjami.");
	}

	private void WskazXML()
	{
		using var dialog = new OpenFileDialog();
		dialog.Filter = "Wniosek o dostęp (*.xml)|*.xml|Wszystkie pliki (*.*)|*.*";
		dialog.Title = "Wybierz plik z podpisanym wnioskiem o dostęp";
		dialog.RestoreDirectory = true;
		if (dialog.ShowDialog() != DialogResult.OK) return;

		var signedXml = File.ReadAllText(dialog.FileName);

		string? token = null;
		OknoPostepu.Uruchom(async cancellationToken =>
		{
			using var api = new IO.KSEF2.API(SrodowiskoKSeF);
			await api.PrzeslijZadanieDostepuAsync(signedXml, cancellationToken);
			token = await api.UtworzTokenAsync(cancellationToken);
		});
		if (token == null) return;

		Token = token;
		IO.KSEF2.API.ZapomnijAktywnaSesje();
		OknoKomunikatu.Informacja("Dostęp do KSeF nadany pomyślnie. Można skasować utworzone pliki.");
		ParentForm?.Close();
	}

	private void EPUAP()
	{
		Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = "https://moj.gov.pl/nforms/signer/upload?xFormsAppName=SIGNER" });
	}
}
