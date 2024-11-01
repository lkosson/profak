using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WinForms;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Windows.Compatibility;

namespace ProFak.Wydruki
{
	class Faktura : Wydruk
	{
		private readonly List<FakturaDTO> dane;

		public Faktura(Baza baza, IEnumerable<Ref<DB.Faktura>> fakturyRefs)
		{
			dane = new List<FakturaDTO>();
			foreach (var fakturaRef in fakturyRefs)
			{
				var faktura = baza.Znajdz(fakturaRef);
				var pozycje = baza.PozycjeFaktur
					.Where(pozycja => pozycja.FakturaId == faktura.Id)
					.Include(pozycja => pozycja.StawkaVat)
					.Include(pozycja => pozycja.Towar).ThenInclude(towar => towar.JednostkaMiary)
					.OrderBy(pozycja => pozycja.LP)
					.ThenBy(pozycja => pozycja.CzyPrzedKorekta)
					.ToList();
				var wplaty = baza.Wplaty.Where(wplata => wplata.FakturaId == faktura.Id).ToList();
				var zaplacono = wplaty.Sum(wplata => wplata.Kwota);
				var dozaplaty = faktura.RazemBrutto - zaplacono;
				var waluta = baza.Znajdz(faktura.WalutaRef);
				var jestvat = pozycje.Any(e => e.StawkaVat != null && !String.Equals(e.StawkaVat.Skrot, "ZW", StringComparison.CurrentCultureIgnoreCase));

				var fakturaDTO = new FakturaDTO();
				if (faktura.Rodzaj == RodzajFaktury.Sprzedaż) fakturaDTO.Tytul = jestvat ? "<b>Faktura VAT</b><br/>" : "<b>Faktura</b><br/>";
				else if (faktura.Rodzaj == RodzajFaktury.Proforma) fakturaDTO.Tytul = "<b>Faktura pro forma</b><br/>";
				else if (faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży) fakturaDTO.Tytul = jestvat ? "<b>Korekta faktury VAT</b><br/>" : "<b>Korekta faktury</b><br/>";
				else fakturaDTO.Tytul = "<b>" + faktura.Rodzaj + "</b><br/>";

				fakturaDTO.Tytul += faktura.Numer;
				fakturaDTO.JestVAT = jestvat;

				if (faktura.FakturaKorygowanaRef.IsNotNull)
				{
					var fakturaBazowa = baza.Znajdz(faktura.FakturaKorygowanaRef);
					if (fakturaBazowa.Rodzaj == RodzajFaktury.Proforma) fakturaDTO.Podtytul = "do faktury pro forma <b>" + fakturaBazowa.Numer + "</b>";
					else fakturaDTO.Podtytul = (jestvat ? "do faktury VAT <b>" : "do faktury <b>") + fakturaBazowa.Numer + "</b><br/>z dnia " + fakturaBazowa.DataWystawienia.ToString(UI.Format.Data) + "<br/>";
				}

				fakturaDTO.Naglowek = "<b>Data wystawienia:</b> " + faktura.DataWystawienia.ToString(UI.Format.Data)
					+ "<br/><b>Data sprzedaży:</b> " + faktura.DataSprzedazy.ToString(UI.Format.Data);

				fakturaDTO.DaneNabywcy = faktura.NazwaNabywcy + "<br/>" + faktura.DaneNabywcy.ToString().Replace("\n", "<br/>");
				if (!String.IsNullOrEmpty(faktura.NIPNabywcy)) fakturaDTO.DaneNabywcy += "<br/><b>NIP:</b> " + faktura.NIPNabywcy;

				fakturaDTO.DaneSprzedawcy = faktura.NazwaSprzedawcy + "<br/>" + faktura.DaneSprzedawcy.ToString().Replace("\n", "<br/>");
				if (!String.IsNullOrEmpty(faktura.NIPSprzedawcy)) fakturaDTO.DaneSprzedawcy += "<br/><b>NIP:</b> " + faktura.NIPSprzedawcy;

				if (dozaplaty < 0)
				{
					fakturaDTO.Stopka = "<b>Do zwrotu:</b> " + (-dozaplaty).ToString(UI.Format.Kwota) + " " + waluta.Skrot + "<br/><b>Słownie:</b> " + SlowniePL.Slownie(-dozaplaty, waluta.Skrot);
				}
				else
				{
					fakturaDTO.Stopka = "<b>Do zapłaty:</b> " + dozaplaty.ToString(UI.Format.Kwota) + " " + waluta.Skrot
						+ "<br/><b>Słownie:</b> " + SlowniePL.Slownie(dozaplaty, waluta.Skrot)
						+ "<br/><b>Termin płatności:</b> " + faktura.TerminPlatnosci.ToString(UI.Format.Data)
						+ "<br/><b>Forma płatności:</b> " + faktura.OpisSposobuPlatnosci;

					if (!String.IsNullOrEmpty(faktura.RachunekBankowy)) fakturaDTO.Stopka += "<br/><b>Numer rachunku:</b> " + faktura.RachunekBankowy;
				}

				if (!String.IsNullOrEmpty(faktura.NumerKSeF)) fakturaDTO.Stopka += "<br/><b>Numer KSeF:</b> " + faktura.NumerKSeF;
				if (!String.IsNullOrEmpty(faktura.UwagiPubliczne)) fakturaDTO.Stopka += "<br/><br/>" + faktura.UwagiPubliczne.Replace("\r", "").Replace("\n", "<br/>");

				/*
				if (!String.IsNullOrEmpty(faktura.NumerKSeF))
				{
					var writer = new BarcodeWriter();
					writer.Options.Margin = 0;
					writer.Options.NoPadding = true;
					writer.Format = BarcodeFormat.QR_CODE;
					var qrKSeF = writer.WriteAsBitmap(faktura.URLKSeF);
					var ms = new MemoryStream();
					qrKSeF.Save(ms, ImageFormat.Png);
					fakturaDTO.KodKSeF = Convert.ToBase64String(ms.ToArray());
					fakturaDTO.NumerKSeF = faktura.NumerKSeF;
				}
				*/

				fakturaDTO.OpisPozycji = "";

				dane.Add(fakturaDTO);

				foreach (var pozycja in pozycje)
				{
					var pozycjaDTO = new FakturaDTO();
					pozycjaDTO.LP = pozycja.LP.ToString();
					pozycjaDTO.Tytul = fakturaDTO.Tytul;
					pozycjaDTO.Podtytul = fakturaDTO.Podtytul;
					pozycjaDTO.JestVAT = jestvat;

					if (faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży)
						pozycjaDTO.NaglowekPozycji = pozycja.CzyPrzedKorekta ? "Przed korektą" : "Po korekcie";
					else
						pozycjaDTO.NaglowekPozycji = "";

					pozycjaDTO.OpisPozycji = pozycja.Opis;
					pozycjaDTO.CenaNetto = pozycja.Cena;
					pozycjaDTO.Ilosc = Math.Abs(pozycja.Ilosc / 1.000000000000m) + " " + pozycja.Towar?.JednostkaMiary?.Skrot;
					pozycjaDTO.WartoscNetto = pozycja.WartoscNetto;
					pozycjaDTO.WartoscVat = pozycja.WartoscVat;
					pozycjaDTO.WartoscBrutto = pozycja.WartoscBrutto;
					pozycjaDTO.StawkaVAT = pozycja.StawkaVat?.Skrot ?? "-";

					dane.Add(pozycjaDTO);
				}
			}
		}

		public override void Przygotuj(LocalReport report)
		{
			var asm = Assembly.GetCallingAssembly();
			var rdlc = asm.GetManifestResourceStream("ProFak.Wydruki.Faktura.rdlc");
			report.LoadReportDefinition(rdlc);
			report.DataSources.Add(new ReportDataSource("DSFaktury", dane));
		}
	}
}
