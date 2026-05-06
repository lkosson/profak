using ProFak.DB;
using System.Diagnostics;

namespace ProFak.UI;

class ZapiszJakoPDFZKSeFAkcja(bool spisKSeF = false) : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "🖫 Zapisz PDF KSeF [CTRL-P]";
	public override bool CzyKlawiszSkrotu(TKeys klawisz, TKeyModifiers modyfikatory) => modyfikatory == TKeyModifiers.Control && klawisz == TKeys.P;
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) 
		=> zaznaczoneRekordy.Any(e => !String.IsNullOrEmpty(e.NumerKSeF));
	public override bool PrzeladujPoZakonczeniu => false;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		var faktury = zaznaczoneRekordy.Where(e => !String.IsNullOrEmpty(e.NumerKSeF)).ToList();
		if (faktury.Count == 0)
		{
			OknoKomunikatu.Ostrzezenie("Żadna z wybranych faktur nie posiada danych KSeF.");
			return;
		}

		if (faktury.Count == 1)
		{
			var faktura = faktury[0];

			var plik = OknoWyboruPliku.Zapisz("Zapisywanie wizualizacji faktury", "Dokument PDF", "*.pdf", faktura.NumerKSeFJakoNazwaPliku + ".pdf");
			if (plik == null) return;

			byte[] pdf = [];
			OknoPostepu.Uruchom(async cancellationToken =>
			{
				if (spisKSeF && String.IsNullOrEmpty(faktura.XMLKSeF))
				{
					var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
					using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
					await api.UwierzytelnijAsync(podmiot.NIP, podmiot.TokenKSeF, cancellationToken);
					faktura.XMLKSeF = await api.PobierzFaktureAsync(faktura.NumerKSeF, cancellationToken);
				}
				pdf = IO.KSEFPDF.Generator.ZbudujPDF(faktura.XMLKSeF, faktura.NumerKSeF, faktura.URLKSeF, cancellationToken);
			});
			File.WriteAllBytes(plik, pdf);
			Process.Start(new ProcessStartInfo { UseShellExecute = true, FileName = plik });
		}
		else
		{
			var katalog = OknoWyboruPliku.Katalog("Wybierz katalog, do którego mają zostać zapisane pliki PDF.");
			if (katalog == null) return;

			var liczbaPlikow = 0;
			OknoPostepu.Uruchom(async cancellationToken =>
			{
				if (spisKSeF)
				{
					var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
					using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
					await api.UwierzytelnijAsync(podmiot.NIP, podmiot.TokenKSeF, cancellationToken);
					foreach (var naglowek in faktury)
					{
						if (!String.IsNullOrEmpty(naglowek.XMLKSeF)) continue;
						naglowek.XMLKSeF = await api.PobierzFaktureAsync(naglowek.NumerKSeF, cancellationToken);
						cancellationToken.ThrowIfCancellationRequested();
					}
				}

				foreach (var faktura in faktury)
				{
					var pdf = IO.KSEFPDF.Generator.ZbudujPDF(faktura.XMLKSeF, faktura.NumerKSeF, faktura.URLKSeF, cancellationToken);
					var nazwaPliku = faktura.NumerKSeFJakoNazwaPliku + ".pdf";
					File.WriteAllBytes(Path.Combine(katalog, nazwaPliku), pdf);
					liczbaPlikow++;
				}
			});
			OknoKomunikatu.Informacja($"Liczba wygenerowanych plików PDF: {liczbaPlikow}.");
		}
	}
}
