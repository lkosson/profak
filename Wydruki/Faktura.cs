using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.WinForms;
using ProFak.DB;
using QRCoder;

namespace ProFak.Wydruki;

public class Faktura : Wydruk
{
	private readonly List<FakturaDTO> dane;

	public Faktura(Baza baza, IEnumerable<Ref<DB.Faktura>> fakturyRefs, bool duplikat = false)
	{
		dane = new List<FakturaDTO>();
		foreach (var fakturaRef in fakturyRefs)
		{
			var faktura = baza.Znajdz(fakturaRef);
			var pozycje = baza.PozycjeFaktur
				.Where(pozycja => pozycja.FakturaId == faktura.Id)
				.Include(pozycja => pozycja.StawkaVat)
				.Include(pozycja => pozycja.JednostkaMiary)
				.Include(pozycja => pozycja.Towar).ThenInclude(towar => towar!.JednostkaMiary)
				.OrderBy(pozycja => pozycja.LP)
				.ThenBy(pozycja => pozycja.CzyPrzedKorekta)
				.ToList();
			var odbiorca = baza.DodatkowePodmioty
				.FirstOrDefault(dodatkowyPodmiot => dodatkowyPodmiot.FakturaId == faktura.Id && dodatkowyPodmiot.Rodzaj == RodzajDodatkowegoPodmiotu.Odbiorca);
			var wplaty = baza.Wplaty.Where(wplata => wplata.FakturaId == faktura.Id).ToList();
			var zaplacono = wplaty.Sum(wplata => wplata.Kwota);
			var dozaplaty = faktura.RazemBrutto - zaplacono;
			var waluta = baza.ZnajdzLubNull(faktura.WalutaRef);
			var walutaVAT = baza.Waluty.FirstOrDefault(waluta => waluta.CzyDomyslna);
			var walutaSkrot = waluta?.Skrot ?? "zł";
			var walutaVATSkrot = walutaVAT?.Skrot ?? walutaSkrot;
			var jestvat = pozycje.Any(e => e.StawkaVat != null && !String.Equals(e.StawkaVat.Skrot, "ZW", StringComparison.CurrentCultureIgnoreCase)) && faktura.ProceduraMarzy == ProceduraMarży.NieDotyczy;
			var jestrabat = pozycje.Any(e => e.RabatProcent > 0 || e.RabatCena > 0 || e.RabatWartosc > 0);

			var fakturaDTO = new FakturaDTO();
			if (faktura.Rodzaj == RodzajFaktury.Sprzedaż) fakturaDTO.Rodzaj = jestvat ? "Faktura VAT" : "Faktura";
			else if (faktura.Rodzaj == RodzajFaktury.Rachunek) fakturaDTO.Rodzaj = "Rachunek";
			else if (faktura.Rodzaj == RodzajFaktury.Proforma) fakturaDTO.Rodzaj = "Faktura pro forma";
			else if (faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży) fakturaDTO.Rodzaj = jestvat ? "Korekta faktury VAT" : "Korekta faktury";
			else if (faktura.Rodzaj == RodzajFaktury.KorektaRachunku) fakturaDTO.Rodzaj = "Korekta rachunku";
			else if (faktura.Rodzaj == RodzajFaktury.DowódWewnętrzny) fakturaDTO.Rodzaj = "Dowód wewnętrzny";
			else if (faktura.Rodzaj == RodzajFaktury.VatMarża) fakturaDTO.Rodzaj = "Faktura VAT marża";
			else if (faktura.Rodzaj == RodzajFaktury.KorektaVatMarży) fakturaDTO.Rodzaj = "Korekta faktury VAT marża";
			else fakturaDTO.Rodzaj = faktura.Rodzaj.ToString();

			fakturaDTO.Numer = faktura.Numer;
			fakturaDTO.JestVAT = jestvat;
			fakturaDTO.JestRabat = jestrabat;
			fakturaDTO.Waluta = walutaSkrot;
			fakturaDTO.WalutaVAT = walutaVATSkrot;
			fakturaDTO.KursWaluty = faktura.KursWaluty;
			if (faktura.ProceduraMarzy != ProceduraMarży.NieDotyczy) fakturaDTO.ProceduraMarzy = Rekord.Format(faktura.ProceduraMarzy);

			if (faktura.FakturaPierwotnaRef.IsNotNull)
			{
				var fakturaBazowa = baza.Znajdz(faktura.FakturaPierwotnaRef);
				if (fakturaBazowa.Rodzaj == RodzajFaktury.Proforma) fakturaDTO.Korekta = "do faktury pro forma <b>" + fakturaBazowa.Numer + "</b>";
				else if (fakturaBazowa.Rodzaj == RodzajFaktury.VatMarża) fakturaDTO.Korekta = "<b>do faktury VAT marża</b> " + fakturaBazowa.Numer + "<br/><b>z dnia</b> " + fakturaBazowa.DataWystawienia.ToString(UI.Wyglad.FormatDaty) + "<br/>";
				else fakturaDTO.Korekta = (jestvat ? "<b>do faktury VAT</b> " : "<b>do faktury</b> ") + fakturaBazowa.Numer + "<br/><b>z dnia</b> " + fakturaBazowa.DataWystawienia.ToString(UI.Wyglad.FormatDaty) + "<br/>";
			}

			if (duplikat)
			{
				if (!String.IsNullOrEmpty(fakturaDTO.Korekta)) fakturaDTO.Korekta += "<br/>";
				fakturaDTO.Korekta += "<b>Duplikat z dnia</b> " + DateTime.Now.ToString(UI.Wyglad.FormatDaty);
			}

			fakturaDTO.DataWystawienia = faktura.DataWystawienia.ToString(UI.Wyglad.FormatDaty);
			fakturaDTO.DataSprzedazy = faktura.DataSprzedazy.ToString(UI.Wyglad.FormatDaty);

			fakturaDTO.NazwaNabywcy = faktura.NazwaNabywcy;
			fakturaDTO.AdresNabywcy = faktura.DaneNabywcy;
			fakturaDTO.NIPNabywcy = faktura.NIPNabywcy;

			fakturaDTO.NazwaSprzedawcy = faktura.NazwaSprzedawcy;
			fakturaDTO.AdresSprzedawcy = faktura.DaneSprzedawcy;
			fakturaDTO.NIPSprzedawcy = faktura.NIPSprzedawcy;

			if (odbiorca != null)
			{
				fakturaDTO.DaneOdbiorcy = odbiorca.Nazwa;
				if (!String.IsNullOrEmpty(odbiorca.Adres)) fakturaDTO.DaneOdbiorcy += "<br/>" + odbiorca.Adres.Replace("\n", "<br/>");
				if (!String.IsNullOrEmpty(odbiorca.NIP)) fakturaDTO.DaneOdbiorcy += "<br/><b>NIP:</b> " + odbiorca.NIP;
				if (!String.IsNullOrEmpty(odbiorca.VatUE)) fakturaDTO.DaneOdbiorcy += "<br/><b>Nr VAT UE:</b> " + odbiorca.VatUE;
			}

			if (dozaplaty < 0)
			{
				fakturaDTO.Slownie = SlowniePL.Slownie(-dozaplaty, walutaSkrot);
				fakturaDTO.TerminPlatnosci = "";
				fakturaDTO.FormaPlatnosci = "";
				fakturaDTO.DoZwrotu = (-dozaplaty).ToString(UI.Wyglad.FormatKwoty) + " " + walutaSkrot;
				fakturaDTO.DoZaplaty = "";
				fakturaDTO.NumerRachunku = "";
				fakturaDTO.NazwaBanku = "";
			}
			else
			{
				fakturaDTO.TerminPlatnosci = faktura.TerminPlatnosci.ToString(UI.Wyglad.FormatDaty);
				fakturaDTO.FormaPlatnosci = faktura.OpisSposobuPlatnosci;
				fakturaDTO.Slownie = SlowniePL.Slownie(dozaplaty, walutaSkrot);
				fakturaDTO.DoZwrotu = "";
				fakturaDTO.DoZaplaty = dozaplaty.ToString(UI.Wyglad.FormatKwoty) + " " + walutaSkrot;
				fakturaDTO.NumerRachunku = faktura.RachunekBankowy;
				fakturaDTO.NazwaBanku = faktura.NazwaBanku;
			}

			fakturaDTO.Rozliczenia = "";
			foreach (var wplata in wplaty.OrderBy(e => e.Data).ThenBy(e => e.Id))
			{
				if (!String.IsNullOrEmpty(fakturaDTO.Rozliczenia)) fakturaDTO.Rozliczenia += "<br/>";
				if (wplata.CzyRozliczenie) fakturaDTO.Rozliczenia += "<b>" + wplata.Uwagi + ":</b> " + wplata.Kwota.ToString(UI.Wyglad.FormatKwoty) + " " + walutaSkrot;
				else fakturaDTO.Rozliczenia += "<b>Zapłacono " + wplata.Data.ToString(UI.Wyglad.FormatDaty) + ":</b> " + wplata.Kwota.ToString(UI.Wyglad.FormatKwoty) + " " + walutaSkrot;
			}

			fakturaDTO.NumerKSeF = faktura.NumerKSeF;
			fakturaDTO.Uwagi = faktura.UwagiPubliczne;
			fakturaDTO.KodKSeF = "";

			if (!String.IsNullOrEmpty(faktura.URLKSeF))
			{
				using var qrc = QRCodeGenerator.GenerateQrCode(faktura.URLKSeF, QRCodeGenerator.ECCLevel.Default);
				using var renderer = new PngByteQRCode(qrc);
				var qr = renderer.GetGraphic(20);
				fakturaDTO.KodKSeF = Convert.ToBase64String(qr);
			}

			fakturaDTO.OpisPozycji = "";

			dane.Add(fakturaDTO);

			foreach (var pozycja in pozycje)
			{
				var pozycjaDTO = new FakturaDTO();
				pozycjaDTO.LP = pozycja.LP.ToString();
				pozycjaDTO.Numer = faktura.Numer; // musi tu być - po tym jest grupowanie stron
				pozycjaDTO.JestVAT = jestvat;
				pozycjaDTO.JestRabat = jestrabat;

				if (faktura.Rodzaj == RodzajFaktury.KorektaSprzedaży || faktura.Rodzaj == RodzajFaktury.KorektaVatMarży)
					pozycjaDTO.NaglowekPozycji = pozycja.CzyPrzedKorekta ? "Przed korektą" : "Po korekcie";
				else
					pozycjaDTO.NaglowekPozycji = "";

				var jm = pozycja.JednostkaMiary ?? pozycja.Towar?.JednostkaMiary;

				pozycjaDTO.OpisPozycji = pozycja.Opis;
				pozycjaDTO.CenaNetto = faktura.ProceduraMarzy == ProceduraMarży.NieDotyczy ? pozycja.CenaNetto : pozycja.Cena;
				pozycjaDTO.Ilosc = Math.Abs(pozycja.Ilosc / 1.000000000000m) + " " + jm?.Skrot;
				pozycjaDTO.WartoscNetto = pozycja.WartoscNetto;
				pozycjaDTO.WartoscVat = (pozycja.WartoscVat * faktura.KursWaluty).Zaokragl();
				pozycjaDTO.WartoscBrutto = pozycja.WartoscBrutto;
				pozycjaDTO.StawkaVAT = pozycja.StawkaVat?.Skrot ?? "-";
				pozycjaDTO.Rabat = pozycja.RabatFmt.Replace(", ", "\n") + (pozycja.RabatWartosc > 0 || pozycja.RabatCena > 0 ? " " + walutaSkrot : "");
				pozycjaDTO.RabatRazem = pozycja.RabatRazem;
				pozycjaDTO.Waluta = walutaSkrot;
				pozycjaDTO.WalutaVAT = walutaVATSkrot;

				dane.Add(pozycjaDTO);
			}
		}
	}

	public override void Przygotuj(LocalReport report)
	{
		using var rdlc = WczytajSzablon("Faktura");
		report.DisplayName = String.Join(", ", dane.Select(e => e.Numer).Distinct().Order());
		report.LoadReportDefinition(rdlc);
		report.LoadSubreportDefinition("PozycjeVatRabat", WczytajSzablon("FakturaPozycjeVatRabat"));
		report.LoadSubreportDefinition("PozycjeVat", WczytajSzablon("FakturaPozycjeVat"));
		report.LoadSubreportDefinition("PozycjeRabat", WczytajSzablon("FakturaPozycjeRabat"));
		report.LoadSubreportDefinition("Pozycje", WczytajSzablon("FakturaPozycje"));
		report.SubreportProcessing += SubreportProcessing;
		report.DataSources.Add(new ReportDataSource("DSFaktury", dane));

		void SubreportProcessing(object? sender, SubreportProcessingEventArgs e)
		{
			e.DataSources.Add(report.DataSources[0]);
		}
	}
}
