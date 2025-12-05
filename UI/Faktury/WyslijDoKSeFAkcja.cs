using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class WyslijDoKSeFAkcja : AkcjaNaSpisie<Faktura>
	{
		public override string Nazwa => "✉ Wyślij do KSeF [CTRL-K]";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Any();
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => klawisz == Keys.K && modyfikatory == Keys.Control;

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
		{
			var rekordy = zaznaczoneRekordy;
			OknoPostepu.Uruchom(async delegate
			{
				var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
				if (String.IsNullOrEmpty(podmiot.TokenKSeF)) throw new ApplicationException("Brak tokena dostępowego do KSeF w danych firmy.");

				var doWyslania = new List<Faktura>();
				foreach (var faktura in rekordy)
				{
					if (!String.IsNullOrWhiteSpace(faktura.NumerKSeF))
					{
						var res = MessageBox.Show($"Faktura {faktura.Numer} już była wysłana do KSeF. Czy chcesz ją wysłać ponownie?", "ProFak", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
						if (res == DialogResult.Cancel) return;
						if (res == DialogResult.No) continue;
					}
					else if (String.IsNullOrWhiteSpace(faktura.XMLKSeF))
					{
#if FA_2
						faktura.XMLKSeF = IO.FA_2.Generator.ZbudujXML(kontekst.Baza, faktura);
#elif FA_3
						faktura.XMLKSeF = IO.FA_3.Generator.ZbudujXML(kontekst.Baza, faktura);
#endif
						kontekst.Baza.Zapisz(faktura);
					}
					doWyslania.Add(faktura);
				}

				if (!doWyslania.Any()) return;

#if KSEF_1
				using var api = new IO.KSEF.API(podmiot.SrodowiskoKSeF);
				var cts = new CancellationTokenSource();
				await api.AuthenticateAsync(podmiot.NIP, podmiot.TokenKSeF);
				foreach (var faktura in doWyslania)
				{
					(faktura.NumerKSeF, faktura.DataKSeF, faktura.URLKSeF) = await api.SendInvoiceAsync(faktura.XMLKSeF, faktura.NIPNabywcy, faktura.DataWystawienia, cts.Token);
					kontekst.Baza.Zapisz(faktura);
				}
				await api.Terminate();
#elif KSEF_2
				using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
				var cts = new CancellationTokenSource();
				await api.AuthenticateAsync(podmiot.NIP, podmiot.TokenKSeF, cts.Token);
				var (sessionReferenceNumber, encryptionData) = await api.OpenSessionAsync(cts.Token);
				var wyslane = new List<(Faktura faktura, string invoiceReferenceNumber)>();
				foreach (var faktura in doWyslania)
				{
					cts.Token.ThrowIfCancellationRequested();
					var (invoiceReferenceNumber, verificationLink) = await api.SendInvoiceAsync(sessionReferenceNumber, encryptionData, faktura.XMLKSeF, faktura.NIPSprzedawcy, faktura.DataWystawienia, cts.Token);
					wyslane.Add((faktura, invoiceReferenceNumber));
					faktura.URLKSeF = verificationLink;
					kontekst.Baza.Zapisz(faktura);
				}
				await api.CloseSessionAsync(sessionReferenceNumber, cts.Token);
				await api.FillSessionInvoiceMetadata(sessionReferenceNumber, wyslane, cts.Token);
				foreach (var (faktura, _) in wyslane)
				{
					kontekst.Baza.Zapisz(faktura);
				}
#endif
			});
		}
	}
}
