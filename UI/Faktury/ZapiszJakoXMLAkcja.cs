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
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count() >= 1;
		public override bool CzyKlawiszSkrotu(Keys klawisz, Keys modyfikatory) => modyfikatory == Keys.Control && klawisz == Keys.S;
		public override bool PrzeladujPoZakonczeniu => false;

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
		{
			var podmiot = kontekst.Baza.Kontrahenci.First(kontrahent => kontrahent.CzyPodmiot);
			if (zaznaczoneRekordy.Count() == 1) ZapiszJeden(podmiot, zaznaczoneRekordy.Single());
			else ZapiszWiele(podmiot, zaznaczoneRekordy);
		}

		private void ZapiszJeden(Kontrahent podmiot, Faktura naglowek)
		{
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

		private void ZapiszWiele(Kontrahent podmiot, IEnumerable<Faktura> naglowki)
		{
			using var dialog = new FolderBrowserDialog();
			dialog.Description = "Wybierz katalog, do którego mają zostać zapisane pliki.";
			dialog.AutoUpgradeEnabled = false;
			if (dialog.ShowDialog() != DialogResult.OK) return;
			var katalog = dialog.SelectedPath;

			OknoPostepu.Uruchom(async cancellationToken =>
			{
				using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
				await api.UwierzytelnijAsync(podmiot.NIP, podmiot.TokenKSeF, cancellationToken);
				foreach (var naglowek in naglowki)
				{
					if (cancellationToken.IsCancellationRequested) break;
					var xml = await api.PobierzFaktureAsync(naglowek.NumerKSeF, cancellationToken);
					var plik = Path.Combine(katalog, naglowek.NumerKSeF) + ".xml";
					File.WriteAllText(plik, xml);
					File.SetLastWriteTime(plik, naglowek.DataKSeF ?? naglowek.DataWystawienia);
				}
			});
		}
	}
}
