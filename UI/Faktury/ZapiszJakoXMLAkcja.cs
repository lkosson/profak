using ProFak.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ProFak.UI
{
	class ZapiszJakoXMLAkcja : AkcjaNaSpisie<Faktura>
	{
		public override string Nazwa => "🖫 Zapisz jako XML [CTRL-S]";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() == 1;// && zaznaczoneRekordy.Single().Id == 0;
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.Control && klawisz == Keys.S;

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
		{
			var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
			var naglowek = zaznaczoneRekordy.Single();

			using var dialog = new SaveFileDialog();
			dialog.Title = "Zapisywanie pliku";
			dialog.RestoreDirectory = true;
			dialog.FileName = naglowek.NumerKSeF + ".xml";
			if (dialog.ShowDialog() != DialogResult.OK) return;

			var xml = "";
			OknoPostepu.Uruchom(async cancellationToken =>
			{
				using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
				await api.UwierzytelnijAsync(podmiot.NIP, podmiot.TokenKSeF, cancellationToken);
				xml = await api.PobierzFaktureAsync(naglowek.NumerKSeF, cancellationToken);
			});

			File.WriteAllText(dialog.FileName, xml);
		}
	}
}
