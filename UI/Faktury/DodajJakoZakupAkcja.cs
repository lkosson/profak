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
		public override string Nazwa => "➕ Dodaj jako zakup [INS]";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;// && zaznaczoneRekordy.Single().Id == 0;
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.None && klawisz == Keys.Insert;

		protected override Faktura UtworzRekord(Kontekst kontekst, IEnumerable<Faktura> zaznaczoneRekordy)
		{
			var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
			var naglowek = zaznaczoneRekordy.Single();
			var xml = "";
			OknoPostepu.Uruchom(async cancellationToken =>
			{
				using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
				await api.AuthenticateAsync(podmiot.NIP, podmiot.TokenKSeF, cancellationToken);
				xml = await api.GetInvoiceAsync(naglowek.NumerKSeF, cancellationToken);
				await api.Terminate();
				cancellationToken.ThrowIfCancellationRequested();
			});
			var faktura = IO.FA_3.Generator.ZbudujDB(kontekst.Baza, xml);
			faktura.NumerKSeF = naglowek.NumerKSeF;
			faktura.DataKSeF = naglowek.DataKSeF;
			kontekst.Baza.Zapisz(faktura);
			return faktura;
		}
	}
}
