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
					doWyslania.Add(faktura);
				}

				if (!doWyslania.Any()) return;

#if KSEF_1
				using var api = new IO.KSEF.API(podmiot.SrodowiskoKSeF);
#else
				using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
#endif
				var cts = new CancellationTokenSource();
				await api.AuthenticateAsync(podmiot.NIP, podmiot.TokenKSeF);
				foreach (var faktura in doWyslania)
				{
					faktura.XMLKSeF = IO.KSEF.Generator.ZbudujXML(kontekst.Baza, faktura);
					kontekst.Baza.Zapisz(faktura);
					(faktura.NumerKSeF, faktura.DataKSeF, faktura.URLKSeF) = await api.SendInvoiceAsync(faktura.XMLKSeF, faktura.NIPNabywcy, faktura.DataWystawienia, cts.Token);
					kontekst.Baza.Zapisz(faktura);
				}
				await api.Terminate();
			});
		}
	}
}
