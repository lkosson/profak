using Microsoft.EntityFrameworkCore;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProFak.UI
{
	class DodajJakoZakupAkcja : DodajRekordAkcja<Faktura, FakturaEdytor>
	{
		public override string Nazwa => "➕ Dodaj jako zakup";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;// && zaznaczoneRekordy.Single().Id == 0;

		protected override Faktura UtworzRekord(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
		{
			var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
			var naglowek = zaznaczoneRekordy.Single();
			var xml = "";
			OknoPostepu.Uruchom(async delegate
			{
				using var api = new IO.KSEF.API(podmiot.SrodowiskoKSeF);
				var cts = new CancellationTokenSource();
				cts.CancelAfter(TimeSpan.FromSeconds(10));
				await api.AuthenticateAsync(podmiot.NIP, podmiot.TokenKSeF);
				xml = await api.GetInvoiceAsync(naglowek.NumerKSeF);
				await api.Terminate();
			});
			var faktura = IO.KSEF.Generator.ZbudujDB(kontekst.Baza, xml);
			faktura.NumerKSeF = naglowek.NumerKSeF;
			faktura.DataKSeF = naglowek.DataKSeF;
			kontekst.Baza.Zapisz(faktura);
			return faktura;
		}
	}
}
