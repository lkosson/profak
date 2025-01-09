using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WinForms;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;

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
				var jestrabat = pozycje.Any(e => e.RabatProcent > 0 || e.RabatCena > 0 || e.RabatWartosc > 0);

				var fakturaDTO = new FakturaDTO();
				if (faktura.Rodzaj == RodzajFaktury.Sprzedaż) fakturaDTO.Rodzaj = jestvat ? "Faktura VAT" : "Faktura";
				else if (faktura.Rodzaj == RodzajFaktury.Proforma) fakturaDTO.Rodzaj = "Faktura pro forma";
				else if (faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży) fakturaDTO.Rodzaj = jestvat ? "Korekta faktury VAT" : "Korekta faktury";
				else if (faktura.Rodzaj == RodzajFaktury.DowódWewnętrzny) fakturaDTO.Rodzaj = "Dowód wewnętrzny";
				else fakturaDTO.Rodzaj = faktura.Rodzaj.ToString();

				fakturaDTO.Numer = faktura.Numer;
				fakturaDTO.JestVAT = jestvat;
				fakturaDTO.JestRabat = jestrabat;

				if (faktura.FakturaKorygowanaRef.IsNotNull)
				{
					var fakturaBazowa = baza.Znajdz(faktura.FakturaKorygowanaRef);
					if (fakturaBazowa.Rodzaj == RodzajFaktury.Proforma) fakturaDTO.Korekta = "do faktury pro forma <b>" + fakturaBazowa.Numer + "</b>";
					else fakturaDTO.Korekta = (jestvat ? "do faktury VAT <b>" : "do faktury <b>") + fakturaBazowa.Numer + "</b><br/>z dnia " + fakturaBazowa.DataWystawienia.ToString(UI.Format.Data) + "<br/>";
				}

				fakturaDTO.DataWystawienia = faktura.DataWystawienia.ToString(UI.Format.Data);
				fakturaDTO.DataSprzedazy = faktura.DataSprzedazy.ToString(UI.Format.Data);

				fakturaDTO.NazwaNabywcy = faktura.NazwaNabywcy;
				fakturaDTO.AdresNabywcy = faktura.DaneNabywcy;
				fakturaDTO.NIPNabywcy = faktura.NIPNabywcy;

				fakturaDTO.NazwaSprzedawcy = faktura.NazwaSprzedawcy;
				fakturaDTO.AdresSprzedawcy = faktura.DaneSprzedawcy;
				fakturaDTO.NIPSprzedawcy = faktura.NIPSprzedawcy;

				if (dozaplaty < 0)
				{
					fakturaDTO.Slownie = SlowniePL.Slownie(-dozaplaty, waluta.Skrot);
					fakturaDTO.TerminPlatnosci = "";
					fakturaDTO.FormaPlatnosci = "";
					fakturaDTO.DoZwrotu = (-dozaplaty).ToString(UI.Format.Kwota) + " " + waluta.Skrot;
					fakturaDTO.DoZaplaty = "";
					fakturaDTO.NumerRachunku = "";
				}
				else
				{
					fakturaDTO.TerminPlatnosci = faktura.TerminPlatnosci.ToString(UI.Format.Data);
					fakturaDTO.FormaPlatnosci = faktura.OpisSposobuPlatnosci;
					fakturaDTO.Slownie = SlowniePL.Slownie(dozaplaty, waluta.Skrot);
					fakturaDTO.DoZwrotu = "";
					fakturaDTO.DoZaplaty = dozaplaty.ToString(UI.Format.Kwota) + " " + waluta.Skrot;
					fakturaDTO.NumerRachunku = faktura.RachunekBankowy;
				}

				fakturaDTO.NumerKSeF = faktura.NumerKSeF;
				fakturaDTO.Uwagi = faktura.UwagiPubliczne;
				fakturaDTO.KodKSeF = "";

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
				}
				*/

				fakturaDTO.OpisPozycji = "";

				dane.Add(fakturaDTO);

				foreach (var pozycja in pozycje)
				{
					var pozycjaDTO = new FakturaDTO();
					pozycjaDTO.LP = pozycja.LP.ToString();
					pozycjaDTO.Numer = faktura.Numer; // musi tu być - po tym jest grupowanie stron
					pozycjaDTO.JestVAT = jestvat;
					pozycjaDTO.JestRabat = jestrabat;

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
					pozycjaDTO.Rabat = pozycja.RabatFmt.Replace(", ", "\n");
					pozycjaDTO.RabatRazem = pozycja.RabatRazem;

					dane.Add(pozycjaDTO);
				}
			}
		}

		public override void Przygotuj(LocalReport report)
		{
			using var rdlc = WczytajSzablon("Faktura");
			report.LoadReportDefinition(rdlc);
			report.DataSources.Add(new ReportDataSource("DSFaktury", dane));
		}
	}
}
