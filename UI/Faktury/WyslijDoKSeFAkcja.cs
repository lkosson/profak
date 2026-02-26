using ProFak.DB;

namespace ProFak.UI;

class WyslijDoKSeFAkcja : AkcjaNaSpisie<Faktura>
{
	public override string Nazwa => "✉ Wyślij do KSeF [CTRL-K]";
	public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any();
	public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => klawisz == Keys.K && modyfikatory == Keys.Control;

	public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
	{
		var rekordy = zaznaczoneRekordy;
		OknoPostepu.Uruchom(async cancellationToken =>
		{
			var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
			if (String.IsNullOrEmpty(podmiot.TokenKSeF)) throw new ApplicationException("Brak tokena dostępowego do KSeF w danych firmy.\nNadaj dostęp do KSeF w oknie \"Kontrahenci\" -> \"Moja firma\" -> \"Dane urzędowe\" -> \"Token KSeF\".");

			var doWyslania = new List<Faktura>();
			foreach (var faktura in rekordy)
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (!String.IsNullOrWhiteSpace(faktura.NumerKSeF))
				{
					var res = MessageBox.Show($"Faktura {faktura.Numer} już była wysłana do KSeF. Czy chcesz ją wysłać ponownie?", "ProFak", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if (res == DialogResult.Cancel) return;
					if (res == DialogResult.No) continue;
				}
				else if (String.IsNullOrWhiteSpace(faktura.XMLKSeF))
				{
					faktura.XMLKSeF = IO.FA_3.Generator.ZbudujXML(kontekst.Baza, faktura);
					kontekst.Baza.Zapisz(faktura);
				}
				doWyslania.Add(faktura);
			}

			if (!doWyslania.Any()) return;

			using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
			await api.UwierzytelnijAsync(podmiot.NIP, podmiot.TokenKSeF, cancellationToken);
			var (sessionReferenceNumber, encryptionData) = await api.RozpocznijSesjeAsync(cancellationToken);
			var wyslane = new List<(Faktura faktura, string invoiceReferenceNumber)>();
			foreach (var faktura in doWyslania)
			{
				cancellationToken.ThrowIfCancellationRequested();
				var invoiceReferenceNumber = await api.WyslijFaktureAsync(sessionReferenceNumber, encryptionData, faktura.XMLKSeF, cancellationToken);
				var verificationLink = api.ZbudujUrl(faktura.XMLKSeF, faktura.NIPSprzedawcy, faktura.DataWystawienia);
				wyslane.Add((faktura, invoiceReferenceNumber));
				faktura.URLKSeF = verificationLink;
				kontekst.Baza.Zapisz(faktura);
			}
			await api.ZakonczSesjeAsync(sessionReferenceNumber, cancellationToken);
			await api.SprawdzStanWysylkiAsync(sessionReferenceNumber, wyslane, cancellationToken);
			foreach (var (faktura, _) in wyslane)
			{
				kontekst.Baza.Zapisz(faktura);
			}
		});
	}
}
