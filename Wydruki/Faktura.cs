using Microsoft.Reporting.WinForms;
using ProFak.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
				var pozycje = baza.PozycjeFaktur.Where(pozycja => pozycja.FakturaId == faktura.Id).ToList();
				var wplaty = baza.Wplaty.Where(wplata => wplata.FakturaId == faktura.Id).ToList();
				var zaplacono = wplaty.Sum(wplata => wplata.Kwota);
				var dozaplaty = faktura.RazemBrutto - zaplacono;
				var waluta = baza.Znajdz(faktura.WalutaRef);

				var fakturaDTO = new FakturaDTO();
				if (faktura.Rodzaj == RodzajFaktury.Sprzedaż) fakturaDTO.Tytul = "<b>Faktura VAT</b><br/>";
				else if (faktura.Rodzaj == RodzajFaktury.Proforma) fakturaDTO.Tytul = "<b>Faktura pro forma</b><br/>";
				else if (faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży) fakturaDTO.Tytul = "<b>Korekta faktury VAT</b><br/>";
				else fakturaDTO.Tytul = "<b>" + faktura.Rodzaj + "</b><br/>";

				fakturaDTO.Tytul += faktura.Numer;

				if (faktura.FakturaKorygowanaRef.IsNotNull)
				{
					var fakturaBazowa = baza.Znajdz(faktura.FakturaKorygowanaRef);
					if (fakturaBazowa.Rodzaj == RodzajFaktury.Proforma) fakturaDTO.Podtytul = "do faktury pro forma <b>" + fakturaBazowa.Numer + "</b>";
					else fakturaDTO.Podtytul = "do faktury VAT <b>" + fakturaBazowa.Numer + "</b><br/>z dnia " + fakturaBazowa.DataWystawienia.ToLongDateString() + "<br/>";
				}

				fakturaDTO.Naglowek = "<b>Data wystawienia:</b> " + faktura.DataWystawienia.ToLongDateString()
					+ "<br/><b>Data sprzedaży:</b> " + faktura.DataSprzedazy.ToLongDateString();

				fakturaDTO.DaneNabywcy = faktura.NazwaNabywcy + "<br/>" + faktura.DaneNabywcy.ToString().Replace("\n", "<br/>");
				if (!String.IsNullOrEmpty(faktura.NIPNabywcy)) fakturaDTO.DaneNabywcy += "<br/><b>NIP:</b> " + faktura.NIPNabywcy;

				fakturaDTO.DaneSprzedawcy = faktura.NazwaSprzedawcy + "<br/>" + faktura.DaneSprzedawcy.ToString().Replace("\n", "<br/>");
				if (!String.IsNullOrEmpty(faktura.NIPSprzedawcy)) fakturaDTO.DaneSprzedawcy += "<br/><b>NIP:</b> " + faktura.NIPSprzedawcy;

				if (dozaplaty < 0)
				{
					fakturaDTO.Stopka = "<b>Do zwrotu:</b> " + (-dozaplaty).ToString("n2") + " " + waluta.Skrot + "<br/><b>Słownie:</b> " + SlowniePL.Slownie(-dozaplaty, waluta.Skrot);
				}
				else
				{
					fakturaDTO.Stopka = "<b>Do zapłaty:</b> " + dozaplaty.ToString("n2") + waluta.Skrot
						+ "<br/><b>Słownie:</b> " + SlowniePL.Slownie(dozaplaty, waluta.Skrot)
						+ "<br/><b>Termin płatności:</b> " + faktura.TerminPlatnosci.ToLongDateString()
						+ "<br/><b>Forma płatności:</b> przelew, mechanizm podzielonej płatności";

					if (!String.IsNullOrEmpty(faktura.RachunekBankowy)) fakturaDTO.Stopka += "<br/><b>Numer rachunku:</b> " + faktura.RachunekBankowy;
				}

				if (!String.IsNullOrEmpty(faktura.UwagiPubliczne)) fakturaDTO.Stopka += "<br/><br/>" + faktura.UwagiPubliczne.Replace("\r", "").Replace("\n", "<br/>");

				fakturaDTO.OpisPozycji = "";

				dane.Add(fakturaDTO);

				foreach (var pozycja in pozycje)
				{
					var pozycjaDTO = new FakturaDTO();
					pozycjaDTO.Tytul = fakturaDTO.Tytul;
					pozycjaDTO.Podtytul = fakturaDTO.Podtytul;

					if (faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży)
						pozycjaDTO.NaglowekPozycji = pozycja.Ilosc < 0 ? "Przed korektą" : "Po korekcie";
					else
						pozycjaDTO.NaglowekPozycji = "";

					pozycjaDTO.OpisPozycji = pozycja.Opis;
					pozycjaDTO.CenaNetto = pozycja.Cena;
					pozycjaDTO.Ilosc = Math.Abs(pozycja.Ilosc);
					pozycjaDTO.WartoscNetto = pozycja.WartoscNetto;
					pozycjaDTO.WartoscVat = pozycja.WartoscVat;
					pozycjaDTO.WartoscBrutto = pozycja.WartoscBrutto;
					pozycjaDTO.StawkaVAT = "??";

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
