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

		protected string WybierzPlik(string numerKSeF)
		{
			using var dialog = new SaveFileDialog();
			dialog.Title = "Zapisywanie pliku";
			dialog.RestoreDirectory = true;
			dialog.FileName = numerKSeF + ".xml";
			if (dialog.ShowDialog() != DialogResult.OK) return null;
			return dialog.FileName;
		}

		protected string WybierzKatalog()
		{
			using var dialog = new FolderBrowserDialog();
			dialog.Description = "Wybierz katalog, do którego mają zostać zapisane pliki.";
			dialog.AutoUpgradeEnabled = false;
			if (dialog.ShowDialog() != DialogResult.OK) return null;
			return dialog.SelectedPath;
		}

		protected void ZapiszXml(string plik, Faktura naglowek, string xml)
		{
			File.WriteAllText(plik, xml);
			File.SetLastWriteTime(plik, naglowek.DataKSeF ?? naglowek.DataWystawienia);
		}

		private void ZapiszJeden(Kontrahent podmiot, Faktura naglowek)
		{
			var plik = WybierzPlik(naglowek.NumerKSeF);
			if (plik == null) return;
			var xml = "";
			OknoPostepu.Uruchom(async cancellationToken =>
			{
				using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
				await api.UwierzytelnijAsync(podmiot.NIP, podmiot.TokenKSeF, cancellationToken);
				xml = await api.PobierzFaktureAsync(naglowek.NumerKSeF, cancellationToken);
			});

			ZapiszXml(plik, naglowek, xml);
		}

		private void ZapiszWiele(Kontrahent podmiot, IEnumerable<Faktura> naglowki)
		{
			var katalog = WybierzKatalog();
			if (katalog == null) return;

			OknoPostepu.Uruchom(async cancellationToken =>
			{
				using var api = new IO.KSEF2.API(podmiot.SrodowiskoKSeF);
				await api.UwierzytelnijAsync(podmiot.NIP, podmiot.TokenKSeF, cancellationToken);
				foreach (var naglowek in naglowki)
				{
					if (cancellationToken.IsCancellationRequested) break;
					var xml = await api.PobierzFaktureAsync(naglowek.NumerKSeF, cancellationToken);
					var plik = Path.Combine(katalog, naglowek.NumerKSeF) + ".xml";
					ZapiszXml(plik, naglowek, xml);
				}
			});
		}
	}

	class ZapiszJakoXMLLokalneAkcja : ZapiszJakoXMLAkcja
	{
		public override string Nazwa => "🖫 Zapisz KSeF XML [CTRL-S]";
		public override bool CzyDostepnaDlaRekordow(IEnumerable<Faktura> zaznaczoneRekordy) => zaznaczoneRekordy.Count(e => e.CzySprzedaz || !String.IsNullOrEmpty(e.XMLKSeF)) >= 1;

		public override void Uruchom(Kontekst kontekst, ref IEnumerable<Faktura> zaznaczoneRekordy)
		{
			if (zaznaczoneRekordy.Count() == 1) ZapiszJeden(kontekst, zaznaczoneRekordy.Single());
			else ZapiszWiele(kontekst, zaznaczoneRekordy);
		}

		private string Numer(Faktura faktura)
		{
			return String.IsNullOrEmpty(faktura.NumerKSeF) ? faktura.Numer.Replace('/', '-').Replace('\\', '-') : faktura.NumerKSeF;
		}

		private void ZapiszJeden(Kontekst kontekst, Faktura faktura)
		{
			var plik = WybierzPlik(Numer(faktura));
			if (plik == null) return;
			var xml = faktura.XMLKSeF;
			if (String.IsNullOrEmpty(xml) && faktura.CzySprzedaz) xml = IO.FA_3.Generator.ZbudujXML(kontekst.Baza, faktura);
			ZapiszXml(plik, faktura, xml);
		}

		private void ZapiszWiele(Kontekst kontekst, IEnumerable<Faktura> faktury)
		{
			var katalog = WybierzKatalog();
			if (katalog == null) return;

			OknoPostepu.Uruchom(async cancellationToken =>
			{
				foreach (var faktura in faktury)
				{
					if (cancellationToken.IsCancellationRequested) break;
					var xml = faktura.XMLKSeF;
					if (String.IsNullOrEmpty(xml) && faktura.CzySprzedaz) xml = IO.FA_3.Generator.ZbudujXML(kontekst.Baza, faktura);
					var plik = Path.Combine(katalog, Numer(faktura)) + ".xml";
					ZapiszXml(plik, faktura, xml);
				}
			});
		}
	}
}
