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
			var kontrahent = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
			var naglowek = zaznaczoneRekordy.Single();
			var xml = System.IO.File.ReadAllText("e:\\1.xml");
			/*
			OknoPostepu.Uruchom(async delegate
			{
				using var api = new IO.KSEF.API(false);
				var cts = new CancellationTokenSource();
				cts.CancelAfter(TimeSpan.FromSeconds(10));
				await api.AuthenticateAsync(kontrahent.NIP, kontrahent.TokenKSeF);
				xml = await api.GetInvoiceAsync(naglowek.NumerKSeF);
				await api.Terminate();
			});
			*/
			var faktura = IO.KSEF.Generator.ZbudujDB(kontekst.Baza, xml);
			faktura.NumerKSeF = naglowek.NumerKSeF;
			kontekst.Baza.Zapisz(faktura);
			return faktura;
		}
	}
}
